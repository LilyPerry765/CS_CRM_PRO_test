IF NOT EXISTS (SELECT * FROM RequestType WHERE ID = 119)
	BEGIN
		
		SET IDENTITY_INSERT RequestType ON

		INSERT INTO RequestType
		(
			ID,Title,Abonman,InsertDate
		)
		values
		(
			119,N'بازدید از محل بر اساس تلفن',0,Getdate()
		)

		SET IDENTITY_INSERT RequestType OFF
	END
ELSE
	PRINT 'this value is existed!'