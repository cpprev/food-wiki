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
			return Ok(await _foodService.GetApi().GetAll());
        }

		[HttpGet("load")]
		public async Task<IActionResult> Load()
		{
			var contents = System.IO.File.ReadAllText("C:\\CaloryCalculator\\objects\\food.json");
			var elements = JsonConvert.DeserializeObject<List<FoodDescription>>(contents);
			foreach (var element in elements)
            {
				await _foodService.GetApi().Insert(element);
			}
			return Ok();
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

