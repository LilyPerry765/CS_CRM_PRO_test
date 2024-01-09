--13931016 - 11:32
--گزارش بازداشت و توقیف بر روی سیستم خودم نهایی شد حالا باید ابتدا بر روی سرور 14 آن را به روزرسانی کنم
--سپس از آنجا بر روی سرور کرمانشاه

--UPDATE R14
--SET Template = RL.Template
--FROM
--	ReportTemplate RL
--INNER JOIN 
--	[192.168.0.14\pendarsql].CRM.DBO.ReportTemplate R14 ON RL.id = R14.ID
--WHERE 
--	RL.ID = 262 and R14.ID = 262


--بروزرسانی بر روی سرور کرمانشاه
--13931016 - 11:44

--UPDATE RR
--SET Template = R14.Template
--FROM 
--	ReportTemplate R14
--INNER JOIN 
--	[78.39.252.109].CRM.dbo.ReportTemplate RR ON R14.id = RR.ID
--WHERE 
--	R14.ID = 262 and rr.ID = 262


--گزارش اطلاعات جامع فیلد شماره تلفن را نداشت. اصلاح شد
--13931016 - 12:20
--UPDATE R14
--SET Template = RM.Template
--FROM 
--	ReportTemplate RM
--INNER JOIN 
--	[192.168.0.14\pendarsql].CRM.dbo.ReportTemplate R14 ON Rm.ID = R14.ID
--WHERE 
--	RM.ID = 254 AND R14.ID = 254


--13931016 - 12:28
--گزارش اطلاعات جامع بر روی سرور کرمانشاه بروزرسانی شد
--update RR
--set Template = r14.Template
--FROM 
--	ReportTemplate R14
--INNER JOIN 
--	[78.39.252.109].CRM.DBO.ReportTemplate RR ON R14.Id = rr.ID
--WHERE 
--	R14.ID = 254 AND RR.id = 254

