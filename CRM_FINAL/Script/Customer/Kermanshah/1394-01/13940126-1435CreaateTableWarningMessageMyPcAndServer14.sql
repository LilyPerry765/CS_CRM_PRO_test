--13940126 - 1516
--ایجاد یک جدول برای ثبت متون مربوط به انواع اخطار ها
--سیستم خودم
--سرور 14
--CREATE TABLE WarningMessage
--(
--	ID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
--	[Message] nvarchar(max) not null,
--	WarningType tinyint not null
--)

--EXEC sp_addextendedproperty N'MS_Description',N'شناسه پیغام اخطار','schema',dbo,'table',WarningMessage,'column',ID
--exec sp_addextendedproperty N'MS_Description',N'متن پیغام اخطار','schema',dbo,'table',WarningMessage,'column',[Message]
--exec sp_addextendedproperty N'MS_Description',N'نوع اخطار','schema',dbo,'table',WarningMessage,'column',WarningType

--CREATE UNIQUE NONCLUSTERED INDEX IX_WarningType ON WarningMessage (WarningType)