USE [ElkaData]
GO

 --insert into CRM.dbo.Role
 --update [SCOTT_OFFIC-final] set GR_TITLE = N'رییس مرکز' where GR_TITLE = N'رئيس مرکز'
  --SELECT GR_TITLE , null , 1
  --FROM [SCOTT_OFFIC-final]
  --group by GR_TITLE Having  GR_TITLE not in ( select Name COLLATE Arabic_CI_AI from CRM.dbo.Role)

  --select Name COLLATE Arabic_CI_AI from CRM.dbo.Role

 -- 

 --DECLARE @folderParentRole uniqueidentifier = ( select ID from [CRM.Folder].dbo.Role as R where R.Name = N'CRM' and r.ParentID = '00000000-0000-0000-0000-000000000000')
 --select @folderParentRole

--insert into [CRM.Folder].dbo.Role 
  --SELECT NEWID() , LEVEL_NAME , @folderParentRole , 1
  --FROM [SCOTT_OFFIC-final]
  --group by LEVEL_NAME
  
  --insert into CRM.dbo.[User]
  --        select 
		--         UO_USERNAME --UserName
		--        , UO_NAME --FirstName
		--		, UO_LASTNAME --LastName
		--		, null --Email
		--		, null --RoleID
		--		, r.ID -- RoleID
		--		,null --Picture
		--		,null --Config
		--		, 0 --TimeStamp
		--		, null --LastLoginDate
  -- from [dbo].[SCOTT_OFFIC-final]  as e join CRM.dbo.Role as r on e.GR_TITLE = r.Name COLLATE Arabic_CI_AI

  --select UserName from crm.dbo.[User] group by UserName having count(*) > 1
  --insert into [CRM.Folder].dbo.[User] 
  --select 
  --  NEWID()
  -- , UO_USERNAME --Username
  --, UO_PASSWORD --Password
  --, UO_NAME + ' ' +UO_LASTNAME --Fullname
  --, 1 --IsEnable
  --, null --Comment
  --, null --Email
  --, null --LastLoginDate
  --,'4-28-2015' --CreationDate
  --, null --SqlConnectionProfile
  --, null --Preferences
  --from [dbo].[SCOTT_OFFIC-final] as e join CRM.dbo.Role as r on e.GR_TITLE = r.Name COLLATE Arabic_CI_AI

  --select UserName from [CRM.Folder].dbo.[User]  group by UserName having count(*) > 1

  --select u.ID , fR.ID from [CRM.Folder].dbo.[User] as u 
  --join [CRM].dbo.[user] as cu on cu.username = u.username COLLATE Arabic_CI_AI 
  --join [CRM].dbo.[Role] as cr on cr.ID = cu.RoleID
  --join  [CRM.Folder].dbo.[Role] as fR on fR.Name = cr.Name COLLATE Arabic_CI_AI  and fR.ParentID = '50EA71D3-2AC6-4DEA-BDA1-81F4EE404FC2'
  --where u.CreationDate = '4-27-2015'

  ------------------------------------------------
--insert into [CRM.Folder].dbo.[Role] 
-- select   NEWID() , cr.name ,'50EA71D3-2AC6-4DEA-BDA1-81F4EE404FC2' , null ,1
-- select * from  [CRM].dbo.[Role] as cr
-- left join  [CRM.Folder].dbo.[Role] as fR on fR.Name = cr.Name COLLATE Arabic_CI_AI  
-- where fR.ID is null
 -----------------------------------------------

 --delete [CRM.Folder].dbo.[UserRole] where UserID in ( select u.ID from [CRM.Folder].dbo.[User] as u 
 -- join [CRM].dbo.[user] as cu on cu.username = u.username COLLATE Arabic_CI_AI 
 -- join [CRM].dbo.[Role] as cr on cr.ID = cu.RoleID
 -- join  [CRM.Folder].dbo.[Role] as fR on fR.Name = cr.Name COLLATE Arabic_CI_AI  
 -- and fR.ParentID = '50EA71D3-2AC6-4DEA-BDA1-81F4EE404FC2'
 -- where u.CreationDate = '4-28-2015' and fR.Type = 1)

  --  insert into [CRM.Folder].dbo.[UserRole] select DISTINCT u.ID , fR.ID , 1
  --  from [CRM.Folder].dbo.[User] as u 
  --join [CRM].dbo.[user] as cu on cu.username = u.username COLLATE Arabic_CI_AI 
  --join [CRM].dbo.[Role] as cr on cr.ID = cu.RoleID
  --join  [CRM.Folder].dbo.[Role] as fR on fR.Name = cr.Name COLLATE Arabic_CI_AI  
  --and fR.ParentID = '50EA71D3-2AC6-4DEA-BDA1-81F4EE404FC2'
  --where u.CreationDate = '4-28-2015' and fR.Type = 1

  --DELETE FROM CRM.dbo.UserCenter WHERE UserID in
  --(select u.ID  FROM [dbo].[SCOTT_OFFIC-final] as e 
  --join CRM.dbo.[user] as u on u.UserName  = e.UO_USERNAME COLLATE Arabic_CI_AI 
  --)

 --insert into CRM.dbo.UserCenter  select DISTINCT u.ID , c.ID  FROM [dbo].[SCOTT_OFFIC-final] as e 
 -- join CRM.dbo.[user] as u on u.UserName  = e.UO_USERNAME COLLATE Arabic_CI_AI 
 -- join [SCOTT_OFFICE_CENTER1] as oc on oc.OF_CODE = e.of_code
 -- join CRM.dbo.CENTER as c on c.CenterCode = oc.CEN_CODE

 -- select * from CRM.dbo.[center]
 
  select u.ID from [CRM.Folder].dbo.[User] as u 
  join [CRM].dbo.[user] as cu on cu.username = u.username COLLATE Arabic_CI_AI 
  join [CRM].dbo.[Role] as cr on cr.ID = cu.RoleID
  join  [CRM.Folder].dbo.[Role] as fR on fR.Name = cr.Name COLLATE Arabic_CI_AI  
  join [CRM.Folder].dbo.[UserRole] as ur on ur.UserID = u.id
  and fR.ParentID = '50EA71D3-2AC6-4DEA-BDA1-81F4EE404FC2'
  where u.CreationDate = '4-27-2015' and fR.Type = 1
  select * from  [CRM.Folder].dbo.[Role] where ID = '50EA71D3-2AC6-4DEA-BDA1-81F4EE404FC2'



  



