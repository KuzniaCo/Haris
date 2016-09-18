using Haris.DataModel.Luis;

namespace Haris.DataModel.IntentRecognition
{
	public class IntentRecognitionResultDto
	{
		public IntentLabel IntentLabel { get; set; }
		public string ThingParameter { get; set; }
		public string PropertyParameter { get; set; }
		public string RoomParameter { get; set; }
		public int? NumericParameter { get; set; }
		public LuisIntent OriginalIntent { get; set; }
	}
}