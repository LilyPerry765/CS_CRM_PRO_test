USE [OldCustomerDate]
GO

--update T2 set T2.CustomerTypeID = CT.ID , T2.CustomerGroupID = CG.ID
--FROM [dbo].[customerTypeeditsheet1_20150408] as T1 
--join [CRM].[dbo].[Telephone] as T2 on T1.TelephoneNo = T2.TelephoneNo
--join [CRM].[dbo].[CustomerType] as CT on T1.Column1 = CT.Code
--join [CRM].[dbo].[CustomerGroup] as CG on T1.Column2 = CG.KerID and CT.ID = CG.CustomerTypeID


update IR set IR.TelephoneType = CT.ID , IR.TelephoneTypeGroup = CG.ID
FROM [dbo].[customerTypeeditsheet2_20150408] as T1 
join [CRM].[dbo].[InstallRequest] as IR on T1.ID = IR.RequestID
join [CRM].[dbo].[CustomerType] as CT on T1.Column1 = CT.Code
join [CRM].[dbo].[CustomerGroup] as CG on T1.Column2 = CG.KerID 
and CT.ID = CG.CustomerTypeID