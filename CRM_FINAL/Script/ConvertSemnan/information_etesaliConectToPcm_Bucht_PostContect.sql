SELECT be.[ETS_ID]
      ,be.[ETS_NUM]
      ,be.[METRAJH]
      ,be.[PCM_STATUS]
      ,be.[STATUS]
      ,be.[BE_DATE]
      ,be.[POST_ID]
      ,be.[BE_HOUR]
	 -- ,p.*
  FROM [ORACLECRM]..[SCOTT].[BASE_ETESALI] AS be 
        --   JOIN  [ORACLECRM]..[SCOTT].[AIR_PCM] AS ap ON ap.[ETS_ID] = be.[ETS_ID]
		--JOIN  [ORACLECRM]..[SCOTT].[PCM] AS p ON p.[PCM_ID] = ap.[PCM_ID]
		WHERE be.[PCM_STATUS] = 1



SELECT [ID]
      ,[PCMID]
      ,[PortNumber]
      ,[PortType]
      ,[Status]
      ,[ElkaID]
  FROM [dbo].[PCMPort]
  WHERE ElkaID IN (
  SELECT p.PCM_ID
  FROM [ORACLECRM]..[SCOTT].[BASE_ETESALI] AS be 
        JOIN  [ORACLECRM]..[SCOTT].[AIR_PCM] AS ap ON ap.[ETS_ID] = be.[ETS_ID]
		JOIN  [ORACLECRM]..[SCOTT].[PCM] AS p ON p.[PCM_ID] = ap.[PCM_ID]
  WHERE 10167740 = be.[ETS_ID])

SELECT [ID]
       ,ROW_NUMBER() OVER(ORDER BY [PortNo]) AS Row
      ,[MDFRowID]
      ,[SwitchPortID]
      ,[CablePairID]
      ,[CabinetInputID]
      ,[BuchtTypeID]
      ,[BuchtNo]
      ,[PCMPortID]
      ,[ADSLPortID]
      ,[ADSLType]
      ,[PortNo]
      ,[ConnectionID]
      ,[ConnectionIDBucht]
      ,[BuchtIDConnectedOtherBucht]
      ,[MDFUses]
      ,[ADSLStatus]
      ,[Status]
      ,[ElkaID]
  FROM [dbo].[Bucht] 
  WHERE [PCMPortID] IN (
						SELECT [ID]
						  FROM [dbo].[PCMPort]
						  WHERE ElkaID IN (
						  SELECT p.PCM_ID
						  FROM [ORACLECRM]..[SCOTT].[BASE_ETESALI] AS be 
								JOIN  [ORACLECRM]..[SCOTT].[AIR_PCM] AS ap ON ap.[ETS_ID] = be.[ETS_ID]
								JOIN  [ORACLECRM]..[SCOTT].[PCM] AS p ON p.[PCM_ID] = ap.[PCM_ID]
						  WHERE 10167740 = be.[ETS_ID]))
--SELECT [ID]
--      ,[CabinetID]
--      ,[PostTypeID]
--      ,[PostGroupID]
--      ,[Number]
--      ,[AorBType]
--      ,[FromPostContact]
--      ,[ToPostContact]
--      ,[Capacity]
--      ,[Distance]
--      ,[IsOutBorder]
--      ,[OutBorderMeter]
--      ,[PostalCode]
--      ,[Address]
--      ,[Status]
--  FROM [dbo].[Post]
--    WHERE ID = 1016794
--USE [CRM]
--GO

SELECT
      [ID]
	  ,ROW_NUMBER() OVER(ORDER BY ID) AS Row
      ,[PostID]
      ,[ConnectionNo]
      ,[ConnectionType]
      ,[Status]
      ,[ElkaID]
  FROM [dbo].[PostContact]
  WHERE [ElkaID] = 10167740 AND ConnectionType = 5
GO




