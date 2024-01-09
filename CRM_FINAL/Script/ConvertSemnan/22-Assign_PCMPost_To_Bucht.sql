USE [CRM]
GO
-- وضعیت 3 چون مشخص نبود برای یک پورت متصل به مشترک گذاشته شده است یا پورت آزاد موقتا 20 گزاشته شد
UPDATE [dbo].[Bucht]
   SET 
       [BuchtTypeID] = CASE WHEN bt.[BOOKHT_TYPE_ID] = 22 THEN 8 WHEN bt.[BOOKHT_TYPE_ID] = 23  THEN 9 END
      ,[PCMPortID] = (SELECT PCMPort.ID FROM PCMPort WHERE PCMPort.ElkaID = p.[PCM_ID])
      ,[PortNo] = (SELECT PCMPort.PortNumber FROM PCMPort WHERE PCMPort.ElkaID = p.[PCM_ID])
      ,[Status] =  case when b.[STATUS] = 1 then 7
	                    when b.[STATUS] = 2 then 1
						when b.[STATUS] = 3 then 20
						when b.[STATUS] = 4 then 3
						when b.[STATUS] = 5 then 11
						when b.[STATUS] = 7 then 18 END
 FROM [ORACLECRM]..[SCOTT].[PCM] AS p 
   JOIN [ORACLECRM]..[SCOTT].[PCM_CON] AS pc ON p.[PCM_ID] = pc.[PCM_ID]  
   JOIN [ORACLECRM]..[SCOTT].[BASE_BOOKHT] AS b ON pc.[BOOKHT_ID] = b.[BOOKHT_ID]
   JOIN [ORACLECRM]..[SCOTT].[BOOKHT_TYPE] AS bt ON bt.[BOOKHT_TYPE_ID] = b.[BOOKHT_TYPE]
   WHERE ElkaID = b.[BOOKHT_ID]
GO

SELECT [STATUS_ID]
      ,[STATUS_NAME]
  FROM [ORACLECRM]..[SCOTT].[STATUS]
  ORDER BY [STATUS_ID]
