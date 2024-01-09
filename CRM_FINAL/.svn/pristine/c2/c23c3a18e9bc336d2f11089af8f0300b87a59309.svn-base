use crm
go

Declare @cityid int;set @cityid=2
Declare @centercode int;set @centercode=2508


-- select AP.* from Post join Cabinet on Post.CabinetID = Cabinet.ID 
-- join [OldData].[dbo].[ASHRAFi_POST] as AP  on AP.Code = Cabinet.CenterID and AP.KAFO = Cabinet.CabinetNumber
-- where P1 != 0 and AP.POST = 69

--select * from [OldData].[dbo].[ASHRAFi_POST] as AP  
--join Cabinet on Cabinet.CenterID = AP.Code and Cabinet.CabinetNumber = AP.KAFO 
--join Post  on Post.CabinetID = Cabinet.ID and Post.Number = AP.Post


--select * from [OldData].[dbo].[ASHRAFi_POST] as AP  
--join Cabinet on Cabinet.CenterID = AP.Code and Cabinet.CabinetNumber = AP.KAFO 
--join Post  on Post.CabinetID = Cabinet.ID and Post.Number = AP.Post

insert into [dbo].[AdjacentPost] select Post.ID ,Post2.ID  from Post join Cabinet on  Post.CabinetID = Cabinet.ID 
join [eslam_kamzarfiat].[dbo].[Post] as AP  on AP.Post = Post.Number and Cabinet.CabinetNumber = AP.KAFO
join Post as Post2 on Post2.Number = AP.p1 and Post2.CabinetID = Cabinet.ID
join [CRM].dbo.Center ON  Cabinet.CenterID= Center.ID INNER JOIN
[CRM].dbo.Region ON Center.RegionID = Region.ID INNER JOIN
[CRM].dbo.City ON Region.CityID = City.ID	 
where  AP.p1!= 0 and city.id=@cityid and center.centercode=@centercode

insert into [dbo].[AdjacentPost] select Post.ID ,Post2.ID  from Post join Cabinet on  Post.CabinetID = Cabinet.ID 
join [eslam_kamzarfiat].[dbo].[Post] as AP  on AP.Post = Post.Number and Cabinet.CabinetNumber = AP.KAFO
join Post as Post2 on Post2.Number = AP.p2 and Post2.CabinetID = Cabinet.ID
join [CRM].dbo.Center ON  Cabinet.CenterID= Center.ID INNER JOIN
[CRM].dbo.Region ON Center.RegionID = Region.ID INNER JOIN
[CRM].dbo.City ON Region.CityID = City.ID	 
where  AP.p2!= 0 and city.id=@cityid and center.centercode=@centercode

insert into [dbo].[AdjacentPost] select Post.ID ,Post2.ID  from Post join Cabinet on  Post.CabinetID = Cabinet.ID 
join [eslam_kamzarfiat].[dbo].[Post] as AP  on AP.Post = Post.Number and Cabinet.CabinetNumber = AP.KAFO
join Post as Post2 on Post2.Number = AP.p3 and Post2.CabinetID = Cabinet.ID
join [CRM].dbo.Center ON  Cabinet.CenterID= Center.ID INNER JOIN
[CRM].dbo.Region ON Center.RegionID = Region.ID INNER JOIN
[CRM].dbo.City ON Region.CityID = City.ID	 
where  AP.p3!= 0 and city.id=@cityid and center.centercode=@centercode

insert into [dbo].[AdjacentPost] select Post.ID ,Post2.ID  from Post join Cabinet on  Post.CabinetID = Cabinet.ID 
join [eslam_kamzarfiat].[dbo].[Post] as AP  on AP.Post = Post.Number and Cabinet.CabinetNumber = AP.KAFO
join Post as Post2 on Post2.Number = AP.p4 and Post2.CabinetID = Cabinet.ID
join [CRM].dbo.Center ON  Cabinet.CenterID= Center.ID INNER JOIN
[CRM].dbo.Region ON Center.RegionID = Region.ID INNER JOIN
[CRM].dbo.City ON Region.CityID = City.ID	 
where  AP.p4!= 0 and city.id=@cityid and center.centercode=@centercode