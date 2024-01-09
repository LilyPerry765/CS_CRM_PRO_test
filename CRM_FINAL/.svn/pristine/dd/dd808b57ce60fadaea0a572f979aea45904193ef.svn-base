--13940320 
--تعریف ستون فایل دستور مداری
ALTER TABLE SpaceAndPower 
add CircuitCommandFile uniqueidentifier
go
exec sp_addextendedproperty N'MS_Description',N'فایل دستور مداری','Schema',dbo,'table',SpaceAndPower,'column',CircuitCommandFile
Go
alter table SpaceAndPower 
with check Add constraint FK_SpaceAndPower_DocumentsFile_CircuitCommandFile foreign key (CircuitCommandFile) references DocumentsFile (stream_id)
go
