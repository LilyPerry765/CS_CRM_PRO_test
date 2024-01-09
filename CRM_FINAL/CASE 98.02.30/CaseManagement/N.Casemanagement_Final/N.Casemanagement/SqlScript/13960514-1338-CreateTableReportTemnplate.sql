IF NOT EXISTS (SELECT *  FROM  SYS.TABLES WHERE  NAME = 'ReportTemplate')
	CREATE TABLE [dbo].[ReportTemplate]
	(
		[ID] [int]  NOT NULL,
		[Template] [varbinary](max) NULL,
		[Title] [nvarchar](100) NULL,
	 CONSTRAINT [PK_JSReportTemplate] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
 
ELSE 
 Print 'این جدول از  قبل موجود است '
 
 
 