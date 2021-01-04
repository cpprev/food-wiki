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
			return await _service.DataBase.Add<FoodDescription>(food);
        }

		public async Task<bool> RemoveById(string id)
		{
			return await _service.DataBase.RemoveById<FoodDescription>(id);
		}

		public async Task<bool> RemoveByName(string name)
		{
			return await _service.DataBase.RemoveByProperty<FoodDescription>(nameof(FoodDescription.Name), name);
		}

		public async Task<FoodDescription> GetById(string id)
		{
			return await _service.DataBase.GetElementById<FoodDescription>(id);
		}

		public async Task<FoodDescription> GetByName(string name)
		{
			return await _service.DataBase.GetElementByProperty<FoodDescription>(nameof(FoodDescription.Name), name);
		}

		public async Task<List<FoodDescription>> GetAll()
        {
			return await _service.DataBase.GetAllElements<FoodDescription>();
        }
	}
}
