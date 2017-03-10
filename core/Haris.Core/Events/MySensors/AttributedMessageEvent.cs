namespace Haris.Core.Events.MySensors
{
    public class AttributedMessageEvent : BaseEvent<string>
    {
        public AttributedMessageEvent(string cmd): base(cmd)
        {
        }
    }
}
