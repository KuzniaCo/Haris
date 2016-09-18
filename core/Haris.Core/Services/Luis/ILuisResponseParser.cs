using Haris.DataModel.IntentRecognition;
using Haris.DataModel.Luis;

namespace Haris.Core.Services.Luis
{
	public interface ILuisResponseParser
	{
		IntentRecognitionResultDto Parse(LuisResponseDto responseDto);
	}
}