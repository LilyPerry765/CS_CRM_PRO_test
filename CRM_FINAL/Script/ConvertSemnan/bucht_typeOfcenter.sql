
SELECT bb.[CEN_CODE] ,bb.[BOOKHT_TYPE], (select id  from Center where CenterCode = bb.[CEN_CODE])as CenterID ,bt.[BOOKHT_TYPE_NAME]
  FROM [ORACLECRM]..[SCOTT].[BASE_BOOKHT] as bb join [ORACLECRM]..[SCOTT].[BOOKHT_TYPE] as bt on bb.[BOOKHT_TYPE] = bt.[BOOKHT_TYPE_ID]
  group by bb.[CEN_CODE] , bb.[BOOKHT_TYPE] ,bt.[BOOKHT_TYPE_NAME]
  having [CEN_CODE] = 3
GO