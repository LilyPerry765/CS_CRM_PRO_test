alter table ShahkarWebApiLog
add RequestID bigint null
go
exec sp_addextendedproperty N'MS_Description',N'شناسه درخواست در سیستم خودمان که تلفن آن باید از سمت شاهکار تاییدبه بگیرد','schema',dbo,'table',ShahkarWebApiLog,'column',RequestID