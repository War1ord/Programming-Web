using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Collections;
using System.Configuration.Provider;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.Configuration;

namespace BudgetManager.Enums
{
	public class Connections : ProviderBase
	{
		#region Declarations

		private static int commandTimeOut = -1;

		private readonly CommandType commandType;

		private readonly string connectionString = "";

		private readonly List<SqlParameter> parameters;

		private string sql = "";

		private SqlParameterCollection sqlParameterCollection;

		#endregion

		#region Constructors

		public Connections(string connectionString, string sqlStatement)
		{
			this.connectionString = connectionString;
			sql = sqlStatement;
			parameters = new List<SqlParameter>();
			commandType = CommandType.StoredProcedure;
		}

		public Connections(string connectionString, CommandType commandType, string sqlStatement)
		{
			this.connectionString = connectionString;
			sql = sqlStatement;
			parameters = new List<SqlParameter>();
			this.commandType = commandType;
		}

		#endregion

		#region Properties

		#region SqlStatement

		public string SqlStatement
		{
			get { return sql; }
			set { sql = value; }
		}

		#endregion

		public static int CommandTimeOut
		{
			get
			{
				if (commandTimeOut == -1)
				{
					commandTimeOut = Convert.ToInt32(WebConfigurationManager.AppSettings["CommandTimeOut"] ?? "40");
				}
				return commandTimeOut;
			}
		}

		#endregion

		#region Methods

		public static bool HasResult(DataSet dataSet)
		{
			bool result = (dataSet.Tables.Count > 0) && (dataSet.Tables[0].Rows.Count > 0);
			return result;
		}

		#region ConvertObjects

		/// <summary>
		///     Converts an object to a string
		///     if null, an empty string is returned
		/// </summary>
		/// <param name="dbValue">Value to be converted</param>
		/// <returns>String</returns>
		public string ToString(object dbValue)
		{
			string result = "";
			if ((dbValue != null) && (dbValue != DBNull.Value))
			{
				result = dbValue.ToString();
			}
			return result;
		}

		/// <summary>
		///     Converts an object to an integer value
		///     if null, -1 is returned
		/// </summary>
		/// <param name="dbValue">Value to be converted</param>
		/// <returns></returns>
		public int ToInt(object dbValue)
		{
			int result = -1;
			if ((dbValue != null) && (dbValue != DBNull.Value))
			{
				result = Convert.ToInt32(dbValue, CultureInfo.CurrentCulture);
			}
			return result;
		}

		/// <summary>
		///     Converts an object to a boolean value
		///     if null, false is returned
		/// </summary>
		/// <param name="dbValue">Value to be converted</param>
		/// <returns></returns>
		public bool ToBool(object dbValue)
		{
			bool result = false;
			if ((dbValue != null) && (dbValue != DBNull.Value))
			{
				result = Convert.ToBoolean(dbValue, CultureInfo.CurrentCulture);
			}
			return result;
		}

		/// <summary>
		///     Converts an object to a double value
		///     if null, -1 is returned
		/// </summary>
		/// <param name="dbValue">Value to be converted</param>
		/// <returns></returns>
		public double ToDouble(object dbValue)
		{
			double result = -1;
			if ((dbValue != null) && (dbValue != DBNull.Value))
			{
				result = Convert.ToDouble(dbValue, CultureInfo.InvariantCulture);
			}
			return result;
		}

		/// <summary>
		///     Converts an object to a DateTimeValue value
		///     if null is handelled
		/// </summary>
		/// <param name="dbValue">Value to be converted</param>
		/// <returns></returns>
		public DateTime ToDateTime(object dbValue)
		{
			DateTime result = DateTime.MinValue;
			if ((dbValue != null) && (dbValue != DBNull.Value))
			{
				result = Convert.ToDateTime(dbValue, CultureInfo.CurrentCulture);
			}
			return result;
		}

		/// <summary>
		///     Converts an object to a Guid value
		///     if not null
		/// </summary>
		/// <param name="dbValue">Value to be converted</param>
		/// <returns></returns>
		public Guid ToGuid(object dbValue)
		{
			var result = new Guid();
			if ((dbValue != null) && (dbValue != DBNull.Value))
			{
				result = new Guid(dbValue.ToString());
			}
			return result;
		}

		#endregion

		#region addParameter

		public void AddParameter(string parameterName, object parameterValue, SqlDbType sqlDbType)
		{
			var parameter = new SqlParameter(parameterName, sqlDbType) {Value = parameterValue};
			parameters.Add(parameter);
		}

		public void AddParameter(string parameterName, object parameterValue, SqlDbType sqlDbType, int size)
		{
			var parameter = new SqlParameter(parameterName, sqlDbType, size) {Value = parameterValue};
			parameters.Add(parameter);
		}

		public SqlParameter AddParameter(ParameterDirection direction, string parameterName, SqlDbType sqlDbType)
		{
			SqlParameter parameter = new SqlParameter(parameterName, sqlDbType);
			parameter.Direction = direction;
			parameters.Add(parameter);
			return parameter;
		}

		#endregion

