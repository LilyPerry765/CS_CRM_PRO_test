USE Salas
GO


declare @CITY int = 7;

delete [CRM].dbo.RequestLog from [CRM].dbo.RequestLog as Rl join [CRM].dbo.Request as R on RL.RequestID = R.ID INNER JOIN
                     [CRM].dbo.Center ON R.CenterID = Center.ID INNER JOIN
                     [CRM].dbo.Region ON Center.RegionID = Region.ID INNER JOIN
                     [CRM].dbo.City ON Region.CityID = City.ID
					 where  City.ID = @CITY and R.RequestTypeID = 1 and R.CreatorUserID is null

delete [CRM].dbo.InstallRequest from [CRM].dbo.InstallRequest as IR join [CRM].dbo.Request as R on IR.RequestID = R.ID INNER JOIN
                     [CRM].dbo.Center ON R.CenterID = Center.ID INNER JOIN
                     [CRM].dbo.Region ON Center.RegionID = Region.ID INNER JOIN
                     [CRM].dbo.City ON Region.CityID = City.ID
					 where  City.ID = @CITY and R.RequestTypeID = 1 and R.CreatorUserID is null

delete [CRM].dbo.RequestPayment from [CRM].dbo.RequestPayment as RP join [CRM].dbo.Request as R on RP.RequestID = R.ID INNER JOIN
                     [CRM].dbo.Center ON R.CenterID = Center.ID INNER JOIN
                     [CRM].dbo.Region ON Center.RegionID = Region.ID INNER JOIN
                     [CRM].dbo.City ON Region.CityID = City.ID
					 where  City.ID = @CITY and R.RequestTypeID = 1 and R.CreatorUserID is null

delete [CRM].dbo.Request From [CRM].dbo.Request as R INNER JOIN
                     [CRM].dbo.Center ON R.CenterID = Center.ID INNER JOIN
                     [CRM].dbo.Region ON Center.RegionID = Region.ID INNER JOIN
                     [CRM].dbo.City ON Region.CityID = City.ID
					 where  City.ID = @CITY and R.RequestTypeID = 1 and R.CreatorUserID is null


declare @ID float
declare @ID_FINANCE float
declare	@Telephone  bigint
declare @S_START_DATE  nvarchar(8)
declare @reqeustID bigint ; 
declare @CenterID bigint ;  

declare @CustomerID bigint ;
declare @ID_TYPE bigint ;  
declare @ID_TYKARBR bigint ;  


declare @TelephoneType int;      
declare @TelephoneTypeGroup int; 

declare @AdressID bigint  ;
declare @F_VA_DATE bigint ;
declare @F_ID_DATE bigint ;

declare @ID_DATE smalldatetime; 
declare @VA_DATE smalldatetime;
declare @START_DATE smalldatetime;
DECLARE @xml XML
DECLARE @exCount int = 0
DECLARE @exi int = 0
declare @ID_S nvarchar(max); 
declare @p_ID_DATE nvarchar(max);
declare @Pay_DATE smalldatetime;


declare @CityCode bigint ;
set @CityCode = (select C.Code from CRM.dbo.City as C where C.ID = @CITY )


IF OBJECT_ID('tempdb..#subscrib') IS NOT NULL
DROP TABLE #subscrib
CREATE TABLE #subscrib(
	[ID] [float] NULL,
	[ID_FINANCE] [float] NULL,
	[TEL] [nvarchar](255) NULL,
	[TEL_PISH] [nvarchar](255) NULL,
	[ALLOC_DATE] [nvarchar](255) NULL,
	[START_DATE] [nvarchar](255) NULL,
	[STOP_DATE] [nvarchar](255) NULL,
	[ID_SYNTAX] [float] NULL,
	[ID_FREE] [float] NULL,
	[COD118] [float] NULL,
	[DC] [float] NULL,
	[TOGIF] [float] NULL,
	[ENTID] [float] NULL,
	[ID_TELTYPE] [float] NULL,
	[ID_FOLD] [float] NULL,
	[TELEE] [nvarchar](255) NULL,
	[newtelephone] [bigint] NULL
) 

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


