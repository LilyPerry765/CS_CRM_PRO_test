


Drop table #T
SELECT CCen.ID  as CENTERID, pc.BOOKHT_ID , TI.TEL_NUMBER
into #T
FROM [ORACLECRM]..[SCOTT].[BASE_BOOKHT] AS bb 
join [ORACLECRM]..[SCOTT].[PCM_CON] as pc on pc.BOOKHT_ID = bb.BOOKHT_ID
join [ORACLECRM]..[SCOTT].[PCM] as p on pc.PCM_ID = p.PCM_ID
join [ORACLECRM]..[SCOTT].[PCM_ETS] as pe on pe.PCM_ETS_ID = p.PCM_ID
join [ORACLECRM]..[SCOTT].[TELEPHONEINFORMATION] as TI on TI.FI_CODE = pe.FI_CODE
JOIN [ORACLECRM]..[SCOTT].[CENTER] AS CEN ON TI.CENTER_CODE = CEN.CEN_CODE
JOIN CRM.dbo.Center as CCen ON CCen.CenterCode = CEN.CEN_CODE
JOIN [ORACLECRM]..[SCOTT].[CITY] AS CI ON CEN.[CI_CODE] = CI.[CI_CODE]
where 22 = BOOKHT_TYPE

update Bucht set SwitchPortID = t3.SwitchPortID
				 from
				 (
					select TEL.SwitchPortID as SwitchPortID , BUCH.ID as BuchtID 
					from #T as T
					join dbo.Telephone AS TEL on TEL.TelephoneNoIndividual =  T.[TEL_NUMBER] and TEL.CenterID = T.CENTERID
					join dbo.Bucht AS BUCH on BUCH.ElkaID = T.[BOOKHT_ID]
					where TEL.SwitchPortID not in (select SwitchPortID from dbo.Bucht where SwitchPortID  is not null)
				 ) as t3
				 where 
		         ID = t3.BuchtID
