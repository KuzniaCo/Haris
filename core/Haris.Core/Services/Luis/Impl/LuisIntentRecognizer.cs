using Haris.Core.Events.Command;
using Haris.Core.Modules.IntentRecognition.Core;
using Haris.DataModel.IntentRecognition;
using System.Threading;
using System.Threading.Tasks;

namespace Haris.Core.Services.Luis.Impl
{
	public class LuisIntentRecognizer: IIntentRecognizer
	{
		private readonly ILuisClient _luisClient;
		private readonly ILuisResponseParser _luisResponseParser;

		public LuisIntentRecognizer(ILuisClient luisClient, ILuisResponseParser luisResponseParser)
		{
			_luisClient = luisClient;
			_luisResponseParser = luisResponseParser;
		}

		public async Task<IntentRecognitionResultDto> InterpretIntent(CommandTextAcquiredEvent evt)
		{
			
			return await InterpretIntent(evt, CancellationToken.None);
		}

		public async Task<IntentRecognitionResultDto> InterpretIntent(CommandTextAcquiredEvent evt, CancellationToken ct)
		{
			var response = await _luisClient.AskLuis(evt.Payload, ct);
			var result = _luisResponseParser.Parse(response);
			return result;
		}
	}
}