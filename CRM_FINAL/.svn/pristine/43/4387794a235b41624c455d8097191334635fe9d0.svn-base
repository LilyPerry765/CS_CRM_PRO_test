--13931213 - 1157
--گزارش پست پر در بخش پست های مرکز بر روی سیستم خودم نهایی شد و ستون اتصالی های رزرو به آن اضافه شد حالا باید بروی سرور 14 هم بروزرسانی شود
UPDATE R14
SET TEMPLATE = RM.Template
FROM 
	ReportTemplate RM
INNER JOIN 
	[192.168.0.14\PENDARSQL].CRM.DBO.REPORTTEMPLATE R14 ON R14.ID = RM.ID
WHERE 
	RM.ID = 78 AND R14.ID = 78

--13931213 - 1207
UPDATE RR
SET Template = R14.Template
FROM
	ReportTemplate R14
INNER JOIN 
	[78.39.252.109].CRM.DBO.REPORTTEMPLATE RR ON R14.ID = RR.ID
WHERE 
	R14.ID = 78 AND RR.ID = 78