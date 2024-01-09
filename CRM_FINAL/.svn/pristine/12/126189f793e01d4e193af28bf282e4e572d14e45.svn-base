--SELECT CARD,	SHELF,	ROCK 
--  FROM [Javanrood].[dbo].[PCM4]
--  GROUP BY CARD,	SHELF,	ROCK 
----------------------------------------------------
----insert Rock

	begin
	insert into  PCMRock  
	select rshc.code,rshc.ROCK from [Javanrood].[dbo].[PCM4] as RSHC 
	join PCMRock on PCMRock.CenterID=RSHC.code
	group by rshc.code,rshc.ROCK,PCMRock.CenterID,PCMRock.Number
	having RSHC.ROCK!=PCMRock.Number and PCMRock.CenterID = 265 and rshc.ROCK not in (1,2)
	end
--------------------------------------------------------------------------------------------------------------
----insert Shelf
	begin
	insert into  PCMShelf  
		
	select PCMRock1.ID,RSHC1.SHELF from [Javanrood].[dbo].[PCM4] as RSHC1
	join PCMRock as PCMRock1  on PCMRock1.CenterID=RSHC1.code and PCMRock1.Number=RSHC1.Rock
	join PCMShelf on RSHC1.SHELF=PCMShelf.Number  
	where not EXISTS 
		 ( select PCMRock.ID,RSHC.SHELF from [Javanrood].[dbo].[PCM4] as RSHC 
		join PCMRock  on PCMRock.CenterID=RSHC.code  and PCMRock.Number=RSHC.Rock
		join PCMShelf on RSHC.SHELF=PCMShelf.Number and PCMShelf.PCMRockID=PCMRock.id
		where RSHC1.SHELF=RSHC.SHELF and PCMRock1.ID=PCMRock.ID
		 )
	 group by PCMRock1.ID,RSHC1.SHELF
	end


----------------------------------------------------------------------------------------------------------------
-----UPDATE PCM (CARD)

update pcm 
	set card=RSHC.card
		    from    pcm INNER JOIN
                         PCMShelf on PCMShelf.id=pcm.ShelfID INNER JOIN
						 PCMRock on PCMRock.ID=PCMShelf.PCMRockID INNER JOIN
                         PCMPort on PCMPort.PCMID=pcm.ID INNER JOIN
                         dbo.Bucht ON dbo.PCMPort.ID = dbo.Bucht.PCMPortID
                         INNER JOIN dbo.VerticalMDFRow ON dbo.Bucht.MDFRowID = dbo.VerticalMDFRow.ID INNER JOIN
                         dbo.VerticalMDFColumn ON dbo.VerticalMDFRow.VerticalMDFColumnID = dbo.VerticalMDFColumn.ID INNER JOIN
                         dbo.MDFFrame ON dbo.VerticalMDFColumn.MDFFrameID = dbo.MDFFrame.ID INNER JOIN
                         dbo.MDF ON dbo.MDFFrame.MDFID = dbo.MDF.ID INNER JOIN
						 [Javanrood].[dbo].[PCM4] as RSHC on RSHC.CODE=dbo.MDF.CenterID 
						 INNER JOIN CabinetInput as ci on ci.ID=bucht.CabinetInputID 
						 INNER JOIN Cabinet as c on c.id=ci.CabinetID

WHERE        (dbo.PCMRock.CenterID = 265) AND  (dbo.PCMRock.CenterID=mdf.CenterID) and (mdf.description='PCM')
And ( dbo.VerticalMDFColumn.VerticalCloumnNo=RSHC.RAD_2) and (dbo.VerticalMDFRow.VerticalRowNo=RSHC.TAB_2) and ( dbo.Bucht.BuchtNo=RSHC.ETE_2)
and c.CabinetNumber=PCMShelf.Number and ci.InputNumber=pcm.Card

----------------------------------------------------------------------------------------------------------------
-----UPDATE PCM (SHELFID)

