
declare	@CenterID  int ; set @CenterID = 1


update PC set PC.Status = 14  from PostContact as PC 
join Post as p on PC.PostID = p.ID 
join Cabinet as C on C.ID = P.CabinetID
where c.CenterID = @CenterID and PC.ConnectionType = 5

update PC set PC.ConnectionType = 3 , PC.Status = 5  from PostContact as PC 
join Post as p on PC.PostID = p.ID 
join Cabinet as C on C.ID = P.CabinetID
where c.CenterID = @CenterID and PC.ConnectionType = 4


UPDATE       Bucht set SwitchPortID = null , CablePairID = null ,	CabinetInputID = NULL , BuchtTypeID = 13 , PCMPortID= NULL , 	PortNo = NULL ,ConnectionID	= NULL , BuchtIDConnectedOtherBucht = NULL ,Status = 0
FROM            MDF INNER JOIN
                         MDFFrame ON MDF.ID = MDFFrame.MDFID INNER JOIN
                         VerticalMDFColumn ON MDFFrame.ID = VerticalMDFColumn.MDFFrameID INNER JOIN
                         VerticalMDFRow ON VerticalMDFColumn.ID = VerticalMDFRow.VerticalMDFColumnID INNER JOIN
                         Bucht ON VerticalMDFRow.ID = Bucht.MDFRowID where MDF.Description = N'PCM' and MDF.CenterID = @CenterID

UPDATE       Bucht set  SwitchPortID = null , PCMPortID= NULL , PortNo = NULL , ConnectionID	= NULL , BuchtIDConnectedOtherBucht = NULL ,Status = 0
FROM            MDF INNER JOIN
                         MDFFrame ON MDF.ID = MDFFrame.MDFID INNER JOIN
                         VerticalMDFColumn ON MDFFrame.ID = VerticalMDFColumn.MDFFrameID INNER JOIN
                         VerticalMDFRow ON VerticalMDFColumn.ID = VerticalMDFRow.VerticalMDFColumnID INNER JOIN
                         Bucht ON VerticalMDFRow.ID = Bucht.MDFRowID where  MDF.CenterID = @CenterID and Bucht.Status = 13

					  
delete     PCMPort
FROM         PCMRock INNER JOIN
                      PCMShelf ON PCMRock.ID = PCMShelf.PCMRockID INNER JOIN
                      PCM ON PCMShelf.ID = PCM.ShelfID INNER JOIN
                      PCMPort ON PCM.ID = PCMPort.PCMID
					  where PCMRock.CenterID = @CenterID 

delete     PCM
FROM         PCMRock INNER JOIN
                      PCMShelf ON PCMRock.ID = PCMShelf.PCMRockID INNER JOIN
                      PCM ON PCMShelf.ID = PCM.ShelfID
					  where PCMRock.CenterID = @CenterID 

delete     PCMShelf
FROM         PCMRock INNER JOIN
                      PCMShelf ON PCMRock.ID = PCMShelf.PCMRockID
					  where PCMRock.CenterID = @CenterID 

delete     PCMRock
            FROM  PCMRock
            where PCMRock.CenterID = @CenterID