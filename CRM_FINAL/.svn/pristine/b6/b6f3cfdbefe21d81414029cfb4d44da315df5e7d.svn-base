USE [CRM]
GO


DELETE [Bucht]
DBCC CHECKIDENT ('Bucht',RESEED , 0)



--IF OBJECT_ID(N'tempdb..#temp_BASE_BOOKHT', N'U') IS NOT NULL 
--DROP Table #temp_BASE_BOOKHT
--GO

--SELECT * INTO #temp_BASE_BOOKHT FROM [ORACLECRM]..[TT].[BASE_BOOKHT]
--UPDATE #temp_BASE_BOOKHT
--       SET #temp_BASE_BOOKHT.[BOOKHT_TYPE] = 22
--	   WHERE #temp_BASE_BOOKHT.[BOOKHT_TYPE] = 23

INSERT INTO [dbo].[Bucht]
           (
		    [MDFRowID]
           ,[SwitchPortID]
           ,[CablePairID]
           ,[CabinetInputID]
		   ,[BuchtTypeID]
           ,[BuchtNo]
           ,[PCMPortID]
           ,[PortNo]
           ,[ConnectionID]
           ,[ConnectionIDBucht]
           ,[BuchtIDConnectedOtherBucht]
           ,[ADSLStatus]
           ,[Status]
		   ,ElkaID
		   )
SELECT  
		(select vr.id from  VerticalMDFRow as vr 
		where bb.[TABAGHE] = vr.VerticalRowNo 
		and vr.VerticalMDFColumnID = (select vc.id from VerticalMDFColumn as vc 
		                                     where  vc.MDFFrameID  =  (select mf.ID from MDFFrame as mf join MDF as m on mf.MDFID = m.ID 
											                             where m.CenterID = (select id  from Center where CenterCode = bb.[CEN_CODE]) 
																		      AND  m.ID = CASE WHEN (select MDF.ID from MDF where CenterID = (select id  from Center where CenterCode = bb.[CEN_CODE]) AND ElkaBuchtTypeID in (bb.[BOOKHT_TYPE]) ) IS NOT NULL
																						THEN (select MDF.ID from MDF where CenterID = (select id  from Center where CenterCode = bb.[CEN_CODE]) AND bb.[BOOKHT_TYPE] = ElkaBuchtTypeID)
																						ELSE
																						(select MDF.ID from MDF where CenterID = (select id  from Center where CenterCode = bb.[CEN_CODE]) AND ElkaBuchtTypeID = -1)
						   																END  ) 
																		 and bb.RADIF = vc.VerticalCloumnNo))
		,null
		,null
		,null
		,(select id from BuchtType where  bb.[BOOKHT_TYPE]= ElkaID )
		,bb.ETESALI
		,null
		,null
		,null
		,null
        ,null
		,0
		,case when bb.[STATUS] = 1 then 0
	          when bb.[STATUS] = 2 then 1
			  when bb.[STATUS] = 3 then 2
			  when bb.[STATUS] = 4 then 3
			  when bb.[STATUS] = 5 then 11
			  when bb.[STATUS] = 7 then 18 
			  else 0 end
        ,bb.[BOOKHT_ID]
        FROM [ORACLECRM]..[TT].[BASE_BOOKHT] as bb join [ORACLECRM]..[TT].[BOOKHT_TYPE] as bt on bb.[BOOKHT_TYPE] = bt.[BOOKHT_TYPE_ID]

		--DROP Table #temp_BASE_BOOKHT
    
GO


