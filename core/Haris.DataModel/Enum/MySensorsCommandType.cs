namespace Haris.DataModel.Enum
{
	internal enum MySensorsCommandType
	{
		C_PRESENTATION = 0,
		C_SET = 1,
		C_REQ = 2,
		C_INTERNAL = 3,
		C_STREAM = 4 // For Firmware and other larger chunks of data that need to be divided into pieces.
	}
}