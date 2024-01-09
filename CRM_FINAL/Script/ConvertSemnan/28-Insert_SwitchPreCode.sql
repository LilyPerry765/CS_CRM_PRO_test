USE [CRM]
GO

DELETE SwitchPrecode
DBCC CHECKIDENT('SwitchPrecode',RESEED , 0)
INSERT INTO [dbo].[SwitchPrecode]
           ([CenterID]
           ,[SwitchID]
           ,[SwitchPreNo]
           ,[PreCodeType]
           ,[Capacity]
           ,[OperationalCapacity]
           ,[InstallCapacity]
           ,[SpecialServiceCapacity]
           ,[DeploymentType]
           ,[DorshoalNumberType]
           ,[Status]
           ,[FromNumber]
           ,[ToNumber])
SELECT 
       (SELECT CRMCenter.ID  FROM [CRM].[dbo].[Center] CRMCenter WHERE CRMCenter.CenterCode = C.Code)
	  , (SELECT ID FROM [CRM].[dbo].[Switch] sw 
	              WHERE sw.CenterID = (SELECT CRMCenter.ID  FROM [CRM].[dbo].[Center] CRMCenter WHERE CRMCenter.CenterCode = C.Code) 
				       AND sw.SwitchCode = S.[Prefix]
					   AND sw.SwitchTypeID = S.SwitchTypeID)
      ,S.Prefix
	  ,1
	  ,0
      ,NULL
	  ,NULL
	  ,0
	  ,0
	  ,NULL
	  ,0 
	  ,S.[Prefix] + S.FromRange
	  ,S.[Prefix] + S.ToRange
  FROM [abonman.semnan].[dbo].[Switch] S JOIN [abonman.semnan].[dbo].[Center] C ON S.CenterID = C.ID 
  WHERE (SELECT CRMCenter.ID  FROM [CRM].[dbo].[Center] CRMCenter WHERE CRMCenter.CenterCode = C.Code) IS NOT NULL 
GO


