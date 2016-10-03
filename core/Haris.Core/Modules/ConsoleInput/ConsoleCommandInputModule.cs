using Caliburn.Micro;
using Haris.Core.Events.Command;
using Haris.Core.Events.System;
using Haris.Core.Services.Gpio;
using Haris.Core.Services.Logging;
using Haris.Core.Services.Luis;
using System;
using System.Threading;

namespace Haris.Core.Modules.ConsoleInput
{
	public class ConsoleCommandInputModule: HarisModuleBase<GpioStatusChangeEvent>
	{
		private readonly IEventAggregator _eventAggregator;
		private readonly IIntentToActionConversionService _intentToActionConversionService;
		private readonly IGpioOutputService _gpioOutputService;
		private readonly CancellationTokenSource _cts;

		public ConsoleCommandInputModule(IEventAggregator eventAggregator,
			IIntentToActionConversionService intentToActionConversionService, IGpioOutputService gpioOutputService)
		{
			_eventAggregator = eventAggregator;
			_intentToActionConversionService = intentToActionConversionService;
			_gpioOutputService = gpioOutputService;
			_cts = new CancellationTokenSource();
		}

		public override void Dispose()
		{
			_cts.Cancel();
		}

		public override void Init()
		{
			RunInBusyContextWithErrorFeedback(() =>
			{
				while (_cts.IsCancellationRequested == false)
				{
					Logger.LogPrompt("Type commands to send to LUIS:");
					var cmd = Console.ReadLine();
					if (string.IsNullOrWhiteSpace(cmd) == false)
					{
						_eventAggregator.Publish(new CommandTextAcquiredEvent(cmd));
					}
					else if(cmd == null)
					{
						break;
					}
				}
			}, _cts.Token);
			_eventAggregator.Subscribe(this);
		}

		public override void Handle(GpioStatusChangeEvent message)
		{
			RunInBusyContextWithErrorFeedback(() =>
			{
				var result = message.Payload;
				var onOff = result.State ? "on" : "off";
				Logger.LogInfo($"Pin {result.PinNumber} set to {onOff}");
			}, _cts.Token);
		}
	}
}