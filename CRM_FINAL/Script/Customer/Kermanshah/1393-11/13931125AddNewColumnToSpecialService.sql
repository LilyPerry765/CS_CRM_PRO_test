--13931125 - 1635
--با توجه به بررسی انجام گرفته بر روی گزارش سرویس ویژه لازم بود تا در بخش تبت درخواست سرویس ویژه ، امکان تعیین تاریخ حذف وجود داشته باشد
--که باید ستون آن به جدول اضافه میشد
--سیستم خودم
--ALTER TABLE SpecialService
--ADD UninstallDate smalldatetime 
--exec sp_addextendedproperty 'MS_Description',N'تاریخ حذف سرویس','Schema',dbo,'table',SpecialService,'column',UninstallDate

--بر روی سرور 14 و سرور کرمانشاه هم اضافه شد
--ALTER TABLE SpecialService
--ADD UninstallDate smalldatetime 
--exec sp_addextendedproperty 'MS_Description',N'تاریخ حذف سرویس','Schema',dbo,'table',SpecialService,'column',UninstallDate

