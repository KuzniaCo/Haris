using System;

namespace Haris.Core.Events
{
	public class LocationProperty: IAttachedProperty
	{
		public Guid PropertyId { get { return AttachedPropertyKey.UserLocation; } }
		public string Location { get; set; }
	}
}