namespace Haris.Core.Events.MySensors
{
    public class DisconnectedGatewayEvent : BaseEvent<string>
    {
        public DisconnectedGatewayEvent(string payload) : base(payload)
        {
        }
    }
}
