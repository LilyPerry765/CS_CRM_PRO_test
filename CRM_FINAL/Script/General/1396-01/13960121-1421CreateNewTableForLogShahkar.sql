--13960121 - 1421
use CRM
go
CREATE TABLE ShahkarWebApiLog
(
	ID bigint not null primary key identity(1,1),
	UserName nvarchar(100) not null,
	UserFirstName nvarchar(100),
	UserLastName nvarchar(100),
	SendDate smalldatetime not null,
	ActionDetails xml not null,
	ActionRelativeURL nvarchar(200) not null,
	ActionType int not null,
	CustomerID bigint
)
go
print 'Table is created!'
go
alter table ShahkarWebApiLog
add constraint FK_ShahkarWebApiLog_Customer_ID foreign key (CustomerID) references Customer(ID)
go
print 'Relate with Customer'
go
--چون هیچ گونه دیدی از نماینده و مشترک حقوقی ندارم ، فعلاً در ثبت لاگ به شناسه مشترک بسنده میکنم
----alter table ShahkarWebApiLog
----add constraint FK_ShahkarWebApiLog_Agent_ID foreign key (AgentID) references Agent(ID)
----go
----alter table ShahkarWebApiLog
----drop constraint FK_ShahkarWebApiLog_Agent_ID
----go
----alter table ShahkarWebApiLog
----drop column AgentID 
----go
exec sp_addextendedproperty N'MS_Description',N'جدول لاگ عملیات هایی که از طریق سامانه شاهکار صورت میگیرند','schema',dbo,'table',ShahkarWebApiLog,'column',ID
exec sp_addextendedproperty N'MS_Description',N' کاربری که در خواست را به سمت شاهکار زده است','schema',dbo,'table',ShahkarWebApiLog,'column',UserName
exec sp_addextendedproperty N'MS_Description',N'نام کاربری که در خواست را به سمت شاهکار زده است','schema',dbo,'table',ShahkarWebApiLog,'column',UserFirstName
exec sp_addextendedproperty N'MS_Description',N'فامیلی کاربری که در خواست را به سمت شاهکار زده است','schema',dbo,'table',ShahkarWebApiLog,'column',UserLastName
exec sp_addextendedproperty N'MS_Description',N'تاریخ ارسال','schema',dbo,'table',ShahkarWebApiLog,'column',SendDate
exec sp_addextendedproperty N'MS_Description',N'جزئیات عملیات','schema',dbo,'table',ShahkarWebApiLog,'column',ActionDetails
exec sp_addextendedproperty N'MS_Description',N'آدرس اکشن','schema',dbo,'table',ShahkarWebApiLog,'column',ActionRelativeURL
exec sp_addextendedproperty N'MS_Description',N'نوع عملیات','schema',dbo,'table',ShahkarWebApiLog,'column',ActionType
exec sp_addextendedproperty N'MS_Description',N'شناسه مشترک - در صورتی که درخواست احراز هویت برای مشترکین حقیقی باشد','schema',dbo,'table',ShahkarWebApiLog,'column',CustomerID
go