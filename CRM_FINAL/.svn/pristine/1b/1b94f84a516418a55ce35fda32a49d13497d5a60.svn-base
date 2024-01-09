USE OldCustomerDate
GO

declare @CITY int = 1;

delete [CRM].dbo.RequestLog from [CRM].dbo.RequestLog as Rl join [CRM].dbo.Request as R on RL.RequestID = R.ID INNER JOIN
                     [CRM].dbo.Center ON R.CenterID = Center.ID INNER JOIN
                     [CRM].dbo.Region ON Center.RegionID = Region.ID INNER JOIN
                     [CRM].dbo.City ON Region.CityID = City.ID
					 where  City.ID = @CITY and R.RequestTypeID = 97 and R.CreatorUserID is null

delete [CRM].dbo.TitleIn118 from [CRM].dbo.TitleIn118 as IR join [CRM].dbo.Request as R on IR.ID = R.ID INNER JOIN
                     [CRM].dbo.Center ON R.CenterID = Center.ID INNER JOIN
                     [CRM].dbo.Region ON Center.RegionID = Region.ID INNER JOIN
                     [CRM].dbo.City ON Region.CityID = City.ID
					 where  City.ID = @CITY and R.RequestTypeID = 97  and R.CreatorUserID is null


delete [CRM].dbo.Request From [CRM].dbo.Request as R INNER JOIN
                     [CRM].dbo.Center ON R.CenterID = Center.ID INNER JOIN
                     [CRM].dbo.Region ON Center.RegionID = Region.ID INNER JOIN
                     [CRM].dbo.City ON Region.CityID = City.ID
					 where  City.ID = @CITY and R.RequestTypeID = 97 and R.CreatorUserID is null


declare @ID float;
declare @ID_FINANCE float;
declare	@Telephone  bigint;

declare @COD118 int;
declare @TITL nvarchar(max);


declare @D_STOP_DATE nvarchar(8);
declare @D_START_DATE nvarchar(8);

declare @STOP_DATE smalldatetime;
declare @START_DATE smalldatetime;


declare @reqeustID bigint;
declare @CenterID bigint;
declare @CenterCode bigint; 
declare @CustomerID bigint;
declare @ID_SUBSCRI bigint;
declare @ID_PERSON bigint;


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
    [ID_FINANCE] [nvarchar](10) NULL,
	[TEL] [nvarchar](4) NULL,
	[TEL_PISH] [nvarchar](3) NULL,
	[ALLOC_DATE] [nvarchar](8) NULL,
	[START_DATE] [nvarchar](8) NULL,
	[STOP_DATE] [nvarchar](8) NULL,
	[ID_SYNTAX] [nvarchar](10) NULL,
	[ID_FREE] [nvarchar](10) NULL,
	[COD118] [nvarchar](10) NULL,
	[DC] [nvarchar](10) NULL,
	[TOGIF] [nvarchar](10) NULL,
	[ID_TELTYPE] [nvarchar](10) NULL,
	[ID_FOLD] [nvarchar](10) NULL,
	[ROW] int
)

declare subscrib_cursor cursor read_only for select ID , ID_FINANCE , NewTelephone ,TITL, COD118 , [START_DATE] , STOP_DATE , [ID_PERSON] from OldCustomerDate.dbo.DATA118 where STOP_DATE <> '99999999'

OPEN subscrib_cursor;
   fetch next from subscrib_cursor into @ID , @ID_FINANCE , @Telephone , @TITL , @COD118 , @D_START_DATE , @D_STOP_DATE , @ID_PERSON
while @@FETCH_STATUS = 0
	Begin
	--BEGIN TRY

	set @reqeustID = CAST((CAST(STUFF( @D_START_DATE, 1, 2, '') AS nvarchar(max))+ CAST(@ID as nvarchar(max) ) + '97') as bigint)

	set @CenterID = (select Top(1) [CRM].dbo.Center.ID from FIMARK join [CRM].dbo.Center on FIMARK.ID_MARKAZ = [CRM].dbo.Center.CenterCode where FIMARK.ID_FINANCE = @ID_FINANCE)
	set @CenterCode = (SELECT [CRM].dbo.City.Code  FROM [CRM].dbo.Center INNER JOIN [CRM].dbo.Region ON [CRM].dbo.Center.RegionID = [CRM].dbo.Region.ID INNER JOIN [CRM].dbo.City ON [CRM].dbo.Region.CityID = [CRM].dbo.City.ID WHERE [CRM].dbo.Center.ID = @CenterID)
	set @CustomerID = (select ID from [CRM].dbo.Customer as C where C.KerID = @ID_PERSON and kercity = @CITY)
	
	BEGIN TRY
	set @START_DATE   = ([CRM].[dbo].[sh2mi](STUFF(STUFF(@D_START_DATE , 5 ,0,'/') , 8 , 0 ,'/')));
	END TRY
    BEGIN CATCH
    END CATCH
	if  @START_DATE is null begin set @START_DATE = CAST('1900-01-01' as smalldatetime) end


    BEGIN TRY
    set @STOP_DATE   = ([CRM].[dbo].[sh2mi](STUFF(STUFF(@D_STOP_DATE , 5 ,0,'/') , 8 , 0 ,'/')));
	END TRY
    BEGIN CATCH
    END CATCH
    if  @STOP_DATE is null begin set @STOP_DATE = CAST('1900-01-01' as smalldatetime) end

	

INSERT INTO [CRM].[dbo].[Request] VALUES(
 @reqeustID , --[ID]
@Telephone,
 null,
 null,
 97,
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
 5614,
 @STOP_DATE,
 null, --[ModifyDate] ,
 @STOP_DATE,
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

 INSERT INTO [CRM].[dbo].[TitleIn118]
     VALUES
           (@reqeustID --<ID, bigint,>
           ,@Telephone --<TelephoneNo, bigint,>
           ,null --<NameTitleAt118, nvarchar(50),>
           ,null --<LastNameAt118, nvarchar(50),>
           ,@TITL -- <TitleAt118, nvarchar(50),>
           ,97 -- <Status, int,>
           ,@START_DATE --<Date, smalldatetime,>
		   ,@STOP_DATE
		   ,@COD118
		   )

DECLARE @xml XML
SELECT @xml = '<?xml version="1.0" encoding="UTF-8"?>
<Title118 xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Date>' + Cast(@STOP_DATE as varchar)+ '</Date>
  <Status>97</Status>
</Title118>'

    INSERT INTO [CRM].[dbo].[RequestLog] VALUES
           (@reqeustID
           ,97
           ,null
           ,0
           , Cast(@CenterCode as nvarchar(3)) + Cast(@Telephone as nvarchar(max))
           ,null
           ,(select CustomerID from CRM.dbo.Customer where ID = @CustomerID)
           ,null
           ,@STOP_DATE
           ,@xml)
	--END TRY
 --   BEGIN CATCH
	--insert into [dalaho].dbo.requestLog values(@reqeustID , (select ERROR_MESSAGE()));
 --   END CATCH
 
	fetch next from subscrib_cursor into  @ID , @ID_FINANCE , @Telephone , @TITL , @COD118 , @D_START_DATE , @D_STOP_DATE , @ID_PERSON
	end
	close subscrib_cursor;
	deallocate subscrib_cursor;


