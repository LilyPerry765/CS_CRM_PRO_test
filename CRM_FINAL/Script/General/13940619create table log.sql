USE [CRM]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Log](
	[ID] [bigint] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[TableName] [nvarchar](50) NULL,
	[InsertDate] [smalldatetime] NULL,
	[UserID] [int] NULL,
	[IdentityName] [nvarchar](50) NULL,
	[IdentityValue] [nvarchar](50) NULL,
	[RequestID] [bigint] NULL,
	[Changes] [xml] NULL,
 CONSTRAINT [PK_Log] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO