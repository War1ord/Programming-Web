using System;
using BudgetManager.Business.Error;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BudgetManager.Business.Test
{
	[TestClass]
	public class ErrorTests
	{
		[TestMethod]
		public void LogException()
		{
			try
			{
				try
				{
					throw new Exception("Test Exception");
				}
				catch (Exception e)
				{
					ErrorManager.LogException(e);
				}
			}
			catch (Exception e)
			{
				Assert.Fail("LogException failed, " + e.Message);
			}
		}
	}
}