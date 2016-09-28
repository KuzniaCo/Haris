using Haris.Core.Services.Logging;
using PiSharp.LibGpio;
using PiSharp.LibGpio.Entities;
using System;

namespace Haris.Core.Services.Gpio.Impl
{
	public class PiSharpGpioOutputService : IGpioOutputService
	{
		public void SetPin(int pin, bool state)
		{
			try
			{
				LibGpio.Gpio.SetupChannel((RaspberryPinNumber) pin, Direction.Output);
				LibGpio.Gpio.OutputValue((RaspberryPinNumber) pin, state);
			}
			catch (Exception e)
			{
				Logger.LogError("Error setting pin {0} to {1}: {2}\n{3}", pin, state, e.Message, e.StackTrace);
			}
		}
	}
}