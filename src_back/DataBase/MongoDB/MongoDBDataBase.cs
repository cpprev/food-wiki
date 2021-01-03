using System;
using System.Collections.Generic;
using MongoDB.Driver;

namespace DataBase
{
	public class MongoDBDataBase
	{
		public T GetElementById<T>(string id)
        {
			return default(T);
        }

		public List<T> GetAllElements<T>()
        {
			return null;
        }

		public bool Add<T>(T element)
        {
			return true;
        }
			
		public bool RemoveById<T>(string id)
        {
			return true;
        }
	}
}

