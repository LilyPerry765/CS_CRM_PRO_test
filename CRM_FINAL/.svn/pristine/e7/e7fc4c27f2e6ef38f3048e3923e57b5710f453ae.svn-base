USE [CRM]
GO

/****** Object:  Table [dbo].[GSMSimCard]    Script Date: 1/26/2016 12:17:45 PM ******/
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

ALTER TABLE [dbo].[GSMSimCard]  WITH CHECK ADD  CONSTRAINT [FK_GSMSimCard_Telephone] FOREIGN KEY([TelephoneNo])
REFERENCES [dbo].[Telephone] ([TelephoneNo])
GO

ALTER TABLE [dbo].[GSMSimCard] CHECK CONSTRAINT [FK_GSMSimCard_Telephone]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'تلفن' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GSMSimCard', @level2type=N'COLUMN',@level2name=N'TelephoneNo'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'سریال سیم کارت' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GSMSimCard', @level2type=N'COLUMN',@level2name=N'Code'
GO


