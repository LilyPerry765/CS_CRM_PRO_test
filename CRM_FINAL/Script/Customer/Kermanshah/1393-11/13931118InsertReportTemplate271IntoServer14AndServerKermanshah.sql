--13931118 - 15:28
--گزارش آمار تلفن های مشغول به کار بر اساس سوئیچ در سیستم خودم نهایی شد حالا باید بر روی سرور 14 هم وارد شد
--INSERT INTO [192.168.0.14\pendarsql].CRM.dbo.ReportTemplate
--SELECT 
--	*
--FROM 
--	ReportTemplate RM
--WHERE 
--	RM.ID = 271

--13931118 = 15:38
--UPDATE ReportTemplate
--SET UserControlName = 'WorkingTelephoneStatisticsBySwitchTypeReport'
--WHERE ID = 271

--UPDATE [192.168.0.14\pendarsql].CRM.dbo.ReportTemplate
--SET UserControlName = 'WorkingTelephoneStatisticsBySwitchTypeReport'
--WHERE ID = 271


--UPDATE ReportTemplate
--SET Title = N'آمار تلفن های مشغول به کار بر اساس نوع سوئیچ'
--WHERE ID = 271


--13931118 - 15:56
--الگوی گزارش 271 بر روی سیستم خودم نهایی شد حالا باید بر روی سرور 14 هم بروزرسانی شود
--UPDATE R14
--SET Template = RM.Template
--FROM 
--	ReportTemplate RM
--INNER JOIN 
--	[192.168.0.14\PENDARSQL].CRM.DBO.ReportTemplate R14 ON Rm.ID = R14.ID 
--WHERE 
--	RM.ID = 271 AND R14.ID = 271

--13931118 - 16:11
--گزارش آمار تلفن های مشغول بکار بر اساس نوع سوئیچ بر روی سرور 14 هم نهایی شد حالا باید بر روی سرور کرمانشاه هم وارد شود
--INSERT INTO [78.39.252.109].CRM.DBO.ReportTemplate
--SELECT 
--	*
--FROM 
--	ReportTemplate R14
--WHERE 
--	R14.ID = 271