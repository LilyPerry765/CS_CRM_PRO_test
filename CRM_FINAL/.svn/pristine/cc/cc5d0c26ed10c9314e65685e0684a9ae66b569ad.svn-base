use CRM
GO

DECLARE @cityid int = 9
DELETE dbo.Cabinet 
	 from dbo.Cabinet   
	join center on center.id=cabinet.centerid
	join region on center.regionid=region.id
	join city on city.id=region.cityid
	where city.id=@cityid and Center.CenterCode!=490


Delete cable
	 from dbo.cable   
	join center on center.id=cable.centerid
	join region on center.regionid=region.id
	join city on city.id=region.cityid
	where city.id=@cityid and Center.CenterCode!=490


--Delete CablePair
--DBCC CHECKIDENT ('CablePair', RESEED , 0 );

--Delete Cable
--DBCC CHECKIDENT ('Cable', RESEED , 0 );

--Delete CabinetInput
--DBCC CHECKIDENT ('CabinetInput', RESEED , 0 );

--Delete Cabinet
--DBCC CHECKIDENT ('Cabinet', RESEED , 0 );
--تاریخ کافو چک شود
INSERT INTO [dbo].[Cabinet]
           (
		    [CenterID]
           ,[CabinetNumber]
           ,[ABType]
           ,[CabinetTypeID]
           ,[CabinetUsageType]
           ,[FromInputNo]
           ,[ToInputNo]
		   ,[DistanceFromCenter]
		   ,[IsOutBound]
           ,[Address]
		   ,[FromPostalCode]
		   ,[ToPostalCode]
           ,[Status]
           ,[Capacity]
		   ,[DateOfLastReview]
		   ,IsLimitPost
		   ,ApplyQuota)
	select 
	       (select ID from Center as C where C.CenterCode =  k.CODE)
	       ,k.KAFO
		   ,1
		   ,iif(CabinetType.ID is null , 1 ,CabinetType.ID)
		   ,case k.TYPE_KAFO  when 'ilomam_ofak' then 3
		                      when 'ILOMAM OFAK' then 3
							  when 'KAFO MAMOLI' then 3
							  when 'kafo mamoli' then 3
		                      when 'isasethke-lbak' then 5
							  when 'ISASETHKE OFAK' then 5
							  when 'iron-ofak' then 4
							  when 'IRON OFAK' then 4
							  when 'LLW' then 1 end
           ,1
		   ,k.MARKAZI
		   ,0
		   ,0
		   ,k.ADR
		   ,0
		   ,0
		   ,0
		   ,ZARFEAT 
		   --,[dbo].[sh2mi](STUFF(STUFF(DAT_, 4 ,1,'') , 6 , 1 ,''))
		   ,[dbo].[sh2mi](STUFF(STUFF(k.DAT_ , 3 ,0,'/') , 6 , 0 ,'/'))
		   --,[dbo].[sh2mi](k.DATE)
		   ,0
		   ,1
	from [Javanrood_kamzarfiat].[dbo].[Kafo] as k left join CabinetType on k.MARKAZI = CabinetType.CabinetTypeName
	Go 
	

	--ای دی شهر چک شود
	DECLARE @cityid int = 9
	DECLARE @Cabinet_id bigint
	DECLARE @Markazi_count bigint
	DECLARE @Center_ID bigint
	DECLARE @CabinetNumber bigint
    DECLARE @IdentityCable table ( ID bigint )
	DECLARE @IdentityCabinetInput table ( ID bigint )
	DECLARE Cabinet_Cursor CURSOR  READ_ONLY FOR SELECT cabinet.ID , ToInputNo , CenterID , CabinetNumber from dbo.Cabinet   
																										join center on center.id=cabinet.centerid
																										join region on center.regionid=region.id
																										join city on city.id=region.cityid
																									     where city.id=@cityid and Center.CenterCode!=490

	OPEN Cabinet_Cursor;
    FETCH NEXT FROM Cabinet_Cursor INTO @Cabinet_id , @Markazi_count , @Center_ID , @CabinetNumber;
    WHILE @@FETCH_STATUS = 0
      BEGIN
	    INSERT INTO Cable output inserted.ID into @IdentityCable VALUES (@Center_ID , @CabinetNumber , 1 , 1 ,0,null ,1 , @Markazi_count ,0 ,'1900-01-01' , null , null)
	    DECLARE @i int
		SET @i = 1
		while @i <= @Markazi_count
			begin
			INSERT INTO CabinetInput output inserted.ID into @IdentityCabinetInput VALUES (@Cabinet_id , @i ,'1900-01-01' , 1 , null)
			INSERT INTO CablePair VALUES ((Select ID from @IdentityCable) , (Select ID from @IdentityCabinetInput) , @i , 2 ,'1900-01-01' , null)
			set @i = @i + 1
			Delete @IdentityCabinetInput;
			end
			Delete @IdentityCable;
      FETCH NEXT FROM Cabinet_Cursor INTO @Cabinet_id , @Markazi_count , @Center_ID , @CabinetNumber;
    END;

CLOSE Cabinet_Cursor;
DEALLOCATE Cabinet_Cursor;
GO

update C 
set CableTypeID = 6
from Cable C join Cabinet on C.CableNumber = Cabinet.CabinetNumber
where Cabinet.CabinetUsageType = 5
GO

update C 
set CableUsedChannelID = 8
from Cable C join Cabinet on C.CableNumber = Cabinet.CabinetNumber
where Cabinet.CabinetUsageType = 4
GO

--INSERT INTO [dbo].[PostGroup]
--           ([CenterID]
--           ,[GroupNo])
--        select  Cabinet.CenterID ,
--                Cabinet.CabinetNumber
--        from Cabinet where Cabinet.CabinetUsageType = 5
--GO





