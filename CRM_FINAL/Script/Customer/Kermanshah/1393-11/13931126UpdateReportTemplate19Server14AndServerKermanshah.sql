﻿--13931126 - 1116
--الگوی گزارش سرویس ویژه فوتری که شماره صفحه را نشان دهد ، نداشت که اضافه شد
--UPDATE R14
--SET TEMPLATE = RM.Template
--FROM 
--	ReportTemplate RM
--INNER JOIN 
--	[192.168.0.14\PENDARSQL].CRM.DBO.ReportTemplate R14 ON RM.id = r14.id
--where 
--	rm.id = 19 and r14.id = 19

--بر روی سرور کرمانشاه هم باید بروزرسانی میشد
--UPDATE RR
--SET Template = R14.Template
--FROM
--	ReportTemplate R14
--INNER JOIN 
--	[78.39.252.109].CRM.dbo.ReportTemplate RR ON R14.id = rr.ID
--where 
--	r14.id = 19 and rr.id = 19