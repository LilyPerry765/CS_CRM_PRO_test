
-- فیلتر روی 1 بخاطر وجود تایپ های مشترک برای مارک های مختلف بود که در اضافه کردن کارت ها باید لحاض شود
USE [CRM]
GO

DELETE [dbo].[PCMType]
DBCC CHECKIDENT('PCMType', RESEED,0)

INSERT INTO [dbo].[PCMType]
           ([Name]
           ,[OutLine]
           ,[AorB])
SELECT [PCM_TYPE_NAME]
      ,CASE WHEN [EXTRA] IS NULL THEN 3 WHEN [EXTRA] = 'a' THEN 1 WHEN [EXTRA] = 'b' THEN 2 END
      ,[PCM_TYPE_OUTPORT]
  FROM [ORACLECRM]..[TT].[PCM_TYPE]
  --WHERE [PCM_MARK_ID] = 1
GO


