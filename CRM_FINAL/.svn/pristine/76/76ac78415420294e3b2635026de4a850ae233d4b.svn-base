update tel set InitialInstallationDate = InstallRequest.InstallationDate from Telephone as tel join Request on tel.TelephoneNo = Request.TelephoneNo
 join InstallRequest on InstallRequest.RequestID = Request.ID
  where tel.Status = 2 and RequestTypeID = 1

Update tel set  tel.InitialInstallationDate = 
 (select top 1 InstallRequest.InstallationDate
 from(
 select * from Request where TelephoneNo in ( select  TelephoneNo from RequestLog where TelephoneNo = tel.TelephoneNo )
 union
 select * from Request where TelephoneNo in ( select  TelephoneNo from RequestLog where ToTelephoneNo = tel.TelephoneNo )
 union
 select * from Request where TelephoneNo  = tel.TelephoneNo ) as T 
 join InstallRequest on T.ID = InstallRequest.RequestID
 Order by  InstallRequest.InstallationDate DESC)
  from Telephone as tel where Status = 2 and tel.InitialInstallationDate is null