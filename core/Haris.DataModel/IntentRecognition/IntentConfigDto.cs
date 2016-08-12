using Haris.DataModel.Action;

namespace Haris.DataModel.IntentRecognition
{
	public class IntentConfigDto
	{
		public IntentLabel IntentLabel { get; set; }
		public ActionDescriptorDto[] Actions { get; set; }
	}
}