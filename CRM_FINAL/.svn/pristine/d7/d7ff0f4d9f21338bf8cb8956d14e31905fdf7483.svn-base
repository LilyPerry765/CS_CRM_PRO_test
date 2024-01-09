alter table Bucht add  CityID int null
alter table Bucht add  CenterID int null
alter table Bucht add  Center nvarchar(50) null
alter table Bucht add  MDFID int null
alter table Bucht add  MDF nvarchar(100) null
alter table Bucht add  FrameID int null
alter table Bucht add  Frame int null
alter table Bucht add  ColumnNo int null
alter table Bucht add  RowNo int null
alter table Bucht add  TelephoneNo bigint null
EXEC sp_rename 'dbo.Bucht.ConnectionIDBucht', 'PCMMainBuchtID', 'COLUMN';


update Bucht set CityID = Region.CityID , CenterID = Center.ID , Center = Center.CenterName ,MDFID = MDF.ID , MDF = CAST( MDF.Number as nvarchar) + '('+MDF.Description+')' ,	Frame = MDFFrame.FrameNo,	FrameID	= MDFFrame.ID , ColumnNo = VerticalMDFColumn.VerticalCloumnNo ,	RowNo  = VerticalMDFRow.VerticalRowNo ,	TelephoneNo = Telephone.TelephoneNo
FROM                     Region INNER JOIN 
                         Center	ON Center.RegionID = Region.ID INNER JOIN
						 MDF  ON MDF.CenterID = Center.ID INNER JOIN
                         MDFFrame ON MDF.ID = MDFFrame.MDFID INNER JOIN
                         VerticalMDFColumn ON MDFFrame.ID = VerticalMDFColumn.MDFFrameID INNER JOIN
                         VerticalMDFRow ON VerticalMDFColumn.ID = VerticalMDFRow.VerticalMDFColumnID INNER JOIN
                         Bucht ON VerticalMDFRow.ID = Bucht.MDFRowID LEFT JOIN
                         Telephone ON Bucht.SwitchPortID = Telephone.SwitchPortID


update B set B.PCMMainBuchtID = (select top 1 ID from Bucht where CabinetInputID = B.CabinetInputID and Bucht.BuchtTypeID = 13 ) from Bucht as B where BuchtTypeID in (8,9)
go




--CREATE TRIGGER TriggerFillConnction ON  Bucht  AFTER  INSERT
--AS 
--BEGIN
--	SET NOCOUNT ON;

--update Bucht set CityID = Region.CityID , CenterID = Center.ID , Center = Center.CenterName ,	MDFID = MDF.ID , MDF = CAST( MDF.Number as nvarchar) + '('+MDF.Description+')' ,	Frame = MDFFrame.FrameNo,	FrameID	= MDFFrame.ID , ColumnNo = VerticalMDFColumn.VerticalCloumnNo ,	RowNo  = VerticalMDFRow.VerticalRowNo ,	TelephoneNo = Telephone.TelephoneNo
--FROM             Region INNER JOIN 
--                         Center	ON Center.RegionID = Region.ID INNER JOIN
--						 MDF  ON MDF.CenterID = Center.ID INNER JOIN
--                         MDFFrame ON MDF.ID = MDFFrame.MDFID INNER JOIN
--                         VerticalMDFColumn ON MDFFrame.ID = VerticalMDFColumn.MDFFrameID INNER JOIN
--                         VerticalMDFRow ON VerticalMDFColumn.ID = VerticalMDFRow.VerticalMDFColumnID INNER JOIN
--                         Bucht ON VerticalMDFRow.ID = Bucht.MDFRowID INNER JOIN
--						 inserted ON inserted.ID = Bucht.ID LEFT JOIN
--                         Telephone ON Bucht.SwitchPortID = Telephone.SwitchPortID 
--END
--GO

CREATE TRIGGER TriggerFillTelephoneNo ON  Bucht  AFTER  UPDATE
AS 
BEGIN
	SET NOCOUNT ON;

if exists(SELECT * FROM inserted where inserted.SwitchPortID is not null)
     begin
     update Bucht set TelephoneNo = Telephone.TelephoneNo
     FROM  Bucht join  inserted on Bucht.ID = inserted.ID JOIN Telephone ON Bucht.SwitchPortID = Telephone.SwitchPortID 
     end

if exists(SELECT * FROM inserted where inserted.BuchtTypeID in (8,9))
     begin
     update B set  B.PCMMainBuchtID = (select top 1 ID from Bucht where CabinetInputID = isd.CabinetInputID and Bucht.BuchtTypeID = 13 )
     FROM  Bucht as B join inserted as isd on isd.ID = b.ID
     end
END
GO






