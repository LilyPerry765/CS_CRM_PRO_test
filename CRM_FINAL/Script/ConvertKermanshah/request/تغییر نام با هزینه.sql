
USE Salas
GO


declare @CITY int = 7;

delete [CRM].dbo.RequestLog from [CRM].dbo.RequestLog as Rl join [CRM].dbo.Request as R on RL.RequestID = R.ID INNER JOIN
                     [CRM].dbo.Center ON R.CenterID = Center.ID INNER JOIN
                     [CRM].dbo.Region ON Center.RegionID = Region.ID INNER JOIN
                     [CRM].dbo.City ON Region.CityID = City.ID
					 where  City.ID = @CITY and R.RequestTypeID = 28 and CreatorUserID is null

delete [CRM].dbo.ChangeName  from [CRM].dbo.ChangeName  as CE join [CRM].dbo.Request as R on CE.ID = R.ID INNER JOIN
                     [CRM].dbo.Center ON R.CenterID = Center.ID INNER JOIN
                     [CRM].dbo.Region ON Center.RegionID = Region.ID INNER JOIN
                     [CRM].dbo.City ON Region.CityID = City.ID
					 where  City.ID = @CITY and R.RequestTypeID = 28 and CreatorUserID is null

delete [CRM].dbo.RequestPayment from [CRM].dbo.RequestPayment as RP join [CRM].dbo.Request as R on RP.RequestID = R.ID INNER JOIN
                     [CRM].dbo.Center ON R.CenterID = Center.ID INNER JOIN
                     [CRM].dbo.Region ON Center.RegionID = Region.ID INNER JOIN
                     [CRM].dbo.City ON Region.CityID = City.ID
					 where  City.ID = @CITY and R.RequestTypeID = 28 and CreatorUserID is null

delete [CRM].dbo.Request From [CRM].dbo.Request as R INNER JOIN
                     [CRM].dbo.Center ON R.CenterID = Center.ID INNER JOIN
                     [CRM].dbo.Region ON Center.RegionID = Region.ID INNER JOIN
                     [CRM].dbo.City ON Region.CityID = City.ID
					 where  City.ID = @CITY and R.RequestTypeID = 28 and CreatorUserID is null


declare @ID float
declare @ID_FINANCE bigint
declare	@Telephone  bigint
declare @S_START_DATE  nvarchar(8)
DECLARE @CountEX int ;
DECLARE @iEX int = 1 ;
declare @ID_S nvarchar(max); 
declare @p_ID_DATE nvarchar(max);
declare @reqeustID bigint;
declare @S_STOP_DATE nvarchar(max);
declare @oldCustomerID bigint ;
declare @newCustomerID bigint ;
declare @CenterID bigint ;  
declare @CenterCode bigint ;
DECLARE @Count int ; 
DECLARE @i int = 1

IF OBJECT_ID('tempdb..#Customer') IS NOT NULL
DROP TABLE #Customer

CREATE TABLE #Customer(
	[ID] [bigint]  NOT NULL,
	[CustomerID] [char](15) NULL,
	[PersonType] [tinyint] NOT NULL,
	[NationalID] [nvarchar](50) NULL,
	[NationalCodeOrRecordNo] [nvarchar](50) NULL,
	[FirstNameOrTitle] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NULL,
	[FatherName] [nvarchar](50) NULL,
	[Gender] [tinyint] NULL,
	[BirthCertificateID] [nvarchar](50) NULL,
	[BirthDateOrRecordDate] [smalldatetime] NULL,
	[IssuePlace] [nvarchar](50) NULL,
	[UrgentTelNo] [nvarchar](20) NULL,
	[MobileNo] [nvarchar](20) NULL,
	[Email] [nvarchar](100) NULL,
	[Agency] [nvarchar](50) NULL,
	[AgencyNumber] [nvarchar](50) NULL,
	[InsertDate] [smalldatetime] NOT NULL,
	[ElkaID] [bigint] NULL,
	[UserName] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[KerStopDate] [nvarchar](50) NULL,
	[KerStartDate] [nvarchar](50) NULL,
	[KerID] [bigint] NULL,
	[kercity] [nchar](10) NULL,
	[ChangeDate] [smalldatetime] NULL,
	[AddressID] [bigint] NULL,
	[Fax] [nvarchar](40) NULL,
	[Row] [int] NULL)

