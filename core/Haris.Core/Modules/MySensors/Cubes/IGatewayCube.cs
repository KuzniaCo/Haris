using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Haris.DataModel.MySensors;

namespace Haris.Core.Modules.MySensors.Cubes
{
    public interface IGatewayCube
    {
        void Connect();
        void Disconnect();
        void SendMessage(MySensorsMessage message);

    }
}
