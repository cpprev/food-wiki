﻿using System;
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
                    _base = InitializeDataBase();
                return _base;
            }
        }

        public MongoDBDataBase(string collectionName)
        {
            InitializeCollection(collectionName);
        }

        private IMongoDatabase InitializeDataBase()
        {
            IMongoClient client = new MongoClient("mongodb://localhost:27017/");
            return client.GetDatabase("admin");
        }

        private void InitializeCollection(string collectionName)
        {
            if (GetCollection(collectionName) == null)
                Base.CreateCollection(collectionName);
        }

        private IMongoCollection<BsonDocument> GetCollection(string collectionName)
        {
            return Base.GetCollection<BsonDocument>(collectionName);
        }

        #region DataBase methods

        public override async Task<T> GetElementById<T>(string collectionName, string id)
        {
            return await GetElementByProperty<T>(collectionName, "_id", id);
        }

        public override async Task<T> GetElementByProperty<T>(string collectionName, string propName, string propValue)
        {
            var filter = Builders<BsonDocument>.Filter.Eq(propName, propValue);
            var element = await GetCollection(collectionName).Find(filter).FirstOrDefaultAsync();
            return element != null ? BsonSerializer.Deserialize<T>(element) : default;
        }

        public override async Task<List<T>> GetAllElements<T>(string collectionName)
        {
            var documents = GetCollection(collectionName).Find(new BsonDocument()).ToList();
            var objects = new List<T>();
            foreach (var doc in documents)
            {
                objects.Add(BsonSerializer.Deserialize<T>(doc));
            }
            return objects;
        }

        public override async Task<bool> Add<T>(string collectionName, T element)
        {
            var doc = element is BsonDocument ? element as BsonDocument : element.ToBsonDocument();
            var filter = Builders<BsonDocument>.Filter.Eq("_id", doc.GetValue("_id"));
            if (await GetCollection(collectionName).Find(filter).FirstOrDefaultAsync() == null)
            {
                await GetCollection(collectionName).InsertOneAsync(doc);
            }
            else
            {
                return false;
            }
            return true;
        }

        public override async Task<bool> RemoveById<T>(string collectionName, string id)
        {
            return await RemoveByProperty<T>(collectionName, "_id", id);
        }

        public override async Task<bool> RemoveByProperty<T>(string collectionName, string propName, string propValue)
        {
            try
            {
                var filter = Builders<BsonDocument>.Filter.Eq(propName, propValue);
                await GetCollection(collectionName).DeleteOneAsync(filter);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public override async Task Clear(string collectionName)
        {
            await GetCollection(collectionName).DeleteManyAsync(Builders<BsonDocument>.Filter.Empty);
        }

        #endregion
    }
}
