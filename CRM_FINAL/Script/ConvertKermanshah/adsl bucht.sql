
declare @centerID int = 3;

;with cte as (select * from  OldData.dbo.ZAFAR_VORODI where TYPE in (500 ,510 ,520 ,530 ,540 ,550 ,560  ,570 ,580))


update BM set BM.BuchtIDConnectedOtherBucht = T3.ID , ADSLStatus = 1 from CRM.dbo.Bucht as BM
join (
select * from  (
                    select  b.ID as baseID, VI.TEL_NO as Basetel
					from [CRM].dbo.bucht as b join [CRM].dbo.VerticalMDFRow as vr on vr.ID = b.MDFRowID
	                join [CRM].dbo.VerticalMDFColumn as vc on vc.ID = vr.VerticalMDFColumnID
		            join [CRM].dbo.MDFFrame as mf on mf.ID = vc.MDFFrameID
					join [CRM].dbo.MDF as m on m.ID = mf.MDFID
					join cte as VI on vr.VerticalRowNo = VI.TAB  AND vc.VerticalCloumnNo = VI.RAD  and b.BuchtNo = VI.ETE AND m.Description in  ( N'اصلی مرکز' , N'اختصاصی' , N'نوری'  , N'WLL' ) and m.CenterID = @centerID
					)as T1 join 
					(
					select  b2.ID , VI2.TEL_NO  from   [CRM].dbo.bucht as b2 join [CRM].dbo.VerticalMDFRow as vr2 on vr2.ID = b2.MDFRowID
	                join [CRM].dbo.VerticalMDFColumn as vc2 on vc2.ID = vr2.VerticalMDFColumnID
		            join [CRM].dbo.MDFFrame as mf2 on mf2.ID = vc2.MDFFrameID
					join [CRM].dbo.MDF as m2 on m2.ID = mf2.MDFID
					join cte as VI2 on vr2.VerticalRowNo = VI2.TAB_1  AND vc2.VerticalCloumnNo = VI2.RAD_1  and b2.BuchtNo = VI2.ETE_1 AND m2.Description in ( N'ADSL' ) and (m2.CenterID = @centerID)

					) as T2 on T1.Basetel = T2.TEL_NO
					)as T3 on BM.ID = T3.baseID

;with cte as (select * from  OldData.dbo.ZAFAR_VORODI where TYPE in (500 ,510 ,520 ,530 ,540 ,550 ,560  ,570 ,580))

update BM set BM.PAPInfoID = (select ID from PAPInfo where KermanshahTypeID = code) , status = 14 from CRM.dbo.Bucht as BM
join (
select * from  (
                    select  b.ID as baseID, VI.TEL_NO as Basetel , VI.type as code
					from [CRM].dbo.bucht as b join [CRM].dbo.VerticalMDFRow as vr on vr.ID = b.MDFRowID
	                join [CRM].dbo.VerticalMDFColumn as vc on vc.ID = vr.VerticalMDFColumnID
		            join [CRM].dbo.MDFFrame as mf on mf.ID = vc.MDFFrameID
					join [CRM].dbo.MDF as m on m.ID = mf.MDFID
					join cte as VI on vr.VerticalRowNo = VI.TAB  AND vc.VerticalCloumnNo = VI.RAD  and b.BuchtNo = VI.ETE AND m.Description in  ( N'اصلی مرکز' , N'اختصاصی' , N'نوری'  , N'WLL' ) and m.CenterID = @centerID
					)as T1 join 
					(
					select  b2.ID as adslbuchtID , VI2.TEL_NO  from   [CRM].dbo.bucht as b2 join [CRM].dbo.VerticalMDFRow as vr2 on vr2.ID = b2.MDFRowID
	                join [CRM].dbo.VerticalMDFColumn as vc2 on vc2.ID = vr2.VerticalMDFColumnID
		            join [CRM].dbo.MDFFrame as mf2 on mf2.ID = vc2.MDFFrameID
					join [CRM].dbo.MDF as m2 on m2.ID = mf2.MDFID
					join cte as VI2 on vr2.VerticalRowNo = VI2.TAB_1  AND vc2.VerticalCloumnNo = VI2.RAD_1  and b2.BuchtNo = VI2.ETE_1 AND m2.Description in ( N'ADSL' ) and (m2.CenterID = @centerID)

					) as T2 on T1.Basetel = T2.TEL_NO
					)as T3 on BM.ID = T3.adslbuchtID




				
