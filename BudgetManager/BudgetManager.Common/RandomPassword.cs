using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManager.Common
{
	/// <summary>
	/// A class to generate a random password.
	/// </summary>
	public class RandomPassword
	{
		/// <summary>
		/// Gets or sets the password.
		/// </summary>
		/// <value>
		/// The password.
		/// </value>
		public string Password { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="RandomPassword" /> class.
		/// </summary>
		/// <param name="length">The length.</param>
		public RandomPassword(int length)
		{
			Password = CreateRandomPassword(length);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RandomPassword"/> class.
		/// </summary>
		/// <param name="length">The length.</param>
		/// <param name="numberOfNonAlphanumericCharacters">The number of non alphanumeric characters.</param>
		public RandomPassword(int length, int numberOfNonAlphanumericCharacters)
		{
			Password = GenerateRandomPassword(length, numberOfNonAlphanumericCharacters);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RandomPassword" /> class.
		/// </summary>
		public RandomPassword()
		{
			Password = CreateRandomPassword(10);
		}

		/// <summary>
		/// Creates the random password.
		/// </summary>
		/// <param name="length">The length.</param>
		/// <returns></returns>
		public string CreateRandomPassword(int length)
		{
			var builder = new StringBuilder();
			var random = new Random((int) DateTime.Now.Ticks);
			for (int i = 0; i < length; i++)
			{
				double floor = Floor(random);
				char value = Convert.ToChar(Convert.ToInt32(floor));
				builder.Append(value);
			}
			return builder.ToString();
		}

		/// <summary>
		/// Generates the random password.
		/// </summary>
		/// <param name="length">The length.</param>
		/// <param name="numberOfNonAlphanumericCharacters">The number of non alphanumeric characters.</param>
		/// <returns></returns>
		public string GenerateRandomPassword(int length, int numberOfNonAlphanumericCharacters)
		{
			return System.Web.Security.Membership.GeneratePassword(length, numberOfNonAlphanumericCharacters);
		}

		/// <summary>
		/// Floors the specified random.
		/// </summary>
		/// <param name="random">The random.</param>
		/// <returns></returns>
		public static double Floor(Random random)
		{
			const int added = 65;
			const int multiplier = 26;
			double nextDouble = random.NextDouble();
			return Math.Floor(multiplier*nextDouble + added);
		}
	}
}