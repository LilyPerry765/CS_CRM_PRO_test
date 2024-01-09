alter table ZeroStatus
add HasSecondZeroBlockCost bit null;
go
exec sp_addextendedproperty N'MS_Description',N'اگر درخواست مربوط به بستن صفر دوم باشد. وارد کردن هزینه اختیاری است','schema',dbo,'table',ZeroStatus,'column',HasSecondZeroBlockCost 
go
--کلاس تلفن صفر ما در اینام های کد نداریم
UPDATE T 
SET T.ClassTelephone = 1
FROM 
	TELEPHONE T 
WHERE 
	T.STATUS = 2 
	AND
	T.ClassTelephone = 0 
