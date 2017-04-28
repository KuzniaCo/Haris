using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Haris.Core.Cubes;
using Haris.Core.Events.MySensors;
using Haris.Core.Helpers;
using Haris.Core.Services.Logging;
using Haris.DataModel.DataModels;
using Haris.DataModel.Repositories;

namespace Haris.Core.Services
{
    public class EngineService
    {
        private Socket _connectedSocketEndpoint;

        private SerialPort _serialEndpoint;

        public readonly ICubeRepository _cubeRepository;

        public EngineService(ICubeRepository cubeRepository)
        {
            _cubeRepository = cubeRepository;
        }

        public void ProccessMessage(MessageReceivedEvent message)
        {
            Logger.LogPrompt("Recived message: " + message.Payload);
            var decodedMessage = DecodeMessage(message.Payload);
            var address = decodedMessage[0];
            

            if (address.Length == 0)
            {
                var cubeType = decodedMessage[2];
                var tempAddress = decodedMessage[1];
                RegisterNewCube(tempAddress, cubeType);
            }
            else
            {
                var engineCube = CreateDeliveryCube(address);
                engineCube.ProcessMessage(message.Payload);
            }
        }

        public List<string> DecodeMessage(string message)
        {
            String[] messageItems = message.Split(new char[] { '|' });
            return messageItems.ToList();
        }

        public void RegisterNewCube(string tempAddress, string cubeType)
        {
            Logger.LogInfo("Starting register new "+ cubeType);
            Cube newDevice = new Cube()
            {
                CubeType = cubeType,
                Name = ""
            };
            _cubeRepository.CreateCube(newDevice);
            var hashids = new Hashids("HarisIotHub", 6);
            newDevice.CubeAddress = hashids.Encode(newDevice.Id);
            
            _cubeRepository.UpdateCube(newDevice);
            Logger.LogInfo("New device was addressed. Address is:  " + newDevice.CubeAddress);
            SendMessage(tempAddress+"|"+newDevice.CubeAddress);
            Logger.LogInfo("New address was send to device.");
        }

        public void SendMessage(string message)
        {
            Logger.LogInfo("Send message: " + message);
            byte[] msg = Encoding.ASCII.GetBytes(message);
            try
            {
                _connectedSocketEndpoint.Send(msg);
                //_serialEndpoint.Write(message);
            }
            catch (NullReferenceException)
            {
                
                Logger.LogError("Any endpoint is not connected");
            }
        }

        public BaseCube CreateDeliveryCube(string address)
        {
            Cube addressedCube = _cubeRepository.GetCube(address);
            if (addressedCube == null)
            {
                Logger.LogError("Not found cube addressed: " + address);
            }
            var cubeType = GetType().Assembly.GetTypes()
                .FirstOrDefault(x => x.Name.Contains(addressedCube.CubeType));
            Object[] args = { addressedCube, _cubeRepository, this };
            BaseCube cube = (BaseCube)Activator.CreateInstance(cubeType, args);
            return cube;
        }

        public void OpenSerialPort(int boudRate, string portName)
        {
            Task.Run(() =>
            {
                _serialEndpoint = new SerialPort(portName)
                {
                    BaudRate = boudRate,
                };
                _serialEndpoint.Open();
                Logger.LogPrompt("Gateway connected");
                while (true)
                {
                    var message = _serialEndpoint.ReadLine();
                    ProccessMessage(new MessageReceivedEvent(message));
                }
            }, new CancellationToken());
        }

        public void StartSocketServer()
        {
#if !DEBUG
            IPAddress ipAddress = IPAddress.Parse("193.70.84.40");
#endif
#if DEBUG
            IPAddress ipAddress = IPAddress.Parse("192.168.0.4");
#endif
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            Socket listener = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(10);

                while (true)
                {
                    _connectedSocketEndpoint = listener.Accept();

                    Task.Run(() =>
                    {
                        while (true)
                        {
                            byte[] bytes = new byte[1024];
                            try
                            {
                                int bytesRec = _connectedSocketEndpoint.Receive(bytes);
                                var data = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                                ProccessMessage(new MessageReceivedEvent(data));
                            }
                            catch (SocketException se)
                            {
                                break;
                            }
                        }
                    });
                }

            }
            catch (Exception e)
            {
                Logger.LogError(e.Message.ToString());
            }

        }
    }
}
