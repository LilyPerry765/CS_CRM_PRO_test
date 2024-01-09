---- 4/30/2015
---- The IsDelete added to the post Table

--alter table Post add IsDelete bit null
--update Post set IsDelete = 0
--alter table Post alter column IsDelete bit NOT NULL

-------------------------------------------------------------------

---- 5/3/2015

--insert into InvestigatePossibility SELECT ID , null , ReservBuchtID , null ,null , NewPostContactID FROM [dbo].[ChangeLocation] where ReservBuchtID is not null

--ALTER TABLE dbo.[ChangeLocation] DROP CONSTRAINT FK_ChangeLocation_Bucht1; 
--ALTER TABLE dbo.[ChangeLocation] DROP CONSTRAINT FK_ChangeLocation_PostContact; 
--alter Table [ChangeLocation] drop column ReservBuchtID,NewPostContactID,NewCabinetInputID


--insert into InvestigatePossibility SELECT ReqeustID , null , BuchtID , null ,null , PostContactID FROM [dbo].[E1Link]  join Request on Request.ID = [E1Link].ReqeustID  where BuchtID is not null and Request.RequestTypeID = 93
--insert into InvestigatePossibility SELECT RequestID , null , BuchtID , null ,null , PostContactID FROM [dbo].[SpecialWire]  join Request on Request.ID = [SpecialWire].RequestID  where BuchtID is not null

--ALTER TABLE dbo.[SpecialWire] DROP CONSTRAINT FK_PrivateWire_Bucht; 
--ALTER TABLE dbo.[SpecialWire] DROP CONSTRAINT FK_PrivateWire_PostContact; 
--ALTER TABLE dbo.[SpecialWire] DROP CONSTRAINT FK_PrivateWire_CabinetInput; 
--alter Table [SpecialWire] drop column PostContactID,CabinetInputID,BuchtID

--------------------------------------------------------


---- 5/6/2015


--alter table dbo.TranslationOpticalCabinetToNormal add  TransferWaitingList bit

--update dbo.TranslationOpticalCabinetToNormal set TransferWaitingList = 0

--alter table dbo.TranslationOpticalCabinetToNormal alter column TransferWaitingList bit NOT NULL



--alter table dbo.TranslationOpticalCabinetToNormal add  TransferBrokenPostContact bit

--update dbo.TranslationOpticalCabinetToNormal set TransferBrokenPostContact = 0

--alter table dbo.TranslationOpticalCabinetToNormal alter column TransferBrokenPostContact bit NOT NULL


-----5/9/2015




----------------------------------------
--DROP TABLE [dbo].[ExchangeCabinetInputConncetions]


--SET ANSI_NULLS ON
--GO

--SET QUOTED_IDENTIFIER ON
--GO

--CREATE TABLE [dbo].[ExchangeCabinetInputConncetions](
--	[ID] [bigint] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
--	[RequestID] [bigint] NOT NULL,
--	[CustomerID] [bigint] NULL,
--	[InstallAddressID] [bigint] NULL,
--	[CorrespondenceAddressID] [bigint] NULL,
--	[FromPostID] [int] NOT NULL,
--	[FromPostContactID] [bigint] NOT NULL,
--	[FromCabinetInputID] [bigint] NULL,
--	[FromTelephoneNo] [bigint] NULL,
--	[FromBucht] [bigint] NULL,
--	[ToPostID] [int] NOT NULL,
--	[ToPostConntactID] [bigint] NOT NULL,
--	[ToCabinetInputID] [bigint] NOT NULL,
--	[ToBucht] [bigint] NULL,
-- CONSTRAINT [PK_ExchangeCabinetConncetions] PRIMARY KEY CLUSTERED 
--(
--	[ID] ASC
--)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
--) ON [PRIMARY]

--GO

--ALTER TABLE [dbo].[ExchangeCabinetInputConncetions]  WITH CHECK ADD  CONSTRAINT [FK_ExchangeCabinetConncetions_Address] FOREIGN KEY([InstallAddressID])
--REFERENCES [dbo].[Address] ([ID])
--GO

--ALTER TABLE [dbo].[ExchangeCabinetInputConncetions] CHECK CONSTRAINT [FK_ExchangeCabinetConncetions_Address]
--GO

--ALTER TABLE [dbo].[ExchangeCabinetInputConncetions]  WITH CHECK ADD  CONSTRAINT [FK_ExchangeCabinetConncetions_Address1] FOREIGN KEY([CorrespondenceAddressID])
--REFERENCES [dbo].[Address] ([ID])
--GO

