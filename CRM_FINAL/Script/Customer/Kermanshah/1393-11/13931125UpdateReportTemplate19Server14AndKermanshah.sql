

--13931125 - 1322
--گزارش سرویس ویژه را بدلیل نداشتن دیتا ابتدا بر روی سرور 14 ایجاد کردم حالا باید بر روی سیستم خودم هم بروزرسانی شود
--UPDATE RM
--SET Template = R14.Template
--FROM 
--	ReportTemplate RM
--INNER JOIN 
--	[192.168.0.14\Pendarsql].CRM.dbo.ReportTemplate R14 ON RM.ID = r14.Id
--WHERE 
--	RM.ID = 19 AND R14.ID = 19

--13931125 - 1329
--گزارش سرویس ویژه بر روی سرور کرمانشاه هم بروزرسانی شد
--UPDATE RR
--SET Template = R14.Template
--FROM 
--	ReportTemplate R14
--INNER JOIN 
--	[78.39.252.109].CRM.DBO.ReportTemplate RR ON R14.ID = RR.ID
--WHERE 
--	R14.ID = 19 AND RR.ID = 19