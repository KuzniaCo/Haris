using Haris.DataModel.IntentRecognition;

namespace Haris.Core.Events.Intent
{
	public class IntentRecognitionCompletionEvent : BaseEvent<IntentRecognitionResultDto>
	{
		public IntentRecognitionCompletionEvent(IntentRecognitionResultDto recognitionResult): base(recognitionResult)
		{
			
		}
	}
}