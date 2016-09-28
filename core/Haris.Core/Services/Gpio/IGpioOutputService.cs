namespace Haris.Core.Services.Gpio
{
	public interface IGpioOutputService
	{
		void SetPin(int pin, bool state);
	}
}