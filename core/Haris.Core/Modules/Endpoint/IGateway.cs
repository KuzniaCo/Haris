namespace Haris.Core.Modules.Endpoint
{
    public interface IGateway
    {
        void Connect();
        void Disconnect();
        void SendMessage(string message);

    }
}
