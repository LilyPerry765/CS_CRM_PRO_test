USE [Casemanagement97]
GO

/****** Object:  StoredProcedure [dbo].[usp_IncomeFlowProvince_Chart]    Script Date: 2018/04/15 11:41:56 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[usp_IncomeFlowProvince_Chart]
	
AS
BEGIN
	DECLARE @Now_Date_Year_Shamsi INT = SUBSTRING( dbo.mi2sh( GETDATE() , 1 ) , 1 , 4 )
	DECLARE @Last_Year_Shamsi VARCHAR(4) = ( @Now_Date_Year_Shamsi - 2 )
	DECLARE @Last_Date DATE = ( dbo.sh2mi( @Last_Year_Shamsi + '/12/29' ) )

	;WITH
	ActivityRequest_Raw AS
	(
		SELECT ar.ProvinceID, ar.IncomeFlowID, COUNT(*) CNT, SUM( ar.TotalLeakage ) TotalLeakage, SUM( ar.Recovered ) Recovered
		FROM
			ActivityRequest ar
		WHERE
			ar.IsDeleted != 1 
			AND ar.EndDate IS NOT NULL 
			AND ar.DiscoverLeakDate > @Last_Date
		GROUP BY
			ar.ProvinceID, ar.IncomeFlowID
	)

	SELECT
		DENSE_RANK()OVER( ORDER BY p.Name ) Province_Sort_ID ,
		--p.ID ProvinceID, 
		p.Name Province_Name,		
		ISNULL( a1.CNT , 0 ) CNT_ID1, ISNULL( a1.TotalLeakage , 0 ) TotalLeakage_ID1, ISNULL( a1.Recovered , 0 ) Recovered_ID1,
		ISNULL( a2.CNT , 0 ) CNT_ID2, ISNULL( a2.TotalLeakage , 0 ) TotalLeakage_ID2, ISNULL( a2.Recovered , 0 ) Recovered_ID2,
		ISNULL( a3.CNT , 0 ) CNT_ID3, ISNULL( a3.TotalLeakage , 0 ) TotalLeakage_ID3, ISNULL( a3.Recovered , 0 ) Recovered_ID3,
		ISNULL( a4.CNT , 0 ) CNT_ID4, ISNULL( a4.TotalLeakage , 0 ) TotalLeakage_ID4, ISNULL( a4.Recovered , 0 ) Recovered_ID4,
		ISNULL( a5.CNT , 0 ) CNT_ID5, ISNULL( a5.TotalLeakage , 0 ) TotalLeakage_ID5, ISNULL( a5.Recovered , 0 ) Recovered_ID5,
		ISNULL( a6.CNT , 0 ) CNT_ID6, ISNULL( a6.TotalLeakage , 0 ) TotalLeakage_ID6, ISNULL( a6.Recovered , 0 ) Recovered_ID6,
		ISNULL( a7.CNT , 0 ) CNT_ID7, ISNULL( a7.TotalLeakage , 0 ) TotalLeakage_ID7, ISNULL( a7.Recovered , 0 ) Recovered_ID7,
		ISNULL( a8.CNT , 0 ) CNT_ID8, ISNULL( a8.TotalLeakage , 0 ) TotalLeakage_ID8, ISNULL( a8.Recovered , 0 ) Recovered_ID8,
		ISNULL( a9.CNT , 0 ) CNT_ID9, ISNULL( a9.TotalLeakage , 0 ) TotalLeakage_ID9, ISNULL( a9.Recovered , 0 ) Recovered_ID9

	FROM
		Province p
	LEFT JOIN
		ActivityRequest_Raw a1 ON a1.IncomeFlowID = 1 AND p.ID = a1.ProvinceID
	LEFT JOIN
		ActivityRequest_Raw a2 ON a2.IncomeFlowID = 2 AND p.ID = a2.ProvinceID
	LEFT JOIN
		ActivityRequest_Raw a3 ON a3.IncomeFlowID = 3 AND p.ID = a3.ProvinceID
	LEFT JOIN
		ActivityRequest_Raw a4 ON a4.IncomeFlowID = 4 AND p.ID = a4.ProvinceID
	LEFT JOIN
		ActivityRequest_Raw a5 ON a5.IncomeFlowID = 5 AND p.ID = a5.ProvinceID
	LEFT JOIN
		ActivityRequest_Raw a6 ON a6.IncomeFlowID = 6 AND p.ID = a6.ProvinceID
	LEFT JOIN
		ActivityRequest_Raw a7 ON a7.IncomeFlowID = 7 AND p.ID = a7.ProvinceID
	LEFT JOIN
		ActivityRequest_Raw a8 ON a8.IncomeFlowID = 8 AND p.ID = a8.ProvinceID
	LEFT JOIN
		ActivityRequest_Raw a9 ON a9.IncomeFlowID = 9 AND p.ID = a9.ProvinceID

	ORDER BY
		1

END


GO

