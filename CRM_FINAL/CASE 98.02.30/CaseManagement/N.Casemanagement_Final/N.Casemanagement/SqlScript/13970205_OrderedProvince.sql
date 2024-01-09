USE [Casemanagement97]
GO

/****** Object:  StoredProcedure [dbo].[usp_OrderedProvince]    Script Date: 2018/04/25 3:42:38 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_OrderedProvince] 

AS
BEGIN

	DECLARE @Now_Date_Year_Shamsi INT = SUBSTRING( dbo.mi2sh( GETDATE() , 1 ) , 1 , 4 )
	DECLARE @Last_Year_Shamsi VARCHAR(4) = ( @Now_Date_Year_Shamsi - 2 )
	DECLARE @Last_Date DATE = ( dbo.sh2mi( @Last_Year_Shamsi + '/12/29' ) )
SELECT
	    pr.ID,
		DENSE_RANK()OVER( ORDER BY pr.Name ) Province_Sort_ID ,
		pr.Code,
		pr.EnglishName,
		pr.LeaderID,
		pr.ManagerName,
		pr.ModifiedDate,
		pr.ModifiedUserID,
		pr.Name,
		pr.PMOLevelID
	
	FROM Province pr
	WHERE EXISTS
(SELECT ActivityCode
  FROM ActivityRequest ar WHERE
  ar.ProvinceID = pr.ID 
  and 
  
		ar.IsDeleted != 1 
		AND ar.EndDate IS NOT NULL 
		AND ar.DiscoverLeakDate  > @Last_Date

  );


END

GO

