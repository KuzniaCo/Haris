using Haris.DataModel;

namespace Haris.Core.Events.System
{
	public class GpioStatusChangeEvent : BaseEvent<GpioStatusChangedDto>
	{
		public GpioStatusChangeEvent(int pinNumber, bool state)
		{
			Payload = new GpioStatusChangedDto(pinNumber, state);
		}
	}
}