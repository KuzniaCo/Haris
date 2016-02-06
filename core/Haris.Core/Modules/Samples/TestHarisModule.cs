using System;
using Caliburn.Micro;
using Haris.Core.Events.Samples;

namespace Haris.Core.Modules.Samples
{
	public class TestHarisModule : HarisModuleBase<SampleTimeEvent>
	{
		private readonly IEventAggregator _eventAggregator;

		public TestHarisModule(IEventAggregator eventAggregator)
		{
			_eventAggregator = eventAggregator;
		}

		public override void Init()
		{
			Console.WriteLine("Test module running...");
			_eventAggregator.Subscribe(this);
		}

		public override void Dispose()
		{
			Console.WriteLine("Test module disposing...");
			_eventAggregator.Unsubscribe(this);
		}

		public override void Handle(SampleTimeEvent message)
		{
			Console.WriteLine("{0}: {1}", message.Payload, message.Id);
		}
	}
}