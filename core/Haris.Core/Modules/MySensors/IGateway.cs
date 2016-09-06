using Haris.DataModel.MySensors;

namespace Haris.Core.Modules.MySensors
{
    public interface IGateway
    {
        void Connect();
        void Disconnect();
        void SendMessage(MySensorsMessage message);

    }
}
