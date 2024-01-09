use crm
go

----Truncate Table Test..NewOne
--;WITH CTEBokht as
--(
--select * FROM OPENROWSET('Microsoft.ACE.OLEDB.12.0','Excel 12.0;Database=D:\Kermansh\bokht9.xls;HDR=YES','SELECT * FROM [d2w]')
--)
--select *
--into Test..NewOne
--from CTEBokht 
--drop table #Results


--delete Bucht 
--DBCC checkident('Bucht' , reseed , 0)

--delete MDF 
--DBCC checkident('MDF' , reseed , 0)

--delete MDFFrame 
--DBCC checkident('MDFFrame' , reseed , 0)

--delete  [dbo].[VerticalMDFRow]
--DBCC checkident('[dbo].[VerticalMDFRow]' , reseed , 0)


--delete [dbo].[VerticalMDFColumn]
--DBCC checkident('[dbo].[VerticalMDFColumn]' , reseed , 0)


----فاصله اول نوع کافو حذف شود
update [salas].[dbo].[BOKHT] set TYPE=ltrim (type)

--برای کافو نوری شماره کافو در شرط چک شود
DECLARE @MDFTable table( MDFID int);

IF OBJECT_ID('tempdb..#Error') IS NOT NULL
DROP TABLE #Error

IF OBJECT_ID('tempdb..#Results') IS NOT NULL
DROP TABLE #Results

CREATE TABLE #Error (Kafo int ,Radif int , tabbagh int , etesali int , error nvarchar(max))

CREATE TABLE #Results (number int ,cc int , kc int , ra int , ta int , ase int ,tae int)
 DECLARE @sql nvarchar(Max);
 set @sql = 'SELECT 1 , [CODE] ,[KAFO] ,[RA1] ,[TA1] ,[AS_ET1] ,[TA_ET1] FROM [salas].[dbo].[BOKHT] where TYPE = ''mamooli'''
 DECLARE @i int
		SET @i = 2
		while @i <= 16
			begin
              set @sql = @sql + ' Union ALL SELECT ' + cast(@i as nvarchar(2)) + ',[CODE] ,[KAFO] ,[RA'+ cast(@i as nvarchar(2))+'] ,[TA'+ cast(@i as nvarchar(2))+'] ,[AS_ET'+ cast(@i as nvarchar(2))+'] ,[TA_ET'+ cast(@i as nvarchar(2))+'] FROM [salas].[dbo].[BOKHT] where TYPE = ''mamooli''' 
			  set @i = @i + 1
			end

insert into #Results EXEC ( @sql)
delete from #Results where ra = 0 and	ta	= 0 and ase	= 0 and tae = 0

delete @MDFTable

INSERT INTO [dbo].[MDF]
           ([CenterID]
           ,[Type]
           ,[Number]
           ,[LastNoVerticalFrames]
           ,[LastNoHorizontalFrames]
           ,[Description])
		   OUTPUT INSERTED.ID
           INTO @MDFTable
SELECT  
        (select id  from Center where CenterCode = R.cc)
		,0
		--,ROW_NUMBER() OVER (PARTITION BY R.cc ORDER BY R.cc) AS number
		,1
		,null
		,null
		,N'اصلی مرکز'
		--,N'اختصاصی'
		--,N'نوری'
		--,N'wll'
        FROM #Results as R
       group by R.cc 

INSERT INTO [dbo].[MDFFrame]
([MDFID]
,[FrameNo]
) values( (select top(1) MDFID from @MDFTable) ,1 )



INSERT INTO [dbo].[VerticalMDFColumn]
           ([VerticalCloumnNo]
           ,[MDFFrameID])
SELECT   T.ra , T.MDF_FrameID
FROM (  SELECT 
		R.ra
        , (select mf.ID 
			from MDFFrame as mf join MDF as m on mf.MDFID = m.ID 
			where mf.MDFID = (select top(1) MDFID from @MDFTable) ) as MDF_FrameID
			--where m.CenterID = (select id  from Center where CenterCode = R.cc)) as MDF_FrameID
        FROM #Results as R
        group by R.cc , R.ra
) as T
GROUP BY T.MDF_FrameID , T.ra 


INSERT INTO [dbo].[VerticalMDFRow]
           ([VerticalMDFColumnID]
           ,[VerticalRowNo]
		   ,RowCapacity)
