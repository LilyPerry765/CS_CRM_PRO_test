
ALTER TABLE SpaceAndPower
add SwitchDesigningOfficeFile uniqueidentifier
go
exec sp_addextendedproperty N'MS_Description',N'فایل اداره طراحی سوئیچ','Schema',dbo,'table',SpaceAndPower,'column',SwitchDesigningOfficeFile