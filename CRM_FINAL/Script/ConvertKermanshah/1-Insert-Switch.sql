USE [CRM]
GO


declare @cityid int ;set @cityid=7;

delete Telephone 
FROM         Switch INNER JOIN
                      Center ON Switch.CenterID = Center.ID INNER JOIN
                      Region ON Center.RegionID = Region.ID INNER JOIN
                      City ON Region.CityID = City.ID INNER JOIN
                      SwitchPrecode ON Switch.ID = SwitchPrecode.SwitchID AND Center.ID = SwitchPrecode.CenterID INNER JOIN
                      Telephone ON Center.ID = Telephone.CenterID AND SwitchPrecode.ID = Telephone.SwitchPrecodeID
WHERE     (City.ID = @cityid)

delete     SwitchPort
FROM         City INNER JOIN
                      Region ON City.ID = Region.CityID INNER JOIN
                      Center ON Region.ID = Center.RegionID INNER JOIN
                      Switch ON Center.ID = Switch.CenterID INNER JOIN
                      SwitchPort ON Switch.ID = SwitchPort.SwitchID
WHERE     (City.ID = @cityid)

delete SwitchPrecode 
FROM         SwitchPrecode INNER JOIN
                      Center ON SwitchPrecode.CenterID = Center.ID INNER JOIN
                      Region ON Center.RegionID = Region.ID INNER JOIN
                      City ON Region.CityID = City.ID 
WHERE     (City.ID = @cityid)

delete Switch
FROM         Switch INNER JOIN
                      Center ON Switch.CenterID = Center.ID INNER JOIN
                      Region ON Center.RegionID = Region.ID INNER JOIN
                      City ON Region.CityID = City.ID 

WHERE     (City.ID = @cityid)



--نامشخص
INSERT INTO [CRM].[dbo].[Switch]
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
      ,[Status]
	  )
 select   
   (select id  from [CRM].dbo.Center where CenterCode = s.ID_MARKAZ),
  5,
  NULL,
  1,
  1,
  0,
  NULL,
  NULL,
  NULL,
  8
  from [salas].[dbo].[PHRANGE] as s join [salas].[dbo].[TELTYPE] as t on t.id =s.ID_TELTYPE
   where t.ID not in (3,6,7,8,11,12)
  group by ID_MARKAZ

  ---wll
  INSERT INTO [CRM].[dbo].[Switch]
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
      ,[Status]
	  )
 select   
   (select id  from [CRM].dbo.Center where CenterCode = s.ID_MARKAZ),
  6,
  NULL,
  1,
  2,
  0,
  NULL,
  NULL,
  NULL,
  8
  from [salas].[dbo].[PHRANGE] as s join [salas].[dbo].[TELTYPE] as t on t.id =s.ID_TELTYPE
  where t.ID  in (3)
  group by ID_MARKAZ
  

    ---کد خدماتی
  INSERT INTO [CRM].[dbo].[Switch]
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
      ,[Status]
	  )
 select   
   (select id  from [CRM].dbo.Center where CenterCode = s.ID_MARKAZ),
  8,
  NULL,
  1,
  3,
  0,
  NULL,
  NULL,
  NULL,
  8
  from [salas].[dbo].[PHRANGE] as s join [salas].[dbo].[TELTYPE] as t on t.id =s.ID_TELTYPE
  where t.ID  in (7)
  group by ID_MARKAZ
  

      ---سیم خصوصی
  INSERT INTO [CRM].[dbo].[Switch]
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
      ,[Status]
	  )
 select   
   (select id  from [CRM].dbo.Center where CenterCode = s.ID_MARKAZ),
  7,
  NULL,
  1,
  4,
  0,
  NULL,
  NULL,
  NULL,
  8
  from [salas].[dbo].[PHRANGE] as s join [salas].[dbo].[TELTYPE] as t on t.id =s.ID_TELTYPE
  where t.ID  in (11)
  group by ID_MARKAZ

      ---نوری
  INSERT INTO [CRM].[dbo].[Switch]
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
      ,[Status]
	  )
 select   
   (select id  from [CRM].dbo.Center where CenterCode = s.ID_MARKAZ),
  3,
  NULL,
  1,
  10,--با کد کافو چک شود
  0,
  NULL,
  NULL,
  NULL,
  8
  from [salas].[dbo].[PHRANGE] as s join [salas].[dbo].[TELTYPE] as t on t.id =s.ID_TELTYPE
  where t.ID  in (8) --and s.CH1   in (11)
  group by ID_MARKAZ

  
