using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Haris.DataModel.Luis;
using RestSharp;

namespace Haris.Core.Services.Luis
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
			Console.WriteLine("{0:HH:m:s}> Error asking LUIS: {1} with code {2}", DateTime.Now, response.ErrorMessage,
				response.StatusCode);
			throw new Exception(response.ErrorMessage);//TODO Add custom exception + handling
		}
	}
}