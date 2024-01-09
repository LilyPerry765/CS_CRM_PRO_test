USE Salas
GO


declare @CITY int = 7;
delete [CRM].dbo.RequestLog from [CRM].dbo.RequestLog as Rl join [CRM].dbo.Request as R on RL.RequestID = R.ID INNER JOIN
 [CRM].dbo.ChangeLocation as CL on CL.ID = R.ID INNER JOIN 
                     [CRM].dbo.Center ON R.CenterID = Center.ID INNER JOIN
                     [CRM].dbo.Region ON Center.RegionID = Region.ID INNER JOIN
                     [CRM].dbo.City ON Region.CityID = City.ID
					 where  City.ID = @CITY and R.RequestTypeID = 25 and CL.ChangeLocationTypeID = 1  and CreatorUserID is null

delete [CRM].dbo.ChangeLocation from [CRM].dbo.ChangeLocation as CL join [CRM].dbo.Request as R on CL.ID = R.ID INNER JOIN
                     [CRM].dbo.Center ON R.CenterID = Center.ID INNER JOIN
                    [CRM].dbo. Region ON Center.RegionID = Region.ID INNER JOIN
                     [CRM].dbo.City ON Region.CityID = City.ID
					 where  City.ID = @CITY and R.RequestTypeID = 25 and CL.ChangeLocationTypeID = 1  and CreatorUserID is null

 delete [CRM].dbo.RequestPayment from [CRM].dbo.RequestPayment as RP join [CRM].dbo.Request as R on RP.RequestID = R.ID INNER JOIN
 -- [CRM].dbo.ChangeLocation as CL on CL.ID = R.ID INNER JOIN 
                     [CRM].dbo.Center ON R.CenterID = Center.ID INNER JOIN
                     [CRM].dbo.Region ON Center.RegionID = Region.ID INNER JOIN
                     [CRM].dbo.City ON Region.CityID = City.ID
					 where  City.ID = @CITY and R.RequestTypeID = 25   and R.CreatorUserID is null

delete [CRM].dbo.Request From [CRM].dbo.Request as R INNER JOIN
 --[CRM].dbo.ChangeLocation as CL on CL.ID = R.ID INNER JOIN 
                     [CRM].dbo.Center ON R.CenterID = Center.ID INNER JOIN
                     [CRM].dbo.Region ON Center.RegionID = Region.ID INNER JOIN
                     [CRM].dbo.City ON Region.CityID = City.ID
					 where  City.ID = @CITY and R.RequestTypeID = 25   and CreatorUserID is null


declare @ID float
declare @ID_FINANCE float
declare	@Telephone  bigint
declare @S_START_DATE  nvarchar(8)
declare @SUB_START_DATE  nvarchar(8)
declare @SUB_STOP_DATE   nvarchar(8)



IF OBJECT_ID('tempdb..#Address1') IS NOT NULL
DROP TABLE #Address1

CREATE TABLE #Address1(
[ID] [nvarchar](10) NULL,
	[ID_FINANCE] [nvarchar](10) NULL,
	[ADDRESS] [nvarchar](72) NULL,
	[POST_COD] [nvarchar](10) NULL,
	[START_DATE] [nvarchar](8) NULL,
	[STOP_DATE] [nvarchar](8) NULL,
	[METR] [nvarchar](10) NULL,
	[ENTID] [nvarchar](10) NULL,
	[ID_TYADR] [nvarchar](10) NULL,
	[ID_FOLD] [nvarchar](10) NULL,
	[ADR] [nvarchar](71) NULL,
	[Row] [int] NULL)

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

declare @cerCount  float;  set @cerCount = ( select count(*) from Subscrib where ID_FREE <> 9 )
declare @cerCourent  bigint; set @cerCourent = 0

declare subscrib_cursor cursor read_only fast_forward for select  ID ,  ID_FINANCE ,  START_DATE ,  STOP_DATE , NewTelephone  from Subscrib where ID_FREE <> 9 

