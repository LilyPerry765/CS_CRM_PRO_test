IF NOT EXISTS
 (
		SELECT * 
		FROM sys.indexes i  
		INNER JOIN sys.objects o  ON o.object_id = i.object_id 
		WHERE
		o.name ='ReportTemplate' AND i.name ='IX_ReportTemplate'
 ) 
	CREATE UNIQUE NONCLUSTERED INDEX IX_ReportTemplate ON dbo.ReportTemplate(Title) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
 
		
		
		
