--13931209 - 1449
--رگورد گزارش گواهی استرداد ودیعه تلفن ثابت بر روی سیستم خودم وارد شد
--INSERT INTO ReportTemplate 
--(ID,Title,Category,Template,IconName,UserControlName)
--VALUES 
--(281,N'گواهی استرداد ودیعه تلفن ثابت',N'گواهی',NULL,'Report2.png','RefundDepositCertificateReport')


--13931209 - 1451
--بر روی سرور 14 هم وارد شد
--INSERT INTO [192.168.0.14\PENDARSQL].CRM.DBO.ReportTemplate 
--(ID,Title,Category,Template,IconName,UserControlName)
--VALUES 
--(281,N'گواهی استرداد ودیعه تلفن ثابت',N'گواهی',NULL,'Report2.png','RefundDepositCertificateReport')

--UPDATE ReportTemplate 
--SET Template = (SELECT Template FROM ReportTemplate WHERE ID  = 192)
--WHERE ID = 281

--13931209 - 1708
--گزارش گواهی استراداد ودیعه تلفن ثابت بر روی سیستم خودم نهایی شد حالا باید بروی سرور 14 هم بروزرسانی شود
--UPDATE R14
--SET TEMPLATE = RM.Template
--FROM
--	ReportTemplate RM 
--INNER JOIN 
--	[192.168.0.14\PENDARSQL] .CRM.DBO.ReportTemplate R14 ON RM.ID = R14.ID
--WHERE 
--	RM.ID = 281 AND R14.ID = 281
	
--13931209 - 1721
--بر روی سرور کرمانشاه هم وارد شد
--INSERT INTO [78.39.252.109].CRM.DBO.REPORTTEMPLATE
--(ID,Title,Category,UserControlName,IconName,Template)
--SELECT 
--	ID,Title,Category,UserControlName,IconName,Template
--FROM 
--	ReportTemplate
--WHERE 
--	ID = 281