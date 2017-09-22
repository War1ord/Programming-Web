using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RequestForService.Common.Extensions;

namespace RequestForService.Common.Tests
{
	[TestClass]
	public class StringExtentions
	{
		[TestMethod]
		public void StringToGenericType_ToInt_123()
		{
			//Arrange
			const string str = "123";
			//Act
			var numb123 = str.To<int>();
			//Assert
			Assert.AreEqual(numb123, 123, "The conversion failed");
		}

		[TestMethod]
		public void StringToGenericType_ToDecimal_50P55()
		{
			//Arrange
			const string str = "50.55";
			const decimal dec = (decimal)50.55;
			//Act
			var dec50P55 = str.To<decimal>();
			//Assert
			Assert.AreEqual(dec50P55, dec, "The conversion failed");
		}

		[TestMethod]
		public void StringToGenericType_ToNullableInt_123()
		{
			//Arrange
			const string str = "123";
			//Act
			int? numb123 = str.To<int?>();
			//Assert
			Assert.AreEqual(numb123, 123, "The conversion failed");
		}

		[TestMethod]
		public void StringToGenericType_ToNullableDecimal_50P55()
		{
			//Arrange
			const string str = "50.55";
			const decimal dec = (decimal)50.55;
			//Act
			decimal? dec50P55 = str.To<decimal?>();
			//Assert
			Assert.AreEqual(dec50P55, dec, "The conversion failed");
		}

		[TestMethod]
		public void StringToGenericType_ToNullableIntWithBlankValue_Null()
		{
			//Arrange
			const string str = "";
			//Act
			int? numb123 = str.To<int?>();
			//Assert
			Assert.AreEqual(numb123, null, "The conversion failed");
		}

		[TestMethod]
		public void StringToGenericType_ToNullableDecimalWithBlankValue_Null()
		{
			//Arrange
			const string str = "";
			//Act
			decimal? dec50P55 = str.To<decimal?>();
			//Assert
			Assert.AreEqual(dec50P55, null, "The conversion failed");
		}

		[TestMethod]
		public void StringToGenericType_ToNullableIntWithNullValue_Null()
		{
			//Arrange
			const string str = null;
			//Act
			int? numb123 = str.To<int?>();
			//Assert
			Assert.AreEqual(numb123, null, "The conversion failed");
		}

		[TestMethod]
		public void StringToGenericType_ToNullableDecimalWithNullValue_Null()
		{
			//Arrange
			const string str = null;
			//Act
			decimal? dec50P55 = str.To<decimal?>();
			//Assert
			Assert.AreEqual(dec50P55, null, "The conversion failed");
		}

		[TestMethod]
		public void StringToGenericType_ToIntWithBlankValue_InValid()
		{
			//Arrange
			const string str = "";
			int numb123 = 0;
			bool isInValid = false;
			//Act
			try
			{
				numb123 = str.To<int>();
			}
			catch
			{
				isInValid = true;
			}
			//Assert
			Assert.IsTrue(isInValid && numb123 != 123, "The conversion failed");
		}

		[TestMethod]
		public void StringToGenericType_ToDecimalWithBlankValue_InValid()
		{
			//Arrange
			const string str = "";
			const decimal dec = (decimal)50.55;
			decimal dec50P55 = 0;
			bool isInValid = false;
			//Act
			try
			{
				dec50P55 = str.To<decimal>();
			}
			catch
			{
				isInValid = true;
			}
			//Assert
			Assert.IsTrue(isInValid && dec50P55 != dec, "The conversion failed");
		}

		[TestMethod]
		public void StringTryToGenericType_ToInt_123()
		{
			//Arrange
			const string str = "123";
			int numb123;
			//Act
			var isvalid = str.TryTo<int>(out numb123);
			//Assert
			Assert.IsTrue(isvalid && numb123 == 123, "The conversion failed");
		}

		[TestMethod]
		public void StringTryToGenericType_ToDecimal_50P55()
		{
			//Arrange
			const string str = "50.55";
			const decimal dec = (decimal)50.55;
			decimal dec50P55;
			//Act
			var isvalid = str.TryTo<decimal>(out dec50P55);
			//Assert
			Assert.IsTrue(isvalid && dec50P55 == dec, "The conversion failed");
		}

		[TestMethod]
		public void StringTryToGenericType_ToNullableInt_123()
		{
			//Arrange
			const string str = "123";
			int? numb123;
			//Act
			bool isvalid = str.TryTo<int?>(out numb123);
			//Assert
			Assert.IsTrue(isvalid && numb123 == 123, "The conversion failed");
		}

