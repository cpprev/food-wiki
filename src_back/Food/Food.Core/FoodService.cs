using System;

namespace Food.Core
{
	public class FoodService
	{
		public DataBase.Core.DataBase DataBase { get; set; }

		public readonly string CollectionName = "Food";

		public FoodService()
        {
			string collectionName = CollectionName;
			DataBase = new MongoDBDataBase.Core.MongoDBDataBase(collectionName);
        }

		public FoodApi GetApi()
        {
			return new FoodApi(this);
        }
	}
}
