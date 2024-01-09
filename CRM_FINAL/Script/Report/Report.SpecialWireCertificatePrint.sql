if object_id('[dbo].[Report.SpecialWireCertificatePrint]','P') is not null 
drop procedure [dbo].[Report.SpecialWireCertificatePrint]
/****** Object:  StoredProcedure [dbo].[Report.SpecialWireCertificatePrint]    Script Date: 12/15/2014 11:49:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  Procedure [dbo].[Report.SpecialWireCertificatePrint]
(
	@cityIDs VARCHAR(MAX) = null,
	 @centerIDS VARCHAR(MAX) = null,
	 @RequestID VARCHAR(MAX) = null,
	 @fromDate  VARCHAR(MAX) = NULL,
	 @toDate VARCHAR(MAX)= NULL,
	 @telePhoneNo bigint = NULL,
	 @fromCreateDate  VARCHAR(MAX) = NULL,
	 @toCreateDate VARCHAR(MAX)= NULL
 )
AS
BEGIN

     SELECT  
		Request.ID RequestID
		,Cast(ISNULL(swa.SpecialTypeID,0) as Nvarchar(2)) SpecialType
        ,Customer.FirstNameOrTitle
		,Customer.LastName 
        ,Customer.UrgentTelNo UrgentTelNo
        ,PersonType = Customer.PersonType
        ,InstallAddress = InstallAddress.AddressContent
        ,PostalCodeInstallAddress = InstallAddress.PostalCode
        ,TelephoneNo = dbo.Request.TelephoneNo
        ,CenterName = Center.CenterName
        ,RegionName = City.Name
        ,CorrespondenceAddress = InstallCorrespondenceAddress.AddressContent
		,PostalCodeCorrespondenceAddress = InstallCorrespondenceAddress.PostalCode
		

		FROM Request 
		LEFT JOIN Telephone T On T.TelephoneNo = Request.TelephoneNo
		LEFT JOIN Customer on Customer.ID = Request.CustomerID
		LEFT JOIN SpecialWireAddresses swa on swa.TelephoneNO = Request.TelephoneNo
		LEFT JOIN [Address] InstallAddress on InstallAddress.ID = swa.InstallAddressID
		LEFT JOIN [Address] InstallCorrespondenceAddress ON InstallCorrespondenceAddress.ID = swa.CorrespondenceAddressID
		LEFT JOIN Bucht on Bucht.ID = BuchtID
		LEFT JOIN VerticalMDFRow on Bucht.MDFRowID = VerticalMDFRow.ID 
		LEFT JOIN VerticalMDFColumn on VerticalMDFRow.VerticalMDFColumnID = VerticalMDFColumn .ID
		LEFT JOIN MDFFrame on MDFFrame.ID = MDFFrameID 
		LEFT JOIN MDF on MDF.ID = MDFID
		LEFT JOIN Center on MDF.CenterID = Center.ID
		LEFT join Region on Region.ID = Center.RegionID 
		LEFT JOIN City on City.ID = Region.CityID
		where 
			  (@requestID IS NULL OR  LEN(@RequestID) = 0 OR Request.ID IN (SELECT * FROM  ufnSplitList(@RequestID)))
		AND	  (@fromDate  IS NULL OR  LEN(@fromDate) = 0  OR Request.InsertDate >= @fromDate)
		AND	  (@toDate  IS NULL OR  LEN(@toDate) = 0  OR Request.InsertDate <= @toDate)
		AND   (@telephoneNo IS NULL OR  LEN(@telephoneNo) = 0  OR request.TelePhoneNo = @telephoneNo)
		AND   (Request.EndDate Is NULL)
		AND   (@cityIDs IS NULL OR LEN(@cityIDs) = 0 OR City.ID IN (SELECT * FROM  ufnSplitList(@cityIDs)))
		AND   (@centerIDs IS NULL OR LEN(@centerIDs) = 0 OR Center.ID IN (SELECT * FROM  ufnSplitList(@centerIDs)))
		AND	  (@fromCreateDate  IS NULL OR  LEN(@fromCreateDate) = 0  OR T.InstallationDate >= @fromCreateDate)
		AND	  (@toCreateDate  IS NULL OR  LEN(@toCreateDate) = 0  OR T.InstallationDate <= @toCreateDate)

		

end
