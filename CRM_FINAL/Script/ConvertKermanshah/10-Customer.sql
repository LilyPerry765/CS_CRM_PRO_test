USE [CRM]
GO


-- اصلاح شود فاینسس تکراری را وارد نمی کند
--delete Customer
--DBCC checkident('Customer', reseed , 0)

Declare @CityId int=7

delete  customer where kercity=@CityId


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
		   ,KerStopDate
		   ,KerStartDate
		   ,kerID
		   ,kercity)
SELECT 
      null
	  -- REPLACE(CONVERT(CHAR(15) ,ABS([ID_FINANCE])),' ','0')
	  ,IIF(ID_TYPE  = 1 , 0 , 1) 
	  ,null
	  ,[NATION_COD]
      ,IIF(ID_TYPE  = 1 , [NAME] , [FAMILY])
      ,IIF(ID_TYPE  = 1 , [FAMILY] , null)
      ,[FATHER]
	  ,null
      ,[CERTIFI_NO]
	  , null
      -- ,IIF([DATE_BIRTH] <> 99999999 , [CRM].[dbo].[sh2mi](STUFF(STUFF([DATE_BIRTH] , 5 ,0,'/') , 8 , 0 ,'/')) , null )
      ,[CERT_PLACE]
      ,null
      ,null
      ,null
      ,null
      ,null
      , CONVERT( smalldatetime , '1900-01-01'  )
      --,IIF([START_DATE] <> 99999999 ,IIF ([dbo].[sh2mi](STUFF(STUFF([START_DATE] , 5 ,0,'/') , 8 , 0 ,'/')) is null ,CONVERT( DATETIME , '1/1/1753 12:00:00 AM'  ) , [dbo].[sh2mi](STUFF(STUFF([START_DATE] , 5 ,0,'/') , 8 , 0 ,'/'))), CONVERT( DATETIME , '1/1/1753 12:00:00 AM'  )) as InsertDate
	  ,[ID_FINANCE]
      ,[STOP_DATE]
	  ,[START_DATE]
	  ,[salas].[dbo].[PERSONAL].ID_FOLD
	  ,@CityId
  FROM [salas].[dbo].[PERSONAL]
GO


