if object_iD('[dbo].[Report.ChangeLocationSpecialWireCertificate]', 'P') is not null
drop procedure [dbo].[Report.ChangeLocationSpecialWireCertificate]
/****** Object:  StoredProcedure [dbo].[Report.ChangeLocationSpecialWireCertificate]    Script Date: 1/5/2015 5:05:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  Procedure [dbo].[Report.ChangeLocationSpecialWireCertificate]
(
	  @FromDate varchar(20) = null,
	  @ToDate varchar(20) = null,
	  @CityIDs varchar(max) = null,
	  @CenterIDs varchar(max) = null,
	  @RequestIDs varchar(max) = null
)
AS
BEGIN
;WITH LastWiring AS
(
	SELECT 
		clsw.RequestId, Max(WiringInsertDate) WiringInsertDate
	FROM [dbo].[ChangeLocationSpecialWire] clsw
		LEFT JOIN Request r ON r.ID = clsw.RequestID
		LEFT JOIN IssueWiring iw ON iw.RequestID = r.ID 
		LEFT JOIN Wiring w ON w.IssueWiringID = iw.ID
	WHERE
		(@RequestIDs IS NULL OR LEN(@RequestIDs) = 0 OR r.ID in (SELECT * FROM dbo.ufnSplitList(@RequestIDs)))
		group by clsw.RequestId
		
)
	select 
		CONCAT(c.FirstNameOrTitle, ' ' , C.LastName) FullCustomerName,
		r.ID RequestNo,
		dbo.mi2sh(r.InsertDate,1) InsertDate,
		r.TelephoneNo,
		NewAddress.AddressContent NewInstallAddress,
		OLDAddress.AddressContent OLDInstallAddress
		
	from [dbo].[ChangeLocationSpecialWire] clsw
	LEFT JOIN Request r on r.ID = clsw.RequestID
	LEFT JOIN LastWiring lw on lw.RequestID = clsw.RequestID
	LEFT JOIN Customer C on C.ID = r.CustomerID
	LEFT JOIN [ADDRESS] NewAddress on NewAddress.ID = InstallAddressID
	LEFT JOIN [ADDRESS] OLDAddress on OLDAddress.ID = OLDInstallAddressID
	LEFT JOIN Center on Center.ID = r.CenterID
	LEFT JOIN Region on Region.ID = REgionID
	LEFT JOIN City on City.ID = CityID


	where 
			(@RequestIDs IS NULL OR LEN(@RequestIDs) = 0 OR r.ID in (Select * From dbo.ufnSplitList(@RequestIDs)))
		AND (@CityIDs IS NULL OR LEN(@CityIDs) = 0 OR City.ID in (Select * From dbo.ufnSplitList(@CityIDs)))
		AND (@CenterIDs IS NULL OR LEN(@CenterIDs) = 0 OR r.CenterID in (Select * From dbo.ufnSplitList(@CenterIDs)))
		AND (@FromDate IS NULL OR LEN(@FromDate) = 0 OR WiringInsertDate >= @FromDate )
		AND (@ToDate IS NULL OR LEN(@ToDate) = 0 OR WiringInsertDate <= @ToDate )

END