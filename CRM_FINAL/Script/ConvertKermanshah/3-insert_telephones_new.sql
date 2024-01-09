﻿USE CRM
GO


declare @CITY int = 7;
DECLARE @Count int = 0
DECLARE @i int = 0
declare @from BIGINT
declare @to BIGINT  
declare @switchID INT  
declare @SwitchPreCode INT = 0 
declare @centerID INT
declare @CityCode SMALLINT;   

delete Telephone 
FROM         Switch INNER JOIN
                      Center ON Switch.CenterID = Center.ID INNER JOIN
                      Region ON Center.RegionID = Region.ID INNER JOIN
                      City ON Region.CityID = City.ID INNER JOIN
                      SwitchPrecode ON Switch.ID = SwitchPrecode.SwitchID AND Center.ID = SwitchPrecode.CenterID INNER JOIN
                      Telephone ON Center.ID = Telephone.CenterID AND SwitchPrecode.ID = Telephone.SwitchPrecodeID
WHERE     (City.ID = @CITY)


delete     SwitchPort
FROM         City INNER JOIN
                      Region ON City.ID = Region.CityID INNER JOIN
                      Center ON Region.ID = Center.RegionID INNER JOIN
                      Switch ON Center.ID = Switch.CenterID INNER JOIN
                      SwitchPort ON Switch.ID = SwitchPort.SwitchID
WHERE     (City.ID = @CITY)


--delete telephone from Telephone as t 
--join Center as c on c.id=t.CenterID
--join Region as r on r.id=c.RegionID
--join city on City.ID=r.CityID
--where City.id=14

--delete Telephone 


--delete     SwitchPort
--DBCC CHECKIDENT('SwitchPort', RESEED,0)


IF OBJECT_ID('tempdb..#Error') IS NOT NULL
DROP TABLE #Error
CREATE TABLE #Error (switchprecodeID int , error nvarchar(max))

IF OBJECT_ID('tempdb..#SwitchPrecode') IS NOT NULL
DROP TABLE #SwitchPrecode

CREATE TABLE #SwitchPrecode(
	[ID] [int],
	[CenterID] [int] NOT NULL,
	[SwitchID] [int] NOT NULL,
	[SwitchPreNo] [bigint] NOT NULL,
	[PreCodeType] [tinyint] NOT NULL,
	[Capacity] [int] NOT NULL,
	[OperationalCapacity] [int] NULL,
	[InstallCapacity] [int] NULL,
	[SpecialServiceCapacity] [int] NOT NULL,
	[DeploymentType] [tinyint] NOT NULL,
	[LinkType] [tinyint] NULL,
	[DorshoalNumberType] [tinyint] NULL,
	[Status] [tinyint] NOT NULL,
	[FromNumber] [bigint] NOT NULL,
	[ToNumber] [bigint] NOT NULL,
	[ROW] [int] NOT NULL
	)

IF OBJECT_ID('tempdb..#SwitchPort') IS NOT NULL
DROP TABLE #SwitchPort

CREATE TABLE #SwitchPort(
	[SwitchID] [int] NOT NULL,
	[PortNo] [bigint] NOT NULL,
	[MDFHorizentalID] [nvarchar](50) NULL,
	[Type] [bit] NULL,
	[Status] [tinyint] NOT NULL,
	[num] [bigint]
	)
	
    insert into #SwitchPrecode SELECT sw.* , ROW_NUMBER() OVER(ORDER BY sw.ID ASC) AS Row FROM dbo.SwitchPrecode as  sw join dbo.Center on sw.CenterID = dbo.Center.ID INNER JOIN dbo.Region ON dbo.Center.RegionID = dbo.Region.ID INNER JOIN dbo.City ON dbo.Region.CityID = dbo.City.ID WHERE dbo.City.ID = @CITY 

	set @Count = (select Count(*) from #SwitchPrecode)
	set @i = 1
    WHILE @i <=  @Count
    BEGIN -- while

	     BEGIN TRY

		    delete #SwitchPort
			select @SwitchPreCode = ID , @from   =FromNumber ,@to = ToNumber ,@switchID =SwitchID ,@centerID = CenterID FROM #SwitchPrecode WHERE [Row] = @i
			SET @CityCode = (SELECT  dbo.City.Code FROM City where City.ID = @CITY)
		    
            DECLARE @codePortNO bigint = CONVERT(char(2) , @CityCode) + CONVERT(NVARCHAR(max) , @from)
			DECLARE @MaxNum bigint = CONVERT(char(2) , @CityCode) + CONVERT(NVARCHAR(max) , @to)

	

             ;WITH CTE(SwitchID ,PortNo ,MDFHorizentalID ,Type ,Status , num)AS 
             (                                                                                           
             SELECT @switchID ,  @codePortNO , @codePortNO ,1 ,1 , @from
             UNION ALL
             SELECT SwitchID ,PortNo + 1 ,PortNo + 1 ,Type ,Status , num + 1
             FROM CTE
             WHERE PortNo < @MaxNum
             )
			 INSERT INTO #SwitchPort select * from CTE
			  OPTION(MAXRECURSION 0)

			  
			
			 insert into dbo.Telephone select PortNo, num , null ,@SwitchPreCode, 1 , null,null , null , 0 ,  NULL , @centerID , 0 ,0 , NULL , null ,NULL ,0 ,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL from #SwitchPort 
			 INSERT INTO [CRM].dbo.SwitchPort select SwitchID ,CAST( PortNo as nvarchar),MDFHorizentalID ,Type ,Status from #SwitchPort

			 update T set SwitchPortID = swi.ID from [CRM].dbo.Telephone as T join [CRM].dbo.SwitchPort as swi on T.TelephoneNo = swi.PortNo where swi.PortNo in (select PortNo from #SwitchPort)
			 END TRY
            BEGIN CATCH
              insert into #Error VALUES(@SwitchPreCode , (select ERROR_MESSAGE()) )
            END CATCH
			set @i = @i + 1;

END --while


select * from #Error


---------------------------------------------
-------------نام مرکز تغییر کند------------
---------------------------------------------

--------------- update gsm tel


update  telephone set UsageType=3
where SwitchPrecodeid in
(
	SELECT     SwitchPrecode.id
	FROM         City INNER JOIN
                      Region ON City.ID = Region.CityID INNER JOIN
                      Center ON Region.ID = Center.RegionID INNER JOIN
                      Switch ON Center.ID = Switch.CenterID INNER JOIN
                      SwitchPrecode ON Center.ID = SwitchPrecode.CenterID AND Switch.ID = SwitchPrecode.SwitchID
	WHERE     (City.ID = @CITY)

	and  SwitchPrecode.FromNumber in 
		(
		 SELECT substring (TEL_PISH+[TFIRST],3,8)
		 FROM [Salas].[dbo].[PHRANGE]
		 where ID_TELTYPE=4 )
 )

--------------- update simkhosusi tel

update  telephone set UsageType=2
where SwitchPrecodeid in
(
	SELECT     SwitchPrecode.id
	FROM         City INNER JOIN
                      Region ON City.ID = Region.CityID INNER JOIN
                      Center ON Region.ID = Center.RegionID INNER JOIN
                      Switch ON Center.ID = Switch.CenterID INNER JOIN
                      SwitchPrecode ON Center.ID = SwitchPrecode.CenterID AND Switch.ID = SwitchPrecode.SwitchID
	WHERE     (City.ID = @CITY)

	and  SwitchPrecode.FromNumber in 
		(
		 SELECT substring (TEL_PISH+[TFIRST],3,8)
		 FROM [Salas].[dbo].[PHRANGE]
		 where ID_TELTYPE=11 )
 )


