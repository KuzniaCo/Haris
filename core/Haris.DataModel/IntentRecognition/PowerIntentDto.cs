namespace Haris.DataModel.IntentRecognition
{
	public class PowerIntentDto : IIntentDto
	{
		public IntentLabel IntentLabel { get; set; }
		public string EntityLabel { get; set; }
		public string RoomLabel { get; set; }
	}
}