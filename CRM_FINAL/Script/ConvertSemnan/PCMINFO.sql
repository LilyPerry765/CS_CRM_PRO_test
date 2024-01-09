SELECT 
      p.[PCM_ID]
      ,p.[CEN_CODE]
      ,convert(INT,(select c.id from Center c where c.CenterCode = p.[CEN_CODE]))
      ,p.[ROCK]
      ,p.[SHELF]
      ,p.[CARD]
      ,p.[PORT]
      ,p.[STATUS]
      ,p.[PCM_TYPE_ID]
      ,p.[REQ_ID]
	  ,b.[BOOKHT_ID]
	  ,b.[RADIF]
      ,b.[TABAGHE]
      ,b.[ETESALI]
      ,bt.[BOOKHT_TYPE_NAME]
	  , p.[STATUS]
	,  ap.[ETS_ID] 
	 ,kpb.[BOOKHT_ID]
    ,kpb.[RADIF]
   ,kpb.[TABAGHE]
     ,kpb.[ETESALI]
    ,kpb.[BOOKHT_TYPE]
   ,kpb.[STATUS]
  FROM [ORACLECRM]..[SCOTT].[PCM] AS p 
   JOIN [ORACLECRM]..[SCOTT].[PCM_CON] AS pc ON p.[PCM_ID] = pc.[PCM_ID]  
   JOIN [ORACLECRM]..[SCOTT].[BASE_BOOKHT] AS b ON pc.[BOOKHT_ID] = b.[BOOKHT_ID]
   JOIN [ORACLECRM]..[SCOTT].[BOOKHT_TYPE] AS bt ON bt.[BOOKHT_TYPE_ID] = b.[BOOKHT_TYPE]
   Left JOIN [ORACLECRM]..[SCOTT].[AIR_PCM] AS ap ON ap.[PCM_ID] = p.[PCM_ID]
   Left JOIN [ORACLECRM]..[SCOTT].[KAFU_PCM] AS kp ON kp.[PORT_ID] =  p.[PCM_ID]
   Left JOIN [ORACLECRM]..[SCOTT].[BASE_BOOKHT] AS kpb ON kpb.[BOOKHT_ID] = kp.[BOOKHT_ID]
     WHERE p.[CEN_CODE] =2 AND Rock = 1 AND shelf = 1 AND card = 6 


--   WHERE b.[BOOKHT_ID] = 1047
  -- JOIN [ORACLECRM]..[SCOTT].[BASE_BOOKHT] 
  -- GROUP BY   p.[STATUS]
--  where '1050' = b.[BOOKHT_ID]
		
   -- WHERE bt.[BOOKHT_TYPE_NAME] =  'بوخت PCMO' AND p.[PORT] <> 0
  --  WHERE 10059537 = p.[PCM_ID]
     --ORDER BY p.[CEN_CODE]


-- SELECT        dbo.PCMRock.CenterID, dbo.PCMRock.Number, dbo.PCMShelf.Number AS Expr1, dbo.PCM.Card, dbo.PCMPort.ID, dbo.PCMPort.PortNumber, dbo.PCMPort.ElkaID, 
--                         dbo.Bucht.ID AS Expr2, dbo.Bucht.BuchtNo, dbo.Bucht.ElkaID AS Expr3
--FROM            dbo.PCMPort INNER JOIN
--                         dbo.PCM ON dbo.PCMPort.PCMID = dbo.PCM.ID INNER JOIN
--                         dbo.Bucht ON dbo.PCMPort.ID = dbo.Bucht.PCMPortID INNER JOIN
--                         dbo.PCMRock INNER JOIN
--                         dbo.PCMShelf ON dbo.PCMRock.ID = dbo.PCMShelf.PCMRockID ON dbo.PCM.ShelfID = dbo.PCMShelf.ID
--WHERE        (dbo.PCMRock.CenterID = 2) AND (dbo.PCMRock.Number = 1) AND (dbo.PCMShelf.Number = 1) AND (dbo.PCM.Card = 1)
--GO


