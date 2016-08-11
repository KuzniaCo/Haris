namespace Haris.Core.Events.MySensors
{
    public class ConnectedGatewayEvent : BaseEvent<string>
    {
        public ConnectedGatewayEvent(string payload) : base(payload)
        {
        }
    }
}
