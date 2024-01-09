USE Salas
GO

--with cte as 
--(
--SELECT [ID]
--      ,[ID_SUBSCRI]
--      ,[ID_FINANCE]
--      ,[DCDATE]
--      ,[CDATE]
--      ,[TEL_PISH]
--      ,[TEL]
--      ,[ENTID]
--      ,[Newtelephone]
--	  ,row_number ()over (partition by ID_SUBSCRI order by ID_FINANCE  ) as ro
--  FROM [Salas].[dbo].[ZEROO2]
--  )
--delete  cte where ro>1


--update [Salas].[dbo].[ZEROO2] set newtelephone=s.newtelephone
--  FROM [Salas].[dbo].[ZEROO2] as z
--  join [Salas].[dbo].[SUBSCRIB] as s
--  on z.ID_FINANCE=s.ID_FINANCE



declare @CITY int = 7;

delete [CRM].dbo.RequestLog from [CRM].dbo.RequestLog as Rl join [CRM].dbo.Request as R on RL.RequestID = R.ID INNER JOIN
                     [CRM].dbo.Center ON R.CenterID = Center.ID INNER JOIN
                     [CRM].dbo.Region ON Center.RegionID = Region.ID INNER JOIN
                     [CRM].dbo.City ON Region.CityID = City.ID
					 where  City.ID = @CITY and R.RequestTypeID = 34 and R.CreatorUserID is null

delete [CRM].dbo.ZeroStatus  from [CRM].dbo.ZeroStatus  as CE join [CRM].dbo.Request as R on CE.ID = R.ID INNER JOIN
                     [CRM].dbo.Center ON R.CenterID = Center.ID INNER JOIN
                     [CRM].dbo.Region ON Center.RegionID = Region.ID INNER JOIN
                     [CRM].dbo.City ON Region.CityID = City.ID
					 where  City.ID = @CITY and R.RequestTypeID = 34 and R.CreatorUserID is null

delete [CRM].dbo.RequestPayment from [CRM].dbo.RequestPayment as RP join [CRM].dbo.Request as R on RP.RequestID = R.ID INNER JOIN
                     [CRM].dbo.Center ON R.CenterID = Center.ID INNER JOIN
                     [CRM].dbo.Region ON Center.RegionID = Region.ID INNER JOIN
                     [CRM].dbo.City ON Region.CityID = City.ID
					 where  City.ID = @CITY and R.RequestTypeID = 34 and R.CreatorUserID is null

delete [CRM].dbo.Request From [CRM].dbo.Request as R INNER JOIN
                     [CRM].dbo.Center ON R.CenterID = Center.ID INNER JOIN
                     [CRM].dbo.Region ON Center.RegionID = Region.ID INNER JOIN
                     [CRM].dbo.City ON Region.CityID = City.ID
					 where  City.ID = @CITY and R.RequestTypeID = 34 and R.CreatorUserID is null



declare @ID float;
declare @ID_FINANCE float;
declare	@Telephone  bigint;


declare @Z_DCDATE nvarchar(8);
declare @Z_CDATE nvarchar(8);

declare @STOP_DATE smalldatetime;
declare @START_DATE smalldatetime;


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

declare subscrib_cursor cursor read_only for select ID , ID_SUBSCRI, ID_FINANCE , DCDATE , CDATE , NewTelephone from ZEROO2

OPEN subscrib_cursor;
   fetch next from subscrib_cursor into @ID ,@ID_SUBSCRI, @ID_FINANCE ,@Z_DCDATE , @Z_CDATE , @Telephone
while @@FETCH_STATUS = 0
	Begin
	--BEGIN TRY

	set @reqeustID = CAST((CAST(STUFF( @Z_DCDATE, 1, 2, '') AS nvarchar(max))+ CAST(@ID_SUBSCRI as nvarchar(max) ) + '34'+cast (77 as nvarchar (max))) as bigint)

	set @CenterID = (select Top(1) [CRM].dbo.Center.ID from FIMARK join [CRM].dbo.Center on FIMARK.ID_MARKAZ = [CRM].dbo.Center.CenterCode where FIMARK.ID_FINANCE = @ID_FINANCE)
	set @CenterCode = (SELECT [CRM].dbo.City.Code  FROM [CRM].dbo.Center INNER JOIN [CRM].dbo.Region ON [CRM].dbo.Center.RegionID = [CRM].dbo.Region.ID INNER JOIN [CRM].dbo.City ON [CRM].dbo.Region.CityID = [CRM].dbo.City.ID WHERE [CRM].dbo.Center.ID = @CenterID)
	set @CustomerID = (select top(1) ID from [CRM].dbo.Customer as C where C.ElkaID = @ID_FINANCE and kercity = @CITY and C.KerStopDate = '99999999')
	
	BEGIN TRY
	set @START_DATE   = ([CRM].[dbo].[sh2mi](STUFF(STUFF(@Z_DCDATE , 5 ,0,'/') , 8 , 0 ,'/')));
	END TRY
    BEGIN CATCH
    END CATCH
	if  @START_DATE is null begin set @START_DATE = CAST('1900-01-01' as smalldatetime) end

	if (@Z_CDATE <> '99999999')
	BEGIN
       BEGIN TRY
	   set @STOP_DATE   = ([CRM].[dbo].[sh2mi](STUFF(STUFF(@Z_CDATE , 5 ,0,'/') , 8 , 0 ,'/')));
	   END TRY
       BEGIN CATCH
       END CATCH
        if  @STOP_DATE is null begin set @STOP_DATE = CAST('1900-01-01' as smalldatetime) end
	 END
	ELSE BEGIN
	  set @STOP_DATE = null;
	  END
	

