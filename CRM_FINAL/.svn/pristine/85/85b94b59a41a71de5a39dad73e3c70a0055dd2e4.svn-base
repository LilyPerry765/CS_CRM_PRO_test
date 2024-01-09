USE [CRM]
GO

declare @cityid int = 5

INSERT INTO [dbo].[Customer]
           ([CustomerID]
           ,[PersonType]
           ,[NationalID]
           ,[NationalCodeOrRecordNo]
           ,[FirstNameOrTitle]
           ,[LastName]
           ,[FatherName]
           ,[Gender]
           ,[BirthCertificateID]
           ,[BirthDateOrRecordDate]
           ,[IssuePlace]
           ,[UrgentTelNo]
           ,[MobileNo]
           ,[Email]
           ,[Agency]
           ,[AgencyNumber]
           ,[InsertDate]
           ,[ElkaID]
           ,[UserName]
           ,[Password]
           ,[KerStopDate]
           ,[KerStartDate]
           ,[KerID]
           ,[kercity])
select 
           null
           ,1
           ,null
           ,null
           ,s.NAME
           ,null
           ,null
           ,null
           ,null
           ,null
           ,null
           ,null
           ,null
           ,null
           ,null
           ,null
           ,11/23/2014
           ,s.TEL_NO
           ,null
           ,null
           ,null
           ,null
           ,null
           ,@cityid
		   from [Sarpol].[dbo].[SIM] as s


update t set t.CustomerID = c.ID FROM [dbo].[Customer] as c 
join Telephone as t on c.ElkaID = t.TelephoneNoIndividual 
join center on center.id=t.centerid
join region on center.regionid=region.id
join city on city.id=region.cityid
join [Sarpol].[dbo].[SIM] as a on a.TEL_NO = c.ElkaID and city.id=@cityid
GO


