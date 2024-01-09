

USE [CRM]
GO
--ALTER DATABASE [CRM]
--	SET SINGLE_USER WITH ROLLBACK IMMEDIATE
--GO

DELETE CRM.dbo.CabinetInput
DBCC CHECKIDENT('CRM.dbo.CabinetInput', RESEED,0)



INSERT INTO [dbo].[CabinetInput]
           ([CabinetID]
           ,[InputNumber]
           ,[InsertDate]
           ,[Status]
           ,[Direction]
		   ,ElkaID)
  SELECT 
       c.ID
      ,ki.[KAFU_INPUT]
	 -- ,CRM.dbo.[sh2miByTime]([KI_DATE] , [KI_HOUR])
	 ,'11-26-2014'
      ,case when ki.[STATUS] = 1 then 1 -- خالی
	          when ki.[STATUS] = 2 then 1 -- پر
 			  when ki.[STATUS] = 3 then 0 -- خراب
			  when ki.[STATUS] = 4 then 1 --رزرو
			  when ki.[STATUS] = 5 then 1 -- در حال تعويض بوخت
			  when ki.[STATUS] = 6 then 1 -- پی سی ام داخل کافو
			  when ki.[STATUS] = 7 then 1 
			  else 1			  
	    END -- برگردان مرکز به مرکز
      ,NULL
      ,ki.[INPUT_ID]    
  FROM [ORACLECRM]..[TT].[KAFU_INPUT] as ki join CRM.dbo.Cabinet as c on  ki.KAFU_ID = c.ElkaID 
GO

--ALTER DATABASE [CRM]
--	SET MULTI_USER
--GO



