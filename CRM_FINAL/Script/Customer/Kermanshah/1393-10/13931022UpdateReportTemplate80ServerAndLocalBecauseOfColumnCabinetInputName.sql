﻿--13931022 - 22:11 
--گزارش کلی کافو و پست ستون تعدادی ورودی به تعداد مرکزی تغییر نام داد 
--UPDATE R14
--SET TEMPLATE = RM.Template
--FROM 
--	ReportTemplate RM
--INNER JOIN 
--	[192.168.0.14\PENDARSQL].CRM.DBO.REPORTTEMPLATE R14 ON RM.ID = R14.ID
--WHERE
--	RM.ID = 80 AND R14.ID = 80

--13931022 - 22:15
--UPDATE RR
--SET Template = R14.Template
--FROM 
--	ReportTemplate R14
--INNER JOIN 
--	[78.39.252.109].CRM.DBO.REPORTTEMPLATE RR ON RR.ID = R14.ID
--WHERE 
--	RR.ID = 80 AND R14.ID = 80