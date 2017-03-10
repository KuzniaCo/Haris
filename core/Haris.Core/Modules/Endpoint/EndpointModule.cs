using System;
using System.IO.Ports;
using System.Runtime.Remoting.Channels;
using Caliburn.Micro;
using Haris.Core.Events.MySensors;
using Haris.Core.Services.Logging;
using Haris.DataModel.Repositories;

namespace Haris.Core.Modules.Endpoint
{
    public class EndpointModule : HarisModuleBase<AttributedMessageEvent>
    {
        private readonly IEventAggregator _eventAggregator;
        private SerialPort _serialPort;
        private int _baudRate;
        private string _portName;

        public EndpointModule(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public override void Dispose()
        {
            throw new NotImplementedException();
        }

        public override void Init()
        {
            _eventAggregator.Subscribe(this);
            _baudRate = 115200;
            _portName = "COM4";

            try
            {
                _serialPort = new SerialPort(_portName)
                {
                    BaudRate = _baudRate,
                    Parity = Parity.None,
                    StopBits = StopBits.One,
                    DataBits = 8
                };
                _serialPort.DataReceived += OnDataReceived;
                _serialPort.Disposed += (sender, args) => { Logger.LogError("Dispose Serial"); };

                _serialPort.Open();
                var foo = _serialPort.ReadLine();

                //_eventAggregator.Publish(new ConnectedGatewayEvent("GATEWAY IS READY ON " + _portName));
                Logger.LogPrompt("Gateway connected");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
            }
            Logger.LogPrompt("EndpointModule ready");
            var rep = new CubeRepository();
            var cube = rep.GetCube("ad5ft");
            Logger.LogPrompt(cube.CubeType);
        }

        public override void Handle(AttributedMessageEvent message)
        {
            _serialPort.WriteLine("Test");
        }

        private void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string data = _serialPort.ReadExisting();
            _eventAggregator.Publish(new MessageReceivedEvent(data));
        }
    }
}