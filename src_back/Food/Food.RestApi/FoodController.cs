using System;
using Microsoft.AspNetCore.Mvc;
using Food.Core;
using Food.DataModel;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Food.RestApi
{
	[ApiController]
	[Route("[controller]")]
	public class FoodController : Controller
	{
		private FoodService _foodService;

		public FoodController(FoodService foodService)
		{
			_foodService = foodService;
		}

		[HttpGet()]
		public async Task<IActionResult> GetAll()
        {
			var advice = new Dictionary<string, string>() { { "Health", "3 per day is preferred" } };
			FoodDescription food1 = new FoodDescription("Apple", 1.1, advice);
			var advice2 = new Dictionary<string, string>() { { "Additional", "ayo" } };
			FoodDescription food2 = new FoodDescription("Tomato", 1.1, advice2);

			await _foodService.GetApi().Insert(food1);
			await _foodService.GetApi().Insert(food2);

			return Ok(await _foodService.GetApi().GetAll());
        }

		[HttpGet("insert")]
		public async Task<IActionResult> Insert([FromBody] FoodDescription food)
		{
			var element = await _foodService.GetApi().Insert(food);
			return Ok(element);
		}

		[HttpGet("getById/{id}")]
		public async Task<IActionResult> GetById([FromRoute] string id)
		{
			var element = await _foodService.GetApi().GetById(id);
			if (element == null)
				return BadRequest($"Object with id '{id}' was not found in database.");
			return Ok(element);
		}

		[HttpGet("getByName/{name}")]
		public async Task<IActionResult> GetByName([FromRoute] string name)
		{
			var element = await _foodService.GetApi().GetByName(name);
			if (element == null)
				return BadRequest($"Object with name '{name}' was not found in database.");
			return Ok(element);
		}

		[HttpGet("removeById/{id}")]
		public async Task<IActionResult> RemoveById([FromRoute] string id)
		{
			if (await _foodService.GetApi().RemoveById(id))
				return BadRequest($"Object with id '{id}' was not found in database.");
			return Ok();
		}

		[HttpGet("removeByName/{name}")]
		public async Task<IActionResult> RemoveByName([FromRoute] string name)
		{
			if (await _foodService.GetApi().RemoveByName(name))
				return BadRequest($"Object with name '{name}' was not found in database.");
			return Ok();
		}

		// FIXME Temporary
		[HttpGet("clear")]
		public async Task<IActionResult> Clear()
		{
			await _foodService.DataBase.Clear();
			return Ok();
		}
	}
}

