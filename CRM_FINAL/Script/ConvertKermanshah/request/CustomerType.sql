select t.telephoneno,t.CustomerTypeID,ir.TelephoneType
--update [InstallRequest] set TelephoneType=t.CustomerTypeid
 from crm.dbo.CustomerType as c
join [InstallRequest]  as ir on ir.TelephoneType=c.ID
join request as r on r.id = ir.requestid
join telephone as t on t.telephoneno=r.telephoneno
JOIN Center ON t.CenterID = Center.ID  
JOIN Region ON Center.RegionID = Region.ID  
JOIN City ON Region.CityID = City.ID  

where  c.IsShow=0 
and t.status not in (5,0) 
--and city.id=10 
order by t.telephoneno
----------------------------------------------------------
--تلفن هایی که نوع مشترک قدیمی داردند
DECLARE @regionid int =7
select cen.centername, t.telephoneno,cu.FirstNameOrTitle,cu.LastName,adds.PostalCode,adds.AddressContent,c.title
 from crm.dbo.CustomerType as c
join Telephone  as t on t.CustomerTypeID=c.ID
join center as cen on cen.id=t.centerid
join region on region.id=cen.regionid
full join customer as cu on cu.id=t.customerid
full join address as adds on adds.id=t.InstallAddressID
where  c.IsShow=0 
and region.id=@regionid 
order by cen.id
-------------------------------
--تلفن های دایر که نوع مشترک ندارند
DECLARE @regionid int =7
  select cen.centername, t.telephoneno,cu.FirstNameOrTitle,cu.LastName,adds.PostalCode,adds.AddressContent,
  case t.status 
when 2 then N'دایر' 
else N'قطع'
end
FROM Telephone  as t 
join center as cen on cen.id=t.centerid
full join customer as cu on cu.id=t.customerid
full join address as adds on adds.id=t.InstallAddressID
join region on region.id=cen.regionid
where t.status  in (2,3)  and CustomerTypeID is null 
 and t.telephoneno not in
  (
  select  t.telephoneno--تلفن های در روال
FROM Telephone  as t 
join center as cen on cen.id=t.centerid
join request as r on r.telephoneno=t.telephoneno
full join customer as cu on cu.id=t.customerid
full join address as adds on adds.id=t.InstallAddressID
  where t.status  in (2,3)  and CustomerTypeID is null 
  and r.enddate is  null
  )
    and region.id=@regionid
    order by t.centerid 