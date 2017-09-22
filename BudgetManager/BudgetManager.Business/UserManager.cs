using BudgetManager.Extentions;
using BudgetManager.Models.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace BudgetManager.Business
{
	/// <summary>
	/// The User Manager Business Class, containing all the functionality for User(s).
	/// </summary>
	public class UserManager : Base.BusinessBase
	{
		/// <summary>
		/// The user
		/// </summary>
		private User _user;

		/// <summary>
		/// Initializes a new instance of the <see cref="UserManager"/> class, with out setting the user.
		/// </summary>
		public UserManager()
		{
		}
        /// <summary>
        /// Initializes a new instance of the <see cref="UserManager" /> class.
        /// </summary>
        /// <param name="email">The email.</param>
		public UserManager(string email)
		{
			NewUser(email);
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="UserManager"/> class.
		/// </summary>
		/// <param name="user">The user.</param>
		public UserManager(User user)
		{
			_user = user;
		}

		/// <summary>
		/// Determines whether user name is valid.
		/// </summary>
		/// <returns>
		///   <c>true</c> if the user name is valid; otherwise, <c>false</c>.
		/// </returns>
		public bool IsEmailValid()
		{
			return _user != null && !string.IsNullOrWhiteSpace(_user.Email);
		}
		/// <summary>
		/// Determines whether user is valid.
		/// </summary>
		/// <returns>
		///   <c>true</c> if the user is valid; otherwise, <c>false</c>.
		/// </returns>
		public bool IsUserValid()
		{
			return IsEmailValid() && _user.Id != Guid.Empty;
		}
		/// <summary>
		/// Determines whether the specified user is authenticated.
		/// </summary>
		/// <returns>
		///   <c>true</c> if the specified user is authenticated; otherwise, <c>false</c>.
		/// </returns>
		public bool IsUserAuthenticated()
		{
			if (!IsUserValid())
			{
				return false;
			}
			return _user.IsAuthenticated;
		}
        /// <summary>
        /// Determines whether the specified user is validated.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public bool IsUserValidated()
        {
            if (!IsUserValid())
            {
                return false;
            }
            return _user.IsValidated;
        }
        /// <summary>
		/// Determines whether the password is valid.
		/// </summary>
		/// <param name="password">The password.</param>
		/// <returns>
		///   <c>true</c> if the password valid; otherwise, <c>false</c>.
		/// </returns>
		public bool IsUserAndPasswordValid(string password)
		{
			return IsUserValid() && _user.PasswordHash == password.ToPasswordHash();
		}
        /// <summary>
        /// Determines whether the one time pin is valid.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public bool IsValidOneTimePin(string oneTimePin)
        {
            return IsUserValid() && _user.OneTimePin == oneTimePin;
        }

		/// <summary>
		/// Loads the user from 
		/// </summary>
		/// <returns></returns>
		public bool Load()
		{
			if (!IsEmailValid())
			{
				return false;
			}
			_user = Db.Users.Include(u => u.BankAccounts).FirstOrDefault(u => u.Email == _user.Email);
			return true;
		}
		/// <summary>
		/// Loads the user from 
		/// </summary>
		/// <param name="email">The email.</param>
		/// <returns></returns>
		public bool Load(string email)
		{
            _user = Db.Users.FirstOrDefault(u => u.Email == email);
			if (!IsUserValid())
			{
				return false;
			}
			return true;
		}

		/// <summary>
		/// Gets the user.
		/// </summary>
		/// <returns></returns>
		public User GetUser()
		{
			return _user;
		}
		/// <summary>
		/// Gets a list of all users.
		/// </summary>
		/// <returns></returns>
		public List<User> GetUsers()
		{
			return Db.Users
				.OrderBy(user => user.Person.FirstName)
				.ThenBy(user => user.Person.Surname)
				.ToList();
		}
		/// <summary>
		/// Gets the user from Db per Id.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <returns></returns>
		public User GetUser(Guid id)
		{
			return Db.Users.FirstOrDefault(user => user.Id == id);
		}
		/// <summary>
		/// Gets the user.
		/// </summary>
		/// <param name="email">The email.</param>
		/// <returns></returns>
		public User GetUser(string email)
		{
			return Db.Users.FirstOrDefault(user => user.Email == email);
		}

		/// <summary>
		/// Sets the authenticated.
		/// </summary>
		/// <param name="isAuthenticated">if set to <c>true</c> [is authenticated].</param>
		public bool SetAuthenticated(bool isAuthenticated)
		{
			if (!IsUserValid())
			{
				return false;
			}
			_user.IsAuthenticated = isAuthenticated;
			return true;
		}

		/// <summary>
		/// Creates a new instance of the user
		/// </summary>
		/// <param name="email">The email.</param>
		public UserManager NewUser(string email)
		{
            _user = new User { Email = email };
			return this;
		}
		/// <summary>
		/// Sets the user.
		/// </summary>
		/// <param name="user">The user.</param>
		public UserManager SetUser(User user)
		{
			_user = user;
			return this;
		}

        /// <summary>
		/// Updates the user to the 
		/// </summary>
		/// <returns></returns>
		public bool Update()
		{
			if (!IsUserValid())
			{
				return false;
			}
			try
			{
                _user.OneTimePin = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
                Db.Entry(Db.Users.Attach(_user)).State = System.Data.Entity.EntityState.Modified;
				Db.SaveChanges();
				return true;
			}
			catch (Exception e)
			{
				return LogException(e);
			}
		}
		/// <summary>
		/// Adds the user to the 
		/// </summary>
		/// <returns></returns>
		public bool Add()
		{
			if (!IsEmailValid())
			{
				return false;
			}
			try
			{
                _user.OneTimePin = Guid.NewGuid().ToString().Replace("-", "").Substring(0,6);
				Db.Entry(Db.Users.Attach(_user)).State = System.Data.Entity.EntityState.Added;
				Db.SaveChanges();
				return true;
			}
			catch (Exception e)
			{
				return LogException(e);
			}
		}
        public bool Delete(User user)
		{
			if (!IsEmailValid())
			{
				return false;
			}
			try
			{
				Db.Entry(Db.Users.Attach(_user)).State = System.Data.Entity.EntityState.Deleted;
				Db.SaveChanges();
				return true;
			}
			catch (Exception e)
			{
				return LogException(e);
			}
		}
        public bool ClearOneTimePin()
        {
            if (!IsEmailValid())
            {
                return false;
            }
            try
            {
                _user.OneTimePin = null;
                Db.Entry(Db.Users.Attach(_user)).State = System.Data.Entity.EntityState.Modified;
                Db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return LogException(e);
            }
        }
        public bool UpdateToValidated()
        {
            if (!IsEmailValid())
            {
                return false;
            }
            try
            {
                _user.IsValidated = true;
                Db.Entry(Db.Users.Attach(_user)).State = System.Data.Entity.EntityState.Modified;
                Db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return LogException(e);
            }
        }
        public bool UpdateToAuthorized()
        {
            if (!IsEmailValid())
            {
                return false;
            }
            try
            {
                _user.IsAuthenticated = true;
                Db.Entry(Db.Users.Attach(_user)).State = System.Data.Entity.EntityState.Modified;
                Db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return LogException(e);
            }
        }
    }
}