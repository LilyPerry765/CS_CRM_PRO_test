USE [CRM]
GO

DELETE CRM.dbo.Center
DBCC CHECKIDENT('CRM.dbo.Center', RESEED,0)

INSERT INTO [dbo].[Center]
           ([RegionID]
           ,[CenterCode]
           ,[BillingCode]
           ,[CenterName]
           ,[SubsidiaryCodeTelephone]
           ,[SubsidiaryCodeService]
           ,[SubsidiaryCodeADSL]
           ,[Telephone]
           ,[Address]
           ,[IsActive]
           ,[InsertDate]
           ,[ModifyDate]
           ,[Elka_CI_CODE]
           ,[Type]
           ,[Latitude]
           ,[Longitude])
SELECT
  RE.ID ,
  ECEN.CEN_CODE  ,
  null,
  ECEN.CEN_NAME ,
  null ,
  null ,
  null ,
  null ,
  null ,
  1 ,
  '11-26-2014' ,
  '11-26-2014', 
  ECEN.CEN_CODE  ,
  1 ,
  null ,
  null
  FROM [ORACLECRM]..[TT].[CENTER] as ECEN join [ORACLECRM]..[TT].[CITY] as ECI on ECEN.CI_CODE = ECI.CI_CODE
  join CITY as CI on CI.Code =  ECI.CI_CODE
  join Region as RE on RE.CITYID = CI.ID

 --select * from center

  

