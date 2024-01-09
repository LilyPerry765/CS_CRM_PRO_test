USE [CRM]
GO

/****** Object:  Table [dbo].[TransferDepartmentOffice]    Script Date: 7/14/2015 12:39:47 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TransferDepartmentOffice](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RequestID] [bigint] NOT NULL,
	[TransferDepartmentFileID] [uniqueidentifier] NOT NULL,
	[InsertDate] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_TransferDepartmentOffice] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[TransferDepartmentOffice]  WITH CHECK ADD  CONSTRAINT [FK_TransferDepartmentOffice_Request] FOREIGN KEY([RequestID])
REFERENCES [dbo].[Request] ([ID])
GO

ALTER TABLE [dbo].[TransferDepartmentOffice] CHECK CONSTRAINT [FK_TransferDepartmentOffice_Request]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'شناسه' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TransferDepartmentOffice', @level2type=N'COLUMN',@level2name=N'ID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'شناسه درخواست' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TransferDepartmentOffice', @level2type=N'COLUMN',@level2name=N'RequestID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'شناسه فایل' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TransferDepartmentOffice', @level2type=N'COLUMN',@level2name=N'TransferDepartmentFileID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'تاریخ ثبت' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TransferDepartmentOffice', @level2type=N'COLUMN',@level2name=N'InsertDate'
GO






----------------------------

USE [CRM]
GO

/****** Object:  Table [dbo].[CableDesignOffice]    Script Date: 7/14/2015 12:41:13 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CableDesignOffice](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RequestID] [bigint] NOT NULL,
	[CableDesignFileID] [uniqueidentifier] NOT NULL,
	[InsertDate] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_CableDesignOffice] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[CableDesignOffice]  WITH CHECK ADD  CONSTRAINT [FK_CableDesignOffice_Request] FOREIGN KEY([RequestID])
REFERENCES [dbo].[Request] ([ID])
GO

ALTER TABLE [dbo].[CableDesignOffice] CHECK CONSTRAINT [FK_CableDesignOffice_Request]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'شناسه' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CableDesignOffice', @level2type=N'COLUMN',@level2name=N'ID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'شناسه درخواست' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CableDesignOffice', @level2type=N'COLUMN',@level2name=N'RequestID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'شناسه فایل' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CableDesignOffice', @level2type=N'COLUMN',@level2name=N'CableDesignFileID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'تاریخ ثبت' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CableDesignOffice', @level2type=N'COLUMN',@level2name=N'InsertDate'
GO




---------------------------------------------


USE [CRM]
GO

/****** Object:  Table [dbo].[PowerOffice]    Script Date: 7/14/2015 12:41:46 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PowerOffice](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RequestID] [bigint] NOT NULL,
	[PowerFileID] [uniqueidentifier] NOT NULL,
	[InsertDate] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_PowerOffice] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[PowerOffice]  WITH CHECK ADD  CONSTRAINT [FK_PowerOffice_Request] FOREIGN KEY([RequestID])
REFERENCES [dbo].[Request] ([ID])
GO

ALTER TABLE [dbo].[PowerOffice] CHECK CONSTRAINT [FK_PowerOffice_Request]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'شناسه' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PowerOffice', @level2type=N'COLUMN',@level2name=N'ID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'شناسه درخواست' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PowerOffice', @level2type=N'COLUMN',@level2name=N'RequestID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'شناسه فایل' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PowerOffice', @level2type=N'COLUMN',@level2name=N'PowerFileID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'تاریخ ثبت' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PowerOffice', @level2type=N'COLUMN',@level2name=N'InsertDate'
GO





--------------------------------------------------

USE [CRM]
GO

/****** Object:  Table [dbo].[SwitchOffice]    Script Date: 7/14/2015 12:42:09 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SwitchOffice](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RequestID] [bigint] NOT NULL,
	[SwitchFileID] [uniqueidentifier] NOT NULL,
	[InsertDate] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_SwitchOffice] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SwitchOffice]  WITH CHECK ADD  CONSTRAINT [FK_SwitchOffice_Request] FOREIGN KEY([RequestID])
REFERENCES [dbo].[Request] ([ID])
GO

ALTER TABLE [dbo].[SwitchOffice] CHECK CONSTRAINT [FK_SwitchOffice_Request]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'شناسه' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SwitchOffice', @level2type=N'COLUMN',@level2name=N'ID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'شناسه درخواست' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SwitchOffice', @level2type=N'COLUMN',@level2name=N'RequestID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'شناسه فایل' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SwitchOffice', @level2type=N'COLUMN',@level2name=N'SwitchFileID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'تاریخ ثبت' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SwitchOffice', @level2type=N'COLUMN',@level2name=N'InsertDate'
GO





