using System.Collections.Generic;
using System.Data;
using System.Linq;
using SqlEnums.master;
using SqlEnums.master.Columns;
using Sql_Auto_Data_Discovery.Business.Data.Base;
using Sql_Auto_Data_Discovery.Business.Extentions;
using Sql_Auto_Data_Discovery.Business.Models;
using Sql_Auto_Data_Discovery.Business.Models.Commom;
using Sql_Auto_Data_Discovery.Business.Models.Sql;
using System;
using Sql_Auto_Data_Discovery.Business.ViewModels.Discover;

namespace Sql_Auto_Data_Discovery.Business.Data
{
    public class Database : RepositoryBase
    {
        public Database(string connectionString) : base(connectionString){}

        public List<SqlObjectsModel> GetListOfViewableObjects()
        {
            return Db.Get<SqlObjectsModel>(@"
                    select object_id, name, type_desc 
                    from sys.objects 
                    where type_desc in ('USER_TABLE', 'VIEW') 
                    order by type_desc, name
                ");
        }

        public List<SqlObjectsModel> GetListOfObjects()
        {
            var table = SelectDataTable(Schemas.sys, Views.objects
                , fields: new object[]
                {
                    objects.object_id,
                    objects.name,
                    objects.type_desc,
                }
                , order_by: new object[]
                {
                    objects.type_desc,
                    objects.name,
                });
            return table
                .AsEnumerable()
                .Select(i => new SqlObjectsModel
                {
                    name = i.Field<string>(objects.name.ToString()),
                    object_id = i.Field<int>(objects.object_id.ToString()),
                    type_desc = i.Field<string>(objects.type_desc.ToString()),
                }).ToList();
        }

        public List<SqlTableStructureModel> GetSqlTableStructure(int objectId)
        {
            var table = SelectDataTable(Schemas.sys, Views.all_columns
                , fields: new object[]
                {
                    all_columns.column_id,
                    all_columns.name,
                    all_columns.max_length,
                    all_columns.is_nullable,
                    all_columns.is_identity,
                    all_columns.is_computed,
                }
                , where: new[]
                {
                    new KeyValue(all_columns.object_id, objectId)
                });
            return table.AsEnumerable()
                .Select(i => new SqlTableStructureModel
                {
                    column_id = i.Field<int>(all_columns.column_id.ToString()),
                    name = i.Field<string>(all_columns.name.ToString()),
                    max_length = i.Field<short>(all_columns.max_length.ToString()),
                    is_nullable = i.Field<bool>(all_columns.is_nullable.ToString()),
                    is_identity  = i.Field<bool>(all_columns.is_identity.ToString()),
                    is_computed = i.Field<bool>(all_columns.is_computed.ToString()),
                }).ToList();
        }

        public DataTable GetDataTable(string table, Details_Filter_ViewModel filter, Details_OrderBy_ViewModel orderBy)
        {
            return Db.GetDataTable(string.Format("SELECT TOP {1} {2} FROM {0} {3}", table, filter.Top
                , filter.ColumnsToShow.IsSet() && filter.ColumnsToShow.Any()
                    ? string.Join(", ", filter.ColumnsToShow)
                    : "*"
                , orderBy.ColumnsToOrderBy.IsSet() && orderBy.ColumnsToOrderBy.Any()
                    ? string.Format("order by {0}", string.Join(", ", orderBy.ColumnsToOrderBy))
                    : ""
                ));
        }

        public DataTable GetColumnNames(string t)
        {
            return Db.GetDataTable(@"
                SELECT c.object_id, c.name
                FROM sys.objects o
                INNER JOIN sys.columns c ON c.object_id = o.object_id
                WHERE o.name = @object_name
            ", new KeyValue("object_name", t));
        }
    }
}
