--13931207 - 1039
--بر روی سیستم خودم
--UPDATE ReportTemplate 
--SET UserControlName = 'ChangeLocationCenterToCenterCertificateReport',
--	Category = N'گواهی',
--	Title=N'گواهی تغییر مکان  مرکز به مرکز'
--WHERE ID = 138

--13931207 - 1402
--گزارش گواهی تغییر مکان مرکز به مرکز بر روی سیستم خودم نهایی شد
--حالا باید بر روی سرور 14 بروزرسانی شود
--UPDATE R14
--SET TEMPLATE = RM.Template,
--	TITLE = RM.Title,
--	USERCONTROLNAME = RM.UserControlName,
--	CATEGORY= RM.Category
--FROM 
--	ReportTemplate RM
--INNER JOIN 
--	[192.168.0.14\PENDARSQL].CRM.DBO.ReportTemplate R14 ON RM.ID = R14.ID
--WHERE 
--	RM.ID = 138 AND R14.ID = 138


--13931207  - 1409
--بر روی کرمانشاه هم بروزرسانی شد
UPDATE RR
SET Template = R14.Template,
	Title = R14.Title,
	UserControlName = R14.UserControlName,
	Category = R14.Category
FROM
	ReportTemplate R14
INNER JOIN 
	[78.39.252.109].CRM.DBO.REPORTTEMPLATE RR ON R14.ID = RR.ID
WHERE 
	RR.ID = 138 AND R14.ID = 138