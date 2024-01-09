USE [CRM]
GO
DELETE [MDFFrame]
DBCC CHECKIDENT ('MDFFrame',RESEED , 0)
DELETE [MDF]
DBCC CHECKIDENT ('MDF',RESEED , 0)

INSERT INTO [dbo].[MDF]
           ([CenterID]
           ,[Type]
           ,[Number]
           ,[LastNoVerticalFrames]
           ,[LastNoHorizontalFrames]
           ,[Description]
		   ,[ElkaBuchtTypeID])
SELECT  
        (select id  from Center where CenterCode = bb.[CEN_CODE])
		,0
		,ROW_NUMBER() OVER (PARTITION BY bb.[CEN_CODE] ORDER BY bb.[CEN_CODE]) AS number
		,null
		,null
		,'اصلی مرکز'
		,-1
        FROM [ORACLECRM]..[TT].[BASE_BOOKHT] as bb
        group by bb.[CEN_CODE] 


INSERT INTO [dbo].[MDF]
           ([CenterID]
           ,[Type]
           ,[Number]
           ,[LastNoVerticalFrames]
           ,[LastNoHorizontalFrames]
           ,[Description]
		   ,[ElkaBuchtTypeID])
					SELECT  
							(select id  from Center where CenterCode = T.[CEN_CODE])
							,0
							,ROW_NUMBER() OVER (PARTITION BY T.[CEN_CODE] ORDER BY T.[CEN_CODE])+1 AS number
							--,(select MAX(Number) + 1 from MDF where CenterID = (select id  from Center where CenterCode = T.[CEN_CODE]))
							,null
							,null
							,T.[BOOKHT_TYPE_NAME]
							,T.[BOOKHT_TYPE]
					FROM -- محاسبه بوخت های تکرار برای جدا سازی ام دی اف انها
					(
					(    
					  SELECT [BOOKHT_ID]
						  ,[CEN_CODE]
						  ,[RADIF]
						  ,[TABAGHE]
						  ,[ETESALI]
						  ,[BOOKHT_TYPE]
						  ,[STATUS]
						  ,[BB_DATE]
						  ,[BB_HOUR]
						  ,[BOOKHT_TYPE_NAME]
					  FROM [ORACLECRM]..[TT].[BASE_BOOKHT] JOIN [ORACLECRM]..[TT].[BOOKHT_TYPE] ON  [BOOKHT_TYPE] = [BOOKHT_TYPE_ID]
					  ) as T
					  join
					  (
					  SELECT 
						  [CEN_CODE]
						  ,[RADIF] 
						  ,[TABAGHE] 
						  ,[ETESALI] 
						  ,COUNT(*) AS num
					  FROM [ORACLECRM]..[TT].[BASE_BOOKHT]
					  GROUP BY [CEN_CODE],[RADIF],[TABAGHE],[ETESALI]
					  HAVING (COUNT(*)>1)
					  ) AS C ON T.[CEN_CODE] = C.[CEN_CODE] and T.[RADIF] = C.[RADIF] and T.[TABAGHE] = C.[TABAGHE] and T.[ETESALI] = C.[ETESALI])

					  GROUP BY T.[CEN_CODE] , T.[BOOKHT_TYPE] , T.[BOOKHT_TYPE_NAME]
					   HAVING T.[BOOKHT_TYPE] != 21




INSERT INTO [dbo].[MDFFrame]
           ([MDFID]
           ,[FrameNo]
)
SELECT [ID]
      ,1
  FROM [dbo].[MDF]

GO





