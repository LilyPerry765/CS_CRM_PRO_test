--13931128 - 1003
--باید با جدول سرویس ویژه یک ستون جدید اضافه شود تا خود سیستم در هنگام دایری سرویس ویژه مقدار آن را پر کند
--بر روی سیستم خودم
--ALTER TABLE SpecialService
--add InsertDate smalldatetime
--exec sp_addextendedproperty 'MS_Description',N'تاریخ برقراری سرویس توسط سیستم','Schema',dbo,'table',SpecialService,'column',InsertDate

-- بر روی سرور 14 هم اضافه شد
--ALTER TABLE SpecialService
--add InsertDate smalldatetime
--exec sp_addextendedproperty 'MS_Description',N'تاریخ برقراری سرویس توسط سیستم','Schema',dbo,'table',SpecialService,'column',InsertDate

--13931128 - 1049
--بر روی سرور کرمانشاه هم اضافه شد
--ALTER TABLE SpecialService
--add InsertDate smalldatetime
--exec sp_addextendedproperty 'MS_Description',N'تاریخ برقراری سرویس توسط سیستم','Schema',dbo,'table',SpecialService,'column',InsertDate