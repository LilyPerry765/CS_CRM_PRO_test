
USE [CRM]
GO
--DELETE dbo.ADSLSellerAgent
--DELETE dbo.OfficeEmployee
--DELETE dbo.Office
--DELETE dbo.PAPInfoLimitation
--DELETE dbo.Failure117PostAccuracy
--DELETE dbo.ADSLPAPPort
--DELETE dbo.ADSLMDFRange
--DELETE dbo.MDF
--DELETE dbo.ADSLModemProperty
--DELETE dbo.E1Number
--DELETE dbo.E1Position
--DELETE dbo.E1Bay
--DELETE dbo.E1DDF
--DELETE dbo.ADSLPAPCabinetAccuracy
--DELETE dbo.PostGroup
--DELETE dbo.ADSLTelephoneNoHistory
--DELETE dbo.Failure117CabenitAccuracy
--DELETE dbo.PAPInfoSpaceandPower
--DELETE dbo.MDFPersonnel
--DELETE dbo.PCMPort
--DELETE dbo.PCM
--DELETE dbo.PCMShelf
--DELETE dbo.PCMRock
--DELETE dbo.ADSLInstalCostCenter
--DELETE dbo.ADSLTelephoneAccuracy
--DELETE dbo.Failure117TelephoneAccuracy
--DELETE dbo.UserCenter
--DELETE dbo.ADSLEquipment
--DELETE dbo.RoundSaleInfo
--DELETE dbo.Center
--DELETE dbo.Region
DELETE CRM.dbo.City
DBCC CHECKIDENT('CRM.dbo.City', RESEED,0)


INSERT INTO [dbo].[City]
           (
		    [ProvinceID]
           ,[Code]
           ,[Name])
select  25 ,C.CI_CODE , C.CI_NAME  from [ORACLECRM]..[TT].[CITY] as C


insert into region select ID , NAME from City

GO

--select * from [ORACLECRM]..[TT].[CITY] 
--select * from City
--select * from CRM.dbo.Province