IF OBJECT_ID('tempdb..#Data') IS NOT NULL
DROP TABLE #Data
CREATE TABLE #Data
(
	[ID] bigint NULL,
    [Telephone] bigint null,
	[CenterID] int NULL,
	[OrgInsertDate] [nvarchar](8) NULL,
	[OrgStartDate] [nvarchar](8) NULL,
	[OrgEndDate] [nvarchar](8) NULL,
	[insertDate] smalldatetime NULL,
	[StartDate] smalldatetime NULL,
	[EndDate] smalldatetime NULL,
	[CustomerID] bigint NULL,
	[status] int ,
	[OrgSubscribID] bigint NULL,
	[ID_FINANCE]  float NULL,
	[ID_TYPE] int NULL,
	[ID_TYKARBR] int NULL,
	[CustomerID_FOLD] bigint NULL,
	[TelephoneType]      int NULL,
	[TelephoneTypeGroup] int NULL,
	[AdressID] int null,
	[SwitchPreNo] int null,
	[SwitchID] int null,
	[SwitchPortID] int null,
	[PostalCode] nvarchar(20) null,
	[Row] [int] NULL
)

IF OBJECT_ID('tempdb..#FIMARK') IS NOT NULL
DROP TABLE #FIMARK
CREATE TABLE #FIMARK
(
	[ID_FINANCE] [float] NULL,
	[START_DATE] [nvarchar](8) NULL,
	[STOP_DATE] [nvarchar](8) NULL,
	[ID_MARKAZ] [smallint] NULL,
	[ID_CITY] [float] NULL,
	[ID_AGMAR] [smallint] NULL,
	[ENTID] [smallint] NULL,
	[ID_ADDRESS] [smallint] NULL,
	[ID_FOLD] [float] NULL,
	[Row] [int] NULL
)


IF OBJECT_ID('tempdb..#Payment') IS NOT NULL
DROP TABLE #Payment
CREATE TABLE #Payment
(
	[ReqeustID] bigint NULL,
	[BaseCostID] int NULL,
	[OrgPay_DATE] [nvarchar](8) NULL,
	[Pay_DATE] smalldatetime NULL,
    [Cost] bigint null,
	[PAY_MALI] bigint null,
	[ExpenseID] bigint null,
	[ID_EXPTYPE] int null
)


;with CTEFIMARK as ( SELECT * , ROW_NUMBER() OVER(PARTITION BY ID_FINANCE ORDER BY STOP_DATE ASC) AS Row FROM [dbo].[FIMARK]),CTEFIMARK2 as (select * FROM CTEFIMARK where Row = 1 )
insert into #FIMARK select * from CTEFIMARK2


;with CTE as ( SELECT * , ROW_NUMBER() OVER(PARTITION BY ID_FINANCE ORDER BY STOP_DATE ASC) AS Row FROM SUBSCRIB ),CTE2 as (select * FROM CTE where Row = 1  )


insert into #Data 
(
	[ID],
    [Telephone],
	[CenterID],
	[OrgInsertDate],
	[OrgStartDate],
	[OrgEndDate],
	[ID_FINANCE],
	[status],
	[OrgSubscribID],	
	[insertDate],
	[StartDate],
	[EndDate],
	[CustomerID],
	[ID_TYPE],
	[ID_TYKARBR],
	[CustomerID_FOLD],
	[TelephoneType],      
	[TelephoneTypeGroup],
	[AdressID],
	[SwitchPreNo],
	[SwitchID],
	[SwitchPortID],
	[PostalCode],
	[Row]
	
)
select 
CAST((CAST(STUFF( c2.START_DATE, 1, 2, '')  AS varchar)+ CAST(c2.ID as varchar )+CAST(f2.ID_MARKAZ as varchar ) +CAST(@city as varchar )) as bigint), 
NewTelephone,
Center.ID,
fi.ID_DATE,
c2.START_DATE,
fi.VA_DATE,
fi.ID,
1379,
c2.ID,
null,
null,
null,
null,
null,
null,
null,
null,
null,
null,
null,
null,
null,
null,
ROW_NUMBER() OVER(PARTITION BY c2.ID_FINANCE ORDER BY c2.STOP_DATE ASC) AS Row 
from CTE2 as c2 join #FIMARK as f2 on c2.ID_FINANCE = f2.ID_FINANCE join [CRM].dbo.Center as Center on f2.ID_MARKAZ = Center.CenterCode join FINANCE as fi on fi.ID = c2.ID_FINANCE



