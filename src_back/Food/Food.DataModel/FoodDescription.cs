using MongoDB.Bson.Serialization.Attributes;

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
        /// Number of calory (in [measure to define])
        /// </summary>
		public double Calory { get; set; }

		/// <summary>
        /// Advice on that food
        /// </summary>
		public Dictionary<string, string> Advice { get; set; } = new Dictionary<string, string>();

		public FoodDescription(string name, double calory, Dictionary<string, string> advice)
		{
			Name = name;
			Calory = calory;
			Advice = new Dictionary<string, string>(advice);

			Id = SetId();
		}

		private string SetId()
        {
			string input = Name + Calory.ToString() + String.Join(';', Advice);
			Console.WriteLine("Hash input is : " + input);

			byte[] data = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(input));
			var sBuilder = new StringBuilder();
			for (int i = 0; i < data.Length; i++)
			{
				sBuilder.Append(data[i].ToString("x2"));
			}

			string hash = sBuilder.ToString();
			Console.WriteLine($"The SHA256 hash is: {hash}.");

			return hash;
        }
	}
}
