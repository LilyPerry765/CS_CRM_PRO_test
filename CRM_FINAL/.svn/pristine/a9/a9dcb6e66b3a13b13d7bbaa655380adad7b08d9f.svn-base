--update  CB set ConnectionID = CPC.ID
--  -- COUNT(*)
--  --top(10)  TI.TEL_NUMBER , BB.BOOKHT_ID , BS.ETS_ID , CB.ID , CPC.ID
--  FROM         ElkaData.[TT].[TELEPHONEINFORMATION] AS TI 
--         JOIN  ElkaData.[TT].[V_BOOKHT] AS VB ON TI.[FI_CODE] = VB.[FI_CODE]
--	     JOIN  ElkaData.[TT].[BASE_BOOKHT] AS BB ON VB.[BOOKHT_ID] = BB.[BOOKHT_ID]
--	     JOIN  ElkaData.[TT].[ETESALI] AS E ON TI.[FI_CODE] = E.[FI_CODE]
--	     JOIN  ElkaData.[TT].[BASE_ETESALI] AS BS ON E.[ETS_ID] = BS.[ETS_ID]
--	     JOIN  ElkaData.[TT].[CENTER] AS C ON C.[CEN_CODE] = BB.[CEN_CODE]
--	     JOIN crm.dbo.Bucht as  CB on CB.ElkaID = BB.BOOKHT_ID
--	     JOIN crm.dbo.PostContact as  CPC on CPC.ElkaID = BS.ETS_ID
--	     where TI.TEL_STATUS = 2

-- update  Bucht set SwitchPortID = null

-- Normal Telephone
update  CB set CB.SwitchPortID = CT.SwitchPortID
  FROM   ElkaData.[TT].[TELEPHONEINFORMATION] AS TI 
         JOIN  ElkaData.[TT].[V_BOOKHT] AS VB ON TI.[FI_CODE] = VB.[FI_CODE]
	     JOIN  ElkaData.[TT].[BASE_BOOKHT] AS BB ON VB.[BOOKHT_ID] = BB.[BOOKHT_ID]
	     JOIN  ElkaData.[TT].[CENTER] AS C ON C.[CEN_CODE] = BB.[CEN_CODE]
		 join  ElkaData.[TT].[BOOKHT_TYPE] as BT on BT.BOOKHT_TYPE_ID = BB.BOOKHT_TYPE 
		 join Crm.dbo.Center as Cen on Cen.CenterCode = C.[CEN_CODE] 
	     JOIN crm.dbo.Bucht as  CB on CB.ElkaID = BB.BOOKHT_ID
		 join crm.dbo.Telephone as  CT on CT.TelephoneNoIndividual = TI.TEL_NUMBER and CT.CenterID = Cen.ID
	     where TI.TEL_STATUS = 2 and BT.INPUT_BOOKHT = 1
		 -- and TI.TEL_NUMBER = 33220049

update Bucht set SwitchPortID = null where BuchtIDConnectedOtherBucht is not null and Status = 13
-- PCM Telephone
update  CB set CB.SwitchPortID = CT.SwitchPortID
  FROM   ElkaData.[TT].[TELEPHONEINFORMATION] AS TI 
         JOIN  ElkaData.[TT].[V_BOOKHT] AS VB ON TI.[FI_CODE] = VB.[FI_CODE]
	     JOIN  ElkaData.[TT].[BASE_BOOKHT] AS BB ON VB.[BOOKHT_ID] = BB.[BOOKHT_ID]
	     JOIN  ElkaData.[TT].[CENTER] AS C ON C.[CEN_CODE] = BB.[CEN_CODE]
		 join  ElkaData.[TT].[BOOKHT_TYPE] as BT on BT.BOOKHT_TYPE_ID = BB.BOOKHT_TYPE 
		 join Crm.dbo.Center as Cen on Cen.CenterCode = C.[CEN_CODE] 
	     JOIN crm.dbo.Bucht as  CB on CB.ElkaID = BB.BOOKHT_ID
		 join crm.dbo.Telephone as  CT on CT.TelephoneNoIndividual = TI.TEL_NUMBER and CT.CenterID = Cen.ID
	     where TI.TEL_STATUS = 2 and BT.BOOKHT_TYPE_ID = 22 





		 