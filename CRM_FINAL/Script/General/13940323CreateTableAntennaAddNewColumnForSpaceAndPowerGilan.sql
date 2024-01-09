--13940323 
ALTER TABLE SpaceAndPower
ADD RigSpace nvarchar(300)
GO
EXEC sp_addextendedproperty N'MS_Description',N'فضای روی دکل','Schema',dbo,'table',SpaceAndPower,'column',RigSpace
GO
CREATE TABLE Antenna
(
	ID bigint not null primary key IDENTITY(1,1),
	Name nvarchar(300) not null,
	[Count] int not null,
	Height nvarchar(300) not null
)
GO
EXEC sp_addextendedproperty N'MS_Description',N'شناسه آنتن','Schema',dbo,'table',Antenna,'column',ID
EXEC sp_addextendedproperty N'MS_Description',N'نوع آنتن','Schema',dbo,'table',Antenna,'column',Name
EXEC sp_addextendedproperty N'MS_Description',N'تعداد آنتن','Schema',dbo,'table',Antenna,'column',[Count]
EXEC sp_addextendedproperty N'MS_Description',N'ارتفاع آنتن','Schema',dbo,'table',Antenna,'column',Height
GO
ALTER TABLE Antenna
ADD SpaceAndPowerID bigint 
GO
ALTER TABLE Antenna
ADD CONSTRAINT FK_Antenna_SpaceAndPower FOREIGN KEY (SpaceAndPowerID) REFERENCES SpaceAndPower (ID)
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_SpaceAndPowerID] ON [dbo].[Antenna]
(
	[SpaceAndPowerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO


