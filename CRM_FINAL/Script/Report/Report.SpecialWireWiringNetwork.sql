if  object_id ('[dbo].[Report.SpecialWireWiringNetwork]','P') is not null
drop procedure [dbo].[Report.SpecialWireWiringNetwork]

/****** Object:  StoredProcedure [dbo].[Report.SpecialWireWiringNetwork]    Script Date: 12/14/2014 9:23:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  Procedure [dbo].[Report.SpecialWireWiringNetwork]
(@RequestID VARCHAR(MAX) = null)
as
begin
 --execute [dbo].[Report.DayeriWiringNetwork] 9206240006
    
    
     SELECT  
		Request.ID RequestID
        ,CONCAT(Customer.FirstNameOrTitle, ' ' , Customer.LastName) as CustomerName 
        ,CAST(Customer.UrgentTelNo AS varchar(MAX)) as UrgentTelNo
        , cast(Customer.MobileNo AS varchar(MAX)) as MobileNo 
        ,CAST(SpecialWire.NearestTelephone as varchar(MAX)) as NearestTelephon
        , CAST(CabinetInput.InputNumber AS varchar(MAX)) as CabinetinputNo
        , CAST(Cabinet.CabinetNumber AS varchar(MAX)) AS CabinetNo
        , CAST(PCM.[Card] AS varchar(MAX)) as [Card]
        , Shelf =  cast(PCMShelf.Number as varchar(max)) 
        ,Rock = CAST(PCMRock.Number AS varchar(max))
        ,Port = CAST(PCMPort.PortNumber AS varchar(max)) 
        ,Radif = cast(VerticalMDFColumn.VerticalCloumnNo AS varchar(max))
        ,Tabagheh = CAST(VerticalMDFRow.VerticalRowNo AS varchar(max))
        ,Etesali = CAST(Bucht.BuchtNo AS varchar(max))
        ,PersonType = cast(Customer.PersonType AS varchar(max))
        ,InstallAddress = InstallAddress.AddressContent
        ,PostalCode = InstallAddress.PostalCode
        ,TelephoneNo = CAST(dbo.Request.TelephoneNo AS varchar(max))
        ,TelephoneType = CustomerType.Title
        ,PostEtesaliNo = CAST(PostContact.ConnectionNo AS varchar(max))
        ,PostNo = CAST(Post.Number AS varchar(max))
        ,CenterName = Center.CenterName
        ,RegionName = City.Name
        ,PCM = CASE WHEN cast(Bucht.PCMPortID AS varchar(max)) != '' THEN '*' END
        , CorrespondenceAddress = InstallCorrespondenceAddress.AddressContent
		



FROM Request 
LEFT JOIN dbo.SpecialWire on Request.ID = SpecialWire.RequestID
LEFT JOIN dbo.InvestigatePossibility Investigate on Request.ID = Investigate.RequestID
LEFT JOIN Customer on Customer.ID = Request.CustomerID
LEFT JOIN CustomerType on CustomerType.ID = SpecialWire.CustomerTypeID
LEFT JOIN [Address] InstallAddress on InstallAddress.ID = dbo.SpecialWire.InstallAddressID
LEFT JOIN [Address] InstallCorrespondenceAddress ON InstallCorrespondenceAddress.ID = SpecialWire.CorrespondenceAddressID

LEFT JOIN Bucht on Bucht.ID = Investigate.BuchtID
LEFT JOIN PostContact on PostContact.ID = Bucht.ConnectionID 
LEFT JOIN Post on Post.ID = PostContact.PostID
LEFT JOIN CabinetInput on Bucht.CabinetInputID = CabinetInput.ID
LEFT JOIN Cabinet on Cabinet.ID = CabinetInput.CabinetID
Left JOIN PCMPort on Bucht.PCMPortID = PCMPort.ID
Left JOIN PCM on PCM.ID = PCMPort.PCMID 
Left JOIN PCMShelf on PCM.ShelfID = PCMShelf.ID
Left JOIN pcmrock on PCMRock.ID = PCMShelf.PCMRockID
LEFT JOIN VerticalMDFRow on Bucht.MDFRowID = VerticalMDFRow.ID 
LEFT JOIN VerticalMDFColumn on VerticalMDFRow.VerticalMDFColumnID = VerticalMDFColumn .ID
LEFT JOIN Center on Request.CenterID = Center.ID
LEFT join Region on Region.ID = Center.RegionID 
LEFT JOIN City on City. ID = Region.CityID
where (@requestID IS NULL OR  LEN(@RequestID) = 0 OR Request.ID IN (SELECT * FROM  ufnSplitList(@RequestID)))

end
