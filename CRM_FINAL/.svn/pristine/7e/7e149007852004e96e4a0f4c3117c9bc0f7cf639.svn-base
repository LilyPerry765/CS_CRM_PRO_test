
SELECT tem.CEN_NAME , MIN(Tem.phoneNumber) AS Min_Phone, MAX(Tem.phoneNumber) AS Max_Phone
FROM
(
SELECT  ET.*
FROM dbo.Telephone AS T RIGHT JOIN (

 	   SELECT  CONVERT(NVARCHAR(MAX) , CI.[CI_PISH_CODE]) + CONVERT(NVARCHAR(MAX) , TI.[TEL_NUMBER]) AS phoneNumber
			   ,C.[CEN_NAME]
	      FROM [ORACLECRM]..[SCOTT].[TELEPHONEINFORMATION] AS TI 
		       LEFT JOIN  [ORACLECRM]..[SCOTT].[CENTER] AS C ON C.[CEN_CODE] = TI.[CENTER_CODE]
               LEFT JOIN [ORACLECRM]..[SCOTT].[CITY] AS CI ON CI.[CI_CODE] = C.[CI_CODE]
			   LEFT JOIN dbo.Customer AS cus ON cus.ElkaID = TI.[FI_CODE]) AS ET ON T.TelephoneNo = ET.phoneNumber
			   WHERE TelephoneNo IS NULL  
) AS Tem
GROUP BY tem.CEN_NAME 
