USE [CRM]
GO

UPDATE [dbo].[Telephone]
   SET InstallAddressID = ET.ID,
    CorrespondenceAddressID = ET.ID
FROM dbo.Telephone AS T JOIN (

 	   SELECT  CONVERT(BIGINT , (CONVERT(NVARCHAR(MAX) ,CONVERT(SMALLINT, CI.[CI_PISH_CODE]) )+ CONVERT(NVARCHAR(MAX) , TI.[TEL_NUMBER]) )) AS phoneNumber
			   ,ars.ID
	      FROM ElkaData.[TT].[TELEPHONEINFORMATION] AS TI 
		       LEFT JOIN  ElkaData.[TT].[CENTER] AS C ON C.[CEN_CODE] = TI.[CENTER_CODE]
			    JOIN  ElkaData.[TT].[CITY] AS CI ON CI.[CI_CODE] = C.[CI_CODE]
			   LEFT JOIN dbo.Address AS ars ON ars.ElkaID = TI.[FI_CODE] where TI.TEL_STATUS = 2 ) AS ET ON T.TelephoneNo = ET.phoneNumber 

UPDATE [dbo].[Telephone]
   SET [CustomerID] = ET.ID
FROM dbo.Telephone AS T JOIN (

 	   SELECT  CONVERT(BIGINT , (CONVERT(NVARCHAR(MAX) ,CONVERT(SMALLINT, CI.[CI_PISH_CODE]) )+ CONVERT(NVARCHAR(MAX) , TI.[TEL_NUMBER]) )) AS phoneNumber
			   ,cus.ID
	      FROM ElkaData.[TT].[TELEPHONEINFORMATION] AS TI 
		       LEFT JOIN  ElkaData.[TT].[CENTER] AS C ON C.[CEN_CODE] = TI.[CENTER_CODE]
			    JOIN  ElkaData.[TT].[CITY] AS CI ON CI.[CI_CODE] = C.[CI_CODE]
			   LEFT JOIN dbo.Customer AS cus ON cus.ElkaID = TI.[FI_CODE] where TI.TEL_STATUS = 2 ) AS ET ON T.TelephoneNo = ET.phoneNumber 


GO
