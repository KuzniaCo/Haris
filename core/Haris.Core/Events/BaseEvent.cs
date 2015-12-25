using System;
using System.Collections.Generic;

namespace Haris.Core.Events
{
	public abstract class BaseEvent : IEvent
	{
		public Guid Id { get; protected set; }
		public IEnumerable<IAttachedProperty> AttachedProperties { get; protected set; }

		protected BaseEvent()
		{
			Id = Guid.NewGuid();
		}
	}

	public abstract class BaseEvent<TPayload>: BaseEvent, IEvent<TPayload>
	{
		public TPayload Payload { get; protected set; }
	}
}