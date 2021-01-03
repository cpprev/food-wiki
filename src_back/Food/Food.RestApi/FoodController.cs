using System;
using Microsoft.AspNetCore.Mvc;

namespace Food.RestApi
{
	public class FoodController : Controller
	{
		[HttpGet()]
		public IActionResult TestGet()
        {
			Console.WriteLine("Works :)");
			return Ok();
        }
	}
}

