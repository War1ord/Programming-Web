using System;
using System.Collections.Generic;
using System.IO;

namespace BudgetManager.Common.FoldersAndFiles
{
	/// <summary>
	/// Handles the reading of the text files
	/// </summary>
	public abstract class TextFileReader
	{
		public string File { get; set; }

		private StreamReader TextStream { get; set; }

		public List<string> Data { get; set; }

		public Exception Error { get; set; }

		protected TextFileReader(string file)
		{
			try
			{
				File = file;
				Data = new List<string>();
				TextStream = new StreamReader(file);
				string readLine = TextStream.ReadLine();
				while (readLine != null)
				{
					Data.Add(readLine);
					readLine = TextStream.ReadLine();
				}
			}
			catch (Exception e)
			{
				Error = e;
			}
		}
	}
}