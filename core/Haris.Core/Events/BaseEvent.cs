using System;

namespace Haris.Core.Events
{
	public abstract class BaseEvent : IEvent
	{
		public Guid Id { get; set; }

		protected BaseEvent()
		{
			Id = Guid.NewGuid();
		}
	}
}