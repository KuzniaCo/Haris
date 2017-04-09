using Caliburn.Micro;
using Haris.Core.Events.MySensors;
using Haris.Core.Services.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Haris.Core.Modules.Endpoint
{
    public class EndpointSocketModule : HarisModuleBase<AttributedMessageEvent>
    {
        private readonly IEventAggregator _eventAggregator;

        private List<Socket> Endpoints = new List<Socket>();

        public EndpointSocketModule(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public override void Dispose()
        {
            //TODO throw new NotImplementedException();
        }

        public override void Init()
        {
            Task.Run(() =>
            {
                StartListening();
            });

        }

        public override void Handle(AttributedMessageEvent message)
        {

        }

        public void StartListening()
        {
            // Establish the local endpoint for the socket.  
            // Dns.GetHostName returns the name of the   
            // host running the application.  
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());

#if !DEBUG
            IPAddress ipAddress = IPAddress.Parse("193.70.84.40");
#endif
#if DEBUG
            IPAddress ipAddress = ipHostInfo.AddressList[1];
#endif
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            // Create a TCP/IP socket.  
            Socket listener = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and   
            // listen for incoming connections.  
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(10);

                // Start listening for connections.  
                while (true)
                { 
                    Socket handler = listener.Accept();

                    Endpoints.Add(handler);
                    Task.Run(() =>
                    {
                        handler.ReceiveTimeout = 900000;

                        while (true)
                        {
                            byte[]  bytes = new byte[1024];
                            try
                            {
                                int bytesRec = handler.Receive(bytes);
                                var data = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                                _eventAggregator.Publish(new MessageReceivedEvent(data));
                                Console.WriteLine("Text received : {0}", data);

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
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();

        }

        public static bool IsConnected(Socket client)
        {
            // This is how you can determine whether a socket is still connected.
            bool blockingState = client.Blocking;

            try
            {
                byte[] tmp = new byte[1];

                client.Blocking = false;
                client.Send(tmp, 0, 0);
                return true;
            }
            catch (SocketException e)
            {
                // 10035 == WSAEWOULDBLOCK
                if (e.NativeErrorCode.Equals(10035))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally
            {
                client.Blocking = blockingState;
            }
        }
        private bool IsSocketConnected(Socket s)
        {
            return !((s.Poll(1000, SelectMode.SelectRead) && (s.Available == 0)) || !s.Connected);

            /* The long, but simpler-to-understand version:

                    bool part1 = s.Poll(1000, SelectMode.SelectRead);
                    bool part2 = (s.Available == 0);
                    if ((part1 && part2 ) || !s.Connected)
                        return false;
                    else
                        return true;

            */
        }
    }
}