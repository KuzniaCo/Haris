using System;
using System.IO.Ports;
using System.Runtime.Remoting.Channels;
using System.Threading;
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
        private readonly CancellationTokenSource _cts;

        public EndpointModule(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _cts = new CancellationTokenSource();
        }

        public override void Dispose()
        {
            Logger.LogInfo("Dispose Endpin");
        }

        public override void Init()
        {
            RunInBusyContextWithErrorFeedback(() =>
            {

                _eventAggregator.Subscribe(this);
                _baudRate = 115200;
                _portName = "COM5";

                try
                {
                    _serialPort = new SerialPort(_portName)
                    {
                        BaudRate = _baudRate,
                    };
                    _serialPort.DataReceived += OnDataReceived;
                    _serialPort.Disposed += (sender, args) => { Logger.LogError("Dispose Serial"); };

                    _serialPort.Open();

                    _eventAggregator.Publish(new ConnectedGatewayEvent("GATEWAY IS READY ON " + _portName));
                    Logger.LogPrompt("Gateway connected");
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex.Message);
                }
                Logger.LogPrompt("EndpointModule ready");
            }, _cts.Token);

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