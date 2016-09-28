using Haris.Core.Services.Logging;
using Raspberry.IO.GeneralPurpose;
using System;

namespace Haris.Core.Services.Gpio.Impl
{
	public class SharpIoOutputService : IGpioOutputService
	{
		public void SetPin(int pin, bool state)
		{
			try
			{
				var connectorPin = ((ConnectorPin)pin);
				var pinConfig = connectorPin.Output().Enable();
				var connection = new GpioConnection(pinConfig);
				connection[pinConfig] = state;
			}
			catch(Exception e)
			{
				Logger.LogError("Error setting pin {0} to {1}: {2}\n{3}", pin, state, e.Message, e.StackTrace);
			}
		}
	}
}