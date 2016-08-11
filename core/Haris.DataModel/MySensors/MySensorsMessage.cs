using Haris.DataModel.Enums;

namespace Haris.DataModel.MySensors
{
    public class MySensorsMessage
    {
        public int NodeId { get; set; }

        public int SensorId { get; set; }

        public MySensorsDataType MessageType { get; set; }

        public bool Ack { get; set; }

        public int SubType { get; set; }

        public string Payload { get; set; }
    }
}
