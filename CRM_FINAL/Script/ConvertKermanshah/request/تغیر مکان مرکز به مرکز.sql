USE Salas
GO

declare @CITY int = 7;



delete [CRM].dbo.RequestLog from [CRM].dbo.RequestLog as Rl join [CRM].dbo.Request as R on RL.RequestID = R.ID INNER JOIN
                     [CRM].dbo.ChangeLocation as CL on CL.ID = R.ID INNER JOIN 
                     [CRM].dbo.Center ON R.CenterID = Center.ID INNER JOIN
                     [CRM].dbo.Region ON Center.RegionID = Region.ID INNER JOIN
                     [CRM].dbo.City ON Region.CityID = City.ID
					 where  City.ID = @CITY and R.RequestTypeID = 63 and CreatorUserID is null

delete [CRM].dbo.ChangeLocation from [CRM].dbo.ChangeLocation as CL join [CRM].dbo.Request as R on CL.ID = R.ID INNER JOIN
                     [CRM].dbo.Center ON R.CenterID = Center.ID INNER JOIN
                     [CRM].dbo.Region ON Center.RegionID = Region.ID INNER JOIN
                     [CRM].dbo.City ON Region.CityID = City.ID
					 where  City.ID = @CITY and R.RequestTypeID = 63 and CreatorUserID is null

					 
 delete [CRM].dbo.RequestPayment from [CRM].dbo.RequestPayment as RP join [CRM].dbo.Request as R on RP.RequestID = R.ID INNER JOIN
   --[CRM].dbo.ChangeLocation as CL on CL.ID = R.ID INNER JOIN 
                     [CRM].dbo.Center ON R.CenterID = Center.ID INNER JOIN
                     [CRM].dbo.Region ON Center.RegionID = Region.ID INNER JOIN
                     [CRM].dbo.City ON Region.CityID = City.ID
					 where  City.ID = @CITY and R.RequestTypeID = 63  and CreatorUserID is null

delete [CRM].dbo.Request From [CRM].dbo.Request as R INNER JOIN
                     --[CRM].dbo.ChangeLocation as CL on CL.ID = R.ID INNER JOIN 
                     [CRM].dbo.Center ON R.CenterID = Center.ID INNER JOIN
                     [CRM].dbo.Region ON Center.RegionID = Region.ID INNER JOIN
                     [CRM].dbo.City ON Region.CityID = City.ID
					 where  City.ID = @CITY and R.RequestTypeID = 63 and CreatorUserID is null



declare @ID float
declare @ID_FINANCE float
declare @S_START_DATE  nvarchar(8)

declare @reqeustID bigint;
declare @S_STOP_DATE nvarchar(max);
declare @oldAdressID bigint ;
declare @newAdressID bigint ;


declare @oldTelephone bigint ;
declare @newTelephone bigint ;

declare @oldCenterID bigint ;
declare @newCenterID bigint ;

declare @oldStartDate nvarchar(8) ;
declare @newStartDate nvarchar(8) ;

declare @oldStopDate nvarchar(8) ;
declare @newStopDate nvarchar(8) ;


declare @oldCenterCode bigint ;
declare @newCenterCode bigint ;

DECLARE @Count int ; 
DECLARE @i int = 1

declare @S_START_DATE2  nvarchar(8)

	
	
	DECLARE @CountEX int ; 
	DECLARE @iEX int = 1
	declare @ID_S nvarchar(max); 
	declare @p_ID_DATE nvarchar(max);
	declare @Pay_DATE smalldatetime;


IF OBJECT_ID('tempdb..#FIMARK') IS NOT NULL
DROP TABLE #FIMARK

IF OBJECT_ID('tempdb..#Subscrib2') IS NOT NULL
DROP TABLE #Subscrib2

CREATE TABLE #FIMARK(
	[ID_FINANCE] [float] NULL,
	[START_DATE] [nvarchar](8) NULL,
	[STOP_DATE] [nvarchar](8) NULL,
	[ID_MARKAZ] [float] NULL,
	[ID_CITY] [float] NULL,
	[ID_AGMAR] [float] NULL,
	[ENTID] [float] NULL,
	[ID_ADDRESS] [float] NULL,
	[ID_FOLD] [float] NULL,
	[Row] [int] NULL
)

