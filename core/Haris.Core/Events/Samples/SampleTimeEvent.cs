using System;

namespace Haris.Core.Events.Samples
{
	public class SampleTimeEvent: BaseEvent<DateTime>
	{
		public SampleTimeEvent(DateTime payload): base(payload)
		{
		}
	}
}