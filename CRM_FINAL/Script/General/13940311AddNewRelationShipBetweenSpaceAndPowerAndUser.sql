ALTER TABLE SpaceAndPower
WITH CHECK ADD CONSTRAINT FK_SpaceAndPower_User_FinancialScope foreign key (FinancialScopeUserID) references [User](ID)
go
ALTER TABLE SpaceAndPower
WITH CHECK ADD CONSTRAINT FK_SpaceAndPower_User_DesignManager foreign key (DesignManagerUserID) references [User](ID)
GO
ALTER TABLE SpaceAndPower
WITH CHECK ADD CONSTRAINT FK_SpaceAndPower_User_SwitchDesigningOffice foreign key (SwitchDesigningOfficeUserID) references [User](ID)
go
alter table SpaceAndPower
with check add constraint FK_SpaceAndPower_User_DesignManagerFinalCheck foreign key (DesignManagerFinalCheckUserID) references [User](ID)
go
alter table SpaceAndPower
with check add constraint FK_SpaceAndPower_User_NetworkAssistant foreign key (NetworkAssistantUserID) references [User](ID)
go
alter table SpaceAndPower 
with check add constraint FK_SpaceAndPower_User_AdministrationOfTheTelecommunicationEquipment foreign key (AdministrationOfTheTelecommunicationEquipmentUserID) references [User](ID)
go
