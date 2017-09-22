using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RequestForService.Data.Tests
{
	[TestClass]
	public class DataContextTests
	{
		[TestMethod]
		public void InitializeDataContext()
		{
			Data.DataContext.Initialize();
		}
	}
}
