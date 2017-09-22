using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Sql_Auto_Data_Discovery.Business.Extentions;
using Sql_Auto_Data_Discovery.Business.Models.Commom;

namespace Sql_Auto_Data_Discovery.Business.Data.Base
{
    public class RepositoryBase : IDisposable
    {
        internal CommunicationService Db { get; set; }

        public int DefaultSelectTopNumberOfRows = 100000;

        public static readonly string LongDateFormat = "dd MMM yyyy HH:mm:ss";
        public static readonly string ShortDateFormat = "dd MMM yyyy";
        private const string DefaultSchema = "dbo";

        public RepositoryBase(string connectionString)
        {
            Db = new CommunicationService(connectionString);
        }

        #region Error Results
        protected Result ReturnExceptionResult(Exception exception)
        {
            return Results.ErrorResult(string.Format("{0}{1}{2}", exception.Message, Environment.NewLine, exception.StackTrace));
        }
        protected Result ReturnErrorResult(Exception exception)
        {
            return Results.ErrorResult(exception.Message);
        }
        protected Result<T> ReturnExceptionResult<T>(Exception exception)
        {
            return Results.ErrorResult(string.Format("{0}{1}{2}", exception.Message, Environment.NewLine, exception.StackTrace), default(T));
        }
        protected Result<T> ReturnErrorResult<T>(Exception exception)
        {
            return Results.ErrorResult(exception.Message, entity: default(T));
        } 
        #endregion

