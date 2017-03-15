using System.Configuration;
using MongoDB.Driver;

namespace Haris.DataModel.Repositories
{
    public abstract class MongoRepository
    {
        protected MongoClient _provider;
        protected IMongoDatabase _db;

        public MongoRepository()
        {
            _provider = new MongoClient(ConfigurationManager.ConnectionStrings["db"].ConnectionString);
            _db = this._provider.GetDatabase(
            MongoUrl.Create(ConfigurationManager.ConnectionStrings["db"].ConnectionString).DatabaseName);
        }
    }
}
