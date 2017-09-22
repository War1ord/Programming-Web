using BudgetManager.Business.Error;
using BudgetManager.Business.Helpers;
using BudgetManager.Common.Messages;
using BudgetManager.Data;
using BudgetManager.Data.Extensions;
using BudgetManager.Models.Base;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace BudgetManager.Business
{
    public static class DataManagement
    {
        public static void InitializeDatabase()
        {
            DataContext.Initialize();
        }

        /// <summary>
        /// SQL bulk copy to database.
        /// </summary>
        /// <param name="dataTable">The data table. NOTE: data table must have correct table name and column name.</param>
        /// <param name="connectionString">The connection string.</param>
        /// <returns></returns>
        public static bool SqlBulkCopyToDatabase(DataTable dataTable, string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                SqlTransaction transaction = null;
                connection.Open();
                try
                {
                    transaction = connection.BeginTransaction();
                    using (var sqlBulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.TableLock, transaction))
                    {
                        sqlBulkCopy.DestinationTableName = dataTable.TableName;
                        dataTable.Columns.Cast<DataColumn>()
                            .ForEach(c => sqlBulkCopy.ColumnMappings
                                              .Add(c.ColumnName, c.ColumnName));
                        sqlBulkCopy.WriteToServer(dataTable);
                    }
                    transaction.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    ErrorManager.LogException(e);
                    return false;
                }
            }
        }
    }

    public class DataManagement<T> where T : IdModelBase
    {
        /// <summary>
        /// Saves the object WITHOUT audit logging.
        /// </summary>
        /// <param name="entity">The object.</param>
        /// <returns>the error object</returns>
        public Result Save(T entity)
        {
            try
            {
                using (var db = new Data.DataContext())
                {
                    //validate the object before doing anything
                    var result = db.Entry(entity).GetValidationResult();
                    if (result.IsValid)
                    {
                        db.Save(entity);
                        db.SaveChanges();
                        return new Result();
                    }
                    else
                    {
                        entity.Result.Message = ValidationHelpers.GetValidationErrorMessage("{0}", result.ValidationErrors);
                        return new Result() { Message = entity.Result.Message };
                    }
                }
            }
            catch (Exception e)
            {
                return new Result() { Message = ErrorManager.LogException(e), Type = ResultType.Error, };
            }
        }
        /// <summary>
        /// Saves the object WITH audit logging.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="userid">The userid.</param>
        /// <returns>the error object</returns>
        public Result Save(T obj, Guid userid)
        {
            try
            {
                using (var db = new Data.DataContext())
                {
                    //validate the object before doing anything
                    var result = db.Entry(obj).GetValidationResult();
                    if (result.IsValid)
                    {
                        db.Save(obj);
                        db.SaveChanges();
                        return new Result();
                    }
                    else
                    {
                        obj.Result.Message = ValidationHelpers.GetValidationErrorMessage("{0}", result.ValidationErrors);
                        return new Result() { Message = obj.Result.Message };
                    }
                }
            }
            catch (Exception e)
            {
                return new Result() { Message = ErrorManager.LogException(e), Type = ResultType.Error, };
            }
        }
        /// <summary>
        /// Saves the list WITHOUT audit logging.
        /// </summary>
        /// <param name="list">The list of entities.</param>
        /// <returns>the error object</returns>
        public Result Save(List<T> list)
        {
            if (list == null || !list.Any()) return new Result() { Message = "There was nothing to save.", Type = ResultType.Warning, };
            try
            {
                using (var db = new DataContext())
                {
                    //validate list first, before doing anything
                    var validationErrors = list.Select(entity => db.Entry(entity).GetValidationResult()).ToList();
                    if (validationErrors.All(i => i.IsValid))
                    {
                        foreach (var entity in list)
                        {
                            try
                            {
                                db.Save(entity);
                                db.SaveChanges();
                                return new Result() { Message = "The data have been saved.", Type = ResultType.Success, };
                            }
                            catch (Exception e)
                            {
                                entity.Result.Message = ErrorManager.LogException(e);
                            }
                        }
                    }
                    else
                    {
                        foreach (var entity in list)
                        {
                            var employee = entity;
                            var employeesValidationResults = validationErrors.FirstOrDefault(i => i.Entry.Entity.Equals(employee));
                            if (employeesValidationResults == null) return new Result();
                            entity.Result.Message = ValidationHelpers.GetValidationErrorMessage("{0}", employeesValidationResults.ValidationErrors);
                        }
                        return new Result() { Message = CommonMessages.UnexpectedError, Type = ResultType.Error, };
                    }
                }
                return new Result() { Message = CommonMessages.UnexpectedError, Type = ResultType.Error, };
            }
            catch (Exception e)
            {
                return new Result() { Message = ErrorManager.LogException(e), Type = ResultType.Error, };
            }
        }
        /// <summary>
        /// Saves the list WITH audit logging.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="userid">The userid.</param>
        /// <returns>the error object</returns>
        public Result Save(List<T> list, Guid userid)
        {
            try
            {
                using (var db = new DataContext())
                {
                    //validate list first, before doing anything
                    var validationErrors = list.Select(entity => db.Entry(entity).GetValidationResult()).ToList();
                    if (validationErrors.All(i => i.IsValid))
                    {
                        foreach (var entity in list)
                        {
                            try
                            {
                                db.Save(entity);
                                db.SaveChanges();
                            }
                            catch (Exception e)
                            {
                                entity.Result.Message = ErrorManager.LogException(e);
                            }
                        }
                        return new Result();
                    }
                    else
                    {
                        foreach (var entity in list)
                        {
                            var employee = entity;
                            var employeesValidationResults = validationErrors.FirstOrDefault(i => i.Entry.Entity.Equals(employee));
                            if (employeesValidationResults == null) return new Result();
                            entity.Result.Message = ValidationHelpers.GetValidationErrorMessage("{0}", employeesValidationResults.ValidationErrors);
                        }
                        return new Result() { Message = CommonMessages.UnexpectedError, Type = ResultType.Error, };
                    }
                }
            }
            catch (Exception e)
            {
                return new Result() { Message = ErrorManager.LogException(e), Type = ResultType.Error, };
            }
        }
        /// <summary>
        /// Return the object
        /// </summary>
        /// <param name="ID">The identifier.</param>
        /// <returns></returns>
        public T Get(Guid ID)
        {
            try
            {
                using (var db = new DataContext())
                {
                    return db.Set<T>().FirstOrDefault(i => i.Id.Equals(ID));
                }
            }
            catch (Exception e)
            {
                return (T)Activator.CreateInstance(typeof(T), new object[] { ID, new Result() { Message = ErrorManager.LogException(e), Type = ResultType.Error, } });
            }
        }
        /// <summary>
        /// Returns the list
        /// </summary>
        /// <param name="includeDeleted">if set to <c>true</c> include deleted.</param>
        /// <returns></returns>
        public List<T> Get(bool includeDeleted = false)
        {
            try
            {
                using (var db = new DataContext())
                {
                    return includeDeleted
                        ? db.Set<T>().ToList()
                        : db.Set<T>().Where(i => !i.IsDeleted).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<T> { (T)Activator.CreateInstance(typeof(T), new object[] { new Result() { Message = ErrorManager.LogException(e), Type = ResultType.Error, } }) };
            }
        }
        /// <summary>
        /// Mark object as deleted.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="userid"> </param>
        public Result SoftDelete(T obj, Guid userid)
        {
            //NOTE: Cleon - Charles K - Because of Audit data would not save.
            //db.Set<T>().Update(i => i.ID == obj.ID, i => (T)Activator.CreateInstance(typeof(T), new object[] { true }));
            try
            {
                using (var db = new DataContext())
                {
                    obj.IsDeleted = true;

                    //validate the object before doing anything
                    var result = db.Entry(obj).GetValidationResult();
                    if (result.IsValid)
                    {
                        db.Save(obj);
                        db.SaveChanges();
                        return new Result();
                    }
                    else
                    {
                        obj.Result.Message = ValidationHelpers.GetValidationErrorMessage("{0}", result.ValidationErrors);
                        return new Result() { Message = obj.Result.Message, Type = ResultType.Error, };
                    }
                }
            }
            catch (Exception e)
            {
                return new Result() { Message = ErrorManager.LogException(e), Type = ResultType.Error, };
            }
        }
    }

}