--ALTER TABLE [dbo].[ExchangeCabinetInputConncetions] CHECK CONSTRAINT [FK_ExchangeCabinetConncetions_Address1]
--GO

--ALTER TABLE [dbo].[ExchangeCabinetInputConncetions]  WITH CHECK ADD  CONSTRAINT [FK_ExchangeCabinetConncetions_Bucht] FOREIGN KEY([FromBucht])
--REFERENCES [dbo].[Bucht] ([ID])
--GO

--ALTER TABLE [dbo].[ExchangeCabinetInputConncetions] CHECK CONSTRAINT [FK_ExchangeCabinetConncetions_Bucht]
--GO

--ALTER TABLE [dbo].[ExchangeCabinetInputConncetions]  WITH CHECK ADD  CONSTRAINT [FK_ExchangeCabinetConncetions_Bucht1] FOREIGN KEY([ToBucht])
--REFERENCES [dbo].[Bucht] ([ID])
--GO

--ALTER TABLE [dbo].[ExchangeCabinetInputConncetions] CHECK CONSTRAINT [FK_ExchangeCabinetConncetions_Bucht1]
--GO

--ALTER TABLE [dbo].[ExchangeCabinetInputConncetions]  WITH CHECK ADD  CONSTRAINT [FK_ExchangeCabinetConncetions_CabinetInput2] FOREIGN KEY([FromCabinetInputID])
--REFERENCES [dbo].[CabinetInput] ([ID])
--GO

--ALTER TABLE [dbo].[ExchangeCabinetInputConncetions] CHECK CONSTRAINT [FK_ExchangeCabinetConncetions_CabinetInput2]
--GO

--ALTER TABLE [dbo].[ExchangeCabinetInputConncetions]  WITH CHECK ADD  CONSTRAINT [FK_ExchangeCabinetConncetions_CabinetInput3] FOREIGN KEY([ToCabinetInputID])
--REFERENCES [dbo].[CabinetInput] ([ID])
--GO

--ALTER TABLE [dbo].[ExchangeCabinetInputConncetions] CHECK CONSTRAINT [FK_ExchangeCabinetConncetions_CabinetInput3]
--GO

--ALTER TABLE [dbo].[ExchangeCabinetInputConncetions]  WITH CHECK ADD  CONSTRAINT [FK_ExchangeCabinetConncetions_Customer] FOREIGN KEY([CustomerID])
--REFERENCES [dbo].[Customer] ([ID])
--GO

--ALTER TABLE [dbo].[ExchangeCabinetInputConncetions] CHECK CONSTRAINT [FK_ExchangeCabinetConncetions_Customer]
--GO

--ALTER TABLE [dbo].[ExchangeCabinetInputConncetions]  WITH CHECK ADD  CONSTRAINT [FK_ExchangeCabinetConncetions_Post] FOREIGN KEY([FromPostID])
--REFERENCES [dbo].[Post] ([ID])
--GO

--ALTER TABLE [dbo].[ExchangeCabinetInputConncetions] CHECK CONSTRAINT [FK_ExchangeCabinetConncetions_Post]
--GO

--ALTER TABLE [dbo].[ExchangeCabinetInputConncetions]  WITH CHECK ADD  CONSTRAINT [FK_ExchangeCabinetConncetions_Post1] FOREIGN KEY([ToPostID])
--REFERENCES [dbo].[Post] ([ID])
--GO

--ALTER TABLE [dbo].[ExchangeCabinetInputConncetions] CHECK CONSTRAINT [FK_ExchangeCabinetConncetions_Post1]
--GO

--ALTER TABLE [dbo].[ExchangeCabinetInputConncetions]  WITH CHECK ADD  CONSTRAINT [FK_ExchangeCabinetConncetions_PostContact] FOREIGN KEY([FromPostContactID])
--REFERENCES [dbo].[PostContact] ([ID])
--GO

--ALTER TABLE [dbo].[ExchangeCabinetInputConncetions] CHECK CONSTRAINT [FK_ExchangeCabinetConncetions_PostContact]
--GO

--ALTER TABLE [dbo].[ExchangeCabinetInputConncetions]  WITH CHECK ADD  CONSTRAINT [FK_ExchangeCabinetConncetions_PostContact1] FOREIGN KEY([ToPostConntactID])
--REFERENCES [dbo].[PostContact] ([ID])
--GO

--ALTER TABLE [dbo].[ExchangeCabinetInputConncetions] CHECK CONSTRAINT [FK_ExchangeCabinetConncetions_PostContact1]
--GO

