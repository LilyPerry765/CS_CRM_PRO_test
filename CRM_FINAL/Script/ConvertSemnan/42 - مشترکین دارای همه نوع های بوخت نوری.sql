
 select *
into #ETES
from
(
select ROW_NUMBER() OVER (PARTITION BY FI_CODE ORDER BY case when [STATUS] = 2 then 1 else 0 end DESC) AS RN 
       ,*
from  [ORACLECRM]..[SCOTT].[ETESALI]
) as ET
where ET.RN = 1



select *
into #VBO
from
(
select ROW_NUMBER() OVER (PARTITION BY FI_CODE  ORDER BY case when [STATUS] = 2 then 1 else 0 end DESC) AS RN ,* 
from [ORACLECRM]..[SCOTT].[V_BOOKHT]
) as VB
where VB.RN = 1

SELECT  CCen.ID  as CENTERID, TI.[TEL_NUMBER] , BB.[BOOKHT_ID],ET.ETS_ID , TI.FI_CODE
into #T
FROM 
[ORACLECRM]..[SCOTT].[BASE_BOOKHT] AS BB 
JOIN #VBO AS VB ON  BB.[BOOKHT_ID] = VB.[BOOKHT_ID]
JOIN [ORACLECRM]..[SCOTT].[TELEPHONEINFORMATION] AS TI ON TI.[FI_CODE] = VB.[FI_CODE] 
JOIN #ETES AS ET ON ET.FI_CODE = TI.FI_CODE
JOIN [ORACLECRM]..[SCOTT].[CENTER] AS CEN ON TI.CENTER_CODE = CEN.CEN_CODE
JOIN CRM.dbo.Center as CCen ON CCen.CenterCode = CEN.CEN_CODE
JOIN [ORACLECRM]..[SCOTT].[CITY] AS CI ON CEN.[CI_CODE] = CI.[CI_CODE]
where BB.BOOKHT_TYPE in (select ElkaID from BuchtType where ID = 1 or ParentID = 1  )

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