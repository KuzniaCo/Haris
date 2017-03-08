using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Haris.Core.Events.MySensors;
using Haris.Core.Events.System;
using Haris.Core.Services.Logging;

namespace Haris.Core.Modules.MySensors
{
    class MySensorsMessageEngineHub : HarisModuleBase<MessageReceivedEvent>
    {

        public MySensorsMessageEngineHub()
        {
        }

        public override void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public override void Init()
        {
            Logger.LogPrompt("Initilized Message Engine Module");
        }

        public override void Handle(MessageReceivedEvent message)
        {
            Task.Run(() =>
            {
                var msg = new MessageEngine.MessageEngine(message.Payload);
            });
        }
    }
}
