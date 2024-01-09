USE [CRM]
GO



delete address 
 FROM         City INNER JOIN
                      Region ON City.ID = Region.CityID INNER JOIN
                      Center ON Region.ID = Center.RegionID INNER JOIN
                      Address ON Center.ID = Address.CenterID 
                     
WHERE     (City.ID = 7)




INSERT INTO [dbo].[Address]
           ([CenterID]
           ,[PostalCode]
           ,[AddressContent]
           ,[Status]
           ,[ElkaMOKATEBEH_ADDRESS_OR_NASB_ADDRESS]
           ,[ElkaID]
		   ,KerStopDate
		   ,KerStartDate
		   ,kerID)
SELECT 
      (select ID from Center where Center.CenterCode = fim.ID_MARKAZ)
      ,sd.POST_COD
	  ,sd.ADDRESS
	  ,0
      ,null
	  ,sd.ID_FINANCE
	  ,sd.STOP_DATE
	  ,sd.START_DATE
	  , sd.ID_FOLD
  FROM [salas].[dbo].[Address] as sd join [salas].[dbo].[FINANCE] as fi on sd.ID_FINANCE = fi.ID 
                                           join [salas].[dbo].[FIMARK] as fim  on fim.ID_FINANCE = fi.ID

GO
