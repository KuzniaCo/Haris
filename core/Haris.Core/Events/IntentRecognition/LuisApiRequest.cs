using Haris.DataModel.Luis;

namespace Haris.Core.Events.IntentRecognition
{
	public class LuisApiRequest: BaseEvent<string>
	{
		public LuisApiRequest(string cmd): base(cmd)
		{
			
		}
	}

	public class LuisApiResponse : BaseEvent<LuisResponseDto>
	{
		public LuisApiResponse(LuisResponseDto result): base(result)
		{
			
		}
	}
}