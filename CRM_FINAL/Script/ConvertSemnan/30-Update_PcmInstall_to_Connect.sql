  SELECT 
	   (SELECT          dbo.PCMShelf.ID
        FROM            dbo.PCMShelf INNER JOIN
                        dbo.PCMRock ON dbo.PCMShelf.PCMRockID = dbo.PCMRock.ID
	    WHERE 	dbo.PCMRock.CenterID = 	convert(INT,(select c.id from Center c where c.CenterCode = p.[CEN_CODE]))
			       AND 	dbo.PCMRock.Number = p.[ROCK]
				   AND  dbo.PCMShelf.Number = p.[SHELF]
						)
	  ,p.[CARD]
	  , pt.[PCM_MARK_ID] 
	  ,CASE WHEN p.[PCM_TYPE_ID] = 4 THEN 2
	        WHEN p.[PCM_TYPE_ID] = 5 THEN 12
		    WHEN p.[PCM_TYPE_ID] = 6 THEN 13 
			ELSE p.[PCM_TYPE_ID] END
	  ,NULL
    	,NULL
	,(SELECT   case when count(kp.[BOOKHT_ID]) > 0 then 2 else 1 end
      FROM [ORACLECRM]..[SCOTT].[PCM] AS ps 
			JOIN [ORACLECRM]..[SCOTT].[PCM_CON] AS pc ON ps.[PCM_ID] = pc.[PCM_ID]  
			JOIN [ORACLECRM]..[SCOTT].[BASE_BOOKHT] AS b ON pc.[BOOKHT_ID] = b.[BOOKHT_ID]
			JOIN [ORACLECRM]..[SCOTT].[BOOKHT_TYPE] AS bt ON bt.[BOOKHT_TYPE_ID] = b.[BOOKHT_TYPE]
			Left JOIN [ORACLECRM]..[SCOTT].[AIR_PCM] AS ap ON ap.[PCM_ID] = ps.[PCM_ID]
			Left JOIN [ORACLECRM]..[SCOTT].[KAFU_PCM] AS kp ON kp.[PORT_ID] =  ps.[PCM_ID]
			Left JOIN [ORACLECRM]..[SCOTT].[BASE_BOOKHT] AS kpb ON kpb.[BOOKHT_ID] = kp.[BOOKHT_ID]
			WHERE p.[CEN_CODE] =ps.[CEN_CODE] AND p.Rock = ps.Rock AND p.shelf = ps.shelf AND p.card = ps.card and p.[PCM_TYPE_ID] = ps.[PCM_TYPE_ID])

  FROM [ORACLECRM]..[SCOTT].[PCM] AS p JOIN [ORACLECRM]..[SCOTT].[PCM_TYPE] pt ON p.[PCM_TYPE_ID] = pt.[PCM_TYPE_ID]
  GROUP BY p.[CEN_CODE] ,p.[ROCK],p.[SHELF] , p.[CARD] ,p.[PCM_TYPE_ID],pt.[PCM_MARK_ID] 
 