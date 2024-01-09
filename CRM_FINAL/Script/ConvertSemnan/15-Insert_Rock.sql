USE [CRM]

GO

DELETE [dbo].[PCMRock]
DBCC CHECKIDENT('PCMRock', RESEED,0)
INSERT INTO [dbo].[PCMRock]
           ([CenterID]
           ,[Number])
 SELECT 
      convert(INT,(select id from Center where CenterCode = p.[CEN_CODE]))
      ,p.[ROCK]
  FROM [ORACLECRM]..[TT].[PCM] AS p
  GROUP BY p.[CEN_CODE] ,p.[ROCK]
GO
GO


