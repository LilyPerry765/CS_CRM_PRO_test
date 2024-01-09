SELECT 
	  --(SELECT ps.ID FROM PCMShelf ps, PCMRock  pr WHERE ps.PCMRockID = pr.ID AND pr.CenterID =  convert(INT,(select c.id from Center c where c.CenterCode = p.[CEN_CODE])) AND pr.Number = p.[ROCK] AND ps.Number = p.[SHELF])
	  (SELECT Pc.ID FROM PCM Pc , PCMRock pr , PCMShelf ps WHERE pc.ShelfID = ps.ID and  ps.PCMRockID = pr.ID and (Pc.Card COLLATE SQL_Latin1_General_CP1_CI_AS)= (p.[CARD] COLLATE SQL_Latin1_General_CP1_CI_AS) AND p.shelf = ps.Number and p.[ROCK] = pr.Number AND p.[PCM_TYPE_ID] = Pc.PCMTypeID AND pr.CenterID = convert(INT,(select c.id from Center c where c.CenterCode = p.[CEN_CODE])))
	 -- (SELECT Pc.ID FROM PCM Pc , PCMRock pr , PCMShelf ps WHERE pc.ShelfID = ps.ID and  ps.PCMRockID = pr.ID and (Pc.Card COLLATE SQL_Latin1_General_CP1_CI_AS)= '1' AND p.shelf = 1 and p.[ROCK] = 1 AND p.PCM_TYPE_ID = 12 AND pr.CenterID = convert(INT,(select c.id from Center c where c.CenterCode = 1)))
	 , p.[PORT]
	 ,p.[PCM_TYPE_ID]
	  --,(SELECT pt.[PCM_MARK_ID]  From [ORACLECRM]..[SCOTT].[PCM_TYPE] pt WHERE pt.[PCM_TYPE_ID] =  CASE WHEN p.[PCM_TYPE_ID] = 4 THEN 2 WHEN p.[PCM_TYPE_ID] = 5 THEN 12 WHEN p.[PCM_TYPE_ID] = 6 THEN 13 ELSE p.[PCM_TYPE_ID] END)
	  --,CASE WHEN p.[PCM_TYPE_ID] = 4 THEN 2  WHEN p.[PCM_TYPE_ID] = 5 THEN 12   WHEN p.[PCM_TYPE_ID] = 6 THEN 13 ELSE p.[PCM_TYPE_ID] END
	  --,NULL
   --   ,NULL
	  --,2
	
  FROM [ORACLECRM]..[SCOTT].[PCM] AS p
  ORDER BY p.[CEN_CODE]
GO


