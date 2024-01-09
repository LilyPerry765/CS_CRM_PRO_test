USE [Casemanagement]
GO

/****** Object:  StoredProcedure [dbo].[usp_ActivityProvince]    Script Date: 2018/03/15 11:25:09 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[usp_ActivityProvince]	
AS
BEGIN
	
	DECLARE @Now_Date_Year_Shamsi INT = SUBSTRING( dbo.mi2sh( GETDATE() , 1 ) , 1 , 4 )
	DECLARE @Last_Year_Shamsi VARCHAR(4) = ( @Now_Date_Year_Shamsi - 1 )
	DECLARE @Last_Date DATE = ( dbo.sh2mi( CONCAT( @Last_Year_Shamsi , '/12/29' ) ) )

	SELECT 
		ar.ActivityCode ,
		act.CodeName ,
		pr.Name ,
		COUNT(*) CNT ,
		SUM( ar.TotalLeakage ) TotalLeakage ,
		SUM( ar.Recovered ) Recovered

	FROM
		ActivityRequest ar 
	LEFT JOIN
		Activity act ON ar.ActivityID = act.ID
	LEFT JOIN
		Province pr ON ar.ProvinceID = pr.ID

	WHERE
		ar.IsDeleted != 1 
		AND ar.EndDate IS NOT NULL 
		AND ar.DiscoverLeakDate  > @Last_Date

	GROUP BY 
		ar.ActivityCode ,
		act.CodeName , 
		ar.PRovinceID ,
		pr.Name

	ORDER BY
		ar.ActivityCode ,
		ar.ProvinceID		

END

GO

