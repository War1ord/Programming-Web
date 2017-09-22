using System.Data;

namespace Sql_Auto_Data_Discovery.Business.Extentions
{
    using System;
    using System.Collections.Generic;

    public static class DataTableExtentions
    {
        public static DataTable FirstOrNull(this DataSet dataSet)
        {
            return dataSet != null && dataSet.Tables.Count > 0 ? dataSet.Tables[0] : null;
        }

        public static DataRow FirstOrNull(this DataTable dataTable)
        {
            return dataTable != null && dataTable.Rows.Count > 0 ? dataTable.Rows[0] : null;
        }

        public static KeyValuePair<int, DataColumn>[] GetColumns(this DataTable dataTable)
        {
            var list = new List<KeyValuePair<int, DataColumn>>();
            if (dataTable != null)
            {
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    list.Add(new KeyValuePair<int, DataColumn>(i, dataTable.Columns[i]));
                }
            }
            return list.ToArray();
        }

        public static KeyValuePair<int, string>[] GetColumnNames(this DataTable dataTable)
        {
            var list = new List<KeyValuePair<int, string>>();
            if (dataTable != null)
            {
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    list.Add(new KeyValuePair<int, string>(i, dataTable.Columns[i].ColumnName));
                }
            }
            return list.ToArray();
        }

        public static string[] GetColumnNamesArray(this DataTable dataTable)
        {
            var list = new List<string>();
            if (dataTable != null)
            {
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    list.Add(dataTable.Columns[i].ColumnName);
                }
            }
            return list.ToArray();
        }

        public static KeyValuePair<int, KeyValuePair<Type, string>>[] GetColumnNamesAndType(this DataTable dataTable)
        {
            var list = new List<KeyValuePair<int, KeyValuePair<Type, string>>>();
            if (dataTable != null)
            {
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    list.Add(new KeyValuePair<int, KeyValuePair<Type, string>>(i,
                        new KeyValuePair<Type, string>(dataTable.Columns[i].DataType
                            , dataTable.Columns[i].ColumnName)));
                }
            }
            return list.ToArray();
        }

    }
}