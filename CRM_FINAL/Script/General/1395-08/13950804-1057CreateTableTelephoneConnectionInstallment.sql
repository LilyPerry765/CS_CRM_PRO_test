--13950804 - 0840
--use CRMKermanshahNew
--update InstallRequest 
--set MethodOfPaymentForTelephoneConnection = 2
--where 
--	MethodOfPaymentForTelephoneConnection is null 
--	and 
--	len(RequestID) = 10

--13950804 - 1057
--use CRMKermanshahNew 
--CREATE TABLE TelephoneConnectionInstallment
--(
--	ID bigint not null identity(1,1) primary key,
--	RequestPaymentID bigint not null,
--	UserID int not null,
--	InstallmentsCount int not null,
--	InsertDate smalldatetime not null
	
--	constraint FK_TelephoneConnectionInstallment_RequestPayment_RequestPaymentID foreign key (RequestPaymentID) references RequestPayment (ID),
--	constraint FK_TelephoneConnectionInstallment_User_UserID foreign key (UserID) references [User](ID)
--)

