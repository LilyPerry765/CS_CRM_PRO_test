
use songhor
go
   
  select TEL_NO from Vorodi as v 
  where stuff (v.TEL_NO,1,0,'83') not in 
  ( select TEL_PISH+TEL from Subscrib as t join FIMARK as F on f.ID_FINANCE =  t.ID_FINANCE where t.STOP_DATE=99999999  
  AND
   F.ID_MARKAZ in (select CenterCode from crm.dbo.center where RegionID=14) ) 

   --select * from SUBSCRIB
   --where TEL_PISH+TEL=cast (2545787 as nvarchar)