using System;
using System.Threading.Tasks;
using Caliburn.Micro;
using Haris.Core.Events.MySensors;
using Haris.Core.Services.Logging;
using RestSharp;
using RestSharp.Authenticators;

namespace Haris.Core.Modules.MessageEngine
{
    public class TeaMonitorModule : HarisModuleBase<MessageReceivedEvent>
    {
        private readonly IEventAggregator _eventAggregator;

        public TeaMonitorModule(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public override void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public override void Init()
        {
            Logger.LogPrompt("Initilized Tea Monitor Module");
            _eventAggregator.Subscribe(this);
        }

        public override void Handle(MessageReceivedEvent message)
        {
//            Task.Run(() =>
//            {
//                String[] items = message.Payload.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
//                Double teaTemp = Double.Parse(items[1].Replace(".", ","));
//
//                if (teaTemp > 25 && teaTemp < 30)
//                {
//                    SendEmail("sebcza@outlook.com");
//                }
//            });
//            
        }

        private IRestResponse SendEmail(string email)
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.mailgun.net/v3/sandboxc7f68e8fc9d84bcd9a3791dac436a415.mailgun.org");
            client.Authenticator = new HttpBasicAuthenticator(
                "api", "key-0835f0a7b43afff68fb88e2ffe79af8e");
            RestRequest request = new RestRequest();
            request.AddParameter("domain",
                                "sandboxc7f68e8fc9d84bcd9a3791dac436a415.mailgun.org", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "Excited User <postmaster@sandboxc7f68e8fc9d84bcd9a3791dac436a415.mailgun.org>");
            request.AddParameter("to", email);
            request.AddParameter("subject", "Twoja Herbata");
            request.AddParameter("text", "Woda gotowa do parzenia");
            request.Method = Method.POST;
            return client.Execute(request);
        }
    }
}
