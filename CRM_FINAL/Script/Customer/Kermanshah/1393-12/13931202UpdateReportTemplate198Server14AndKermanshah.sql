﻿--13931202 - 1342
--سیستم خودم
--UPDATE ReportTemplate
--SET Category = N'گواهی',
--	UserControlName = 'ChangeNameCertificateReport',
--	Title = N'گواهی تغییر نام'
--WHERE ID = 198 

--13931202 - 1424
--سرور 14
--UPDATE [192.168.0.14\pendarsql].crm.dbo.ReportTemplate
--SET Category = N'گواهی',
--	UserControlName = 'ChangeNameCertificateReport',
--	Title = N'گواهی تغییر نام'
--WHERE ID = 198 

--13931202 - 1425
--سرور کرمانشاه
--UPDATE [78.39.252.109].crm.dbo.ReportTemplate
--SET Category = N'گواهی',
--	UserControlName = 'ChangeNameCertificateReport',
--	Title = N'گواهی تغییر نام'
--WHERE ID = 198 

--13931202 - 1642
--الگوی گزارش گواهی تغییر نام بر روی سیستم خودم نهایی شد حالا باید بر روی سرور 124 هم بروزرسانی شود
--UPDATE R14
--SET TEMPLATE = RM.Template
--FROM 
--	ReportTemplate RM
--INNER JOIN 
--	[192.168.0.14\PENDARSQL].CRM.DBO.ReportTemplate R14 ON RM.id = r14.ID 
--where 
--	rm.id = 198 and r14.ID = 198

--13931202 - 1648
--بر روی سرور کرمانشاه هم بروزرسانی شد
--UPDATE RR
--SET TEMPLATE = R14.Template
--FROM 
--	ReportTemplate R14
--INNER JOIN 
--	[78.39.252.109].CRM.DBO.ReportTemplate RR ON RR.id = r14.ID 
--where 
--	rR.id = 198 and r14.ID = 198
