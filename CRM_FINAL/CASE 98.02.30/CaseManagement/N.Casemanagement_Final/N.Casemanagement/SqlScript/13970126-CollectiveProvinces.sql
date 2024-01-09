USE [Casemanagement97]
GO

/****** Object:  StoredProcedure [dbo].[usp_NewCollectiveProvince]    Script Date: 2018/04/15 11:54:02 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE  PROCEDURE [dbo].[usp_NewCollectiveProvince]	 
AS
BEGIN

	DECLARE @Now_Date_Year_Shamsi INT = SUBSTRING( dbo.mi2sh( GETDATE() , 1 ) , 1 , 4 )
	DECLARE @Now_Date_Month_Shamsi INT = SUBSTRING( dbo.mi2sh( GETDATE() , 1 ) , 6 , 2 )
	DECLARE @ProvinceProgram_YearID INT = CASE
												WHEN @Now_Date_Month_Shamsi < 5 THEN ( SELECT ID FROM Year WHERE Year = ( @Now_Date_Year_Shamsi - 1 ) )
												WHEN @Now_Date_Month_Shamsi > 4 THEN ( SELECT ID FROM Year WHERE Year = @Now_Date_Year_Shamsi )
										  END
	
	;WITH
	TechnicalCte AS  
	 ( 
		SELECT ar.ProvinceID, COUNT(*) TechnicalCount   --تعداد تائيد فني
		FROM 
    		ActivityRequest ar
		WHERE
			ar.IsDeleted = 0
			AND ar.EndDate IS  NULL 
			AND  ar.ConfirmTypeID = 2
			AND ar.DiscoverLeakDate > '2017-03-20' 
		GROUP BY
			ar.ProvinceID 
	 )
	,Report_Collective_Raw AS
	(
		SELECT 		
			p.ID								ProvinceID,
			p.Name								ProvinceName,
			ISNULL(MIN (TechnicalCount),0)		TechnicalCount,
			COUNT(CASE 
					WHEN ar.ConfirmTypeID = 2 AND ar.DiscoverLeakDate > '2017-03-20' AND ar.EndDate IS NOT NULL THEN 1 				
				  END )							ConfirmedCount, --تعداد پايان يافته		
			SUM(ar.CycleCost)					CycleCost,
			SUM(ar.DelayedCost)					DelayedCost,
			SUM(ar.TotalLeakage)				TotalLeakage,
			SUM(ar.RecoverableLeakage)			RecoverableLeakage,
			SUM(ar.Recovered)					Recovered		
		FROM
			Province p
		LEFT JOIN
			ActivityRequest ar ON p.ID = ar.ProvinceID 
									AND ar.IsDeleted = 0
									AND ar.EndDate IS NOT NULL
									AND ar.DiscoverLeakDate > '2017-03-20'								
		LEFT JOIN
			TechnicalCte tc ON tc.ProvinceID = p.ID	
		GROUP BY 
			p.ID , p.Name
	)
	SELECT
		r.* , 
		pp.Program , 
		ROUND( ( ( r.Recovered * 100 ) / CAST( pp.Program AS FLOAT ) ) , 2 ) Recovered_Percent_Program
	FROM
		Report_Collective_Raw r
	LEFT JOIN
		ProvinceProgram pp ON r.ProvinceID = pp.ProvinceID AND pp.YearID = @ProvinceProgram_YearID
	ORDER BY
		r.ProvinceName

END


GO

