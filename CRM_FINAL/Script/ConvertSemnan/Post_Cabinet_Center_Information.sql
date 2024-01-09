SELECT p.[POST_ID]
      ,p.[KAFU_ID]
      ,p.[POST_NUM]
      ,p.[CAPACITY]
      ,p.[ADDRESS]
      ,p.[POST_TYPE_ID]
      ,p.[GR_ID]
      ,p.[DISTANCE]
      ,p.[POSTCODE]
	  ,k.[CEN_CODE]
      ,k.[KAFU_NUM]
	  ,c.[CEN_NAME]
  FROM [ORACLECRM]..[SCOTT].[POST] AS p JOIN [ORACLECRM]..[SCOTT].[KAFU] AS k ON k. [KAFU_ID] = p.[KAFU_ID] JOIN [ORACLECRM]..[SCOTT].[CENTER] AS c ON k.[CEN_CODE] = c.[CEN_CODE]
 WHERE [POST_ID] in(1055228,1065585,1065725,1065738)
 --WHERE [POST_ID] in(1072812)
 
  --SELECT  COUNT(*)
  --FROM [ORACLECRM]..[SCOTT].[POST]
 --WHERE p.[KAFU_ID] =100938 AND p.[POST_NUM]= '30َ'
   --  WHERE [KAFU_ID] =100684 AND [POST_NUM]=N'31b'
	  

--GO

--SELECT        CabinetID, Number, COUNT(*) AS Expr1
--FROM            Post
--GROUP BY CabinetID, Number
--HAVING        (COUNT(*) > 1)


--SELECT
--     [KAFU_ID]
--   ,[POST_NUM]
--   ,COUNT(*) AS Expr1
--FROM [ORACLECRM]..[SCOTT].[POST]
--GROUP BY [KAFU_ID],[POST_NUM]
--HAVING        (COUNT(*) > 1)




