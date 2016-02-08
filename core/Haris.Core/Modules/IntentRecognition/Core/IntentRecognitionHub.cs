using System.Threading.Tasks;
using Caliburn.Micro;
using Haris.Core.Events.Command;
using Haris.Core.Events.System;

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
			
		}

		public override void Init()
		{
			_eventAggregator.Subscribe(this);
		}

		public override void Handle(CommandTextAcquiredEvent message)
		{
			Task.Run(async () =>
			{
				var actions = await _intentRecognizer.InterpretIntent(message);
				foreach (var action in actions)
				{
					_eventAggregator.Publish(new SystemActionRequest(action));
				}
			});
		}
	}
}