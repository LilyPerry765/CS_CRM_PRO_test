

alter table InstallRequest add IsGSM bit null
GO
update InstallRequest set IsGSM = 0
GO
alter table InstallRequest alter column IsGSM bit not null
GO

alter table dbo.SpecialConditions  add IsGSM bit null
GO
update dbo.SpecialConditions set IsGSM = 0
GO
alter table dbo.SpecialConditions alter column IsGSM bit NOT NULL
GO