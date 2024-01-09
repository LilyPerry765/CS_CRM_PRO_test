
declare @centerID int = 3;

update BM set BM.status = 2 from CRM.dbo.Bucht as BM
join (   select  b.ID as baseID
		 from [CRM].dbo.bucht as b join [CRM].dbo.VerticalMDFRow as vr on vr.ID = b.MDFRowID
	                join [CRM].dbo.VerticalMDFColumn as vc on vc.ID = vr.VerticalMDFColumnID
		            join [CRM].dbo.MDFFrame as mf on mf.ID = vc.MDFFrameID
					join [CRM].dbo.MDF as m on m.ID = mf.MDFID
					join [OldData].dbo.ZAFAR_KHARAB as VI on vr.VerticalRowNo = VI.TAB  AND vc.VerticalCloumnNo = VI.RAD  and b.BuchtNo = VI.ETE  and m.CenterID = @centerID and b.CabinetNumber = VI.KAFO
					)as T1  on  BM.ID = T1.baseID 