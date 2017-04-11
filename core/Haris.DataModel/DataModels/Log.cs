using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Haris.DataModel.DataModels
{
    public class Log
    {
        public int Id { get; set; }

        public int CubeId { get; set; }

        public string Value { get; set; }

        public string OriginMessage { get; set; }

        public DateTime Date { get; set; }

    }
}
