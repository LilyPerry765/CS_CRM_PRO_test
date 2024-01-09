if object_id('[dbo].[Report.VacateSpecialWireWiringNetwork]','P') is not null
drop procedure [dbo].[Report.VacateSpecialWireWiringNetwork]
/****** Object:  StoredProcedure [dbo].[Report.SpecialWireWiringNetwork]    Script Date: 12/30/2014 3:37:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  Procedure [dbo].[Report.VacateSpecialWireWiringNetwork]
(@RequestID VARCHAR(MAX) = null)
as
begin
 --execute [dbo].[Report.DayeriWiringNetwork] 9206240006
    
    
     SELECT  
		Request.ID RequestID
        ,CONCAT(Customer.FirstNameOrTitle, ' ' , Customer.LastName) as CustomerName 
        ,CAST(Customer.UrgentTelNo AS varchar(MAX)) as UrgentTelNo
        ,cast(Customer.MobileNo AS varchar(MAX)) as MobileNo 
        --,CAST([dbo].[VacateSpecialWire].NearestTelephone as varchar(MAX)) as NearestTelephon
        , CAST(CabinetInput.InputNumber AS varchar(MAX)) as CabinetinputNo
        , CAST(Cabinet.CabinetNumber AS varchar(MAX)) AS CabinetNo
        
        ,Radif = cast(VerticalMDFColumn.VerticalCloumnNo AS varchar(max))
        ,Tabagheh = CAST(VerticalMDFRow.VerticalRowNo AS varchar(max))
        ,Etesali = CAST(Bucht.BuchtNo AS varchar(max))
        ,PersonType = cast(Customer.PersonType AS varchar(max))
        ,InstallAddress = InstallAddress.AddressContent
        ,PostalCode = InstallAddress.PostalCode
        ,TelephoneNo = CAST(dbo.Request.TelephoneNo AS varchar(max))
        ,PostEtesaliNo = CAST(PostContact.ConnectionNo AS varchar(max))
        ,PostNo = CAST(Post.Number AS varchar(max))
        ,CenterName = Center.CenterName
        ,RegionName = City.Name
        ,PCM = CASE WHEN cast(Bucht.PCMPortID AS varchar(max)) != '' THEN '*' END
        , CorrespondenceAddress = InstallCorrespondenceAddress.AddressContent
		,CASE WHEN Customer.PersonType = 0 THEN 'حقیقی' 
			  WHEN Customer.PersonType = 1 THEN 'حقوقی' 
			  ELSE 'نامشخص'
	     END PersonType
		



FROM Request 
LEFT JOIN [dbo].[VacateSpecialWire] on Request.ID = [dbo].[VacateSpecialWire].RequestID
LEFT JOIN Customer on Customer.ID = Request.CustomerID
LEFT JOIN [Address] InstallAddress on InstallAddress.ID = OldInstallAddressID
LEFT JOIN [Address] InstallCorrespondenceAddress ON InstallCorrespondenceAddress.ID = OLDCorrespondenceAddressID

LEFT JOIN Bucht on Bucht.ID = BuchtID
LEFT JOIN PostContact on PostContact.ID = Bucht.ConnectionID 
LEFT JOIN Post on Post.ID = PostContact.PostID
LEFT JOIN CabinetInput on Bucht.CabinetInputID = CabinetInput.ID
LEFT JOIN Cabinet on Cabinet.ID = CabinetInput.CabinetID

LEFT JOIN VerticalMDFRow on Bucht.MDFRowID = VerticalMDFRow.ID 
LEFT JOIN VerticalMDFColumn on VerticalMDFRow.VerticalMDFColumnID = VerticalMDFColumn .ID
LEFT JOIN Center on Request.CenterID = Center.ID
LEFT join Region on Region.ID = Center.RegionID 
LEFT JOIN City on City. ID = Region.CityID
where (@requestID IS NULL OR  LEN(@RequestID) = 0 OR Request.ID IN (SELECT * FROM  ufnSplitList(@RequestID)))

end
