﻿--13931212 - 1439
--گروه بندی الگوی گزارش انسداد صفر درست کارنمیکرد
UPDATE R14
SET TEMPLATE = RM.Template
FROM 
	ReportTemplate RM
INNER JOIN 
	[192.168.0.14\PENDARSQL].CRM.DBO.REPORTTEMPLATE R14 ON R14.ID = RM.ID 
WHERE 
	R14.ID = 16 AND RM.ID = 16

--13931212 - 1444
UPDATE RR
SET Template = R14.Template
FROM 
	ReportTemplate R14
INNER JOIN 
	[78.39.252.109].CRM.DBO.REPORTTEMPLATE RR ON R14.ID = RR.ID 
WHERE 
	R14.ID = 16 AND RR.ID = 16