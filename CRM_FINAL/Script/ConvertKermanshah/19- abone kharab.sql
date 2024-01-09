USE [crm]
GO

UPDATE [Harsin].[dbo].[Abone] SET [TYPE_POST]=IIF( TYPE_POST is null , 1 , IIF(TYPE_POST = '' ,  1 , IIF(TYPE_POST = 'A' ,  2 , 3)))
--select ab.*,c.CabinetNumber,p.Number,p.AorBType,pc.ConnectionNo
update pc set pc.Status = 4
from crm.dbo.Cabinet  as c 
join crm.dbo.Post as  p on c.ID = p.CabinetID
join crm.dbo.PostContact as pc  on pc.PostID = p.ID
join [Harsin].[dbo].[Abone] as ab on ab.KAFO = c.CabinetNumber and ab.POST = p.Number and pc.ConnectionNo = ab.ZOJ and ab.TYPE_POST=p.AorBType
join center as cen on cen.ID=c.CenterID
join Region as reg on reg.id=cen.regionid
join city on city.id=reg.cityid
where city.id=13 and cen.CenterCode=351


