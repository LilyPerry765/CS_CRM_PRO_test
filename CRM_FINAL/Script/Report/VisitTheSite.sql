

/****** Object:  StoredProcedure [dbo].[VisitTheSite]    Script Date: 2/18/2015 1:32:45 PM ******/
DROP PROCEDURE [dbo].[VisitTheSite]
GO

/****** Object:  StoredProcedure [dbo].[VisitTheSite]    Script Date: 2/18/2015 1:32:45 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--exec VisitTheSite '9208040002'
CREATE procedure [dbo].[VisitTheSite](@RequestIDs VARCHAR(MAX))
AS
BEGIN
	
DECLARE @query VARCHAR(MAX) = ''
DECLARE @RequestTypeId INT 
DECLARE @resultTable TABLE (FirstName NVARCHAR(50),
							LastName NVARCHAR(50),
							TelephoneNo BIGINT ,
							NewAddress NVARCHAR(MAX),
							OldAddress NVARCHAR(MAX),
							NewPostalCode NVARCHAR(20),
							OldPostalCode NVARCHAR(20))


SELECT  @RequestTypeId = RequestTypeID 
FROM Request 
WHERE id IN (SELECT *
			 FROM ufnSplitList(@RequestIDs))
GROUP BY RequestTypeID 

SET @query = '
				;WITH RequestsList AS
					(
						SELECT *
						FROM ufnSplitList('''+@RequestIDs+''')
					)
			 '
SET @query = @query +'SELECT 
						FirstNameOrTitle FirstName,
						LastName,
						R.TelephoneNo,
						A1.AddressContent NewAddress,
						A2.AddressContent OldAddress,
						A1.PostalCode NewPostalCode,
						A2.PostalCode OldPostalCode
					  FROM dbo.Request R
					  LEFT JOIN dbo.Customer C ON C.ID = R.CustomerID 
					  '
SET @query = @query + 
--تغییر مکان داخل مرکز
   CASE WHEN @RequestTypeId  in(25,63) THEN 'LEFT JOIN dbo.ChangeLocation B ON B.ID = R.ID  
										LEFT JOIN [ADDRESS]  A1 ON A1.ID = B.NewInstallAddressID
										LEFT JOIN [ADDRESS]  A2 ON A2.ID = B.OldInstallAddressID'
--دايری,دایری مجدد
		WHEN @RequestTypeId in(1,53) THEN 'LEFT JOIN dbo.InstallRequest B ON B.RequestID = R.ID
										LEFT JOIN [ADDRESS]  A1 ON A1.ID = B.InstallAddressID
										LEFT JOIN [ADDRESS]  A2 ON A2.ID = B.InstallAddressID'
--تغییر مکان سیم خصوصی
		WHEN @RequestTypeId =  78 THEN 'LEFT JOIN dbo.ChangeLocationSpecialWire B ON B.RequestID = R.ID  
										LEFT JOIN [ADDRESS]  A1 ON A1.ID = B.InstallAddressID
										LEFT JOIN [ADDRESS]  A2 ON A2.ID = B.OLDInstallAddressID'
--سیم خصوصی نقاط دیگر
		WHEN @RequestTypeId IN(91,74) THEN 'LEFT JOIN dbo.SpecialWire B ON B.RequestID = R.ID  
										LEFT JOIN [ADDRESS]  A1 ON A1.ID = B.InstallAddressID
										LEFT JOIN [ADDRESS]  A2 ON A2.ID = B.InstallAddressID'
--لینک E1,E1(سیم)
		WHEN @RequestTypeId IN(93,71) THEN 'LEFT JOIN dbo.E1 B ON B.RequestID = R.ID
										LEFT JOIN [ADDRESS]  A1 ON A1.ID = B.InstallAddressID
										LEFT JOIN [ADDRESS]  A2 ON A2.ID = B.InstallAddressID'

	
END

SET @query = @query + '
	WHERE R.ID IN (Select * From RequestsList)'
	
INSERT @resultTable
EXEC (@query)
SELECT *
FROM @resultTable
END 


GO


