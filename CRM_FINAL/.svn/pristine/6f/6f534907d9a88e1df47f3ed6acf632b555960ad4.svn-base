IF NOT EXISTS (SELECT * FROM RequestType WHERE ID = 118)
	BEGIN
		SET IDENTITY_INSERT RequestType ON
			INSERT INTO RequestType
			(
				ID,Title,Abonman,InsertDate
			)
			values 
			(
				118,N'تغییر وضعیت رند بودن تلفن',0,GETDATE()
			)
			
			PRINT 'INSERTED'
		SET IDENTITY_INSERT RequestType OFF
	END
ELSE
	PRINT 'EXISTED!!!'