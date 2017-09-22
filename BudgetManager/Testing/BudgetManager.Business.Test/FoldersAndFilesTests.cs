using System;
using BudgetManager.Business;
using BudgetManager.Business.Error;
using BudgetManager.Common.FoldersAndFiles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BudgetManager.Common.Test
{
	[TestClass]
	public class FoldersAndFiles
	{
		[TestMethod]
		public void FolderManagerGetsData()
		{
			try
			{
				var folderManager = new FolderManager(@"C:\Program Files", "*");
				Assert.IsNotNull(folderManager);
				Assert.IsNull(folderManager.Error);
			}
			catch (Exception e)
			{
				ErrorManager.LogException(e);
				Assert.Fail(e.Message);
			}
		}

		[TestMethod]
		public void CsvFileReaderGetData()
		{
			try
			{
				var csvFileReader =
                    new CsvFileReader(System.Environment.CurrentDirectory + @"\..\..\.." + @"\Testing Data" + @"\DataImports" +
                                      @"\test.csv");
				Assert.IsNotNull(csvFileReader);
				csvFileReader =
					new CsvFileReader(System.Environment.CurrentDirectory + @"\..\..\.." + @"\Testing Data" + @"\DataImports" +
					                  @"\Absa Transation History" + @"\CHEQUE Account" + @"\Absa 2010-09-28 to 2010-11-06.csv");
				Assert.IsNotNull(csvFileReader);
			}
			catch (Exception e)
			{
				ErrorManager.LogException(e);
				Assert.Fail(e.Message);
			}
		}
	}
}