using System.Collections.Generic;
using System.Linq;

namespace BudgetManager.Common.FoldersAndFiles
{
	/// <summary>
	/// Handles the CDV side of the File Reading
	/// </summary>
	public class CsvFileReader : TextFileReader
	{
		public List<List<string>> CsvRawData { get; set; }

		private const char Separator = ',';

		public CsvFileReader(string file) : base(file)
		{
			CsvRawData = new List<List<string>>();
			foreach (string row in Data)
			{
				CsvRawData.Add(row.Split(Separator).ToList());
			}
		}
	}
}