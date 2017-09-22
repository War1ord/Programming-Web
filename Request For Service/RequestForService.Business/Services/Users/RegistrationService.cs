using System;
using System.Linq;
using RequestForService.Business.Models;
using RequestForService.Data.Extentions;
using RequestForService.Models.Users;
using RequestForService.Security.Passwords;
using RequestForService.Data;

namespace RequestForService.Business.Services.Users
{
	public class RegistrationService : Base.DataManagerBase
	{
		public RegistrationService(Guid? userId) : base(userId){}
		public RegistrationService(DataContext db, Guid? userId) : base(db, userId){}

		public Result Create(Registration newRegistration)
		{
			try
			{
				if (newRegistration != null && newRegistration.AcceptTerms && !string.IsNullOrWhiteSpace(newRegistration.Password))
				{
					newRegistration.ValidationCode = Guid.NewGuid().ToString().Substring(0, 6);
					var registration = new Registration
					{
						EmailAddress = newRegistration.EmailAddress,
						Password = newRegistration.Password,
						AcceptTerms = newRegistration.AcceptTerms,
						ReceiveNewsletters = newRegistration.ReceiveNewsletters,
						ValidationCode = newRegistration.ValidationCode,
						Person = newRegistration.Person,
						ContactDetails = newRegistration.ContactDetails,
						JobDetails = newRegistration.JobDetails,
					};
					var validationResult = GetValidationResult(registration);
					if (validationResult.IsValid)
					{
						Db.Set<Registration>().Add(registration);
						Db.SaveChanges();
						return Results.SuccessResult("Saved"); 
					}
					else
					{
						return Results.ErrorResult(validationResult.ValidationErrors.ToHtmlValidMultiLineString());
					}
				}
				else
				{
					if (newRegistration != null && newRegistration.AcceptTerms)
					{
						return Results.ErrorResult("Invalid email or password.");
					}
					else
					{
						return Results.ErrorResult("You need to accept the terms before you may register.");
					}
				}
			}
			catch (Exception e)
			{
				LogException(e);
				return Results.ErrorResult();
			}
		}

		public Result Validate(string code, string email, string password)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
				{
					return Results.Result(ResultType.CriticalError, "Invalid");
				}
				code = code.Trim();
				email = email.Trim().ToLower();
				password = password.Trim();
				var passwordHash = password.ToPasswordHash();
				// check if validation code is valid
				var registration =
					Db.Set<Registration>().FirstOrDefault(
						r => r.ValidationCode.Trim() == code
							&& r.EmailAddress.Trim().ToLower() == email 
							&& r.PasswordHash == passwordHash);
				if (registration != null)
				{
					var oldValidationCode = registration.ValidationCode;
					// wipe validation code
					registration.ValidationCode = null;
					Db.Entry(Db.Set<Registration>().Attach(registration)).State = System.Data.Entity.EntityState.Modified;
					Db.SaveChanges();
					// set new user with registration 
					var user = new User
					{
						EmailAddress = registration.EmailAddress,
						PasswordHash = registration.PasswordHash,
						ContactDetails = registration.ContactDetails,
						JobDetails = registration.JobDetails,
						Person = registration.Person,
						ReceiveNewsletters = registration.ReceiveNewsletters,
					};
					// create new user
					Db.Set<User>().Add(user);
					Db.SaveChanges();
					return Results.SuccessResult();
				}
				else
				{
					return Results.ErrorResult();
				}
			}
			catch (Exception e)
			{
				LogException(e);
				return Results.ErrorResult();
			}
		}

	}
}