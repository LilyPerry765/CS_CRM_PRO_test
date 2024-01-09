USE [CRM]
GO

DELETE PostContact
DBCC CHECKIDENT('PostContact', RESEED,0)


INSERT INTO [dbo].[PostContact]
           ([PostID]
           ,[ConnectionNo]
           ,[ConnectionType]
           ,[Status]
           ,[ElkaID])
SELECT 
	  p.ID
      ,be.[ETS_NUM]
      ,case when be.[PCM_STATUS] = 1 then 5 else 1 end
      ,case when be.[STATUS] = 1 then 5
	        when be.[STATUS] = 2 then 1
			when be.[STATUS] = 3 then 4
			when be.[STATUS] = 4 then 7
			when be.[STATUS] = 5 then 11
			when be.[STATUS] = 7 then 12
	  end
	  ,be.[ETS_ID]
  FROM [ORACLECRM]..[TT].[BASE_ETESALI] as be left join [ORACLECRM]..[TT].[AIR_PCM] as ap ON be.[ETS_ID] = ap.[ETS_ID]
       join CRM.dbo.Post as p on p.ElkaID = be.[POST_ID]

  INSERT INTO [dbo].[PostContact]
           ([PostID]
           ,[ConnectionNo]
           ,[ConnectionType]
           ,[Status]
           ,[ElkaID])
  SELECT 
	   p.ID
      ,be.[ETS_NUM]
      ,4
      ,0
	  ,be.[ETS_ID]
  FROM [ORACLECRM]..[TT].[BASE_ETESALI] as be 
      join CRM.dbo.Post as p on p.ElkaID = be.[POST_ID]
  where be.[PCM_STATUS] = 1 

GO


