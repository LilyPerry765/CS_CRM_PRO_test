
USE Salas
GO

declare @CITY int = 7;
delete [CRM].dbo.RequestLog from [CRM].dbo.RequestLog as Rl join [CRM].dbo.Request as R on RL.RequestID = R.ID INNER JOIN
                     [CRM].dbo.Center ON R.CenterID = Center.ID INNER JOIN
                     [CRM].dbo.Region ON Center.RegionID = Region.ID INNER JOIN
                     [CRM].dbo.City ON Region.CityID = City.ID
					 where  City.ID = @CITY and R.RequestTypeID = 47 and R.CreatorUserID is null

delete [CRM].dbo.ChangeNo from [CRM].dbo.ChangeNo as CN join [CRM].dbo.Request as R on CN.ID = R.ID INNER JOIN
                     [CRM].dbo.Center ON R.CenterID = Center.ID INNER JOIN
                     [CRM].dbo.Region ON Center.RegionID = Region.ID INNER JOIN
                     [CRM].dbo.City ON Region.CityID = City.ID
					 where  City.ID = @CITY and R.RequestTypeID = 47 and R.CreatorUserID is null

delete [CRM].dbo.RequestPayment from [CRM].dbo.RequestPayment as RP join [CRM].dbo.Request as R on RP.RequestID = R.ID INNER JOIN
                     [CRM].dbo.Center ON R.CenterID = Center.ID INNER JOIN
                     [CRM].dbo.Region ON Center.RegionID = Region.ID INNER JOIN
                     [CRM].dbo.City ON Region.CityID = City.ID
					 where  City.ID = @CITY and R.RequestTypeID = 47 and R.CreatorUserID is null

delete [CRM].dbo.Request From [CRM].dbo.Request as R INNER JOIN
                     [CRM].dbo.Center ON R.CenterID = Center.ID INNER JOIN
                     [CRM].dbo.Region ON Center.RegionID = Region.ID INNER JOIN
                     [CRM].dbo.City ON Region.CityID = City.ID
					 where  City.ID = @CITY and R.RequestTypeID = 47 and R.CreatorUserID is null

declare @ID float
declare @ID_FINANCE bigint
declare @S_START_DATE  nvarchar(8)
declare @S_START_DATE2  nvarchar(8)
DECLARE @CountEX int ; 
DECLARE @iEX int = 1 ;
declare @ID_S nvarchar(max); 
declare @p_ID_DATE nvarchar(max);
declare @reqeustID bigint;
declare @S_STOP_DATE nvarchar(max);
declare @oldTelephone bigint ;
declare @newTelephone bigint ;
declare @CenterID bigint   ;
declare @CenterCode bigint ;
declare @CustomerID bigint ;
declare @AdressID bigint   ;
declare @STOP_DATE smalldatetime;
DECLARE @xml XML;
DECLARE @telephoneNo bigint;

IF OBJECT_ID('tempdb..#Subscrib') IS NOT NULL
DROP TABLE #Subscrib

CREATE TABLE #subscrib(
	[ID] [float] NULL,
	[ID_FINANCE] [float] NULL,
	[TEL] [nvarchar](4) NULL,
	[TEL_PISH] [nvarchar](6) NULL,
	[ALLOC_DATE] [nvarchar](8) NULL,
	[START_DATE] [nvarchar](8) NULL,
	[STOP_DATE] [nvarchar](8) NULL,
	[ID_SYNTAX] [smallint] NULL,
	[ID_FREE] [smallint] NULL,
	[COD118] [smallint] NULL,
	[DC] [smallint] NULL,
	[TOGIF] [smallint] NULL,
	[ENTID] [smallint] NULL,
	[ID_TELTYPE] [smallint] NULL,
	[ID_FOLD] [float] NULL,
	[TELE] [nvarchar](10) NULL,
	[NewTelephone] [bigint] NULL,
	[Row] [int] NULL
) 

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

