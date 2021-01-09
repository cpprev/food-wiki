using System;
using Microsoft.AspNetCore.Mvc;
using Food.Core;
using Food.DataModel;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Authentication;

namespace Food.RestApi
{
	[ApiController]
	[Route("food")]
	[Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
	public class AdminFoodController : Controller
	{
		private FoodService _foodService;

		public AdminFoodController(FoodService foodService)
		{
			_foodService = foodService;
		}


		[HttpGet("load")]
		public async Task<IActionResult> Load()
		{
			var contents = System.IO.File.ReadAllText("C:\\FoodWiki\\objects\\food.json");
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


		[HttpGet("removeById/{id}")]
		public async Task<IActionResult> RemoveById([FromRoute] string id)
		{
			if (await _foodService.GetApi().RemoveById(id))
				return NotFound($"Object with id '{id}' not found.");
			return Ok();
		}

		[HttpGet("removeByName/{name}")]
		public async Task<IActionResult> RemoveByName([FromRoute] string name)
		{
			if (await _foodService.GetApi().RemoveByName(name))
				return NotFound($"Object with name '{name}' not found.");
			return Ok();
		}

		[HttpGet("clear")]
		public async Task<IActionResult> Clear()
		{
			await _foodService.GetApi().Clear();
			return Ok();
		}
	}
}

