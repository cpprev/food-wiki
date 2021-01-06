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
	[Route("food")]
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

		[HttpGet("getWithPattern")]
		public async Task<IActionResult> GetWithPattern([FromQuery] string pattern)
		{
			return Ok(await _foodService.GetApi().GetWithPattern(pattern));
		}

		[HttpGet("getById/{id}")]
		public async Task<IActionResult> GetById([FromRoute] string id)
		{
			var element = await _foodService.GetApi().GetById(id);
			if (element == null)
				return NotFound($"Object with id '{id}' not found.");
			return Ok(element);
		}

		[HttpGet("getByName/{name}")]
		public async Task<IActionResult> GetByName([FromRoute] string name)
		{
			var element = await _foodService.GetApi().GetByName(name);
			if (element == null)
				return NotFound($"Object with name '{name}' not found.");
			return Ok(element);
		}
	}
}

