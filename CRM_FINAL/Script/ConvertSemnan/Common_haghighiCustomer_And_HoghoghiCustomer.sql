SELECT T.*
      , C.*
  FROM [ORACLECRM]..[SCOTT].[HOOGHOOGHI_CUSTOMER] T JOIN  [ORACLECRM]..[SCOTT].[HAGHIGHI_CUSTOMER] C ON T.[FI_CODE] = C.[FI_CODE]
GO


