//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System;

//namespace RequestForService.Business.Tests.Users
//{
//	[TestClass]
//	public class RegisterUserTests
//	{
//		[TestMethod]
//		public void RegistrationCreate_NewInstanceOfARegistration_SuccessfulResult()
//		{
//			//Arrange
//			var email = Guid.NewGuid().ToString().Substring(0,6) + "@mail.com";
//			var password = Guid.NewGuid().ToString().Substring(0, 6);
//			var validationCode = Guid.NewGuid().ToString().Substring(0, 6);
//			var registration = new Models.Users.Registration
//			{
//				EmailAddress = email,
//				Password = password,
//				ValidationCode = validationCode,
//				AcceptTerms = true,
//				ReceiveNewsletters = true,
//			};
//			//Act
//			Models.Result result = new Business.Users.Registration().CreateEntity(registration);
//			//Assert
//			Assert.IsTrue(result.IsSuccessful, result.IsMessageSet ? result.Message : "Registration Create failed with no message.");
//		}

//		[TestMethod]
//		public void RegistrationValidate_PassedValidValidationCode_SuccessfulResult()
//		{
//			//Arrange
//			var email = Guid.NewGuid().ToString().Substring(0, 6) + "@mail.com";
//			var password = Guid.NewGuid().ToString().Substring(0, 6);
//			var validationCode = Guid.NewGuid().ToString().Substring(0, 6);
//			Models.Users.Registration entity;
//			{
//				var registration = new Models.Users.Registration
//				{
//					EmailAddress = email,
//					Password = password,
//					ValidationCode = validationCode,
//					AcceptTerms = true,
//					ReceiveNewsletters = true,
//					Person = new Models.ComplexTypes.Person
//					{
//						FirstName = "First Name",
//					},
//				};
//				new Business.Users.Registration().CreateEntity(registration);
//				using (var dataManager = new Data.DataManager())
//				{
//					var result1 = dataManager.GetEntity<Models.Users.Registration>(registration.Id);
//					entity = result1.Entity;
//				}
//			}
//			//Act
//			var result = new Business.Users.Registration().Validate(entity.ValidationCode, entity.EmailAddress, entity.Password);
//			//Assert
//			Assert.IsTrue(result.IsSuccessful, result.IsMessageSet ? result.Message : "Registration Create failed with no message.");
//		}

//	}
//}