--ALTER TABLE [dbo].[ExchangeCabinetInputConncetions]  WITH CHECK ADD  CONSTRAINT [FK_ExchangeCabinetConncetions_Request] FOREIGN KEY([RequestID])
--REFERENCES [dbo].[Request] ([ID])
--GO

--ALTER TABLE [dbo].[ExchangeCabinetInputConncetions] CHECK CONSTRAINT [FK_ExchangeCabinetConncetions_Request]
--GO

--ALTER TABLE [dbo].[ExchangeCabinetInputConncetions]  WITH CHECK ADD  CONSTRAINT [FK_ExchangeCabinetConncetions_Telephone] FOREIGN KEY([FromTelephoneNo])
--REFERENCES [dbo].[Telephone] ([TelephoneNo])
--GO

--ALTER TABLE [dbo].[ExchangeCabinetInputConncetions] CHECK CONSTRAINT [FK_ExchangeCabinetConncetions_Telephone]
--GO



---------------------------


--DROP VIEW [dbo].ViewReservBucht


--CREATE VIEW ViewReservBucht AS
--select DISTINCT * from (
--SELECT        dbo.InvestigatePossibility.RequestID, dbo.InvestigatePossibility.BuchtID, dbo.InvestigatePossibility.PostContactID
--FROM            dbo.InvestigatePossibility INNER JOIN
--                         dbo.Request ON dbo.InvestigatePossibility.RequestID = dbo.Request.ID
--WHERE        (dbo.Request.EndDate IS NULL) AND (Request.IsCancelation = 0)
--union
--SELECT        dbo.TranslationOpticalCabinetToNormalConncetions.RequestID, dbo.TranslationOpticalCabinetToNormalConncetions.ToBucht, 
--                         dbo.TranslationOpticalCabinetToNormalConncetions.ToPostConntactID
--FROM            dbo.TranslationOpticalCabinetToNormalConncetions INNER JOIN
--                         dbo.Request ON dbo.TranslationOpticalCabinetToNormalConncetions.RequestID = dbo.Request.ID
--WHERE        (dbo.Request.EndDate IS NULL) AND (Request.IsCancelation = 0)
--union
--SELECT        dbo.ExchangeCabinetInputConncetions.RequestID, dbo.ExchangeCabinetInputConncetions.ToBucht, dbo.ExchangeCabinetInputConncetions.ToPostConntactID
--FROM            dbo.ExchangeCabinetInputConncetions INNER JOIN
--                         dbo.Request ON dbo.ExchangeCabinetInputConncetions.RequestID = dbo.Request.ID
--WHERE        (dbo.Request.EndDate IS NULL) AND (Request.IsCancelation = 0)
--union
--SELECT        Request.ID, null , TranslationPost.NewPostContactID
--FROM            Request INNER JOIN
--                         TranslationPost ON Request.ID = TranslationPost.RequestID
--WHERE        (Request.EndDate IS NULL) AND (TranslationPost.OverallTransfer = 0) AND (Request.IsCancelation = 0)
--union
--SELECT        Request.ID, NULL AS BuchtID, PostContact.ID AS PostContactID
--FROM            Request INNER JOIN
--                         TranslationPost ON Request.ID = TranslationPost.RequestID INNER JOIN
--                         Post ON TranslationPost.NewPostID = Post.ID INNER JOIN
--                         PostContact ON Post.ID = PostContact.PostID
--WHERE        (Request.EndDate IS NULL) AND (TranslationPost.OverallTransfer = 1) AND (Request.IsCancelation = 0)
--union
--SELECT        TranslationPostInput.RequestID, Bucht.ID, TranslationPostInputConnections.NewConnectionID
--FROM            TranslationPostInputConnections INNER JOIN
--                         TranslationPostInput ON TranslationPostInputConnections.RequestID = TranslationPostInput.RequestID INNER JOIN
--                         Request ON TranslationPostInput.RequestID = Request.ID INNER JOIN
--                         Bucht ON TranslationPostInputConnections.CabinetInputID = Bucht.CabinetInputID
--WHERE        (Request.EndDate IS NULL) AND (Request.IsCancelation = 0)
--union
--SELECT        TranslationPCMToNormal.ID, Bucht.ID AS Expr1, TranslationPCMToNormal.NewPostContactID
--FROM            TranslationPCMToNormal INNER JOIN
--                         Request ON TranslationPCMToNormal.ID = Request.ID LEFT OUTER JOIN
--                         Bucht ON TranslationPCMToNormal.CabinetInputID = Bucht.CabinetInputID
--WHERE        (Request.EndDate IS NULL) AND (Request.IsCancelation = 0)
--union
--SELECT        Request.ID, BuchtSwitching.NewBuchtID, BuchtSwitching.PostContactID
--FROM            BuchtSwitching INNER JOIN
--                         Request ON BuchtSwitching.ID = Request.ID
--WHERE        (Request.EndDate IS NULL) AND (Request.IsCancelation = 0)
--)as T

