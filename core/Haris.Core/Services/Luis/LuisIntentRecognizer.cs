using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Haris.Core.Events.Command;
using Haris.Core.Modules.IntentRecognition.Core;
using Haris.DataModel.IntentRecognition;

namespace Haris.Core.Services.Luis
{
	public class LuisIntentRecognizer: IIntentRecognizer
	{
		private readonly ILuisClient _luisClient;

		public LuisIntentRecognizer(ILuisClient luisClient)
		{
			_luisClient = luisClient;
		}

		public async Task<IntentRecognitionResult> InterpretIntent(CommandTextAcquiredEvent evt)
		{
			
			return await InterpretIntent(evt, CancellationToken.None);
		}

		public async Task<IntentRecognitionResult> InterpretIntent(CommandTextAcquiredEvent evt, CancellationToken ct)
		{
			var response = await _luisClient.AskLuis(evt.Payload, ct);
			var result = new IntentRecognitionResult();
			var intent = response.MostProbableIntent;
			result.OriginalIntent = intent;
			result.IntentLabel = intent.IntentLabel;
			var action = intent.Actions?.FirstOrDefault(a => a.Triggered);
			if (action != null)
			{
				result.PropertyParameter =
					action.Parameters.FirstOrDefault(p => p.Value != null && p.Value.Any(v => v.Type == "Property"))?.Value.First().Entity;
				result.RoomParameter =
					action.Parameters.FirstOrDefault(p => p.Value != null && p.Value.Any(v => v.Type == "Room"))?.Value.First().Entity;
				result.ThingParameter =
					action.Parameters.FirstOrDefault(p => p.Value != null && p.Value.Any(v => v.Type == "Thing"))?.Value.First().Entity;
				var numericParameter =
					action.Parameters.FirstOrDefault(p => p.Value != null && p.Value.Any(v => v.Type == "builtin.number"))?
						.Value.First()
						.Entity;
				if (numericParameter != null)
				{
					result.NumericParameter = int.Parse(numericParameter);
				}
			}
			return result;
		}
	}
}