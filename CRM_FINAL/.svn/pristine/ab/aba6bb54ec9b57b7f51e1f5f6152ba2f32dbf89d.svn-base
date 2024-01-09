use crm
go
declare @cityid int ; set @cityid =7
update T
set T.InstallAddressID = A.ID ,
 T.CorrespondenceAddressID = A.ID
 from [CRM].[dbo].[Address] AS A
join [salas].[dbo].[Subscrib] as S on  A.ElkaID = S.ID_FINANCE 
join [salas].[dbo].[FIMARK] as fim on fim.ID_FINANCE = s.ID_FINANCE
join [salas].[dbo].Markaz as m on m.ID = fim.ID_MARKAZ
join [CRM].[dbo].Center as C on C.CenterCode = m.ID
join region on c.regionid=region.id
join city on city.id=region.cityid
join Telephone as T on T.TelephoneNoIndividual = SUBSTRING(s.TEL_PISH , 3 ,5) + S.TEL
where s.STOP_DATE = '99999999' and A.KerStopDate = '99999999' and T.CenterID = C.ID and  city.id=@cityid


--select * from OldCustomerDate.dbo.Subscrib