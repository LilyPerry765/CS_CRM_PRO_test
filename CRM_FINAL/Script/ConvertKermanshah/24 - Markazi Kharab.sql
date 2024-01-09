use crm
go
  --------مرکزی خراب
  update ci set ci.Status=0    
  from Cabinet as c
  join Center as cen on cen.id=c.CenterID
  join CabinetInput as ci on ci.CabinetID=c.ID
  join [Ravansar].[dbo].[KHARAB] as d on d.KAFO=c.CabinetNumber
  join MDF on mdf.CenterID=cen.ID
  join MDFFrame as md on md.MDFID=mdf.ID
  join VerticalMDFColumn as col on col.MDFFrameID=md.ID
  join VerticalMDFRow as row on row.VerticalMDFColumnID=col.ID
  join Bucht as b on b.CabinetInputID=ci.ID and b.MDFRowID=row.ID
  where c.CabinetNumber=d.KAFO  and ci.InputNumber=d.MARKAZI 
  and b.BuchtNo=d.ETE and row.VerticalRowNo=d.TAB and col.VerticalCloumnNo=d.RAD
  and d.CODE=cen.CenterCode 
  and b.status=0
  -----------بوخت خراب
  update b set  b.status=2 from Cabinet as c
  join Center as cen on cen.id=c.CenterID
  join CabinetInput as ci on ci.CabinetID=c.ID
  join [Ravansar].[dbo].[KHARAB] as d on d.KAFO=c.CabinetNumber
  join MDF on mdf.CenterID=cen.ID
  join MDFFrame as md on md.MDFID=mdf.ID
  join VerticalMDFColumn as col on col.MDFFrameID=md.ID
  join VerticalMDFRow as row on row.VerticalMDFColumnID=col.ID
  join Bucht as b on b.CabinetInputID=ci.ID and b.MDFRowID=row.ID
  where c.CabinetNumber=d.KAFO  and ci.InputNumber=d.MARKAZI 
  and b.BuchtNo=d.ETE and row.VerticalRowNo=d.TAB and col.VerticalCloumnNo=d.RAD
  and d.CODE=cen.CenterCode 
  and b.status=0
  --------
  insert into  [dbo].[Malfuction]
  (
       [MalfuctionType]
      ,[CabinetInputID]
      ,[PostContactID]
      ,[PCMID]
      ,[PCMPortID]
      ,[MalfuctionOrhealthy]
      ,[DateMalfunction]
      ,[TimeMalfunction]
      ,[TypeMalfunction]
      ,[LicenseNumber]
      ,[LicenseFile]
      ,[DistanceFromMDF]
      ,[DistanceFromCabinet]
      ,[Description]
	  )

    select 
	2,
	ci.ID,
	null,
	null,
	null,
	0,
	getdate(),
	null,
	4,
	null,
	null,
	null,
	null,
	d.NAME
  from Cabinet as c
  join Center as cen on cen.id=c.CenterID
  join CabinetInput as ci on ci.CabinetID=c.ID
  join [Ravansar].[dbo].[KHARAB] as d on d.KAFO=c.CabinetNumber
  join MDF on mdf.CenterID=cen.ID
  join MDFFrame as md on md.MDFID=mdf.ID
  join VerticalMDFColumn as col on col.MDFFrameID=md.ID
  join VerticalMDFRow as row on row.VerticalMDFColumnID=col.ID
  join Bucht as b on b.CabinetInputID=ci.ID and b.MDFRowID=row.ID
  where c.CabinetNumber=d.KAFO  and ci.InputNumber=d.MARKAZI 
  and b.BuchtNo=d.ETE and row.VerticalRowNo=d.TAB and col.VerticalCloumnNo=d.RAD
  and d.CODE=cen.CenterCode 
  and b.status=2
  ---------------
  --for Ravansar

--  use crm
--go
--  --------مرکزی خراب
--  update ci set ci.Status=0   
-- from Cabinet as c
--  join Center as cen on cen.id=c.CenterID
--  join CabinetInput as ci on ci.CabinetID=c.ID
--  join [Ravansar].[dbo].[KHARAB] as d on d.KAFO=c.CabinetNumber
--  join MDF on mdf.CenterID=cen.ID
--  join MDFFrame as md on md.MDFID=mdf.ID
--  join VerticalMDFColumn as col on col.MDFFrameID=md.ID
--  join VerticalMDFRow as row on row.VerticalMDFColumnID=col.ID
--  join Bucht as b on b.CabinetInputID=ci.ID and b.MDFRowID=row.ID
--where ci.InputNumber=d.MARKAZI 
-- and d.CODE=cen.CenterCode 
----  -----------

--  insert into  [dbo].[Malfuction]
--  (
--       [MalfuctionType]
--      ,[CabinetInputID]
--      ,[PostContactID]
--      ,[PCMID]
--      ,[PCMPortID]
--      ,[MalfuctionOrhealthy]
--      ,[DateMalfunction]
--      ,[TimeMalfunction]
--      ,[TypeMalfunction]
--      ,[LicenseNumber]
--      ,[LicenseFile]
--      ,[DistanceFromMDF]
--      ,[DistanceFromCabinet]
--      ,[Description]
--	  )

--    select 
--	2,
--	ci.ID,
--	null,
--	null,
--	null,
--	0,
--	getdate(),
--	null,
--	4,
--	null,
--	null,
--	null,
--	null,
--	d.NAME
-- from Cabinet as c
--  join Center as cen on cen.id=c.CenterID
--  join CabinetInput as ci on ci.CabinetID=c.ID
--  join [Ravansar].[dbo].[KHARAB] as d on d.KAFO=c.CabinetNumber
--  join MDF on mdf.CenterID=cen.ID
--  join MDFFrame as md on md.MDFID=mdf.ID
--  join VerticalMDFColumn as col on col.MDFFrameID=md.ID
--  join VerticalMDFRow as row on row.VerticalMDFColumnID=col.ID
--  join Bucht as b on b.CabinetInputID=ci.ID and b.MDFRowID=row.ID
--where ci.InputNumber=d.MARKAZI 
-- and d.CODE=cen.CenterCode 


--------------

