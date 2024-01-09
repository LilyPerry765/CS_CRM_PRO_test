ALTER TABLE SpaceAndPower
with check add constraint FK_SpaceAndPower_DocumentsFile_EnteghalFile foreign key (EnteghalFile) references DocumentsFile(stream_id)
go
alter table SpaceAndPower 
with check add constraint FK_SpaceAndPower_DocumentsFile_NirooFile foreign key (NirooFile) references DocumentsFile(stream_id)
go
alter table SpaceAndPower
with check add constraint FK_SpaceAndPower_DocumentsFile_CableAndNetworkDesignOfficeFile foreign key (CableAndNetworkDesignOfficeFile) references DocumentsFile(stream_id)
go
alter table SpaceAndPower
with check add constraint FK_SpaceAndPower_DocumentsFile_SwitchDesigningOfficeFile foreign key (SwitchDesigningOfficeFile) references DocumentsFile(stream_id)
