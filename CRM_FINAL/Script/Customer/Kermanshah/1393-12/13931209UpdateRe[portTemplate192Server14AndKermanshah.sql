SELECT * FROM ReportTemplate where id = 192


--UPDATE ReportTemplate
--SET UserControlName = 'ChangeNoCertificateReport',
--	Title = N'گواهی تعویض شماره',
--	Category=N'گواهی'
--WHERE ID = 192


--13931209 - 1251
--گزارش گواهی تعویض شماره بر روی سیستم خودم نهایی شد حالا باید بر روی سرور 14 هم بروزرسانی شود
--UPDATE R14
--SET TEMPLATE = RM.Template,
--	TITLE = RM.Title,
--	USERCONTROLNAME = RM.UserControlName,
--	CATEGORY = RM.Category
--FROM 
--	ReportTemplate RM 
--INNER JOIN 
--	[192.168.0.14\PENDARSQL].CRM.DBO.REPORTTEMPLATE R14 ON RM.ID = R14.ID 
--WHERE 
--	R14.ID = 192 AND RM.ID = 192

--13931209 - 1257
-- بر روی سرور کرمانشاه هم بروزرسانی شد
--UPDATE RR
--SET Template = R14.Template,
--	Title = R14.Title,
--	UserControlName=R14.UserControlName,
--	Category = R14.Category
--FROM 
--	ReportTemplate R14
--INNER JOIN 
--	[78.39.252.109].CRM.DBO.REPORTTEMPLATE RR ON R14.ID = RR.ID 
--WHERE 
--	RR.ID = 192 AND R14.ID = 192