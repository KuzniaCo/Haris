using Caliburn.Micro;
using Haris.Core.Events.Command;
using Haris.Core.Events.Intent;
using Haris.Core.Services.Logging;
using Haris.Core.Services.Luis;
using Haris.DataModel.IntentRecognition;
using Raspberry.IO.GeneralPurpose;
using System;
using System.Linq;
using System.Threading;

namespace Haris.Core.Modules.ConsoleInput
{
	public class ConsoleCommandInputModule: HarisModuleBase<IntentRecognitionCompletionEvent>
	{
		private readonly IEventAggregator _eventAggregator;
		private readonly IIntentToActionConversionService _intentToActionConversionService;
		private readonly CancellationTokenSource _cts;

		public ConsoleCommandInputModule(IEventAggregator eventAggregator, IIntentToActionConversionService intentToActionConversionService)
		{
			_eventAggregator = eventAggregator;
			_intentToActionConversionService = intentToActionConversionService;
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

		public override void Handle(IntentRecognitionCompletionEvent message)
		{
			RunInBusyContextWithErrorFeedback(() =>
			{
				var result = message.Payload;
				var actions = _intentToActionConversionService.GetActions(message.Payload);
				Logger.LogInfo(string.Format("{0} th:{1} r:{4} pr:{2} n:{3} pin:{5}", result.IntentLabel, result.ThingParameter,
					result.PropertyParameter, result.NumericParameter, result.RoomParameter, actions.OfType<PowerIntentDto>().FirstOrDefault()?.TargetPinNumber));
				foreach (var intentDto in actions.OfType<PowerIntentDto>().Where(i => i.TargetPinNumber != null))
				{
					var connectorPin = ((ConnectorPin) intentDto.TargetPinNumber);
					var pin = connectorPin.Output().Enable();
					var connection = new GpioConnection(pin);
					connection[pin] = result.IntentLabel == IntentLabel.TurnOn;
				}

			}, _cts.Token);
		}
	}
}