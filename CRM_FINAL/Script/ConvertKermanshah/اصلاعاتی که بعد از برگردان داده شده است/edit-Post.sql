USE CRM
GO
--BEGIN TRY
--    BEGIN TRANSACTION

declare @CenterID int = 5;

--WITH CTEPost (CenterCode,KAFO,POST,TYPE_POST,MARZ,METRAJ,ADR_P,VAZEAT)as
--(
--select * FROM OPENROWSET('Microsoft.ACE.OLEDB.12.0','Excel 12.0; Database=D:\Kermansh\post9.xls;HDR=YES','SELECT * FROM [d2w]')
--)

--Delete PostContact
--DBCC CHECKIDENT ('PostContact', RESEED , 0 );

--Delete Post
--DBCC CHECKIDENT ('Post', RESEED , 0 );




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
           ,[Status])
select  Cabinet.ID as CabinetID,
        IIF(Cabinet.CabinetUsageType = 5 , 6 , 3) as PostTypeID,
		IIF(Cabinet.CabinetUsageType = 5 , (select ID from PostGroup where Cabinet.CabinetNumber = PostGroup.GroupNo and Cabinet.CenterID = PostGroup.CenterID), null) as PostGroupID,
        P.POST as Number,
		case P.TYPE_POST  when '' then 1
						  when NULL then 1
						  when 'A' then 2
						  when 'B' then 3
						  when 'C' then 4 END as AorBType,             
		1 as FromPostContact,
		IIF((P.TYPE_POST is null or P.TYPE_POST = '') , 10 ,5) as ToPostContact,
		0 as Capacity,
		0 as Distance, 
        IIF(P.MARZ = 'F', 0 ,1)  as IsOutBorder, -- changed in excell
		IIF(P.MARZ = 'F', null , P.METRAJ) as OutBorderMeter,
		null as PostalCode,
		P.ADR_P as [Address],
		null as DocumentFileID,
		IIF( P.VAZEAT = N'داير' , 1 , 2)-- changed in excell
from [OldData].[dbo].New_BEASAT_POST as P join Center on P.New_BEASAT_POST_POST_CODE = Center.CenterCode
             join Cabinet on P.KAFO = Cabinet.CabinetNumber and Cabinet.CenterID = Center.ID


	  DECLARE @PostID bigint
	  DECLARE @PostContact_count bigint

	DECLARE Post_Cursor CURSOR  READ_ONLY FOR SELECT P.ID , P.ToPostContact from dbo.Post as P  join Cabinet on P.CabinetID = Cabinet.id and Cabinet.CenterID = @CenterID and Cabinet.CabinetNumber in (select kafo from [OldData].[dbo].New_BEASAT_KAFO group by Kafo)
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
