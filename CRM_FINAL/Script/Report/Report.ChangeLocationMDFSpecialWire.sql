if object_id('[dbo].[Report.ChangeLocationMDFSpecialWire]' ,'P') is not null
drop procedure [dbo].[Report.ChangeLocationMDFSpecialWire]
/****** Object:  StoredProcedure [dbo].[Report.ChangeLocationNetworkSpecialWire]    Script Date: 1/7/2015 1:52:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--exec [dbo].[Report.ChangeLocationNetworkSpecialWire] '9309170365'
CREATE  Procedure [dbo].[Report.ChangeLocationMDFSpecialWire]
(
	  @RequestIDs varchar(max) = null
)
AS
BEGIN

	select 
		    OldOtherVerticalMDFColumn.VerticalCloumnNo OLDOtherVerticalCloumnNo,
		    OldOtherVerticalMDFRow.VerticalRowNo OLDOtherVerticalRowNo,
			OldOtherBucht.BuchtNo  OLDOtherBuchtNo,

		    NewOtherVerticalMDFColumn.VerticalCloumnNo NEWOtherVerticalCloumnNo,
		    NewOtherVerticalMDFRow.VerticalRowNo NEWOtherVerticalRowNo,
			NewOtherBucht.BuchtNo  NEWOtherBuchtNo,

			r.TelephoneNo PhoneNo,
			--NEW
			concat(NEWm.NUMBER,' - ', NEWm.[Description]) NEWMDFID, 
			NEWVerticalMDFRow.VerticalRowNo NEWVerticalRowNo,
			NEWVerticalMDFColumn.VerticalCloumnNo NEWVerticalCloumnNo,
			NEWBucht.BuchtNo NEWBuchtNo,
			NEWCabinet.CabinetNumber NEWCabinetNumber,
			NEWCabinetInput.InputNumber NEWCabinetInputNumber,
			NEWPost.Number NEWPostNo,
			NEWPostContact.ConnectionNo NEWConnectionNo,

			NEWbt.BuchtTypeName OtherBuchtTypeName,
			--OLD
			concat(OLDm.NUMBER,' - ', OLDm.[Description]) OLDMDFID, 
			OLDVerticalMDFRow.VerticalRowNo OLDVerticalRowNo,
			OLDVerticalMDFColumn.VerticalCloumnNo OLDVerticalCloumnNo,
			OLDBucht.BuchtNo OLDBuchtNo,
			OLDCabinet.CabinetNumber OLDCabinetNumber,
			OLDCabinetInput.InputNumber OLDCabinetInputNumber,
			OLDPost.Number OLDPostNo,
			OLDPostContact.ConnectionNo OLDConnectionNo,

			OLDbt.BuchtTypeName OldOtherBuchtTypeName,
			
		 concat(C.FirstNameOrTitle, ' ', C.Lastname) FullName,
		 CASE WHEN C.PersonType = 0 THEN N'حقیقی' 
			  WHEN C.PersonType = 1 THEN N'حقوقی' 
			  ELSE N'نامشخص'
			  END PersonType,
		C.NationalCodeOrRecordNo,
		CASE WHEN C.Gender = 0 THEN N'مرد' 
			  WHEN C.Gender = 1 THEN N'زن' 
			  ELSE N'نامشخص'
			  END Gender,
		UrgentTelNo

	from Request r
	left join ChangeLocationSpecialWire clsw on r.ID = clsw.RequestID
	left join InvestigatePossibility IP on IP.RequestID = r.ID

			--NEW BUCHTInfo
	LEFT JOIN Bucht NewBucht on NewBucht.ID = IP.BuchtID
	LEFT JOIN Bucht NEWOtherBucht on NEWOtherBuchtID = NEWOtherBucht.ID
	LEFT JOIN VerticalMDFRow NEWOtherVerticalMDFRow on NEWOtherBucht.MDFRowID = NEWOtherVerticalMDFRow.ID 
	LEFT JOIN VerticalMDFColumn NEWOtherVerticalMDFColumn on NEWOtherVerticalMDFRow.VerticalMDFColumnID = NEWOtherVerticalMDFColumn .ID
	LEFT JOIN BuchtType NEWbt on NEWbt.ID = NEWOtherBucht.BuchtTypeID 
	LEFT JOIN PostContact NEWPostContact on NEWPostContact.ID = NEWBucht.ConnectionID 
	LEFT JOIN Post NEWPost on  NEWPost.ID = NEWPostContact.PostID
	LEFT JOIN CabinetInput NEWCabinetInput on NewBucht.CabinetInputID = NEWCabinetInput.ID
	LEFT JOIN Cabinet NEWCabinet on NEWCabinet.ID = NEWCabinetInput.CabinetID
	 
	LEFT JOIN VerticalMDFRow NEWVerticalMDFRow on NEWBucht.MDFRowID = NEWVerticalMDFRow.ID 
	LEFT JOIN VerticalMDFColumn NEWVerticalMDFColumn on NEWVerticalMDFRow.VerticalMDFColumnID = NEWVerticalMDFColumn .ID
	LEFT join MDFFrame NEWmf on NEWVerticalMDFColumn.MDFFrameID = NEWmf.ID
	LEFT join MDF NEWm on NEWm.ID = NEWmf.MDFID
	 
			--OLD BUCHTInfo
	LEFT JOIN Bucht OLDBucht on OLDBucht.ID = clsw.OLDBUCHTID
	LEFT JOIN Bucht OldOtherBucht on OldOtherBuchtID = OldOtherBucht.ID
    LEFT JOIN VerticalMDFRow OldOtherVerticalMDFRow on OldOtherBucht.MDFRowID = OldOtherVerticalMDFRow.ID 
	LEFT JOIN VerticalMDFColumn OldOtherVerticalMDFColumn on OldOtherVerticalMDFRow.VerticalMDFColumnID = OldOtherVerticalMDFColumn .ID

	LEFT JOIN BuchtType OLDbt on OLDbt.ID = OLDOtherBucht.BuchtTypeID
	 
	LEFT JOIN PostContact OLDPostContact on OLDPostContact.ID = clsw.OldPostContactID 
	LEFT JOIN Post OLDPost on  OLDPost.ID = OLDPostContact.PostID

	LEFT JOIN CabinetInput OLDCabinetInput on clsw.OLDCabinetInputID = OLDCabinetInput.ID
	LEFT JOIN Cabinet OLDCabinet on OLDCabinet.ID = OLDCabinetInput.CabinetID
	 
	LEFT JOIN VerticalMDFRow OLDVerticalMDFRow on OLDBucht.MDFRowID = OLDVerticalMDFRow.ID 
	LEFT JOIN VerticalMDFColumn OLDVerticalMDFColumn on OLDVerticalMDFRow.VerticalMDFColumnID = OLDVerticalMDFColumn .ID
	LEFT join MDFFrame OLDmf on OLDVerticalMDFColumn.MDFFrameID = OLDmf.ID
	LEFT join MDF OLDm on OLDm.ID = OLDmf.MDFID


	LEFT join Customer C on C.id = r.CustomerID
	
	where 
		(@RequestIDs IS NULL OR LEN(@RequestIDs) = 0 OR r.ID in (Select * From dbo.ufnSplitList(@RequestIDs)))
		


END