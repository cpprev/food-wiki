using System;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;
using Food.DataModel;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization;

namespace MongoDBDataBase.Core
{
	public class MongoDBDataBase : DataBase.Core.DataBase
	{
        private IMongoDatabase _base = null;
        public IMongoDatabase Base
        {
            get
            {
                if (_base == null)
                {
                    _base = InitializeDataBase();
                }
                return _base;
            }
        }

        public IMongoDatabase InitializeDataBase()
        {
            IMongoClient client = new MongoClient("mongodb://localhost:27017/");
            IMongoDatabase database = client.GetDatabase("admin");
            if (database.GetCollection<BsonDocument>("Food") == null)
            {
                database.CreateCollection("Food");
            }

            return database;
        }

        public IMongoCollection<BsonDocument> GetCollection()
        {
            return Base.GetCollection<BsonDocument>("Food");
        }

        //////////////////////////////////////////////////////////////////////////////////////////////

        public override async Task<T> GetElementById<T>(string id)
        {
            return await GetElementByProperty<T>("_id", id);
        }

        public override async Task<T> GetElementByProperty<T>(string propName, string propValue)
        {
            var filter = Builders<BsonDocument>.Filter.Eq(propName, propValue);
            var element = await GetCollection().Find(filter).FirstOrDefaultAsync();
            return element != null ? BsonSerializer.Deserialize<T>(element) : default;
        }

        public override async Task<List<T>> GetAllElements<T>()
        {
            var documents = GetCollection().Find(new BsonDocument()).ToList();
            var objects = new List<T>();
            foreach (var doc in documents)
            {
                objects.Add(BsonSerializer.Deserialize<T>(doc));
            }
            return objects;
        }

        public override async Task<bool> Add<T>(T element)
        {
            var doc = element is BsonDocument ? element as BsonDocument : element.ToBsonDocument();
            var filter = Builders<BsonDocument>.Filter.Eq("_id", doc.GetValue("_id"));
            if (await GetCollection().Find(filter).FirstOrDefaultAsync() == null)
            {
                await GetCollection().InsertOneAsync(doc);
            }
            else
            {
                return false;
            }
            return true;
        }

        public override async Task<bool> RemoveById<T>(string id)
        {
            return await RemoveByProperty<T>("_id", id);
        }

        public override async Task<bool> RemoveByProperty<T>(string propName, string propValue)
        {
            try
            {
                var filter = Builders<BsonDocument>.Filter.Eq(propName, propValue);
                await GetCollection().DeleteOneAsync(filter);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public override async Task Clear()
        {
            await GetCollection().DeleteManyAsync(Builders<BsonDocument>.Filter.Empty);
        }
    }
}