IF OBJECT_ID('tempdb..#EXPENSE') IS NOT NULL
DROP TABLE #EXPENSE

CREATE TABLE #EXPENSE
(
	[ID] [nvarchar](10) NULL,
	[ID_SUBSCRI] [nvarchar](10) NULL,
	[TEL] [nvarchar](4) NULL,
	[TEL_PISH] [nvarchar](6) NULL,
	[ID_EXPTYPE] [nvarchar](10) NULL,
	[PAY_MALI] [nvarchar](10) NULL,
	[PAY_DATE] [nvarchar](8) NULL,
	[ERSAL_DATE] [nvarchar](8) NULL,
	[ID_FINANCE] [nvarchar](10) NULL,
	[PAY_PERCEN] [nvarchar](10) NULL,
	[ID_BARSI] [nvarchar](10) NULL,
	[SAL] [nvarchar](4) NULL,
	[ENTID] [nvarchar](10) NULL,
	[ID_MARKAZ] [nvarchar](10) NULL,
	[ID_FOLD] [nvarchar](10) NULL,
	[ROW] int
)


declare subscrib_cursor cursor read_only for select ElkaID from [CRM].dbo.Customer group by ElkaID , kercity having count(*) > 1 and kercity = @CITY

OPEN subscrib_cursor;
   fetch next from subscrib_cursor into @ID_FINANCE
