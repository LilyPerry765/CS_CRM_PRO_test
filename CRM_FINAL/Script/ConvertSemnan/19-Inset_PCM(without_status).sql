USE [CRM]
GO
-- ابتدا نوع فیلد کارت را در جدول پی سی ام ورکر قرار داده شد(بعلت وجود اطلاعات رشته ای ) سپس بعد از ورود اطلاعات پورت ها فیلد آپدید شد و مقدار رشته ها به عدد تغییر پیدا کرد

DELETE [dbo].[PCM]
DBCC CHECKIDENT('PCM', RESEED,0)

INSERT INTO [dbo].[PCM]
           ([ShelfID]
           ,[Card]
           ,[PCMBrandID]
           ,[PCMTypeID]
           ,[InstallAddress]
           ,[InstallPostCode]
           ,[Status])
  SELECT 
	   (SELECT        dbo.PCMShelf.ID
        FROM            dbo.PCMShelf INNER JOIN
                        dbo.PCMRock ON dbo.PCMShelf.PCMRockID = dbo.PCMRock.ID
	    WHERE 	dbo.PCMRock.CenterID = 	convert(INT,(select c.id from Center c where c.CenterCode = p.[CEN_CODE]))
			       AND 	dbo.PCMRock.Number = p.[ROCK]
				   AND  dbo.PCMShelf.Number = p.[SHELF]
						)
	  ,IIF(ISNUMERIC(p.[CARD]) <> 0 , p.[CARD] ,REPLACE(REPLACE(p.[CARD] , 'a' , '1') , 'b','2'))
	  , pt.[PCM_MARK_ID] 
	  ,CASE WHEN p.[PCM_TYPE_ID] = 4 THEN 2
	        WHEN p.[PCM_TYPE_ID] = 5 THEN 12
		    WHEN p.[PCM_TYPE_ID] = 6 THEN 13 
			ELSE p.[PCM_TYPE_ID] END
	  ,NULL
    	,NULL
	,(SELECT   case when count(ap.[ETS_ID]) > 0 then 2 else 1 end
      FROM [ORACLECRM]..[TT].[PCM] AS ps 
			JOIN [ORACLECRM]..[TT].[PCM_CON] AS pc ON ps.[PCM_ID] = pc.[PCM_ID]  
			JOIN [ORACLECRM]..[TT].[BASE_BOOKHT] AS b ON pc.[BOOKHT_ID] = b.[BOOKHT_ID]
			JOIN [ORACLECRM]..[TT].[BOOKHT_TYPE] AS bt ON bt.[BOOKHT_TYPE_ID] = b.[BOOKHT_TYPE]
			Left JOIN [ORACLECRM]..[TT].[AIR_PCM] AS ap ON ap.[PCM_ID] = ps.[PCM_ID]
			Left JOIN [ORACLECRM]..[TT].[KAFU_PCM] AS kp ON kp.[PORT_ID] =  ps.[PCM_ID]
			Left JOIN [ORACLECRM]..[TT].[BASE_BOOKHT] AS kpb ON kpb.[BOOKHT_ID] = kp.[BOOKHT_ID]
			WHERE p.[CEN_CODE] =ps.[CEN_CODE] AND p.Rock = ps.Rock AND p.shelf = ps.shelf AND p.card = ps.card and p.[PCM_TYPE_ID] = ps.[PCM_TYPE_ID])
	
  FROM [ORACLECRM]..[TT].[PCM] AS p
       JOIN [ORACLECRM]..[TT].[PCM_TYPE] pt ON p.[PCM_TYPE_ID] = pt.[PCM_TYPE_ID]
  GROUP BY p.[CEN_CODE] ,p.[ROCK],p.[SHELF] , p.[CARD] ,p.[PCM_TYPE_ID],pt.[PCM_MARK_ID] 
GO


