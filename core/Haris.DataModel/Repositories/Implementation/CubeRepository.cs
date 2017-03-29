using System;
using Haris.DataModel.DataModels;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Haris.DataModel.Repositories.Implementation
{
    public class CubeRepository : MongoRepository, ICubeRepository
    {
        private IMongoCollection<Cube> _cubes;

        public CubeRepository()
        {
            _cubes = _db.GetCollection<Cube>("cubes");
        }

        public void CreateCube(Cube cube)
        {
            _cubes.InsertOne(cube);
        }

        public Cube GetCube(string address)
        {
            return _cubes.FindSync(x => x.CubeAddress.Contains(address)).FirstOrDefault();
        }

        public void AddLog(Log log, string address)
        {
            var filtr = Builders<Cube>
                .Filter.Eq(e => e.CubeAddress, address);
            var update = Builders<Cube>.Update
            .Push<Log>(e => e.Logs, log);
            _cubes.FindOneAndUpdate<Cube>(filtr, update);
            
        }

        public void UpdateCube(Cube cube)
        {
            _cubes.ReplaceOne(x=>x.Id == cube.Id, cube);
        }

    }
}
