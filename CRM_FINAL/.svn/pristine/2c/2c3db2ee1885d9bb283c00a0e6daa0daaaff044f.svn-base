Drop table #T
SELECT  CCen.ID  as CENTERID, TI.[TEL_NUMBER] , BB.[BOOKHT_ID],ET.ETS_ID , TI.FI_CODE
into #T
FROM 
[ORACLECRM]..[SCOTT].[BASE_BOOKHT] AS BB 
JOIN [ORACLECRM]..[SCOTT].[V_BOOKHT] AS VB ON  BB.[BOOKHT_ID] = VB.[BOOKHT_ID]
JOIN [ORACLECRM]..[SCOTT].[TELEPHONEINFORMATION] AS TI ON TI.[FI_CODE] = VB.[FI_CODE] 
JOIN [ORACLECRM]..[SCOTT].[ETESALI] AS ET ON ET.FI_CODE = TI.FI_CODE
JOIN [ORACLECRM]..[SCOTT].[CENTER] AS CEN ON TI.CENTER_CODE = CEN.CEN_CODE
join CRM.dbo.Center as CCen ON CCen.CenterCode = CEN.CEN_CODE
JOIN [ORACLECRM]..[SCOTT].[CITY] AS CI ON CEN.[CI_CODE] = CI.[CI_CODE]
where BB.BOOKHT_TYPE = 21 and BB.BOOKHT_ID not in(SELECT bb.[BOOKHT_ID]
												  FROM [ORACLECRM]..[SCOTT].[PCM] as pc
												   join [ORACLECRM]..[SCOTT].[KAFU_PCM] as kp on kp.[PORT_ID] = pc.[PCM_ID]
												   join [ORACLECRM]..[SCOTT].[BASE_BOOKHT] as bb on bb.[BOOKHT_ID] =kp.[BOOKHT_ID])



--select T.* , TEL.SwitchPortID , BUCH.ID  , PC.ID
--from #T as T
--join dbo.Telephone AS TEL on T.TelephoneNo = TEL.TelephoneNo
--join dbo.Bucht AS BUCH on BUCH.ElkaID = T.[BOOKHT_ID]
--join dbo.PostContact AS PC on PC.ElkaID = T.ETS_ID
--where 245505 = TEL.SwitchPortID
select * from #T
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
				 



