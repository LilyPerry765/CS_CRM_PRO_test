select top 1 InstallRequest.*
 from(
 select * from Request where TelephoneNo in ( select  TelephoneNo from RequestLog where TelephoneNo = tel.TelephoneNo )
 union
 select * from Request where TelephoneNo in ( select  TelephoneNo from RequestLog where ToTelephoneNo = tel.TelephoneNo )
 union
 select * from Request where TelephoneNo  = tel.TelephoneNo ) as T 
 join InstallRequest on T.ID = InstallRequest.RequestID
 Order by  InstallRequest.InstallationDate DESC