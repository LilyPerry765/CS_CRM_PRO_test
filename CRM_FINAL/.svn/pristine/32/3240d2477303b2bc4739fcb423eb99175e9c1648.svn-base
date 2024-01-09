SELECT T.TelephoneNo , ET.phoneNumber
FROM dbo.Telephone AS T RIGHT JOIN (

 	   SELECT  CONVERT(NVARCHAR(MAX) , CI.[CI_PISH_CODE]) + CONVERT(NVARCHAR(MAX) , TI.[TEL_NUMBER]) AS phoneNumber
			   ,cus.ID
	      FROM [ORACLECRM]..[SCOTT].[TELEPHONEINFORMATION] AS TI 
		       LEFT JOIN  [ORACLECRM]..[SCOTT].[CENTER] AS C ON C.[CEN_CODE] = TI.[CENTER_CODE]
               LEFT JOIN [ORACLECRM]..[SCOTT].[CITY] AS CI ON CI.[CI_CODE] = C.[CI_CODE]
			   LEFT JOIN dbo.Customer AS cus ON cus.ElkaID = TI.[FI_CODE]) AS ET ON T.TelephoneNo = ET.phoneNumber
			   WHERE TelephoneNo IS NULL