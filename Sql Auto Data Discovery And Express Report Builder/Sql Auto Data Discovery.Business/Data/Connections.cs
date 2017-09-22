using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace Sql_Auto_Data_Discovery.Business.Data
{
    public static class Connections
    {
        private static List<KeyValuePair<string, SqlConnectionStringBuilder>> _list;

        public static List<KeyValuePair<string, SqlConnectionStringBuilder>> List
        {
            get
            {
                return _list ?? (_list =
                    Enumerable.Range(0, ConfigurationManager.ConnectionStrings.Count)
                        .Select(i => new KeyValuePair<string, SqlConnectionStringBuilder>(
                            ConfigurationManager.ConnectionStrings[i].Name,
                            new SqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings[i].ConnectionString)))
                        .Where(i => i.Key != "LocalSqlServer")
                        .ToList());
            }
        }

        public static KeyValuePair<string, SqlConnectionStringBuilder> Selected { get; set; }
    }
}