update pcm 
	set ShelfID=PCMShelf.ID,card=RSHC.card
			FROM         pcm INNER JOIN
						[Javanrood].[dbo].[PCM4] as RSHC on RSHC.CARD=pcm.Card INNER JOIN
                         PCMShelf on PCMShelf.Number=RSHC.SHELF INNER JOIN
						 PCMRock on PCMRock.Number=RSHC.ROCK INNER JOIN
                         PCMPort on PCMPort.PCMID=pcm.ID INNER JOIN
                         dbo.Bucht ON dbo.PCMPort.ID = dbo.Bucht.PCMPortID INNER JOIN
                         dbo.VerticalMDFRow ON dbo.Bucht.MDFRowID = dbo.VerticalMDFRow.ID INNER JOIN
                         dbo.VerticalMDFColumn ON dbo.VerticalMDFRow.VerticalMDFColumnID = dbo.VerticalMDFColumn.ID INNER JOIN
                         dbo.MDFFrame ON dbo.VerticalMDFColumn.MDFFrameID = dbo.MDFFrame.ID INNER JOIN
                         dbo.MDF ON dbo.MDFFrame.MDFID = dbo.MDF.ID  


WHERE        (dbo.PCMRock.CenterID = 265) AND  (dbo.PCMRock.CenterID=mdf.CenterID) and (mdf.description='PCM') and  RSHC.CODE=dbo.MDF.CenterID
And ( dbo.VerticalMDFColumn.VerticalCloumnNo=RSHC.RAD_2) and (dbo.VerticalMDFRow.VerticalRowNo=RSHC.TAB_2) and ( dbo.Bucht.BuchtNo=RSHC.ETE_2)
and PCMRock.CenterID=RSHC.code
and PCMShelf.PCMRockID=PCMRock.ID
--------------------------------------------------------------------

--select * from PCMRock as r
--where r.centerID=1 order by r.Number


--SELECT        dbo.PCMRock.Number AS rocknumber, dbo.PCMShelf.Number AS shelfnumber, dbo.PCM.Card, dbo.PCMPort.PortNumber, 
--                         dbo.MDF.Description,RSHC.*,c.CabinetNumber,ci.InputNumber,PCMShelf.id
--FROM            dbo.PCMRock INNER JOIN
--                         dbo.PCMShelf ON dbo.PCMRock.ID = dbo.PCMShelf.PCMRockID INNER JOIN
--                         dbo.PCM ON dbo.PCMShelf.ID = dbo.PCM.ShelfID INNER JOIN
--                         dbo.PCMPort ON dbo.PCM.ID = dbo.PCMPort.PCMID INNER JOIN
--                         dbo.Bucht ON dbo.PCMPort.ID = dbo.Bucht.PCMPortID INNER JOIN
--                         dbo.VerticalMDFRow ON dbo.Bucht.MDFRowID = dbo.VerticalMDFRow.ID INNER JOIN
--                         dbo.VerticalMDFColumn ON dbo.VerticalMDFRow.VerticalMDFColumnID = dbo.VerticalMDFColumn.ID INNER JOIN
--                         dbo.MDFFrame ON dbo.VerticalMDFColumn.MDFFrameID = dbo.MDFFrame.ID INNER JOIN
--                         dbo.MDF ON dbo.MDFFrame.MDFID = dbo.MDF.ID INNER JOIN
--						 [Javanrood].[dbo].[PCM4] as RSHC on RSHC.CODE=dbo.MDF.CenterID INNER JOIN
--						 CabinetInput as ci on ci.ID=bucht.CabinetInputID INNER JOIN
--						 Cabinet as c on c.id=ci.CabinetID

--WHERE        (dbo.PCMRock.CenterID = 265) AND  (dbo.PCMRock.CenterID=mdf.CenterID) and (mdf.description='PCM')
--And ( dbo.VerticalMDFColumn.VerticalCloumnNo=RSHC.RAD_2) and (dbo.VerticalMDFRow.VerticalRowNo=RSHC.TAB_2) and ( dbo.Bucht.BuchtNo=RSHC.ETE_2)
--AND dbo.PCMRock.Number =RSHC.ROCK
--AND dbo.PCMShelf.Number =RSHC.SHELF
--AND dbo.PCM.Card =RSHC.CARD
--order by dbo.PCMRock.Number , dbo.PCMShelf.Number, dbo.PCM.Card, dbo.PCMPort.PortNumber
