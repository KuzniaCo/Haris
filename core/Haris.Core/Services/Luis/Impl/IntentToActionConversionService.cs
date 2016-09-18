using Haris.Core.Services.Logging;
using Haris.DataModel.IntentRecognition;
using System.Collections.Generic;
using System.Linq;

namespace Haris.Core.Services.Luis.Impl
{
	public class IntentToActionConversionService : IIntentToActionConversionService
	{
		private readonly ILuisIntentToActionMappingRepository _intentToActionMappingRepository;
		
		public IntentToActionConversionService(ILuisIntentToActionMappingRepository intentToActionMappingRepository)
		{
			_intentToActionMappingRepository = intentToActionMappingRepository;
		}

		public IIntentDto[] GetActions(IntentRecognitionResultDto response, string defaultLocation = null)
		{
			var thing = response.ThingParameter;
			var property = response.PropertyParameter;
			var room = response.RoomParameter ?? defaultLocation;
			if (response.RoomParameter == null)
			{
				response.RoomParameter = defaultLocation;
			}

			if (thing == null && property == null && room == null)
			{
				Logger.LogError("Parameters missing");
				return new IIntentDto[0];
			}
			var config = _intentToActionMappingRepository.CurrentConfig;
			var actions =
				config.Where(c => c.SupportedIntents.Contains(response.IntentLabel)) //quick-filter by intent
					.SelectMany(c => WhereResponseIsMatch(c, response)).ToArray();
			return actions;
		}

		private IEnumerable<IIntentDto> WhereResponseIsMatch(CubeConfigDto cubeConfigDto, IntentRecognitionResultDto intent)
		{
			var result = new List<IIntentDto>();
			if (intent.IntentLabel == IntentLabel.Get && cubeConfigDto.SupportedIntents.Contains(IntentLabel.Get))
			{
				var actions = MatchGetActions(cubeConfigDto, intent);
				if (actions.Any())
				{
					result.AddRange(actions);
				}
			}
			if (intent.IntentLabel == IntentLabel.Set && cubeConfigDto.SupportedIntents.Contains(IntentLabel.Set))
			{
				var actions = MatchSetActions(cubeConfigDto, intent);
				if (actions.Any())
				{
					result.AddRange(actions);
				}
			}
			if (intent.IntentLabel == IntentLabel.TurnOn && cubeConfigDto.SupportedIntents.Contains(IntentLabel.TurnOn))
			{
				var actions = MatchTurnOnActions(cubeConfigDto, intent);
				if (actions.Any())
				{
					result.AddRange(actions);
				}
			}
			if (intent.IntentLabel == IntentLabel.TurnOff && cubeConfigDto.SupportedIntents.Contains(IntentLabel.TurnOff))
			{
				var actions = MatchTurnOffActions(cubeConfigDto, intent);
				if (actions.Any())
				{
					result.AddRange(actions);
				}
			}

			return result;
		}

		private IList<PropertyRelatedIntentDto> MatchGetActions(CubeConfigDto cubeConfigDto, IntentRecognitionResultDto intent)
		{
			var actions = cubeConfigDto.GetIntentActions;
			return
				actions.Where(
					a =>
						intent.PropertyParameter == a.PropertyLabel && OptionalMatch(intent.ThingParameter, a.EntityLabel) &&
						OptionalMatch(intent.RoomParameter, a.RoomLabel)).ToArray();
		}

		private IList<PropertyRelatedIntentDto> MatchSetActions(CubeConfigDto cubeConfigDto, IntentRecognitionResultDto intent)
		{
			var actions = cubeConfigDto.SetIntentActions.Where(
				a =>
					intent.PropertyParameter == a.PropertyLabel &&
					OptionalMatch(intent.ThingParameter, a.EntityLabel) &&
					OptionalMatch(intent.RoomParameter, a.RoomLabel)).ToArray();
			if (intent.NumericParameter != null)
			{
				foreach (var action in actions)
				{
					action.NumericParameter = intent.NumericParameter.Value;
				}
			}
			return actions;
		}

		private IList<PowerIntentDto> MatchTurnOnActions(CubeConfigDto cubeConfigDto, IntentRecognitionResultDto intent)
		{
			var actions = cubeConfigDto.TurnOnIntentActions;
			return
				actions.Where(
					a =>
						intent.ThingParameter == a.EntityLabel && 
						OptionalMatch(intent.RoomParameter, a.RoomLabel)).ToArray();
		}

		private IList<PowerIntentDto> MatchTurnOffActions(CubeConfigDto cubeConfigDto, IntentRecognitionResultDto intent)
		{
			var actions = cubeConfigDto.TurnOffIntentActions;
			return
				actions.Where(
					a =>
						intent.ThingParameter == a.EntityLabel &&
						OptionalMatch(intent.RoomParameter, a.RoomLabel)).ToArray();
		}

		private bool OptionalMatch(string s1, string s2)
		{
			if(s1 == null)
			{
				return true;
			}
			return s1 == s2;
		}
	}
}