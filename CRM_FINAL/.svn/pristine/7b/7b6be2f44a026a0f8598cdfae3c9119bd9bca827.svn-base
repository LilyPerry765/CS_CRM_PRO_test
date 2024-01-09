USE [CRM]
GO

DELETE [dbo].[Switch]
DBCC CHECKIDENT('Switch', RESEED,0)

INSERT INTO [dbo].[Switch]
           ([CenterID]
           ,[SwitchTypeID]
           ,[FeatureONU]
           ,[WorkUnitResponsible]
           ,[SwitchCode]
           ,[Capacity]
           ,[OperationalCapacity]
           ,[InstallCapacity]
           ,[DataCapacity]
           ,[Status]
           ,[ElkaID])
SELECT convert(INT,(select id from Center where CenterCode = [SS].[CEN_CODE]))
      ,CONVERT(INT,(SELECT ID FROM SwitchType WHERE ElkaID =  [SS].[SWITCH_TYPE_ID]))
	  ,NULL
      ,1  
	  ,SS.[SWITH_NAME]  
      ,SS.[CAPACITY]
      ,0
      ,0
      ,0
      ,0
	  ,SS.[SALON_SWITCH_ID]
  FROM [ORACLECRM]..[TT].[SALON_SWITCH] AS SS
GO


