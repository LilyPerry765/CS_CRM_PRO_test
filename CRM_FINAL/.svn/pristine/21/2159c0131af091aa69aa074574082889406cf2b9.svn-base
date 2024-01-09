

/****** Object:  StoredProcedure [dbo].[uspReportE1WiringNetwork]    Script Date: 2/18/2015 3:28:26 PM ******/
DROP PROCEDURE [dbo].[uspReportE1WiringNetwork]
GO

/****** Object:  StoredProcedure [dbo].[uspReportE1WiringNetwork]    Script Date: 2/18/2015 3:28:26 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create  Procedure [dbo].[uspReportE1WiringNetwork]
(@RequestID VARCHAR(MAX) = null)
AS
BEGIN
     SELECT  
		Request.ID RequestID
        ,CONCAT(Customer.FirstNameOrTitle, ' ' , Customer.LastName) as CustomerName 
        ,CAST(Customer.UrgentTelNo AS varchar(MAX)) as UrgentTelNo
        , cast(Customer.MobileNo AS varchar(MAX)) as MobileNo 
        --,CAST(E1.NearestTelephone as varchar(MAX)) as NearestTelephon
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
LEFT JOIN dbo.E1 on Request.ID = E1.RequestID
LEFT JOIN Customer on Customer.ID = Request.CustomerID
LEFT JOIN CustomerType on CustomerType.ID = E1.TelephoneType
LEFT JOIN [Address] InstallAddress on InstallAddress.ID = dbo.E1.InstallAddressID
LEFT JOIN [Address] InstallCorrespondenceAddress ON InstallCorrespondenceAddress.ID = E1.CorrespondenceAddressID

LEFT JOIN Bucht on Bucht.ID = BuchtID
LEFT JOIN PostContact on PostContact.ID = E1.ConnectionID 
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
END
GO


