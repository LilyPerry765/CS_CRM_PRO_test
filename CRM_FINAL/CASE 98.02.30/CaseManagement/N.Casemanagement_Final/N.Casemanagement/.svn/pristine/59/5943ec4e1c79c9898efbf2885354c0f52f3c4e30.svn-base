USE [Casemanagement97]
GO

/****** Object:  StoredProcedure [dbo].[usp_IncomeFlowProvince]    Script Date: 2018/05/06 3:20:30 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_IncomeFlowProvince]
	
AS
BEGIN
	DECLARE @Now_Date_Year_Shamsi INT = SUBSTRING( dbo.mi2sh( GETDATE() , 1 ) , 1 , 4 )
	DECLARE @Last_Year_Shamsi VARCHAR(4) = ( @Now_Date_Year_Shamsi - 2 )
	DECLARE @Last_Date DATE = ( dbo.sh2mi( @Last_Year_Shamsi + '/12/29' ) )

	SELECT 
		DENSE_RANK()OVER( ORDER BY pr.Name ) Province_Sort_ID ,
		CAST(ar.IncomeFlowID AS INT) IncomeFlowID ,
		INFLOW.Name IncomeFlowName ,
		pr.Name ,
		COUNT(*) CNT ,
		SUM( ar.TotalLeakage ) TotalLeakage ,
		SUM( ar.Recovered ) Recovered

	FROM
		ActivityRequest ar 
	LEFT JOIN
		IncomeFlow INFLOW ON ar.IncomeFlowID = INFLOW.ID
	LEFT JOIN
		Province pr ON ar.ProvinceID = pr.ID
		
		WHERE
		     ar.IsDeleted != 1 
		 AND ar.EndDate IS NOT NULL 
		 AND ar.DiscoverLeakDate  > @Last_Date

		 GROUP BY 
		 INFLOW.Name,
		 ar.IncomeFlowID , 
		 ar.PRovinceID ,
		 pr.Name

		ORDER BY
		pr.Name
		--INFLOW.ID   



END

GO

