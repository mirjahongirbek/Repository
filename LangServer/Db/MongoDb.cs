
using MongoDB.Driver;

namespace LangServer.Db
{
    public class MongoDb : MongoRepository.Context.IMongoContext
    {
        //TODO ConnectionStirng
        public MongoDb()
        {
            string connectionString = "mongodb://localhost/johalang";
                var _databaseName = MongoUrl.Create(connectionString).DatabaseName;
            MongoClient client = new MongoClient(connectionString);
           Database=  client.GetDatabase(_databaseName);
        }
        public IMongoDatabase Database { get; }
    }
}
