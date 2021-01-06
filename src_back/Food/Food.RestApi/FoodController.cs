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
		private readonly string portalUrl = "http://localhost:4200";
		private FoodService _foodService;

		public FoodController(FoodService foodService)
		{
			_foodService = foodService;
		}

		public void AllowPortal()
		{
			Response.Headers.Add("Access-Control-Allow-Origin", portalUrl);
		}

		[HttpGet()]
		public async Task<IActionResult> GetAll()
		{
			AllowPortal();

			return Ok(await _foodService.GetApi().GetAll());
		}

		[HttpGet("getWithPattern")]
		public async Task<IActionResult> GetWithPattern([FromQuery] string pattern)
		{
			AllowPortal();

			return Ok(await _foodService.GetApi().GetWithPattern(pattern));
		}

		[HttpGet("load")]
		public async Task<IActionResult> Load()
		{
			AllowPortal();

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
			AllowPortal();

			var element = await _foodService.GetApi().Insert(food);
			return Ok(element);
		}

		[HttpGet("getById/{id}")]
		public async Task<IActionResult> GetById([FromRoute] string id)
		{
			AllowPortal();

			var element = await _foodService.GetApi().GetById(id);
			if (element == null)
				return NotFound($"Object with id '{id}' not found.");
			return Ok(element);
		}

		[HttpGet("getByName/{name}")]
		public async Task<IActionResult> GetByName([FromRoute] string name)
		{
			AllowPortal();

			var element = await _foodService.GetApi().GetByName(name);
			if (element == null)
				return NotFound($"Object with name '{name}' not found.");
			return Ok(element);
		}

		[HttpGet("removeById/{id}")]
		public async Task<IActionResult> RemoveById([FromRoute] string id)
		{
			AllowPortal();

			if (await _foodService.GetApi().RemoveById(id))
				return NotFound($"Object with id '{id}' not found.");
			return Ok();
		}

		[HttpGet("removeByName/{name}")]
		public async Task<IActionResult> RemoveByName([FromRoute] string name)
		{
				AllowPortal();

				if (await _foodService.GetApi().RemoveByName(name))
				return NotFound($"Object with name '{name}' not found.");
			return Ok();
		}

		// FIXME Temporary
		[HttpGet("clear")]
		public async Task<IActionResult> Clear()
		{
			AllowPortal();

			await _foodService.GetApi().Clear();
			return Ok();
		}
	}
}

