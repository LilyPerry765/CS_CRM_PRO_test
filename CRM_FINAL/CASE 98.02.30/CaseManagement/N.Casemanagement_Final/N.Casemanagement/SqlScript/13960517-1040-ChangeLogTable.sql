
/*
 Log Table
*/
IF  NOT EXISTS (SELECT NULL FROM INFORMATION_SCHEMA.[COLUMNS] WHERE TABLE_NAME = 'Log' AND COLUMN_NAME = 'OldData')
		ALTER Table [Log] ADD OldData nvarchar(max)
