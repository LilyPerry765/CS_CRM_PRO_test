USE [CRM]
GO

DELETE [dbo].[SwitchType]
DBCC CHECKIDENT('SwitchType', RESEED,0)

INSERT INTO [dbo].[SwitchType]
           ([CommercialName]
           ,[SwitchTypeValue]
           ,[IsDigital]
           ,[Capacity]
           ,[OperationalCapacity]
           ,[InstallCapacity]
           ,[SpecialServiceCapacity]
           ,[CounterDigitCount]
           ,[SupportPublicNo]
           ,[PublicCapacity]
           ,[TrafficTypeCode]
		   ,ElkaID)
SELECT [SWITCH_TYPE_NAME] 
           ,0
           ,1
           ,0
           ,0
           ,0
           ,0
           ,0
           ,1
           ,0
           ,1
      ,[SWITCH_TYPE_ID]
  FROM [ORACLECRM]..[TT].[SWITCH_TYPE]
GO


