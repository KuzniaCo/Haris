using Haris.DataModel.IntentRecognition;

namespace Haris.Core.Events.Intent
{
	public class IntentRecognitionCompletionEvent : BaseEvent<IntentRecognitionResult>
	{
		public IntentRecognitionCompletionEvent(IntentRecognitionResult recognitionResult): base(recognitionResult)
		{
			
		}
	}
}