---------------------------

--alter table dbo.ExchangeCabinetInput add  [Type] tinyint

--update dbo.ExchangeCabinetInput set [Type] = 1

--alter table dbo.ExchangeCabinetInput alter column [Type] tinyint NOT NULL




--alter table dbo.ExchangeCabinetInput add  TransferWaitingList bit

--update dbo.ExchangeCabinetInput set TransferWaitingList = 0

--alter table dbo.ExchangeCabinetInput alter column TransferWaitingList bit NOT NULL



--alter table dbo.ExchangeCabinetInput add  TransferBrokenPostContact bit

--update dbo.ExchangeCabinetInput set TransferBrokenPostContact = 0

--alter table dbo.ExchangeCabinetInput alter column TransferBrokenPostContact bit NOT NULL


--alter table dbo.ExchangeCabinetInput alter column FromNewCabinetInputID bigint  NULL
--alter table dbo.ExchangeCabinetInput alter column FromOldCabinetInputID bigint  NULL
--alter table dbo.ExchangeCabinetInput alter column ToNewCabinetInputID bigint  NULL
--alter table dbo.ExchangeCabinetInput alter column ToOldCabinetInputID bigint  NULL


----- 5/18/2015

--alter table dbo.E1Link add  InvestigatePossibilityID bigint

--ALTER TABLE [dbo].[E1Link]  WITH CHECK ADD  CONSTRAINT [FK_E1Link_InvestigatePossibility] FOREIGN KEY(InvestigatePossibilityID)
--REFERENCES [dbo].[InvestigatePossibility] ([ID])
--GO

--insert into InvestigatePossibility SELECT RequestID , null , BuchtID , null ,null , PostContactID FROM [dbo].[ChangeLocationSpecialWire]  join Request on Request.ID = [ChangeLocationSpecialWire].RequestID  where BuchtID is not null and Request.RequestTypeID = 78
--ALTER TABLE dbo.[ChangeLocationSpecialWire] DROP CONSTRAINT FK_ChangeLocationSpecialWire_PostContact1; 
--ALTER TABLE dbo.[ChangeLocationSpecialWire] DROP CONSTRAINT FK_ChangeLocationSpecialWire_Bucht2; 
--alter Table ChangeLocationSpecialWire drop column PostContactID,CabinetInputID,BuchtID


----5/23/2015

--drop table [SavedLog]
--CREATE TABLE [dbo].[SavedLog](
--	[ID] [bigint] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
--	[TableName] [nvarchar](50) NULL,
--	[InsertDate] [smalldatetime] NULL,
--	[UserID] [int] NULL,
--	[IdentityName] [nvarchar](50) NULL,
--	[IdentityValue] [nvarchar](50) NULL,
--	[RequestID] [bigint] NULL,
--	[Changes] [xml] NULL,
-- CONSTRAINT [PK_SavedLog] PRIMARY KEY CLUSTERED 
--(
--	[ID] ASC
--)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
--) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

--GO


----5/23/2015


--alter table dbo.[E1] add  DesignDirectorFileID uniqueidentifier null
--ALTER TABLE [dbo].[E1]  WITH CHECK ADD  CONSTRAINT [FK_E1_DocumentsFile] FOREIGN KEY([DesignDirectorFileID])
--REFERENCES [dbo].[DocumentsFile] ([stream_id])
--ON UPDATE SET NULL
--ON DELETE SET NULL
--GO

--alter table dbo.[E1] add  DesignDirectorDescription  [nvarchar](200) NULL



--alter table dbo.[E1] add  TransferDepartmentFileID uniqueidentifier null
--ALTER TABLE [dbo].[E1]  WITH CHECK ADD  CONSTRAINT [FK_E1_DocumentsFile1] FOREIGN KEY([DesignDirectorFileID])
--REFERENCES [dbo].[DocumentsFile] ([stream_id])
--GO

--alter table dbo.[E1] add  CommandCircuitFileID uniqueidentifier null
--ALTER TABLE [dbo].[E1]  WITH CHECK ADD  CONSTRAINT [FK_E1_DocumentsFile2] FOREIGN KEY([CommandCircuitFileID])
--REFERENCES [dbo].[DocumentsFile] ([stream_id])
--GO

