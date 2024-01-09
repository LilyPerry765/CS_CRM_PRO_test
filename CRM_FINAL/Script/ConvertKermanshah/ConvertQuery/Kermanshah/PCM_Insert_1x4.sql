
Use crm
GO

IF OBJECT_ID('tempdb..#Error') IS NOT NULL
DROP TABLE #Error
CREATE TABLE #Error(
	[CenterID] [int] NULL,
	[Kafo] [int] NULL,
	[vorodi] [int] NULL,
	[Post] [int] NULL,
	[Etesali] [int] NULL,
	[radif] [int] NULL,
	[tabaghe] [int] NULL,
	[tetesal] [int] NULL,
	[des] [nvarchar](50) NULL,
	[pcmradif] [int] NULL,
	[pcmtabaghe] [int] NULL,
	[pcmetesali] [int] NULL
) 

--BEGIN TRY
--    BEGIN TRANSACTION

declare @TABAGHE int
declare @RADIF int
declare	@ETESALE  int
declare	@CenterID  int ; set @CenterID = 265
DECLARE @insertTable table(ID int);
declare @VorodiBuchtID int;
declare @VorodiBuchtIDCabinetInputID int;
declare @PCMRockID int

insert into PCMRock values(@CenterID,1)
set @PCMRockID = (SELECT ID from [dbo].PCMRock where CenterID = @CenterID and Number =1);


