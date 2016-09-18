using Haris.DataModel.IntentRecognition;
using Haris.DataModel.Luis;
using System.Linq;

namespace Haris.Core.Services.Luis.Impl
{
	public class LuisResponseParser : ILuisResponseParser
	{
		public IntentRecognitionResultDto Parse(LuisResponseDto response)
		{
			var result = new IntentRecognitionResultDto();
			var intent = response.MostProbableIntent;
			result.OriginalIntent = intent;
			result.IntentLabel = intent.IntentLabel;
			var action = intent.Actions?.FirstOrDefault(a => a.Triggered);
			if (action != null)
			{
				result.PropertyParameter =
					action.Parameters.FirstOrDefault(p => p.Value != null && p.Value.Any(v => v.Type == "Property"))?
						.Value.First()
						.Entity;
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