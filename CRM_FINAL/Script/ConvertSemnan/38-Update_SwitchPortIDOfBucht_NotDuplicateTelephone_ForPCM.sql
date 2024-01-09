
DROP TABLE #T
SELECT (select ID from Center where Center.CenterCode = BB.CEN_CODE ) AS CENTERID ,TI.[TEL_NUMBER], BB.BOOKHT_ID
INTO #T
FROM [ORACLECRM]..[SCOTT].[PCM] AS P
JOIN [ORACLECRM]..[SCOTT].[PCM_CON] AS PC ON PC.PCM_ID = P.PCM_ID
JOIN [ORACLECRM]..[SCOTT].[BASE_BOOKHT] AS BB ON BB.[BOOKHT_ID] = PC.BOOKHT_ID
JOIN [ORACLECRM]..[SCOTT].[PCM_ETS] AS PE ON PE.[PCM_ETS_ID] = P.[PCM_ID]
JOIN [ORACLECRM]..[SCOTT].[TELEPHONEINFORMATION] AS TI ON TI.[FI_CODE] = PE.[FI_CODE] 


update Bucht set SwitchPortID = t3.SwitchPortID
				 from
				 (
				 select TEL.SwitchPortID as SwitchPortID , BUCH.ID as BuchtID 
				 from #T as T
                 join dbo.Telephone AS TEL on TEL.TelephoneNoIndividual =  T.[TEL_NUMBER] and TEL.CenterID = T.CENTERID
                 join dbo.Bucht AS BUCH on BUCH.ElkaID = T.[BOOKHT_ID]
				 ) as t3
				 where 
		         ID = t3.BuchtID


