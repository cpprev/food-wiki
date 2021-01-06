using MongoDB.Bson.Serialization.Attributes;
//using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;

namespace Food.DataModel
{
	public class FoodDescription
	{
		public FoodDescription()
		{}

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

		/// <summary>
		/// Id of the food
		/// </summary>
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
		[JsonConverter(typeof(JsonStringEnumConverter))]
		public NutriScore NutriScore { get; set; }

		/// <summary>
		/// Similar Food Ids
		/// </summary>
		public List<string> SimilarFoodId { get; set; } = new List<string>();

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
	}
}
