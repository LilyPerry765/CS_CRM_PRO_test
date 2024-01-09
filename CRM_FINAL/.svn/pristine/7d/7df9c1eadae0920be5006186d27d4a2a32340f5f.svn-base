
update Bucht set CityID = Region.CityID , CenterID = Center.ID , Center = Center.CenterName ,MDFID = MDF.ID , MDF = CAST( MDF.Number as nvarchar) + '('+MDF.Description+')' ,	Frame = MDFFrame.FrameNo,	FrameID	= MDFFrame.ID , ColumnNo = VerticalMDFColumn.VerticalCloumnNo ,	RowNo  = VerticalMDFRow.VerticalRowNo ,	TelephoneNo = Telephone.TelephoneNo
FROM                     Region INNER JOIN 
                         Center	ON Center.RegionID = Region.ID INNER JOIN
						 MDF  ON MDF.CenterID = Center.ID INNER JOIN
                         MDFFrame ON MDF.ID = MDFFrame.MDFID INNER JOIN
                         VerticalMDFColumn ON MDFFrame.ID = VerticalMDFColumn.MDFFrameID INNER JOIN
                         VerticalMDFRow ON VerticalMDFColumn.ID = VerticalMDFRow.VerticalMDFColumnID INNER JOIN
                         Bucht ON VerticalMDFRow.ID = Bucht.MDFRowID LEFT JOIN
                         Telephone ON Bucht.SwitchPortID = Telephone.SwitchPortID 
						 where bucht.CityID is null


update B set B.PCMMainBuchtID = (select top 1 ID from Bucht where CabinetInputID = B.CabinetInputID and Bucht.BuchtTypeID = 13 ) from Bucht as B where BuchtTypeID in (8,9)
go
