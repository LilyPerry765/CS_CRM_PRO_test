
IF OBJECT_ID('[dbo].[usp_GetStaleActivityRequest]','P') IS NOT NULL 
	DROP PROCEDURE [dbo].[usp_GetStaleActivityRequest]
GO

CREATE PROCEDURE [dbo].[usp_GetStaleActivityRequest]  
WITH ENCRYPTION 
AS
BEGIN

	DECLARE @Selected_Year TINYINT = ( SELECT SUBSTRING(dbo.mi2sh(GETDATE(),1),3,2) )
	DECLARE @TMPTable TABLE
	(	
		ProvinceID INT,
		ActivityRequestID BIGINT,		
		StatusID INT,
		RoleID INT,
		UserID INT,
		DisplayName NVARCHAR(100),
		MobileNo VARCHAR(15)
	)

	;WITH
	MessageLog_Raw AS
	(
		SELECT *, ROW_NUMBER() OVER(PARTITION BY ActivityRequestID, MessageID ORDER BY ID DESC) RepID
		FROM MessageLog 
		WHERE DATEDIFF (DAY, InsertDate, GETDATE()) <= 14 AND IsSent = 1
	)
	,UserRole_Raw AS
	(
		SELECT u.ProvinceID, ur.RoleID, u.UserID, u.DisplayName, u.[Rank], u.MobileNo,
			CASE WHEN ur.RoleId = 13 THEN 6 WHEN ur.RoleId = 15 THEN 10 END Work_Flow_Status
		FROM
			UserRoles ur
		INNER JOIN
			Users u ON ur.UserId = u.UserId
		WHERE
			ur.RoleId IN ( 13, 15 )
			AND u.IsActive = 1
			AND u.IsDeleted = 0
			AND u.MobileNo IS NOT NULL
	)
	,ActivityRequest_Raw AS
	(
		SELECT 
			ar.ProvinceID, ar.ID ActivityRequestID, ar.StatusID, ar.CreatedDate, ar.EndDate, ar.SendDate, 
			ur.UserID, ur.DisplayName, ur.RoleID, ur.MobileNo, ml.ID MessageLogID, ml.InsertDate MessageInsertDate		
		FROM
			ActivityRequest ar
		LEFT JOIN 
			UserRole_Raw ur ON ar.ProvinceID = ur.ProvinceID AND ar.StatusID = ur.Work_Flow_Status
		LEFT JOIN 
			MessageLog_Raw ml ON ar.ID = ml.ActivityRequestID AND ur.UserID = ml.ReceiverUserID AND ml.RepID = 1
		WHERE
			LEN(ar.ID) >= 10
			AND DATEDIFF (DAY, ar.SendDate, GETDATE()) >= 14
			AND ar.StatusID IN ( 6 , 10 )			
			AND ar.EndDate IS NULL
			AND ar.ID LIKE CONCAT( @Selected_Year , '%' )
			AND ml.ID IS NULL
	)

	INSERT INTO @TMPTable( ProvinceID, ActivityRequestID, StatusID, RoleID, UserID, DisplayName, MobileNo )
	SELECT ProvinceID, ActivityRequestID, StatusID, RoleID, UserID, DisplayName, MobileNo
	FROM ActivityRequest_Raw

	INSERT INTO MessageLog
			(
			 InsertDate
			,ActivityRequestID
			,MessageID
			,SenderUserID
			,SenderUserName
			,ReceiverProvinceID
			,ReceiverUserID
			,ReceiverUserName
			,MobileNumber
			,TextSent
			,IsSent
			,IsDelivered
			)
	SELECT
			GETDATE()				InsertDate
			,t.ActivityRequestID	ActivityRequestID
			,0						MessageID
			,-1						SenderUserID
			,'Windows Service'		SenderUserName
			,t.ProvinceID			ReceiverProvinceID
			,t.UserID				ReceiverUserID
			,t.DisplayName			ReceiverUserName
			,t.MobileNo				MobileNumber
			,NULL					TextSent
			,1						IsSent
			,0						IsDelivered
	FROM
		@TMPTable t

	SELECT *
	FROM @TMPTable
	ORDER BY ProvinceID, StatusID, UserID

END

