--13940601 - 10:22
--ستون آدرس در جدول فضا و پاور اضافه شد
--سیستم خودم
USE CRMGilan
GO
ALTER TABLE SpaceAndPower
add AddressID bigint 
GO
EXEC sp_addextendedproperty 'MS_Description',N'آدرس مشترک','Schema',dbo,'table',SpaceAndPower,'column',AddressID
GO
ALTER TABLE SpaceAndPower
add constraint FK_SpaceAndPower_Address_AddressID foreign key (AddressID) references [Address](ID)
GO
--********************************************************************************************************************
--سرور 14
GO
ALTER TABLE SpaceAndPower
add AddressID bigint 
GO
EXEC sp_addextendedproperty 'MS_Description',N'آدرس مشترک','Schema',dbo,'table',SpaceAndPower,'column',AddressID
GO
ALTER TABLE SpaceAndPower
add constraint FK_SpaceAndPower_Address_AddressID foreign key (AddressID) references [Address](ID)
GO