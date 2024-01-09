--13960121 - 1108

--اگر توسط سرویس احراز هویت شاهکار ، احراز شود آنگاه باید مقدار یک بگیرد تا این مشترک بتواند ثبت درخواست دایری انجام دهد
use CRM
go
alter table Customer
add IsAuthenticated bit 
go
exec sp_addextendedproperty N'MS_Description',N'آیا این مشترک توسط شاهکار احراز هویت شده است یا خیر','schema',dbo,'table',Customer,'column',IsAuthenticated
go
