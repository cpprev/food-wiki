using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataBase.Core
{
	public abstract class DataBase
	{
		public abstract Task<T> GetElementById<T>(string collectionName, string id);

		public abstract Task<T> GetElementByProperty<T>(string collectionName, string propName, string propValue);

		public abstract Task<List<T>> GetAllElements<T>(string collectionName);

		public abstract Task<bool> Add<T>(string collectionName, T element);

		public abstract Task<bool> RemoveById<T>(string collectionName, string id);

		public abstract Task<bool> RemoveByProperty<T>(string collectionName, string propName, string propValue);

		public abstract Task Clear(string collectionName);
	}
}