INSERT INTO [CRM].[dbo].[Request] VALUES(
 @reqeustID , --[ID]
@Telephone,
 null,
 null,
 34,
 @CenterID,
 @CustomerID,
 @START_DATE,
 null, --[RequestLetterNo] ,
 null, --[RequestLetterDate],
 null, --[RequesterName],
 null, --[RequestPaymentTypeID] ,
 null, --[RepresentitiveNo],
 null, --[RepresentitiveDate],
 null,  --[RepresentitiveExpireDate],
 193,
 @START_DATE,
 null, --[ModifyDate] ,
 @START_DATE,
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

INSERT INTO [CRM].[dbo].[ZeroStatus]
     VALUES
           (@reqeustID --<ID, bigint,>
           ,3 --<ClassTelephone, tinyint,>
           ,1 --<OldClassTelephone, tinyint,>
           ,null --<ZeroTypeCloseStatusReportID, bigint,>
           ,null --<ZeroTypeOpenStatusReportID, bigint,>
           ,null --<InstallHour, char(10),>
           ,@START_DATE --<InstallDate, smalldatetime,>
		   ,@START_DATE
		   )

DECLARE @xml XML
SELECT @xml = '<?xml version="1.0" encoding="UTF-8"?>
<OpenAndCloseZero xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <ClassTelephone>3</ClassTelephone>
</OpenAndCloseZero>'

    INSERT INTO [CRM].[dbo].[RequestLog] VALUES
           (@reqeustID
           ,34
           ,null
           ,0
           , @Telephone
           ,null
           ,(select CustomerID from CRM.dbo.Customer where ID = @CustomerID)
           ,null
           ,@START_DATE
           ,@xml)

   update [CRM].dbo.Telephone set ClassTelephone = 3 where TelephoneNo = @Telephone and CenterID = @CenterID


     -- request payment

	delete #EXPENSE
	insert into #EXPENSE select * , ROW_NUMBER() OVER(ORDER BY ID ASC) AS Row from EXPENSE where ID_SUBSCRI = @ID_SUBSCRI and (ID_EXPTYPE = 14)

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
		                 join [Salas].dbo.EXPTYPE on EXPTYPE.ID = E1.ID_EXPTYPE
		                 where E1.ID = @ID_S  and E1.PAY_DATE <= MALI.STOP_DATE and E1.PAY_DATE >= MALI.START_DATE and EXPTYPE.ID in(16))
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
		   join [Salas].dbo.EXPTYPE on EXPTYPE.ID = E1.ID_EXPTYPE
		   join CRM.dbo.BaseCost as B on B.KerID = MALI.ID
		   where E1.ID = @ID_S and E1.PAY_DATE <= MALI.STOP_DATE and E1.PAY_DATE >= MALI.[START_DATE] and EXPTYPE.ID in(16)

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
		   join [Salas].dbo.EXPTYPE on EXPTYPE.ID = E1.ID_EXPTYPE
		   join CRM.dbo.BaseCost as B on EXPTYPE.NAME = B.Title
		   where E1.ID = @ID_S and B.KerID = -1 and EXPTYPE.ID in(16)
    END


	 SET @iEX = @iEX + 1
     END

	 -- request payment


	--END TRY
 --   BEGIN CATCH
	--insert into [SalasAbad].dbo.requestLog values(@reqeustID , (select ERROR_MESSAGE()));
 --   END CATCH
 
	fetch next from subscrib_cursor into @ID ,@ID_SUBSCRI, @ID_FINANCE ,@Z_DCDATE , @Z_CDATE , @Telephone
	end
	close subscrib_cursor;
	deallocate subscrib_cursor;


