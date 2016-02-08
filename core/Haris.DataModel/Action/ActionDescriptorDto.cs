using System;

namespace Haris.DataModel.Action
{
	public class ActionDescriptorDto
	{
		public Guid? TargetGuid { get; set; }
		public byte ActionId { get; set; }
		public object ActionParameter { get; set; }
		public object OriginalIntent { get; set; }
	}
}