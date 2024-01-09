USE [CRM]
GO

DELETE [dbo].[PCMShelf]
DBCC CHECKIDENT('PCMShelf', RESEED,0)

INSERT INTO [dbo].[PCMShelf]
           ([PCMRockID]
           ,[Number])
SELECT 
	   (SELECT pr.ID FROM PCMRock pr WHERE pr.CenterID =  convert(INT,(select id from Center where CenterCode = p.[CEN_CODE])) AND pr.Number = p.[ROCK])
	  ,p.[SHELF]
  FROM [ORACLECRM]..[TT].[PCM] AS p
  GROUP BY p.[CEN_CODE] ,p.[ROCK],p.[SHELF]
  ORDER BY p.[CEN_CODE]
GO


