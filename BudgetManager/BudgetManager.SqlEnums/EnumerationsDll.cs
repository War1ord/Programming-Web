using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManager.Enums
{
	public class EnumerationsDll
	{
		private static string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();

		public EnumerationsDll()
		{
			CreateEnumerationsDll(_connectionString, "BudgetManager.Enums", "BudgetManager.SqlEnums");
		}

		/// <summary>
		/// Creates the Enumerations Dll.
		/// </summary>
		private static void CreateEnumerationsDll(string connectionString, string Namespace, string assemblyNameString)
		{
			const string dllFileExtention = ".dll";
			AppDomain currentDomain = GetCurrentDomain();
			/*Create a dynamic assembly in the current application domain, and allow it to be executed and saved to disk.*/
			AssemblyName assemblyName = CreateAssemblyName(assemblyNameString);
			AssemblyBuilder assemblyBuilder = DefineDynamicAssemblyBuilder(currentDomain, assemblyName);
			/*Define a dynamic module in "MyEnums" assembly. For a single-module assembly, the module has the same name as the assembly.*/
			ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule(assemblyName.Name,
			                                                                  assemblyName.Name + dllFileExtention);
			/* Define a public enumeration with the name of SqlStoredProcedures and an underlying type of Integer. 
			 * and Get data from database
			 * and Create the enum*/
			const string columnNameForSqlObjects = "name";
			const string objectIdForSqlObjects = "object_id";
			string sqlStoredProceduresEnumerationName = Namespace + "." + "StoredProcedures";
			const string sqlStoredProceduresSqlStatement
				= "select o.object_id ,o.name from sys.objects as o where o.type = 'p' order by o.type_desc,o.name ";
			DefineAnEnumerationOfSqlObjects(sqlStoredProceduresEnumerationName, moduleBuilder, connectionString,
			                                sqlStoredProceduresSqlStatement, columnNameForSqlObjects, objectIdForSqlObjects);
			/* Define a public enumeration with the name of SqlViews and an underlying type of Integer
			 * and Get data from database
			 * and Create the enum */
			string sqlViewsEnumerationName = Namespace + "." + "Views";
			const string sqlViewsSqlStatement
				= "select o.object_id ,o.name from sys.objects as o where o.type = 'v' order by o.type_desc,o.name ";
			DefineAnEnumerationOfSqlObjects(sqlViewsEnumerationName, moduleBuilder, connectionString, sqlViewsSqlStatement,
			                                columnNameForSqlObjects, objectIdForSqlObjects);
			/* Define a public enumeration with the name of SqlTables and an underlying type of Integer */
			string sqlTablesEnumerationName = Namespace + "." + "Tables";
			const string sqlTablesSqlStatement
				= "select o.object_id ,o.name from sys.objects as o where o.type = 'u' order by o.type_desc,o.name ";
			DefineAnEnumerationOfSqlObjects(sqlTablesEnumerationName, moduleBuilder, connectionString, sqlTablesSqlStatement,
			                                columnNameForSqlObjects, objectIdForSqlObjects);
			/* Defines a collection of public enumerations with the name of enumeratedtypesMyenum and an underlying type of Integer */
			string sqltablesColumnsEnumerationName1StPart = Namespace + "." + "TablesColumns";
			const string sqltablescolumnsColumnName = "COLUMN_NAME";
			const string sqlTablesColumnsLiteralValue = "ORDINAL_POSITION";
			const string sqlTablesColumnsGroupingColumnName = "TABLE_NAME";
			const string sqlTablesColumnsSqlStatement
				=
				"select c.ORDINAL_POSITION, c.COLUMN_NAME, c.TABLE_NAME from INFORMATION_SCHEMA.COLUMNS as c order by c.TABLE_NAME, c.ORDINAL_POSITION";
			DefineACollectionOfSqlTablesColumnsEnumerations(sqltablesColumnsEnumerationName1StPart, moduleBuilder,
			                                                connectionString, sqltablescolumnsColumnName,
			                                                sqlTablesColumnsSqlStatement, sqlTablesColumnsGroupingColumnName,
			                                                sqlTablesColumnsLiteralValue);
			/* Defines a collection of public enumerations with the name of enumeratedtypesMyenum and an underlying type of Integer */
			string sqlParametersEnumerationName1StPart = Namespace + "." + "Parameters";
			const string sqlParametersColumnName = "PARAMETER_NAME";
			const string sqlParametersLiteralValue = "ORDINAL_POSITION";
			const string sqlParametersGroupingColumnName = "SPECIFIC_NAME";
			const string sqlParametersSqlStatement
				=
				"select p.ORDINAL_POSITION, replace(p.PARAMETER_NAME,'@','')PARAMETER_NAME, p.SPECIFIC_NAME from INFORMATION_SCHEMA.PARAMETERS as p order by p.SPECIFIC_NAME, p.ORDINAL_POSITION";
			DefineACollectionOfSqlTablesColumnsEnumerations(sqlParametersEnumerationName1StPart, moduleBuilder, connectionString,
			                                                sqlParametersColumnName, sqlParametersSqlStatement,
			                                                sqlParametersGroupingColumnName, sqlParametersLiteralValue);
			SaveAssembly(assemblyBuilder, assemblyName.Name + dllFileExtention);
		}

		/// <summary>
		/// Defines an enumeration of SQL objects.
		/// </summary>
		/// <param name="enumerationName">Name of the enumeration.</param>
		/// <param name="moduleBuilder">The module builder.</param>
		/// <param name="connectionString">The connection string.</param>
		/// <param name="sqlStatement">The SQL statement.</param>
		/// <param name="columnName">Name of the column.</param>
		/// <param name="literalValue">The object id.</param>
		private static void DefineAnEnumerationOfSqlObjects(string enumerationName, ModuleBuilder moduleBuilder,
		                                                    string connectionString, string sqlStatement,
		                                                    string columnName, string literalValue)
		{
			EnumBuilder enumBuilder = moduleBuilder.DefineEnum(enumerationName, TypeAttributes.Public, typeof (int));
			Connections connections = new Connections(connectionString, CommandType.Text, sqlStatement);
			DataTable storedProceduresDataTable = connections.GetDataTable();
			foreach (DataRow row in storedProceduresDataTable.Rows)
			{
				enumBuilder.DefineLiteral(row[columnName].ToString(), int.Parse(row[literalValue].ToString()));
			}
			enumBuilder.CreateType();
		}

		/// <summary>
		/// Defines the A collection of SQL tables columns enumerations.
		/// </summary>
		/// <param name="sqltablescolumnsEnumerationName1StPart">The sqltablescolumns enumeration name1 st part.</param>
		/// <param name="moduleBuilder">The module builder.</param>
		/// <param name="connectionString">The connection string.</param>
		/// <param name="sqlTablesColumnsColumnName">Name of the SQL tables columns column.</param>
		/// <param name="tablesColumnsSqlStatement">The tables columns SQL statement.</param>
		/// <param name="sqlTablesColumnsGroupingColumnName">Name of the SQL tables columns grouping column.</param>
		/// <param name="sqlTablesColumnsLiteralValue">The SQL tables columns literal value.</param>
		private static void DefineACollectionOfSqlTablesColumnsEnumerations(string sqltablescolumnsEnumerationName1StPart,
		                                                                    ModuleBuilder moduleBuilder,
		                                                                    string connectionString,
		                                                                    string sqlTablesColumnsColumnName,
		                                                                    string tablesColumnsSqlStatement,
		                                                                    string sqlTablesColumnsGroupingColumnName,
		                                                                    string sqlTablesColumnsLiteralValue)
		{
			var tablesColumnsConn = new Connections(connectionString, CommandType.Text, tablesColumnsSqlStatement);
			var tablesColumnsDataTable = tablesColumnsConn.GetDataTable();
			var tablesColumnsGrouped = tablesColumnsDataTable.Rows
				.Cast<DataRow>()
				.GroupBy(row => row[sqlTablesColumnsGroupingColumnName].ToString())
				.Select(rowGroupings => rowGroupings);
			foreach (IGrouping<string, DataRow> groupings in tablesColumnsGrouped)
			{
				string sqltablescolumnsEnumerationName = sqltablescolumnsEnumerationName1StPart + "." + groupings.Key;
				EnumBuilder tablesColumnsEnumBuilder = moduleBuilder.DefineEnum(sqltablescolumnsEnumerationName,
				                                                                TypeAttributes.Public, typeof (int));
				foreach (DataRow dataRow in groupings)
				{
					int literalValue = int.Parse(dataRow[sqlTablesColumnsLiteralValue].ToString());
					if (literalValue > 0)
					{
						tablesColumnsEnumBuilder.DefineLiteral(dataRow[sqlTablesColumnsColumnName].ToString(), literalValue);
					}
				}
				tablesColumnsEnumBuilder.CreateType();
			}
		}

		/// <summary>
		/// Gets the current domain.
		/// </summary>
		/// <returns></returns>
		private static AppDomain GetCurrentDomain()
		{
			// Get the current application domain for the current thread
			AppDomain currentDomain = AppDomain.CurrentDomain;
			return currentDomain;
		}

		/// <summary>
		/// Creates the name of the assembly.
		/// </summary>
		/// <param name="assemblyName"> </param>
		/// <returns></returns>
		private static AssemblyName CreateAssemblyName(string assemblyName)
		{
			return new AssemblyName(assemblyName);
		}

		/// <summary>
		/// Defines the dynamic assembly builder.
		/// </summary>
		/// <param name="currentDomain">The current domain.</param>
		/// <param name="assemblyName">Name of the assembly.</param>
		/// <returns></returns>
		private static AssemblyBuilder DefineDynamicAssemblyBuilder(AppDomain currentDomain, AssemblyName assemblyName)
		{
			AssemblyBuilder assemblyBuilder = currentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndSave,
			                                                                      currentDomain.BaseDirectory);
			return assemblyBuilder;
		}

		/// <summary>
		/// Saves the assembly.
		/// </summary>
		/// <param name="assemblyBuilder">The assembly builder.</param>
		/// <param name="assemblyfileName">Name of the assemblyfile.</param>
		private static void SaveAssembly(AssemblyBuilder assemblyBuilder, string assemblyfileName)
		{
			// Finally, save the assembly
			assemblyBuilder.Save(assemblyfileName);
		}

		/// <summary>
		/// Prompts the user.
		/// </summary>
		public void PromptUser()
		{
			Console.WriteLine("Press any key to continue . . .");
			Console.Read();
		}

		public void DisplayConfirmation()
		{
			Console.WriteLine("Sql Enumerations Dll Created. . .");
		}
	}
}