SELECT BB.[BOOKHT_ID]
      ,BB.[CEN_CODE]
      ,BB.[RADIF]
      ,BB.[TABAGHE]
      ,BB.[ETESALI]
      ,BB.[BOOKHT_TYPE]
      ,BB.[STATUS]
      ,BB.[BB_DATE]
      ,BB.[BB_HOUR]
	  ,KI.[KAFU_INPUT]
	  ,K.[KAFU_NUM]
  FROM [ORACLECRM]..[SCOTT].[BASE_BOOKHT] as BB
  Left join [ORACLECRM]..[SCOTT].[KAFU_INPUT] as KI ON  BB.[BOOKHT_ID] = KI.[INPUT_ID]
  Left join [ORACLECRM]..[SCOTT].[KAFU] as K ON K.[KAFU_ID] = KI.[KAFU_ID] 
  where BB.[CEN_CODE] = 2 and [RADIF] = 1 and [TABAGHE] = 1 and [ETESALI] = 1
GO


