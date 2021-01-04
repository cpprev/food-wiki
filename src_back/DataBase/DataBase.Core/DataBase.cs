using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataBase.Core
{
	public abstract class DataBase
	{
		public abstract Task<T> GetElementById<T>(string id);

		public abstract Task<T> GetElementByProperty<T>(string propName, string propValue);

		public abstract Task<List<T>> GetAllElements<T>();

		public abstract Task<bool> Add<T>(T element);

		public abstract Task<bool> RemoveById<T>(string id);

		public abstract Task<bool> RemoveByProperty<T>(string propName, string propValue);

		public abstract Task Clear();
	}
}

