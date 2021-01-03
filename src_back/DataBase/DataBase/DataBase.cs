using System;
using System.Collections.Generic;

namespace DataBase
{
	public abstract class DataBase
	{
		public abstract T GetElementById<T>(string id);

		public abstract List<T> GetAllElements<T>();

		public abstract bool Add<T>(T element);

		public abstract bool RemoveById<T>(string id);
	}
}

