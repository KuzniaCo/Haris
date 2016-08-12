using Haris.DataModel.IntentRecognition;
using Haris.DataModel.Luis;

namespace Haris.Core.Services.Luis
{
	public interface ILuisResponseParser
	{
		IntentRecognitionResult Parse(LuisResponseDto responseDto);
	}
}