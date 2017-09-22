using BudgetManager.Models;
using BudgetManager.Models.ComplexTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading;

namespace BudgetManager.Data.Test
{
	/// <summary>
	/// The DataContextCreate test Class holding all Create Static Data and Create Db
	/// </summary>
	[TestClass]
	public class DataContextCreate
	{
		/// <summary>
		/// Create database	
		/// </summary>
		[TestMethod]
		public void CreateDatabase()
		{
			DataContext.Initialize();
		}
	}
}