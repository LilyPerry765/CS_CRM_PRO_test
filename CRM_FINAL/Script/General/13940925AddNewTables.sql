USE [CRM]
GO
/****** Object:  Table [dbo].[ExchangeGSM]    Script Date: 12/20/2015 1:40:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExchangeGSM](
	[ID] [bigint] NOT NULL,
	[Type] [tinyint] NOT NULL,
	[FromTelephoneNo] [bigint] NULL,
	[ToTelephoneNo] [bigint] NULL,
	[OldCabinetID] [int] NOT NULL,
	[MDFAccomplishmentDate] [smalldatetime] NULL,
	[MDFAccomplishmentTime] [nchar](10) NULL,
	[NetworkAccomplishmentDate] [smalldatetime] NULL,
	[NetworkAccomplishmentTime] [nchar](10) NULL,
	[ChoiceNumberAccomplishmentDate] [smalldatetime] NULL,
	[ChoiceNumberAccomplishmentTime] [nchar](10) NULL,
	[SwitchAccomplishmentDate] [smalldatetime] NULL,
	[SwitchAccomplishmentTime] [nchar](10) NULL,
	[CompletionDate] [smalldatetime] NULL,
 CONSTRAINT [PK_ExchangeGSM] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ExchangeGSMConnections]    Script Date: 12/20/2015 1:40:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExchangeGSMConnections](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RequestID] [bigint] NOT NULL,
	[BuchtID] [bigint] NULL,
	[BuchtStatus] [tinyint] NULL,
	[CabinetID] [int] NULL,
	[CabinetInputID] [bigint] NULL,
	[PostID] [int] NULL,
	[PostContactID] [bigint] NULL,
	[PostContactStatus] [tinyint] NULL,
	[FromTelephoneNo] [bigint] NULL,
	[FromSwitchPreCodeID] [int] NULL,
	[FromTelephoneStatus] [tinyint] NULL,
	[ToTelephoneNo] [bigint] NULL,
	[ToSwitchPreCodeID] [int] NULL,
	[ToTelephoneStatus] [tinyint] NULL,
 CONSTRAINT [PK_ExchangeGSMConnections] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GSMSimCard]    Script Date: 12/20/2015 1:40:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GSMSimCard](
	[TelephoneNo] [bigint] NOT NULL,
	[Code] [varchar](50) NULL,
 CONSTRAINT [PK_GSMSimCard_1] PRIMARY KEY CLUSTERED 
(
	[TelephoneNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_GSMSimCard] UNIQUE NONCLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[ExchangeGSM]  WITH CHECK ADD  CONSTRAINT [FK_ExchangeGSM_Request] FOREIGN KEY([ID])
REFERENCES [dbo].[Request] ([ID])
GO
ALTER TABLE [dbo].[ExchangeGSM] CHECK CONSTRAINT [FK_ExchangeGSM_Request]
GO
ALTER TABLE [dbo].[ExchangeGSM]  WITH CHECK ADD  CONSTRAINT [FK_ExchangeGSM_Telephone] FOREIGN KEY([FromTelephoneNo])
REFERENCES [dbo].[Telephone] ([TelephoneNo])
GO
ALTER TABLE [dbo].[ExchangeGSM] CHECK CONSTRAINT [FK_ExchangeGSM_Telephone]
GO
ALTER TABLE [dbo].[ExchangeGSM]  WITH CHECK ADD  CONSTRAINT [FK_ExchangeGSM_Telephone1] FOREIGN KEY([ToTelephoneNo])
REFERENCES [dbo].[Telephone] ([TelephoneNo])
GO
ALTER TABLE [dbo].[ExchangeGSM] CHECK CONSTRAINT [FK_ExchangeGSM_Telephone1]
GO
ALTER TABLE [dbo].[ExchangeGSMConnections]  WITH CHECK ADD  CONSTRAINT [FK_ExchangeGSMConnections_Bucht] FOREIGN KEY([BuchtID])
REFERENCES [dbo].[Bucht] ([ID])
GO
ALTER TABLE [dbo].[ExchangeGSMConnections] CHECK CONSTRAINT [FK_ExchangeGSMConnections_Bucht]
GO
ALTER TABLE [dbo].[ExchangeGSMConnections]  WITH CHECK ADD  CONSTRAINT [FK_ExchangeGSMConnections_Cabinet] FOREIGN KEY([CabinetID])
REFERENCES [dbo].[Cabinet] ([ID])
GO
ALTER TABLE [dbo].[ExchangeGSMConnections] CHECK CONSTRAINT [FK_ExchangeGSMConnections_Cabinet]
GO
ALTER TABLE [dbo].[ExchangeGSMConnections]  WITH CHECK ADD  CONSTRAINT [FK_ExchangeGSMConnections_CabinetInput] FOREIGN KEY([CabinetInputID])
REFERENCES [dbo].[CabinetInput] ([ID])
GO
ALTER TABLE [dbo].[ExchangeGSMConnections] CHECK CONSTRAINT [FK_ExchangeGSMConnections_CabinetInput]
GO
ALTER TABLE [dbo].[ExchangeGSMConnections]  WITH CHECK ADD  CONSTRAINT [FK_ExchangeGSMConnections_Post] FOREIGN KEY([PostID])
REFERENCES [dbo].[Post] ([ID])
GO
ALTER TABLE [dbo].[ExchangeGSMConnections] CHECK CONSTRAINT [FK_ExchangeGSMConnections_Post]
GO
ALTER TABLE [dbo].[ExchangeGSMConnections]  WITH CHECK ADD  CONSTRAINT [FK_ExchangeGSMConnections_PostContact] FOREIGN KEY([PostContactID])
REFERENCES [dbo].[PostContact] ([ID])
GO
ALTER TABLE [dbo].[ExchangeGSMConnections] CHECK CONSTRAINT [FK_ExchangeGSMConnections_PostContact]
GO
ALTER TABLE [dbo].[ExchangeGSMConnections]  WITH CHECK ADD  CONSTRAINT [FK_ExchangeGSMConnections_Telephone] FOREIGN KEY([FromTelephoneNo])
REFERENCES [dbo].[Telephone] ([TelephoneNo])
GO
ALTER TABLE [dbo].[ExchangeGSMConnections] CHECK CONSTRAINT [FK_ExchangeGSMConnections_Telephone]
GO
ALTER TABLE [dbo].[ExchangeGSMConnections]  WITH CHECK ADD  CONSTRAINT [FK_ExchangeGSMConnections_Telephone1] FOREIGN KEY([ToTelephoneNo])
REFERENCES [dbo].[Telephone] ([TelephoneNo])
GO
ALTER TABLE [dbo].[ExchangeGSMConnections] CHECK CONSTRAINT [FK_ExchangeGSMConnections_Telephone1]
GO
ALTER TABLE [dbo].[GSMSimCard]  WITH CHECK ADD  CONSTRAINT [FK_GSMSimCard_Telephone] FOREIGN KEY([TelephoneNo])
REFERENCES [dbo].[Telephone] ([TelephoneNo])
GO
ALTER TABLE [dbo].[GSMSimCard] CHECK CONSTRAINT [FK_GSMSimCard_Telephone]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'شناسه درخواست' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ExchangeGSM', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'تاریخ انجام' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ExchangeGSM', @level2type=N'COLUMN',@level2name=N'MDFAccomplishmentDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'تاریخ انجام' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ExchangeGSM', @level2type=N'COLUMN',@level2name=N'MDFAccomplishmentTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'تلفن' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GSMSimCard', @level2type=N'COLUMN',@level2name=N'TelephoneNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'سریال سیم کارت' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GSMSimCard', @level2type=N'COLUMN',@level2name=N'Code'
GO
