using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Haris.DataModel.DataModels
{
    public class Log
    {
        public string Value { get; set; }

        public string OriginMessage { get; set; }

        public DateTime Date { get; set; }
    }
}
