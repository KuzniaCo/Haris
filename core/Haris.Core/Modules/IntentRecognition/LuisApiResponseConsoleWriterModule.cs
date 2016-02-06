using System;
using System.Linq;
using Caliburn.Micro;
using Haris.Core.Events.IntentRecognition;

namespace Haris.Core.Modules.IntentRecognition
{
	public class LuisApiResponseConsoleWriterModule : HarisModuleBase<LuisApiResponse>
	{
		private readonly IEventAggregator _eventAggregator;

		public LuisApiResponseConsoleWriterModule(IEventAggregator eventAggregator)
		{
			_eventAggregator = eventAggregator;
		}

		public override void Dispose()
		{
			
		}

		public override void Init()
		{
			_eventAggregator.Subscribe(this);
		}

		public override void Handle(LuisApiResponse message)
		{
			var entities = message.Payload.Entities;
			Console.WriteLine("Luis API response:\n{0} {1}", message.Payload.MostProbableIntent.Intent, entities.Count > 0 ? entities.First().Entity : string.Empty);
		}
	}
}