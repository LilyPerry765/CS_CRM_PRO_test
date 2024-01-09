use CRM
GO

BEGIN TRY
    BEGIN TRANSACTION

declare @CenterID int ; set @CenterID =5
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
				                                                                                 join MDF as m on m.ID = mf.MDFID where m.CenterID = @CenterID and Bucht.CabinetNumber in (select kafo from [OldData].[dbo].New_BEASAT_KAFO group by Kafo)
)
 as B join
(
select ROW_NUMBER() OVER (PARTITION BY CabinetInput.cabinetID ORDER BY CabinetInput.InputNumber) AS number , CabinetInput.* , cabinet.CabinetNumber from CabinetInput join cabinet on cabinet.id = CabinetInput.CabinetID and cabinet.CenterID = @CenterID and Cabinet.CabinetNumber in (select kafo from [OldData].[dbo].New_BEASAT_KAFO group by Kafo)
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
				                                                                                 join MDF as m on m.ID = mf.MDFID where m.CenterID = @CenterID and Bucht.CabinetNumber in (select kafo from [OldData].[dbo].New_BEASAT_KAFO group by Kafo)
)
 as B join
(
select ROW_NUMBER() OVER (PARTITION BY CablePair.cableID ORDER BY CablePair.CablePairNumber) AS number , CablePair.* , Cable.CableNumber from CablePair join Cable on Cable.id = CablePair.CableID and Cable.CenterID = @CenterID and Cable.CableNumber in (select kafo from [OldData].[dbo].New_BEASAT_KAFO group by Kafo)
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