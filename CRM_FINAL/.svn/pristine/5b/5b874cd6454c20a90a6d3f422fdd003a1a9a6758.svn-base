Declare @Cityid int;set @Cityid=7
----دائمی
update ir set ir.PosessionType=0
 from [CRM].dbo.InstallRequest as IR join [CRM].dbo.Request as R on IR.RequestID = R.ID INNER JOIN
                     [CRM].dbo.Center ON R.CenterID = Center.ID INNER JOIN
                     [CRM].dbo.Region ON Center.RegionID = Region.ID INNER JOIN
                     [CRM].dbo.City ON Region.CityID = City.ID					
					 where  City.ID = @Cityid and R.RequestTypeID = 1 and R.CreatorUserID is null

------موقت
update ir set ir.PosessionType=1
 from [CRM].dbo.InstallRequest as IR join [CRM].dbo.Request as R on IR.RequestID = R.ID INNER JOIN
                     [CRM].dbo.Center ON R.CenterID = Center.ID INNER JOIN
                     [CRM].dbo.Region ON Center.RegionID = Region.ID INNER JOIN
                     [CRM].dbo.City ON Region.CityID = City.ID					
					 join Salas.[dbo].[SUBSCRIB] s on s.newtelephone=r.telephoneno
					 JOIN Salas.[dbo].[FINANCE] f on f.id=s.ID_FINANCE
					 where  City.ID = @Cityid and R.RequestTypeID = 1 and R.CreatorUserID is null  and f.ID_SABT=8 




  

