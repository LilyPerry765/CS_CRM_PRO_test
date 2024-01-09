USE [CRM]
GO

INSERT INTO [dbo].[Switch]
           (
		    [CenterID]
           ,[SwitchTypeID]
           ,[FeatureONU]
           ,[WorkUnitResponsible]
           ,[SwitchCode]
           ,[Capacity]
           ,[OperationalCapacity]
           ,[InstallCapacity]
           ,[DataCapacity]
           ,[Status])

SELECT 
       (SELECT CRMCenter.ID  FROM [CRM].[dbo].[Center] CRMCenter WHERE CRMCenter.CenterCode = C.Code)
      ,S.[SwitchTypeID]
	  ,NULL
	  ,1
      ,S.[Prefix]
	  ,0
	  ,NULL
	  ,NULL
	  ,NULL
	  ,0 
  FROM [abonman.semnan].[dbo].[Switch] S JOIN [abonman.semnan].[dbo].[Center] C ON S.CenterID = C.ID 
  GROUP By S.[CenterID],S.[SwitchTypeID],S.[Prefix],C.Code
  HAVING (SELECT CRMCenter.ID  FROM [CRM].[dbo].[Center] CRMCenter WHERE CRMCenter.CenterCode = C.Code) IS NOT NULL 

GO


