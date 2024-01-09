--13940608 
--mypc , server14 , kermanshah , gilan ,semnan
ALTER TABLE Customer
ADD AddressID BIGINT  null,
	Fax  nvarchar(40) null
GO
EXEC sp_addextendedproperty 'MS_Description',N'شناسه آدرس','Schema',dbo,'table',Customer,'column',AddressID
EXEC sp_addextendedproperty 'MS_Description',N'نمابر','Schema',dbo,'table',Customer,'column',Fax
GO
ALTER TABLE Customer
ADD CONSTRAINT FK_Customer_Address_AddressID foreign key (AddressID) references [Address] (ID)
GO