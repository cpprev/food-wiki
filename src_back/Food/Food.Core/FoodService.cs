using System;

namespace Food.Core
{
	public class FoodService
	{
		public DataBase.Core.DataBase DataBase { get; set; }

		public FoodService()
        {
			DataBase = new MongoDBDataBase.Core.MongoDBDataBase();
        }

		public FoodApi GetApi()
        {
			return new FoodApi(this);
        }
	}
}
