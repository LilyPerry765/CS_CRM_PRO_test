use crm
go

update [InstallRequest] set isgsm=1
from telephone as t
join request as req on req.telephoneno=t.telephoneno
join InstallRequest as ir on ir.RequestID=req.id
where  t.usagetype=3


