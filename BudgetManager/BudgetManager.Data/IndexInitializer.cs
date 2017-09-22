using BudgetManager.Attributes;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Reflection;

namespace BudgetManager.Data
{
	/// <summary>
	/// Helper to add index
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class IndexInitializer<T> : IDatabaseInitializer<T> where T : DbContext
    {
        /// <summary>
        /// The create index query template
        /// </summary>
        private const string CreateIndexQueryTemplate = @"IF NOT EXISTS(
                                                                    SELECT 1 
                                                                    FROM sys.indexes 
                                                                    WHERE name = '{indexName}' 
                                                                    AND object_id = OBJECT_ID('{tableName}')
                                                                    )
                                                            BEGIN
	                                                        CREATE {unique} INDEX {indexName} ON {tableName} ({columnName})
                                                            END";
        public void InitializeDatabase(T context)
        {
            const BindingFlags PublicInstance = BindingFlags.Public | BindingFlags.Instance;
            foreach (var dataSetProperty in typeof(T).GetProperties(PublicInstance).Where(p => p.PropertyType.Name == typeof(DbSet<>).Name))
            {
                var entityType = dataSetProperty.PropertyType.GetGenericArguments().Single();
                TableAttribute[] tableAttributes = (TableAttribute[])entityType.GetCustomAttributes(typeof(TableAttribute), false);
                foreach (var property in entityType.GetProperties(PublicInstance))
                {
                    DatabaseIndexAttribute[] indexAttributes = (DatabaseIndexAttribute[])property.GetCustomAttributes(typeof(DatabaseIndexAttribute), false);
                    NotMappedAttribute[] notMappedAttributes = (NotMappedAttribute[])property.GetCustomAttributes(typeof(NotMappedAttribute), false);
                    if (indexAttributes.Length > 0 && notMappedAttributes.Length == 0)
                    {
                        ColumnAttribute[] columnAttributes = (ColumnAttribute[])property.GetCustomAttributes(typeof(ColumnAttribute), false);
                        foreach (var indexAttribute in indexAttributes)
                        {
                            string indexName = indexAttribute.Name;
                            string tableName = tableAttributes.Length != 0 ? tableAttributes[0].Name : dataSetProperty.Name;
                            string columnName = columnAttributes.Length != 0 ? columnAttributes[0].Name : property.Name;
                            string query = CreateIndexQueryTemplate.Replace("{indexName}", indexName).Replace("{tableName}", tableName).Replace("{columnName}", columnName).Replace("{unique}", indexAttribute.IsUnique ? "UNIQUE" : string.Empty);

                            context.Database.CreateIfNotExists();
                            context.Database.ExecuteSqlCommand(query);
                        }
                    }
                }
            }
        }
    }
}