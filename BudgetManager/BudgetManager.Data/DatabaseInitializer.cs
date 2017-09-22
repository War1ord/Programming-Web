using System.Collections.Generic;
using System.Data.Entity;

namespace BudgetManager.Data
{
	/// <summary>
	/// Helper to initialize database
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class DatabaseInitializer<T> : IDatabaseInitializer<T> where T : DbContext
	{
		private readonly List<IDatabaseInitializer<T>> _initializers;

		/// <summary>
		/// Initializes a new instance of the <see cref="DatabaseInitializer{T}"/> class.
		/// </summary>
		/// <param name="databaseInitializers">The database initializers.</param>
		public DatabaseInitializer(params IDatabaseInitializer<T>[] databaseInitializers)
		{
			_initializers = new List<IDatabaseInitializer<T>>();
			_initializers.AddRange(databaseInitializers);
		}

		/// <summary>
		/// Initializes the database.
		/// </summary>
		/// <param name="context">The context.</param>
		public void InitializeDatabase(T context)
		{
			foreach (var databaseInitializer in _initializers)
			{
				databaseInitializer.InitializeDatabase(context);
			}
		}

		/// <summary>
		/// Adds a initializer.
		/// </summary>
		/// <param name="databaseInitializer">The database initializer.</param>
		public void Add(IDatabaseInitializer<T> databaseInitializer)
		{
			_initializers.Add(databaseInitializer);
		}

		/// <summary>
		/// Removes a initializer.
		/// </summary>
		/// <param name="databaseInitializer">The database initializer.</param>
		public void Remove(IDatabaseInitializer<T> databaseInitializer)
		{
			_initializers.Remove(databaseInitializer);
		}
	}
}