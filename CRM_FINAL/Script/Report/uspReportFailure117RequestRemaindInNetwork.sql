
if object_ID('[dbo].[uspReportFailure117RequestRemaindInNetwork]','p') is not null
drop procedure [dbo].[uspReportFailure117RequestRemaindInNetwork]
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATe  Procedure [dbo].[uspReportFailure117RequestRemaindInNetwork]
(
	@CityID AS int = NULL,
	@CenterID As int = NULL,
	@FromInsertDate As Varchar(15) = NULL,
	@ToInsertDate As Varchar(15) = NULL
)
as
begin
 

select 
	r.ID ,
	r.TelephoneNo ,
	cen.CenterName  ,
	cit.Name CityName ,
	dbo.mi2sh(r.InsertDate,1) InsertDate ,
	dbo.mi2sh(r.ModifyDate,1) ModifyDate ,
	concat(usr.FirstName, ' ' , usr.LastName) FullNameModifyUser ,
	ISNULL(fls.Title,'-') FailureLineStatus,
	ISNULL(ffs.Title,'-') FailureStatus 
	
from Failure117 f
join request r on f.ID = r.id
Left join Failure117LineStatus fls on fls.id = f.LineStatusID
Left join Failure117FailureStatus ffs on ffs.id = FailureStatusID



Left join center cen on cen.id = r.centerid
Left join region reg on reg.id = regionid
Left join city cit on cit.id = cityid
Left join [User] usr on usr.id = r.ModifyUserID
where
 r.RequestTypeID = 65
AND StatusID = 1364
AND r.enddate is null
AND ((@FromInsertDate IS null OR @FromInsertDate <= r.InsertDate) AND (@ToInsertDate Is Null OR @ToInsertDate >= r.InsertDate))
AND (@CityID is NULL OR CityID = @CityID)
AND (@CenterID is NULL OR r.CenterID = @CenterID)

END