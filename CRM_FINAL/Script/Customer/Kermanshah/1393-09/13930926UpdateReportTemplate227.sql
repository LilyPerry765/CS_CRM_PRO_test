﻿--گزارش تعویض بوخت - ام دی اف
--13930926 09:00
--چون  در سرور اشتباه کار میکرد ولی در لوکال درست بود
--UPDATE RR
--SET Template = RL.Template,
--	[TimeStamp]= RR.[TimeStamp]
--from 
--	[78.39.252.109].[CRM].[dbo].ReportTemplate RR
--INNER JOIN 
--	CRM.DBO.ReportTemplate RL ON RR.ID = RL.ID
--where
--	RR.ID = 227 AND RL.ID = 227

--13930926 09:18
--در روی سرور کلا ساختار فرم گزارش را عوض کردم پس باید بر روی لوکال هم به روز رسانی میکردم
--UPDATE RL
--SET Template = RR.Template,
--	[TimeStamp] = RR.[TimeStamp]
--FROM 
--	ReportTemplate RL
--INNER JOIN 
--	[78.39.252.109].[CRM].[dbo].ReportTemplate RR ON RR.ID =RL.ID
--WHERE 
--	RL.ID = 227 AND RR.ID = 227

--13930926 10:00
--فونت ها را بر روی لوکال عوض کردم یکان شدند حالا باید بر روی سرور هم به روزرسانی شوند
--UPDATE RR
--SET Template = RL.Template,
--	[TimeStamp]= RR.[TimeStamp]
--from 
--	[78.39.252.109].[CRM].[dbo].ReportTemplate RR
--INNER JOIN 
--	CRM.DBO.ReportTemplate RL ON RR.ID = RL.ID
--where
--	RR.ID = 227 AND RL.ID = 227

--13930926 10:46
--برای چاپ به صورت گروهی در 
--RequestsInbox 
--باید در جدول 
--RequestStep 
--ستون ReportTemplateID  را مقدار میدادم
--RequestStep = 1341 ام دی اف
--ReportTemplateID = 227 --تعویض بوخت - ام دی اف

--Local
--update RequestStep
--set ReportTemplateID = 227
--where 
--	ID = 1341

--کرمانشاه
--UPDATE [78.39.252.109].[CRM].[dbo].RequestStep
--set ReportTemplateID = 227
--where ID = 1341