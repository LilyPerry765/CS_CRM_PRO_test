use songhor
go

   select t.TEL_PISH + t.TEL from Subscrib as t join FIMARK as F on f.ID_FINANCE =  t.ID_FINANCE  
   where F.ID_MARKAZ in (select CenterCode from crm.dbo.center where RegionID=14) 
   and 
   t.TEL_PISH + t.TEL not in(select stuff (v.TEL_NO,1,0,'83') from VORODI as v) 
   and 
   t.STOP_DATE=99999999

   --select * from VORODI
   --where TEL_NO=cast (5854151 as nvarchar)

