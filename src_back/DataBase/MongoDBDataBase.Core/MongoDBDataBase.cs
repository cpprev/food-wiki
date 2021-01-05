using System;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;
using Food.DataModel;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization;
using Trie.DataModel;
using System.Linq;

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

        #region TrieDataBase methods

        private async Task<TrieRootDescription> GetTrieById(string collectionName, string id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
            var element = (await GetCollection(collectionName).FindAsync(filter)).FirstOrDefault();
            return element != null ? BsonSerializer.Deserialize<TrieRootDescription>(element) : default;
        }

        public override async Task<bool> AddOrUpdateTrie(string collectionName, string word)
        {
            var prefix = word.Substring(0, 2);
            Console.WriteLine("prefix: " + prefix);
            var trie = await GetTrieById(collectionName, prefix);
            if (trie == null)
            {
                Console.WriteLine("(Add) : NULL");

                trie = new TrieRootDescription(prefix);
                trie.AddWord(word);
                await GetCollection(collectionName).InsertOneAsync(trie.ToBsonDocument());
            }
            else
            {
                trie.AddWord(word);
                var filter = Builders<BsonDocument>.Filter.Eq("_id", trie.Id);
                await GetCollection(collectionName).DeleteOneAsync(trie.ToBsonDocument());
                await GetCollection(collectionName).InsertOneAsync(trie.ToBsonDocument());
            }
            
            return true;
        }

        public override async Task<List<string>> GetWithPattern(string collectionName, string pattern)
        {
            /// TODO For now, if pattern is one char long, take a random trie
            if (pattern.Length == 1)
            {
                for (var i = 0; i < TrieConstants.NbCharSupported; ++i)
                {
                    char c = TrieConstants.CharTable.FirstOrDefault(p => p.Value == i).Key;
                    var tempPattern = pattern + c;
                    var testTrie = await GetTrieById(collectionName, tempPattern);
                    if (testTrie != null)
                    {
                        return testTrie.AutoComplete(testTrie.Trie, tempPattern);
                    }
                }
                return new List<string>();
            }

            string savePattern = pattern;
            pattern = pattern.Substring(0, 2);

            var trie = await GetTrieById(collectionName, pattern);
            if (trie == null)
            {
                return new List<string>();
            }

            return trie.AutoComplete(trie.Trie, savePattern);
        }

        #endregion

        #region DataBase methods

        public override async Task<T> GetElementById<T>(string collectionName, string id)
        {
            return await GetElementByProperty<T>(collectionName, "_id", id);
        }

        public override async Task<T> GetElementByProperty<T>(string collectionName, string propName, string propValue)
        {
            var filter = Builders<BsonDocument>.Filter.Eq(propName, propValue);
            var element = (await GetCollection(collectionName).FindAsync(filter)).FirstOrDefault();
            return element != null ? BsonSerializer.Deserialize<T>(element) : default;
        }

        public override async Task<List<T>> GetAllElements<T>(string collectionName)
        {
            var documents = (await GetCollection(collectionName).FindAsync(new BsonDocument())).ToList();
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
            if ((await GetCollection(collectionName).FindAsync(filter)).FirstOrDefault() == null)
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

