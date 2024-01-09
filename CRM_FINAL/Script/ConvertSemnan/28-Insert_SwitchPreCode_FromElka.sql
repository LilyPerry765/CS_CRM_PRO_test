USE [CRM]
GO

DELETE [dbo].[SwitchPrecode]
DBCC CHECKIDENT('SwitchPrecode', RESEED,0)

INSERT INTO [dbo].[SwitchPrecode]
           ([CenterID]
           ,[SwitchID]
           ,[SwitchPreNo]
           ,[PreCodeType]
           ,[Capacity]
           ,[OperationalCapacity]
           ,[InstallCapacity]
           ,[SpecialServiceCapacity]
           ,[DeploymentType]
           ,[LinkType]
           ,[DorshoalNumberType]
           ,[Status]
           ,[FromNumber]
           ,[ToNumber]
		   ,ElkaID )
 SELECT 
       convert(INT,(select ID from Center where CenterCode = [PC].[CEN_CODE]))
      , convert(INT,(select ID from dbo.Switch where ElkaID = [PC].[SALON_SWITCH_ID])) 
	  ,[PRE_CODE]
	  ,IIF ( [PC].[TEL_TYPE] = 1, 2, 1 ) 
	  ,(SELECT [PRE_END]-[PRE_START]+1)
	  ,(SELECT [PRE_END]-[PRE_START]+1)
      ,(SELECT [PRE_END]-[PRE_START]+1)
	  ,0
	  ,0
	  ,NULL
      ,NULL 
	  ,0   
      ,[PRE_START]
      ,[PRE_END]
	  ,[PREFIX_ID]
  FROM [ORACLECRM]..[TT].[PREFIX_CENTER] AS PC

GO


