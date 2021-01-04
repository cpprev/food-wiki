using System;

namespace Food.Core
{
	public class FoodService
	{
		public DataBase.Core.DataBase DataBase { get; set; }

		public readonly string CollectionName = "Food";

		public FoodService()
        {
			DataBase = new MongoDBDataBase.Core.MongoDBDataBase(CollectionName);
        }

		public FoodApi GetApi()
        {
			return new FoodApi(this);
        }
	}
}
