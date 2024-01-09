use CRM
go


declare @CityID int ; set @CityID = 7;

update T
set status = 2
 from Telephone as T join
(
select TEL_NO as tel from [salas].[dbo].[Vorodi] as v where v.TYPE != 504 and v.TYPE != 508 and v.isvalid is null
) as Temp on Temp.tel = T.TelephoneNoIndividual 
											 join center on center.id=t.CenterID
											 join region on center.regionid=region.id
											 join city on city.id=region.cityid
											 where city.id=@CityID


update Bu
set Bu.Status = 1 , 
    Bu.SwitchPortID = Bt.SwitchPortID
from Bucht as Bu join 
(
SELECT     dbo.Bucht.ID , dbo.Telephone.SwitchPortID
FROM       dbo.MDF INNER JOIN
                      dbo.MDFFrame ON dbo.MDF.ID = dbo.MDFFrame.MDFID  
					  join center on center.id=MDF.CenterID
					  join region on center.regionid=region.id
					  join city on city.id=region.cityid INNER JOIN
                      dbo.VerticalMDFColumn ON dbo.MDFFrame.ID = dbo.VerticalMDFColumn.MDFFrameID INNER JOIN
                      dbo.VerticalMDFRow ON dbo.VerticalMDFColumn.ID = dbo.VerticalMDFRow.VerticalMDFColumnID INNER JOIN
                      dbo.Bucht ON dbo.VerticalMDFRow.ID = dbo.Bucht.MDFRowID inner join
					  [salas].[dbo].[Vorodi] as kv   on dbo.Bucht.BuchtNo = kv.ETE  and dbo.VerticalMDFRow.VerticalRowNo = kv.TAB  and dbo.VerticalMDFColumn.VerticalCloumnNo = kv.RAD and kv.TYPE != 504 and kv.TYPE != 508 and kv.isvalid is null join
					  dbo.Telephone on kv.TEL_NO  = Telephone.TelephoneNoIndividual  and Telephone.centerid=center.id
					
where  dbo.MDF.Description not in (N'PCM' , N'ADSL') and city.id=@CityID and  kv.kafo=bucht.cabinetnumber
) as BT on Bt.ID = Bu.ID


update Bu
set Bu.ConnectionID = postContactID
from Bucht as Bu join 
(
SELECT     dbo.Bucht.ID , PostContact.ID as postContactID
FROM       dbo.MDF INNER JOIN
                      dbo.MDFFrame ON dbo.MDF.ID = dbo.MDFFrame.MDFID 
					  join center on center.id=MDF.CenterID
					  join region on center.regionid=region.id
					  join city on city.id=region.cityid INNER JOIN
                      dbo.VerticalMDFColumn ON dbo.MDFFrame.ID = dbo.VerticalMDFColumn.MDFFrameID INNER JOIN
                      dbo.VerticalMDFRow ON dbo.VerticalMDFColumn.ID = dbo.VerticalMDFRow.VerticalMDFColumnID INNER JOIN
                      dbo.Bucht ON dbo.VerticalMDFRow.ID = dbo.Bucht.MDFRowID inner join
					  [salas].[dbo].[Vorodi] as kv   on dbo.Bucht.BuchtNo = kv.ETE  and dbo.VerticalMDFRow.VerticalRowNo = kv.TAB  and dbo.VerticalMDFColumn.VerticalCloumnNo = kv.RAD  join
					  dbo.Cabinet on  Cabinet.CabinetNumber = kv.KAFO and dbo.Cabinet.CenterID = center.ID  join
					  dbo.Post on Cabinet.id = post.CabinetID and post.Number = kv.POST join
					  dbo.PostContact on Post.id = PostContact.PostID and PostContact.ConnectionNo = kv.ZOJ
where kv.TYPE != 504 and kv.TYPE != 508 and post.AorBType = kv.AORB and dbo.MDF.Description not in (N'PCM' , N'ADSL') and kv.isvalid is null  AND city.id=@CityID
) as BT on Bt.ID = Bu.ID


update PC
set PC.Status = 1  
from PostContact as PC join 
(
SELECT      PostContact.ID as postContactID
FROM        [salas].[dbo].[Vorodi] as kv  join 	
					  dbo.Cabinet on  Cabinet.CabinetNumber = kv.KAFO  
					  join center on center.id=cabinet.CenterID
					  join region on center.regionid=region.id
					  join city on city.id=region.cityid INNER JOIN
					  dbo.Post on Cabinet.id = post.CabinetID and post.Number = kv.POST  join
					  dbo.PostContact on Post.id = PostContact.PostID and PostContact.ConnectionNo = kv.ZOJ
where kv.TYPE != 504 and kv.TYPE != 508 and post.AorBType = kv.AORB and kv.isvalid is null  AND city.id=@CityID 
) as BT on Bt.postContactID = PC.ID
 
