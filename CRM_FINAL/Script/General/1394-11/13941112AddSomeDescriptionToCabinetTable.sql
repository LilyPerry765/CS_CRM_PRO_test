﻿--13941113
--تعدادی از ستون های جدول کافو توضیحات نداشت که اضافه کردیم
--exec sp_addextendedproperty N'MS_Description',N'محدودیت پست','schema',dbo,'table',Cabinet,'column',IsLimitPost
--exec sp_addextendedproperty N'MS_Description',N'خارج از مرز','schema',dbo,'table',Cabinet,'column',IsOutBound
--exec sp_addextendedproperty N'MS_Description',N'محدودیت سهمیه رزرو','schema',dbo,'table',Cabinet,'column',ApplyQuota
--exec sp_addextendedproperty N'MS_Description',N'ظرفیت اسمی','schema',dbo,'table',Cabinet,'column',Capacity
--exec sp_addextendedproperty N'MS_Description',N'تاریخ کنترل','schema',dbo,'table',Cabinet,'column',CreateDate
--exec sp_addextendedproperty N'MS_Description',N'حداکثر تعداد پست','schema',dbo,'table',Cabinet,'column',MaxNumberPost