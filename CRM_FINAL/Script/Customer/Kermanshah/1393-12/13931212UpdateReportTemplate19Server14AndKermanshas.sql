﻿--13931212 - 1447
--گروه بندی گزارش سرویس ویژه درست کار نمیکرد
--UPDATE R14
--SET TEMPLATE = RM.Template
--FROM 
--	ReportTemplate RM
--INNER JOIN 
--	[192.168.0.14\PENDARSQL].CRM.DBO.REPORTTEMPLATE R14 ON RM.ID = R14.ID 
--WHERE 
--	RM.ID = 19 AND R14.ID = 19

--13931212 - 1449
--UPDATE RR
--SET Template = R14.Template
--FROM 
--	ReportTemplate R14 
--INNER JOIN 
--	[78.39.252.109].CRM.DBO.REPORTTEMPLATE RR ON RR.ID = R14.ID 
--WHERE 
--	RR.ID = 19 AND R14.ID = 19