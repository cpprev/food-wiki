using System;
using System.Collections.Generic;
using Food.DataModel;
using System.Threading.Tasks;

namespace Food.Core
{
	public class FoodApi
	{
		private FoodService _service;

		public FoodApi(FoodService service)
        {
			_service = service;
        }

		public async Task<bool> Insert(FoodDescription food)
        {
			return await _service.DataBase.Add<FoodDescription>(_service.CollectionName, food);
        }

		public async Task<bool> RemoveById(string id)
		{
			return await _service.DataBase.RemoveById<FoodDescription>(_service.CollectionName, id);
		}

		public async Task<bool> RemoveByName(string name)
		{
			return await _service.DataBase.RemoveByProperty<FoodDescription>(_service.CollectionName, nameof(FoodDescription.Name), name);
		}

		public async Task<FoodDescription> GetById(string id)
		{
			return await _service.DataBase.GetElementById<FoodDescription>(_service.CollectionName, id);
		}

		public async Task<FoodDescription> GetByName(string name)
		{
			return await _service.DataBase.GetElementByProperty<FoodDescription>(_service.CollectionName, nameof(FoodDescription.Name), name);
		}

		public async Task<List<FoodDescription>> GetAll()
        {
			return await _service.DataBase.GetAllElements<FoodDescription>(_service.CollectionName);
        }

		public async Task Clear()
		{
			await _service.DataBase.Clear(_service.CollectionName);
		}
	}
}