--alter table dbo.[E1] add  TransferDepartmentDescription  [nvarchar](200) NULL



--alter table dbo.[E1] add  PowerFileID uniqueidentifier null
--ALTER TABLE [dbo].[E1]  WITH CHECK ADD  CONSTRAINT [FK_E1_DocumentsFile3] FOREIGN KEY([PowerFileID])
--REFERENCES [dbo].[DocumentsFile] ([stream_id])

--GO

--alter table dbo.[E1] add  PowerDescription  [nvarchar](200) NULL


---- 5/25/2015
--alter table dbo.[E1] add  InstallingDepartmentDescription  [nvarchar](200) NULL

--alter table dbo.[E1] add  MonitoringDepartmentDescription  [nvarchar](200) NULL

--alter table dbo.[E1] add  TargetInstallAddressID  bigint NULL
--ALTER TABLE [dbo].[E1]  WITH CHECK ADD  CONSTRAINT [FK_E1_Address2] FOREIGN KEY([TargetInstallAddressID])
--REFERENCES [dbo].[Address] ([ID])

----5/27/2015

--alter table dbo.[E1] add  CableDesignFileID uniqueidentifier null
--ALTER TABLE [dbo].[E1]  WITH CHECK ADD  CONSTRAINT [FK_E1_DocumentsFile4] FOREIGN KEY(CableDesignFileID)
--REFERENCES [dbo].[DocumentsFile] ([stream_id])

--alter table dbo.[E1] add  SwitchFileID uniqueidentifier null
--ALTER TABLE [dbo].[E1]  WITH CHECK ADD  CONSTRAINT [FK_E1_DocumentsFile5] FOREIGN KEY(SwitchFileID)
--REFERENCES [dbo].[DocumentsFile] ([stream_id])

--alter table dbo.[Cabinet] add  ApplyQuota bit null
--update dbo.[Cabinet] set ApplyQuota = 1

--alter table dbo.[Cabinet] alter column ApplyQuota bit NOT NULL

----5/31/2015


--ALTER TABLE [dbo].[Telephone]  WITH CHECK ADD  CONSTRAINT [FK_Telephone_CauseOfCut] FOREIGN KEY([CauseOfCutID])
--REFERENCES [dbo].[CauseOfCut] ([ID])
--GO


--23/6/2015

alter table dbo.[ADSLService] add  ForWireless bit null
update dbo.[ADSLService] set ForWireless = 0
alter table dbo.[ADSLService] alter column ForWireless bit NOT NULL

alter table dbo.ADSLModem add  ForWireless bit null
update dbo.ADSLModem set ForWireless = 0
alter table dbo.ADSLModem alter column ForWireless bit NOT NULL


--27/6/2015
alter table dbo.TranslationPost  add  TransferWaitingList bit null
update dbo.TranslationPost set TransferWaitingList = 0
alter table dbo.TranslationPost alter column TransferWaitingList bit NOT NULL

alter table dbo.TranslationPost  add  TransferBrokenPostContact bit null
update dbo.TranslationPost set TransferBrokenPostContact = 0
alter table dbo.TranslationPost alter column TransferBrokenPostContact bit NOT NULL

alter table dbo.E1  add LineType tinyint null

alter table dbo.SpecialConditions  DROP COLUMN IsOptical
alter table dbo.SpecialConditions  add IsOpticalE1 bit null
update dbo.SpecialConditions set IsOpticalE1 = 0
alter table dbo.SpecialConditions alter column IsOpticalE1 bit NOT NULL

alter table dbo.SpecialConditions  add IsE1PTP bit null
update dbo.SpecialConditions set IsE1PTP = 0
alter table dbo.SpecialConditions alter column IsE1PTP bit NOT NULL


--28/06/2015
ALTER TABLE [dbo].[SpaceAndPower] DROP CONSTRAINT [FK_SpaceAndPower_SpaceAndPowerCustomer]

ALTER TABLE dbo.[SpaceAndPower]
   ALTER COLUMN SpaceAndPowerCustomerID bigint

ALTER TABLE [dbo].[SpaceAndPower]  WITH CHECK ADD  CONSTRAINT [FK_SpaceAndPower_Customer] FOREIGN KEY([SpaceAndPowerCustomerID])
REFERENCES [dbo].[Customer] ([ID])
ALTER TABLE [dbo].[SpaceAndPower] CHECK CONSTRAINT [FK_SpaceAndPower_Customer]


-----