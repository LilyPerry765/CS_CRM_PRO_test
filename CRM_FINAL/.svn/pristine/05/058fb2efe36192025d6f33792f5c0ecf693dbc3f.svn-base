USE [CRM]
GO
DELETE VerticalMDFRow
DBCC CHECKIDENT ('VerticalMDFRow',RESEED , 0)

--IF OBJECT_ID(N'tempdb..#temp_BASE_BOOKHT', N'U') IS NOT NULL 
--DROP Table #temp_BASE_BOOKHT
--GO

--SELECT * INTO #temp_BASE_BOOKHT FROM [ORACLECRM]..[TT].[BASE_BOOKHT]
--UPDATE #temp_BASE_BOOKHT
--       SET #temp_BASE_BOOKHT.[BOOKHT_TYPE] = 22
--	   WHERE #temp_BASE_BOOKHT.[BOOKHT_TYPE] = 23

INSERT INTO [dbo].[VerticalMDFRow]
           ([VerticalMDFColumnID]
           ,[VerticalRowNo]
		   ,RowCapacity)
       SELECT T.VerticalMDFColumnID , T.[TABAGHE] , T.RowCapacity
  FROM
  (
        SELECT  
		--(select vc.id from  VerticalMDFColumn vc where bb.[TABAGHE] = vr.VerticalRowNo and vr.VerticalMDFColumnID = (select vc.id from VerticalMDFColumn as vc where   (select mf.ID from MDFFrame as mf join MDF as m on mf.MDFID = m.ID where m.CenterID = (select id  from Center where CenterCode = bb.[CEN_CODE]) AND m.BuchtTypeID = (select id from BuchtType where bb.[BOOKHT_TYPE] = ElkaID and  (select id  from Center where CenterCode = bb.[CEN_CODE]) = CenterID)) = vc.MDFFrameID and bb.RADIF = vc.VerticalCloumnNo))
		(SELECT        dbo.VerticalMDFColumn.ID
         FROM            dbo.MDF INNER JOIN
                         dbo.MDFFrame ON dbo.MDF.ID = dbo.MDFFrame.MDFID INNER JOIN
                         dbo.VerticalMDFColumn ON dbo.MDFFrame.ID = dbo.VerticalMDFColumn.MDFFrameID

		WHERE  dbo.VerticalMDFColumn.VerticalCloumnNo = bb.[RADIF]  
		       AND dbo.MDF.CenterID = (select id  from Center where CenterCode = bb.[CEN_CODE])
			   AND dbo.MDF.ID = CASE WHEN (select MDF.ID from MDF where CenterID = (select id  from Center where CenterCode = bb.[CEN_CODE]) AND ElkaBuchtTypeID in (bb.[BOOKHT_TYPE]) ) IS NOT NULL
							    THEN (select MDF.ID from MDF where CenterID = (select id  from Center where CenterCode = bb.[CEN_CODE]) AND bb.[BOOKHT_TYPE] = ElkaBuchtTypeID)
							    ELSE
							    (select MDF.ID from MDF where CenterID = (select id  from Center where CenterCode = bb.[CEN_CODE]) AND ElkaBuchtTypeID = -1)
						   	    END  

			   ) as VerticalMDFColumnID
		,bb.[TABAGHE]
		,0 as RowCapacity
        FROM [ORACLECRM]..[TT].[BASE_BOOKHT] as bb 
        group by bb.[CEN_CODE] ,bb.[RADIF], bb.[TABAGHE] , bb.[BOOKHT_TYPE]
	) as T
Group BY T.VerticalMDFColumnID , T.[TABAGHE] , T.RowCapacity

--DROP Table #temp_BASE_BOOKHT
GO
