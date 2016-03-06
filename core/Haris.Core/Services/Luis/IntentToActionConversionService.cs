using System.Linq;
using Haris.DataModel.Action;
using Haris.DataModel.IntentRecognition;
using Haris.DataModel.Luis;

namespace Haris.Core.Services.Luis
{
	public class IntentToActionConversionService
	{
		private CubeConfigDto[] _configuration = new CubeConfigDto[0];

		public ActionDescriptorDto[] GetActions(LuisResponseDto response)
		{
			var intent = response.MostProbableIntent;
			var entities = response.Entities.Where(e => e.Type == "Thing").ToList();
			var thing = entities.FirstOrDefault();
			var properties = response.Entities.Where(e => e.Type == "Property").ToList();
			var numbers = response.Entities.Where(e => e.Type == "builtin.number").ToList();

			if (thing == null)
			{
				return new ActionDescriptorDto[0];
			}

			var actions =
				_configuration
					.SelectMany(c => c.Entities.Where(e => e.EntityTags.Contains(thing.Entity)))
					.SelectMany(ecd => ecd.IntentConfigurations.Where(ic => ic.IntentLabel == intent.IntentLabel))
					.SelectMany(ic => ic.Actions).ToArray();
			return actions;
		}
	}
}