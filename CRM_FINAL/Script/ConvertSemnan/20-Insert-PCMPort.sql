USE [CRM]
GO

DELETE [dbo].[PCMPort]
DBCC CHECKIDENT('PCMPort', RESEED,0)

INSERT INTO [dbo].[PCMPort]
          ([PCMID]
         ,[PortNumber]
          ,[PortType]
         ,[Status]
	   ,[ElkaID])
SELECT 
	   (SELECT        dbo.PCM.ID
       FROM            dbo.PCM INNER JOIN
                         dbo.PCMShelf ON dbo.PCM.ShelfID = dbo.PCMShelf.ID INNER JOIN
                         dbo.PCMRock ON dbo.PCMShelf.PCMRockID = dbo.PCMRock.ID
	  WHERE        (dbo.PCM.Card = (IIF(ISNUMERIC(p.[CARD]) <> 0 , p.[CARD] ,REPLACE(REPLACE(p.[CARD] , 'a' , '1') , 'b','2')) COLLATE SQL_Latin1_General_CP1_CI_AS)) 
	                AND (dbo.PCMShelf.Number = p.shelf)
					AND (dbo.PCMRock.Number = p.[ROCK])
					AND (dbo.PCM.PCMTypeID = CASE WHEN p.[PCM_TYPE_ID] = 4 THEN 2
	                                              WHEN p.[PCM_TYPE_ID] = 5 THEN 12
		                                          WHEN p.[PCM_TYPE_ID] = 6 THEN 13 
			                                      ELSE p.[PCM_TYPE_ID] END)
					AND (dbo.PCM.PCMBrandID = pt.[PCM_MARK_ID])
				    AND (dbo.PCMRock.CenterID = convert(INT,(select c.id from Center c where c.CenterCode = p.[CEN_CODE])))) 
	 ,p.[PORT]
	 , CASE WHEN p.[PORT] = 0  THEN 9 ELSE 8 END	

	 , CASE WHEN p.[STATUS] =1 THEN 1
	        WHEN p.[STATUS] =2 THEN 2
			WHEN p.[STATUS] =3 THEN 3
			WHEN p.[STATUS] =4 THEN 4
			WHEN p.[STATUS] =5 THEN 2
			WHEN p.[STATUS] =7 THEN 2 END
			
     ,p.[PCM_ID]
  FROM [ORACLECRM]..[SCOTT].[PCM] AS p
    JOIN [ORACLECRM]..[SCOTT].[PCM_TYPE] pt ON p.[PCM_TYPE_ID] = pt.[PCM_TYPE_ID]

  ORDER BY p.[PCM_ID]

GO


