IF OBJECT_ID('PowerType') IS NOT NULL
	BEGIN
		DROP TABLE POWERTYPE
		PRINT 'PowerType is dropped!'
	END
GO
IF OBJECT_ID('SpaceAndPowerPowerType')  IS NOT NULL
	BEGIN
		DROP TABLE SpaceAndPowerPowerType;
		PRINT 'SpaceAndPowerPowerType is dropped'
	END
GO
CREATE TABLE PowerType
(
	ID int primary key not null identity(1,1),
	Title nvarchar(128) not null unique 
)
GO
exec sp_addextendedproperty 'MS_Description',N'شناسه نوع پاور','schema',dbo,'table',PowerType,'column',ID
exec sp_addextendedproperty N'MS_Description',N'عنوان پاور','schema',dbo,'table',PowerType,'column',Title
GO
CREATE TABLE SpaceAndPowerPowerType
(
	ID int primary key not null identity(1,1),
	SpaceAndPowerID bigint not null,
	PowerTypeID int not null,
	InsertDate datetime not null default(getdate())

	constraint FK_SpaceAndPowerPowerType_SpaceAndPower_SpaceAndPowerID foreign key (SpaceAndPowerID)  references SpaceAndPower(ID),
	constraint FK_SpaceAndPowerPowerType_PowerType_PowerTypeID foreign key (PowerTypeID) references PowerType(ID)
)
GO
set identity_insert PowerType on
GO
INSERT INTO PowerType
(ID,Title)
VALUES 
(1,N'با پشتوانه تک فاز AC'),
(2,N'بدون پشتوانه تک فاز AC'),
(3,N'DC'),
(4,N'BTS'),
(5,N'عدم نیاز به پاور'),
(6,N'با پشتوانه سه فاز AC'),
(7,N'بدون پشتوانه سه فاز AC')
GO
PRINT 'values inserted.'
GO
SET IDENTITY_INSERT PowerType OFF
GO
select * from PowerType
GO
INSERT INTO [SpaceAndPowerPowerType]
(SpaceAndPowerID,PowerTypeID)
SELECT 
	SP.ID,
	SP.PowerType
FROM 
	SpaceAndPower SP
GO