        public List<T> Select<T>(object table, object[] fields, params object[] order_by)
        {
            return Select<T>(DefaultSchema, table, null, fields, order_by, null);
        }
        public List<T> Select<T>(object table, object[] fields, object[] order_by, params KeyValue[] where)
        {
            return Select<T>(DefaultSchema, table, null, fields, order_by, where);
        }
        public List<T> Select<T>(object table, object[] fields, params KeyValue[] where)
        {
            return Select<T>(DefaultSchema, table, null, fields, null, where);
        }
        public List<T> Select<T>(object table, int? top, object[] fields, params KeyValue[] where)
        {
            return Select<T>(DefaultSchema, table, top, fields, null, where);
        }
        public List<T> Select<T>(object schema, object table, object[] fields, params object[] order_by)
        {
            return Select<T>(schema, table, null, fields, order_by, null);
        }
        public List<T> Select<T>(object schema, object table, object[] fields, object[] order_by, params KeyValue[] where)
        {
            return Select<T>(schema, table, null, fields, order_by, where);
        }
        public List<T> Select<T>(object schema, object table, object[] fields, params KeyValue[] where)
        {
            return Select<T>(schema, table, null, fields, null, where);
        }
        public List<T> Select<T>(object schema, object table, int? top, object[] fields, params KeyValue[] where)
        {
            return Select<T>(schema, table, top, fields, null, where);
        }
        public List<T> Select<T>(object schema, object table, int? top, object[] fields, object[] order_by, params KeyValue[] where)
        {
            try
            {
                if (table.IsNotSet()) throw new ArgumentNullException("table");
                if (fields.IsNotSet()) throw new ArgumentNullException("fields");
                if (fields.Any() == false) throw new Exception("There is no fields to select.");

                var fieldSql = new StringBuilder();
                var whereSql = new StringBuilder();
                var orderBySql = new StringBuilder();

                var isWhere = where.IsSet() && where.Any();
                var isOrderBy = order_by.IsSet() && order_by.Any();

                foreach (var i in fields.Select((field, index) => new { index, field }))
                    fieldSql.AppendLine(string.Format(" {0}{1} "
                        , i.index > 0 ? ", " : ""
                        , i.field));
                if (isWhere)
                    foreach (var i in where.Select((w, index) => new { index, where = w }))
                        whereSql.AppendLine(string.Format(" {0}{1} {2} @{1} "
                            , i.index > 0 ? "and " : ""
                            , i.where.Key
                            , i.where.OperatorValue));
                if (isOrderBy)
                    foreach (var i in order_by.Select((orderby, index) => new { orderby, index }))
                        orderBySql.AppendLine(string.Format(" {0}{1} "
                            , i.index > 0 ? ", " : ""
                            , i.orderby));

                var sql = string.Format(@"
                    select top {0} 
                    {1}
                    from {5}.{2}
                    {3}
                    {4}
                ", top ?? DefaultSelectTopNumberOfRows
                    , fieldSql
                    , table
                    , isWhere ? "where " + whereSql : ""
                    , isOrderBy ? "order by " + orderBySql : ""
                    , schema);
                return Db.Get<T>(sql, where);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public int? SelectCount(object table, params KeyValue[] where)
        {
            return SelectCount(schema: DefaultSchema, table: table, @where: where);
        }
        public int? SelectCount(object schema, object table, params KeyValue[] where)
        {
            try
            {
                if (table.IsNotSet()) throw new ArgumentNullException("table");

                var whereSql = new StringBuilder();

                var isWhere = where.IsSet() && where.Any();

                if (isWhere)
                    foreach (var i in where.Select((w, index) => new { index, where = w }))
                        whereSql.AppendLine(string.Format(" {0}{1} {2} @{1} "
                            , i.index > 0 ? "and " : ""
                            , i.where.Key
                            , i.where.OperatorValue));
                var sql = string.Format(@"
                    select count(*) 
                    from {2}.{0}
                    {1}
                ", table
                    , isWhere ? "where " + whereSql : ""
                    , schema);
                return Db.GetField(sql, where).AsInt();
            }
            catch (Exception e)
            {

                return null;
            }
        }
        public int? SelectFieldCount(object table, object field, params KeyValue[] where)
        {
            return SelectFieldCount(schema: DefaultSchema, table: table, field: field, @where: where);
        }
        public int? SelectFieldCount(string schema, object table, object field, params KeyValue[] where)
        {
            try
            {
                if (table.IsNotSet()) throw new ArgumentNullException("table");

                var whereSql = new StringBuilder();

                var isWhere = where.IsSet() && where.Any();

                if (isWhere)
                    foreach (var i in where.Select((w, index) => new { index, where = w }))
                        whereSql.AppendLine(string.Format(" {0}{1} {2} @{1} "
                            , i.index > 0 ? "and " : ""
                            , i.where.Key
                            , i.where.OperatorValue));

                var sql = string.Format(@"
                    select count({2}) 
                    from {3}.{0}
                    {1}
                ", table
                    , isWhere ? "where " + whereSql : ""
                    , field
                    , schema);
                return Db.GetField(sql, where).AsInt();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public string SelectField(object table, object field, params KeyValue[] where)
        {
            return SelectField(DefaultSchema, table, field, where);
        }
        public string SelectField(object schema, object table, object field, params KeyValue[] where)
        {
            try
            {
                if (table.IsNotSet()) throw new ArgumentNullException("table");

                var whereSql = new StringBuilder();

                var isWhere = where.IsSet() && where.Any();

                if (isWhere)
                    foreach (var i in where.Select((w, index) => new { index, where = w }))
                        whereSql.AppendLine(string.Format(" {0}{1} {2} @{1} "
                            , i.index > 0 ? "and " : ""
                            , i.where.Key
                            , i.where.OperatorValue));

                var sql = string.Format(@"
                    select {2} 
                    from {3}.{0}
                    {1}
                ", table
                    , isWhere ? "where " + whereSql : ""
                    , field
                    , schema);
                return Db.GetField(sql, where);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public int? SelectFieldInt(object table, object field, params KeyValue[] where)
        {
            return SelectFieldInt(DefaultSchema, table, field, where);
        }
        public int? SelectFieldInt(object schema, object table, object field, params KeyValue[] where)
        {
            try
            {
                if (table.IsNotSet()) throw new ArgumentNullException("table");

                var whereSql = new StringBuilder();

                var isWhere = where.IsSet() && where.Any();

                if (isWhere)
                    foreach (var i in where.Select((w, index) => new { index, where = w }))
                        whereSql.AppendLine(string.Format(" {0}{1} {2} @{1} "
                            , i.index > 0 ? "and " : ""
                            , i.where.Key
                            , i.where.OperatorValue));

                var sql = string.Format(@"
                    select {2} 
                    from {3}.{0}
                    {1}
                ", table
                    , isWhere ? "where " + whereSql : ""
                    , field
                    , schema);
                return Db.GetField(sql, where)
                .AsInt();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public string SelectMax(object table, object field, params KeyValue[] where)
        {
            return SelectMax(DefaultSchema, table, field, where);
        }
        public string SelectMax(object schema, object table, object field, params KeyValue[] where)
        {
            try
            {
                if (table.IsNotSet()) throw new ArgumentNullException("table");

                var whereSql = new StringBuilder();

                var isWhere = where.IsSet() && where.Any();

                if (isWhere)
                    foreach (var i in where.Select((w, index) => new { index, where = w }))
                        whereSql.AppendLine(string.Format(" {0}{1} {2} @{1} "
                            , i.index > 0 ? "and " : ""
                            , i.where.Key
                            , i.where.OperatorValue));

                var sql = string.Format(@"
                    select max({2}) 
                    from {3}.{0}
                    {1}
                ", table
                    , isWhere ? "where " + whereSql : ""
                    , field
                    , schema);
                return Db.GetField(sql, where);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public int? SelectMaxInt(object table, object field, params KeyValue[] where)
        {
            return SelectMaxInt(DefaultSchema, table, field, where);
        }
        public int? SelectMaxInt(object schema, object table, object field, params KeyValue[] where)
        {
            try
            {
                if (table.IsNotSet()) throw new ArgumentNullException("table");

                var whereSql = new StringBuilder();

                var isWhere = where.IsSet() && where.Any();

                if (isWhere)
                    foreach (var i in where.Select((w, index) => new { index, where = w }))
                        whereSql.AppendLine(string.Format(" {0}{1} {2} @{1} "
                            , i.index > 0 ? "and " : ""
                            , i.where.Key
                            , i.where.OperatorValue));

                var sql = string.Format(@"
                    select max({2}) 
                    from {3}.{0}
                    {1}
                ", table
                    , isWhere ? "where " + whereSql : ""
                    , field
                    , schema);
                return Db.GetField(sql, where)
                .AsInt();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public DateTime? SelectMaxDate(object table, object field, params KeyValue[] where)
        {
            return SelectMaxDate(DefaultSchema, table, field, where);
        }
        public DateTime? SelectMaxDate(object schema, object table, object field, params KeyValue[] where)
        {
            try
            {
                if (table.IsNotSet()) throw new ArgumentNullException("table");

                var whereSql = new StringBuilder();

                var isWhere = where.IsSet() && where.Any();

                if (isWhere)
                    foreach (var i in where.Select((w, index) => new { index, where = w }))
                        whereSql.AppendLine(string.Format(" {0}{1} {2} @{1} "
                            , i.index > 0 ? "and " : ""
                            , i.where.Key
                            , i.where.OperatorValue));

                var sql = string.Format(@"
                    select max({2}) 
                    from {3}.{0}
                    {1}
                 ", table
                    , isWhere ? "where " + whereSql : ""
                    , field
                    , schema);
                var result = Db.GetField(sql, where);
                return result.IsSet()
                    ? result.AsDateTime()
                    : null as DateTime?;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public string SelectMin(object table, object field, params KeyValue[] where)
        {
            return SelectMin(DefaultSchema, table, field, where);
        }
        public string SelectMin(object schema, object table, object field, params KeyValue[] where)
        {
            try
            {
                if (table.IsNotSet()) throw new ArgumentNullException("table");

                var whereSql = new StringBuilder();

                var isWhere = where.IsSet() && where.Any();

                if (isWhere)
                    foreach (var i in where.Select((w, index) => new { index, where = w }))
                        whereSql.AppendLine(string.Format(" {0}{1} {2} @{1} "
                            , i.index > 0 ? "and " : ""
                            , i.where.Key
                            , i.where.OperatorValue));

                var sql = string.Format(@"
                    select min({2}) 
                    from {3}.{0}
                    {1}
                ", table
                    , isWhere ? "where " + whereSql : ""
                    , field
                    , schema);
                return Db.GetField(sql, where);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public int? SelectMinInt(object table, object field, params KeyValue[] where)
        {
            return SelectMinInt(DefaultSchema, table, field, where);
        }
        public int? SelectMinInt(object schema, object table, object field, params KeyValue[] where)
        {
            try
            {
                if (table.IsNotSet()) throw new ArgumentNullException("table");

                var whereSql = new StringBuilder();

                var isWhere = where.IsSet() && where.Any();

                if (isWhere)
                    foreach (var i in where.Select((w, index) => new { index, where = w }))
                        whereSql.AppendLine(string.Format(" {0}{1} {2} @{1} "
                            , i.index > 0 ? "and " : ""
                            , i.where.Key
                            , i.where.OperatorValue));

                var sql = string.Format(@"
                    select min({2}) 
                    from {3}.{0}
                    {1}
                ", table
                    , isWhere ? "where " + whereSql : ""
                    , field
                    , schema);
                return Db.GetField(sql, where).AsInt();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public DateTime? SelectMinDate(object table, object field, params KeyValue[] where)
        {
            return SelectMinDate(DefaultSchema, table, field, where);
        }
        public DateTime? SelectMinDate(object schema, object table, object field, params KeyValue[] where)
        {
            try
            {
                if (table.IsNotSet()) throw new ArgumentNullException("table");

                var whereSql = new StringBuilder();

                var isWhere = where.IsSet() && where.Any();

                if (isWhere)
                    foreach (var i in where.Select((w, index) => new { index, where = w }))
                        whereSql.AppendLine(string.Format(" {0}{1} {2} @{1} "
                            , i.index > 0 ? "and " : ""
                            , i.where.Key
                            , i.where.OperatorValue));

                var sql = string.Format(@"
                    select min({2}) 
                    from {3}.{0}
                    {1}
                ", table
                    , isWhere ? "where " + whereSql : ""
                    , field
                    , schema);
                return Db.GetField(sql, where).AsDateTime();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public DataRow SelectDataRow(object table, params object[] fields)
        {
            return SelectDataTable(table: table, top: 1, fields: fields).FirstOrNull();
        }
        public DataRow SelectDataRow(object table, object[] fields, params KeyValue[] where)
        {
            return SelectDataTable(schema: DefaultSchema, table: table, top: 1, fields: fields, order_by: null, @where: where).FirstOrNull();
        }
        public DataRow SelectDataRow(object table, object[] fields, object[] order_by, params KeyValue[] where)
        {
            return SelectDataTable(schema: DefaultSchema, table: table, top: 1, fields: fields, order_by: order_by, @where: where).FirstOrNull();
        }

        public DataTable SelectDataTable(object table, int? top, params object[] fields)
        {
            return SelectDataTable(table: table, top: top, fields: fields, @where: null);
        }
        public DataTable SelectDataTable(object table, object[] fields, params KeyValue[] where)
        {
            return SelectDataTable(schema: DefaultSchema, table: table, top: null, fields: fields, @where: where, order_by: null);
        }
        public DataTable SelectDataTable(object schema, object table, object[] fields, params KeyValue[] where)
        {
            return SelectDataTable(schema: schema, table: table, top: null, fields: fields, @where: where, order_by: null);
        }
        public DataTable SelectDataTable(object table, int? top, object[] fields, params KeyValue[] where)
        {
            return SelectDataTable(schema: DefaultSchema, table: table, top: top, fields: fields, @where: where, order_by: null);
        }
        public DataTable SelectDataTable(object schema, object table, object[] fields, object[] order_by, params KeyValue[] where)
        {
            return SelectDataTable(schema: schema, table: table, top: null, fields: fields, @where: where, order_by: order_by);
        }
        public DataTable SelectDataTable(object table, object[] fields, object[] order_by, params KeyValue[] where)
        {
            return SelectDataTable(schema: DefaultSchema, table: table, top: null, fields: fields, @where: where, order_by: order_by);
        }
        public DataTable SelectDataTable(object table, int? top, object[] fields, object[] order_by, params KeyValue[] where)
        {
            return SelectDataTable(DefaultSchema, table, top, fields, order_by, where);
        }
        public DataTable SelectDataTable(object schema, object table, int? top, object[] fields, object[] order_by, params KeyValue[] where)
        {
            try
            {
                if (table.IsNotSet()) throw new ArgumentNullException("table");
                if (fields.IsNotSet()) throw new ArgumentNullException("fields");
                if (fields.Any() == false) throw new Exception("There is no fields to select.");

                var fieldSql = new StringBuilder();
                var whereSql = new StringBuilder();
                var orderBySql = new StringBuilder();

                var isWhere = where.IsSet() && where.Any();
                var isOrderBy = order_by.IsSet() && order_by.Any();

                foreach (var i in fields.Select((field, index) => new { index, field }))
                    fieldSql.AppendLine(string.Format(" {0}{1} "
                        , i.index > 0 ? ", " : ""
                        , i.field));
                if (isWhere)
                    foreach (var i in where.Select((w, index) => new { index, where = w }))
                        whereSql.AppendLine(string.Format(" {0}{1} {2} @{1} "
                            , i.index > 0 ? "and " : ""
                            , i.where.Key
                            , i.where.OperatorValue));
                if (isOrderBy)
                    foreach (var i in order_by.Select((orderby, index) => new { orderby, index }))
                        orderBySql.AppendLine(string.Format(" {0}{1} "
                            , i.index > 0 ? ", " : ""
                            , i.orderby));

                var sql = string.Format(@"
                    select top {0} 
                    {1}
                    from {5}.{2}
                    {3}
                    {4}
                ", top ?? DefaultSelectTopNumberOfRows
                    , fieldSql
                    , table
                    , isWhere ? "where " + whereSql : ""
                    , isOrderBy ? "order by " + orderBySql : ""
                    , schema);
                return Db.GetDataTable(sql, where);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public Result Insert(object table, params KeyValue[] fields)
        {
            return Insert(schema: DefaultSchema, table: table, fields: fields);
        }
        public Result Insert(string schema, object table, params KeyValue[] fields)
        {
            try
            {
                if (table.IsNotSet()) throw new ArgumentNullException("table");
                if (fields.IsNotSet()) throw new ArgumentNullException("fields");
                if (fields.Any() == false) throw new Exception("There is no fields to insert.");

                var fieldsSql = new StringBuilder();
                var valuesSql = new StringBuilder();

                foreach (var i in fields.Select((v, index) => new { index, field = v }))
                {
                    fieldsSql.AppendLine(string.Format(" {0}{1} ", i.index > 0 ? ", " : "", i.field.Key));
                    valuesSql.AppendLine(string.Format(" {0}@{1} ", i.index > 0 ? ", " : "", i.field.Key));
                }

                var sql = string.Format(@"
                    insert into {3}.{0}
                    (
                        {1}
                    )
                    values
                    (
                        {2}
                    )
                ", table
                 , fieldsSql
                 , valuesSql
                 , schema);
                return Db.Insert(sql, fields);
            }
            catch (Exception e)
            {
                return ReturnErrorResult(e);
            }
        }
        public Result<int?> InsertAndReturnInt(object table, object return_field, params KeyValue[] fields)
        {
            return InsertAndReturnInt(DefaultSchema, table, return_field, fields);
        }
        public Result<int?> InsertAndReturnInt(object schema, object table, object return_field, params KeyValue[] fields)
        {
            try
            {
                if (table.IsNotSet()) throw new ArgumentNullException("table");
                if (fields.IsNotSet()) throw new ArgumentNullException("fields");
                if (fields.Any() == false) throw new Exception("There is no fields to insert.");
                if (return_field.IsNotSet()) throw new ArgumentNullException("return_field");

                var fieldsSql = new StringBuilder();
                var valuesSql = new StringBuilder();

                foreach (var i in fields.Select((v, index) => new { index, field = v }))
                {
                    fieldsSql.AppendLine(string.Format(" {0}{1} ", i.index > 0 ? ", " : "", i.field.Key));
                    valuesSql.AppendLine(string.Format(" {0}@{1} ", i.index > 0 ? ", " : "", i.field.Key));
                }

                var sql = string.Format(@"
                    insert into {4}.{0}
                    (
                        {1}
                    )
                    output inserted.{3}
                    values
                    (
                        {2}
                    )
                ", table
                 , fieldsSql
                 , valuesSql
                 , return_field
                 , schema);
                return Db.Insert_ReturnFieldInt(sql, fields);
            }
            catch (Exception e)
            {
                return ReturnErrorResult<int?>(e);
            }
        }
        public Result<string> InsertAndReturn(object table, object return_field, params KeyValue[] fields)
        {
            return InsertAndReturn(DefaultSchema, table, return_field, fields);
        }
        public Result<string> InsertAndReturn(object schema, object table, object return_field, params KeyValue[] fields)
        {
            try
            {
                if (table.IsNotSet()) throw new ArgumentNullException("table");
                if (fields.IsNotSet()) throw new ArgumentNullException("fields");
                if (fields.Any() == false) throw new Exception("There is no fields to insert.");
                if (return_field.IsNotSet()) throw new ArgumentNullException("return_field");

                var fieldsSql = new StringBuilder();
                var valuesSql = new StringBuilder();

                foreach (var i in fields.Select((v, index) => new { index, field = v }))
                {
                    fieldsSql.AppendLine(string.Format(" {0}{1} ", i.index > 0 ? ", " : "", i.field.Key));
                    valuesSql.AppendLine(string.Format(" {0}@{1} ", i.index > 0 ? ", " : "", i.field.Key));
                }

                var sql = string.Format(@"
                    insert into {4}.{0}
                    (
                        {1}
                    )
                    output inserted.{3}
                    values
                    (
                        {2}
                    )
                ", table
                 , fieldsSql
                 , valuesSql
                 , return_field
                 , schema);
                return Db.Insert_ReturnField(sql, fields);
            }
            catch (Exception e)
            {
                return ReturnErrorResult<string>(e);
            }
        }
        public Result Update(object table, KeyValue[] fields, params KeyValue[] where)
        {
            return Update(DefaultSchema, table, fields, where);
        }
        public Result Update(object schema, object table, KeyValue[] fields, params KeyValue[] where)
        {
            try
            {
                if (table.IsNotSet()) throw new ArgumentNullException("table");
                if (fields.IsNotSet()) throw new ArgumentNullException("fields");
                if (fields.Any() == false) throw new Exception("There is no fields to update.");
                if (where.IsNotSet()) throw new ArgumentNullException("where");
                if (where.Any() == false) throw new Exception("There is no where clause, for this function this is not allowed.");

                var fieldsSql = new StringBuilder();
                var whereSql = new StringBuilder();

                foreach (var i in fields.Select((v, index) => new { index, field = v }))
                    fieldsSql.AppendLine(string.Format(" {0}{1} = @{1} ", i.index > 0 ? ", " : "", i.field.Key));
                foreach (var i in where.Select((v, index) => new { index, where = v }))
                    whereSql.AppendLine(string.Format(" {0}{1} {2} @{1} ", i.index > 0 ? "and " : "", i.where.Key, i.where.OperatorValue));

                var sqlParameters = new System.Collections.Generic.List<KeyValue>();
                sqlParameters.AddRange(fields);
                sqlParameters.AddRange(where);
                var sql = string.Format(@"
                    update {3}.{0}
                    set {1}
                    where {2}
                ", table
                 , fieldsSql
                 , whereSql
                 , schema);
                return Db.Update(sql, sqlParameters.ToArray());
            }
            catch (Exception e)
            {
                return ReturnErrorResult(e);
            }
        }
        public Result Delete(object table, params KeyValue[] where)
        {
            return Delete(DefaultSchema, table, where);
        }
        public Result Delete(object schema, object table, params KeyValue[] where)
        {
            try
            {
                if (table.IsNotSet()) throw new ArgumentNullException("table");
                if (where.IsNotSet()) throw new ArgumentNullException("where");
                if (where.Any() == false) throw new Exception("There is no where clause, for this function this is not allowed.");

                var whereSql = new StringBuilder();

                foreach (var i in where.Select((v, index) => new { index, where = v }))
                    whereSql.AppendLine(string.Format(" {0}{1} {2} @{1} ", i.index > 0 ? "and " : "", i.where.Key, i.where.OperatorValue));

                var sql = string.Format(@"
                    delete from {2}.{0}
                    where {1}
                ", table
                 , whereSql
                 , schema);
                return Db.Update(sql, where);
            }
            catch (Exception e)
            {
                return ReturnErrorResult(e);
            }
        }

        public Result ExecuteProcedure(object storedProcedure)
        {
            return ExecuteProcedure(DefaultSchema, storedProcedure, null);
        }
        public Result ExecuteProcedure(object schema, object storedProcedure, params KeyValue[] fields)
        {
            try
            {
                if (storedProcedure.IsNotSet()) throw new ArgumentNullException("storedProcedure");

                var fieldsSql = new StringBuilder();

                if (fields.IsSet() && fields.Any())
                {
                    foreach (var i in fields.Select((v, index) => new { index, field = v }))
                    {
                        fieldsSql.AppendLine(string.Format(" {0}@{1} = @{1} ", i.index > 0 ? ", " : "", i.field.Key));
                    }
                }

                var sql = string.Format(@"
                    execute {2}{0}
                        {1} 
                ", storedProcedure
                 , fieldsSql
                 , schema);
                return Db.ExecuteProcedure(sql, fields);
            }
            catch (Exception e)
            {
                return ReturnErrorResult(e);
            }
        }

        public void Dispose()
        {
            if (Db != null)
            {
                Db.Dispose();
            }
            Db = null;
        }
    }
}