USE [CRM]

DELETE dbo.SwitchPort
DBCC CHECKIDENT ('dbo.SwitchPort',RESEED,0)

DELETE dbo.SwitchPrecode
DBCC CHECKIDENT ('dbo.SwitchPrecode',RESEED,0)

DELETE Switch
DBCC CHECKIDENT ('Switch',RESEED,0)

DELETE SwitchType
DBCC CHECKIDENT ('SwitchType',RESEED,0)



INSERT INTO [dbo].[SwitchType]
      ( [ID]
      ,[CommercialName]
      ,[SwitchType]
      ,[IsDigital]
      ,[Capacity]
      ,[SpecialServiceCapacity]
      ,[SupportPublicNo])
 SELECT         ID , Name , 0 , 1 , 0 , 0 , 1
FROM            [abonman.semnan].dbo.[SwitchType]-- Abonman.Semnan.dbo.SwitchType 
GO


