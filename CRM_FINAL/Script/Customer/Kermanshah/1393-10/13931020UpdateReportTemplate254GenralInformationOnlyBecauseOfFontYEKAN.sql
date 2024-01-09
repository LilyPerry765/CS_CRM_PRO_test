--13931020 - 19:08
--هیچ تغییری بر روی الگوی اطلاعات جامع صورت نگرفته است فقط به خاطر فونت یکان
--چون امروز آقای صمدی بر روی سرور این فونت را نصب کرد
UPDATE RR
SET Template = R14.Template
FROM 
	ReportTemplate R14
INNER JOIN 
	[78.39.252.109].CRM.DBO.ReportTemplate RR ON RR.ID = R14.ID
WHERE 
	R14.ID = 254  AND RR.ID = 254