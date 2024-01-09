USE Salas
GO

--with cte as 
--(
--SELECT [ID]
--,[ID_SUBSCRI]
--      ,[ID_FINANCE]
--      ,[TEL]
--      ,[TEL_PISH]
--      ,[DC_DATE]
--      ,[C_DATE]
--      ,[DC_RADIF]
--      ,[C_RADIF]
--      ,[ID_DCTYPE]
--      ,[newtelephone]
--	  ,row_number ()over (partition by ID_SUBSCRI order by ID_FINANCE  ) as ro
--  FROM [Salas].[dbo].[DCTABLE]
--)
--delete  cte where ro>1


--update [Salas].[dbo].[DCTABLE] set newtelephone=s.newtelephone
--  FROM [Salas].[dbo].[DCTABLE] as z
--  join [Salas].[dbo].[SUBSCRIB] as s
--  on z.ID_FINANCE=s.ID_FINANCE

declare @CITY int = 7;

delete [CRM].dbo.RequestLog from [CRM].dbo.RequestLog as Rl join [CRM].dbo.Request as R on RL.RequestID = R.ID INNER JOIN
                     [CRM].dbo.Center ON R.CenterID = Center.ID INNER JOIN
                     [CRM].dbo.Region ON Center.RegionID = Region.ID INNER JOIN
                     [CRM].dbo.City ON Region.CityID = City.ID
					 where  City.ID = @CITY and R.RequestTypeID = 30  and CreatorUserID is null

delete [CRM].dbo.CutAndEstablish from [CRM].dbo.CutAndEstablish as CE join [CRM].dbo.Request as R on CE.ID = R.ID INNER JOIN
                     [CRM].dbo.Center ON R.CenterID = Center.ID INNER JOIN
                     [CRM].dbo.Region ON Center.RegionID = Region.ID INNER JOIN
                     [CRM].dbo.City ON Region.CityID = City.ID
					 where  City.ID = @CITY and R.RequestTypeID = 30 and CreatorUserID is null

delete [CRM].dbo.RequestPayment from [CRM].dbo.RequestPayment as RP join [CRM].dbo.Request as R on RP.RequestID = R.ID INNER JOIN
                     [CRM].dbo.Center ON R.CenterID = Center.ID INNER JOIN
                     [CRM].dbo.Region ON Center.RegionID = Region.ID INNER JOIN
                     [CRM].dbo.City ON Region.CityID = City.ID
					 where  City.ID = @CITY and R.RequestTypeID = 30 and CreatorUserID is null

delete [CRM].dbo.Request From [CRM].dbo.Request as R INNER JOIN
                     [CRM].dbo.Center ON R.CenterID = Center.ID INNER JOIN
                     [CRM].dbo.Region ON Center.RegionID = Region.ID INNER JOIN
                     [CRM].dbo.City ON Region.CityID = City.ID
					 where  City.ID = @CITY and R.RequestTypeID = 30 and CreatorUserID is null

declare @ID float;
declare @ID_FINANCE float;
declare	@Telephone  bigint;
declare @DC_TYPE int;
declare @ID_DCTYPE int;

declare @S_START_DATE  nvarchar(8)
declare @D_DC_DATE  nvarchar(8)

declare @S_STOP_DATE nvarchar(max);

declare @STOP_DATE smalldatetime;
declare @DC_DATE smalldatetime;
declare @reqeustID bigint;
declare @CenterID bigint;
declare @CenterCode bigint; 
declare @CustomerID bigint;
declare @ID_SUBSCRI bigint;


declare @ID_S nvarchar(max); 
declare @p_ID_DATE nvarchar(max);
declare @Pay_DATE smalldatetime;
DECLARE @CountEX int ; 


IF OBJECT_ID('tempdb..#subscrib') IS NOT NULL
DROP TABLE #subscrib

IF OBJECT_ID('tempdb..#EXPENSE') IS NOT NULL
DROP TABLE #EXPENSE

CREATE TABLE #EXPENSE
(
	[ID] [float] NULL,
	[ID_SUBSCRI] [float] NULL,
	[TEL] [nvarchar](4) NULL,
	[TEL_PISH] [nvarchar](6) NULL,
	[ID_EXPTYPE] [float] NULL,
	[PAY_MALI] [float] NULL,
	[PAY_DATE] [nvarchar](8) NULL,
	[ERSAL_DATE] [nvarchar](8) NULL,
	[ID_FINANCE] [float] NULL,
	[PAY_PERCEN] [float] NULL,
	[ID_BARSI] [float] NULL,
	[SAL] [nvarchar](4) NULL,
	[ENTID] [float] NULL,
	[ID_MARKAZ] [float] NULL,
	[ID_FOLD] [float] NULL,
	[ROW] int
)

