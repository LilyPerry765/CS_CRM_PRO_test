USE [CRM]
GO

DELETE CRM.dbo.Cable
DBCC CHECKIDENT('CRM.dbo.Cable', RESEED,0)

INSERT INTO [dbo].[Cable]
           ([CenterID]
           ,[CableNumber]
           ,[CableTypeID]
           ,[CableUsedChannelID]
           ,[CableDiameter]
           ,[PhysicalType]
           ,[FromCablePairNumber]
           ,[ToCablePairNumber]
           ,[Status]
           ,[InsertDate]
           ,[CabinetIDInVirtualCable]
           ,[ElkaID])

SELECT C.[CenterID]
      ,C.[CabinetNumber]
      ,1
      ,1
      ,0
      ,1
      ,C.[FromInputNo]
      ,C.[ToInputNo]
      ,1
      ,'11-29-2014'
      ,null
      ,C.ID
  FROM [dbo].[Cabinet] as C


