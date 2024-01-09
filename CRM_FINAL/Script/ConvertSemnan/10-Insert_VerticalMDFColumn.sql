USE [CRM]
GO
DELETE [VerticalMDFColumn]
DBCC CHECKIDENT ('VerticalMDFColumn',RESEED , 0)

--IF OBJECT_ID(N'tempdb..#temp_BASE_BOOKHT', N'U') IS NOT NULL 
--DROP Table #temp_BASE_BOOKHT
--GO

--SELECT * INTO #temp_BASE_BOOKHT FROM [ORACLECRM]..[TT].[BASE_BOOKHT]
--UPDATE #temp_BASE_BOOKHT
--       SET #temp_BASE_BOOKHT.[BOOKHT_TYPE] = 22
--	   WHERE #temp_BASE_BOOKHT.[BOOKHT_TYPE] = 23



INSERT INTO [dbo].[VerticalMDFColumn]
           ([VerticalCloumnNo]
           ,[MDFFrameID])
      SELECT   T.RADIF , T.MDF_FrameID
FROM

(

        SELECT 
		bb.[RADIF]
        , (select mf.ID 
			from MDFFrame as mf join MDF as m on mf.MDFID = m.ID 
			where m.CenterID = (select id  from Center where CenterCode = bb.[CEN_CODE])
			      and m.ID = CASE WHEN (select MDF.ID from MDF where CenterID = (select id  from Center where CenterCode = bb.[CEN_CODE]) AND ElkaBuchtTypeID in (bb.[BOOKHT_TYPE]) ) IS NOT NULL
							 THEN (select MDF.ID from MDF where CenterID = (select id  from Center where CenterCode = bb.[CEN_CODE]) AND bb.[BOOKHT_TYPE] = ElkaBuchtTypeID)
							 ELSE
							 (select MDF.ID from MDF where CenterID = (select id  from Center where CenterCode = bb.[CEN_CODE]) AND ElkaBuchtTypeID = -1)
						   	END  
				  )as MDF_FrameID
        FROM [ORACLECRM]..[TT].[BASE_BOOKHT] as bb
        group by bb.[CEN_CODE] ,  bb.[RADIF] , bb.[BOOKHT_TYPE]
) as T
GROUP BY T.MDF_FrameID , T.RADIF 

----DROP Table #temp_BASE_BOOKHT
--GO
--SELECT   T.RADIF , T.MDF_FrameID
--FROM

--(

--        SELECT 
--		bb.[RADIF]
--        , (select mf.ID 
--			from MDFFrame as mf join MDF as m on mf.MDFID = m.ID 
--			where m.CenterID = (select id  from Center where CenterCode = bb.[CEN_CODE])
--			      and m.ID = CASE WHEN (select MDF.ID from MDF where CenterID = (select id  from Center where CenterCode = bb.[CEN_CODE]) AND ElkaBuchtTypeID in (bb.[BOOKHT_TYPE]) ) IS NOT NULL
--							 THEN (select MDF.ID from MDF where CenterID = (select id  from Center where CenterCode = bb.[CEN_CODE]) AND bb.[BOOKHT_TYPE] = ElkaBuchtTypeID)
--							 ELSE
--							 (select MDF.ID from MDF where CenterID = (select id  from Center where CenterCode = bb.[CEN_CODE]) AND ElkaBuchtTypeID = -1)
--						   	END  
--				  )as MDF_FrameID
--        FROM [ORACLECRM]..[TT].[BASE_BOOKHT] as bb
--        group by bb.[CEN_CODE] ,  bb.[RADIF] , bb.[BOOKHT_TYPE]
--) as T
--GROUP BY T.MDF_FrameID , T.RADIF 





