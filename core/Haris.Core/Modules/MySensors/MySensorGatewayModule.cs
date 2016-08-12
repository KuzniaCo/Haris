using Haris.Core.Modules.MySensors.Cubes;

namespace Haris.Core.Modules.MySensors
{
    public class MySensorGatewayModule : IHarisModule
    {
        private IGateway _serialGateway;
        public MySensorGatewayModule(GatewaySerial serialGateway)
        {
            _serialGateway = serialGateway;
        }

        public void Dispose()
        {
            _serialGateway.Disconnect();
        }

        public void Init()
        {
            _serialGateway.Connect();   
        }
    }
}