while @@FETCH_STATUS = 0
	Begin
	BEGIN TRY
	set @i=1
	delete #Customer
	insert into #Customer select * , ROW_NUMBER() OVER(ORDER BY KerStopDate ASC) AS Row from [CRM].dbo.Customer where ElkaID = @ID_FINANCE and kercity = @CITY

	

	select top(1) @ID = ID , @Telephone = NewTelephone , @S_START_DATE = [START_DATE] from Salas.dbo.Subscrib where ID_FINANCE = @ID_FINANCE order by ID

	 set @CenterID = (select Top(1) [CRM].dbo.Center.ID from Salas.dbo.FIMARK join [CRM].dbo.Center on Salas.dbo.FIMARK.ID_MARKAZ = [CRM].dbo.Center.CenterCode where Salas.dbo.FIMARK.ID_FINANCE = @ID_FINANCE)
	 set @CenterCode = (SELECT [CRM].dbo.City.Code  FROM [CRM].dbo.Center INNER JOIN [CRM].dbo.Region ON [CRM].dbo.Center.RegionID = [CRM].dbo.Region.ID INNER JOIN [CRM].dbo.City ON [CRM].dbo.Region.CityID = [CRM].dbo.City.ID WHERE [CRM].dbo.Center.ID = @CenterID)
	--declare @CustomerID bigint ; set @CustomerID = (select top(1) ID from [CRM].dbo.Customer where ElkaID = @ID_FINANCE order by [CRM].dbo.Customer.KerStopDate asc)

	
	set @Count = (select Count(*) from #Customer)
	 
    WHILE @i <  @Count
    BEGIN

	 set @reqeustID = CAST((CAST(STUFF( @S_START_DATE, 1, 2, '') AS nvarchar(max))+ CAST(@id as nvarchar(max) ) + CAST(@i as varchar) + '28') as bigint)


	 set @oldCustomerID = (select  ID from #Customer as C where Row = @i )
	 set @newCustomerID = (select  ID from #Customer as C where  Row = @i + 1 )

	 set @S_STOP_DATE = (select KerStopDate from #Customer where Row = @i)
    declare @STOP_DATE smalldatetime; set @STOP_DATE = CAST('1900-01-01' as smalldatetime);
	BEGIN TRY
	set @STOP_DATE =( [CRM].[dbo].[sh2mi](STUFF(STUFF(@S_STOP_DATE , 5 ,0,'/') , 8 , 0 ,'/')));
	END TRY
    BEGIN CATCH
    END CATCH
	if @STOP_DATE is null begin set @STOP_DATE = CAST('1900-01-01' as smalldatetime) end


	
INSERT INTO [CRM].[dbo].[Request] VALUES(
@reqeustID , --[ID]
@Telephone ,
null,
null,
28,
@CenterID,
@oldCustomerID,
@STOP_DATE,
null, --[RequestLetterNo] ,
null, --[RequestLetterDate],
null, --[RequesterName],
null, --[RequestPaymentTypeID] ,
null, --[RepresentitiveNo],
null, --[RepresentitiveDate],
null,  --[RepresentitiveExpireDate],
1506,
@STOP_DATE ,
null, --[ModifyDate] ,
@STOP_DATE , --EndDate
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

INSERT INTO [CRM].[dbo].[ChangeName]
     VALUES
           (@reqeustID --<ID, bigint,>
           ,@oldCustomerID --<OldCustomerID, bigint,>
           ,@newCustomerID --<NewCustomerID, bigint,>
           ,null --<LastCyleID, int,>
           ,null --<LastBillDate, smalldatetime,>
           ,0 --<HasCourtLetter, bit,>
           ,null --<CourtName, nvarchar(200),>
           ,null --<AgentName, nvarchar(50),>
           ,null --<CourtVerdictNo, nvarchar(50),>
           ,null --<CourtVerdictDate, smalldatetime,>
           ,null --<PeaceDocumentNo, nvarchar(50),>
           ,null --<PeaceDocumentDate, smalldatetime,>
           ,null --<PeaceDocumentInsertDate, smalldatetime,>
           ,null --<NoticeID, bigint,>
           ,null --<FileInfoReportID, bigint,>
           ,null --<AlternativeOfficeCode, nvarchar(200),>
           ,null --<PreviousOwnerID, bigint,>
           ,null --<IsTransferToReletives, tinyint,>
           ,null --<DocumentsFileID, uniqueidentifier,>
		   )

	

DECLARE @xml XML
SELECT @xml = '<?xml version="1.0" encoding="UTF-8"?>
<ChangeName xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <OldCustomerID>'+ CAST(@oldCustomerID as varchar) +'</OldCustomerID>
  <NewCustomerID>'+ CAST(@newCustomerID as varchar) +'</NewCustomerID>
</ChangeName>'


INSERT INTO [CRM].[dbo].[RequestLog] VALUES
           (@reqeustID
           ,28
           ,null
           ,0
           , @Telephone
           ,null
           ,(select CustomerID from CRM.dbo.Customer where ID = @oldCustomerID)
           ,null
           ,@STOP_DATE
           ,@xml)


     -- request payment

	delete #EXPENSE
	insert into #EXPENSE select * , ROW_NUMBER() OVER(ORDER BY ID ASC) AS Row from EXPENSE where (ID_EXPTYPE in( 9)) and (ID_SUBSCRI = @ID)
	 
	set @CountEX = (select Count(*) from #EXPENSE)
	set @iEX = 1
    WHILE @iEX <=  @CountEX
    BEGIN

	select @p_ID_DATE = E1.PAY_DATE  , @ID_S = E1.ID from #EXPENSE as E1 where E1.[Row] = @iEX

	declare @Pay_DATE smalldatetime;
	BEGIN TRY
	set @Pay_DATE =( [CRM].[dbo].[sh2mi](STUFF(STUFF(@p_ID_DATE , 5 ,0,'/') , 8 , 0 ,'/')));
	END TRY
    BEGIN CATCH
    END CATCH
	if(@Pay_DATE is null) begin set @Pay_DATE = CAST('1900-01-01' as smalldatetime) end 

	if exists(select *  from EXPENSE as E1 join MALI on E1.ID_EXPTYPE = MALI.ID_EXPTYPE 
		                 join Salas.dbo.EXPTYPE on EXPTYPE.ID = E1.ID_EXPTYPE
		                 where E1.ID = @ID_S  and E1.PAY_DATE <= MALI.STOP_DATE and E1.PAY_DATE >= MALI.START_DATE and EXPTYPE.ID in(9))
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
		   where E1.ID = @ID_S and E1.PAY_DATE <= MALI.STOP_DATE and E1.PAY_DATE >= MALI.START_DATE and EXPTYPE.ID in(9)
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
		   where E1.ID = @ID_S and B.KerID = -1 and EXPTYPE.ID in(9)
    END

	 
	SET @iEX = @iEX + 1
	
    END
	-- request payment
    SET @i = @i + 1
    END
	END TRY
    BEGIN CATCH
	 insert into Salas.dbo.requestLog values(@reqeustID ,  (select ERROR_MESSAGE()));
    END CATCH
	fetch next from subscrib_cursor into  @ID_FINANCE
	end
	close subscrib_cursor;
	deallocate subscrib_cursor;


