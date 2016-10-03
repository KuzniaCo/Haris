using Caliburn.Micro;
using Haris.Core.Events.Intent;
using Haris.Core.Events.System;
using Haris.Core.Services.Gpio;
using Haris.Core.Services.Logging;
using Haris.Core.Services.Luis;
using Haris.DataModel.IntentRecognition;
using System.Linq;
using System.Threading;

namespace Haris.Core.Modules.IntentRecognition.Core
{
	public class IntentToActionHub : HarisModuleBase<IntentRecognitionCompletionEvent>
	{
		private readonly IEventAggregator _eventAggregator;
		private readonly IIntentToActionConversionService _intentToActionConversionService;
		private readonly IGpioOutputService _gpioOutputService;
		private readonly CancellationTokenSource _cts;

		public IntentToActionHub(IEventAggregator eventAggregator, IIntentToActionConversionService intentToActionConversionService, IGpioOutputService gpioOutputService)
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
					var pinNumber = intentDto.TargetPinNumber.Value;
					var state = intentDto.IntentLabel == IntentLabel.TurnOn;
					_gpioOutputService.SetPin(pinNumber, state);
					_eventAggregator.Publish(new GpioStatusChangeEvent(pinNumber, state));
				}
			}, _cts.Token);
		}
	}
}