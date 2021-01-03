using System;
using Microsoft.AspNetCore.Mvc;
using Food.DataModel;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Food.RestApi
{
	[ApiController]
	[Route("[controller]")]
	public class FoodController : ControllerBase
	{
		public FoodController()
		{
		}

		[HttpGet()]
		public IActionResult TestGet()
        {
			var advice = new Dictionary<string, string>() { { "Health", "3 per day is preferred" } };
			FoodDescription food = new FoodDescription("Apple", 1.1, advice);
            return Ok(food);
        }
	}
}

