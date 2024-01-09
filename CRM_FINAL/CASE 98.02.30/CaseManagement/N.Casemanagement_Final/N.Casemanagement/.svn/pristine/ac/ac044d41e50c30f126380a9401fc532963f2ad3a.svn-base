USE [Casemanagement97]
GO

/****** Object:  StoredProcedure [dbo].[usp_CollectiveActivity]    Script Date: 2018/05/06 1:46:44 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_CollectiveActivity]

AS

BEGIN

	DECLARE @Now_Date_Year_Shamsi INT = SUBSTRING( dbo.mi2sh( GETDATE() , 1 ) , 1 , 4 )
	DECLARE @Now_Date_Month_Shamsi INT = SUBSTRING( dbo.mi2sh( GETDATE() , 1 ) , 6 , 2 )
	DECLARE @ProvinceProgram_YearID INT = CASE
												WHEN @Now_Date_Month_Shamsi < 5 THEN ( SELECT ID FROM Year WHERE Year = ( @Now_Date_Year_Shamsi - 1 ) )
												WHEN @Now_Date_Month_Shamsi > 4 THEN ( SELECT ID FROM Year WHERE Year = @Now_Date_Year_Shamsi )
										  END
	;WITH
	Activity_Raw AS
	(
	
		SELECT 		
			Act.Code					    ActivityCode,
			Act.CodeName								ActivityName,
			COUNT(CASE 
					WHEN ar.ConfirmTypeID = 2 AND ar.DiscoverLeakDate > '2017-03-20' AND ar.EndDate IS NOT NULL THEN 1 				
				  END )							ConfirmedCount, --تعداد پايان يافته		
			SUM(ar.CycleCost)					CycleCost,
			SUM(ar.DelayedCost)					DelayedCost,
			SUM(ar.TotalLeakage)				TotalLeakage,
			SUM(ar.RecoverableLeakage)			RecoverableLeakage,
			SUM(ar.Recovered)					Recovered		
		FROM
			Activity Act
		LEFT JOIN
			ActivityRequest ar ON Act.ID = ar.ActivityID 
									AND ar.IsDeleted = 0
									AND ar.EndDate IS NOT NULL
									AND ar.DiscoverLeakDate > '2017-03-20'								
	
		GROUP BY 
			Act.Code , Act.CodeName
			)
				SELECT * , 
			--ROUND( ( ( a.TotalLeakage * 100 ) / ( SELECT CAST( SUM( TotalLeakage ) AS FLOAT ) FROM Activity_Raw ) ) , 2 ) TotalLeakage_Percentage ,
			CASE
				WHEN ( SELECT CAST( SUM( TotalLeakage ) AS FLOAT ) FROM Activity_Raw ) != 0 THEN ROUND( ( ( a.TotalLeakage * 100 ) / ( SELECT CAST( SUM( TotalLeakage ) AS FLOAT ) FROM Activity_Raw ) ) , 4 )
				ELSE 0
			END TotalLeakage_Percentage,
					    
			CASE
				WHEN ( SELECT CAST( SUM( RecoverableLeakage ) AS FLOAT ) FROM Activity_Raw ) != 0 THEN ROUND( ( ( A.RecoverableLeakage * 100 ) / ( SELECT CAST( SUM( RecoverableLeakage ) AS FLOAT ) FROM Activity_Raw ) ) , 4 )
				ELSE 0
			END RecoverableLeakage_Percentage,
			
			CASE
				WHEN a.TotalLeakage != 0 THEN ROUND( ( ( a.Recovered * 100 ) / (CAST( A.TotalLeakage  AS FLOAT) )) , 4 )
				ELSE 0
			END Recovered_Percentage

		FROM Activity_Raw A	
END

GO

