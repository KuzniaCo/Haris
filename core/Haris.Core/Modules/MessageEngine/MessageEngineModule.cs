using System;
using System.Threading.Tasks;
using Caliburn.Micro;
using Haris.Core.Events.MySensors;
using Haris.Core.Services.Logging;

namespace Haris.Core.Modules.MessageEngine
{
    public class MessageEngineModule : HarisModuleBase<MessageReceivedEvent>
    {
        private readonly IEventAggregator _eventAggregator;

        public MessageEngineModule(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public override void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public override void Init()
        {
            Logger.LogPrompt("Initilized Message Engine Module");
            _eventAggregator.Subscribe(this);
        }

        public override void Handle(MessageReceivedEvent message)
        {
//            Task.Run(() =>
//            {
//                var msg = new Modules.MessageEngine.MessageEngine(message.Payload);
//            });
            Logger.LogPrompt("Recived message: " + message.Payload);
        }
    }
}
