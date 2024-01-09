USE songhor
GO

declare @CITY int = 14 ;

--delete [CRM].dbo.RequestLog where RequestID = -1

--delete [CRM].dbo.ChangeName

--delete [CRM].dbo.Request  where [CRM].dbo.Request.RequestTypeID = 28

declare @ID float
declare @ID_FINANCE bigint
declare @S_START_DATE  nvarchar(8)
declare @S_START_DATE2  nvarchar(8)
declare @reqeustID bigint;
declare @S_STOP_DATE nvarchar(max);
declare @oldTelephone bigint ;
declare @newTelephone bigint ;

declare @CenterID bigint   ;       
declare @CenterCode bigint ;       
declare @CustomerID bigint ;    




IF OBJECT_ID('tempdb..#Subscrib') IS NOT NULL
DROP TABLE #Subscrib

CREATE TABLE #Subscrib(
	[ID] [float] NULL,
	[ID_FINANCE] [float] NULL,
	[TEL] [nvarchar](4) NULL,
	[TEL_PISH] [nvarchar](6) NULL,
	[ALLOC_DATE] [nvarchar](8) NULL,
	[START_DATE2] [nvarchar](8) NULL,
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
	[IDID] [float] NULL,
	[TELE] [nvarchar](7) NULL,
	[NewTelephone] [bigint] NULL,
	[Row] [int] NULL
)

declare subscrib_cursor cursor read_only for select ID , ID_FINANCE , [START_DATE]  from Subscrib where ID_FREE = 10

OPEN subscrib_cursor;
   fetch next from subscrib_cursor into @ID , @ID_FINANCE , @S_START_DATE2
while @@FETCH_STATUS = 0
	Begin
--	BEGIN TRY
	delete #Subscrib
	insert into #Subscrib select * , 1 from Subscrib where ID = @ID
	insert into #Subscrib select top(1) * , 2 from Subscrib where ID_FINANCE = @ID_FINANCE and START_DATE > @S_START_DATE2

	set @CenterID = (select Top(1) [CRM].dbo.Center.ID from dbo.FIMARK join [CRM].dbo.Center on dbo.FIMARK.ID_MARKAZ = [CRM].dbo.Center.CenterCode where dbo.FIMARK.ID_FINANCE = @ID_FINANCE)
	set @CenterCode = (SELECT [CRM].dbo.City.Code  FROM [CRM].dbo.Center INNER JOIN [CRM].dbo.Region ON [CRM].dbo.Center.RegionID = [CRM].dbo.Region.ID INNER JOIN [CRM].dbo.City ON [CRM].dbo.Region.CityID = [CRM].dbo.City.ID WHERE [CRM].dbo.Center.ID = @CenterID)
	set @CustomerID = (select top(1) ID from [CRM].dbo.Customer where ElkaID = @ID_FINANCE  and kercity = @CITY and [CRM].dbo.Customer.KerStopDate = '99999999')


	 select  top(1) @ID = ID , @oldTelephone = @newTelephone  from #Subscrib as C where Row = 1
	 select  @newTelephone = @newTelephone from #Subscrib as C where  Row = 2

	 set @reqeustID = CAST((CAST(@ID AS nvarchar(max))+ CAST(@oldTelephone as nvarchar(max) ) + CAST(1 as varchar) + '82') as bigint)

	 set @S_START_DATE = (select START_DATE from #Subscrib where Row = 2)
    declare @START_DATE smalldatetime;
	BEGIN TRY
	set @START_DATE =( [CRM].[dbo].[sh2mi](STUFF(STUFF(@S_START_DATE , 5 ,0,'/') , 8 , 0 ,'/')));
	END TRY
    BEGIN CATCH
    END CATCH
	if @START_DATE is null begin set @START_DATE = CAST('1900-01-01' as smalldatetime) end

DECLARE @xml XML;
SELECT @xml = '<?xml version="1.0" encoding="UTF-8"?>
  <TransferTelephoneNoFeature xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <SpecialServiceIds/>
  <ClassTelephone></ClassTelephone>
</TransferTelephoneNoFeature>'


INSERT INTO [CRM].[dbo].[RequestLog] VALUES
           (-1
           ,82
           ,null
           ,0
           , @oldTelephone
           , @newTelephone
           ,(select CustomerID from CRM.dbo.Customer where ID = @CustomerID)
           ,null
           ,@START_DATE
           ,@xml)

		   update T set DischargeDate =@START_DATE  from CRM.dbo.Telephone as T where T.TelephoneNo = @oldTelephone


	--END TRY
    --BEGIN CATCH
	--insert into [EslamAbad].dbo.requestLog values(@reqeustID , (select ERROR_MESSAGE()));
    --END CATCH
	fetch next from subscrib_cursor into @ID , @ID_FINANCE , @S_START_DATE2
	end
	close subscrib_cursor;
	deallocate subscrib_cursor;


	


