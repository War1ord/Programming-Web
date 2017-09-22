using System;
using System.Collections.Generic;
using System.Linq;
using RequestForService.Business.Models;
using RequestForService.Models.Users;
using RequestForService.Security.Passwords;
using RequestForService.Data;

namespace RequestForService.Business.Services.Users
{
	public partial class AccountService : Base.DataManagerBase
	{
		public AccountService(Guid? userId) : base(userId){}
		public AccountService(DataContext db, Guid? userId) : base(db, userId){}

		public Result<User> Authenticate(string email, string password, bool isDeleted = false)
		{
			try
			{
				var emailToCompare = email.Trim().ToLower();
				var passwordhash = password.ToPasswordHash();
				var user = Db.Set<User>()
					.FirstOrDefault(u => u.EmailAddress.Trim().ToLower() == emailToCompare
										&& u.PasswordHash == passwordhash
										&& u.IsDeleted == isDeleted);
				if (user != null)
				{
					return Results.SuccessResult(user);
				}
				else
				{
					return Results.ErrorResult<User>("Invalid email or password.", null);
				}
			}
			catch (Exception e)
			{
				LogException(e);
				return Results.ErrorResult<User>();
			}
		}
		public Result<User> GetUser(Guid userId)
		{
			try
			{
				return GetEntity<User>(userId);
			}
			catch (Exception e)
			{
				LogException(e);
				return Results.ErrorResult<User>();
			}
		}
		public Result<List<User>> GetUsers()
		{
			try
			{
				return GetEntityList<User>();
			}
			catch (Exception e)
			{
				LogException(e);
				return Results.ErrorResult<List<User>>();
			}
		}
	}
}