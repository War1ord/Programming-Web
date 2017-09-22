select distinct type,type_desc from sys.all_objects order by type_desc
select schema_id, name from sys.schemas
select * from sys.all_objects where type='U' order by name -- U - USER_TABLE
select * from sys.all_objects where type='V' order by name -- V - VIEW
select * from sys.all_objects where type='P' order by name -- P - SQL_STORED_PROCEDURE
select * from sys.all_objects where type='FN' order by name -- FN - SQL_SCALAR_FUNCTION
select * from sys.all_objects where type='IF' order by name -- IF - SQL_INLINE_TABLE_VALUED_FUNCTION
select * from sys.all_objects where type='AF' order by name -- AF - AGGREGATE_FUNCTION
select * from sys.all_objects where type='S' order by name -- S - SYSTEM_TABLE

select col.column_id, col.name, obj.name as [object_name] from sys.all_columns col inner join sys.all_objects obj on obj.object_id = col.object_id order by obj.name, col.column_id
select par.parameter_id, replace(par.name, '@', ''), obj.name as [object_name] from sys.all_parameters par inner join sys.all_objects obj on obj.object_id = par.object_id order by obj.name, par.parameter_id

select * from sys.all_sql_modules
select * from sys.objects

select c.ORDINAL_POSITION, c.COLUMN_NAME, c.TABLE_NAME from INFORMATION_SCHEMA.COLUMNS as c order by c.TABLE_NAME, c.ORDINAL_POSITION
select obj.name,* from sys.all_columns as col left join sys.all_objects as obj on obj.object_id = col.object_id


