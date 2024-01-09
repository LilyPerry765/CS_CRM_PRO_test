

/****** Object:  StoredProcedure [dbo].[uspReportRequestState]    Script Date: 1/31/2015 10:13:36 AM ******/
DROP PROCEDURE [dbo].[uspReportRequestState]
GO

/****** Object:  StoredProcedure [dbo].[uspReportRequestState]    Script Date: 1/31/2015 10:13:36 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--exec [dbo].[uspReportRequestState] null,null,null,null
CREATE  Procedure [dbo].[uspReportRequestState]
(
	@cityIDs VARCHAR(MAX) = null,
	@centerIDS VARCHAR(MAX) = null,
	@FromDate varchar(15) = null,
	@ToDate varchar(15) = null
)
as
begin
 

	select Title, Steptitle,ISNULL(NotFinished,0)NotFinished,ISNULL(Finished,0)Finished, ISNULL(IsCanceled,0)IsCanceled
	from (
			Select *
			FROM
			(

				Select T.Title,T.StepTitle,LastState, ISNULL(Count(StepTitle),0) StepTitleCount
				from 
				(
					select 
						rt.Title, 
						case when r.Enddate Is Null Then rs.StepTitle Else N'' END StepTitle,
						case when r.Enddate Is Null AND IsCancelation = 0 Then 'NotFinished'
							 when r.Enddate Is NOT Null AND IsCancelation = 0 THEN 'Finished'
							 when r.Enddate Is Null AND IsCancelation = 1  Then 'IsCanceled' END LastState
					from Request r
					Left join RequestType rt on rt.ID = r.RequestTypeID
					Left Join [status] s on s.iD = r.StatusID
					Left join RequestStep rs on rs.ID = s.RequeststepID
					LEFT JOIN Center on r.CenterID = Center.ID
					LEFT join Region on Region.ID = Center.RegionID 
					LEFT JOIN City on City.ID = Region.CityID
					where 
							(@cityIDs IS NULL OR LEN(@cityIDs) = 0 OR City.ID IN (SELECT * FROM  ufnSplitList(@cityIDs)))
						AND (@centerIDs IS NULL OR LEN(@centerIDs) = 0 OR Center.ID IN (SELECT * FROM  ufnSplitList(@centerIDs)))
						AND	(@FromDate IS NULL OR LEN(@FromDate) = 0 OR @FromDate <= r.InsertDate)
						AND (@ToDate IS NULL OR LEN(@ToDate) = 0 OR @ToDate >= r.InsertDate)
				)T

				group by T.Title,T.StepTitle,LastState
			)src
			pivot
			(
			  sum(StepTitleCount)
			  for LastState in ([NotFinished], [Finished], [IsCanceled])
			) piv
	)result
	order by Title, Steptitle
END
GO


