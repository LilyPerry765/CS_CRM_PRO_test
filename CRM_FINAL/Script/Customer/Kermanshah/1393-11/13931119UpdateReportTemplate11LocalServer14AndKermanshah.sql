﻿--13931119 - 16:10
--طبق لیست 14 موردی میلاد باید به گزارش درخواست دایری در بخش امورمشترکین فیلد تاریخ دایری اضافه میشد
--حالا باید بر روی سرور 14 و همچنین سرور کرمانشاه بروزرسانی شود
--UPDATE R14
--SET Template = RM.Template,
--	Category= RM.Category
--FROM 
--	ReportTemplate RM
--INNER JOIN 
--	[192.168.0.14\PendarSql].CRM.dbo.ReportTemplate R14 ON RM.ID = R14.ID
--WHERE 
--	RM.ID = 11 AND R14.ID = 11

----13931119 - 16:19
--سرور کرمانشاه
--UPDATE RR
--SET Template = R14.Template
--FROM
--	ReportTemplate R14
--INNER JOIN 
--	[78.39.252.109].CRM.dbo.ReportTemplate RR ON R14.ID = RR.ID 
--WHERE 
--	R14.ID = 11 AND RR.ID = 11