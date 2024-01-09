--13930927 10:08
--گزارش را در سرور وارد کردم
--INSERT INTO ReportTemplate
--(
--	ID,Title,Template,IconName,Category,UserControlName
--)
--VALUES (254,N'گزارش اطلاعات جامع',null,'Report2.png',N'درخواستها','GeneralInformationReport')


--13930927 10:10
--گزارش را در لوکال وارد کردم
--INSERT INTO ReportTemplate
--(
--	ID,Title,Template,IconName,Category,UserControlName
--)
--VALUES (254,N'گزارش اطلاعات جامع',null,'Report2.png',N'درخواستها','GeneralInformationReport')

--الگوی گزارش را درلوکال درست کردم حالا باید در سرور هم به روز رسانی شود
--13930927 11:38
--UPDATE RR
--SET Template = RL.Template,
--	[TimeStamp] = RL.[TimeStamp]
--FROM 
--	[78.39.252.109].[CRM].[dbo].ReportTemplate RR
--INNER JOIN 
--	ReportTemplate RL ON RR.ID = RL.ID
--WHERE 
--	RR.ID = 254 AND RL.ID = 254

