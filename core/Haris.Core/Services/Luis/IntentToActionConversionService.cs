using System.Linq;
using Haris.DataModel.Action;
using Haris.DataModel.Luis;

namespace Haris.Core.Services.Luis
{
	public interface IIntentToActionConversionService
	{
		ActionDescriptorDto[] GetActions(LuisResponseDto response);
	}

	public class IntentToActionConversionService : IIntentToActionConversionService
	{
		private readonly ILuisIntentToActionMappingRepository _intentToActionMappingRepository;
		
		public IntentToActionConversionService(ILuisIntentToActionMappingRepository intentToActionMappingRepository)
		{
			_intentToActionMappingRepository = intentToActionMappingRepository;
		}

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
			var config = _intentToActionMappingRepository.CurrentConfig;
			var actions =
				config
					.SelectMany(c => c.Entities.Where(e => e.EntityTags.Contains(thing.Entity)))
					.SelectMany(ecd => ecd.IntentConfigurations.Where(ic => ic.IntentLabel == intent.IntentLabel))
					.SelectMany(ic => ic.Actions).ToArray();
			foreach (var action in actions)
			{
				action.OriginalIntent = response;
			}
			return actions;
		}
	}
}