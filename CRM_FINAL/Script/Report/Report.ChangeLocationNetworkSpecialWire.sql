if object_id('[dbo].[Report.ChangeLocationNetworkSpecialWire]','P') is not null
drop procedure [dbo].[Report.ChangeLocationNetworkSpecialWire]
/****** Object:  StoredProcedure [dbo].[Report.ChangeLocationNetworkSpecialWire]    Script Date: 1/8/2015 10:19:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--exec [dbo].[Report.ChangeLocationNetworkSpecialWire] '9309170365'
CREATE  Procedure [dbo].[Report.ChangeLocationNetworkSpecialWire]
(
	  @RequestIDs varchar(max) = null
)
AS
BEGIN

	SELECT  
		Request.ID RequestID
        ,CONCAT(Customer.FirstNameOrTitle, ' ' , Customer.LastName) as CustomerName 
        ,CAST(Customer.UrgentTelNo AS varchar(MAX)) as UrgentTelNo
        , cast(Customer.MobileNo AS varchar(MAX)) as MobileNo 
        ,CAST(clsw.NearestTelephone as varchar(MAX)) as NearestTelephon
       
        ,TelephoneNo = CAST(dbo.Request.TelephoneNo AS varchar(max))
        --,TelephoneType = CustomerType.Title
        
        ,CenterName = Center.CenterName
        ,RegionName = City.Name
		--NEW
		,NEWPostEtesaliNo = CAST(NEWPostContact.ConnectionNo AS varchar(max))
        ,NEWPostNo = CAST(NEWPost.Number AS varchar(max))
        , CAST(NEWCabinetInput.InputNumber AS varchar(MAX)) as NEWCabinetinputNo
        , CAST(NEWCabinet.CabinetNumber AS varchar(MAX)) AS NEWCabinetNo
		--OLD
		,OLDPostEtesaliNo = CAST(OLDPostContact.ConnectionNo AS varchar(max))
        ,OLDPostNo = CAST(OLDPost.Number AS varchar(max))
        , CAST(OLDCabinetInput.InputNumber AS varchar(MAX)) as OLDCabinetinputNo
        , CAST(OLDCabinet.CabinetNumber AS varchar(MAX)) AS OLDCabinetNo

		,NEWInstallAddress = NEWInstallAddress.AddressContent
		,OLDInstallAddress = OLDInstallAddress.AddressContent
        ,CorrespondenceAddress = NEWInstallCorrespondenceAddress.AddressContent
		,PostalCode = NEWInstallAddress.PostalCode
		,CASE WHEN Customer.PersonType = 0 THEN N'حقیقی' 
			  WHEN Customer.PersonType = 1 THEN N'حقوقی' 
			  ELSE N'نامشخص'
	     END PersonType
		



FROM Request 
LEFT JOIN dbo.ChangeLocationSpecialWire clsw on Request.ID = clsw.RequestID
LEFT JOIN dbo.InvestigatePossibility Investigate on Request.ID = Investigate.RequestID
Left Join Telephone t on Request.TelephoneNo = t.TelephoneNo
LEFT JOIN Customer on Customer.ID = Request.CustomerID
LEFT JOIN [Address] OLDInstallAddress on OLDInstallAddress.ID = clsw.OLDInstallAddressID

LEFT JOIN [Address] NEWInstallAddress on NEWInstallAddress.ID = clsw.InstallAddressID
LEFT JOIN [Address] NEWInstallCorrespondenceAddress ON NEWInstallCorrespondenceAddress.ID = t.CorrespondenceAddressID


--NEW BUCHTInfo
LEFT JOIN Bucht NewBucht on NewBucht.ID = Investigate.BUCHTID
LEFT JOIN Bucht NEWOtherBucht on NEWOtherBuchtID = NEWOtherBucht.ID
LEFT JOIN BuchtType NEWbt on NEWbt.ID = NEWOtherBucht.BuchtTypeID 
LEFT JOIN PostContact NEWPostContact on NEWPostContact.ID = NEWBucht.ConnectionID 
LEFT JOIN Post NEWPost on  NEWPost.ID = NEWPostContact.PostID
LEFT JOIN CabinetInput NEWCabinetInput on NewBucht.CabinetInputID = NEWCabinetInput.ID
LEFT JOIN Cabinet NEWCabinet on NEWCabinet.ID = NEWCabinetInput.CabinetID

	--OLD BUCHTInfo
LEFT JOIN Bucht OLDBucht on OLDBucht.ID = clsw.OLDBUCHTID
LEFT JOIN Bucht OldOtherBucht on OldOtherBuchtID = OldOtherBucht.ID
LEFT JOIN BuchtType OLDbt on OLDbt.ID = OLDOtherBucht.BuchtTypeID
	 
LEFT JOIN PostContact OLDPostContact on OLDPostContact.ID = clsw.OldPostContactID 
LEFT JOIN Post OLDPost on  OLDPost.ID = OLDPostContact.PostID

LEFT JOIN CabinetInput OLDCabinetInput on clsw.OLDCabinetInputID = OLDCabinetInput.ID
LEFT JOIN Cabinet OLDCabinet on OLDCabinet.ID = OLDCabinetInput.CabinetID


LEFT JOIN Center on Request.CenterID = Center.ID
LEFT join Region on Region.ID = Center.RegionID 
LEFT JOIN City on City. ID = Region.CityID
where (@RequestIDs IS NULL OR  LEN(@RequestIDs) = 0 OR Request.ID IN (SELECT * FROM  ufnSplitList(@RequestIDs)))
		


END