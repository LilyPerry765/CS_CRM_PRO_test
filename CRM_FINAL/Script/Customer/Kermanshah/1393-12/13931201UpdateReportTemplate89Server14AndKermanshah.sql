﻿-- 13931201 - 1042 
--بع الکوی گزارش خراب در بخش آمار ورودی طبق فکس کرمانشاه ستون زمان خرابی اضافه شد و بر روی سیستم خودم نهایی شد حالا باید بر روی سرور 14 هم بروز رسانی شود
--UPDATE R14
--SET TEMPLATE = RM.Template
--FROM 
--	ReportTemplate RM
--INNER JOIN 
--	[192.168.0.14\PENDARSQL].CRM.DBO.REPORTtEMPLATE R14 ON RM.ID = R14.ID 
--WHERE 
--	RM.ID = 89 AND R14.ID = 89

--13931201 - 1048
--گزارش خراب در بخش آمار ورودی بر روی سرور کرمانشاه هم باید برزورسانی میشد
--UPDATE RR
--SET Template = R14.Template
--FROM 
--	ReportTemplate R14 
--INNER JOIN 
--	[78.39.252.109].CRM.DBO.REPORTTEMPLATE RR ON R14.ID = RR.ID
--WHERE 
--	RR.ID = 89 AND R14.ID = 89