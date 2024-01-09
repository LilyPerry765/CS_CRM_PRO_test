--بر اساس ساختار شاهکار ، احراز مشترکین حقوقی از طریق احراز هویت نماینده آن صورت میگیرد
create table Agent
(
	ID int not null identity(1,1) primary key,
	FirstName nvarchar(100) not null,
	LastName nvarchar(100) not null,
	Father nvarchar(100) not null,
	BirthCertificateId nvarchar(20) not null, --چه شماره شناسنامه ای معتبر است
	Birthdate smalldatetime  null,
	NationlCode nvarchar(20) not null,
	Mobile varchar(11) null
)


exec sp_addextendedproperty N'MS_Description',N'شناسه نمایندگی','schema',dbo,'table',Agent,'column',ID
exec sp_addextendedproperty N'MS_Description',N'نام','schema',dbo,'table',Agent,'column',FirstName
exec sp_addextendedproperty N'MS_Description',N'نام خانوادگی','schema',dbo,'table',Agent,'column',LastName
exec sp_addextendedproperty N'MS_Description',N'نام پدر','schema',dbo,'table',Agent,'column',Father
exec sp_addextendedproperty N'MS_Description',N'شماره شناسنامه','schema',dbo,'table',Agent,'column',BirthCertificateId
exec sp_addextendedproperty N'MS_Description',N'تاریخ تولد','schema',dbo,'table',Agent,'column',Birthdate
exec sp_addextendedproperty N'MS_Description',N'کد ملی','schema',dbo,'table',Agent,'column',NationlCode
exec sp_addextendedproperty N'MS_Description',N'موبایل','schema',dbo,'table',Agent,'column',Mobile

