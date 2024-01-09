--13950207 - 1124
--سرور کرمانشاه

--CREATE TABLE WebService
--(
--	ID int not null primary key identity(1,1),
--	Name nvarchar(300) not null,
--	[Description] nvarchar(200) null
--)
--GO
--CREATE TABLE RoleWebService
--(
--	ID int not null primary key identity(1,1),
--	RoleID int null,
--	WebServiceID int null

--	constraint FK_Role_RoleWebService_RoleID foreign key (RoleID) references [Role] (ID),
--	constraint FK_WebService_RoleWebService_WebServiceID foreign key(WebServiceID) references WebService (ID)
--)
--GO
--ALTER TABLE [Role]
--add IsServiceRole bit not null default (0)
--GO

--exec sp_rename 'FK_Role_RoleWebService_RoleID','FK_RoleWebService_Role',N'OBJECT'
--exec sp_rename 'FK_WebService_RoleWebService_WebServiceID','FK_RoleWebService_WebService',N'OBJECT'

--CREATE UNIQUE NONCLUSTERED INDEX IX_WebService_Name ON WebService (Name)

--INSERT INTO WebService
--(
--	Name
--)
--VALUES
--('TelephoneInfo'),
--('ChangeTelephoneInfo'),
--('CityInfo'),
--('CenterInfo'),
--('RequestTypeInfo'),
--('TelephoneType'),
--('TelephoneGroupType'),
--('CauseOfCut'),
--('GetPAPInstallRequestCount'),
--('GetPAPDischargeRequestCount'),
--('GetPAPExchangeRequestCount'),
--('GetFeasibilityRequest'),
--('SaveInstallRequest'),
--('SaveDischargeRequest'),
--('SaveExchangeRequest'),
--('FollowRequest')
