using Haris.Core.Services.Logging;
using Haris.DataModel.Repositories;

namespace Haris.Core.Modules.Endpoint
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
            Logger.LogPrompt("EndpointModule ready");
            var rep = new CubeRepository();
            var cube = rep.GetCube("ad5ft");
            Logger.LogPrompt(cube.CubeType);
            _serialGateway.Connect();   
        }
    }
}