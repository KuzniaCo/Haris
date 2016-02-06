using System.Collections.Generic;
using System.Linq;

namespace Haris.DataModel.Luis
{
	public class LuisResponseDto
	{
		public string Query { get; set; }
		public List<LuisIntent> Intents { get; set; }
		public List<LuisEntity> Entities { get; set; }

		public LuisIntent MostProbableIntent
		{
			get { return Intents.OrderByDescending(i => i.Score).FirstOrDefault(); }
		}
	}
}