SELECT T.VerticalMDFColumnID , T.ta , T.RowCapacity
  FROM
  (      SELECT  
		(SELECT          dbo.VerticalMDFColumn.ID
         FROM            dbo.MDF INNER JOIN
                         dbo.MDFFrame ON dbo.MDF.ID = dbo.MDFFrame.MDFID INNER JOIN
                         dbo.VerticalMDFColumn ON dbo.MDFFrame.ID = dbo.VerticalMDFColumn.MDFFrameID 
						 WHERE  dbo.VerticalMDFColumn.VerticalCloumnNo = R.ra  AND dbo.MDF.ID = (select top(1) MDFID from @MDFTable) -- dbo.MDF.CenterID = (select id  from Center where CenterCode = R.cc)
			   ) as VerticalMDFColumnID
		,R.ta
		,0 as RowCapacity
        FROM #Results as R
        group by R.cc ,R.ra, R.ta
	) as T
Group BY T.VerticalMDFColumnID , T.ta , T.RowCapacity


DECLARE @cc  int
DECLARE @kc  int
DECLARE @ra  int
DECLARE @ta  int
DECLARE @ase int 
DECLARE @tae int
DECLARE @i2 int;
DECLARE Cabinet_Cursor CURSOR  READ_ONLY FOR SELECT cc  , kc  , ra  , ta  , ase  ,tae  from #Results order by kc , number
OPEN Cabinet_Cursor;
    FETCH NEXT FROM Cabinet_Cursor INTO @cc  , @kc  , @ra  , @ta  , @ase  ,@tae
    WHILE @@FETCH_STATUS = 0
      BEGIN 
	   
		SET @i2 = @ase
		while @i2 <= @tae
			begin
			BEGIN TRY
				DECLARE @vrID int
		         SET @vrID =(select vr.id from  VerticalMDFRow as vr join VerticalMDFColumn as vc on vc.ID = vr.VerticalMDFColumnID
		                                         join MDFFrame as mf on mf.ID = vc.MDFFrameID
												 join MDF as m on m.ID = mf.MDFID
												 WHERE  vc.VerticalCloumnNo = @ra  AND  vr.VerticalRowNo = @ta AND m.ID =(select top(1) MDFID from @MDFTable)) -- m.CenterID = (select id  from Center where CenterCode = @cc) )
			
			Insert into Bucht VALUES (	@vrID,null,null,null,13,@i2,null,null,null,null,null ,0,null , 0,null  , @kc,null,null,null,null,null,null,null,null,null,null,null);
			END TRY
            BEGIN CATCH
            insert into #Error VALUES(@kc , @ra , @ta , @i2 , (select ERROR_MESSAGE()) )
            END CATCH
			
			set @i2 = @i2 +1
			end
        FETCH NEXT FROM Cabinet_Cursor  INTO @cc  , @kc  , @ra  , @ta  , @ase  ,@tae
      END;

CLOSE Cabinet_Cursor;
DEALLOCATE Cabinet_Cursor;


select * from #Error 



--INSERT INTO [dbo].[Bucht]
--           (
--		    [MDFRowID]
--           ,[SwitchPortID]
--           ,[CablePairID]
--           ,[CabinetInputID]
--	   ,[BuchtTypeID]
--           ,[BuchtNo]
--           ,[MAMOLIPortID]
--           ,[PortNo]
--           ,[ConnectionID]
--           ,[ConnectionIDBucht]
--           ,[BuchtIDConnectedOtherBucht]
--           ,[MAMOLIStatus]
--           ,[Status]
--		   )
--SELECT  
--		(select vr.id from  VerticalMDFRow as vr join VerticalMDFColumn as vc on vc.ID = vr.VerticalMDFColumnID
--		                                         join MDFFrame as mf on mf.ID = vc.MDFFrameID
--												 join MDF as m on m.ID = mf.MDFID
--												 WHERE  vc.VerticalCloumnNo = R.ra  AND m.CenterID = (select id  from Center where CenterCode = R.cc) AND vr.VerticalRowNo = R.ta)
--		,null
--		,null
--		,null
--		,13
--		,@i
--		,null
--		,null
--		,null
--        ,null
--		,null
--		,null
--        ,null
--		,0
--		,1
--        FROM #Results as R


    
--GO




