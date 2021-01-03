using System;
using System.Collections.Generic;

namespace Food.DataModel
{
	public class Food
	{
		/// <summary>
        /// Number of calory (in [measure to define])
        /// </summary>
		public float Calory { get; set; }

		/// Enum Quality : good, bad, intermediate, ...
		/// TODO

		/// <summary>
        /// Advice on that food
        /// </summary>
		public Dictionary<string, string> Advice { get; set; } = new Dictionary<string, string>();
	}
}
