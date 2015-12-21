using System;

namespace Haris.Core.Events
{
	public interface IEvent
	{
		Guid Id { get; set; }
	}
}