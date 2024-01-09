DECLARE @cmd nvarchar(max)
SET @CMD = 'SELECT max(ID) FROM ReportTemplate'
EXECUTE(N'USE CRM;'+@cmd) AT [192.168.0.14\pendarsql]

--سیستم خ.دم
--INSERT INTO ReportTemplate
--(ID,Title,Category,UserControlName,IsVisible,IconName,Template)
--VALUES 
--(287,N'گزارش اخطارها',N'درخواستها','WarningReport',1,'Report2.png',null)

--13940126 - 1750
--گزارش اخطارها بر روی سرور 14 هم وارد شد
--INSERT INTO [192.168.0.14\PENDARSQL].CRM.dbo.reportTemplate
--SELECT * FROM ReportTemplate WHERE ID = 287

--13940126 - 1753
--سرور کرمانشاه
--INSERT INTO [78.39.252.109].CRM.DBO.REPORTTEMPLATE
--SELECT * FROM ReportTemplate WHERE ID = 287