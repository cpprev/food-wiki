using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataBase.Core
{
	public abstract class DataBase
	{
		public abstract Task<bool> AddOrUpdateTrie(string collectionName, string word);

		public abstract Task<List<string>> GetWithPattern(string collectionName, string pattern);

		/// <summary>
		/// Retreives all elements from a collection
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="collectionName"></param>
		/// <returns></returns>
		public abstract Task<List<T>> GetAllElements<T>(string collectionName);

		/// <summary>
		/// Adds an elements in a collection
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="collectionName"></param>
		/// <param name="element"></param>
		/// <returns></returns>
		public abstract Task<bool> Add<T>(string collectionName, T element);

		/// <summary>
		/// Gets an element from a collection by its Id.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="collectionName"></param>
		/// <param name="id"></param>
		/// <returns></returns>
		public abstract Task<T> GetElementById<T>(string collectionName, string id);

		/// <summary>
		/// Gets an element from a collection by a given property
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="collectionName"></param>
		/// <param name="propName"></param>
		/// <param name="propValue"></param>
		/// <returns></returns>
		public abstract Task<T> GetElementByProperty<T>(string collectionName, string propName, string propValue);

		/// <summary>
		/// Removes an elements from a collection thanks to its Id
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="collectionName"></param>
		/// <param name="id"></param>
		/// <returns></returns>
		public abstract Task<bool> RemoveById<T>(string collectionName, string id);

		/// <summary>
		/// Removes an elements from a collection thanks to a given property
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="collectionName"></param>
		/// <param name="propName"></param>
		/// <param name="propValue"></param>
		/// <returns></returns>
		public abstract Task<bool> RemoveByProperty<T>(string collectionName, string propName, string propValue);

		/// <summary>
		/// Removes all elements from a collection
		/// </summary>
		/// <param name="collectionName"></param>
		/// <returns></returns>
		public abstract Task Clear(string collectionName);
	}
}

