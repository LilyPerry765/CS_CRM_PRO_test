

--13931208 - 1540
--گزارش گواهی تغییر نام و مکان مرکز به مرکز بر روی سسیستم خودم
--UPDATE ReportTemplate
--SET Title = N'گواهی تغییر مکان و نام مرکز به مرکز',
--	Category=N'گواهی',
--	UserControlName='ChangeLocationAndNameCenterToCenterCertificateReport'
--WHERE ID = 139 

--13931208 - 1625
--گواهی تغییر نام و مکان مرکز به مرکز بروی سیستم خودم نهایی شد حالا باید بر روی سرور 14 هم بروزرسانی شود
--UPDATE R14
--SET TEMPLATE = RM.Template,
--	USERCONTROLNAME = RM.UserControlName,
--	TITLE= RM.Title,
--	CATEGORY = RM.Category
--FROM 
--	ReportTemplate RM
--INNER JOIN 
--	[192.168.0.14\pendarsql].CRM.dbo.ReportTemplate R14 ON RM.ID = R14.Id
--WHERE 
--	RM.ID = 139 AND R14.ID = 139

--13931208 - 1635
--گزارش گواهی تغییر نام و مکان مرکز به مرکز بر روی سرور کرمانشاه بروز رسانی شد
--UPDATE RR
--SET Template = R14.Template,
--	Title = R14.Title,
--	UserControlName=RR.UserControlName,
--	Category = R14.Category
--FROM 
--	ReportTemplate R14 
--INNER JOIN 
--	[78.39.252.109].CRM.DBO.REPORTTEMPLATE RR ON R14.ID = RR.ID 
--WHERE 
--	RR.ID = 139 AND R14.ID = 139