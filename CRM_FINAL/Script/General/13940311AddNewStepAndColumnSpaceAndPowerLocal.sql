
--13940311 - 1500
--سیستم خودم
--INSERT into RequestStep 
--(
--	RequestTypeID,StepTitle,IsVisible,ReportTemplateID
--)
--VALUES
--(
--	60,N'اداره طراحی شبکه و کابل',1,null
--)

--INSERT INTO RequestStep
--(
--	RequestTypeID,StepTitle,IsVisible,ReportTemplateID
--)
--VALUES
--(
--	60,N'سالن دستگاه',1,NULL
--)

--ALTER TABLE SpaceAndPower
--add AdministrationOfTheTelecommunicationEquipmentOperationDate smalldatetime,
--CableAndNetworkDesignOfficeFile uniqueidentifier,
--	CableAndNetworkDesignOfficeComment nvarchar(max),
--	CableAndNetworkDesignOfficeDate smalldatetime,
--	CableAndNetworkDesignOfficeUserID int,
--	DeviceHallComment nvarchar(max),
--	DeviceHallDate smalldatetime,
--	DeviceHallUserID int


--exec sp_addextendedproperty N'MS_Description',N'تاریخ بهره برداری اداره نظارت تجهیزات مخابراتی','Schema',dbo,'table',SpaceAndPower,'column',AdministrationOfTheTelecommunicationEquipmentOperationDate
--exec sp_addextendedproperty N'MS_Description',N'فایل اداره طراحی شبکه و کابل','Schema',dbo,'table',SpaceAndPower,'column',CableAndNetworkDesignOfficeFile
--exec sp_addextendedproperty N'MS_Description',N'توضیحات اداره طراحی شبکه و کابل','Schema',dbo,'table',SpaceAndPower,'column',CableAndNetworkDesignOfficeComment
--exec sp_addextendedproperty N'MS_Description',N'تاریخ بررسی توسط اداره طراحی شبکه و کابل','Schema',dbo,'table',SpaceAndPower,'column',CableAndNetworkDesignOfficeDate
--exec sp_addextendedproperty N'MS_Description',N'کاربر اداره طراحی شبکه و کابل','Schema',dbo,'table',SpaceAndPower,'column',CableAndNetworkDesignOfficeUserID
--exec sp_addextendedproperty N'MS_Description',N'توضیحات سالن دستگاه','Schema',dbo,'table',SpaceAndPower,'column',DeviceHallComment
--exec sp_addextendedproperty N'MS_Description',N'تاریخ بررسی توسط سالن دستگاه','Schema',dbo,'table',SpaceAndPower,'column',DeviceHallDate
--exec sp_addextendedproperty N'MS_Description',N'کاربر سالن دستگاه','Schema',dbo,'table',SpaceAndPower,'column',DeviceHallUserID

--alter table SpaceAndPower 
--add constraint FK_SpaceAndPower_User_CableAndNetworkDesignOffice foreign key (CableAndNetworkDesignOfficeUserID) references [User](ID)
--go
--alter table SpaceAndPower 
--add constraint FK_SpaceAndPower_User_DeviceHall foreign key (DeviceHallUserID) references [User](ID)

