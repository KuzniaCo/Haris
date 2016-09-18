using Haris.DataModel.IntentRecognition;

namespace Haris.Core.Services.Luis
{
	public interface IIntentToActionConversionService
	{
		IIntentDto[] GetActions(IntentRecognitionResultDto response, string defaultLocation = null);
	}
}