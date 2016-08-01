namespace Haris.DataModel.IntentRecognition
{
	public class PropertyRelatedIntentDto: IIntentDto
	{
		public IntentLabel IntentLabel { get; set; }
		public string EntityLabel { get; set; }
		public string RoomLabel { get; set; }
		public string PropertyLabel { get; set; }
	}
}