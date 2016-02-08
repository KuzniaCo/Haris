using Haris.DataModel.Action;

namespace Haris.Core.Events.System
{
	public class SystemActionRequest: BaseEvent<ActionDescriptorDto>
	{
		public SystemActionRequest(ActionDescriptorDto payload): base(payload)
		{
			
		}
	}
}