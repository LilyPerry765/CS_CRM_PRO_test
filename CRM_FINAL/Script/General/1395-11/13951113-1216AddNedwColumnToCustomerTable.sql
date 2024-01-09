--بر اساس داکیومنت شاهکار ، برای مشترکین حقوقی میبایست نوع شرکت مشخص باشد
--13951113 - 1216
--go
--alter table Customer
--add CompanyType tinyint null 

--go

--exec sp_addextendedproperty N'MS_Description',N'نوع شرکت - برای مشترکین حقوقی','schema',dbo,'table',Customer,'column',CompanyType
