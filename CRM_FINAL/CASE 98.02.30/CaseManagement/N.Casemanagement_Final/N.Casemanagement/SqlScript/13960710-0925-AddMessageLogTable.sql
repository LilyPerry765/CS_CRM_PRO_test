
IF EXISTS(SELECT NULL FROM sys.tables WHERE name = 'MessageLog') 
	DROP TABLE dbo.[MessageLog]
GO 

CREATE TABLE [MessageLog]
(
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[InsertDate] [datetime] NOT NULL,
	[ActivityRequestID] [bigint] NULL,
	[MessageID] [int] NOT NULL,
	[SenderUserID] [int] NOT NULL,
	[SenderUserName] [nvarchar] (255) NULL,
	[ReceiverProvinceID] [int] NOT NULL,
	[ReceiverUserID] [int] NOT NULL,
	[ReceiverUserName] [nvarchar] (255) NULL,
	[MobileNumber] [varchar] (15) NULL,
	[TextSent] [nvarchar] (1000) NULL,
	[IsSent] [bit] NOT NULL,
	[IsDelivered] [bit] NOT NULL
		CONSTRAINT [PK_MessageLog] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)	
	WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
