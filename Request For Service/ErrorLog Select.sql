select l.Host,l.Version,l.Message,l.Namespace,l.Class,l.Method,l.StackTrace,l.DateCreated,u.Person_FirstName,u.Person_LastName
from RequestForService_01_001_Local..errorlogs l 
left join RequestForService_01_001_Local..users u	
on u.id = l.CreatedByUserId
order by l.DateCreated desc
-- truncate table RequestForService_01_001_Local..ErrorLogs
/*

*/