USE [CRM]
GO

DELETE [dbo].Customer
DBCC CHECKIDENT('Customer', RESEED,0)

INSERT INTO [dbo].[Customer]
           ([CustomerID]
           ,[PersonType]
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
           ,[InsertDate]
		   ,[ElkaID]
		   )
SELECT 
       REPLACE(CONVERT(CHAR(15) ,ABS([FI_CODE])),' ','0')
      ,0
	  ,[HA_CU_CODE_MELLI]
      ,[HA_CU_NAME]
      ,[HA_CU_LASTNAME]
	  ,[HA_CU_FATHER_NAME]
	  ,CASE WHEN   [SEX] = 1 THEN 0 WHEN [SEX] = 2 THEN 1 END 
	  ,[HA_CU_SHENASNAME]
	  --,CONVERT(smalldatetime , CRM.dbo.[sh2mi]([HA_CU_BIRTHDAY]))
	  ,NULL
      ,[SODUR_PLACE]
	  ,[HA_TELEPHONE]
	  ,NULL
      ,[HA_EMAIL]
	  ,CONVERT(SMALLDATETIME , CRM.dbo.serverdate())
	  ,[FI_CODE]
  FROM [ORACLECRM]..[SCOTT].[HAGHIGHI_CUSTOMER]

  
INSERT INTO [dbo].[Customer]
           (
		    [CustomerID]
           ,[PersonType]
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
		   )
SELECT 
       REPLACE(CONVERT(CHAR(15) ,ABS([FI_CODE])),' ','0')
	   ,1
	   ,[HO_CU_SABT]
      ,[HO_CU_NAME]
      ,NULL
      ,NULL
	  ,NULL
      ,NULL 
	 -- ,CONVERT(smalldatetime , CRM.dbo.[sh2mi]([HO_CU_DATE])) 
	 ,NULL
	   ,NULL 
	  ,[HO_TELEPHONE]
	   ,NULL 
	   ,[HO_EMAIL]
     ,[HO_CU_NOMAYANDEGI]
     , [HO_CU_NUM_NOMAYANDEGI]
	  ,CONVERT(SMALLDATETIME , CRM.dbo.serverdate())
	  ,[FI_CODE]
  FROM [ORACLECRM]..[SCOTT].[HOOGHOOGHI_CUSTOMER]

GO

 --SELECT 
 --     COUNT(*)
 -- FROM [ORACLECRM]..[SCOTT].[HAGHIGHI_CUSTOMER]
GO


