use CRM
GO

--Delete CablePair
--DBCC CHECKIDENT ('CablePair', RESEED , 0 );

--Delete Cable
--DBCC CHECKIDENT ('Cable', RESEED , 0 );

--Delete CabinetInput
--DBCC CHECKIDENT ('CabinetInput', RESEED , 0 );

--Delete Cabinet
--DBCC CHECKIDENT ('Cabinet', RESEED , 0 );

declare @center int = 5;

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
		   ,[DateOfLastReview])
	select 
	       (select ID from Center as C where C.CenterCode =  k.[New_BEASAT_KAFO_KAFO_CODE])
	       ,k.KAFO
		   ,1
		   ,iif(CabinetType.ID is null , 1 ,CabinetType.ID)
		   ,case k.TYPE_KAFO  when 'ilomam_ofak' then 3
		                      when 'ILOMAM OFAK' then 3
		                      when 'isasethke-lbak' then 5
							  when 'ISASETHKE OFAK' then 5
							  when 'iron-ofak' then 4
							  when 'IRON OFAK' then 4
							  when 'LLW' then 1
							  when 'LLLW' then 1
							  when 'lsda' then 3
							  when 'LSDA' then 3 end
           ,1
		   ,k.MARKAZI
		   ,0
		   ,0
		   ,k.ADR
		   ,0
		   ,0
		   ,0
		   ,ZARFEAT 
		   ,[dbo].[sh2mi](STUFF(STUFF(k.DAT_ , 3 ,0,'/') , 6 , 0 ,'/'))
	from [OldData].[dbo].[New_BEASAT_KAFO] as k left join CabinetType on k.MARKAZI = CabinetType.CabinetTypeName
	Go


	DECLARE @Cabinet_id bigint
	DECLARE @Markazi_count bigint
	DECLARE @Center_ID bigint
	DECLARE @CabinetNumber bigint
    DECLARE @IdentityCable table ( ID bigint )
	DECLARE @IdentityCabinetInput table ( ID bigint )
	DECLARE Cabinet_Cursor CURSOR  READ_ONLY FOR SELECT ID , ToInputNo , CenterID , CabinetNumber from dbo.Cabinet where CenterID = @center AND CabinetNumber in  ( select kafo from [OldData].[dbo].[New_BEASAT_KAFO] Group by KAFO) 

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
--        from Cabinet where Cabinet.CabinetUsageType = 5 and CenterID = 5
GO





