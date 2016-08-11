using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Haris.Core.Events.MySensors;
using Haris.Core.Events.System;

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
            throw new System.NotImplementedException();
        }

        public override void Handle(MessageReceivedEvent message)
        {
            Task.Run(() =>
            {
                var msg = new MessageEngine(message.Payload);
            });
        }
    }
}
