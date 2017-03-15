using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Haris.DataModel.DataModels
{
    public class Cube
    {
        public ObjectId Id { get; set; }

        [BsonElement("CubeAddress")]
        public string CubeAddress { get; set; }

        [BsonElement("CubeType")]
        public string CubeType { get; set; }

        public string Name { get; set; }

        [BsonElement("Logs")]
        public List<Log> Logs { get; set; }

        [BsonElement("WebHooks")]
        public List<WebHook> WebHooks { get; set; }
    }
}
