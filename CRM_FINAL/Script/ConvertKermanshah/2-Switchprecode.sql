USE [CRM]
GO
--TFIRST , TEND اصلاح شوند
------------نامشخص

declare @city int  =7;

delete SwitchPrecode 
FROM         Switch INNER JOIN
                      Center ON Switch.CenterID = Center.ID INNER JOIN
                      Region ON Center.RegionID = Region.ID INNER JOIN
                      City ON Region.CityID = City.ID INNER JOIN
                      SwitchPrecode ON Switch.ID = SwitchPrecode.SwitchID AND Center.ID = SwitchPrecode.CenterID
WHERE     (City.ID = @city)

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
           ,[LinkType]
           ,[DorshoalNumberType]
           ,[Status]
           ,[FromNumber]
           ,[ToNumber])
select
(select id  from Center where CenterCode = s.ID_MARKAZ),
(select top(1) ID from Switch where Switch.CenterID = (select id  from Center where CenterCode = s.ID_MARKAZ) and Switch.SwitchTypeID=5)  ,
SUBSTRING(TEL_PISH , 3 ,5),
case ID_TELTYPE when 1 then 1 when 2 then 2 else 1 end 	,
0,
null,
null,
0,
0,
null,
null,
0,
SUBSTRING(TEL_PISH , 3 ,5) + Cast(s.TFIRST as nvarchar(max)),
SUBSTRING(TEL_PISH , 3 ,5) + Cast(s.TEND as nvarchar(max))
  from [salas].[dbo].[PHRANGE] as s join [salas].[dbo].[TELTYPE] as t on t.id =s.ID_TELTYPE
   where t.ID not in (3,6,7,8,11,12)

 ---------------wll


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
           ,[LinkType]
           ,[DorshoalNumberType]
           ,[Status]
           ,[FromNumber]
           ,[ToNumber])
select
(select id  from Center where CenterCode = s.ID_MARKAZ),
(select top(1) ID from Switch where Switch.CenterID = (select id  from Center where CenterCode = s.ID_MARKAZ) and Switch.SwitchTypeID=6)  ,
SUBSTRING(TEL_PISH , 3 ,5),
case ID_TELTYPE when 1 then 1 when 2 then 2 else 1 end 	,
0,
null,
null,
0,
0,
null,
null,
0,
SUBSTRING(TEL_PISH , 3 ,5) + Cast(TFIRST as nvarchar(max)),
SUBSTRING(TEL_PISH , 3 ,5) + Cast(s.TEND as nvarchar(max))
  from [salas].[dbo].[PHRANGE] as s join [salas].[dbo].[TELTYPE] as t on t.id =s.ID_TELTYPE
   where t.ID  in (3)

-------------------------  کد خدماتی



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
           ,[LinkType]
           ,[DorshoalNumberType]
           ,[Status]
           ,[FromNumber]
           ,[ToNumber])
select
(select id  from Center where CenterCode = s.ID_MARKAZ),
(select top(1) ID from Switch where Switch.CenterID = (select id  from Center where CenterCode = s.ID_MARKAZ) and Switch.SwitchTypeID=8)  ,
SUBSTRING(TEL_PISH , 3 ,5),
case ID_TELTYPE when 1 then 1 when 2 then 2 else 1 end 	,
0,
null,
null,
0,
0,
null,
null,
0,
SUBSTRING(TEL_PISH , 3 ,5) + Cast(TFIRST as nvarchar(max)),
SUBSTRING(TEL_PISH , 3 ,5) + Cast(s.TEND as nvarchar(max))
  from [salas].[dbo].[PHRANGE] as s join [salas].[dbo].[TELTYPE] as t on t.id =s.ID_TELTYPE
   where t.ID  in (7)


------------------------سیم خصوصس

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
           ,[LinkType]
           ,[DorshoalNumberType]
           ,[Status]
           ,[FromNumber]
           ,[ToNumber])
select
(select id  from Center where CenterCode = s.ID_MARKAZ),
(select top(1) ID from Switch where Switch.CenterID = (select id  from Center where CenterCode = s.ID_MARKAZ) and Switch.SwitchTypeID=7 ) ,
SUBSTRING(TEL_PISH , 3 ,5),
case ID_TELTYPE when 1 then 1 when 2 then 2 else 1 end 	,
0,
null,
null,
0,
0,
null,
null,
0,
SUBSTRING(TEL_PISH , 3 ,5) + Cast(TFIRST as nvarchar(max)),
SUBSTRING(TEL_PISH , 3 ,5) + Cast(s.TEND as nvarchar(max))
  from [salas].[dbo].[PHRANGE] as s join [salas].[dbo].[TELTYPE] as t on t.id =s.ID_TELTYPE
   where t.ID  in (11)



------------------------نوری

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
           ,[LinkType]
           ,[DorshoalNumberType]
           ,[Status]
           ,[FromNumber]
           ,[ToNumber])
select
(select id  from Center where CenterCode = s.ID_MARKAZ),
(select top(1) ID from Switch where  Switch.CenterID = (select id  from Center where CenterCode = s.ID_MARKAZ) and Switch.SwitchTypeID=3 ),--and Switch.SwitchCode=102) ,--با کد سوئیچ چک شود
SUBSTRING(TEL_PISH , 3 ,5),
case ID_TELTYPE when 1 then 1 when 2 then 2 else 1 end 	,
0,
null,
null,
0,
0,
null,
null,
0,
SUBSTRING(TEL_PISH , 3 ,5) + Cast(TFIRST as nvarchar(max)),
SUBSTRING(TEL_PISH , 3 ,5) + Cast(s.TEND as nvarchar(max))
  from [salas].[dbo].[PHRANGE] as s join [salas].[dbo].[TELTYPE] as t on t.id =s.ID_TELTYPE
   where t.ID  in (8)-- and s.CH1 not in (11)




-------------------------


  SELECT *
  FROM [salas].[dbo].[PHRANGE]
  where substring (TEL_PISH+TFIRST,3,8) not in
  (

  SELECT     SwitchPrecode.FromNumber
FROM         City INNER JOIN
                      Region ON City.ID = Region.CityID INNER JOIN
                      Center ON Region.ID = Center.RegionID INNER JOIN
                      Switch ON Center.ID = Switch.CenterID INNER JOIN
                      SwitchPrecode ON Center.ID = SwitchPrecode.CenterID AND Switch.ID = SwitchPrecode.SwitchID
WHERE     (City.ID = @city)
)
