SET IDENTITY_INSERT [Users] ON
 

IF EXISTS (SELECT *  FROM [Users] WHERE UserId = -1)
	PRINT  N'ò«—»— „Ê—œ ‰Ÿ— ÊÃÊœ œ«—œ'
ELSE 
	INSERT INTO [Users](UserId,Username,DisplayName,[Source],PasswordHash,Password,InsertDate,InsertUserId,IsActive,PasswordSalt)
	VALUES (-1,N'Windows Service',N'Windows Service','site','--','--',GetDate(),1,1,N'')


SET IDENTITY_INSERT [Users] OFF 
 
