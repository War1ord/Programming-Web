using System;

namespace BudgetManager.Enums
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			try
			{
				EnumerationsDll enumerationsDll = new EnumerationsDll();
				enumerationsDll.DisplayConfirmation();
				//enumerationsDll.PromptUser();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}	
		}
	}
}