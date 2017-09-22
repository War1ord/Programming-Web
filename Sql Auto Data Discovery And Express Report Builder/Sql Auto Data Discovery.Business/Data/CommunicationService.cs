using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using Sql_Auto_Data_Discovery.Business.Extentions;
using Sql_Auto_Data_Discovery.Business.Models.Commom;

namespace Sql_Auto_Data_Discovery.Business.Data
{
	internal class CommunicationService : IDisposable
	{
        #region fields
	    private const int CommandTimeout = 600;
	    private const string LogFileName = @"C:\SqlError.txt";
	    private SqlConnection _connection; 
        #endregion

		public CommunicationService(string connectionString)
		{
		    _connection = new SqlConnection(connectionString);
		}

	    public string ConnectionString
	    {
	        get { return _connection.ConnectionString; }
	    }

        public List<T> Get<T>(string sql, params SqlParameter[] parameters)
	    {
            OpenConnection();
            var type = typeof(T);
            var results = new List<T>();
            try
            {
                using (var command = new SqlCommand(sql, _connection))
                {
                    Add(parameters, command);

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var list = Enumerable.Range(0, reader.FieldCount)
                            .Select(i => new KeyValuePair<string, object>(reader.GetName(i), reader[i]))
                            .ToArray();
                        var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                            .Select(i => i.Name)
                            .ToArray();
                        var instance = (T)Activator.CreateInstance(type);
                        if (properties.Length > 0)
                        {
                            foreach (var property in properties)
                            {
                                var pair = list.FirstOrDefault(w => w.Key.Equals(property, StringComparison.InvariantCultureIgnoreCase));
                                type.GetProperty(property).SetValue(instance, pair.Value, null);
                            }
                        }
                        results.Add(instance);
                    }
                }
            }
            catch (Exception e)
            {
                Log(e, sql);
            }
            return results;
        }
	    public DataSet GetDataSet(string sql, params SqlParameter[] parameters)
        {
            OpenConnection();
            var result = new DataSet();
            try
            {
                Get(sql, parameters).Fill(result);
            }
            catch (Exception e)
            {
                Log(e, sql);
            }
            return result;
        }
	    public DataTable GetDataTable(string sql, params SqlParameter[] parameters)
        {
            OpenConnection();
            var result = new DataTable();
            try
            {
                Get(sql, parameters).Fill(result);
            }
            catch (Exception e)
            {
                Log(e, sql);
            }
            return result;
        }
        public string GetField(string sql, params SqlParameter[] parameters)
        {
            OpenConnection();
            string result = null;
            try
            {
                result = GetCommand(sql, CommandType.Text, parameters)
                        .ExecuteScalar()
                        .ToString();
            }
            catch (Exception e)
            {
                Log(e, sql);
            }
            return result;
        }
        public Result Update(string sql, params SqlParameter[] parameters)
        {
            OpenConnection();
            try
            {
                var rowsAffected = GetCommand(sql, CommandType.Text, parameters)
                    .ExecuteNonQuery();
                return rowsAffected > 0
                    ? Results.SuccessResult()
                    : Results.InvalidResult();
            }
            catch (Exception e)
            {
                Log(e, sql);
                return Results.ErrorResult();
            }
        }
        public Result Insert(string sql, params SqlParameter[] parameters)
        {
            OpenConnection();
            try
            {
                var rowsAffected = GetCommand(sql, CommandType.Text, parameters)
                    .ExecuteNonQuery();
                return rowsAffected > 0
                    ? Results.SuccessResult()
                    : Results.InvalidResult();
            }
            catch (Exception e)
            {
                Log(e, sql);
                return Results.ErrorResult();
            }
        }
        public Result<string> Insert_ReturnField(string sql, params SqlParameter[] parameters)
        {
            OpenConnection();
            try
            {
                var field = GetCommand(sql, parameters)
                    .ExecuteScalar()
                    .ToString();
                return field.IsSet()
                    ? Results.SuccessResult(entity: field)
                    : Results.InvalidResult(entity: null as string);
            }
            catch (Exception e)
            {
                Log(e, sql);
                return Results.ErrorResult(entity: null as string);
            }
        }
        public Result<int?> Insert_ReturnFieldInt(string sql, params SqlParameter[] parameters)
        {
            OpenConnection();
            try
            {
                var field = GetCommand(sql, parameters)
                    .ExecuteScalar()
                    .ToString();
                return field.IsSet()
                    ? Results.SuccessResult(entity: field.AsInt() as int?)
                    : Results.InvalidResult(entity: null as int?);
            }
            catch (Exception e)
            {
                Log(e, sql);
                return Results.ErrorResult(entity: null as int?);
            }
        }
        public Result ExecuteProcedure(string sql, params SqlParameter[] parameters)
        {
            OpenConnection();
            try
            {
                var result = GetCommand(sql, CommandType.Text, parameters)
                    .ExecuteScalar()
                    .ToString();
                return result.IsSet()
                    ? Results.SuccessResult(result)
                    : Results.InvalidResult();
            }
            catch (Exception e)
            {
                Log(e, sql);
                return Results.ErrorResult();
            }
        }

