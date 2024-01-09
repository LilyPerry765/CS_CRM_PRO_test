USE [CRM]
GO
select   T.[RADIF] , T.MDF_FrameID
from(
      SELECT  
        (select id  from Center where CenterCode = bb.[CEN_CODE]) as Center_ID
		,(select id from BuchtType where bb.[BOOKHT_TYPE] = ElkaID and  (select id  from Center where CenterCode = bb.[CEN_CODE]) = CenterID) as Bucht_type
        ,(select mf.ID from MDFFrame as mf join MDF as m on mf.MDFID = m.ID where m.CenterID = (select id  from Center where CenterCode = bb.[CEN_CODE]) AND m.BuchtTypeID = (select id from BuchtType where bb.[BOOKHT_TYPE] = ElkaID and  (select id  from Center where CenterCode = bb.[CEN_CODE]) = CenterID)) as MDF_FrameID
		, bb.[RADIF]
        FROM [ORACLECRM]..[SCOTT].[BASE_BOOKHT] as bb join [ORACLECRM]..[SCOTT].[BOOKHT_TYPE] as bt on bb.[BOOKHT_TYPE] = bt.[BOOKHT_TYPE_ID]
        group by bb.[CEN_CODE] , bb.[BOOKHT_TYPE],bt.[BOOKHT_TYPE_NAME] ,bb.[RADIF]
		having bb.[CEN_CODE] = 1 ) T
		where T.MDF_FrameID = 1 
		order by T.[RADIF]

		
