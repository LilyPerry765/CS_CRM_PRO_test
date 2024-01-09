USE [CRM]
GO
declare @CityID int = 5

INSERT INTO [dbo].[Address]
           ([CenterID]
           ,[PostalCode]
           ,[AddressContent]
           ,[Status]
           ,[ElkaMOKATEBEH_ADDRESS_OR_NASB_ADDRESS]
           ,[ElkaID]
           ,[KerStopDate]
           ,[KerStartDate]
           ,[kerID])
select
            (select id from center where centercode in (s.code))
           ,null
           ,s.ADR
           ,0
           ,null
           ,s.TEL_NO
           ,null
           ,null
           ,s.TEL_NO
from [Sarpol].[dbo].[SIM] as s


update t set t.InstallAddressID = a.ID , t.CorrespondenceAddressID = a.ID 
 FROM [dbo].[Address] as a 
join Telephone as t on a.ElkaID = t.TelephoneNoIndividual
join [Sarpol].[dbo].[SIM] as z on z.TEL_NO = a.ElkaID
join center on center.id=t.CenterID
join region on center.regionid=region.id
join city on city.id=region.cityid
where city.id=@CityID
GO


