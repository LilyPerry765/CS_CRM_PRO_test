USE Salas
GO

declare @CITY int =7;

delete [CRM].dbo.RequestLog from [CRM].dbo.RequestLog as Rl join [CRM].dbo.Request as R on RL.RequestID = R.ID INNER JOIN
                     [CRM].dbo.Center ON R.CenterID = Center.ID INNER JOIN
                     [CRM].dbo.Region ON Center.RegionID = Region.ID INNER JOIN
                     [CRM].dbo.City ON Region.CityID = City.ID
					 where  City.ID = @CITY and R.RequestTypeID = 32 and R.CreatorUserID is null

delete [CRM].dbo.TakePossession from [CRM].dbo.TakePossession as TP join [CRM].dbo.Request as R on TP.ID = R.ID INNER JOIN
                     [CRM].dbo.Center ON R.CenterID = Center.ID INNER JOIN
                     [CRM].dbo.Region ON Center.RegionID = Region.ID INNER JOIN
                     [CRM].dbo.City ON Region.CityID = City.ID
					 where  City.ID = @CITY and R.RequestTypeID = 32 and R.CreatorUserID is null

delete [CRM].dbo.RequestPayment from [CRM].dbo.RequestPayment as RP join [CRM].dbo.Request as R on RP.RequestID = R.ID INNER JOIN
                     [CRM].dbo.Center ON R.CenterID = Center.ID INNER JOIN
                     [CRM].dbo.Region ON Center.RegionID = Region.ID INNER JOIN
                     [CRM].dbo.City ON Region.CityID = City.ID
					 where  City.ID = @CITY and R.RequestTypeID = 32 and R.CreatorUserID is null

delete [CRM].dbo.Request From [CRM].dbo.Request as R INNER JOIN
                     [CRM].dbo.Center ON R.CenterID = Center.ID INNER JOIN
                     [CRM].dbo.Region ON Center.RegionID = Region.ID INNER JOIN
                     [CRM].dbo.City ON Region.CityID = City.ID
					 where  City.ID = @CITY and R.RequestTypeID = 32 and R.CreatorUserID is null

declare @CityCode bigint ;
set @CityCode = (select C.Code from CRM.dbo.City as C where C.ID = @CITY )

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
	[ID_FINANCE]  bigint NULL,
	[ID_FREE]  int NULL,
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

;with CTEFIMARK as ( SELECT * , ROW_NUMBER() OVER(PARTITION BY ID_FINANCE ORDER BY STOP_DATE ASC) AS Row FROM [dbo].[FIMARK]),CTEFIMARK2 as (select * FROM CTEFIMARK where Row = 1 )


insert into #FIMARK select * from CTEFIMARK2

;with CTE as ( SELECT * FROM [dbo].[Subscrib] where STOP_DATE <> 99999999 and ID_FREE in (2, 3, 4, 5, 6, 7, 13))