;with CTECustomer as ( SELECT * , ROW_NUMBER() OVER(PARTITION BY ID_FINANCE ORDER BY START_DATE ASC) AS Row FROM PERSONAL),CTECustomer2 as (select * FROM CTECustomer where Row = 1 )
update D set CustomerID_FOLD = C.ID , ID_TYPE= C.ID_TYPE , ID_TYKARBR = C.ID_TYKARBR from #Data as D join CTECustomer2 as C on C.ID_FINANCE = D.ID_FINANCE


update D set D.insertDate = [CRM].[dbo].[sh2mi]((STUFF(STUFF(D.OrgInsertDate, 5 ,0,'/') , 8 , 0 ,'/'))) from #Data  as D where Len(D.OrgInsertDate) = 8 and ISNUMERIC(D.OrgInsertDate) = 1
update D set D.StartDate = [CRM].[dbo].[sh2mi]((STUFF(STUFF(D.OrgStartDate, 5 ,0,'/') , 8 , 0 ,'/'))) from #Data  as D where Len(D.OrgStartDate) = 8 and ISNUMERIC(D.OrgStartDate) = 1
update D set D.EndDate = [CRM].[dbo].[sh2mi]((STUFF(STUFF(D.OrgEndDate, 5 ,0,'/') , 8 , 0 ,'/'))) from #Data  as D where Len(D.OrgEndDate) = 8 and ISNUMERIC(D.OrgEndDate) = 1

update D set D.insertDate = '1900-01-01' from #Data  as D where D.InsertDate is null
update D set D.StartDate  = '1900-01-01' from #Data  as D where D.StartDate is null
update D set D.EndDate    = '1900-01-01' from #Data  as D where D.EndDate is null

update D set D.CustomerID  = C.ID from #Data  as D join CRM.dbo.Customer C on D.CustomerID_FOLD = C.KerID and C.kercity = @CITY
update D set D.TelephoneType  = C.ID from #Data  as D join CRM.dbo.CustomerType C on D.ID_TYPE = C.Code
update D set D.TelephoneTypeGroup  = C.ID from #Data  as D join CRM.dbo.CustomerGroup C on D.ID_TYKARBR = C.KerID

update D set D.TelephoneType  = 209 from #Data  as D where D.TelephoneType is null



update D set D.AdressID  = A.ID , D.PostalCode = A.PostalCode from #Data  as D join [CRM].dbo.Address as A on A.ElkaID = D.ID_FINANCE and A.CenterID = D.CenterID  join [CRM].dbo.Center on A.CenterID = [CRM].dbo.Center.ID INNER JOIN [CRM].dbo.Region ON [CRM].dbo.Center.RegionID = [CRM].dbo.Region.ID INNER JOIN [CRM].dbo.City ON [CRM].dbo.Region.CityID = [CRM].dbo.City.ID  WHERE [CRM].dbo.City.ID = @CITY
update D set D.SwitchPreNo  = SP.SwitchPreNo from #Data  as D join CRM.dbo.Telephone as te on te.TelephoneNo = D.Telephone join CRM.dbo.SwitchPrecode as SP on te.SwitchPrecodeID = SP.ID
update D set D.SwitchID  = S.ID from #Data  as D join CRM.dbo.Telephone as te on te.TelephoneNo = D.Telephone join CRM.dbo.SwitchPrecode as SP on te.SwitchPrecodeID = SP.ID join CRM.dbo.switch as S on S.ID = SP.SwitchID
update D set D.SwitchPortID  = te.SwitchPortID from #Data  as D join CRM.dbo.Telephone as te on te.TelephoneNo = D.Telephone

