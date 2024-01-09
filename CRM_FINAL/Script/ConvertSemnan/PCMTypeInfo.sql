

SELECT [PCM_TYPE_ID]
      ,[PCM_TYPE_NAME]
      ,CASE WHEN [EXTRA] IS NULL THEN 3 WHEN [EXTRA] = 'a' THEN 1 WHEN [EXTRA] = 'b' THEN 2 END
      ,[PCM_TYPE_OUTPORT]
	  ,[PCM_MARK_ID]
  FROM [ORACLECRM]..[SCOTT].[PCM_TYPE]
  WHERE [PCM_MARK_ID] = 1
   
GO


SELECT [PCM_TYPE_ID]
      ,[PCM_TYPE_NAME]
      ,CASE WHEN [EXTRA] IS NULL THEN 3 WHEN [EXTRA] = 'a' THEN 1 WHEN [EXTRA] = 'b' THEN 2 END
      ,[PCM_TYPE_OUTPORT]
	  ,[PCM_MARK_ID]
  FROM [ORACLECRM]..[SCOTT].[PCM_TYPE]
  WHERE [PCM_MARK_ID] = 2
   
GO

