using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Haris.Core.Services.Logging;
using Haris.DataModel.Luis;
using RestSharp;

namespace Haris.Core.Services.Luis.Impl
{
	public class LuisClient : ILuisClient
	{
		private readonly ILuisUrlProvider _luisUrlProvider;

		public LuisClient(ILuisUrlProvider luisUrlProvider)
		{
			_luisUrlProvider = luisUrlProvider;
		}

		public async Task<LuisResponseDto> AskLuis(string command, CancellationToken ct)
		{
			var client = new RestClient(_luisUrlProvider.BaseUrl);
			var url = _luisUrlProvider.GetUrlForQuery(command);
			var request = new RestRequest(url);
			var response = await client.ExecuteGetTaskAsync<LuisResponseDto>(request, ct);
			if (response.StatusCode == HttpStatusCode.OK)
				return response.Data;
			Logger.LogError("Error asking LUIS: {0} with code {1}", response.ErrorMessage,
				response.StatusCode);
			throw new Exception(response.ErrorMessage);//TODO Add custom exception + handling
		}
	}
}