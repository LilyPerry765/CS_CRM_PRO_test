-- use dbo.[GetBuchtInfo]

if object_ID('uspReportMDFExchangeCabinuteInput','p') is not null
drop procedure uspReportMDFExchangeCabinuteInput
/****** Object:  StoredProcedure [dbo].[uspReportMDFExchangeCabinuteInput]    Script Date: 1/24/2015 3:28:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE  Procedure [dbo].[uspReportMDFExchangeCabinuteInput]
(
	  @RequestIDs varchar(max) = null
)
AS
BEGIN

;with OldBuchtInfo AS
(
select 
	dbo.[GetBuchtInfo](OldBucht.ID) OldBuchtInfo, 
	t.TelephoneNo,
	concat(N'ردیف:' , app.RowNo, N'طبقه:', app.ColumnNo , N'اتصالی:' , app.BuchtNo) ADSLPapInfo
	, ROW_NUMBER() OVER (partition By OldCabinet.ID order by OldCabinet.ID,OldCabinetInput.ID) RowNo,
	dbo.[GetBuchtInfo](OtherBucht.ID) OtherBuchtInfo,
	--[dbo].[GetPCMInfo](PCMID) PCMINFO,
	case when OLdBucht.[Status] = 13 Then N'پی سی ام' Else '' END PCMINFO


from ExchangeCabinetInput eci
LEFT JOIN Request r on r.ID = eci.ID
LEFT JOIN Cabinet OldCabinet on oldcabinet.id = OldCabinetID
LEFT JOIN CabinetInput OldCabinetInput on OldCabinetInput.cabinetID = OldCabinet.ID and (OldCabinetInput.ID between eci.FromOldCabinetInputID and eci.ToOldCabinetInputID)
LEFT JOIN Bucht OldBucht on OldCabinetInput.id = OldBucht.CabinetInputID
LEFT JOIN PCMPORT on PCMPORT.ID = OLDBucht.PCMPORTID
LEFT JOIN Bucht OtherBucht on OtherBucht.ID = OLdBucht.BuchtIDConnectedOtherBucht
LEFT JOIN Telephone t on OldBucht.SwitchPortID = t.SwitchPortID
LEFT JOIN ADSLPapPort app on app.TelephoneNo = t.TelephoneNo 
WHERE 
	(@RequestIDs IS NULL OR LEN(@RequestIDs) = 0 OR r.ID in (Select * From dbo.ufnSplitList(@RequestIDs)) and (OldBucht.BuchtTypeID != 8 and OldBucht.BuchtTypeID != 9))
)
,NewBuchtInfo AS
(
select 
	dbo.[GetBuchtInfo](NEWBucht.ID) NEWBuchtInfo, 
	ROW_NUMBER() OVER (partition By NewCabinet.ID order by NewCabinet.ID,NewCabinetInput.ID) RowNo


from ExchangeCabinetInput eci

LEFT JOIN Cabinet NEWCabinet on NEWcabinet.id = NEWCabinetID
LEFT JOIN CabinetInput NEWCabinetInput on NEWCabinetInput.cabinetID = NEWCabinet.ID and (NEWCabinetInput.ID between eci.FromNEWCabinetInputID and eci.ToNEWCabinetInputID)
LEFT JOIN Bucht NEWBucht on NEWCabinetInput.id = NEWBucht.CabinetInputID

LEFT JOIN Telephone t on NEWBucht.SwitchPortID = t.SwitchPortID
LEFT JOIN ADSLPapPort app on app.TelephoneNo = t.TelephoneNo 
WHERE 
	(@RequestIDs IS NULL OR LEN(@RequestIDs) = 0 OR eci.ID in (Select * From dbo.ufnSplitList(@RequestIDs))) 

)
Select 
		OldBuchtInfo.ADSLPapInfo  ,
		OldBuchtInfo.OldBuchtInfo ,
		OldBuchtInfo.OtherBuchtInfo ,
		OldBuchtInfo.PCMINFO ,
		OldBuchtInfo.TelephoneNo,

		NewBuchtInfo.NEWBuchtInfo
from 
		OLDBuchtInfo
		Left join NewBuchtInfo on OldBuchtInfo.RowNo = NewBuchtInfo.RowNo

END