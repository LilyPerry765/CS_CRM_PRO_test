--
alter table ShahkarWebApiLog
drop column UserName
go
print 'UserName is dropped' 
go
alter table ShahkarWebApiLog
drop column UserFirstName
go
print 'UserFirstName is dropped' 
go
alter table ShahkarWebApiLog
drop column UserLastName
go
print 'UserLastName is dropped' 
go
alter table ShahkarWebApiLog
add UserID int not null
go
alter table ShahkarWebApiLog
add constraint FK_ShahkarWebApiLog_User_UserID foreign key (UserID) references [User]
go