declare subscrib_cursor cursor read_only for select ID , ID_FINANCE , NewTelephone , [START_DATE]  from SUBSCRIB2 where ID_FREE = 8 --and [ID_FINANCE] not in (SELECT [ID_FINANCE] FROM [Salas].[dbo].[changenumber] group by [ID_FINANCE])

OPEN subscrib_cursor;
   fetch next from subscrib_cursor into @ID , @ID_FINANCE , @telephoneNo , @S_START_DATE2
while @@FETCH_STATUS = 0
	Begin
	BEGIN TRY
	set @oldTelephone=null
	set @newTelephone=null
	delete #Subscrib
	insert into #Subscrib select top(1) * , 1 from Subscrib2 where ID = @ID and NewTelephone = @telephoneNo  order by START_DATE
	insert into #Subscrib select top(1) * , 2 from Subscrib2 where ID_FINANCE = @ID_FINANCE and START_DATE > @S_START_DATE2   and id_free!=15
	--select * from #Subscrib

	 set @CenterID = (select Top(1) [CRM].dbo.Center.ID from FIMARK join [CRM].dbo.Center on FIMARK.ID_MARKAZ = [CRM].dbo.Center.CenterCode where FIMARK.ID_FINANCE = @ID_FINANCE)
	 set @CenterCode = (SELECT [CRM].dbo.City.Code  FROM [CRM].dbo.Center INNER JOIN [CRM].dbo.Region ON [CRM].dbo.Center.RegionID = [CRM].dbo.Region.ID INNER JOIN [CRM].dbo.City ON [CRM].dbo.Region.CityID = [CRM].dbo.City.ID WHERE [CRM].dbo.Center.ID = @CenterID)
	 set @CustomerID = (select top(1) ID from [CRM].dbo.Customer where ElkaID = @ID_FINANCE and kercity = @CITY and [CRM].dbo.Customer.KerStopDate = '99999999')
	 set @AdressID = (select top(1) A.ID from [CRM].dbo.Address as A join [CRM].dbo.Center on A.CenterID = [CRM].dbo.Center.ID INNER JOIN [CRM].dbo.Region ON [CRM].dbo.Center.RegionID = [CRM].dbo.Region.ID INNER JOIN [CRM].dbo.City ON [CRM].dbo.Region.CityID = [CRM].dbo.City.ID WHERE [CRM].dbo.City.ID = @CITY and ElkaID = @ID_FINANCE      and A.KerStopDate = '99999999')

     select  @oldTelephone = NewTelephone from #Subscrib as C where Row = 1
  	 select  @newTelephone = NewTelephone from #Subscrib as C where  Row = 2

	 set @reqeustID = CAST((CAST(STUFF( @S_START_DATE2, 1, 2, '') AS nvarchar(max))+ CAST(@ID as nvarchar(max) ) + '47') as bigint)

	 set @S_STOP_DATE = (select STOP_DATE from #Subscrib where Row = 1)
    
	BEGIN TRY
	set @STOP_DATE =( [CRM].[dbo].[sh2mi](STUFF(STUFF(@S_STOP_DATE , 5 ,0,'/') , 8 , 0 ,'/')));
	END TRY
    BEGIN CATCH
    END CATCH
	if @STOP_DATE is null begin set @STOP_DATE = CAST('1900-01-01' as smalldatetime) end




    INSERT INTO [CRM].[dbo].[Request] VALUES(
@reqeustID , --[ID]
@oldTelephone,
null,
null,
47,
@CenterID,
@CustomerID,
@STOP_DATE,
null, --[RequestLetterNo] ,
null, --[RequestLetterDate],
null, --[RequesterName],
null, --[RequestPaymentTypeID] ,
null, --[RepresentitiveNo],
null, --[RepresentitiveDate],
null, --[RepresentitiveExpireDate],
1387,
@STOP_DATE ,
null, --[ModifyDate] ,
@STOP_DATE , --EndDate
null, --[CreatorUserID] 
null, --[ModifyUserID] ,
null, --[SellerID] ,
null, --[PreviousAction] ,
null, --[BillID],
null, --[PaymentID] ,
0,    --[IsWaitingList],
0,    --[IsCancelation] ,
0,    --[IsViewed] ,
0,    --[IsVisible],
0     --[WaitForToBeCalculate]
);



    INSERT INTO [CRM].[dbo].[ChangeNo]
	
           VALUES
           (@reqeustID --<ID, bigint,>
           ,null --<InvestigatePossibilityID, bigint,>
           ,null --<OldRequestID, bigint,>
           ,1    --<CauseOfChangeNoID, int,>
           ,null --<ChangeEntryDate, smalldatetime,>
           ,0    --<NumberType, bit,>
           ,@oldTelephone --<OldTelephoneNo, bigint,>
           ,null  --<OldPostContactID, bigint,>
           ,null  --<OldCabinetInputID, bigint,>
           , @newTelephone --<NewTelephoneNo, bigint,>
           ,null--<ChangeDate, smalldatetime,>
           ,null--<OldSwitchPortID, int,>
           ,null--<OldBuchtID, bigint,>
           ,null--<NewSwitchPortID, int,>
           ,null--<Description, nvarchar(100),>
           ,null--<DescriptionOfExpenditures, nvarchar(100),>
           ,@CustomerID--<CustomerID, bigint,>
           ,@AdressID --<InstallAddressID, bigint,>
           ,@AdressID --<CorrespondenceAddressID, bigint,>
           ,null--<Status, tinyint,>
		   );

		   --select  @oldTelephone as old
		   --select @newTelephone as new 
		  

     SELECT @xml = '<?xml version="1.0" encoding="UTF-8"?>
<ChangeNo xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <OldPostContactID>0</OldPostContactID>
  <OldCabinetInputID>0</OldCabinetInputID>
  <OldTelephoneNo>'+Cast(@oldTelephone as varchar)+'</OldTelephoneNo>
  <OldSwitchPort>0</OldSwitchPort>
  <NewTelephoneNo>'+Cast(@newTelephone as varchar)+'</NewTelephoneNo>
  <NewSwitchPort>0</NewSwitchPort>
</ChangeNo>'



     INSERT INTO [CRM].[dbo].[RequestLog] VALUES
           (@reqeustID
           ,47
           ,null
           ,0
           ,  @oldTelephone 
           ,  @newTelephone 
           ,(select CustomerID from CRM.dbo.Customer where ID = @CustomerID)
           ,null
           ,@STOP_DATE
           ,@xml)


    -- request payment

	delete #EXPENSE
	insert into #EXPENSE select * , ROW_NUMBER() OVER(ORDER BY ID ASC) AS Row from EXPENSE where ID_SUBSCRI = @ID and ID_EXPTYPE = 11
	
	
	set @CountEX = (select Count(*) from #EXPENSE)
	set @iEX= 1 ;
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
		                 where E1.ID = @ID_S  and E1.PAY_DATE <= MALI.STOP_DATE and E1.PAY_DATE >= MALI.START_DATE and EXPTYPE.ID in(11))
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
		   where E1.ID = @ID_S and E1.PAY_DATE <= MALI.STOP_DATE and E1.PAY_DATE >= MALI.START_DATE and EXPTYPE.ID in(11)
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
		   where E1.ID = @ID_S and B.KerID = -1 and EXPTYPE.ID in(11)
    END
	 SET @iEX = @iEX + 1
     END

	 -- request payment

	END TRY
    BEGIN CATCH
	insert into Salas.dbo.requestLog values(@telephoneNo , (select ERROR_MESSAGE()));
    END CATCH
	fetch next from subscrib_cursor into  @ID , @ID_FINANCE,@telephoneNo , @S_START_DATE2
	end
	close subscrib_cursor;
	deallocate subscrib_cursor;


