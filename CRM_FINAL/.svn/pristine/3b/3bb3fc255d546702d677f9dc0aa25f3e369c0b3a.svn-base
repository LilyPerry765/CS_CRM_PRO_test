

--UPDATE ReportTemplate
--SET UserControlName = 'ChangeAddressCertificateReport',
--	Title = N'گواهی اصلاح آدرس',
--	Category = N'گواهی'
--WHERE 
--	ID  = 203

--UPDATE ReportTemplate
--SET Template = (SELECT Template FROM ReportTemplate WHERE ID  = 192)
--WHERE ID = 203
	

--13931210 - 1800
--گزارش گواهی اصلاح آدرس بر روی سیستم خودم نهایی شد حالا باید بر روی سرور 14 هم بروزرسانی شد
--UPDATE R14
--SET TEMPLATE =RM.Template,
--	TITLE=RM.Title,
--	CATEGORY=RM.Category,
--	USERCONTROLNAME=RM.UserControlName
--FROM 
--	ReportTemplate RM
--INNER JOIN 
--	[192.168.0.14\pendarsql].CRM.dbo.ReportTemplate R14 ON RM.ID = R14.ID 
--WHERE 
--	RM.ID = 203 AND R14.ID = 203

--139311210 - 1814
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
--	R14.ID = 203 AND RR.ID = 203