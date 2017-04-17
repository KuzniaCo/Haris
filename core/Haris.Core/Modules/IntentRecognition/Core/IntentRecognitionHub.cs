using Caliburn.Micro;
using Haris.Core.Events.Command;
using Haris.Core.Events.Intent;

namespace Haris.Core.Modules.IntentRecognition.Core
{
	public class IntentRecognitionHub: HarisModuleBase<CommandTextAcquiredEvent>
	{
		private readonly IEventAggregator _eventAggregator;
		private readonly IIntentRecognizer _intentRecognizer;

		public IntentRecognitionHub(IIntentRecognizer intentRecognizer, IEventAggregator eventAggregator)
		{
			_intentRecognizer = intentRecognizer;
			_eventAggregator = eventAggregator;
		}

		public override void Dispose()
		{
			_eventAggregator.Unsubscribe(this);
		}

		public override void Init()
		{
			_eventAggregator.Subscribe(this);
		}

		public override void Handle(CommandTextAcquiredEvent message)
		{
			RunInBusyContextWithErrorFeedback(async () =>
			{
				var recognitionResult = await _intentRecognizer.InterpretIntent(message);
				_eventAggregator.Publish(new IntentRecognitionCompletionEvent(recognitionResult));
			});
		}
	}
}