﻿--13931020 - 10:33
--گزارش تلفن مشغول به کار بر اساس کافو بر روی سیستم خودم نهایی شد پش حالا باید بر روی سرور 14 هم بروزرسانی شود
--UPDATE R14
--SET TEMPLATE = RM.Template
--FROM 
--	ReportTemplate RM
--INNER JOIN 
--	[192.168.0.14\pendarsql].CRM.dbo.ReportTemplate R14 ON RM.ID = R14.ID
--WHERE
--	RM.ID = 238 AND R14.ID = 238

--13931020 - 10:39
--گزارش تلفن مشغول به کار بر روی سرور کرمانشاه هم باید بروزرسانی شود
--UPDATE RR
--SET Template = R14.Template
--FROM 
--	ReportTemplate R14
--INNER JOIN
--	[78.39.252.109].CRM.DBO.ReportTemplate RR ON RR.ID = R14.ID
--WHERE 
--	R14.ID = 238 AND RR.id = 238