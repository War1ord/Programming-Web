using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Sql_Auto_Data_Discovery.Business.Models.Commom;

namespace Sql_Auto_Data_Discovery.Business.Extentions
{
    public static class DataContextExtentions
    {
        public static SqlParameter[] ToSqlParameters(this KeyValue[] parameters)
        {
            return parameters.IsSet()
                ? parameters.Where(p => p.IsSet())
                    .Select(p => new SqlParameter(p.Key.StartsWith("@") ? p.Key : "@" + p.Key, p.Value))
                    .ToArray()
                : null;
        }
        public static object[] ToParametersArray(this IEnumerable<KeyValue> parameters)
        {
            return parameters.IsSet()
                ? parameters.Where(p => p.IsSet())
                    .Select(p => new SqlParameter(p.Key.StartsWith("@") ? p.Key : "@" + p.Key, p.Value))
                    .Cast<object>()
                    .ToArray()
                : null;
        }
    }
}