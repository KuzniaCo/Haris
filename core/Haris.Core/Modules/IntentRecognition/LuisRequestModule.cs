using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using Haris.Core.Events.IntentRecognition;
using Haris.Core.Services.Luis;
using Haris.DataModel.Luis;
using RestSharp;

namespace Haris.Core.Modules.IntentRecognition
{
	public class LuisRequestModule: HarisModuleBase<LuisApiRequest>
	{
		private readonly IEventAggregator _eventAggregator;
		private readonly ILuisUrlProvider _luisUrlProvider;
		private readonly CancellationTokenSource _cts;

		public LuisRequestModule(IEventAggregator eventAggregator, ILuisUrlProvider luisUrlProvider)
		{
			_eventAggregator = eventAggregator;
			_luisUrlProvider = luisUrlProvider;
			_cts = new CancellationTokenSource();
		}

		public override void Dispose()
		{
			if (_cts != null)
				_cts.Cancel();
		}

		public override void Init()
		{
			_eventAggregator.Subscribe(this);
		}

		public override void Handle(LuisApiRequest message)
		{
			Task.Run(async () =>
			{
				try
				{
					await DownloadAndRaise(message.Payload);
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
					
					throw;
				}
				
			});
		}

		private async Task DownloadAndRaise(string command)
		{
			var client = new RestClient(_luisUrlProvider.BaseUrl);
			var url = _luisUrlProvider.GetUrlForQuery(command);
			var request = new RestRequest(url);
			var response = await client.ExecuteGetTaskAsync<LuisResponseDto>(request, _cts.Token);
			if(response.StatusCode == HttpStatusCode.OK)
				_eventAggregator.Publish(new LuisApiResponse(response.Data));
		}
	}
}