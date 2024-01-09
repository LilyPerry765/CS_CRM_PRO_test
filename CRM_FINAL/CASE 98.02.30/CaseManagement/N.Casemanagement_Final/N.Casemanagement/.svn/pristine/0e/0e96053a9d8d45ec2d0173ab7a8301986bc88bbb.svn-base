USE [Casemanagement97]
GO

/****** Object:  StoredProcedure [dbo].[usp_CollectiveIncomeFlow]    Script Date: 2018/05/06 3:02:10 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_CollectiveIncomeFlow]

AS

BEGIN
    
	DECLARE @Now_Date_Year_Shamsi INT = SUBSTRING( dbo.mi2sh( GETDATE() , 1 ) , 1 , 4 )
	DECLARE @Now_Date_Month_Shamsi INT = SUBSTRING( dbo.mi2sh( GETDATE() , 1 ) , 6 , 2 )
	DECLARE @ProvinceProgram_YearID INT = CASE
												WHEN @Now_Date_Month_Shamsi < 5 THEN ( SELECT ID FROM Year WHERE Year = ( @Now_Date_Year_Shamsi - 1 ) )
												WHEN @Now_Date_Month_Shamsi > 4 THEN ( SELECT ID FROM Year WHERE Year = @Now_Date_Year_Shamsi )
										  END
	
	;WITH
	IncomeFlow_Raw AS
	(
		SELECT
				InFlow.ID						    IncomeFlowID,
				InFlow.Name								IncomeFlowName,
				COUNT(CASE 
						WHEN ar.ConfirmTypeID = 2 AND ar.DiscoverLeakDate > '2017-03-20' AND ar.EndDate IS NOT NULL THEN 1 				
					  END )							ConfirmedCount, --تعداد پايان يافته		
				SUM(ar.CycleCost)					CycleCost,
				SUM(ar.DelayedCost)					DelayedCost,
				SUM(ar.TotalLeakage)				TotalLeakage,
				SUM(ar.RecoverableLeakage)			RecoverableLeakage,
				SUM(ar.Recovered)					Recovered		
			FROM
				IncomeFlow InFlow
			LEFT JOIN
				ActivityRequest ar ON InFlow.ID = ar.IncomeFlowID 
										AND ar.IsDeleted = 0
										AND ar.EndDate IS NOT NULL
										AND ar.DiscoverLeakDate > '2017-03-20'								
	
			GROUP BY 
				InFlow.ID , InFlow.Name
		)
		
		SELECT * , 
			ROUND( ( ( i.TotalLeakage * 100 ) / ( SELECT CAST( SUM( TotalLeakage ) AS FLOAT ) FROM IncomeFlow_Raw ) ) , 2 ) TotalLeakage_Percentage ,
			ROUND( ( ( i.RecoverableLeakage * 100 ) / ( SELECT CAST( SUM( RecoverableLeakage ) AS FLOAT ) FROM IncomeFlow_Raw ) ) , 2 ) RecoverableLeakage_Percentage ,
		--	ROUND( ( ( i.Recovered * 100 ) / ( SELECT CAST( SUM( Recovered ) AS FLOAT ) FROM IncomeFlow_Raw ) ) , 2 ) Recovered_Percentage
		    ROUND( ( ( i.Recovered * 100 ) / (CAST( i.TotalLeakage  AS FLOAT) )) , 2 ) Recovered_Percentage
		FROM IncomeFlow_Raw	i

END

GO

