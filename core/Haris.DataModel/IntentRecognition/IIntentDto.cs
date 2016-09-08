namespace Haris.DataModel.IntentRecognition
{
	public interface IIntentDto
	{
		IntentLabel IntentLabel { get; }
		string EntityLabel { get; }
		string RoomLabel { get; }
	}
}