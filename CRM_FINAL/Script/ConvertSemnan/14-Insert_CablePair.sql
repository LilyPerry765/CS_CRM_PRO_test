USE [CRM]
GO

INSERT INTO [dbo].[CablePair]
           ([CableID]
           ,[CabinetInputID]
           ,[CablePairNumber]
           ,[Status]
           ,[InsertDate]
           ,[ElkaID])
SELECT (SELECT ID FROM Cable WHERE ElkaID =  CP.[CABLE_ID])
      ,NULL
      ,CP.[CABLE_PAIR_NUM]
	  ,case when CP.[STATUS] = 1 then 2 -- خالی
	          when CP.[STATUS] = 2 then 1 -- پر
 			  when CP.[STATUS] = 3 then 3 -- خراب
			  when CP.[STATUS] = 4 then 1 --رزرو
			  when CP.[STATUS] = 5 then 4 -- در حال تعويض بوخت
			  when CP.[STATUS] = 7 then 6  END -- برگردان مرکز به مرکز
	  ,CRM.dbo.[sh2miByTime]([CABLE_PAIR_DATE] , [CABLE_PAIR_HOUR])
	  ,CP.[CABLE_PAIR_ID]
  FROM [ORACLECRM]..[SCOTT].[CABLE_PAIR] CP
GO


