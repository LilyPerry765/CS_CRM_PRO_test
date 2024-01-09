USE [CRM]
GO
--ALTER DATABASE [CRM]
--	SET SINGLE_USER WITH ROLLBACK IMMEDIATE
GO

DELETE Post
DBCC CHECKIDENT('Post', RESEED,0)

INSERT INTO [dbo].[Post]
           (
		    [CabinetID]
           ,[PostTypeID]
           ,[PostGroupID]
           ,[Number]
           ,[AorBType]
           ,[FromPostContact]
           ,[ToPostContact]
           ,[Capacity]
           ,[Distance]
           ,[IsOutBorder]
           ,[OutBorderMeter]
           ,[PostalCode]
           ,[Address]
           ,[Status]
		   ,ElkaID
		   )
   SELECT 
	   C.ID
	  ,(case when postID.[POST_TYPE_ID] = 3 then 4 when postID.[POST_TYPE_ID] = 1 then 3 when postID.[POST_TYPE_ID] = 20 then 3 when postID.[POST_TYPE_ID] = 21 then 3 end)
	  ,(case when postID.[GR_ID] = 0 then null else ( SELECT (select id from PostGroup as pg where post.[GR_ID] = pg.GroupNo and pg.CenterID = (select id from Center where CenterCode = kafu.CEN_CODE) )   FROM [ORACLECRM]..[TT].[POST] as post , [ORACLECRM]..[TT].[KAFU] as kafu where post.KAFU_ID = kafu.[KAFU_ID] and post.[POST_ID] =  postID.[POST_ID]) end)
	  ,IIF (ISNUMERIC(postID.[POST_NUM]) <> 0 ,postID.[POST_NUM] ,REPLACE( REPLACE(REPLACE(postID.[POST_NUM],'A','1') , 'B' , '2'),'0َ','3'))
	  ,(case when postID.[POST_TYPE_ID] = 20 then 2 when postID.[POST_TYPE_ID] = 21 then 3 else 1 end)
	  ,(select min(BE.ETS_NUM) from  [ORACLECRM]..[TT].BASE_ETESALI as BE group by BE.POST_ID  having  BE.POST_ID = postID.[POST_ID])
	  ,(select max(BE.ETS_NUM) from  [ORACLECRM]..[TT].BASE_ETESALI as BE group by BE.POST_ID  having  BE.POST_ID = postID.[POST_ID])
      ,postID.[CAPACITY]
      ,iif(postID.[DISTANCE] is null , 0 , postID.[DISTANCE])
	  ,0
	  ,null
      ,postID.[POSTCODE]
	  ,convert(nvarchar(500) , postID.[ADDRESS])
	  ,1
	  ,postID.[POST_ID]
  FROM [ORACLECRM]..[TT].[POST] as postID join CRM.dbo.Cabinet as C on postID.[KAFU_ID] = C.ElkaID
GO
--ALTER DATABASE [CRM]
--	SET MULTI_USER
--GO


