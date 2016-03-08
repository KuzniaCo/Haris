using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Haris.Core.Events.Command;
using Haris.Core.Modules.IntentRecognition.Core;
using Haris.DataModel.Action;
using Haris.DataModel.Luis;

namespace Haris.Core.Services.Luis
{
	public class LuisIntentRecognizer: IIntentRecognizer
	{
		private readonly ILuisClient _luisClient;
		private readonly IIntentToActionConversionService _intentToActionConversionService;

		public LuisIntentRecognizer(ILuisClient luisClient, IIntentToActionConversionService intentToActionConversionService)
		{
			_luisClient = luisClient;
			_intentToActionConversionService = intentToActionConversionService;
		}

		public async Task<IReadOnlyCollection<ActionDescriptorDto>> InterpretIntent(CommandTextAcquiredEvent evt)
		{
			return await InterpretIntent(evt, CancellationToken.None);
		}

		public async Task<IReadOnlyCollection<ActionDescriptorDto>> InterpretIntent(CommandTextAcquiredEvent evt, CancellationToken ct)
		{
			var response = await _luisClient.AskLuis(evt.Payload, ct);
			return InterpretIntentInternal(response);
		}

		private IReadOnlyCollection<ActionDescriptorDto> InterpretIntentInternal(LuisResponseDto response)
		{
			return _intentToActionConversionService.GetActions(response);
		}
	}
}