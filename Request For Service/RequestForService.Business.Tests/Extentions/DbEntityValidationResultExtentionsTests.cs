using System.Collections.Generic;
using System.Data.Entity.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RequestForService.Data.Extentions;

namespace RequestForService.Business.Tests.Extentions
{
	[TestClass]
	public class DbEntityValidationResultExtentionsTests
	{
		[TestMethod]
		public void ToHtmlValidMultiLineString_ReturnsNotNull()
		{
			//Arrange
			var errors = new List<DbValidationError>
			{
				new DbValidationError("test 1", "error 1"),
				new DbValidationError("test 2", "error 2"),
				new DbValidationError("test 3", "error 3"),
				new DbValidationError("test 4", "error 4"),
				new DbValidationError("test 5", "error 5"),
			};
			//Act
			var multiLineString = errors.ToHtmlValidMultiLineString();
			//Assert
			Assert.IsNotNull(multiLineString);
		}
	}
}
