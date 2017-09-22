using System;
using System.Diagnostics;
using BudgetManager.Business;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BudgetManager.Common.Test {
	[TestClass]
	public class Folders {
		[TestMethod]
		public void FoldersGetsData() {
			var folderManager = new FolderManager(@"C:\Program Files");
			Assert.IsNotNull(folderManager);
			Assert.IsNull(folderManager.Error);
		}
	}

	[TestClass]
	public class Data {
		[TestMethod]
		public void GetCsvData() {
			var csvFileReader = new CsvFileReader(Environment.CurrentDirectory
					+ @"\..\..\.."
					+ @"\Testing Data\DataImports\test.csv");
			Assert.IsNotNull(csvFileReader);
			csvFileReader =
				new CsvFileReader(Environment.CurrentDirectory
					+ @"\..\..\.."
					+ @"\Testing Data\DataImports\Absa Transation History\CHEQUE Account\Absa 2010-09-28 to 2010-11-06.csv");
			Assert.IsNotNull(csvFileReader);
		}
	}

	[TestClass]
	public class ExtentionMethods {
		[TestMethod]
		public void ObjectName() {
			//string test = new string("test".ToCharArray());
			//var result = test.ObjectName();
			//Assert.AreEqual("test", result);
		}
	}
}