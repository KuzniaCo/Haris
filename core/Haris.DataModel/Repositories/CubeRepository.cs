using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Haris.DataModel.DataModels;
using MongoDB.Driver;

namespace Haris.DataModel.Repositories
{
    public class CubeRepository
    {
        private IMongoClient _client;
        private IMongoDatabase _db;
        private IMongoCollection<Cube> _cubes;

        public CubeRepository()
        {
            _client = new MongoClient("mongodb://localhost:27017");
            _db = _client.GetDatabase("CubeServer");
            _cubes = _db.GetCollection<Cube>("cubes");
        }

        public void CreateCube()
        {
            _cubes.InsertOne(new Cube() {CubeAddress = "ad5ft", CubeType = "RelayCube"});
        }

        public Cube GetCube(string address)
        {
            return _cubes.FindSync(x => x.CubeAddress == address).First();
        }
    }
}
