using Haris.Core.Modules.MySensors.Cubes;
using Haris.Core.Modules.MySensors.Cubes.Implementations;

namespace Haris.Core.Modules.MySensors
{
    public class MySensorGatewayModule : IHarisModule
    {
        private IGatewayCube _serialGateway;
        public MySensorGatewayModule(GatewaySerialCube serialGateway)
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