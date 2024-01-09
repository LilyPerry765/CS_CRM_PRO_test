--13931211 - 1301
--ستون جدیدی به جدول گزارش اضافه شد
--سیستم خودم ، سرور 14 و کرمانشاه
ALTER TABLE ReportTemplate 
ADD IsVisible bit null 

exec sp_addextendedproperty N'MS_Description',N'آیا گزارش برای کاربران قابل مشاهده باشد یا خیر','Schema',dbo,'table',ReportTemplate,'column',IsVisible