declare subscrib_cursor cursor read_only for select ID , ID_SUBSCRI ,  ID_FINANCE , DC_DATE , ID_DCTYPE , NewTelephone from Salas.dbo.DCTABLE

OPEN subscrib_cursor;
   fetch next from subscrib_cursor into @ID ,@ID_SUBSCRI, @ID_FINANCE ,	@D_DC_DATE , @ID_DCTYPE , @Telephone
while @@FETCH_STATUS = 0
	Begin
	BEGIN TRY

	set @reqeustID = CAST((CAST(STUFF( @D_DC_DATE, 1, 2, '') AS nvarchar(max))+ CAST(@ID_SUBSCRI as nvarchar(max) ) + '30') as bigint)
	set @CenterID = (select Top(1) [CRM].dbo.Center.ID from FIMARK join [CRM].dbo.Center on FIMARK.ID_MARKAZ = [CRM].dbo.Center.CenterCode where FIMARK.ID_FINANCE = @ID_FINANCE)
	set @CenterCode = (SELECT [CRM].dbo.City.Code  FROM [CRM].dbo.Center INNER JOIN [CRM].dbo.Region ON [CRM].dbo.Center.RegionID = [CRM].dbo.Region.ID INNER JOIN [CRM].dbo.City ON [CRM].dbo.Region.CityID = [CRM].dbo.City.ID WHERE [CRM].dbo.Center.ID = @CenterID)
	set @CustomerID = (select top(1) ID from [CRM].dbo.Customer as C where C.ElkaID = @ID_FINANCE and kercity = @CITY and C.KerStopDate = '99999999')
	
	BEGIN TRY
	set @DC_DATE   = ([CRM].[dbo].[sh2mi](STUFF(STUFF(@D_DC_DATE , 5 ,0,'/') , 8 , 0 ,'/')));
	END TRY
    BEGIN CATCH
    END CATCH
	if  @DC_DATE is null begin set @DC_DATE = CAST('1900-01-01' as smalldatetime) end


    INSERT INTO [CRM].[dbo].[Request] VALUES(
 @reqeustID , --[ID]
@Telephone,
 null,
 null,
 30,
 @CenterID,
 @CustomerID,
 @DC_DATE,
 null, --[RequestLetterNo] ,
 null, --[RequestLetterDate],
 null, --[RequesterName],
 null, --[RequestPaymentTypeID] ,
 null, --[RepresentitiveNo],
 null, --[RepresentitiveDate],
 null,  --[RepresentitiveExpireDate],
 164,
 @DC_DATE,
 null, --[ModifyDate] ,
 @DC_DATE,
 null, --[CreatorUserID] 
 null, --[ModifyUserID] ,
 null, --[SellerID] ,
 null, --[PreviousAction] ,
 null, --[BillID],
 null, --[PaymentID] ,
 0, --[IsWaitingList],
 0, --[IsCancelation] ,
 0, --[IsViewed] ,
 0, --[IsVisible],
 0 --[WaitForToBeCalculate]
 );

    INSERT INTO [CRM].[dbo].[CutAndEstablish]
     VALUES
           (@reqeustID --<ID, bigint,>
           ,(select ID from [CRM].dbo.CauseOfCut as CC where CC.KerID = @ID_DCTYPE) --<CutType, int,>
           ,null --<FileNo, nvarchar(20),>
           ,null --<Amount, bigint,>
           ,null --<NoticeID, bigint,>
           ,null --<CutRequesterRef, nvarchar(50),>
           ,null --<EstablishRequesterRef, nvarchar(50),>
           ,null --<CutDurationDays, bigint,>
           ,null --<AutomaticallyEstablish, tinyint,>
           ,null --<EstablishWithOrder, bit,>
           ,null --<CutReportID, bigint,>
           ,null --<EstablishReportID, bigint,>
           ,@DC_DATE --<ActionCutDueDate, smalldatetime,>
           ,null --<ActionEstablishDueDate, smalldatetime,>
           ,@DC_DATE --<CutDate, datetime,>
           ,null --<EstablishDate, datetime,>
           ,null --<CutListingDate, smalldatetime,>
           ,null --<EstablishListingDate, smalldatetime,>
           ,null --<CutComment, nvarchar(max),>
           ,null --<EstablishComment, nvarchar(max),>
           ,null --<UnableCutStatusComment, nvarchar(500),>
           ,null --<UnableEstablishStatusComment, nvarchar(500),>
           ,null --<DebtAbonman, bigint,>
           ,null --<DebtADeposit, bigint,>
           ,null --<Counter, nvarchar(500),>
           ,30 --<Status, tinyint,>
           ,null --<WiringIllegal, nvarchar(max),>
           ,null --<Vacate, bit,>
		   ,@DC_DATE
		   )

    DECLARE @xml XML
    SELECT @xml = '<?xml version="1.0" encoding="UTF-8"?>
      <CutAndEstablish xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
      <TelephoneNo>' +cast( @Telephone as varchar) + '</TelephoneNo>
      <Status>30</Status>
      </CutAndEstablish>'


    INSERT INTO [CRM].[dbo].[RequestLog] VALUES
           (@reqeustID
           ,30
           ,null
           ,0
           , @Telephone
           ,null
           ,(select CustomerID from CRM.dbo.Customer where ID = @CustomerID)
           ,null
           ,@DC_DATE
           ,@xml)

	update [CRM].dbo.Telephone set Status = 3 , CutDate = @DC_DATE , CauseOfCutID = (select ID from [CRM].dbo.CauseOfCut as CC where CC.KerID = @ID_DCTYPE) where TelephoneNo = @Telephone
	


    -- request payment

	delete #EXPENSE
	insert into #EXPENSE select * , ROW_NUMBER() OVER(ORDER BY ID ASC) AS Row from EXPENSE where ID_SUBSCRI = @ID_SUBSCRI and (ID_EXPTYPE = 13)

	set @CountEX = (select Count(*) from #EXPENSE)
	DECLARE @iEX int = 1
    WHILE @iEX <=  @CountEX
    BEGIN

	select @p_ID_DATE = E1.PAY_DATE  , @ID_S = E1.ID from #EXPENSE as E1 where E1.[Row] = @iEX

	BEGIN TRY
	set @Pay_DATE =( [CRM].[dbo].[sh2mi](STUFF(STUFF(@p_ID_DATE , 5 ,0,'/') , 8 , 0 ,'/')));
	END TRY
    BEGIN CATCH
    END CATCH
	if(@Pay_DATE is null) begin set @Pay_DATE = CAST('1900-01-01' as smalldatetime) end 

	if exists(select *  from EXPENSE as E1 join MALI on E1.ID_EXPTYPE = MALI.ID_EXPTYPE 
		                 join Salas.dbo.EXPTYPE on EXPTYPE.ID = E1.ID_EXPTYPE
		                 where E1.ID = @ID_S  and E1.PAY_DATE <= MALI.STOP_DATE and E1.PAY_DATE >= MALI.START_DATE and EXPTYPE.ID in(13))
	BEGIN

	INSERT INTO [CRM].[dbo].[RequestPayment]
           ([RelatedRequestPaymentID]
           ,[RequestID]
           ,[BaseCostID]
           ,[OtherCostID]
           ,[PaymentWay]
           ,[BankID]
           ,[BankBranchID]
           ,[PaymentType]
           ,[Cost]
           ,[Abonman]
           ,[Tax]
           ,[AmountSum]
           ,[PaymentFicheID]
           ,[FicheNunmber]
           ,[FicheDate]
           ,[RecieverPostOfficeCode]
           ,[RecieverPostOfficeRecordNo]
           ,[PaymentDate]
           ,[IsPaid]
           ,[IsAccepted]
           ,[InsertDate]
           ,[AccountNo]
           ,[IsKickedBack]
           ,[BillID]
           ,[PaymentID]
           ,[OrderID]
           ,[DocumentsFileID]
           ,[UserID]
           ,[FactorNumber])
		    select 
		    null -- <RelatedRequestPaymentID, bigint,>
           ,@reqeustID --<RequestID, bigint,>
           ,B.ID       --<BaseCostID, int,>
           ,null --<OtherCostID, int,>
           ,null --<PaymentWay, tinyint,>
           ,null --<BankID, int,>
           ,null --<BankBranchID, int,>
           ,1    --<PaymentType, tinyint,>
           ,B.Cost --<Cost, bigint,>
           ,null --<Abonman, int,>
           ,null --<Tax, int,>
           ,E1.PAY_MALI  --<AmountSum, bigint,>
           ,null --<PaymentFicheID, bigint,>
           ,null --<FicheNunmber, nvarchar(20),>
           ,null --<FicheDate, smalldatetime,>
           ,null --<RecieverPostOfficeCode, nvarchar(50),>
           ,null --<RecieverPostOfficeRecordNo, nvarchar(50),>
           ,@Pay_DATE --<PaymentDate, smalldatetime,>
           ,1 --<IsPaid, bit,>
           ,null --<IsAccepted, bit,>
           ,@Pay_DATE --<InsertDate, smalldatetime,>
           ,null --<AccountNo, char(20),>
           ,0 --<IsKickedBack, bit,>
           ,null --<BillID, nvarchar(50),>
           ,null --<PaymentID, nvarchar(50),>
           ,null --<OrderID, nvarchar(50),>
           ,null --<DocumentsFileID, uniqueidentifier,>
           ,null --<UserID, int,>
           ,null --<FactorNumber, int,>
		   from EXPENSE as E1 join MALI on E1.ID_EXPTYPE = MALI.ID_EXPTYPE 
		   join Salas.dbo.EXPTYPE on EXPTYPE.ID = E1.ID_EXPTYPE
		   join CRM.dbo.BaseCost as B on B.KerID = MALI.ID
		   where E1.ID = @ID_S and E1.PAY_DATE <= MALI.STOP_DATE and E1.PAY_DATE >= MALI.[START_DATE] and EXPTYPE.ID in(13)

	END
	else
	BEGIN
	INSERT INTO [CRM].[dbo].[RequestPayment]
           ([RelatedRequestPaymentID]
           ,[RequestID]
           ,[BaseCostID]
           ,[OtherCostID]
           ,[PaymentWay]
           ,[BankID]
           ,[BankBranchID]
           ,[PaymentType]
           ,[Cost]
           ,[Abonman]
           ,[Tax]
           ,[AmountSum]
           ,[PaymentFicheID]
           ,[FicheNunmber]
           ,[FicheDate]
           ,[RecieverPostOfficeCode]
           ,[RecieverPostOfficeRecordNo]
           ,[PaymentDate]
           ,[IsPaid]
           ,[IsAccepted]
           ,[InsertDate]
           ,[AccountNo]
           ,[IsKickedBack]
           ,[BillID]
           ,[PaymentID]
           ,[OrderID]
           ,[DocumentsFileID]
           ,[UserID]
           ,[FactorNumber])
		    select 
		    null -- <RelatedRequestPaymentID, bigint,>
           ,@reqeustID --<RequestID, bigint,>
           ,B.ID       --<BaseCostID, int,>
           ,null --<OtherCostID, int,>
           ,null --<PaymentWay, tinyint,>
           ,null --<BankID, int,>
           ,null --<BankBranchID, int,>
           ,1    --<PaymentType, tinyint,>
           ,B.Cost --<Cost, bigint,>
           ,null --<Abonman, int,>
           ,null --<Tax, int,>
           ,E1.PAY_MALI  --<AmountSum, bigint,>
           ,null --<PaymentFicheID, bigint,>
           ,null --<FicheNunmber, nvarchar(20),>
           ,null --<FicheDate, smalldatetime,>
           ,null --<RecieverPostOfficeCode, nvarchar(50),>
           ,null --<RecieverPostOfficeRecordNo, nvarchar(50),>
           ,@Pay_DATE --<PaymentDate, smalldatetime,>
           ,1 --<IsPaid, bit,>
           ,null --<IsAccepted, bit,>
           ,@Pay_DATE --<InsertDate, smalldatetime,>
           ,null --<AccountNo, char(20),>
           ,null --<IsKickedBack, bit,>
           ,null --<BillID, nvarchar(50),>
           ,null --<PaymentID, nvarchar(50),>
           ,null --<OrderID, nvarchar(50),>
           ,null --<DocumentsFileID, uniqueidentifier,>
           ,null --<UserID, int,>
           ,null --<FactorNumber, int,>
		   from EXPENSE as E1 
		   join Salas.dbo.EXPTYPE on EXPTYPE.ID = E1.ID_EXPTYPE
		   join CRM.dbo.BaseCost as B on EXPTYPE.NAME = B.Title
		   where E1.ID = @ID_S and B.KerID = -1 and EXPTYPE.ID in(13)
    END


	 SET @iEX = @iEX + 1
     END

	 -- request payment



	END TRY
    BEGIN CATCH
	select  @Telephone
	 select ERROR_MESSAGE()
	--insert into Salas.dbo.requestLog values(@reqeustID , (select ERROR_MESSAGE()));
    END CATCH

	fetch next from subscrib_cursor into  @ID ,@ID_SUBSCRI, @ID_FINANCE ,	@D_DC_DATE , @ID_DCTYPE , @Telephone
	end
	close subscrib_cursor;
	deallocate subscrib_cursor;


	update T set CutDate = (select top(1) r.InsertDate from CRM.dbo.Request as r where r.TelephoneNo = T.TelephoneNo and r.RequestTypeID = 30 order by r.InsertDate desc) from CRM.dbo.Telephone as T


