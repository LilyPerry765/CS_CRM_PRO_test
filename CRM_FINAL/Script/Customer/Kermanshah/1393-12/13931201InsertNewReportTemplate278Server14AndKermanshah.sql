﻿--13931201 - 1231
--رکورد گزارش لیست کافوهای پر شده در سیستم خودم
--INSERT INTO ReportTemplate
--(ID,Title,Category,Template,IconName,UserControlName)
--VALUES
--(278,N'لیست کافو های پر شده',N'کافوهاي مرکز',NULL,'Report2.png','FilledCabinetReport')

--13931201 - 1727
--وارد کردن رکورد گزارش لیست کافو پر شده بر 
--سرور 14
--INSERT INTO [192.168.0.14\PENDARSQL].CRM.DBO.ReportTemplate
--(ID,Title,Category,Template,IconName,UserControlName)
--SELECT 
--	ID,Title,Category,Template,IconName,UserControlName
--FROM 
--	ReportTemplate RM
--WHERE 
--	RM.ID = 278

--13931201 - 1733
--سرور کرمانشاه
--INSERT INTO [78.39.252.109].CRM.DBO.ReportTemplate
--(ID,Title,Category,Template,IconName,UserControlName)
--SELECT 
--	ID,Title,Category,Template,IconName,UserControlName
--FROM 
--	ReportTemplate RM
--WHERE 
--	RM.ID = 278