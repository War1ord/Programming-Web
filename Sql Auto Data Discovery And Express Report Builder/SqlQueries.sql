select object_id,name,type_desc from sys.objects where type_desc in ('USER_TABLE')

select name,type,type_desc,definition,* 
	from sys.all_sql_modules m 
	inner join sys.objects o on o.object_id = m.object_id 
	where name like '%%'

select o.name,o.type,o.type_desc,m.definition,c.name[column_name],* 
	from sys.objects o 
	left join sys.all_sql_modules m on m.object_id = o.object_id 
	left join sys.all_columns c on c.object_id = o.object_id
	where o.name like '%%'

select	column_id
	,	name
	,	system_type_id
	,	user_type_id
	,	max_length
	,	precision
	,	scale
	,	is_nullable
	,	is_identity
	,	is_computed
from sys.all_columns c
where object_id = 1941581955