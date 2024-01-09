USE [CRM]
GO
DELETE CRM.dbo.Cabinet
DBCC CHECKIDENT('CRM.dbo.Cabinet', RESEED,0)

INSERT INTO [dbo].[Cabinet]
           (
		    [CenterID]
           ,[CabinetNumber]
           ,[ABType]
           ,[CabinetCode]
           ,[CabinetTypeID]
           ,[CabinetUsageType]
           ,[FromInputNo]
           ,[ToInputNo]
           ,[DistanceFromCenter]
           ,[IsOutBound]
           ,[OutBoundMeter]
           ,[Address]
           ,[PostCode]
           ,[FromPostalCode]
           ,[ToPostalCode]
           ,[Status]
           ,[Capacity]
		   ,ElkaID)
		   SELECT
		    convert(INT,(select id from Center where CenterCode = [osk].[CEN_CODE]))
            --, IIF (ISNUMERIC([osk].[KAFU_NUM]) <> 0 ,[osk].[KAFU_NUM] ,REPLACE(REPLACE([osk].[KAFU_NUM] ,'dect' ,'1234') , 'n' , '5'))
		   ,[osk].[KAFU_NUM]
           ,1
           ,[osk].[KAFU_NUM]
		   ,(SELECT ID FROM dbo.CabinetType WHERE ElkaID = (select KAFUCAP.[KAFU_CAP_ID] from [ORACLECRM]..[TT].[KAFU_CAP] AS KAFUCAP where [osk].[CAPACITY] = [KAFUCAP].[KAFU_CAP_ID]) )
		   ,CASE WHEN [osk].[KAFU_TYPE_ID] = 21 THEN 1
		         WHEN [osk].[KAFU_TYPE_ID] = 22 THEN 2
				 WHEN [osk].[KAFU_TYPE_ID] = 1  THEN 3
                 WHEN [osk].[KAFU_TYPE_ID] = 2  THEN 4
				 WHEN [osk].[KAFU_TYPE_ID] = 41 THEN 5 END
           ,NULL
           ,NULL
           ,IIF([osk].[DISTANCE] is null , 0 , [osk].[DISTANCE])
           ,0
           ,null
           ,convert (NVARCHAR(1000),[osk].[ADDRESS])
           ,convert(nvarchar(50),[osk].[POSTCODE])
           ,0
           ,0
           ,0
           ,convert (INT ,(select KAFUCAP.[KAFU_CAP_CAP] from [ORACLECRM]..[TT].[KAFU_CAP] AS KAFUCAP where [osk].[CAPACITY] = [KAFUCAP].[KAFU_CAP_ID]))
		   ,[osk].[KAFU_ID]
		    FROM [ORACLECRM]..[TT].[KAFU] as osk
GO
