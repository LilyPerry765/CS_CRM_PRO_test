
CREATE TABLE WebService
(
	ID int not null primary key identity(1,1),
	Name nvarchar(300) not null,
	[Description] nvarchar(200) null
)
GO
CREATE TABLE RoleWebService
(
	ID int not null primary key identity(1,1),
	RoleID int null,
	WebServiceID int null

	constraint FK_Role_RoleWebService_RoleID foreign key (RoleID) references [Role] (ID),
	constraint FK_WebService_RoleWebService_WebServiceID foreign key(WebServiceID) references WebService (ID)
)
GO
ALTER TABLE [Role]
add IsServiceRole bit not null default (0)
GO
