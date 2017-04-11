using System;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using Haris.Core.Events.MySensors;
using Haris.Core.Services;
using Haris.Core.Services.Logging;

namespace Haris.Core.Modules.Endpoint
{
    [DisableModule]
    public class EndpointModule : HarisModuleBase<AttributedMessageEvent>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly EngineService _engineService;
        private SerialPort _serialPort;
        private int _baudRate;
        private string _portName;
        private readonly CancellationTokenSource _cts;

        public EndpointModule(IEventAggregator eventAggregator, EngineService engineService)
        {
            _eventAggregator = eventAggregator;
            _engineService = engineService;
            _cts = new CancellationTokenSource();
        }

        public override void Dispose()
        {
            Logger.LogInfo("Dispose Endpoint module");
            if (_serialPort != null)
            {
                _serialPort.Close();
                _serialPort.Dispose();
            }
        }

        public override void Init()
        {
            Task.Run(() =>
            {
 

                _eventAggregator.Subscribe(this);
                _baudRate = 115200;
                _portName = "COM3";
                    _serialPort = new SerialPort(_portName)
                    {
                        BaudRate = _baudRate,
                    };
                    _serialPort.Open();
                    Logger.LogPrompt("Gateway connected");
                    while (true)
                    {
                        var message = _serialPort.ReadLine();
                        //_eventAggregator.Publish(new MessageReceivedEvent(message));
                        _engineService.ProccessMessage(new MessageReceivedEvent(message));
                    }
                
 
              
            }, _cts.Token);

        }

        public override void Handle(AttributedMessageEvent message)
        {
            _serialPort.WriteLine(message.Payload);
        }

        private void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string data = _serialPort.ReadExisting();
            _eventAggregator.Publish(new MessageReceivedEvent(data));
        }
    }
}