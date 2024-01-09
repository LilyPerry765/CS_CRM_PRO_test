USE CRM
GO


UPDATE [Javanrood_kamzarfiat].[dbo].[Post] SET [TYPE_POST]=NULL
WHERE  [TYPE_POST]=''



      --ای دی شهر چک شود
DECLARE @cityid int = 9

DELETE  Post
from dbo.Post  
join dbo.Cabinet  on Cabinet.id=Post.CabinetID  
join center on center.id=cabinet.centerid
join region on center.regionid=region.id
join city on city.id=region.cityid
where city.id= @cityid and Center.CenterCode!=490


INSERT INTO [dbo].[Post]
           ([CabinetID]
           ,[PostTypeID]
           ,[PostGroupID]
           ,[Number]
           ,[AorBType]
           ,[FromPostContact]
           ,[ToPostContact]
           ,[Capacity]
           ,[Distance]
           ,[IsOutBorder]
           ,[OutBorderMeter]
           ,[PostalCode]
           ,[Address]
           ,[DocumentFileID]
           ,[Status]
		   ,IsDelete)
select  Cabinet.ID as CabinetID,
        IIF(Cabinet.CabinetUsageType = 5 , 6 , 3) as PostTypeID,
		IIF(Cabinet.CabinetUsageType = 5 , (select ID from PostGroup where Cabinet.CabinetNumber = PostGroup.GroupNo and Cabinet.CenterID = PostGroup.CenterID), null) as PostGroupID,
        P.POST as Number,
		IIF( P.TYPE_POST is null , 1 , IIF(P.TYPE_POST = '' ,  1 , IIF(P.TYPE_POST = 'A' ,  2 , 3))) as AorBType,
		1 as FromPostContact,
		IIF(P.TYPE_POST is null , 10 ,5) as ToPostContact,
		0 as Capacity,
		0 as Distance, 
        IIF(P.MARZ = 'F', 0 ,1)  as IsOutBorder, -- changed in excell
		IIF(P.MARZ = 'F', null , P.METRAJ) as OutBorderMeter,
		null as PostalCode,
		P.ADR_P as [Address],
		null as DocumentFileID,
		IIF( P.VAZEAT = N'داير' , 1 , 2),-- changed in excell
		0
from [Javanrood_kamzarfiat].[dbo].[Post] as P join Center on P.CODE = Center.CenterCode
             join Cabinet on P.KAFO = Cabinet.CabinetNumber and Cabinet.CenterID = Center.ID



	
	  DECLARE @PostID bigint
	  DECLARE @PostContact_count bigint

	DECLARE Post_Cursor CURSOR  READ_ONLY FOR SELECT P.ID , P.ToPostContact from dbo.Post as P 
																								join dbo.Cabinet  on Cabinet.id=p.CabinetID  
																								join center on center.id=cabinet.centerid
																								join region on center.regionid=region.id
																								join city on city.id=region.cityid
																								where city.id= @cityid  and Center.CenterCode!=490
	OPEN Post_Cursor;

    FETCH NEXT FROM Post_Cursor INTO @PostID , @PostContact_count;

    WHILE @@FETCH_STATUS = 0
      BEGIN
	    DECLARE @i int
		SET @i = 1
		while @i <= @PostContact_count
			begin
			INSERT INTO PostContact  VALUES (@PostID , @i ,3 ,null , null , 5 , null)
			set @i = @i + 1
			end
        FETCH NEXT FROM Post_Cursor INTO @PostID , @PostContact_count;
       END;

CLOSE Post_Cursor;
DEALLOCATE Post_Cursor;

--COMMIT TRAN -- Transaction Success!
--END TRY
--BEGIN CATCH
--    IF @@TRANCOUNT > 0
--        ROLLBACK TRAN --RollBack in case of Error
--END CATCH



