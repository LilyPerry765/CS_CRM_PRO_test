
IF EXISTS(SELECT NULL FROM sys.tables WHERE name = 'MessagePanel') 
	DROP TABLE dbo.[MessagePanel]
GO 

CREATE TABLE [MessagePanel]
(
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[IsSMS] [bit] NOT NULL
		CONSTRAINT [PK_MessagePanel] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)	
	WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]



