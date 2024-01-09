use CRM
go

declare @centerID int ; set @centerID = 5;

declare @kafo int ; set @kafo = 108;

--update Bucht  set SwitchPortID = null , ConnectionID = null , Status = 0 from Bucht 
-- join Telephone on Bucht.SwitchPortID = Telephone.SwitchPortID 
-- join OldData.[dbo].BEASAT_VORODI as v on  v.TEL_NO = Telephone.TelephoneNoIndividual and Telephone.CenterID = @centerID


update T
set status = 2
from Telephone as T join
(
select TEL_NO as tel from OldData.[dbo].BEASAT_VORODI as v where v.TYPE != 504 and v.TYPE != 508 and v.KAFO = @kafo
) as Temp on Temp.tel = T.TelephoneNoIndividual and T.CenterID = @centerID

update Bu
set Bu.Status = 1 , 
    Bu.SwitchPortID = Bt.SwitchPortID
from Bucht as Bu join 
(
SELECT     dbo.Bucht.ID , dbo.Telephone.SwitchPortID
FROM                 dbo.MDF INNER JOIN
                      dbo.MDFFrame ON dbo.MDF.ID = dbo.MDFFrame.MDFID and MDF.CenterID = @centerID  INNER JOIN
                      dbo.VerticalMDFColumn ON dbo.MDFFrame.ID = dbo.VerticalMDFColumn.MDFFrameID INNER JOIN
                      dbo.VerticalMDFRow ON dbo.VerticalMDFColumn.ID = dbo.VerticalMDFRow.VerticalMDFColumnID INNER JOIN
                      dbo.Bucht ON dbo.VerticalMDFRow.ID = dbo.Bucht.MDFRowID inner join
					  OldData.[dbo].BEASAT_VORODI as kv   on dbo.Bucht.BuchtNo = kv.ETE  and dbo.VerticalMDFRow.VerticalRowNo = kv.TAB  and dbo.VerticalMDFColumn.VerticalCloumnNo = kv.RAD and kv.TYPE != 504 and kv.TYPE != 508 and kv.KAFO = @kafo join
					  dbo.Telephone on kv.TEL_NO  = Telephone.TelephoneNoIndividual and Telephone.CenterID = @centerID 
where  dbo.MDF.Description not in (N'PCM' , N'ADSL')
) as BT on Bt.ID = Bu.ID


update Bu
set Bu.ConnectionID = postContactID
from Bucht as Bu join 
(
SELECT     dbo.Bucht.ID , PostContact.ID as postContactID
FROM       dbo.MDF INNER JOIN
                      dbo.MDFFrame ON dbo.MDF.ID = dbo.MDFFrame.MDFID and MDF.CenterID = @centerID INNER JOIN
                      dbo.VerticalMDFColumn ON dbo.MDFFrame.ID = dbo.VerticalMDFColumn.MDFFrameID INNER JOIN
                      dbo.VerticalMDFRow ON dbo.VerticalMDFColumn.ID = dbo.VerticalMDFRow.VerticalMDFColumnID INNER JOIN
                      dbo.Bucht ON dbo.VerticalMDFRow.ID = dbo.Bucht.MDFRowID inner join
					  OldData.[dbo].BEASAT_VORODI as kv   on dbo.Bucht.BuchtNo = kv.ETE  and dbo.VerticalMDFRow.VerticalRowNo = kv.TAB  and dbo.VerticalMDFColumn.VerticalCloumnNo = kv.RAD  and kv.KAFO = @kafo join
					  dbo.Cabinet on  Cabinet.CabinetNumber = kv.KAFO and dbo.Cabinet.CenterID = @centerID  join
					  dbo.Post on Cabinet.id = post.CabinetID and post.Number = kv.POST join
					  dbo.PostContact on Post.id = PostContact.PostID and PostContact.ConnectionNo = kv.ZOJ
where kv.TYPE != 504 and kv.TYPE != 508 and post.AorBType = kv.AORB and dbo.MDF.Description not in (N'PCM' , N'ADSL')
) as BT on Bt.ID = Bu.ID


update PC
set PC.Status = 1  
from PostContact as PC join 
(
SELECT      PostContact.ID as postContactID
FROM        OldData.[dbo].BEASAT_VORODI as kv  join 	
					  dbo.Cabinet on  Cabinet.CabinetNumber = kv.KAFO  and dbo.Cabinet.CenterID = @centerID join
					  dbo.Post on Cabinet.id = post.CabinetID and post.Number = kv.POST  join
					  dbo.PostContact on Post.id = PostContact.PostID and PostContact.ConnectionNo = kv.ZOJ
where kv.TYPE != 504 and kv.TYPE != 508 and post.AorBType = kv.AORB and kv.KAFO = @kafo
) as BT on Bt.postContactID = PC.ID


update T
set T.InstallAddressID = A.ID ,
 T.CorrespondenceAddressID = A.ID
from [CRM].[dbo].[Address] AS A
join [OldCustomerDate].[dbo].[Subscrib] as S on  A.ElkaID = S.ID_FINANCE 
join [OldCustomerDate].[dbo].[FIMARK] as fim on fim.ID_FINANCE = s.ID_FINANCE
join [OldCustomerDate].[dbo].Markaz as m on m.ID = fim.ID_MARKAZ
join [CRM].[dbo].Center as C on C.CenterCode = m.ID
join Telephone as T on T.TelephoneNoIndividual = SUBSTRING(s.TEL_PISH , 3 ,5) + S.TEL
join [OldData].dbo.BEASAT_VORODI  as  v on T.TelephoneNoIndividual = v.TEL_NO  and v.KAFO = @kafo
where s.STOP_DATE = '99999999' and A.KerStopDate = '99999999' and T.CenterID = C.ID

update T
set T.CustomerID = C.ID
from [CRM].[dbo].Customer as C
join [OldCustomerDate].[dbo].[Subscrib] as S on  S.ID_FINANCE = C.ElkaID 
join [OldCustomerDate].[dbo].[FIMARK] as fim on fim.ID_FINANCE = s.ID_FINANCE
join [OldCustomerDate].[dbo].Markaz as m on m.ID = fim.ID_MARKAZ
join [CRM].[dbo].Center as Ce on Ce.CenterCode = m.ID
join Telephone as T on T.TelephoneNoIndividual = SUBSTRING(s.TEL_PISH , 3 ,5) + S.TEL
join [OldData].dbo.BEASAT_VORODI  as  v on T.TelephoneNoIndividual = v.TEL_NO  and v.KAFO = @kafo
where C.KerStopDate = '99999999' and S.STOP_DATE = '99999999' and T.CenterID = Ce.ID




--select * from OldCustomerDate.dbo.Subscrib
 
