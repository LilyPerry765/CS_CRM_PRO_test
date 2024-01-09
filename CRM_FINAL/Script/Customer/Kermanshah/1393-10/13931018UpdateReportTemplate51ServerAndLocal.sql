--13931018 - 11:55
--گزارش کلي خرابي مرکزی ها  بر روی سیستم خودم نهایی شد حالا باید بر روی سرور 14 هم بروزرسانی شود
--UPDATE R14
--SET Template = RM.Template
--from 
--	ReportTemplate RM 
--Inner join 
--	[192.168.0.14\pendarsql].CRM.dbo.ReportTemplate R14 ON RM.ID = R14.ID
--WHERE 
--	RM.ID = 51 AND R14.ID = 51


--گزارش کلي خرابي مرکزی ها  بر روی سرور کرمانشاه هم بروزرسانی شد
--13931018 - 12:02
--update rr
--set Template = R14.Template
--FROM 
--	ReportTemplate R14
--INNER JOIN 
--	[78.39.252.109].CRM.dbo.ReportTemplate RR ON R14.ID = RR.ID 
--WHERE 
--	R14.ID = 51 AND RR.ID = 51

