USE Salas
GO
declare @CITY int = 7;
declare @ID float
declare @ID_FINANCE float
declare	@Telephone  bigint
declare @S_START_DATE  nvarchar(10)
declare @F_ID_DATE nvarchar(8)
declare	@VADIE  bigint
declare @reqeustID bigint ;
declare @VA_DATE nvarchar(8) ;
declare @Pay_DATE smalldatetime;
declare @IDmarkaz int;


delete [CRM].dbo.RequestPayment from [CRM].dbo.RequestPayment as RP 
                     INNER JOIN [CRM].dbo.Request as R on RP.RequestID = R.ID 
					 INNER JOIN  [CRM].dbo.BaseCost as BC on BC.ID = RP.BaseCostID
                     INNER JOIN  [CRM].dbo.Center ON R.CenterID = Center.ID 
                     INNER JOIN  [CRM].dbo.Region ON Center.RegionID = Region.ID 
                     INNER JOIN [CRM].dbo.City ON Region.CityID = City.ID
					 where  City.ID = @CITY and R.RequestTypeID = 1 and R.CreatorUserID is null and BC.IsDeposit = 1



IF OBJECT_ID('tempdb..#subscrib') IS NOT NULL
DROP TABLE #subscrib


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

;with CTE as ( SELECT * , ROW_NUMBER() OVER(PARTITION BY ID_FINANCE ORDER BY [START_DATE] ASC) AS Row FROM [dbo].[Subscrib]),CTE2 as (select * FROM CTE where Row = 1 )
insert into #subscrib select * from CTE2


declare subscrib_cursor cursor read_only for select S.ID , S.ID_FINANCE ,	S.NewTelephone , S.[START_DATE],f2.ID_MARKAZ  from #subscrib as S join FINANCE  as F ON S.ID_FINANCE = F.ID join fimark as f2 on f2.ID_FINANCE=f.id where F.VADIE <> 0

OPEN subscrib_cursor;
   fetch next from subscrib_cursor into @ID , @ID_FINANCE ,	@Telephone , @S_START_DATE,@IDmarkaz
while @@FETCH_STATUS = 0
	Begin
	BEGIN TRY

	
	set @reqeustID = CAST((CAST(STUFF( @S_START_DATE, 1, 2, '')  AS varchar)+ CAST(@ID as varchar )+CAST(@IDmarkaz as varchar ) +CAST(@city as varchar )) as bigint)
		
	--set @reqeustID = CAST((CAST(STUFF( @S_START_DATE, 1, 2, '')  AS varchar)+ CAST(@ID as varchar )+CAST(@IDmarkaz as varchar ) +CAST(12 as varchar )) as bigint)

	select @F_ID_DATE = ID_DATE , @VADIE = VADIE , @VA_DATE = VA_DATE from FINANCE where ID = @ID_FINANCE
	if(@VA_DATE <> 99999999 and @VA_DATE <> 13999999)
	begin

	BEGIN TRY
	set @Pay_DATE =( [CRM].[dbo].[sh2mi](STUFF(STUFF(@F_ID_DATE , 5 ,0,'/') , 8 , 0 ,'/')));
	END TRY
    BEGIN CATCH
    END CATCH
	if(@Pay_DATE is null) begin set @Pay_DATE = CAST('1900-01-01' as smalldatetime) end 

	if exists(select *  from [OldCustomerDate].[dbo].[VADIE] as V where V.VADIE = @VADIE  and @F_ID_DATE <= V.SP_DATE_SA and @F_ID_DATE >= V.ST_DATE_SA)
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
           ,V.VADIE --<AmountSum, bigint,>
           ,null --<PaymentFicheID, bigint,>
           ,null --<FicheNunmber, nvarchar(20),>
           ,null --<FicheDate, smalldatetime,>
           ,null --<RecieverPostOfficeCode, nvarchar(50),>
           ,null --<RecieverPostOfficeRecordNo, nvarchar(50),>
           ,null --<PaymentDate, smalldatetime,>
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
           from [OldCustomerDate].[dbo].[VADIE] as V join CRM.dbo.BaseCost as B on V.ID = B.KerID  where V.VADIE = @VADIE  and @F_ID_DATE <= V.SP_DATE_SA and @F_ID_DATE >= V.ST_DATE_SA and B.IsDeposit = 1

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
           ,@VADIE  --<AmountSum, bigint,>
           ,null --<PaymentFicheID, bigint,>
           ,null --<FicheNunmber, nvarchar(20),>
           ,null --<FicheDate, smalldatetime,>
           ,null --<RecieverPostOfficeCode, nvarchar(50),>
           ,null --<RecieverPostOfficeRecordNo, nvarchar(50),>
           ,null --<PaymentDate, smalldatetime,>
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
		   from [CRM].dbo.BaseCost as B where ID = 145
    END

	end
	END TRY
    BEGIN CATCH
	   insert into Salas.dbo.requestLog values(@reqeustID , (select ERROR_MESSAGE()));
    END CATCH
	
  fetch next from subscrib_cursor into  @ID , @ID_FINANCE ,	@Telephone , @S_START_DATE,@IDmarkaz
	end
	close subscrib_cursor;
	deallocate subscrib_cursor;
	Go

