
if object_id('[dbo].[Report.MDFVacateSpecialWire]','P') is not null
drop procedure [dbo].[Report.MDFVacateSpecialWire]
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  Procedure [dbo].[Report.MDFVacateSpecialWire]
(
	  @RequestIDs varchar(max) = null
)
AS
BEGIN

	select 
			r.TelephoneNo PhoneNo,
			concat(m.NUMBER,' - ', m.[Description]) MDFID, 
			VerticalMDFRow.VerticalRowNo,
			VerticalMDFColumn.VerticalCloumnNo,
			Bucht.BuchtNo,
			Cabinet.CabinetNumber ,
			CabinetInput.InputNumber CabinetInputNumber,
			Post.Number PostNo,
			ConnectionNo ,

			bt.BuchtTypeName OtherBuchtTypeName,
			concat(om.NUMBER,' - ', om.[Description]) OtherMDFID,
			OtherVerticalMDFRow.VerticalRowNo OtherVerticalRowNo,
			OtherVerticalMDFColumn.VerticalCloumnNo OtherVerticalCloumnNo,
			OtherBucht.BuchtNo OtherBuchtNo


	
	from Request r
	left join VacateSpecialWire vsw on r.ID = vsw.RequestID
	LEFT JOIN Bucht on Bucht.ID = vsw.BUCHTID
	LEFT JOIN Bucht OtherBucht on OtherBuchtID = OtherBucht.ID
	LEFT JOIN BuchtType bt on bt.ID = OtherBucht.BuchtTypeID 
	LEFT JOIN PostContact on PostContact.ID = Bucht.ConnectionID 
	LEFT JOIN Post on Post.ID = PostContact.PostID
	LEFT JOIN CabinetInput on vsw.CabinetInputID = CabinetInput.ID
	LEFT JOIN Cabinet on Cabinet.ID = CabinetInput.CabinetID

	LEFT JOIN VerticalMDFRow on Bucht.MDFRowID = VerticalMDFRow.ID 
	LEFT JOIN VerticalMDFColumn on VerticalMDFRow.VerticalMDFColumnID = VerticalMDFColumn .ID
	LEFT join MDFFrame mf on VerticalMDFColumn.MDFFrameID = mf.ID
	LEFT join MDF m on m.ID = mf.MDFID

	LEFT JOIN VerticalMDFRow OtherVerticalMDFRow on OtherBucht.MDFRowID = OtherVerticalMDFRow.ID 
	LEFT JOIN VerticalMDFColumn OtherVerticalMDFColumn on OtherVerticalMDFRow.VerticalMDFColumnID = OtherVerticalMDFColumn .ID
	LEFT join MDFFrame omf on OtherVerticalMDFColumn.MDFFrameID = omf.ID
	LEFT join MDF om on om.ID = omf.MDFID
	where 
		(@RequestIDs IS NULL OR LEN(@RequestIDs) = 0 OR r.ID in (Select * From dbo.ufnSplitList(@RequestIDs)))

END