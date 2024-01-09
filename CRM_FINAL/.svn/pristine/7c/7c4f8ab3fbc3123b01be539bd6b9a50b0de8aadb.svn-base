use CRM
GO

BEGIN TRY
    BEGIN TRANSACTION

declare @cityid int ; set @cityid =9
declare @centerid int ; set @centerid =491

-- this query connect Bucht to CabinetInput
update B2
set CabinetInputID = R.CabinetID
from bucht as B2 join 
(
select B.ID as BuchtID, T.ID as CabinetID from
(
(
select ROW_NUMBER() OVER (PARTITION BY Bucht.cabinetnumber ORDER BY  Bucht.ID) AS number , Bucht.* from Bucht join VerticalMDFRow as vr on vr.ID = Bucht.MDFRowID
	                                                                                             join VerticalMDFColumn as vc on vc.ID = vr.VerticalMDFColumnID
		                                                                                         join MDFFrame as mf on mf.ID = vc.MDFFrameID
				                                                                                 join MDF as m on m.ID = mf.MDFID 
																								 join center on center.id=m.CenterID
																							 	 join region on center.regionid=region.id
																								 join city on city.id=region.cityid
																								 where city.id=@cityid and center.centercode=@centerid
)
 as B join
(
select ROW_NUMBER() OVER (PARTITION BY CabinetInput.cabinetID ORDER BY InputNumber) AS number , CabinetInput.* , cabinet.CabinetNumber from CabinetInput 
join cabinet on cabinet.id = CabinetInput.CabinetID 
join center on center.id=cabinet.centerid
join region on center.regionid=region.id
join city on city.id=region.cityid
where city.id=@cityid  and center.centercode=@centerid
) as T
on T.Number = B.Number and T. CabinetNumber = B.CabinetNumber
)
) as R on B2.ID = R.BuchtID



-- this query connect Bucht to CablePair
update B2
set CablePairID = R.CablePair
from bucht as B2 join 
(
select B.ID as BuchtID, T.ID as CablePair from
(
(
select ROW_NUMBER() OVER (PARTITION BY Bucht.cabinetnumber ORDER BY  Bucht.ID ) AS number , Bucht.* from Bucht join VerticalMDFRow as vr on vr.ID = Bucht.MDFRowID
	                                                                                             join VerticalMDFColumn as vc on vc.ID = vr.VerticalMDFColumnID
		                                                                                         join MDFFrame as mf on mf.ID = vc.MDFFrameID
				                                                                                 join MDF as m on m.ID = mf.MDFID 
																								 join center on center.id=m.CenterID
																							 	 join region on center.regionid=region.id
																								 join city on city.id=region.cityid
																								 where city.id=@cityid  and center.centercode=@centerid
)
 as B join
(
select ROW_NUMBER() OVER (PARTITION BY CablePair.cableID ORDER BY CablePairNumber) AS number , CablePair.* , Cable.CableNumber from CablePair 
join Cable on Cable.id = CablePair.CableID 
join center on center.id=cable.CenterID
join region on center.regionid=region.id
join city on city.id=region.cityid
where city.id=@cityid  and center.centercode=@centerid
) as T
on T.Number = B.Number and T.CableNumber = B.cabinetnumber
)
) as R on B2.ID = R.BuchtID

COMMIT TRAN -- Transaction Success!
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRAN --RollBack in case of Error
END CATCH