use crm
go

DECLARE @CityID INT=7

update Tel set Tel.CustomerTypeID = S.CatExtCode, Tel.CustomerGroupID = S.SubCatExtCode
 from OldCustomerDate.DBO.Sheet1$ as S 
join CRM.dbo.Telephone as Tel on  S.strFPN = Tel.TelephoneNo
join Center as c on c.id = tel.centerid
join Region as r on r.ID=c.RegionID
join city on city.id=r.CityID
where tel.Status in (2,3) and City.id=@CityID


Update IR2 set IR2.TelephoneType = S.CatExtCode, IR2.TelephoneTypeGroup = S.SubCatExtCode from
--Update S set S.NoInstallRequest = 0
(
select * from 
(
select  R.TelephoneNo , IR.* , ROW_NUMBER() OVER(PARTITION BY R.TelephoneNo ORDER BY R.EndDate DESC) AS Row  
from CRM.dbo.Request as R 
join CRM.dbo.InstallRequest as IR ON R.ID = IR.RequestID
where R.TelephoneNo is not null and R.TelephoneNo != 0
) as T where T.Row = 1
) as T2 join CRM.dbo.Telephone as Tel on Tel.TelephoneNo = T2.TelephoneNo 
join OldCustomerDate.DBO.Sheet1$ as S on S.strFPN = Tel.TelephoneNo
join CRM.dbo.InstallRequest as  IR2 on T2.ID = IR2.ID
join Center as c on c.id = tel.centerid
join Region as r on r.ID=c.RegionID
join city on city.id=r.CityID
where City.id=@CityID


Update S set S.NoInstallRequest = 0 from
(
select * from 
(
select  R.TelephoneNo , IR.* , ROW_NUMBER() OVER(PARTITION BY R.TelephoneNo ORDER BY R.EndDate DESC) AS Row  
from CRM.dbo.Request as R 
join CRM.dbo.InstallRequest as IR ON R.ID = IR.RequestID
where R.TelephoneNo is not null and R.TelephoneNo != 0
) as T where T.Row = 1
) as T2 join CRM.dbo.Telephone as Tel on Tel.TelephoneNo = T2.TelephoneNo 
join OldCustomerDate.DBO.Sheet1$ as S on S.strFPN = Tel.TelephoneNo
join CRM.dbo.InstallRequest as  IR2 on T2.ID = IR2.ID
join Center as c on c.id = tel.centerid
join Region as r on r.ID=c.RegionID
join city on city.id=r.CityID
where City.id=@CityID

--Update Tel set Tel.CustomerTypeID = S.CatExtCode, Tel.CustomerGroupID = S.SubCatExtCode from
--(
--select * from 
--(
--select  R.TelephoneNo , IR.* , ROW_NUMBER() OVER(PARTITION BY R.TelephoneNo ORDER BY R.EndDate DESC) AS Row  
--from CRM.dbo.Request as R 
--join CRM.dbo.InstallRequest as IR ON R.ID = IR.RequestID
--where R.TelephoneNo is not null and R.TelephoneNo != 0
--) as T where T.Row = 1
--) as T2 join CRM.dbo.Telephone as Tel on Tel.TelephoneNo = T2.TelephoneNo 
--join OldCustomerDate.DBO.Sheet1$ as S on S.strFPN = Tel.TelephoneNo
--join CRM.dbo.InstallRequest as  IR2 on T2.ID = IR2.ID


update S set OldTelephone = Rl.TelephoneNo from OldCustomerDate.DBO.Sheet1$ as S 
join CRM.DBO.RequestLog as RL on S.strFPN = RL.ToTelephoneNo
join Telephone as tel on tel.TelephoneNo=rl.ToTelephoneNo
join Center as c on c.id = tel.centerid
join Region as r on r.ID=c.RegionID
join city on city.id=r.CityID
where  City.id=@CityID
and S.NoInstallRequest is null




Update IR2 set IR2.TelephoneType = S.CatExtCode, IR2.TelephoneTypeGroup = S.SubCatExtCode
--Update S set S.NoInstallRequest = 2
from
(
select * from 
(
select  R.TelephoneNo , IR.* , ROW_NUMBER() OVER(PARTITION BY R.TelephoneNo ORDER BY R.EndDate DESC) AS Row  
from CRM.dbo.Request as R 
join CRM.dbo.InstallRequest as IR ON R.ID = IR.RequestID
where R.TelephoneNo is not null and R.TelephoneNo != 0
) as T where T.Row = 1
) as T2 join CRM.dbo.Telephone as Tel on Tel.TelephoneNo = T2.TelephoneNo 
join OldCustomerDate.DBO.Sheet1$ as S on S.OldTelephone = Tel.TelephoneNo
join CRM.dbo.InstallRequest as  IR2 on T2.ID = IR2.ID
join Center as c on c.id = tel.centerid
join Region as r on r.ID=c.RegionID
join city on city.id=r.CityID
where  City.id=@CityID
and S.NoInstallRequest is null




Update S set S.NoInstallRequest = 2
from
(
select * from 
(
select  R.TelephoneNo , IR.* , ROW_NUMBER() OVER(PARTITION BY R.TelephoneNo ORDER BY R.EndDate DESC) AS Row  
from CRM.dbo.Request as R 
join CRM.dbo.InstallRequest as IR ON R.ID = IR.RequestID
where R.TelephoneNo is not null and R.TelephoneNo != 0
) as T where T.Row = 1
) as T2 join CRM.dbo.Telephone as Tel on Tel.TelephoneNo = T2.TelephoneNo 
join OldCustomerDate.DBO.Sheet1$ as S on S.OldTelephone = Tel.TelephoneNo
join CRM.dbo.InstallRequest as  IR2 on T2.ID = IR2.ID
join Center as c on c.id = tel.centerid
join Region as r on r.ID=c.RegionID
join city on city.id=r.CityID
where  City.id=@CityID
and S.NoInstallRequest is null















