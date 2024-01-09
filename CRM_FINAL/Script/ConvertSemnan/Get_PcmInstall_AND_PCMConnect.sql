SELECT   case when count(ap.[ETS_ID]) > 0 then 0 else 1 end
  FROM [ORACLECRM]..[SCOTT].[PCM] AS p 
   JOIN [ORACLECRM]..[SCOTT].[PCM_CON] AS pc ON p.[PCM_ID] = pc.[PCM_ID]  
   JOIN [ORACLECRM]..[SCOTT].[BASE_BOOKHT] AS b ON pc.[BOOKHT_ID] = b.[BOOKHT_ID]
   JOIN [ORACLECRM]..[SCOTT].[BOOKHT_TYPE] AS bt ON bt.[BOOKHT_TYPE_ID] = b.[BOOKHT_TYPE]
   Left JOIN [ORACLECRM]..[SCOTT].[AIR_PCM] AS ap ON ap.[PCM_ID] = p.[PCM_ID]
   Left JOIN [ORACLECRM]..[SCOTT].[KAFU_PCM] AS kp ON kp.[PORT_ID] =  p.[PCM_ID]
   Left JOIN [ORACLECRM]..[SCOTT].[BASE_BOOKHT] AS kpb ON kpb.[BOOKHT_ID] = kp.[BOOKHT_ID]
  WHERE p.[CEN_CODE] =2 AND Rock = 1 AND shelf = 1 AND card = 7 and p.[PCM_TYPE_ID] = 12
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


