using System;
using System.Collections.Generic;

namespace Food.DataModel
{
	public class FoodDescription
	{
		public FoodDescription(string name, double calory, Dictionary<string, string> advice)
        {
			Name = name;
			Calory = calory;
			Advice = new Dictionary<string, string>(advice);
        }

		/// <summary>
		/// Name of the food
		/// </summary>
		public string Name { get; set; }

		/// <summary>
        /// Number of calory (in [measure to define])
        /// </summary>
		public double Calory { get; set; }

		/// Enum Quality : good, bad, intermediate, ...
		/// TODO

		/// <summary>
        /// Advice on that food
        /// </summary>
		public Dictionary<string, string> Advice { get; set; } = new Dictionary<string, string>();
	}
}
