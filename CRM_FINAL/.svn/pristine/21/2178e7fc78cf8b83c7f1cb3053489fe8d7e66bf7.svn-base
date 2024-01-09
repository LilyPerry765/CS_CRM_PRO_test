Drop table #T
select (select ID from Center where Center.CenterCode = CEN.CEN_CODE)  as CENTERID, TI.[TEL_NUMBER] , BB.[BOOKHT_ID],ET.ETS_ID , TI.FI_CODE
into #T
FROM
 [ORACLECRM]..[SCOTT].[BASE_BOOKHT] AS BB 
JOIN [ORACLECRM]..[SCOTT].[V_BOOKHT] AS VB ON  BB.[BOOKHT_ID] = VB.[BOOKHT_ID] 
JOIN [ORACLECRM]..[SCOTT].[TELEPHONEINFORMATION] AS TI ON TI.[FI_CODE] = VB.[FI_CODE] 
JOIN [ORACLECRM]..[SCOTT].[ETESALI] AS ET ON ET.FI_CODE = TI.FI_CODE
JOIN [ORACLECRM]..[SCOTT].[CENTER] AS CEN ON TI.CENTER_CODE = CEN.CEN_CODE
JOIN [ORACLECRM]..[SCOTT].[CITY] AS CI ON CEN.[CI_CODE] = CI.[CI_CODE]
JOIN (
SELECT TI.FI_CODE
FROM 
 [ORACLECRM]..[SCOTT].[BASE_BOOKHT] AS BB 
JOIN [ORACLECRM]..[SCOTT].[V_BOOKHT] AS VB ON  BB.[BOOKHT_ID] = VB.[BOOKHT_ID] 
JOIN [ORACLECRM]..[SCOTT].[TELEPHONEINFORMATION] AS TI ON TI.[FI_CODE] = VB.[FI_CODE] 
JOIN [ORACLECRM]..[SCOTT].[ETESALI] AS ET ON ET.FI_CODE = TI.FI_CODE
JOIN [ORACLECRM]..[SCOTT].[CENTER] AS CEN ON TI.CENTER_CODE = CEN.CEN_CODE
JOIN [ORACLECRM]..[SCOTT].[CITY] AS CI ON CEN.[CI_CODE] = CI.[CI_CODE]
GROUP BY TI.FI_CODE , BB.BOOKHT_TYPE
HAVING COUNT(*) = 1 and BB.BOOKHT_TYPE = 21
) AS TEMP ON TEMP.FI_CODE = TI.FI_CODE 
where  BB.BOOKHT_TYPE = 21


update Bucht set SwitchPortID = t3.SwitchPortID
                 , ConnectionID =  t3.PostContactID
				 from
				 (
				 select TEL.SwitchPortID as SwitchPortID , BUCH.ID as BuchtID  , PC.ID as PostContactID
				 from #T as T
                 join dbo.Telephone AS TEL on TEL.TelephoneNoIndividual =  T.[TEL_NUMBER] and TEL.CenterID = T.CENTERID
                 join dbo.Bucht AS BUCH on BUCH.ElkaID = T.[BOOKHT_ID]
                 join dbo.PostContact AS PC on PC.ElkaID = T.ETS_ID
				 ) as t3
				 where 
		         ID = t3.BuchtID

UPDATE dbo.Bucht SET SwitchPortID = NULL WHERE Status = 13
