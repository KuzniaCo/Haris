using Haris.Core.Modules.MySensors;

namespace Haris.Core.Events.MySensors
{
    public class MessageReceivedEvent : BaseEvent<string>
    {
        public MessageReceivedEvent(string payload) : base(payload)
        {
        }
    }
}
