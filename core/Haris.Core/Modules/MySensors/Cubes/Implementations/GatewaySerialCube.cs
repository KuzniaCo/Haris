using System;
using System.IO.Ports;
using Caliburn.Micro;
using Haris.Core.Events.MySensors;
using Haris.DataModel.MySensors;

namespace Haris.Core.Modules.MySensors.Cubes.Implementations
{
    public sealed class GatewaySerialCube : IGatewayCube
    {
        private readonly IEventAggregator _eventAggregator;
        private SerialPort _serialPort;
        private readonly int _baudRate;
        private readonly string _portName;

        public GatewaySerialCube(int baudRate, string portName, IEventAggregator eventAggregator)
        {
            _baudRate = baudRate;
            _portName = portName;
            _eventAggregator = eventAggregator;
        }

        public void Connect()
        {
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
                _serialPort.Open();
                _eventAggregator.Publish(new ConnectedGatewayEvent("GATEWAY IS READY ON "+ _portName));
            }
            catch (Exception ex)
            {
                    //TODO:Add ErrorConnectionEvent
            }
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

        public void SendMessage(MySensorsMessage message)
        {
            throw new NotImplementedException();
        }
    }
}