declare pcm_cursor cursor read_only for select  RAD	,TAB	,ETE   from [Javanrood].[dbo].[PCM4]   group by  RAD, TAB, ETE , [TYPE],IsValid having [TYPE] = 504 and Count(*) <= 4 AND IsValid IS NULL

    OPEN pcm_cursor;
    fetch next from pcm_cursor into @RADIF , @TABAGHE ,	@ETESALE
	while @@FETCH_STATUS = 0
	Begin

	print 'pcm'
	IF OBJECT_ID('tempdb..#pcm') IS NOT NULL
	 DROP TABLE #pcm


	create table #pcm (TEL_NO  nvarchar(max),KAFO nvarchar(max),Vorodi nvarchar(max), POST nvarchar(max), ZOJ nvarchar(max), RAD nvarchar(max),	TAB nvarchar(max), ETE nvarchar(max),	RAD_2 nvarchar(max), TAB_2 nvarchar(max) , 	ETE_2 nvarchar(max),  TYPE nvarchar(max), CODE nvarchar(max),Rock nvarchar(max),Shelf nvarchar(max),card nvarchar(max) , AORB int,IsValid bit ,Description nvarchar(max))
	                      
	insert into #pcm select * from [Javanrood].[dbo].[PCM4]   as v where v.RAD = @RADIF and v.TAB = @TABAGHE and v.ETE = @ETESALE AND ISVALID IS NULL order by RAD_2, TAB_2, ETE_2


	--if exists (select b.ID from bucht as b join VerticalMDFRow as vr on vr.ID = b.MDFRowID
	--                join VerticalMDFColumn as vc on vc.ID = vr.VerticalMDFColumnID
	--	            join MDFFrame as mf on mf.ID = vc.MDFFrameID
	--				join MDF as m on m.ID = mf.MDFID
	--			    where  vr.VerticalRowNo = @RADIF  AND vc.VerticalCloumnNo = @TABAGHE  and b.BuchtNo = @ETESALE AND m.ID = (select Max(ID) from MDF where MDF.CenterID = @CenterID AND MDF.Number = 1) )
	--begin

		declare @shelfNumber int; 
	set @shelfNumber = (select min(CONVERT(int , KAFO)) from #pcm where KAFO in (select CabinetNumber from [crm].dbo.Cabinet where CenterID = @CenterID))

    if(@shelfNumber is null)
	begin
	INSERT INTO #Error VALUES(@CenterID,@shelfNumber,null,null , null  , @RADIF , @TABAGHE , @ETESALE ,'kafo', null , null ,null);
	Goto Cont;
    end

	set @VorodiBuchtIDCabinetInputID = null;
	set @VorodiBuchtID = null;

    if not exists(select * from bucht as b join VerticalMDFRow as vr on vr.ID = b.MDFRowID
	                join VerticalMDFColumn as vc on vc.ID = vr.VerticalMDFColumnID
		            join MDFFrame as mf on mf.ID = vc.MDFFrameID
					join MDF as m on m.ID = mf.MDFID
				       where vc.VerticalCloumnNo = @RADIF and vr.VerticalRowNo = @TABAGHE  and b.BuchtNo = @ETESALE 
					   --AND b.CabinetNumber = @shelfNumber 
					    and m.CenterID = @CenterID AND (m.Description LIKE  N'%اصلی مرکز%' OR  m.Description LIKE  N'%اختصاصی%')
					   )
	begin
		INSERT INTO #Error VALUES(@CenterID,@shelfNumber,null,null , null  , @RADIF , @TABAGHE , @ETESALE ,'bucht not found', null , null ,null);
		Goto Cont;
	end

   if not exists(select * from bucht as b join VerticalMDFRow as vr on vr.ID = b.MDFRowID
	                join VerticalMDFColumn as vc on vc.ID = vr.VerticalMDFColumnID
		            join MDFFrame as mf on mf.ID = vc.MDFFrameID
					join MDF as m on m.ID = mf.MDFID
				       where vc.VerticalCloumnNo = @RADIF and vr.VerticalRowNo = @TABAGHE  and b.BuchtNo = @ETESALE 
					   --AND b.CabinetNumber = @shelfNumber  
					   and m.CenterID = @CenterID and b.Status = 0 AND (m.Description LIKE  N'%اصلی مرکز%' OR  m.Description LIKE  N'%اختصاصی%')
					   )
	begin
		INSERT INTO #Error VALUES(@CenterID,@shelfNumber,null,null , null  , @RADIF , @TABAGHE , @ETESALE ,'free bucht not found', null , null ,null);
		Goto Cont;
	end

	select @VorodiBuchtID = b.ID , @VorodiBuchtIDCabinetInputID = b.CabinetInputID from bucht as b join VerticalMDFRow as vr on vr.ID = b.MDFRowID
	                join VerticalMDFColumn as vc on vc.ID = vr.VerticalMDFColumnID
		            join MDFFrame as mf on mf.ID = vc.MDFFrameID
					join MDF as m on m.ID = mf.MDFID
				       where  vr.VerticalRowNo = @TABAGHE  AND vc.VerticalCloumnNo = @RADIF  and b.BuchtNo = @ETESALE 
					   --AND bucht.CabinetNumber = @shelfNumber 
					   and m.CenterID = @CenterID AND  (m.Description LIKE  N'%اصلی مرکز%' OR  m.Description LIKE  N'%اختصاصی%')

	 --end
	 --else
	 --begin
	 --	--select @VorodiBuchtID = b.ID , @VorodiBuchtIDCabinetInputID = b.CabinetInputID from bucht as b where ID = @VirtualBuchtID
		----set @VirtualBuchtID = @VirtualBuchtID + 1;
		--declare @vrID int;
		--declare @BuchtNoID int;

		--Insert into Bucht VALUES (	@vrID,null,null,null,13,@i,null,null,null,null,null ,null,0,0,null  , 99999);
	 --end

	declare @shelfID int;
	declare @PCMID int;
	declare @PortID int;
	declare @BuchtID int;
	declare @lastPostContactID int;
	declare @switchPostID int = null;
	declare  @MDFRowID   int = null;
	
	

	declare @PCMNumber int; set @PCMNumber   = (select top(1) Vorodi from #pcm where KAFO = @shelfNumber)
	declare @AORB int; set @AORB   = (select top(1) AORB from #pcm where KAFO = @shelfNumber)
	declare @PostNumber int; set @PostNumber   = (select top(1) #pcm.POST from #pcm where KAFO = @shelfNumber)
	declare @zoje int; set @zoje   = (select top(1) #pcm.ZOJ from #pcm where KAFO = @shelfNumber)

	declare  @pcmRadif   int; set           @pcmRadif   = (select top(1) #pcm.RAD_2   from #pcm where  RAD_2 <> 0)
	declare  @PcmTabaghe int; set           @PcmTabaghe = (select top(1) #pcm.TAB_2   from #pcm where  RAD_2 <> 0)
    DECLARE  @pcmetesali nvarchar(max); set @pcmetesali = (SELECT min(CONVERT(int , #pcm.ETE_2)) FROM #pcm where RAD_2 <> 0)
	

	declare @PostID int; 
	declare @PostContactID int;

	--if exists (select Post.ID from Post join Cabinet on Post.CabinetID = Cabinet.ID where Cabinet.CabinetNumber= @shelfNumber and Post.Number = @PostNumber)
	  set @PostID   = (select Post.ID from Post join Cabinet on Post.CabinetID = Cabinet.ID where Cabinet.CabinetNumber= @shelfNumber and Post.Number = @PostNumber and @AORB = Post.AORBType and Cabinet.CenterID = @CenterID)
		

		  if(@PostID is null)
	  begin
	    INSERT INTO #Error VALUES(@CenterID,@shelfNumber,@PCMNumber,@PostNumber , null  , @RADIF , @TABAGHE , @ETESALE ,'post' , null , null ,null)
		Goto Cont;
	  end
	  print @shelfNumber;
	  print @PostNumber;
	  print @PostID;
	--else
	  --set @PostID   = 41530

	 --if exists (select ID from PostContact where @PostID = PostContact.PostID and PostContact.ConnectionNo = @zoje)
	 set @PostContactID   = (select ID from PostContact where @PostID = PostContact.PostID and PostContact.ConnectionNo = @zoje and PostContact.ConnectionType = 3)
	 if(@PostContactID is null)
	 begin
	   INSERT INTO #Error VALUES(@CenterID,@shelfNumber,@PCMNumber,@PostNumber , @zoje  , @RADIF , @TABAGHE , @ETESALE ,'etesali', null , null ,null)
	   		Goto Cont;
	  end

	  	if not exists( select * from PostContact where Status = 5 and ID = @PostContactID)
	begin
		INSERT INTO #Error VALUES(@CenterID,@shelfNumber,@PCMNumber,@PostNumber , null  , @RADIF , @TABAGHE , @ETESALE ,'free postcontact not found' , null , null ,null)
		Goto Cont;
	end

	 --else 
	   --set @PostContactID   = (select top(1) ID from PostContact where PostID = 41530 and PostContact.Status = 5 and PostContact.ConnectionType = 3 )

	 if exists (select * from PCMShelf where PCMShelf.Number = @shelfNumber and  PCMShelf.PCMRockID = @PCMRockID)
	 begin
	  set @shelfID = (select ID from PCMShelf where PCMShelf.Number = @shelfNumber and  PCMShelf.PCMRockID = @PCMRockID);
	 END
	 Else
	 begin
	  INSERT into PCMShelf  values((select max(ID) from PCMRock) ,@shelfNumber);
	  set @shelfID = (SELECT MAX(ID) from PCMShelf);
	 END

	 -----
	 INSERT INTO [dbo].[PCM]([ShelfID],[Card],[PCMBrandID],[PCMTypeID] ,[InstallAddress],[InstallPostCode],[Status],[InsertDate]) values( @shelfID, @PCMNumber , 1 ,  4 , N'نامشخص' , N'0' ,2,'4/6/2014')
	 set @PCMID = (SELECT MAX(ID) from [dbo].[PCM]);



	 if RIGHT(@pcmetesali,1) = 1 OR RIGHT(@pcmetesali,1) = 2 OR RIGHT(@pcmetesali,1) = 3 OR RIGHT(@pcmetesali,1) = 4 OR RIGHT(@pcmetesali,1) = 5
     BEGIN
	 ------
     insert into PCMPort ([PCMID],[PortNumber],[PortType] ,[Status],[ElkaID]) VALUES (@PCMID , 1 , 9 , 1 , null)
	 set @PortID = (SELECT MAX(ID) from [dbo].[PCMPort]);
	 DECLARE @X1 int; set @X1 = CAST( (SUBSTRING(@pcmetesali , 1 , Len(@pcmetesali) - 1) + '1' ) as int);
	 
	
	 update PostContact set Status = 0 , ConnectionType = 4  where ID = @PostContactID

	 if exists( select * from bucht as b join VerticalMDFRow as vr on vr.ID = b.MDFRowID
	                join VerticalMDFColumn as vc on vc.ID = vr.VerticalMDFColumnID
		            join MDFFrame as mf on mf.ID = vc.MDFFrameID
					join MDF as m on m.ID = mf.MDFID
				    where  vr.VerticalRowNo =  @PcmTabaghe AND vc.VerticalCloumnNo = @pcmRadif      and b.BuchtNo = @X1 AND m.Description in  (  N'PCM') and m.CenterID = @CenterID )
      Begin

	  update b
	    set PCMPortID = @PortID , b.Status = 7 , BuchtIDConnectedOtherBucht = @VorodiBuchtID , CabinetInputID = @VorodiBuchtIDCabinetInputID ,BuchtTypeID = 9 , ConnectionID = @PostContactID OUTPUT inserted.ID into @insertTable
		 from bucht as b join VerticalMDFRow as vr on vr.ID = b.MDFRowID
	                join VerticalMDFColumn as vc on vc.ID = vr.VerticalMDFColumnID
		            join MDFFrame as mf on mf.ID = vc.MDFFrameID
					join MDF as m on m.ID = mf.MDFID
				   where  vr.VerticalRowNo =  @PcmTabaghe AND vc.VerticalCloumnNo = @pcmRadif      and b.BuchtNo = @X1 AND m.Description in  (  N'PCM') and m.CenterID = @CenterID 
	   End
	   else
	   begin
	   INSERT INTO #Error VALUES(@CenterID,@shelfNumber,@PCMNumber,@PostNumber , null  , @RADIF , @TABAGHE , @ETESALE ,'pcm bucht' , @pcmRadif , @PcmTabaghe ,@X1)
	   set  @MDFRowID = null;


	    set @MDFRowID = (select top (1) b.MDFRowID from bucht as b join VerticalMDFRow as vr on vr.ID = b.MDFRowID
	                join VerticalMDFColumn as vc on vc.ID = vr.VerticalMDFColumnID
		            join MDFFrame as mf on mf.ID = vc.MDFFrameID
					join MDF as m on m.ID = mf.MDFID
				    where  vr.VerticalRowNo =  @PcmTabaghe AND vc.VerticalCloumnNo = @pcmRadif     AND m.Description in  (  N'PCM') and m.CenterID = @CenterID)
  if ( @MDFRowID is null)
	   begin
	   	   set @MDFRowID = (select  top(1) b.MDFRowID from bucht as b join VerticalMDFRow as vr on vr.ID = b.MDFRowID
	                join VerticalMDFColumn as vc on vc.ID = vr.VerticalMDFColumnID
		            join MDFFrame as mf on mf.ID = vc.MDFFrameID
					join MDF as m on m.ID = mf.MDFID
				    where   (m.CenterID = @CenterID) and (m.Description in  (  N'PCM')) order by  (b.MDFRowID) DESC )
					set @X1 = (select Max(BuchtNo)+1 from Bucht where Bucht.MDFRowID = @MDFRowID)
	    end

	   INSERT INTO [dbo].[Bucht]([MDFRowID],[SwitchPortID],[CablePairID],[CabinetInputID],[BuchtTypeID],[BuchtNo] ,[PCMPortID],[PortNo] ,[ConnectionID] ,[BuchtIDConnectedOtherBucht],[E1NumberID],[ADSLStatus],[Status],[ElkaID],[CabinetNumber])
                  OUTPUT inserted.ID into @insertTable VALUES(@MDFRowID,null,null,@VorodiBuchtIDCabinetInputID,9,@X1,@PortID ,1, @PostContactID ,@VorodiBuchtID,null,0,7,null,999)
	   end
	  update Bucht set BuchtIDConnectedOtherBucht = (select ID from @insertTable) , Status = 13 , ConnectionID = @PostContactID where ID = @VorodiBuchtID
	  delete @insertTable;
	  ------
	  DECLARE @X2 int; set @X2 = CAST( (SUBSTRING(@pcmetesali , 1 , Len(@pcmetesali) - 1) + '2') as int);
	  print @pcmRadif;
	  print @PcmTabaghe;
	  print @X2;
	  set @switchPostID   = (select top(1) Telephone.SwitchPortID from #pcm join Telephone on Telephone.TelephoneNoIndividual = #pcm.TEL_NO where #pcm.TAB_2 =  @PcmTabaghe   and #pcm.RAD_2 = @pcmRadif and #pcm.ETE_2 = @X2 and Telephone.CenterID = @CenterID)		    

	 insert into PCMPort ([PCMID],[PortNumber],[PortType] ,[Status],[ElkaID]) VALUES (@PCMID , 1 , 8 , IIF(@switchPostID is null , 1  , 2) , null)
	 set @PortID = (SELECT MAX(ID) from [dbo].[PCMPort]);
	
	 INSERT INTO [dbo].[PostContact]([PostID],[ConnectionNo],[ConnectionType],[FirsetCableColorID],[SecondCableColorID],[Status],[ElkaID]) VALUES(@PostID,@zoje,5,null ,null,IIF(@switchPostID is null , 5  , 1),null)
	 set @lastPostContactID = (SELECT MAX(ID) from [dbo].[PostContact]);
    

	 if exists( select * from bucht as b join VerticalMDFRow as vr on vr.ID = b.MDFRowID 
	                join VerticalMDFColumn as vc on vc.ID = vr.VerticalMDFColumnID
		            join MDFFrame as mf on mf.ID = vc.MDFFrameID
					join MDF as m on m.ID = mf.MDFID
				    where  vr.VerticalRowNo = @PcmTabaghe AND vc.VerticalCloumnNo = @pcmRadif   and b.BuchtNo = @X2 AND m.Description in  (  N'PCM') and m.CenterID = @CenterID )
      Begin
	  update b
	    set PCMPortID = @PortID , ConnectionID = @lastPostContactID , b.Status = IIF(@switchPostID is null , 7  , 1) , CabinetInputID = @VorodiBuchtIDCabinetInputID , SwitchPortID = @switchPostID , BuchtTypeID = 8
		 from bucht as b join VerticalMDFRow as vr on vr.ID = b.MDFRowID
	                join VerticalMDFColumn as vc on vc.ID = vr.VerticalMDFColumnID
		            join MDFFrame as mf on mf.ID = vc.MDFFrameID
					join MDF as m on m.ID = mf.MDFID
				    where  vr.VerticalRowNo = @PcmTabaghe AND vc.VerticalCloumnNo = @pcmRadif   and b.BuchtNo = @X2 AND m.Description in  (  N'PCM') and m.CenterID = @CenterID 

	   End
	   else
	   begin
	   INSERT INTO #Error VALUES(@CenterID,@shelfNumber,@PCMNumber,@PostNumber , null  , @RADIF , @TABAGHE , @ETESALE ,'pcm bucht' , @pcmRadif , @PcmTabaghe ,@X2)
	   set  @MDFRowID  = null;

	    set @MDFRowID = (select  top(1) b.MDFRowID from bucht as b join VerticalMDFRow as vr on vr.ID = b.MDFRowID
	                join VerticalMDFColumn as vc on vc.ID = vr.VerticalMDFColumnID
		            join MDFFrame as mf on mf.ID = vc.MDFFrameID
					join MDF as m on m.ID = mf.MDFID
				    where  vr.VerticalRowNo =  @PcmTabaghe AND vc.VerticalCloumnNo = @pcmRadif     AND m.Description in  (  N'PCM') and m.CenterID = @CenterID)

	   if ( @MDFRowID is null)
	   begin
	   	   set @MDFRowID = (select  top(1) b.MDFRowID from bucht as b join VerticalMDFRow as vr on vr.ID = b.MDFRowID
	                join VerticalMDFColumn as vc on vc.ID = vr.VerticalMDFColumnID
		            join MDFFrame as mf on mf.ID = vc.MDFFrameID
					join MDF as m on m.ID = mf.MDFID
				    where   (m.CenterID = @CenterID) and (m.Description in  (  N'PCM')) order by  (b.MDFRowID) DESC )
					set @X2 = (select Max(BuchtNo)+1 from Bucht where Bucht.MDFRowID = @MDFRowID)
	    end


	   INSERT INTO [dbo].[Bucht]([MDFRowID],[SwitchPortID],[CablePairID],[CabinetInputID],[BuchtTypeID],[BuchtNo] ,[PCMPortID],[PortNo] ,[ConnectionID] ,[BuchtIDConnectedOtherBucht],[E1NumberID],[ADSLStatus],[Status],[ElkaID],[CabinetNumber])
                   VALUES(@MDFRowID,@switchPostID,null,@VorodiBuchtIDCabinetInputID,8,@X2,@PortID ,1,@lastPostContactID ,null ,null,0,IIF(@switchPostID is null , 7  , 1) ,null,999)
	   update Bucht set PCMPortID = @PortID where ID = (SELECT MAX(ID) from [dbo].[Bucht]);
	   end
	 ------
	 DECLARE @X3 int; set @X3 = CAST( (SUBSTRING(@pcmetesali , 1 , Len(@pcmetesali) - 1) + '3') as int);
	 set @switchPostID   = (select top(1) Telephone.SwitchPortID from #pcm join Telephone on Telephone.TelephoneNoIndividual = #pcm.TEL_NO where #pcm.TAB_2 =  @PcmTabaghe   and #pcm.RAD_2 = @pcmRadif and #pcm.ETE_2 = @X3 and Telephone.CenterID = @CenterID)		    	    

	 insert into PCMPort ([PCMID],[PortNumber],[PortType] ,[Status],[ElkaID]) VALUES (@PCMID , 2 , 8 , IIF(@switchPostID is null , 1  , 2) , null)
	 set @PortID = (SELECT MAX(ID) from [dbo].[PCMPort]);
	 
	 INSERT INTO [dbo].[PostContact]([PostID],[ConnectionNo],[ConnectionType],[FirsetCableColorID],[SecondCableColorID],[Status],[ElkaID]) VALUES(@PostID,@zoje,5,null ,null,IIF(@switchPostID is null , 5  , 1),null)
	 set @lastPostContactID = (SELECT MAX(ID) from [dbo].[PostContact]);
	 

	 if exists( select * from bucht as b join VerticalMDFRow as vr on vr.ID = b.MDFRowID
	                join VerticalMDFColumn as vc on vc.ID = vr.VerticalMDFColumnID
		            join MDFFrame as mf on mf.ID = vc.MDFFrameID
					join MDF as m on m.ID = mf.MDFID
				    where  vr.VerticalRowNo = @PcmTabaghe AND vc.VerticalCloumnNo = @pcmRadif   and b.BuchtNo = @X3 AND m.Description in  (  N'PCM') and m.CenterID = @CenterID )
      Begin
	  update b
	    set PCMPortID = @PortID , ConnectionID = @lastPostContactID , b.Status = IIF(@switchPostID is null , 7  , 1) , CabinetInputID = @VorodiBuchtIDCabinetInputID , SwitchPortID = @switchPostID ,BuchtTypeID = 8
		 from bucht as b join VerticalMDFRow as vr on vr.ID = b.MDFRowID
	                join VerticalMDFColumn as vc on vc.ID = vr.VerticalMDFColumnID
		            join MDFFrame as mf on mf.ID = vc.MDFFrameID
					join MDF as m on m.ID = mf.MDFID
				    where  vr.VerticalRowNo = @PcmTabaghe AND vc.VerticalCloumnNo = @pcmRadif   and b.BuchtNo = @X3 AND m.Description in  (  N'PCM') and m.CenterID = @CenterID 

	   End
	   else
	   begin
	   INSERT INTO #Error VALUES(@CenterID,@shelfNumber,@PCMNumber,@PostNumber , null  , @RADIF , @TABAGHE , @ETESALE ,'pcm bucht' , @pcmRadif , @PcmTabaghe ,@X3)
	   set  @MDFRowID  = null ;

	    set @MDFRowID = (select  top(1) b.MDFRowID from bucht as b join VerticalMDFRow as vr on vr.ID = b.MDFRowID
	                join VerticalMDFColumn as vc on vc.ID = vr.VerticalMDFColumnID
		            join MDFFrame as mf on mf.ID = vc.MDFFrameID
					join MDF as m on m.ID = mf.MDFID
				    where  vr.VerticalRowNo =  @PcmTabaghe AND vc.VerticalCloumnNo = @pcmRadif     AND m.Description in  (  N'PCM') and m.CenterID = @CenterID)

						   if ( @MDFRowID is null)
	   begin
	   	   set @MDFRowID = (select  top(1) b.MDFRowID from bucht as b join VerticalMDFRow as vr on vr.ID = b.MDFRowID
	                join VerticalMDFColumn as vc on vc.ID = vr.VerticalMDFColumnID
		            join MDFFrame as mf on mf.ID = vc.MDFFrameID
					join MDF as m on m.ID = mf.MDFID
				    where   (m.CenterID = @CenterID) and (m.Description in  (  N'PCM')) order by  (b.MDFRowID) DESC )
					set @X3 = (select Max(BuchtNo)+1 from Bucht where Bucht.MDFRowID = @MDFRowID)
	    end


	   INSERT INTO [dbo].[Bucht]([MDFRowID],[SwitchPortID],[CablePairID],[CabinetInputID],[BuchtTypeID],[BuchtNo] ,[PCMPortID],[PortNo] ,[ConnectionID] ,[BuchtIDConnectedOtherBucht],[E1NumberID],[ADSLStatus],[Status],[ElkaID],[CabinetNumber])
                   VALUES(@MDFRowID,null,null,@VorodiBuchtIDCabinetInputID,8,@X3,@PortID ,1,@lastPostContactID ,null ,null,0,IIF(@switchPostID is null , 7  , 1),null,999)
	   update Bucht set PCMPortID = @PortID where ID = (SELECT MAX(ID) from [dbo].[Bucht]);
	   end

	 ------
	 DECLARE @X4 int; set @X4 = CAST( (SUBSTRING(@pcmetesali , 1 , Len(@pcmetesali) - 1) + '4') as int);
	 set @switchPostID   = (select top(1) Telephone.SwitchPortID from #pcm join Telephone on Telephone.TelephoneNoIndividual = #pcm.TEL_NO where #pcm.TAB_2 =  @PcmTabaghe   and #pcm.RAD_2 = @pcmRadif and #pcm.ETE_2 = @X4 and Telephone.CenterID = @CenterID)		    

	 insert into PCMPort ([PCMID],[PortNumber],[PortType] ,[Status],[ElkaID]) VALUES (@PCMID , 3 , 8 , IIF(@switchPostID is null , 1  , 2) , null)
	 set @PortID = (SELECT MAX(ID) from [dbo].[PCMPort]);
	
	 INSERT INTO [dbo].[PostContact]([PostID],[ConnectionNo],[ConnectionType],[FirsetCableColorID],[SecondCableColorID],[Status],[ElkaID]) VALUES(@PostID,@zoje,5,null ,null,IIF(@switchPostID is null , 5  , 1),null)
	 set @lastPostContactID = (SELECT MAX(ID) from [dbo].[PostContact]);
	   

	 if exists( select * from bucht as b join VerticalMDFRow as vr on vr.ID = b.MDFRowID
	                join VerticalMDFColumn as vc on vc.ID = vr.VerticalMDFColumnID
		            join MDFFrame as mf on mf.ID = vc.MDFFrameID
					join MDF as m on m.ID = mf.MDFID
				    where  vr.VerticalRowNo = @PcmTabaghe AND vc.VerticalCloumnNo = @pcmRadif   and b.BuchtNo = @X4 AND m.Description in  (  N'PCM') and m.CenterID = @CenterID )
      Begin
	  update b
	    set PCMPortID = @PortID , ConnectionID = @lastPostContactID , b.Status = IIF(@switchPostID is null , 7  , 1) , CabinetInputID = @VorodiBuchtIDCabinetInputID ,SwitchPortID = @switchPostID , BuchtTypeID = 8
		 from bucht as b join VerticalMDFRow as vr on vr.ID = b.MDFRowID
	                join VerticalMDFColumn as vc on vc.ID = vr.VerticalMDFColumnID
		            join MDFFrame as mf on mf.ID = vc.MDFFrameID
					join MDF as m on m.ID = mf.MDFID
				    where  vr.VerticalRowNo = @PcmTabaghe AND vc.VerticalCloumnNo = @pcmRadif   and b.BuchtNo = @X4 AND m.Description in  (  N'PCM') and m.CenterID = @CenterID 

	   End
	   else
	   begin
	   INSERT INTO #Error VALUES(@CenterID,@shelfNumber,@PCMNumber,@PostNumber , null  , @RADIF , @TABAGHE , @ETESALE ,'pcm bucht' , @pcmRadif , @PcmTabaghe ,@X4)
	    set  @MDFRowID = null ;

	    set @MDFRowID = (select  top(1) b.MDFRowID from bucht as b join VerticalMDFRow as vr on vr.ID = b.MDFRowID
	                join VerticalMDFColumn as vc on vc.ID = vr.VerticalMDFColumnID
		            join MDFFrame as mf on mf.ID = vc.MDFFrameID
					join MDF as m on m.ID = mf.MDFID
				    where  vr.VerticalRowNo =  @PcmTabaghe AND vc.VerticalCloumnNo = @pcmRadif     AND m.Description in  (  N'PCM') and m.CenterID = @CenterID)

						   if ( @MDFRowID is null)
	   begin
	   	   set @MDFRowID = (select  top(1) b.MDFRowID from bucht as b join VerticalMDFRow as vr on vr.ID = b.MDFRowID
	                join VerticalMDFColumn as vc on vc.ID = vr.VerticalMDFColumnID
		            join MDFFrame as mf on mf.ID = vc.MDFFrameID
					join MDF as m on m.ID = mf.MDFID
				    where   (m.CenterID = @CenterID) and (m.Description in  (  N'PCM')) order by  (b.MDFRowID) DESC )
					set @X4 = (select Max(BuchtNo)+1 from Bucht where Bucht.MDFRowID = @MDFRowID)
	    end



	   INSERT INTO [dbo].[Bucht]([MDFRowID],[SwitchPortID],[CablePairID],[CabinetInputID],[BuchtTypeID],[BuchtNo] ,[PCMPortID],[PortNo] ,[ConnectionID] ,[BuchtIDConnectedOtherBucht],[E1NumberID],[ADSLStatus],[Status],[ElkaID],[CabinetNumber])
                   VALUES(@MDFRowID,@switchPostID,null,@VorodiBuchtIDCabinetInputID,8,@X4,@PortID ,1,@lastPostContactID ,null ,null,0,IIF(@switchPostID is null , 7  , 1) ,null,999)
	   update Bucht set PCMPortID = @PortID where ID = (SELECT MAX(ID) from [dbo].[Bucht]);
	   end

	 ------

	 DECLARE @X5 int; set @X5 = CAST( (SUBSTRING(@pcmetesali , 1 , Len(@pcmetesali) - 1) + '5') as int);
	 set @switchPostID   = (select top(1) Telephone.SwitchPortID from #pcm join Telephone on Telephone.TelephoneNoIndividual = #pcm.TEL_NO where #pcm.TAB_2 =  @PcmTabaghe   and #pcm.RAD_2 = @pcmRadif and #pcm.ETE_2 = @X5 and Telephone.CenterID = @CenterID)		        

	 insert into PCMPort ([PCMID],[PortNumber],[PortType] ,[Status],[ElkaID]) VALUES (@PCMID , 4 , 8 , IIF(@switchPostID is null , 1  , 2) , null)
	 set @PortID = (SELECT MAX(ID) from [dbo].[PCMPort]);
	
	  INSERT INTO [dbo].[PostContact]([PostID],[ConnectionNo],[ConnectionType],[FirsetCableColorID],[SecondCableColorID],[Status],[ElkaID]) VALUES(@PostID,@zoje,5,null ,null,IIF(@switchPostID is null , 5  , 1),null)
	  set @lastPostContactID = (SELECT MAX(ID) from [dbo].[PostContact]);
	  

	 if exists( select * from bucht as b join VerticalMDFRow as vr on vr.ID = b.MDFRowID
	                join VerticalMDFColumn as vc on vc.ID = vr.VerticalMDFColumnID
		            join MDFFrame as mf on mf.ID = vc.MDFFrameID
					join MDF as m on m.ID = mf.MDFID
				    where  vr.VerticalRowNo = @PcmTabaghe AND vc.VerticalCloumnNo = @pcmRadif   and b.BuchtNo = @X5 AND m.Description in  (  N'PCM') and m.CenterID = @CenterID )
      Begin
	  update b
	    set PCMPortID = @PortID , ConnectionID = @lastPostContactID , b.Status = IIF(@switchPostID is null , 7  , 1) , CabinetInputID = @VorodiBuchtIDCabinetInputID , SwitchPortID = @switchPostID , BuchtTypeID = 8
		 from bucht as b join VerticalMDFRow as vr on vr.ID = b.MDFRowID
	                join VerticalMDFColumn as vc on vc.ID = vr.VerticalMDFColumnID
		            join MDFFrame as mf on mf.ID = vc.MDFFrameID
					join MDF as m on m.ID = mf.MDFID
				    where  vr.VerticalRowNo = @PcmTabaghe AND vc.VerticalCloumnNo = @pcmRadif   and b.BuchtNo = @X5 AND m.Description in  (  N'PCM') and m.CenterID = @CenterID 

	   End
	   else
	   begin
	   INSERT INTO #Error VALUES(@CenterID,@shelfNumber,@PCMNumber,@PostNumber , null  , @RADIF , @TABAGHE , @ETESALE ,'pcm bucht' , @pcmRadif , @PcmTabaghe ,@X5)
	   set  @MDFRowID = null ;
	    set @MDFRowID = (select  top(1) b.MDFRowID from bucht as b join VerticalMDFRow as vr on vr.ID = b.MDFRowID
	                join VerticalMDFColumn as vc on vc.ID = vr.VerticalMDFColumnID
		            join MDFFrame as mf on mf.ID = vc.MDFFrameID
					join MDF as m on m.ID = mf.MDFID
				    where  vr.VerticalRowNo =  @PcmTabaghe AND vc.VerticalCloumnNo = @pcmRadif     AND m.Description in  (  N'PCM') and m.CenterID = @CenterID)

	   if ( @MDFRowID is null)
	   begin
	   	   set @MDFRowID = (select  top(1) b.MDFRowID from bucht as b join VerticalMDFRow as vr on vr.ID = b.MDFRowID
	                join VerticalMDFColumn as vc on vc.ID = vr.VerticalMDFColumnID
		            join MDFFrame as mf on mf.ID = vc.MDFFrameID
					join MDF as m on m.ID = mf.MDFID
				    where   (m.CenterID = @CenterID) and (m.Description in  (  N'PCM')) order by  (b.MDFRowID) DESC )

		set @X5 = (select Max(BuchtNo)+1 from Bucht where Bucht.MDFRowID = @MDFRowID)



	    end


	   INSERT INTO [dbo].[Bucht]([MDFRowID],[SwitchPortID],[CablePairID],[CabinetInputID],[BuchtTypeID],[BuchtNo] ,[PCMPortID],[PortNo] ,[ConnectionID] ,[BuchtIDConnectedOtherBucht],[E1NumberID],[ADSLStatus],[Status],[ElkaID],[CabinetNumber])
                   VALUES(@MDFRowID,@switchPostID,null,@VorodiBuchtIDCabinetInputID,8,@X5,@PortID ,1,@lastPostContactID ,null ,null,0,IIF(@switchPostID is null , 7  , 1) ,null,999)
	   update Bucht set PCMPortID = @PortID where ID = (SELECT MAX(ID) from [dbo].[Bucht]);
	   end
	 ------
	 END
	 ELSE IF  RIGHT(@pcmetesali,1) = 6 OR RIGHT(@pcmetesali,1) = 7 OR RIGHT(@pcmetesali,1) = 8 OR RIGHT(@pcmetesali,1) = 9 OR RIGHT(@pcmetesali,1) = 0
	 BEGIN
	 ------

	 DECLARE @X6 int; set @X6 = CAST( (SUBSTRING(@pcmetesali , 1 , Len(@pcmetesali) - 1) + '6' ) as int);

     insert into PCMPort ([PCMID],[PortNumber],[PortType] ,[Status],[ElkaID]) VALUES (@PCMID , 1 , 9 , 1 , null)
	 set @PortID = (SELECT MAX(ID) from [dbo].[PCMPort]);
	 
	 set  @lastPostContactID = @PostContactID
	 update PostContact set Status = 0 , ConnectionType = 4  where ID = @PostContactID

	 if exists( select * from bucht as b join VerticalMDFRow as vr on vr.ID = b.MDFRowID
	                join VerticalMDFColumn as vc on vc.ID = vr.VerticalMDFColumnID
		            join MDFFrame as mf on mf.ID = vc.MDFFrameID
					join MDF as m on m.ID = mf.MDFID
				    where  vr.VerticalRowNo = @PcmTabaghe AND vc.VerticalCloumnNo = @pcmRadif   and b.BuchtNo = @X6 AND m.Description in  (  N'PCM') and m.CenterID = @CenterID )
      Begin
	  update b
	    set PCMPortID = @PortID , b.Status = 7 , BuchtIDConnectedOtherBucht = @VorodiBuchtID, CabinetInputID = @VorodiBuchtIDCabinetInputID , BuchtTypeID = 9 , ConnectionID = @PostContactID OUTPUT inserted.ID into @insertTable
		 from bucht as b join VerticalMDFRow as vr on vr.ID = b.MDFRowID
	                join VerticalMDFColumn as vc on vc.ID = vr.VerticalMDFColumnID
		            join MDFFrame as mf on mf.ID = vc.MDFFrameID
					join MDF as m on m.ID = mf.MDFID
				    where  vr.VerticalRowNo = @PcmTabaghe AND vc.VerticalCloumnNo = @pcmRadif  and b.BuchtNo = @X6 AND m.Description in  (  N'PCM') and m.CenterID = @CenterID 

	   End
	   else
	   begin
	   INSERT INTO #Error VALUES(@CenterID,@shelfNumber,@PCMNumber,@PostNumber , null  , @RADIF , @TABAGHE , @ETESALE ,'pcm bucht' , @pcmRadif , @PcmTabaghe ,@X6)
	   set  @MDFRowID = null ;
	    set @MDFRowID = (select  top(1) b.MDFRowID from bucht as b join VerticalMDFRow as vr on vr.ID = b.MDFRowID
	                join VerticalMDFColumn as vc on vc.ID = vr.VerticalMDFColumnID
		            join MDFFrame as mf on mf.ID = vc.MDFFrameID
					join MDF as m on m.ID = mf.MDFID
				    where  vr.VerticalRowNo =  @PcmTabaghe AND vc.VerticalCloumnNo = @pcmRadif     AND m.Description in  (  N'PCM') and m.CenterID = @CenterID)

						   if ( @MDFRowID is null)
	   begin
	   	   set @MDFRowID = (select  top(1) b.MDFRowID from bucht as b join VerticalMDFRow as vr on vr.ID = b.MDFRowID
	                join VerticalMDFColumn as vc on vc.ID = vr.VerticalMDFColumnID
		            join MDFFrame as mf on mf.ID = vc.MDFFrameID
					join MDF as m on m.ID = mf.MDFID
				    where   (m.CenterID = @CenterID) and (m.Description in  (  N'PCM')) order by  (b.MDFRowID) DESC )

					set @X6 = (select Max(BuchtNo)+1 from Bucht where Bucht.MDFRowID = @MDFRowID)
	    end

	   INSERT INTO [dbo].[Bucht]([MDFRowID],[SwitchPortID],[CablePairID],[CabinetInputID],[BuchtTypeID],[BuchtNo] ,[PCMPortID],[PortNo] ,[ConnectionID] ,[BuchtIDConnectedOtherBucht],[E1NumberID],[ADSLStatus],[Status],[ElkaID],[CabinetNumber])
                   OUTPUT inserted.ID into @insertTable VALUES(@MDFRowID,null,null,@VorodiBuchtIDCabinetInputID,9,@X6,@PortID ,1,@PostContactID ,@VorodiBuchtID ,null,0,7 ,null,999)
	   update Bucht set PCMPortID = @PortID where ID = (SELECT MAX(ID) from [dbo].[Bucht]);
	   end
	   update Bucht set BuchtIDConnectedOtherBucht = (select ID from @insertTable)  , Status = 13 , ConnectionID = @PostContactID where ID = @VorodiBuchtID
	   delete @insertTable;
	 ------

	 DECLARE @X7 int; set @X7 = CAST( (SUBSTRING(@pcmetesali , 1 , Len(@pcmetesali) - 1) + '7') as int);
	 set @switchPostID   = (select top(1) Telephone.SwitchPortID from #pcm join Telephone on Telephone.TelephoneNoIndividual = #pcm.TEL_NO where #pcm.TAB_2 =  @PcmTabaghe   and #pcm.RAD_2 = @pcmRadif and #pcm.ETE_2 = @X7 and Telephone.CenterID = @CenterID)		    	    

	 insert into PCMPort ([PCMID],[PortNumber],[PortType] ,[Status],[ElkaID]) VALUES (@PCMID , 1 , 8 , IIF(@switchPostID is null , 1  , 2) , null)
	 set @PortID = (SELECT MAX(ID) from [dbo].[PCMPort]);
	 
	 INSERT INTO [dbo].[PostContact]([PostID],[ConnectionNo],[ConnectionType],[FirsetCableColorID],[SecondCableColorID],[Status],[ElkaID]) VALUES(@PostID,@zoje,5,null ,null,IIF(@switchPostID is null , 5  , 1),null)
	 set @lastPostContactID = (SELECT MAX(ID) from [dbo].[PostContact]);
	 

	 if exists( select * from bucht as b join VerticalMDFRow as vr on vr.ID = b.MDFRowID
	                join VerticalMDFColumn as vc on vc.ID = vr.VerticalMDFColumnID
		            join MDFFrame as mf on mf.ID = vc.MDFFrameID
					join MDF as m on m.ID = mf.MDFID
				    where  vr.VerticalRowNo = @PcmTabaghe AND vc.VerticalCloumnNo = @pcmRadif   and b.BuchtNo = @X7 AND m.Description in  (  N'PCM') and m.CenterID = @CenterID )
      Begin
	  update b
	    set PCMPortID = @PortID , ConnectionID = @lastPostContactID , b.Status = IIF(@switchPostID is null , 7  , 1) , CabinetInputID = @VorodiBuchtIDCabinetInputID , SwitchPortID = @switchPostID , BuchtTypeID = 8
		 from bucht as b join VerticalMDFRow as vr on vr.ID = b.MDFRowID
	                join VerticalMDFColumn as vc on vc.ID = vr.VerticalMDFColumnID
		            join MDFFrame as mf on mf.ID = vc.MDFFrameID
					join MDF as m on m.ID = mf.MDFID
				    where  vr.VerticalRowNo = @PcmTabaghe AND vc.VerticalCloumnNo = @pcmRadif   and b.BuchtNo = @X7 AND m.Description in  (  N'PCM') and m.CenterID = @CenterID 

	   End
	   else
	   begin
	   INSERT INTO #Error VALUES(@CenterID,@shelfNumber,@PCMNumber,@PostNumber , null  , @RADIF , @TABAGHE , @ETESALE ,'pcm bucht' , @pcmRadif , @PcmTabaghe ,@X7)
	     set  @MDFRowID = null ;
	    set @MDFRowID = (select  top(1) b.MDFRowID from bucht as b join VerticalMDFRow as vr on vr.ID = b.MDFRowID
	                join VerticalMDFColumn as vc on vc.ID = vr.VerticalMDFColumnID
		            join MDFFrame as mf on mf.ID = vc.MDFFrameID
					join MDF as m on m.ID = mf.MDFID
				    where  vr.VerticalRowNo = @PcmTabaghe AND vc.VerticalCloumnNo = @pcmRadif  AND m.Description in  (  N'PCM') and m.CenterID = @CenterID)

						   if ( @MDFRowID is null)
	   begin
	   	   set @MDFRowID = (select  top(1) b.MDFRowID from bucht as b join VerticalMDFRow as vr on vr.ID = b.MDFRowID
	                join VerticalMDFColumn as vc on vc.ID = vr.VerticalMDFColumnID
		            join MDFFrame as mf on mf.ID = vc.MDFFrameID
					join MDF as m on m.ID = mf.MDFID
				    where   (m.CenterID = @CenterID) and (m.Description in  (  N'PCM')) order by  (b.MDFRowID) DESC )

					set @X7 = (select Max(BuchtNo)+1 from Bucht where Bucht.MDFRowID = @MDFRowID)
	    end


	   INSERT INTO [dbo].[Bucht]([MDFRowID],[SwitchPortID],[CablePairID],[CabinetInputID],[BuchtTypeID],[BuchtNo] ,[PCMPortID],[PortNo] ,[ConnectionID] ,[BuchtIDConnectedOtherBucht],[E1NumberID],[ADSLStatus],[Status],[ElkaID],[CabinetNumber])
                   VALUES(@MDFRowID,@switchPostID,null,@VorodiBuchtIDCabinetInputID,8,@X7,@PortID ,1,@lastPostContactID ,null ,null,0,IIF(@switchPostID is null , 7  , 1) ,null,999)
	   update Bucht set PCMPortID = @PortID where ID = (SELECT MAX(ID) from [dbo].[Bucht]);
	   end
	 ------
	 DECLARE @X8 int; set @X8 = CAST( (SUBSTRING(@pcmetesali , 1 , Len(@pcmetesali) - 1) + '8') as int);
	 set @switchPostID   = (select top(1) Telephone.SwitchPortID from #pcm join Telephone on Telephone.TelephoneNoIndividual = #pcm.TEL_NO where #pcm.TAB_2 =  @PcmTabaghe   and #pcm.RAD_2 = @pcmRadif and #pcm.ETE_2 = @X8 and Telephone.CenterID = @CenterID)		    

	 insert into PCMPort ([PCMID],[PortNumber],[PortType] ,[Status],[ElkaID]) VALUES (@PCMID , 2 , 8 , IIF(@switchPostID is null , 1  , 2) , null)
	 set @PortID = (SELECT MAX(ID) from [dbo].[PCMPort]);
	 
	 INSERT INTO [dbo].[PostContact]([PostID],[ConnectionNo],[ConnectionType],[FirsetCableColorID],[SecondCableColorID],[Status],[ElkaID]) VALUES(@PostID,@zoje,5,null ,null,IIF(@switchPostID is null , 5  , 1),null)
	 set @lastPostContactID = (SELECT MAX(ID) from [dbo].[PostContact]);
	 

	 if exists( select * from bucht as b join VerticalMDFRow as vr on vr.ID = b.MDFRowID
	                join VerticalMDFColumn as vc on vc.ID = vr.VerticalMDFColumnID
		            join MDFFrame as mf on mf.ID = vc.MDFFrameID
					join MDF as m on m.ID = mf.MDFID
				    where  vr.VerticalRowNo = @PcmTabaghe AND vc.VerticalCloumnNo = @pcmRadif   and b.BuchtNo = @X8 AND m.Description in  (  N'PCM') and m.CenterID = @CenterID )
      Begin
	  update b
	    set PCMPortID = @PortID , ConnectionID = @lastPostContactID , b.Status = IIF(@switchPostID is null , 7  , 1) , CabinetInputID = @VorodiBuchtIDCabinetInputID , SwitchPortID = @switchPostID , BuchtTypeID = 8
		 from bucht as b join VerticalMDFRow as vr on vr.ID = b.MDFRowID
	                join VerticalMDFColumn as vc on vc.ID = vr.VerticalMDFColumnID
		            join MDFFrame as mf on mf.ID = vc.MDFFrameID
					join MDF as m on m.ID = mf.MDFID
				    where  vr.VerticalRowNo = @PcmTabaghe AND vc.VerticalCloumnNo = @pcmRadif   and b.BuchtNo = @X8 AND m.Description in  (  N'PCM') and m.CenterID = @CenterID 

	   End
	   else
	   begin
	   INSERT INTO #Error VALUES(@CenterID,@shelfNumber,@PCMNumber,@PostNumber , null  , @RADIF , @TABAGHE , @ETESALE ,'pcm bucht' , @pcmRadif , @PcmTabaghe ,@X8)
	   set  @MDFRowID = null ;
	    set @MDFRowID = (select  top(1) b.MDFRowID from bucht as b join VerticalMDFRow as vr on vr.ID = b.MDFRowID
	                join VerticalMDFColumn as vc on vc.ID = vr.VerticalMDFColumnID
		            join MDFFrame as mf on mf.ID = vc.MDFFrameID
					join MDF as m on m.ID = mf.MDFID
				    where  vr.VerticalRowNo =  @PcmTabaghe AND vc.VerticalCloumnNo = @pcmRadif     AND m.Description in  (  N'PCM') and m.CenterID = @CenterID)

						   if ( @MDFRowID is null)
	   begin
	   	   set @MDFRowID = (select  top(1) b.MDFRowID from bucht as b join VerticalMDFRow as vr on vr.ID = b.MDFRowID
	                join VerticalMDFColumn as vc on vc.ID = vr.VerticalMDFColumnID
		            join MDFFrame as mf on mf.ID = vc.MDFFrameID
					join MDF as m on m.ID = mf.MDFID
				    where   (m.CenterID = @CenterID) and (m.Description in  (  N'PCM')) order by  (b.MDFRowID) DESC )

					set @X8 = (select Max(BuchtNo)+1 from Bucht where Bucht.MDFRowID = @MDFRowID)
	    end


	   INSERT INTO [dbo].[Bucht]([MDFRowID],[SwitchPortID],[CablePairID],[CabinetInputID],[BuchtTypeID],[BuchtNo] ,[PCMPortID],[PortNo] ,[ConnectionID] ,[BuchtIDConnectedOtherBucht],[E1NumberID],[ADSLStatus],[Status],[ElkaID],[CabinetNumber])
                   VALUES(@MDFRowID,@switchPostID,null,@VorodiBuchtIDCabinetInputID,8,@X8,@PortID ,1,@lastPostContactID ,null,null,0,IIF(@switchPostID is null , 7  , 1),null,999)
	   update Bucht set PCMPortID = @PortID where ID = (SELECT MAX(ID) from [dbo].[Bucht]);
	   end

	 ------

	 DECLARE @X9 int; set @X9 = CAST( (SUBSTRING(@pcmetesali , 1 , Len(@pcmetesali) - 1) + '9') as int);
	  set @switchPostID   = (select top(1) Telephone.SwitchPortID from #pcm join Telephone on Telephone.TelephoneNoIndividual = #pcm.TEL_NO where #pcm.TAB_2 =  @PcmTabaghe   and #pcm.RAD_2 = @pcmRadif and #pcm.ETE_2 = @X9 and Telephone.CenterID = @CenterID)		    

	 insert into PCMPort ([PCMID],[PortNumber],[PortType] ,[Status],[ElkaID]) VALUES (@PCMID , 3 , 8 , IIF(@switchPostID is null , 1  , 2) , null)
	 set @PortID = (SELECT MAX(ID) from [dbo].[PCMPort]);
	 
	  INSERT INTO [dbo].[PostContact]([PostID],[ConnectionNo],[ConnectionType],[FirsetCableColorID],[SecondCableColorID],[Status],[ElkaID]) VALUES(@PostID,@zoje,5,null ,null,IIF(@switchPostID is null , 5  , 1),null)
	  set @lastPostContactID = (SELECT MAX(ID) from [dbo].[PostContact]);
	 

	 if exists( select * from bucht as b join VerticalMDFRow as vr on vr.ID = b.MDFRowID
	                join VerticalMDFColumn as vc on vc.ID = vr.VerticalMDFColumnID
		            join MDFFrame as mf on mf.ID = vc.MDFFrameID
					join MDF as m on m.ID = mf.MDFID
				    where  vr.VerticalRowNo = @PcmTabaghe AND vc.VerticalCloumnNo = @pcmRadif   and b.BuchtNo = @X9 AND m.Description in  (  N'PCM') and m.CenterID = @CenterID )
      Begin
	  update b
	    set PCMPortID = @PortID , SwitchPortID = @switchPostID , ConnectionID = @lastPostContactID , b.Status = IIF(@switchPostID is null , 7  , 1) , CabinetInputID = @VorodiBuchtIDCabinetInputID  ,BuchtTypeID = 8
		 from bucht as b join VerticalMDFRow as vr on vr.ID = b.MDFRowID
	                join VerticalMDFColumn as vc on vc.ID = vr.VerticalMDFColumnID
		            join MDFFrame as mf on mf.ID = vc.MDFFrameID
					join MDF as m on m.ID = mf.MDFID
				    where  vr.VerticalRowNo = @PcmTabaghe AND vc.VerticalCloumnNo = @pcmRadif   and b.BuchtNo = @X9 AND m.Description in  (  N'PCM') and m.CenterID = @CenterID 

	   End
	   else
	   begin
	   INSERT INTO #Error VALUES(@CenterID,@shelfNumber,@PCMNumber,@PostNumber , null  , @RADIF , @TABAGHE , @ETESALE ,'pcm bucht' , @pcmRadif , @PcmTabaghe ,@X8)
	   set  @MDFRowID = null ;
	    set @MDFRowID = (select top(1) b.MDFRowID from bucht as b join VerticalMDFRow as vr on vr.ID = b.MDFRowID
	                join VerticalMDFColumn as vc on vc.ID = vr.VerticalMDFColumnID
		            join MDFFrame as mf on mf.ID = vc.MDFFrameID
					join MDF as m on m.ID = mf.MDFID
				    where  vr.VerticalRowNo =  @PcmTabaghe AND vc.VerticalCloumnNo = @pcmRadif     AND m.Description in  (  N'PCM') and m.CenterID = @CenterID)

	if ( @MDFRowID is null)
	   begin
	   	   set @MDFRowID = (select  top(1) b.MDFRowID from bucht as b join VerticalMDFRow as vr on vr.ID = b.MDFRowID
	                join VerticalMDFColumn as vc on vc.ID = vr.VerticalMDFColumnID
		            join MDFFrame as mf on mf.ID = vc.MDFFrameID
					join MDF as m on m.ID = mf.MDFID
				    where   (m.CenterID = @CenterID) and (m.Description in  (  N'PCM')) order by  (b.MDFRowID) DESC )
					set @X9 = (select Max(BuchtNo)+1 from Bucht where Bucht.MDFRowID = @MDFRowID)
	    end

	   INSERT INTO [dbo].[Bucht]([MDFRowID],[SwitchPortID],[CablePairID],[CabinetInputID],[BuchtTypeID],[BuchtNo] ,[PCMPortID],[PortNo] ,[ConnectionID] ,[BuchtIDConnectedOtherBucht],[E1NumberID],[ADSLStatus],[Status],[ElkaID],[CabinetNumber])
                   VALUES(@MDFRowID,@switchPostID,null,@VorodiBuchtIDCabinetInputID,8,@X9,@PortID ,1,@lastPostContactID ,null ,null,0,IIF(@switchPostID is null , 7  , 1) ,null,999)
	   update Bucht set PCMPortID = @PortID where ID = (SELECT MAX(ID) from [dbo].[Bucht]);
	   end

	 ------
	 DECLARE @X10 int; set @X10 = CAST( ((SUBSTRING(@pcmetesali , 1 , Len(@pcmetesali) - 1)) + '0') as int);
	 set @X10 = @X10 + 10
	 set @switchPostID   = (select top(1) Telephone.SwitchPortID from #pcm join Telephone on Telephone.TelephoneNoIndividual = #pcm.TEL_NO where #pcm.TAB_2 =  @PcmTabaghe   and #pcm.RAD_2 = @pcmRadif and #pcm.ETE_2 = @X10 and Telephone.CenterID = @CenterID)		    

	 insert into PCMPort ([PCMID],[PortNumber],[PortType] ,[Status],[ElkaID]) VALUES (@PCMID , 4 , 8 , IIF(@switchPostID is null , 1  , 2) , null)
	 set @PortID = (SELECT MAX(ID) from [dbo].[PCMPort]);
	 
	 INSERT INTO [dbo].[PostContact]([PostID],[ConnectionNo],[ConnectionType],[FirsetCableColorID],[SecondCableColorID],[Status],[ElkaID]) VALUES(@PostID,@zoje,5,null ,null,IIF(@switchPostID is null , 5  , 1),null)
	 set @lastPostContactID = (SELECT MAX(ID) from [dbo].[PostContact]);
	  

	 if exists( select * from bucht as b join VerticalMDFRow as vr on vr.ID = b.MDFRowID
	                join VerticalMDFColumn as vc on vc.ID = vr.VerticalMDFColumnID
		            join MDFFrame as mf on mf.ID = vc.MDFFrameID
					join MDF as m on m.ID = mf.MDFID
				    where  vr.VerticalRowNo = @PcmTabaghe AND vc.VerticalCloumnNo = @pcmRadif   and b.BuchtNo = @X10 AND m.Description in  (  N'PCM') and m.CenterID = @CenterID )
      Begin
	  update b
	    set PCMPortID = @PortID , ConnectionID = @lastPostContactID , b.Status = IIF(@switchPostID is null , 7  , 1) , CabinetInputID = @VorodiBuchtIDCabinetInputID , SwitchPortID = @switchPostID , BuchtTypeID = 8
		 from bucht as b join VerticalMDFRow as vr on vr.ID = b.MDFRowID
	                join VerticalMDFColumn as vc on vc.ID = vr.VerticalMDFColumnID
		            join MDFFrame as mf on mf.ID = vc.MDFFrameID
					join MDF as m on m.ID = mf.MDFID
				    where  vr.VerticalRowNo = @PcmTabaghe AND vc.VerticalCloumnNo = @pcmRadif   and b.BuchtNo = @X10 AND m.Description in  (  N'PCM') and m.CenterID = @CenterID

	   End
	   else
	   begin
	   INSERT INTO #Error VALUES(@CenterID,@shelfNumber,@PCMNumber,@PostNumber , null  , @RADIF , @TABAGHE , @ETESALE ,'pcm bucht' , @pcmRadif , @PcmTabaghe ,@X10)
	   set  @MDFRowID = null ;
	   set @MDFRowID = (select  top(1) b.MDFRowID from bucht as b join VerticalMDFRow as vr on vr.ID = b.MDFRowID
	                join VerticalMDFColumn as vc on vc.ID = vr.VerticalMDFColumnID
		            join MDFFrame as mf on mf.ID = vc.MDFFrameID
					join MDF as m on m.ID = mf.MDFID
				    where  vr.VerticalRowNo =  @PcmTabaghe AND vc.VerticalCloumnNo = @pcmRadif     AND m.Description in  (  N'PCM') and m.CenterID = @CenterID)

	   if ( @MDFRowID is null)
	   begin
	   	   set @MDFRowID = (select  top(1) b.MDFRowID from bucht as b join VerticalMDFRow as vr on vr.ID = b.MDFRowID
	                join VerticalMDFColumn as vc on vc.ID = vr.VerticalMDFColumnID
		            join MDFFrame as mf on mf.ID = vc.MDFFrameID
					join MDF as m on m.ID = mf.MDFID
				    where   (m.CenterID = @CenterID) and (m.Description in  (  N'PCM')) order by  (b.MDFRowID) DESC )
					set @X10 = (select Max(BuchtNo)+1 from Bucht where Bucht.MDFRowID = @MDFRowID)
	    end


	   INSERT INTO [dbo].[Bucht]([MDFRowID],[SwitchPortID],[CablePairID],[CabinetInputID],[BuchtTypeID],[BuchtNo] ,[PCMPortID],[PortNo] ,[ConnectionID] ,[BuchtIDConnectedOtherBucht],[E1NumberID],[ADSLStatus],[Status],[ElkaID],[CabinetNumber])
                   VALUES(@MDFRowID,@switchPostID,null,@VorodiBuchtIDCabinetInputID,8,@X10,@PortID ,1,@lastPostContactID ,null ,null,0,IIF(@switchPostID is null , 7  , 1),null,999)
	   update Bucht set PCMPortID = @PortID where ID = (SELECT MAX(ID) from [dbo].[Bucht]);
	   end
	 ------
	 END

	 Cont: fetch next from pcm_cursor into @RADIF ,@TABAGHE , 	@ETESALE
	end
	close pcm_cursor;
	deallocate pcm_cursor;

		select * from #Error

--COMMIT TRAN -- Transaction Success!
--END TRY
--BEGIN CATCH
--    IF @@TRANCOUNT > 0
--        ROLLBACK TRAN --RollBack in case of Error
--END CATCH


