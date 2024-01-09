USE [CRM]
GO
/****** Object:  Table [dbo].[ReportTemplate]    Script Date: 1/10/2016 9:48:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ReportTemplate](
	[ID] [int] NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Template] [varbinary](max) NULL,
	[IconName] [nvarchar](127) NULL,
	[Category] [nvarchar](127) NULL,
	[TimeStamp] [char](15) NULL,
	[UserControlName] [nvarchar](227) NULL,
	[IsVisible] [bit] NULL,
 CONSTRAINT [PK_ReportTemplate] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[ReportTemplate] ADD  CONSTRAINT [DF_ReportTemplate_TimeStamp]  DEFAULT (left(newid(),(15))) FOR [TimeStamp]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'شناسه' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportTemplate', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'عنوان گزارش' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportTemplate', @level2type=N'COLUMN',@level2name=N'Title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'الگوی گزارش' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportTemplate', @level2type=N'COLUMN',@level2name=N'Template'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'نام آیکون' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportTemplate', @level2type=N'COLUMN',@level2name=N'IconName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'گروه' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportTemplate', @level2type=N'COLUMN',@level2name=N'Category'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'آیا گزارش برای کاربران قابل مشاهده باشد یا خیر' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ReportTemplate', @level2type=N'COLUMN',@level2name=N'IsVisible'
GO
