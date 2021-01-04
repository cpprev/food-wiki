using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Food.DataModel
{
	public class FoodDescription
	{
		[BsonId]
		public string Id { get; set; }

		/// <summary>
		/// Name of the food
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Description of the food
		/// </summary>
		public string Description { get; set; }

		/// <summary>
        /// Number of calory (in [measure to define])
        /// </summary>
		public double Calory { get; set; }

		/// <summary>
		/// Quality of a food (good, bad, intermediate)
		/// </summary>
		[JsonConverter(typeof(StringEnumConverter))]
		public NutriScore NutriScore { get; set; }

		/// <summary>
		/// Pros on that food
		/// </summary>
		public Dictionary<string, string> Pros { get; set; } = new Dictionary<string, string>();

		/// <summary>
		/// Cons on that food
		/// </summary>
		public Dictionary<string, string> Cons { get; set; } = new Dictionary<string, string>();

		/// <summary>
		/// Advice on that food
		/// </summary>
		public Dictionary<string, string> Articles { get; set; } = new Dictionary<string, string>();


		public FoodDescription() {}

		public FoodDescription(string name, double calory, Dictionary<string, string> articles)
		{
			Name = name;
			Calory = calory;
			Articles = new Dictionary<string, string>(articles);

			SetId();
		}

		public void SetId()
        {
			string input = Name + Description + Calory.ToString() + NutriScore.ToString()
				+ String.Join(';', Articles) + String.Join(';', Pros) + String.Join(';', Cons);

			byte[] data = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(input));
			var sBuilder = new StringBuilder();
			foreach (byte b in data)
			{
				sBuilder.Append(string.Format("{0:X2}", b));
			}

			Id = sBuilder.ToString();
        }
	}
}