CREATE TABLE #Subscrib2(
	[ID] [float] NULL,
	[ID_FINANCE] [float] NULL,
	[TEL] [nvarchar](4) NULL,
	[TEL_PISH] [nvarchar](6) NULL,
	[ALLOC_DATE] [nvarchar](8) NULL,
	[START_DATE] [nvarchar](8) NULL,
	[STOP_DATE] [nvarchar](8) NULL,
	[ID_SYNTAX] [float] NULL,
	[ID_FREE] [float] NULL,
	[COD118] [float] NULL,
	[DC] [float] NULL,
	[TOGIF] [float] NULL,
	[ENTID] [float] NULL,
	[ID_TELTYPE] [float] NULL,
	[ID_FOLD] [float] NULL,
	[TELEE] [nvarchar](7) NULL,
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

declare subscrib_cursor cursor read_only for select ID , ID_FINANCE ,NewTelephone, [START_DATE] from Subscrib2 where ID_FREE = 9


OPEN subscrib_cursor;
   fetch next from subscrib_cursor into @ID , @ID_FINANCE , @oldTelephone , @S_START_DATE2
while @@FETCH_STATUS = 0
	Begin
	-- BEGIN TRY

	delete #FIMARK ;
	delete #Subscrib2;

	insert into #FIMARK select top(1) * , 1 from FIMARK where  ID_FINANCE = @ID_FINANCE and START_DATE >= @S_START_DATE2
	insert into #FIMARK select top(1) * , 2 from FIMARK where  ID_FINANCE = @ID_FINANCE and START_DATE > (select START_DATE from  #FIMARK where Row = 1) 


	insert into #Subscrib2 select top(1) * , 1 from Subscrib2 where ID = @ID and NewTelephone = @oldTelephone order by START_DATE
	insert into #Subscrib2 select top(1) * , 2 from Subscrib2 where ID_FINANCE = @ID_FINANCE and START_DATE > @S_START_DATE2 and id_free!=15

	

	 (select  @oldCenterID = C.ID , @oldStartDate = START_DATE , @oldStopDate = STOP_DATE from #FIMARK join CRM.dbo.Center as C on C.CenterCode = #FIMARK.ID_MARKAZ  where  Row = 1 ) ;
	 (select  @newCenterID = C.ID  , @newStartDate = START_DATE , @newStopDate = STOP_DATE  from #FIMARK join CRM.dbo.Center as C on C.CenterCode = #FIMARK.ID_MARKAZ   where Row = 2 ) ;



	 set @oldCenterCode = (SELECT CRM.dbo.City.Code  FROM CRM.dbo.Center INNER JOIN CRM.dbo.Region ON CRM.dbo.Center.RegionID = CRM.dbo.Region.ID INNER JOIN CRM.dbo.City ON CRM.dbo.Region.CityID = CRM.dbo.City.ID WHERE CRM.dbo.Center.ID = @oldCenterID)
	 set @newCenterCode = (SELECT CRM.dbo.City.Code  FROM CRM.dbo.Center INNER JOIN CRM.dbo.Region ON CRM.dbo.Center.RegionID = CRM.dbo.Region.ID INNER JOIN CRM.dbo.City ON CRM.dbo.Region.CityID = CRM.dbo.City.ID WHERE CRM.dbo.Center.ID = @newCenterID)
	 
	 set @newTelephone = (select NewTelephone from #Subscrib2 where Row =2)
	 print @ID
	  set @reqeustID = CAST((CAST(STUFF( @S_START_DATE2, 1, 2, '') AS nvarchar(max))+ CAST(@ID as nvarchar(max) ) + CAST(@i as varchar) + '63'+CAST(@city as varchar)) as bigint)
	   SET @i= @i + 1
	 set @oldAdressID = (select top(1) A.ID from CRM.dbo.Address as A join CRM.dbo.Center on A.CenterID = CRM.dbo.Center.ID INNER JOIN CRM.dbo.Region ON CRM.dbo.Center.RegionID = CRM.dbo.Region.ID INNER JOIN CRM.dbo.City ON CRM.dbo.Region.CityID = CRM.dbo.City.ID WHERE CRM.dbo.City.ID = @CITY and ElkaID = @ID_FINANCE and A.KerStartDate = @oldStartDate and A.KerStopDate = @oldStopDate )
	 if @oldAdressID is null set @oldAdressID=600308--آدرس نامشخص
	 set @newAdressID = (select top(1) A.ID from CRM.dbo.Address as A join CRM.dbo.Center on A.CenterID = CRM.dbo.Center.ID INNER JOIN CRM.dbo.Region ON CRM.dbo.Center.RegionID = CRM.dbo.Region.ID INNER JOIN CRM.dbo.City ON CRM.dbo.Region.CityID = CRM.dbo.City.ID WHERE CRM.dbo.City.ID = @CITY and ElkaID = @ID_FINANCE and A.KerStartDate = @newStartDate and A.KerStopDate = @newStopDate )
	 if @newAdressID is null set @newAdressID=600308--آدرس نامشخص
	 declare @CustomerID bigint ; set @CustomerID = (select top(1) ID from CRM.dbo.Customer as C where  kercity = @CITY and  C.KerStartDate = @oldStartDate and C.KerStopDate = @oldStopDate)


	 
     declare @STOP_DATE smalldatetime; set @STOP_DATE = CAST('1900-01-01' as smalldatetime);
	 BEGIN TRY
	 set @STOP_DATE =( CRM.[dbo].[sh2mi](STUFF(STUFF(@oldStopDate , 5 ,0,'/') , 8 , 0 ,'/')));
	 END TRY
     BEGIN CATCH
     END CATCH
	 if @STOP_DATE is null begin set @STOP_DATE = CAST('1900-01-01' as smalldatetime) end


	
INSERT INTO CRM.[dbo].[Request] VALUES(
@reqeustID , --[ID]
@oldTelephone,
null,
null,
63,
@oldCenterID,
@CustomerID,
@STOP_DATE,
null, --[RequestLetterNo] ,
null, --[RequestLetterDate],
null, --[RequesterName],
null, --[RequestPaymentTypeID] ,
null, --[RepresentitiveNo],
null, --[RepresentitiveDate],
null,  --[RepresentitiveExpireDate],
1338,
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

INSERT INTO CRM.[dbo].[ChangeLocation]
     VALUES
           ( 
		   @reqeustID --<ID, bigint,>
           ,2         --<ChangeLocationTypeID, tinyint,>
           ,null      --<InvestigatePossibilityID, bigint,>
           ,null      --<Subscrib2erAddressID, bigint,>
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
           ,@oldTelephone      --,<OldTelephone, bigint,>
           ,null      --,<OldSwitchPortID, int,>
           ,@newTelephone      --,<NewTelephone, bigint,>
           ,null      --,<NearestTelephon, bigint,>
           ,null      --,<NewCounterID, bigint,>
           --,null      --,<NewPostContactID, bigint,>
           --,null      --,<NewCabinetInputID, bigint,>
           ,null      --<OldCounterID, bigint,>
           ,@oldCenterID      --<SourceCenter, int,>
           ,@newCenterID      --<TargetCenter, int,>
           ,null      --<ConfirmTheSourceCenter, bit,>
           ,null      --<ConfirmTheTargetCenter, bit,>
           ,null      --<NewCustomerID, bigint,>
           ,null      --<Status, tinyint,>
		   )

select @oldAdressID
select @newAdressID
select @oldTelephone
select @newTelephone
select @oldCenterID
select @reqeustID
select @newCenterID as newc
declare @n_newTelephone bigint =@newTelephone
if @n_newTelephone = null set @n_newTelephone=0

DECLARE @xml XML
SELECT @xml = '<?xml version="1.0" encoding="UTF-8"?>
<ChangeLocation xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <OldPostContactID>0</OldPostContactID>
  <OldCabinetInputID>0</OldCabinetInputID>
  <OldCustomerAddressID>' + Cast(@oldAdressID as varchar) + '</OldCustomerAddressID>
  <NewCustomerAddressID>' + Cast(@newAdressID as varchar) + '</NewCustomerAddressID>
  <OldTelephone>' + Cast(@oldTelephone as varchar)+ '</OldTelephone>
  <NewTelephone>' +  Cast(@n_newTelephone as varchar)+ '</NewTelephone>
  <OldBucht>0</OldBucht>
  <NewBucht>0</NewBucht>
  <TargetCenter>' +         Cast(@oldCenterID as varchar) + '</TargetCenter>
  <SourceCenter>' +         Cast(@newCenterID as varchar) + '</SourceCenter>
  <RequestType>63</RequestType>
  <ChangeLocationTypeID>2</ChangeLocationTypeID>
  <NewPostContactID>0</NewPostContactID>
  <NewCabinetInputID>0</NewCabinetInputID>
</ChangeLocation>'


           INSERT INTO CRM.[dbo].[RequestLog] VALUES
           (@reqeustID
           ,63
           ,null
           ,0
           , @oldTelephone
           , @newTelephone
           ,(select CustomerID from CRM.dbo.Customer where ID = @CustomerID)
           ,null
           ,@STOP_DATE
           ,@xml)

		   
		    -- request payment

	delete #EXPENSE
	insert into #EXPENSE select * , ROW_NUMBER() OVER(ORDER BY ID ASC) AS Row from EXPENSE where ID_SUBSCRI = (select ID from #Subscrib2 where Row = 1) and (ID_EXPTYPE = 14 or ID_EXPTYPE = 18) 
	 
	set @CountEX = (select Count(*) from #EXPENSE)
	set @iEX = 1;
    WHILE @iEX <=  @CountEX
    BEGIN



	select @p_ID_DATE = E1.PAY_DATE  , @ID_S = E1.ID from #EXPENSE as E1 where E1.[Row] = @iEX
	BEGIN TRY
	set @Pay_DATE =( CRM.[dbo].[sh2mi](STUFF(STUFF(@p_ID_DATE , 5 ,0,'/') , 8 , 0 ,'/')));
	END TRY
    BEGIN CATCH
    END CATCH
	if(@Pay_DATE is null) begin set @Pay_DATE = CAST('1900-01-01' as smalldatetime) end 

	if exists(select *  from EXPENSE as E1 join MALI on E1.ID_EXPTYPE = MALI.ID_EXPTYPE 
		                 join Salas.dbo.EXPTYPE on EXPTYPE.ID = E1.ID_EXPTYPE
		                 where E1.ID = @ID_S  and E1.PAY_DATE <= MALI.STOP_DATE and E1.PAY_DATE >= MALI.START_DATE and EXPTYPE.ID in(18 , 14))
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
		   join Salas.dbo.EXPTYPE on EXPTYPE.ID = E1.ID_EXPTYPE
		   join CRM.dbo.BaseCost as B on B.KerID = MALI.ID
		   where E1.ID = @ID_S and E1.PAY_DATE <= MALI.STOP_DATE and E1.PAY_DATE >= MALI.START_DATE and EXPTYPE.ID in(18 , 14)

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
		   join KermanshahNew.dbo.EXPTYPE on EXPTYPE.ID = E1.ID_EXPTYPE
		   join CRM.dbo.BaseCost as B on EXPTYPE.NAME = B.Title
		   where E1.ID = @ID_S and B.KerID = -1 and EXPTYPE.ID in(18 , 14)
    END


	 SET @iEX = @iEX + 1
     END


		--   	END TRY
   -- BEGIN CATCH
	 --  insert into EslamAbad.dbo.requestLog values(@reqeustID , (select ERROR_MESSAGE()));
    --END CATCH

	

	fetch next from subscrib_cursor into  @ID , @ID_FINANCE , @oldTelephone , @S_START_DATE2
	end
	close subscrib_cursor;
	deallocate subscrib_cursor;


