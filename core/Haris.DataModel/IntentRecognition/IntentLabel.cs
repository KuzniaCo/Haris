using System;

namespace Haris.DataModel.IntentRecognition
{
	[Flags]
	public enum IntentLabel
	{
		None = 0,
		TurnOn = 2,
		TurnOff = 4,
		Set = 8,
		Get = 16
	}
}