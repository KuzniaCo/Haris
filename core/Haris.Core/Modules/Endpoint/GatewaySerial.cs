using System;
using System.IO.Ports;
using Caliburn.Micro;
using Haris.Core.Events.MySensors;
using Haris.Core.Services.Logging;

namespace Haris.Core.Modules.Endpoint
{
    public sealed class GatewaySerial : IGateway
    {
        private readonly IEventAggregator _eventAggregator;
        private SerialPort _serialPort;
        private readonly int _baudRate;
        private readonly string _portName;

        public GatewaySerial(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

        }

        public void Connect()
        {

        }

        private void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string data = _serialPort.ReadExisting();
            _eventAggregator.Publish(new MessageReceivedEvent(data));
        }

        public void Disconnect()
        {
            _serialPort?.Dispose();
            _serialPort = null;
        }

        public void SendMessage(string message)
        {
            _serialPort.WriteLine(message);
        }
    }
}