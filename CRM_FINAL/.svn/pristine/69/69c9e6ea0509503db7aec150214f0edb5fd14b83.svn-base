--13931013 - 13:48
--الگوی گزارش بازداست و توقیف امور مشترکین باید ابتدا بر روی لوکال وارد شود

--INSERT INTO ReportTemplate 
--(
--	ID,Category,Title,IconName,UserControlName,Template
--)
--VALUES
--(
--	262,N'درخواستها',N'بازداشت و توقیف','Report2.png','DetentionAndArrestReport',null
--)


--برای سرور
--13931013 - 14:24
--INSERT INTO [78.39.252.109].CRM.dbo.ReportTemplate 
--(
--	ID,Category,Title,IconName,UserControlName,Template
--)
--VALUES
--(
--	262,N'درخواستها',N'بازداشت و توقیف','Report2.png','DetentionAndArrestReport',null
--)


--13931013 - 21:20
--گزارش بازداشت و توقیف بر روی لوکال نهایی شد حالا باید بر روی سرور نیز به روز رسانی شود
--update RR
--set Template = rl.Template
--FROM 
--	ReportTemplate RL
--INNER JOIN 
--	[78.39.252.109].CRM.dbo.ReportTemplate RR ON RL.id= rr.ID
--WHERE 
--	RL.ID = 262 and rr.ID = 262