--select SwitchPortID from #data
-------------------------------یافتن مقادیر تکراری---------------------
--select * from #Data 
--where Row = 2


--delete #Data where Row = 2
--delete #Data where ID is null

-- Reqeust Date
INSERT INTO [CRM].[dbo].[Request] select
DR.ID , --[ID]
DR.Telephone,
null,
null,
1,
DR.CenterID,
DR.CustomerID,
DR.insertDate,
null, --[RequestLetterNo] ,
null, --[RequestLetterDate],
null, --[RequesterName],
null, --[RequestPaymentTypeID] ,
null, --[RepresentitiveNo],
null, --[RepresentitiveDate],
null,  --[RepresentitiveExpireDate],
DR.status,
DR.StartDate ,
null, --[ModifyDate] ,
DR.EndDate ,
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
from #Data as DR

INSERT INTO [CRM].[dbo].[InstallRequest]
           SELECT
		    dr.ID --[RequestID]
           ,1   --,[SaleFicheID]
           ,1    --,[ChargingType]
           ,DR.TelephoneType --,[TelephoneType]
           ,DR.TelephoneTypeGroup --,[TelephoneTypeGroup]
           ,null --,[TelephoneForCharge]
           ,1    --,[PosessionType]
           ,0    --,[OrderType]
           ,null --,[DepositeNo]
           ,null --,[MoneyInTrustNo]
           ,null --,[Installdate]
           ,0    --,[RegisterAt118]
           ,null --,[NameTitleAt118]
           ,null --,[LastNameAt118]
           ,null --,[TitleAt118]
           ,1    --,[ClassTelephone]
           ,null --,[Uninstalldate]
           ,null --,[InstallRequestTypeID]
           ,null --,[LetterNumberOfReinstalling]
           ,null --,[LetterDateOfReinstall]
           ,null --,[ReasonReinstall]
           ,null --,[LicenseNumber]
           ,null --,[LicenseDate]
           ,null --,[Authorized]
           ,null --,[PassTelephone]
           ,null --,[CurrentTelephone]
           ,dr.AdressID --,[InstallAddressID]
           ,null --,[NearestTelephon]
           ,dr.AdressID --,[CorrespondenceAddressID]
           ,dr.insertDate --,[InsertDate]
           ,1 --,[Status]
           ,0 --,[IsADSL]
           ,dr.EndDate--,[InstallationDate]
		   ,0 --IsGSM
		   from #Data as DR

		  --select * from #data 
INSERT INTO [CRM].[dbo].[RequestLog]
		   select
		   DR.ID --[RequestID]
           ,1 --[RequestTypeID]
           ,null --[LogType]
           ,0 --[IsReject]
           ,DR.Telephone --[TelephoneNo]   
           ,null --[ToTelephoneNo]
           ,DR.CustomerID --[CustomerID]
           ,null --[UserID]
           ,DR.EndDate --[Date]
           ,'<?xml version="1.0" encoding="UTF-8"?>
                   <Dayeri xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                   <TelephoneNo>' +  Cast(DR.Telephone as varchar ) + ' </TelephoneNo>
                   <SwitchPrecode></SwitchPrecode>
                   <SwitchID></SwitchID>
                   <SwitchPort>'+ Cast(DR.SwitchPortID  as varchar ) +'</SwitchPort>
                   <SwitchPortNo>'+ Cast(DR.Telephone as varchar ) +'</SwitchPortNo>
                   <PostalCode>'+ Cast(DR.PostalCode as varchar )+'</PostalCode>
                   <CustomerAddressID>'+ cast(DR.AdressID as varchar)    +'</CustomerAddressID>
                   <CustomerID></CustomerID>
                   </Dayeri>'  --[Description]
		   from #Data as DR
 --end request date


  --telephone insert DataUpdate


  update T  set InstallationDate = r.InsertDate
              ,CustomerTypeID  = ir.TelephoneType
			  ,CustomerGroupID = ir.TelephoneTypeGroup
			  ,InitialInstallationDate=r.InsertDate
 from CRM.dbo.Telephone as T join  CRM.dbo.Request as r on t.TelephoneNo = r.TelephoneNo join CRM.dbo.InstallRequest as ir on ir.RequestID = r.ID
     join [CRM].dbo.Center ON  r.CenterID= Center.ID INNER JOIN
     [CRM].dbo.Region ON Center.RegionID = Region.ID INNER JOIN
     [CRM].dbo.City ON Region.CityID = City.ID
	where  r.RequestTypeID = 1 and city.id=@city

 -----