        public List<T> Get<T>(string sql, params KeyValue[] parameters)
        {
            return Get<T>(sql, parameters.ToSqlParameters());
        }
        public DataSet GetDataSet(string sql, params KeyValue[] parameters)
        {
            return GetDataSet(sql, parameters.ToSqlParameters());
        }
        public DataTable GetDataTable(string sql, params KeyValue[] parameters)
        {
            return GetDataTable(sql, parameters.ToSqlParameters());
        }
        public string GetField(string sql, params KeyValue[] parameters)
        {
            return GetField(sql, parameters.ToSqlParameters());
        }
        public Result Update(string sql, params KeyValue[] parameters)
        {
            return Update(sql, parameters.ToSqlParameters());
        }
        public Result Insert(string sql, params KeyValue[] parameters)
        {
            return Insert(sql, parameters.ToSqlParameters());
        }
        public Result<string> Insert_ReturnField(string sql, params KeyValue[] parameters)
        {
            return Insert_ReturnField(sql, parameters.ToSqlParameters());
        }
        public Result<int?> Insert_ReturnFieldInt(string sql, params KeyValue[] parameters)
        {
            return Insert_ReturnFieldInt(sql, parameters.ToSqlParameters());
        }
        public Result ExecuteProcedure(string sql, params KeyValue[] parameters)
        {
            return ExecuteProcedure(sql, parameters.ToSqlParameters());
        }

        public List<T> Get<T>(string sql)
        {
            return Get<T>(sql, null as SqlParameter);
        }
        public DataSet GetDataSet(string sql)
        {
            return GetDataSet(sql, null as SqlParameter);
        }
        public DataTable GetDataTable(string sql)
        {
            return GetDataTable(sql, null as SqlParameter);
        }
        public string GetField(string sql)
        {
            return GetField(sql, null as SqlParameter);
        }
        public Result Update(string sql)
        {
            return Update(sql, null as SqlParameter);
        }
        public Result Insert(string sql)
        {
            return Insert(sql, null as SqlParameter);
        }
        public Result<string> Insert_ReturnField(string sql)
        {
            return Insert_ReturnField(sql, null as SqlParameter);
        }
        public Result<int?> Insert_ReturnFieldInt(string sql)
        {
            return Insert_ReturnFieldInt(sql, null as SqlParameter);
        }
        public Result ExecuteProcedure(string sql)
        {
            return ExecuteProcedure(sql, null as SqlParameter);
        }

        #region helpers
        private void OpenConnection()
        {
            if (_connection != null && _connection.State != ConnectionState.Open)
                _connection.Open();
        }
        private void Log(Exception exception, string sql)
        {
            try
            {
                using (var writer = new StreamWriter(LogFileName, true))
                {
                    writer.WriteLine(
                        #region log format
@"Scipt: {0}|
Message: {1}|
StackTrace: {2}|
Date: {3}|
-----------------------------------------------------------------------------|
"
                        #endregion
                            , sql
                            , exception.Message
                            , exception.StackTrace
                            , DateTime.Now);
                }
            }
            catch (Exception logException){}
        }
        private SqlDataAdapter Get(string sql, SqlParameter[] parameters)
        {
            return new SqlDataAdapter { SelectCommand = GetCommand(sql, parameters) };
        }
	    private SqlCommand GetCommand(string sql, SqlParameter[] parameters)
	    {
            return GetCommand(sql, CommandType.Text, parameters);
	    }
	    private SqlCommand GetCommand(string sql, CommandType commandType, SqlParameter[] parameters)
	    {
            var command = new SqlCommand(sql, _connection)
	        {
	            CommandTimeout = CommandTimeout,
                CommandType = commandType,
	        };
	        if (parameters != null)
	        {
	            Add(parameters, command);
	        }
	        return command;
	    }
	    private static void Add(SqlParameter[] parameters, SqlCommand command)
        {
            foreach (var parameter in parameters.Where(i => i.IsSet()))
            {
                if (parameter.ParameterName.StartsWith("@").Not())
                {
                    parameter.ParameterName = "@" + parameter.ParameterName;
                }
                command.Parameters.Add(parameter);
            }
        }
        #endregion

        #region IDisposable
        public void Dispose()
        {
            if (_connection.IsSet())
            {
                _connection.Close();
                _connection.Dispose();
            }
            _connection = null;
        } 
        #endregion

	}
}