		[TestMethod]
		public void StringTryToGenericType_ToNullableDecimal_50P55()
		{
			//Arrange
			const string str = "50.55";
			const decimal dec = (decimal)50.55;
			decimal? dec50P55;
			//Act
			bool isvalid = str.TryTo<decimal?>(out dec50P55);
			//Assert
			Assert.IsTrue(isvalid && dec50P55 == dec, "The conversion failed");
		}

		[TestMethod]
		public void StringTryToGenericType_ToNullableIntWithBlankValue_Null()
		{
			//Arrange
			const string str = "";
			int? numb123;
			//Act
			bool isvalid = str.TryTo<int?>(out numb123);
			//Assert
			Assert.IsTrue(isvalid && numb123 == null, "The conversion failed");
		}

		[TestMethod]
		public void StringTryToGenericType_ToNullableDecimalWithBlankValue_Null()
		{
			//Arrange
			const string str = "";
			decimal? dec50P55;
			//Act
			bool isvalid = str.TryTo<decimal?>(out dec50P55);
			//Assert
			Assert.IsTrue(isvalid && dec50P55 == null, "The conversion failed");
		}

		[TestMethod]
		public void StringTryToGenericType_ToNullableIntWithNullValue_Null()
		{
			//Arrange
			const string str = null;
			int? numb123;
			//Act
			bool isvalid = str.TryTo<int?>(out numb123);
			//Assert
			Assert.IsTrue(isvalid && numb123 == null, "The conversion failed");
		}

		[TestMethod]
		public void StringTryToGenericType_ToNullableDecimalWithNullValue_Null()
		{
			//Arrange
			const string str = null;
			decimal? dec50P55;
			//Act
			bool isvalid = str.TryTo<decimal?>(out dec50P55);
			//Assert
			Assert.IsTrue(isvalid && dec50P55 == null, "The conversion failed");
		}

		[TestMethod]
		public void StringTryToGenericType_ToIntWithBlankValue_InValid()
		{
			//Arrange
			const string str = "";
			int numb123;
			//Act
			var isvalid = str.TryTo<int>(out numb123);
			//Assert
			Assert.IsTrue(!isvalid && numb123 != 123, "The conversion failed");
		}

		[TestMethod]
		public void StringTryToGenericType_ToDecimalWithBlankValue_InValid()
		{
			//Arrange
			const string str = "";
			const decimal dec = (decimal)50.55;
			decimal dec50P55;
			//Act
			var isvalid = str.TryTo<decimal>(out dec50P55);
			//Assert
			Assert.IsTrue(!isvalid && dec50P55 != dec, "The conversion failed");
		}

		[TestMethod]
		public void StringToGenericType_ToMyEnum_Three()
		{
			//Arrange
			const string str = "Three";
			//Act
			var value = str.To<MyEnum>();
			//Assert
			Assert.AreEqual(value, MyEnum.Three, "The conversion failed");
		}

		[TestMethod]
		public void StringToGenericType_ToNullableMyEnum_Three()
		{
			//Arrange
			const string str = "Three";
			//Act
			var value = str.To<MyEnum?>();
			//Assert
			Assert.AreEqual(value, MyEnum.Three, "The conversion failed");
		}

		[TestMethod]
		public void StringToGenericType_ToNullableMyEnumBlank_Null()
		{
			//Arrange
			const string str = "";
			//Act
			var value = str.To<MyEnum?>();
			//Assert
			Assert.AreEqual(value, null, "The conversion failed");
		}

		[TestMethod]
		public void StringToGenericType_ToNullableMyEnumNull_Null()
		{
			//Arrange
			const string str = null;
			//Act
			var value = str.To<MyEnum?>();
			//Assert
			Assert.AreEqual(value, null, "The conversion failed");
		}

		[TestMethod]
		public void StringTryToGenericType_ToMyEnumBlank_InValid()
		{
			//Arrange
			const string str = "";
			MyEnum value;
			//Act
			var isvalid = str.TryTo<MyEnum>(out value);
			//Assert
			Assert.IsTrue(!isvalid, "The conversion failed");
		}

		[TestMethod]
		public void StringTryToGenericType_ToMyEnumNull_InValid()
		{
			//Arrange
			const string str = null;
			MyEnum value;
			//Act
			var isvalid = str.TryTo<MyEnum>(out value);
			//Assert
			Assert.IsTrue(!isvalid, "The conversion failed");
		}

		[TestMethod]
		public void StringTryToGenericType_ToNullableMyEnumSix_InValid()
		{
			//Arrange
			const string str = "Six";
			MyEnum? value;
			//Act
			var isvalid = str.TryTo<MyEnum?>(out value);
			//Assert
			Assert.IsTrue(!isvalid && value != MyEnum.One, "The conversion failed");
		}

		#region Helpers
		private enum MyEnum { One, Two, Three, Four }
		#endregion
	}
}
