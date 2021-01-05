using System;

namespace Food.Core
{
	public class FoodService
	{
		public DataBase.Core.DataBase TrieDataBase { get; set; }
		public DataBase.Core.DataBase DataBase { get; set; }

		public readonly string TrieCollectionName = "TrieFood";
		public readonly string CollectionName = "Food";

		public FoodService()
        {
			TrieDataBase = new MongoDBDataBase.Core.MongoDBDataBase(TrieCollectionName);
			DataBase = new MongoDBDataBase.Core.MongoDBDataBase(CollectionName);
        }

		public FoodApi GetApi()
        {
			return new FoodApi(this);
        }
	}
}
