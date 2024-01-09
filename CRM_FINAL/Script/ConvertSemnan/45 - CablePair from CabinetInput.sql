USE [CRM]
GO

DELETE CRM.dbo.[CablePair]
DBCC CHECKIDENT('CRM.dbo.[CablePair]', RESEED,0)

INSERT INTO [dbo].[CablePair]
           ([CableID]
           ,[CabinetInputID]
           ,[CablePairNumber]
           ,[Status]
           ,[InsertDate]
           ,[ElkaID])
SELECT 
       Ca.ID
      ,CI.ID
      ,CI.InputNumber
      ,1
      ,'11-29-2014'
      ,CI.[ElkaID]
  FROM [dbo].[CabinetInput] as CI join Cable as Ca on Ca.ElkaID = CI.CabinetID


GO