OPEN subscrib_cursor;
fetch next from subscrib_cursor into @ID , @ID_FINANCE ,@SUB_START_DATE ,@SUB_STOP_DATE , @Telephone
while @@FETCH_STATUS = 0
	Begin
	set @cerCourent = @cerCourent + 1;
	--BEGIN TRY
	if ((select count(*) from dbo.Address where ID_FINANCE = @ID_FINANCE and  START_DATE >= @SUB_START_DATE and STOP_DATE <= @SUB_STOP_DATE) > 1) 
	Begin

	delete #Address1
	insert into #Address1 select * , ROW_NUMBER() OVER(ORDER BY STOP_DATE ASC) AS Row from dbo.Address where ID_FINANCE = @ID_FINANCE and  START_DATE >= @SUB_START_DATE and STOP_DATE <= @SUB_STOP_DATE
	
	declare @reqeustID bigint;
	declare @S_STOP_DATE nvarchar(max);
	declare @oldAdressID bigint ;
	declare @newAdressID bigint ;

	declare @CenterID bigint ; set @CenterID = (select Top(1) CRM.dbo.Center.ID from FIMARK join CRM.dbo.Center on FIMARK.ID_MARKAZ = CRM.dbo.Center.CenterCode where dbo.FIMARK.ID_FINANCE = @ID_FINANCE)
	declare @CenterCode bigint ; set @CenterCode = (SELECT CRM.dbo.City.Code  FROM CRM.dbo.Center INNER JOIN CRM.dbo.Region ON CRM.dbo.Center.RegionID = CRM.dbo.Region.ID INNER JOIN CRM.dbo.City ON CRM.dbo.Region.CityID = CRM.dbo.City.ID WHERE CRM.dbo.Center.ID = @CenterID)
	declare @CustomerID bigint ; set @CustomerID = (select top(1) ID from CRM.dbo.Customer where ElkaID = @ID_FINANCE and kercity = @CITY order by CRM.dbo.Customer.KerStopDate asc)


	DECLARE @Count int ; set @Count = (select Count(*) from #Address1)
	DECLARE @i int = 1
    WHILE @i <  @Count
    BEGIN
	print @id

		set @oldAdressID = (select Top(1)  A.ID from CRM.dbo.Address as A join CRM.dbo.Center on A.CenterID = CRM.dbo.Center.ID INNER JOIN CRM.dbo.Region ON CRM.dbo.Center.RegionID = CRM.dbo.Region.ID INNER JOIN CRM.dbo.City ON CRM.dbo.Region.CityID = CRM.dbo.City.ID WHERE CRM.dbo.City.ID = @CITY and A.CenterID = @CenterID and A.kerID = (select A2.ID from #Address1 as A2 where Row = @i ))
	if @oldAdressID is null set @oldAdressID=573034
	
	set @newAdressID = (select Top(1)  A.ID from CRM.dbo.Address as A join CRM.dbo.Center on A.CenterID = CRM.dbo.Center.ID INNER JOIN CRM.dbo.Region ON CRM.dbo.Center.RegionID = CRM.dbo.Region.ID INNER JOIN CRM.dbo.City ON CRM.dbo.Region.CityID = CRM.dbo.City.ID WHERE CRM.dbo.City.ID = @CITY and A.CenterID = @CenterID and A.kerID = (select A2.ID from #Address1 as A2 where Row = @i + 1 ))
	if @newAdressID is null set @newAdressID=573034

	set @reqeustID = CAST((CAST(STUFF( @SUB_START_DATE, 1, 2, '') AS nvarchar(max))+ CAST(@ID as nvarchar(max) ) + CAST(@i as varchar) + '25'+ cast (@CenterCode as varchar (max)))  as bigint )
	--set @reqeustID = CAST((CAST(STUFF( @SUB_START_DATE, 1, 2, '') AS nvarchar(max))+ CAST(@ID as nvarchar(max) ) + CAST(@newAdressID as varchar) + cast (@CenterCode as varchar (max)))  as bigint )          
			         



	set @S_STOP_DATE = (select STOP_DATE from #Address1 where Row = @i)
    declare @STOP_DATE smalldatetime; set @STOP_DATE = CAST('1900-01-01' as smalldatetime);
	BEGIN TRY
	set @STOP_DATE =( CRM.[dbo].[sh2mi](STUFF(STUFF(@S_STOP_DATE , 5 ,0,'/') , 8 , 0 ,'/')));
	END TRY
    BEGIN CATCH
    END CATCH
	if @STOP_DATE is null begin set @STOP_DATE = CAST('1900-01-01' as smalldatetime) end


	
INSERT INTO CRM.[dbo].[Request] VALUES(
@reqeustID , --[ID]
@Telephone ,
null,
null,
25,
@CenterID,
@CustomerID,
@STOP_DATE,
null, --[RequestLetterNo] ,
null, --[RequestLetterDate],
null, --[RequesterName],
null, --[RequestPaymentTypeID] ,
null, --[RepresentitiveNo],
null, --[RepresentitiveDate],
null,  --[RepresentitiveExpireDate],
1298,
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

--select @reqeustID
--select @newAdressID
--select @oldAdressID



INSERT INTO CRM.[dbo].[ChangeLocation]
     VALUES
           ( 
		   @reqeustID --<ID, bigint,>
           ,1         --<ChangeLocationTypeID, tinyint,>
           ,null      --<InvestigatePossibilityID, bigint,>
           ,null      --<SubscriberAddressID, bigint,>
           ,null      --<DocumentRequestContentID, bigint,>
           ,null      --<ChangeLocationReoprtIDTo118, bigint,>
           ,null      --<Equipment, tinyint,>
           --,null      --<ReservBuchtID, bigint,>
           ,null      --<OldBuchtID, bigint,>
           ,null      --<OldPostContactID, bigint,>
           ,null      --<OldCabinetInputID, bigint,>
           ,null      --<ChangeLocationReportID, bigint,>
           ,@newAdressID      --,<NewInstallAddressID, bigint,>
           ,@newAdressID      --,<NewCorrespondenceAddressID, bigint,>
           ,@oldAdressID      --,<OldInstallAddressID, bigint,>
           ,@oldAdressID      --,<OldCorrespondenceAddressID, bigint,>
           ,null      --,<OldTelephone, bigint,>
           ,null      --,<OldSwitchPortID, int,>
           ,null      --,<NewTelephone, bigint,>
           ,null      --,<NearestTelephon, bigint,>
           ,null      --,<NewCounterID, bigint,>
           --,null      --,<NewPostContactID, bigint,>
           --,null      --,<NewCabinetInputID, bigint,>
           ,null      --<OldCounterID, bigint,>
           ,null      --<SourceCenter, int,>
           ,null      --<TargetCenter, int,>
           ,null      --<ConfirmTheSourceCenter, bit,>
           ,null      --<ConfirmTheTargetCenter, bit,>
           ,null      --<NewCustomerID, bigint,>
           ,null      --<Status, tinyint,>
		   )


      DECLARE @xml XML
      SELECT @xml = '<?xml version="1.0" encoding="UTF-8"?>
<ChangeLocation xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <OldPostContactID>0</OldPostContactID>
  <OldCabinetInputID>0</OldCabinetInputID>
  <OldCustomerAddressID>' + Cast(@oldAdressID as varchar) + '</OldCustomerAddressID>
  <NewCustomerAddressID>' + Cast(@newAdressID as varchar) + '</NewCustomerAddressID>
  <OldTelephone>' + cast (@Telephone as varchar) + '</OldTelephone>
  <NewTelephone>0</NewTelephone>
  <OldBucht>0</OldBucht>
  <NewBucht>0</NewBucht>
  <TargetCenter>0</TargetCenter>
  <SourceCenter>0</SourceCenter>
  <RequestType>25</RequestType>
  <ChangeLocationTypeID>1</ChangeLocationTypeID>
  <NewPostContactID>0</NewPostContactID>
  <NewCabinetInputID>0</NewCabinetInputID>
</ChangeLocation>'


      INSERT INTO CRM.[dbo].[RequestLog] VALUES
           (@reqeustID
           ,25
           ,null
           ,0
           , @Telephone
           ,null
           ,(select CustomerID from CRM.dbo.Customer where ID = @CustomerID)
           ,null
           ,@STOP_DATE
           ,@xml)

 -- request payment

   declare @P_START_DATE  nvarchar(8)
   declare @P_STOP_DATE   nvarchar(8)
   select @P_START_DATE = START_DATE , @P_STOP_DATE = STOP_DATE from #Address1 where Row = @i + 1

	delete #EXPENSE
	insert into #EXPENSE select * , ROW_NUMBER() OVER(ORDER BY ID ASC) AS Row from EXPENSE where ID_SUBSCRI = @ID and (ID_EXPTYPE = 10 or ID_EXPTYPE = 14) and EXPENSE.PAY_DATE >= @P_START_DATE and EXPENSE.PAY_DATE < @P_STOP_DATE
	 
	DECLARE @CountEX int ; set @CountEX = (select Count(*) from #EXPENSE)
	DECLARE @iEX int = 1
    WHILE @iEX <=  @CountEX
    BEGIN

	declare @ID_S nvarchar(max); 
	declare @p_ID_DATE nvarchar(max);

	select @p_ID_DATE = E1.PAY_DATE  , @ID_S = E1.ID from #EXPENSE as E1 where E1.[Row] = @iEX

	declare @Pay_DATE smalldatetime;
	BEGIN TRY
	set @Pay_DATE =( CRM.[dbo].[sh2mi](STUFF(STUFF(@p_ID_DATE , 5 ,0,'/') , 8 , 0 ,'/')));
	END TRY
    BEGIN CATCH
    END CATCH
	if(@Pay_DATE is null) begin set @Pay_DATE = CAST('1900-01-01' as smalldatetime) end 

	if exists(select *  from EXPENSE as E1 join MALI on E1.ID_EXPTYPE = MALI.ID_EXPTYPE 
		                 join [Salas].dbo.EXPTYPE on EXPTYPE.ID = E1.ID_EXPTYPE
		                 where E1.ID = @ID_S  and E1.PAY_DATE <= MALI.STOP_DATE and E1.PAY_DATE >= MALI.START_DATE and EXPTYPE.ID in(10 , 14))
	BEGIN

	INSERT INTO CRM.[dbo].[RequestPayment]
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
		   join [Salas].dbo.EXPTYPE on EXPTYPE.ID = E1.ID_EXPTYPE
		   join CRM.dbo.BaseCost as B on B.KerID = MALI.ID
		   where E1.ID = @ID_S and E1.PAY_DATE <= MALI.STOP_DATE and E1.PAY_DATE >= MALI.START_DATE and EXPTYPE.ID in(10 , 14)

	END
	else
	BEGIN
	INSERT INTO CRM.[dbo].[RequestPayment]
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
		   join [Salas].dbo.EXPTYPE on EXPTYPE.ID = E1.ID_EXPTYPE
		   join CRM.dbo.BaseCost as B on EXPTYPE.NAME = B.Title
		   where E1.ID = @ID_S and B.KerID = -1 and EXPTYPE.ID in(10 , 14)
    END


	 SET @iEX = @iEX + 1
     END

	 -- request payment

		    SET @i = @i + 1
     END


	 END
	--END TRY
 --   BEGIN CATCH
	-- insert into [SalasAbad].dbo.requestLog values(@reqeustID , (select ERROR_MESSAGE()));
 --   END CATCH

	
	print (@cerCourent * 100) / @CerCount
	fetch next from subscrib_cursor into @ID , @ID_FINANCE ,@SUB_START_DATE ,@SUB_STOP_DATE , @Telephone 
	end
	close subscrib_cursor;
	deallocate subscrib_cursor;


