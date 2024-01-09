--13931020 - 16:41
--گزارش آمار اتصالی - کل بر روی سیستم خودم نهایی شد حالا باید بر روی سرور 14 هم بروزرسانی شود
--UPDATE R14
--SET TEMPLATE = RM.Template
--FROM 
--	ReportTemplate RM
--INNER JOIN 
--	[192.168.0.14\pendarsql].CRM.dbo.ReportTemplate R14 ON RM.ID = R14.ID
--WHERE 
--	RM.ID = 82 AND R14.ID = 82

--13931020 - 18:48
--بر روی سرور کرمانشاه هم بروزرسانی شد
UPDATE RR
SET Template = R14.Template
FROM 
	ReportTemplate R14
INNER JOIN 
	[78.39.252.109].CRM.dbo.ReportTemplate RR ON RR.ID = R14.ID
WHERE 
	R14.ID = 82 AND RR.ID = 82