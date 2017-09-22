using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BudgetManager.Common.Test
{
	/// <summary>
	/// Summary description for RandomPasswordTest
	/// </summary>
	[TestClass]
	public class RandomPasswordTests
	{
		[TestMethod]
		public void RandomPasswordCreate()
		{
			const int length = 10;
			var randomPassword = new RandomPassword(length).Password;
			Assert.IsTrue(!string.IsNullOrWhiteSpace(randomPassword),
			              "The random password is null or empty");
			Assert.IsTrue(randomPassword != null && randomPassword.Length == length,
			              "The length of random password is invalid");
		}

		[TestMethod]
		public void RandomPasswordCreateWithMembership()
		{
			const int length = 10;
			const int numberOfNonAlphanumericCharacters = 5;
			var randomPassword = new RandomPassword(length, numberOfNonAlphanumericCharacters).Password;
			Assert.IsTrue(!string.IsNullOrWhiteSpace(randomPassword),
			              "The random password is null or empty");
			Assert.IsTrue(randomPassword != null && randomPassword.Length == length,
			              "The length of random password is invalid");
		}
	}
}