using System;
using System.Linq;
using RequestForService.Business.Models;
using RequestForService.DataTypes.Enums;

namespace RequestForService.Business.Services.Users
{
	public partial class AccountService
	{
		public Result UpdateUserPersonTitle(Guid userid, DataTypes.Enums.Title? title)
		{
			try
			{
				Db.Entry(
					Db.Set<RequestForService.Models.Users.User>()
						.FirstOrDefault(i => i.Id == userid)
					)
					.Property(i => i.Person.Title)
					.CurrentValue = title;
				Db.SaveChanges();
				return Results.SuccessResult();
			}
			catch (Exception e)
			{
				LogException(e);
				return Results.ErrorResult();
			}
		}
		public Result UpdateUserPersonGender(Guid userid, DataTypes.Enums.Gender? gender)
		{
			try
			{
				Db.Entry(
					Db.Set<RequestForService.Models.Users.User>()
						.FirstOrDefault(i => i.Id == userid)
					)
					.Property(i => i.Person.Gender)
					.CurrentValue = gender;
				Db.SaveChanges();
				return Results.SuccessResult();
			}
			catch (Exception e)
			{
				LogException(e);
				return Results.ErrorResult();
			}
		}
		public Result UpdateUserJobDetailsJobTitle(Guid userid, JobTitle? jobTitle)
		{
			try
			{
				Db.Entry(
					Db.Set<RequestForService.Models.Users.User>()
						.FirstOrDefault(i => i.Id == userid)
					)
					.Property(i => i.JobDetails.JobTitle)
					.CurrentValue = jobTitle;
				Db.SaveChanges();
				return Results.SuccessResult();
			}
			catch (Exception e)
			{
				LogException(e);
				return Results.ErrorResult();
			}
		}
		public Result UpdateUserReceiveNewsletters(Guid userid, bool receiveNewsletters)
		{
			try
			{
				Db.Entry(
					Db.Set<RequestForService.Models.Users.User>()
						.FirstOrDefault(i => i.Id == userid)
					)
					.Property(i => i.ReceiveNewsletters)
					.CurrentValue = receiveNewsletters;
				Db.SaveChanges();
				return Results.SuccessResult();
			}
			catch (Exception e)
			{
				LogException(e);
				return Results.ErrorResult();
			}
		}
		public Result UpdateUserPersonFirstName(Guid userid, string firstName)
		{
			try
			{
				Db.Entry(
					Db.Set<RequestForService.Models.Users.User>()
						.FirstOrDefault(i => i.Id == userid)
					)
					.Property(i => i.Person.FirstName)
					.CurrentValue = firstName;
				Db.SaveChanges();
				return Results.SuccessResult();
			}
			catch (Exception e)
			{
				LogException(e);
				return Results.ErrorResult();
			}
		}
		public Result UpdateUserPersonMiddleName(Guid userid, string middleName)
		{
			try
			{
				Db.Entry(
					Db.Set<RequestForService.Models.Users.User>()
						.FirstOrDefault(i => i.Id == userid)
					)
					.Property(i => i.Person.MiddleName)
					.CurrentValue = middleName;
				Db.SaveChanges();
				return Results.SuccessResult();
			}
			catch (Exception e)
			{
				LogException(e);
				return Results.ErrorResult();
			}
		}
		public Result UpdateUserPersonLastName(Guid userid, string lastName)
		{
			try
			{
				Db.Entry(
					Db.Set<RequestForService.Models.Users.User>()
						.FirstOrDefault(i => i.Id == userid)
					)
					.Property(i => i.Person.LastName)
					.CurrentValue = lastName;
				Db.SaveChanges();
				return Results.SuccessResult();
			}
			catch (Exception e)
			{
				LogException(e);
				return Results.ErrorResult();
			}
		}
		public Result UpdateUserContactDetailsOffice(Guid userid, string contactDetailsOffice)
		{
			try
			{
				Db.Entry(
					Db.Set<RequestForService.Models.Users.User>()
						.FirstOrDefault(i => i.Id == userid)
					)
					.Property(i => i.ContactDetails.Office)
					.CurrentValue = contactDetailsOffice;
				Db.SaveChanges();
				return Results.SuccessResult();
			}
			catch (Exception e)
			{
				LogException(e);
				return Results.ErrorResult();
			}
		}
		public Result UpdateUserContactDetailsMobile(Guid userid, string contactDetailsMobile)
		{
			try
			{
				Db.Entry(
					Db.Set<RequestForService.Models.Users.User>()
						.FirstOrDefault(i => i.Id == userid)
					)
					.Property(i => i.ContactDetails.Mobile)
					.CurrentValue = contactDetailsMobile;
				Db.SaveChanges();
				return Results.SuccessResult();
			}
			catch (Exception e)
			{
				LogException(e);
				return Results.ErrorResult();
			}
		}
	}
}