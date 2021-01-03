using System;
using Microsoft.AspNetCore.Mvc;
using Food.Core;
using Food.DataModel;
using System.Collections.Generic;
using Newtonsoft.Json;

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
		public IActionResult TestGet()
        {
			var advice = new Dictionary<string, string>() { { "Health", "3 per day is preferred" } };
			FoodDescription food = new FoodDescription("Apple", 1.1, advice);
            return Ok(food);
        }

		[HttpGet("{name}")]
		public IActionResult GetByName([FromQuery] string name)
		{
			return Ok();
		}
	}
}

