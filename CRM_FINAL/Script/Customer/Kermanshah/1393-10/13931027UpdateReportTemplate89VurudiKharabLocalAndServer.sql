﻿--13931027 - 11:56
--گزارش خراب در گروه آمار ورودی بر روی سیستم خودم نهایی شد حالا باید بر روی سرور 14 هم بروزرسانی شود
--UPDATE R14
--SET TEMPLATE = RM.Template
--FROM 
--	ReportTemplate RM
--INNER JOIN 
--	[192.168.0.14\PENDARSQL].CRM.DBO.ReportTemplate R14 ON RM.ID = r14.ID
--WHERE
--	rm.id = 89 and r14.id = 89


--13931027 - 12:00
--گزارش خراب در گروه آمار ورودی باید بر روی سرور کرمانشاه هم بروزرسانی شود
--UPDATE RR
--SET Template = R14.Template
--FROM 
--	ReportTemplate R14
--INNER JOIN 
--	[78.39.252.109].CRM.DBO.ReportTemplate RR ON R14.ID = RR.ID
--WHERE 
--	R14.ID = 89 AND RR.ID = 89