insert into #Data 
(
	[ID],
    [Telephone],
	[CenterID],
	[OrgInsertDate],
	[OrgStartDate],
	[OrgEndDate],
	[ID_FINANCE],
	[ID_FREE],
	[status],
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
CAST((CAST(STUFF( c2.START_DATE, 1, 2, '') AS nvarchar(max))+ CAST(c2.id as nvarchar(7) ) + '32' + CAST(@city as nvarchar ) ) as bigint), 
NewTelephone,
Center.ID,
fi.ID_DATE,
c2.START_DATE,
C2.STOP_DATE,
fi.ID,
c2.ID_FREE,
181,
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
from CTE as c2 join #FIMARK as f2 on c2.ID_FINANCE = f2.ID_FINANCE join [CRM].dbo.Center as Center on f2.ID_MARKAZ = Center.CenterCode join FINANCE as fi on fi.ID = c2.ID_FINANCE


update D set CustomerID_FOLD = C.ID  from #Data as D join PERSONAL as C on C.ID_FINANCE = D.ID_FINANCE   where  D.OrgStartDate >= c.STOP_DATE and D.OrgEndDate <= c.STOP_DATE

update D set CustomerID_FOLD = C.ID  from #Data as D join PERSONAL as C on C.ID_FINANCE = D.ID_FINANCE   where D.CustomerID is null


update D set D.EndDate = [CRM].[dbo].[sh2mi]((STUFF(STUFF(D.OrgEndDate, 5 ,0,'/') , 8 , 0 ,'/'))) from #Data  as D where Len(D.OrgEndDate) = 8 and ISNUMERIC(D.OrgEndDate) = 1
update D set D.EndDate    = '1900-01-01' from #Data  as D where D.EndDate is null


update D set D.AdressID  = A.ID , D.PostalCode = A.PostalCode from #Data  as D join [CRM].dbo.Address as A on A.ElkaID = D.ID_FINANCE and A.CenterID = D.CenterID  join [CRM].dbo.Center on A.CenterID = [CRM].dbo.Center.ID INNER JOIN [CRM].dbo.Region ON [CRM].dbo.Center.RegionID = [CRM].dbo.Region.ID INNER JOIN [CRM].dbo.City ON [CRM].dbo.Region.CityID = [CRM].dbo.City.ID  WHERE [CRM].dbo.City.ID = @CITY
update D set D.AdressID    = 600308 from #Data  as D where D.AdressID is null--ادرس نامشخص

update D set D.CustomerID  = C.ID from #Data  as D join CRM.dbo.Customer C on D.CustomerID_FOLD = C.KerID and C.kercity = @CITY
update D set D.CustomerID    = 656783 from #Data  as D where D.CustomerID is null--مشترک نامشخص

update D set D.SwitchID  = S.ID from #Data  as D join CRM.dbo.Telephone as te on te.TelephoneNo = D.Telephone join CRM.dbo.SwitchPrecode as SP on te.SwitchPrecodeID = SP.ID join CRM.dbo.switch as S on S.ID = SP.SwitchID
update D set D.SwitchID    = 0 from #Data  as D where D.SwitchID is null--

delete #Data where Row = 2
--select * from crm.dbo.telephone where telephoneno=(select Telephone from #Data)

INSERT INTO [CRM].[dbo].[Request] select 
D.ID , --[ID]
D.Telephone,
null,
null,
32,
D.CenterID,
D.CustomerID,
D.EndDate,
null, --[RequestLetterNo] ,
null, --[RequestLetterDate],
null, --[RequesterName],
null, --[RequestPaymentTypeID] ,
null, --[RepresentitiveNo],
null, --[RepresentitiveDate],
null,  --[RepresentitiveExpireDate],
D.status,
D.EndDate,
null, --[ModifyDate] ,
D.EndDate, --EndDate
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
from #Data as D

INSERT INTO [CRM].[dbo].[TakePossession] select
           
		   D.ID --<ID, bigint,>
           ,null --<TakePossessionLetterNo, nvarchar(50),>
           ,null --<TakePossessionLetterDate, smalldatetime,>
           ,null --<LegalOfficeLetterNo, nvarchar(50),>
           ,null --<LegalOfficeLetterDate, smalldatetime,>
           ,null --<TakePossessionInsertDate, smalldatetime,>
           ,null --<CounterNoTakePossession, nvarchar(50),>
           ,null --<NoticeID, bigint,>
           ,null --<TakePossessionDate, smalldatetime,>
           ,D.ID_FREE --<CauseOfTakePossessionID, int,>
           ,null --<TakePossessionReportTo118ID, bigint,>
           ,1    --<Status, tinyint,>
           ,D.EndDate --<InsertDate, smalldatetime,>
           ,null --<DescriptionDischarge, nvarchar(200),>
           ,null --<DischargLeterNo, nvarchar(50),>
           ,null --<AbonmanDept, nvarchar(50),>
           ,null --<Credit, nvarchar(50),>
           ,null --<BuchtID, bigint,>
           ,null --<SwitchPortID, int,>
           ,null --<OldTelephone, bigint,>
           ,D.AdressID --<InstallAddressID, bigint,>
           ,D.AdressID --<CorrespondenceAddressID, bigint,>
           ,null --<CustomerID, bigint,>
           ,null --<CabinetInputID, bigint,>
           ,null --<PostContactID, bigint,>
		   from #Data as D



INSERT INTO [CRM].[dbo].[RequestLog] select
           
		   D.ID
           ,32
           ,null
           ,0
           , D.Telephone
           ,null
           ,D.CustomerID
           ,null
           ,D.EndDate
           ,'<?xml version="1.0" encoding="UTF-8"?>
<DischargeTelephone xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <TelephoneNo>' + Cast(D.Telephone as varchar)+ '</TelephoneNo>
  <PortID>'+Cast(D.SwitchID as varchar)+'</PortID>
  <BuchtID></BuchtID>
  <DischargLetterNo></DischargLetterNo>
  <CustomerID></CustomerID>
  <InstallAddressID>'+Cast(D.AdressID as varchar)+'</InstallAddressID>
  <CorrespondenceAddressID>'+ Cast(D.AdressID as varchar) +'</CorrespondenceAddressID>
  <PostContactID></PostContactID>
  <CabinetInputID></CabinetInputID>
  <CenterID>'+Cast(D.CenterID as varchar)+'</CenterID>
</DischargeTelephone>' 
		   from #Data as D



update T set DischargeDate = (select top(1) r.InsertDate from CRM.dbo.Request as r where r.TelephoneNo = T.TelephoneNo and r.RequestTypeID = 32 order by r.InsertDate desc) from CRM.dbo.Telephone as T
update T set InitialDischargeDate = (select top(1) r.InsertDate from CRM.dbo.Request as r where r.TelephoneNo = T.TelephoneNo and r.RequestTypeID = 32 order by r.InsertDate desc) from CRM.dbo.Telephone as T


use crm
go
update crm.dbo.Telephone set Telephone.CauseOfTakePossessionID = TakePossession.CauseOfTakePossessionID  from crm.dbo.TakePossession join crm.dbo.Request on Request.ID = TakePossession.ID 
join Telephone on Telephone.TelephoneNo = Request.TelephoneNo
join center as c on c.id=Telephone.centerid
join region as reg on reg.id=c.regionid
join city on city.id=reg.cityid
where Telephone.Status = 5
and city.id= 8 