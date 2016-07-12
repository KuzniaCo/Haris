using System;
using Caliburn.Micro;
using Haris.Core.Events.Samples;
using Haris.Core.Services.Logging;

namespace Haris.Core.Modules.Samples
{
	[DisableModule]
	public class TestHarisModule : HarisModuleBase<SampleTimeEvent>
	{
		private readonly IEventAggregator _eventAggregator;

		public TestHarisModule(IEventAggregator eventAggregator)
		{
			_eventAggregator = eventAggregator;
		}

		public override void Init()
		{
			Logger.LogInfo("Test module running...");
			_eventAggregator.Subscribe(this);
		}

		public override void Dispose()
		{
			Logger.LogInfo("Test module disposing...");
			_eventAggregator.Unsubscribe(this);
		}

		public override void Handle(SampleTimeEvent message)
		{
			Logger.LogInfo("{0}: {1}", message.Payload, message.Id);
		}
	}
}