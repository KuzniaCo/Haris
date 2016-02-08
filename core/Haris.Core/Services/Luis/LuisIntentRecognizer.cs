using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Haris.Core.Events.Command;
using Haris.Core.Modules.IntentRecognition.Core;
using Haris.DataModel.Action;
using Haris.DataModel.Luis;
using RestSharp;

namespace Haris.Core.Services.Luis
{
	public class LuisIntentRecognizer: IIntentRecognizer
	{
		private readonly ILuisUrlProvider _luisUrlProvider;

		public LuisIntentRecognizer(ILuisUrlProvider luisUrlProvider)
		{
			_luisUrlProvider = luisUrlProvider;
		}

		public async Task<IReadOnlyCollection<ActionDescriptorDto>> InterpretIntent(CommandTextAcquiredEvent evt)
		{
			return await InterpretIntent(evt, CancellationToken.None);
		}

		public async Task<IReadOnlyCollection<ActionDescriptorDto>> InterpretIntent(CommandTextAcquiredEvent evt, CancellationToken ct)
		{
			var response = await AskLuis(evt.Payload, ct);
			return InterpretIntentInternal(response);
		}

		private IReadOnlyCollection<ActionDescriptorDto> InterpretIntentInternal(LuisResponseDto response)
		{
			return new[] {new ActionDescriptorDto {OriginalIntent = response}};
		}

		private async Task<LuisResponseDto> AskLuis(string command, CancellationToken ct)
		{
			var client = new RestClient(_luisUrlProvider.BaseUrl);
			var url = _luisUrlProvider.GetUrlForQuery(command);
			var request = new RestRequest(url);
			var response = await client.ExecuteGetTaskAsync<LuisResponseDto>(request, ct);
			if (response.StatusCode == HttpStatusCode.OK)
				return response.Data;
			throw new Exception(response.ErrorMessage);//TODO Add custom exception + handling
		}
	}
}