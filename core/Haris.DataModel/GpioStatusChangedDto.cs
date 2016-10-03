namespace Haris.DataModel
{
	public class GpioStatusChangedDto
	{
		public int PinNumber { get; }
		public bool State { get; }

		public GpioStatusChangedDto(int pinNumber, bool state)
		{
			PinNumber = pinNumber;
			State = state;
		}
	}
}