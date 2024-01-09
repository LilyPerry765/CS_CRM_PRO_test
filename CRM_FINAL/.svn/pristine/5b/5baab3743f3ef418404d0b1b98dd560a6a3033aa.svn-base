USE [CRM]
GO
DELETE dbo.[Cable]
DBCC CHECKIDENT('Cable', RESEED,0)

INSERT INTO [dbo].[Cable]
           (
		    [CenterID]
           ,[CableNumber]
           ,[CableTypeID]
           ,[CableUsedChannelID]
           ,[CableDiameter]
           ,[PhysicalType]
           ,[FromCablePairNumber]
           ,[ToCablePairNumber]
           ,[Status]
           ,[InsertDate]
		   ,ElkaID)
SELECT 
     convert(INT,(select id from CRM.dbo.Center where CenterCode = [c].[CEN_CODE]))
      ,[c].[CABLE_NUM]
	  ,1 
	  ,[c].[CU_TYPE_ID]
	  ,[c].[DIM]
	  ,[c].[CP_TYPE_ID]
      ,1
      ,[c].[C_COUNT]
	  ,0
	  ,CRM.dbo.serverdate()
	  ,[c].[CABLE_ID]
  FROM [ORACLECRM]..[SCOTT].[CABLE] as c
GO