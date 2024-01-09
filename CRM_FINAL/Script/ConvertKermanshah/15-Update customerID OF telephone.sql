use crm
go
declare @cityid int ; set @cityid =7
update T
set T.CustomerID = C.ID
 from [CRM].[dbo].Customer as C
join [salas].[dbo].[Subscrib] as S on  S.ID_FINANCE = C.ElkaID 
join [salas].[dbo].[FIMARK] as fim on fim.ID_FINANCE = s.ID_FINANCE
join [salas].[dbo].Markaz as m on m.ID = fim.ID_MARKAZ
join [CRM].[dbo].Center as Ce on Ce.CenterCode = m.ID
join region on ce.regionid=region.id
join city on city.id=region.cityid
join Telephone as T on T.TelephoneNoIndividual = SUBSTRING(s.TEL_PISH , 3 ,5) + S.TEL
where C.KerStopDate = '99999999' and S.STOP_DATE = '99999999' and T.CenterID = Ce.ID and  city.id=@cityid

