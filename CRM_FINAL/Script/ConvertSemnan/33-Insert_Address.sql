USE [CRM]
GO
DELETE dbo.Address
DBCC CHECKIDENT('Address' , RESEED , 0)

INSERT INTO [dbo].[Address]
           ([CenterID]
           ,[PostalCode]
           ,[AddressContent]
           ,[Status]
           ,[ElkaMOKATEBEH_ADDRESS_OR_NASB_ADDRESS]
           ,[ElkaID])
SELECT
      convert(INT,(select id from Center where CenterCode = F.[CEN_CODE]))
      ,NA.[NAADD_CODE_POSTI]
      ,NA.[NAADD_KHIYABAN]
      ,NA.[NAADD_SATUS]
      ,0
      ,NA.[FI_CODE]
FROM [ORACLECRM]..[SCOTT].[NASB_ADDRESS] AS NA JOIN [ORACLECRM]..[SCOTT].[FILEINFORMATION] AS F ON NA.[FI_CODE] = F.[FI_CODE]
  
INSERT INTO [dbo].[Address]
           ([CenterID]
           ,[PostalCode]
           ,[AddressContent]
           ,[Status]
           ,[ElkaMOKATEBEH_ADDRESS_OR_NASB_ADDRESS]
           ,[ElkaID])
SELECT 
       convert(INT,(select id from Center where CenterCode = F.[CEN_CODE]))
	  ,MA.[MADD_POSTALCODE]
      ,MA.[MADD_MOKATEBEADDRESS]
      ,MA.[MADD_STATUS]
	  ,1
	  ,MA.[FI_CODE]
  FROM [ORACLECRM]..[SCOTT].[MOKATEBEH_ADDRESS] AS MA JOIN [ORACLECRM]..[SCOTT].[FILEINFORMATION] AS F ON MA.[FI_CODE] = F.[FI_CODE]


