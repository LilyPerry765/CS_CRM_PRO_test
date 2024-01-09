
print('start ...');
DECLARE @Etesali_id bigint
DECLARE Etesali_Cursor CURSOR Read_only FOR SELECT be.[ETS_ID] FROM [ORACLECRM]..[TT].[BASE_ETESALI] AS be  WHERE be.[PCM_STATUS] = 1
OPEN Etesali_Cursor;
FETCH NEXT FROM Etesali_Cursor INTO @Etesali_id;
WHILE @@FETCH_STATUS = 0
   BEGIN
   print(@Etesali_id);
					    UPDATE [dbo].[Bucht] 
						SET [ConnectionID] = C.ID
						FROM [dbo].[Bucht] 
						INNER JOIN (
						SELECT [ID]
							   ,ROW_NUMBER() OVER(ORDER BY [PortNo]) AS Row
							  ,[MDFRowID]
							  ,[SwitchPortID]
							  ,[CablePairID]
							  ,[CabinetInputID]
							  ,[BuchtTypeID]
							  ,[BuchtNo]
							  ,[PCMPortID]
							  ,[PortNo]
							  ,[ConnectionID]
							  ,[ConnectionIDBucht]
							  ,[BuchtIDConnectedOtherBucht]
							  ,[ADSLStatus]
							  ,[Status]
							  ,[ElkaID]
						  FROM [dbo].[Bucht] 
						  WHERE [PCMPortID] IN (
												SELECT [ID]
												  FROM [dbo].[PCMPort]
												  WHERE ElkaID IN (
												  SELECT p.PCM_ID
												  FROM [ORACLECRM]..[TT].[BASE_ETESALI] AS be 
														JOIN  [ORACLECRM]..[TT].[AIR_PCM] AS ap ON ap.[ETS_ID] = be.[ETS_ID]
														JOIN  [ORACLECRM]..[TT].[PCM] AS p ON p.[PCM_ID] = ap.[PCM_ID]
												  WHERE @Etesali_id = be.[ETS_ID]))) B ON B.ID = [dbo].[Bucht].ID
												  INNER JOIN (
						SELECT
							  [ID]
							  ,ROW_NUMBER() OVER(ORDER BY ID) AS Row
							  ,[PostID]
							  ,[ConnectionNo]
							  ,[ConnectionType]
							  ,[Status]
							  ,[ElkaID]
						  FROM [dbo].[PostContact]
						  WHERE [ElkaID] = @Etesali_id AND ConnectionType = 5) C ON C.Row = B.Row

      FETCH NEXT FROM Etesali_Cursor INTO @Etesali_id;
   END;
CLOSE Etesali_Cursor;
DEALLOCATE Etesali_Cursor;
GO