-- Reqeust Payment

insert into #Payment select D.ID , null , ex.Pay_DATE , null , null , ex.PAY_MALI , ex.ID , ex.ID_EXPTYPE from #Data as D join EXPENSE as EX ON EX.ID_FINANCE = D.ID_FINANCE where ex.ID_EXPTYPE IN (1,2,3,4,5,14,15,19,20,21)

update E set E.Pay_DATE = [CRM].[dbo].[sh2mi]((STUFF(STUFF(E.OrgPay_DATE, 5 ,0,'/') , 8 , 0 ,'/'))) from #Payment  as E where Len(E.OrgPay_DATE) = 8 and ISNUMERIC(E.OrgPay_DATE) = 1
update E set E.Pay_DATE = '1900-01-01' from #Payment  as E where E.Pay_DATE is null

update E1 set E1.BaseCostID = B.ID , E1.Cost = B.Cost from #Payment as E1
		   join Salas.dbo.EXPTYPE on EXPTYPE.ID = E1.ID_EXPTYPE
		   join CRM.dbo.BaseCost as B on EXPTYPE.NAME = B.Title
		   where B.RequestTypeID = 1 and B.KerID = -1

update E1 set E1.BaseCostID = B.ID , E1.Cost = B.Cost from #Payment as E1  join MALI on E1.ID_EXPTYPE = MALI.ID_EXPTYPE 
		   join Salas.dbo.EXPTYPE on EXPTYPE.ID = E1.ID_EXPTYPE
		   join CRM.dbo.BaseCost as B on B.KerID = MALI.ID
		   where E1.OrgPay_DATE <= MALI.STOP_DATE and E1.OrgPay_DATE >= MALI.START_DATE

delete #Payment from #Payment as P where P.ReqeustID not in (select R.ID From [CRM].dbo.Request as R INNER JOIN
                     [CRM].dbo.Center ON R.CenterID = Center.ID INNER JOIN
                     [CRM].dbo.Region ON Center.RegionID = Region.ID INNER JOIN
                     [CRM].dbo.City ON Region.CityID = City.ID
					 where  City.ID = @CITY and R.RequestTypeID = 1)

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
           ,P.ReqeustID --<RequestID, bigint,>
           ,p.BaseCostID      --<BaseCostID, int,>
           ,null --<OtherCostID, int,>
           ,null --<PaymentWay, tinyint,>
           ,null --<BankID, int,>
           ,null --<BankBranchID, int,>
           ,1    --<PaymentType, tinyint,>
           ,P.Cost --<Cost, bigint,>
           ,null --<Abonman, int,>
           ,null --<Tax, int,>
           ,P.PAY_MALI  --<AmountSum, bigint,>
           ,null --<PaymentFicheID, bigint,>
           ,null --<FicheNunmber, nvarchar(20),>
           ,null --<FicheDate, smalldatetime,>
           ,null --<RecieverPostOfficeCode, nvarchar(50),>
           ,null --<RecieverPostOfficeRecordNo, nvarchar(50),>
           ,null --<PaymentDate, smalldatetime,>
           ,1 --<IsPaid, bit,>
           ,null --<IsAccepted, bit,>
           ,P.Pay_DATE --<InsertDate, smalldatetime,>
           ,null --<AccountNo, char(20),>
           ,0 --<IsKickedBack, bit,>
           ,null --<BillID, nvarchar(50),>
           ,null --<PaymentID, nvarchar(50),>
           ,null --<OrderID, nvarchar(50),>
           ,null --<DocumentsFileID, uniqueidentifier,>
           ,null --<UserID, int,>
           ,null --<FactorNumber, int,>
		   from #Payment as P
-- end request payment



