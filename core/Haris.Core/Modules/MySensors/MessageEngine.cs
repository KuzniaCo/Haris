using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Haris.DataModel.MySensors;

namespace Haris.Core.Modules.MySensors
{
    internal sealed class MessageEngine
    {
        public string RawMessage { get; private set; }

        public MySensorsMessage DecodedMessage { get; private set; }

        public MessageEngine(string message)
        {
            RawMessage = message;
            DecodeMessage();
        }

        private void DecodeMessage()
        {
            DecodedMessage = new MySensorsMessage();
            //TODO: Add logic to decode message
        }
    }
}