		#region addOutputParameter

		public void AddOutputParameter(string parameterName, SqlDbType sqlDbType)
		{
			var parameter = new SqlParameter(parameterName, sqlDbType) {Direction = ParameterDirection.Output};
			parameters.Add(parameter);
		}

		public void AddOutputParameter(string parameterName, SqlDbType sqlDbType, int size)
		{
			var parameter = new SqlParameter(parameterName, sqlDbType, size) {Direction = ParameterDirection.Output};
			parameters.Add(parameter);
		}

		#endregion

		#region getParameter

		public SqlParameter GetParameter(string parameterName)
		{
			return sqlParameterCollection != null ? sqlParameterCollection[parameterName] : null;
		}

		#endregion

		#region getDataSet

		public DataSet GetDataSet()
		{
			var ds = new DataSet();
			using (var sqlconn = new SqlConnection(connectionString))
			{
				sqlconn.Open();
				SqlCommand cmd = sqlconn.CreateCommand();
				cmd.CommandType = commandType;
				cmd.CommandText = SqlStatement;
				if (parameters.Count > 0)
				{
					foreach (SqlParameter t in parameters)
					{
						cmd.Parameters.Add(t.ParameterName, t.SqlDbType, t.Size).
							Value = t.Value;
						cmd.Parameters[t.ParameterName].Direction = t.Direction;
					}
				}
				var da = new SqlDataAdapter(cmd);
				da.Fill(ds);
				sqlParameterCollection = cmd.Parameters;
				return ds;
			}
		}

		#endregion

		#region getDataTable

		public DataTable GetDataTable()
		{
			var dt = new DataTable();
			using (var sqlconn = new SqlConnection(connectionString))
			{
				sqlconn.Open();
				SqlCommand cmd = sqlconn.CreateCommand();
				cmd.CommandText = SqlStatement;
				cmd.CommandType = commandType;
				cmd.CommandTimeout = CommandTimeOut;
				if (parameters.Count > 0)
				{
					foreach (SqlParameter t in parameters)
					{
						cmd.Parameters.Add(t.ParameterName, t.SqlDbType, t.Size).
							Value = t.Value;
						cmd.Parameters[t.ParameterName].Direction = t.Direction;
					}
				}
				var da = new SqlDataAdapter(cmd);
				da.Fill(dt);
				sqlParameterCollection = cmd.Parameters;
				return dt;
			}
		}

		public SqlDataReader ExecuteDataReader()
		{
			SqlDataReader dataReader = null;
			using (var sqlconn = new SqlConnection(connectionString))
			{
				sqlconn.Open();
				SqlCommand cmd = sqlconn.CreateCommand();
				cmd.CommandText = SqlStatement;
				cmd.CommandType = commandType;
				cmd.CommandTimeout = CommandTimeOut;
				if (parameters.Count > 0)
				{
					foreach (SqlParameter t in parameters)
					{
						cmd.Parameters.Add(t.ParameterName, t.SqlDbType, t.Size).
							Value = t.Value;
						cmd.Parameters[t.ParameterName].Direction = t.Direction;
					}
				}
				dataReader = cmd.ExecuteReader();
				return dataReader;
			}
		}

		#endregion

		#region executeNonQuery

		public int ExecuteNonQuery()
		{
			int returnValue;
			using (var sqlconn = new SqlConnection(connectionString))
			{
				sqlconn.Open();
				SqlCommand cmd = sqlconn.CreateCommand();
				cmd.CommandText = SqlStatement;
				cmd.CommandType = commandType;
				cmd.CommandTimeout = CommandTimeOut;
				if (parameters.Count > 0)
				{
					String sPar = "";
					foreach (SqlParameter t in parameters)
					{
						if (sPar.IndexOf(string.Format(",{0},", t.ParameterName)) == -1)
						{
							cmd.Parameters.Add(t.ParameterName, t.SqlDbType, t.Size)
								.Value = t.Value;
							cmd.Parameters[t.ParameterName].Direction = t.Direction;
						}
						sPar = string.Format("{0},{1},", sPar, t.ParameterName);
					}
				}
				returnValue = cmd.ExecuteNonQuery();
				sqlParameterCollection = cmd.Parameters;
			}
			return returnValue;
		}

		#endregion

		#region executeScalar

		public object ExecuteScalar()
		{
			using (var sqlconn = new SqlConnection(connectionString))
			{
				sqlconn.Open();
				SqlCommand cmd = sqlconn.CreateCommand();
				cmd.CommandText = SqlStatement;
				cmd.CommandType = commandType;
				cmd.CommandTimeout = CommandTimeOut;
				if (parameters.Count > 0)
				{
					foreach (SqlParameter t in parameters)
					{
						cmd.Parameters.Add(t.ParameterName, t.SqlDbType, t.Size).
							Value = t.Value;
						cmd.Parameters[t.ParameterName].Direction = t.Direction;
					}
				}
				object returnValue = cmd.ExecuteScalar();
				sqlParameterCollection = cmd.Parameters;
				return returnValue;
			}
		}

		#endregion

		#endregion
	}
}