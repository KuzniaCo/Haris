using System;
using System.IO.Ports;
using System.Threading;
using Caliburn.Micro;
using Haris.Core.Events.MySensors;
using Haris.Core.Services.Logging;

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
			Logger.LogInfo("Dispose Endpoint module");
			if (_serialPort != null)
			{
				_serialPort.Close();
				_serialPort.Dispose();
			}
		}

		public override void Init()
		{
			_eventAggregator.Subscribe(this);
			RunInBusyContextWithErrorFeedback(() =>
			{
				_baudRate = 115200;
				_portName = "COM3";

				try
				{
					_serialPort = new SerialPort(_portName)
					{
						BaudRate = _baudRate,
					};
					_serialPort.Open();
					Logger.LogPrompt("Gateway connected");
					while (true)
					{
						var message = _serialPort.ReadLine();
						_eventAggregator.Publish(new MessageReceivedEvent(message));
					}
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
			_serialPort.WriteLine(message.Payload);
		}

		private void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
		{
			string data = _serialPort.ReadExisting();
			_eventAggregator.Publish(new MessageReceivedEvent(data));
		}
	}
}