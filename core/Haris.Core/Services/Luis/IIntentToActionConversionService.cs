using Haris.DataModel.IntentRecognition;

namespace Haris.Core.Services.Luis
{
	public interface IIntentToActionConversionService
	{
		IIntentDto[] GetActions(IntentRecognitionResult response, string defaultLocation = null);
	}
}