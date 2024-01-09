USE [CRM]
GO

INSERT INTO [dbo].[PostGroup]
           (
		   [GroupNo]
		   ,[CenterID]
           
          
		   )
SELECT DISTINCT   post.[GR_ID] , convert(INT,(select id from Center where CenterCode = kafu.[CEN_CODE]))
  FROM [ORACLECRM]..[TT].[POST] as post , [ORACLECRM]..[TT].[KAFU] as kafu
  where post.KAFU_ID = kafu.[KAFU_ID] and post.gr_id <> 0
GO
GO


