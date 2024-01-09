USE [CRM]
GO

DECLARE @cityid int = 5

INSERT INTO [dbo].[SpecialWireAddresses]
           ([BuchtID]
           ,[TelephoneNo]
           ,[InstallAddressID]
           ,[CorrespondenceAddressID]
		   ,SpecialTypeID)
select Bucht.ID ,Telephone.TelephoneNo , Telephone.InstallAddressID , Telephone.CorrespondenceAddressID,1
  from Telephone join Bucht on Telephone.SwitchPortID = Bucht.SwitchPortID 
    join cabinetinput as ci on ci.id=bucht.[CabinetInputID]
	join cabinet as c on c.id=ci.cabinetid
  	join center on center.id=c.CenterID
	join region on center.regionid=region.id
	join city on city.id=region.cityid
  where UsageType = 2 and InstallAddressID is not null and city.id=@cityid
GO



