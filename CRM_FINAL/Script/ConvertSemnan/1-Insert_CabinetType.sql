

DELETE CRM.dbo.CabinetType
DBCC CHECKIDENT('CRM.dbo.CabinetType', RESEED,0)

INSERT INTO  CRM.dbo.CabinetType (CabinetTypeName , CabinetCapacity , ElkaID)

SELECT 
      [KAFU_CAP_NAME]
      ,[KAFU_CAP_CAP]
	  ,[KAFU_CAP_ID]
  FROM [ORACLECRM]..[TT].[KAFU_CAP]
GO

--update Ka set KA.KAFU_CAP_NAME  = 1001  FROM [ORACLECRM]..[TT].[KAFU_CAP] as ka where KAFU_CAP_ID = 11


