﻿--13931212 - 1432
--گزارش کلی هزینه برو روی سیستم خودم نهایی شد حالا ابدی بر روی سرور 14 هم بروزرسانی شود
--شهر و مرکز به آن اضافه شد
--UPDATE R14
--SET TEMPLATE = RM.TEMPLATE ,
--	TITLE= RM.Title
--FROM 
--	ReportTemplate RM
--INNER JOIN 
--	[192.168.0.14\PENDARSQL].CRM.DBO.ReportTemplate R14 ON RM.ID = R14.ID 
--WHERE 
--	RM.ID = 154 AND R14.ID = 154

--13931212 - 1436
--بر روی سرور کرمانشاه هم بروزرسانی شد
--UPDATE RR
--SET TEMPLATE = R14.Template,
--	Title = R14.Title
--FROM 
--	ReportTemplate R14 
--INNER JOIN 
--	[78.39.252.109].CRM.DBO.REPORTTEMPLATE RR ON R14.ID = RR.ID 
--WHERE 
--	RR.ID = 154 AND R14.ID = 154

