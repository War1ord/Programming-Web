using BudgetManager.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using BudgetManager.Models.Base;

namespace BudgetManager.Data.Extensions
{
	public static class DataContextExtension
	{
		#region Save, Add and Update

		/// <summary>
		/// Saves the specified data context.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="dataContext">The data context.</param>
		/// <param name="entity">The entity.</param>
		public static void Save<T>(this DataContext dataContext, T entity) where T : IdModelBase
		{
			dataContext.Entry<T>(entity).State =
				(dataContext.Set<T>().Count(item => item.Id == entity.Id) == 0)
					? System.Data.Entity.EntityState.Added
					: System.Data.Entity.EntityState.Modified;
		}

        /// <summary>
        /// Saves the specified data context.
        /// NOTE: This will slow down the adding and updating
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataContext">The data context.</param>
        /// <param name="entity">The entity.</param>
        public static void AttachChanges<T>(this DataContext dataContext, T entity, Guid? associationCode = null) where T : IdModelBase
        {
            var dbEntity = dataContext.Set<T>().FirstOrDefault(i => i.Id.Equals(entity.Id));

            if (dbEntity == null)
            {
                dataContext.Set<T>().Add(entity);
            }
            else
            {
                dataContext.Entry<T>(dbEntity).CurrentValues.SetValues(entity);
            }
        }
        
        /// <summary>
		/// Saves the entity to the data context.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="dbSet">The database set.</param>
		/// <param name="db">The database.</param>
		/// <param name="entity">The entity.</param>
		/// <returns></returns>
		public static bool SaveEntity<T>(this DbSet<T> dbSet, DataContext db, T entity) where T : IdModelBase
		{
			db.Save(dbSet.Attach(entity));
			return db.SaveChanges() > 0;
		}

		/// <summary>
		/// Adds the entity to the data context.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="dbSet">The database set.</param>
		/// <param name="db">The database.</param>
		/// <param name="entity">The entity.</param>
		/// <returns></returns>
		public static bool AddEntity<T>(this DbSet<T> dbSet, DataContext db, T entity) where T : IdModelBase
		{
			db.Entry(dbSet.Attach(entity)).State = System.Data.Entity.EntityState.Added;
			return db.SaveChanges() > 0;
		}

		/// <summary>
		/// Updates the entity to the data context.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="dbSet">The database set.</param>
		/// <param name="db">The database.</param>
		/// <param name="entity">The entity.</param>
		/// <returns></returns>
		public static bool UpdateEntity<T>(this DbSet<T> dbSet, DataContext db, T entity) where T : IdModelBase
		{
			db.Entry(dbSet.Attach(entity)).State = System.Data.Entity.EntityState.Modified;
			return db.SaveChanges() > 0;
		}

		#endregion

        //#region Include

        ///// <summary>
        ///// Helper for the Include function of entity framework to include the full path and the first property.
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <typeparam name="TProperty">The type of the property.</typeparam>
        ///// <param name="source">The source.</param>
        ///// <param name="path">The path.</param>
        ///// <returns></returns>
        //public static IQueryable<T> IncludeFullPath<T, TProperty>(this IQueryable<T> source,
        //                                                          Expression<Func<T, TProperty>> path) where T : class
        //{
        //    string fullString = path.ToString();
        //    int index = fullString.IndexOf(".", StringComparison.Ordinal);
        //    var pathString = fullString.Substring(index >= 0 ? index + 1 : 0);
        //    return source.Include(pathString);
        //}

        //public static IQueryable<T> IncludeFullPaths<T, TProperty>(this IQueryable<T> source,
        //                                                           params Expression<Func<T, TProperty>>[] paths)
        //    where T : class
        //{
        //    foreach (var path in paths)
        //    {
        //        string fullString = path.ToString();
        //        int index = fullString.IndexOf(".", StringComparison.Ordinal);
        //        var pathString = fullString.Substring(index >= 0 ? index + 1 : 0);
        //        source = source.Include(pathString);
        //    }
        //    return source;
        //}

        ////public static IQueryable<T> IncludeFullPaths<T, TProperty>(this IQueryable<T> source, params Expression<Func<T>>[] paths) where T : class
        ////{
        ////	foreach (var path in paths)
        ////	{
        ////		string fullString = path.ToString();
        ////		int index = fullString.IndexOf(".", StringComparison.Ordinal);
        ////		var pathString = fullString.Substring(index >= 0 ? index + 1 : 0);
        ////		source = source.Include(pathString);
        ////	}
        ////	return source;
        ////}

        //public static IQueryable<T> IncludePaths<T>(this IQueryable<T> source, params string[] paths) where T : class
        //{
        //    foreach (var path in paths)
        //        source = source.Include(path);
        //    return source;
        //}

        //public static IQueryable IncludePaths(this IQueryable source, params string[] paths)
        //{
        //    foreach (var path in paths)
        //        source = source.Include(path);
        //    return source;
        //}

        ///// <summary>
        ///// Includes the specified queryable.
        ///// </summary>
        ///// <param name="queryable">The queryable.</param>
        ///// <param name="includes">The includes.</param>
        ///// <returns></returns>
        //public static IQueryable Include(this IQueryable queryable, params string[] includes)
        //{
        //    var includemethod = queryable.GetType().GetMethod("Include", new Type[] {typeof (string)});
        //    if (includemethod != null)
        //        foreach (var include in includes)
        //            queryable = includemethod.Invoke(queryable, new object[] {include}) as IQueryable;
        //    return queryable;
        //}

        ///// <summary>
        ///// Includes the specified queryable.
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="queryable">The queryable.</param>
        ///// <param name="includes">The includes.</param>
        ///// <returns></returns>
        //public static IQueryable<T> Include<T>(this IQueryable<T> queryable, params string[] includes)
        //{
        //    var includemethod = queryable.GetType().GetMethod("Include", new Type[] {typeof (string)});
        //    if (includemethod != null)
        //        foreach (var include in includes)
        //            queryable = includemethod.Invoke(queryable, new object[] {include}) as IQueryable<T>;
        //    return queryable;
        //}

        //#endregion
	}
}