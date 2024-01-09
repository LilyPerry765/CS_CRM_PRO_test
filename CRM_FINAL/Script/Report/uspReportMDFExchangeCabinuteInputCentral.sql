

/****** Object:  StoredProcedure [dbo].[uspReportMDFExchangeCabinuteInputCentral]    Script Date: 2/12/2015 9:50:42 AM ******/
DROP PROCEDURE [dbo].[uspReportMDFExchangeCabinuteInputCentral]
GO

/****** Object:  StoredProcedure [dbo].[uspReportMDFExchangeCabinuteInputCentral]    Script Date: 2/12/2015 9:50:42 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE  Procedure [dbo].[uspReportMDFExchangeCabinuteInputCentral]
(
	  @RequestIDs varchar(max) = null
)
AS
BEGIN

select 
	dbo.[GetBuchtInfo](OldBucht.ID) OldBuchtInfo, 
	dbo.[GetBuchtInfo](NEWBucht.ID) NEWBuchtInfo, 
	t.TelephoneNo, 
	CASE WHEN (app.RowNo IS NULL AND app.ColumnNo IS NULL AND app.BuchtNo IS NULL ) THEN ''
		 ELSE concat(N'ردیف:' , app.RowNo, N'طبقه:', app.ColumnNo , N'اتصالی:' , app.BuchtNo) 
		 END ADSLPapInfo,
	dbo.[GetBuchtInfo](OtherBucht.ID) OtherBuchtInfo,
	[dbo].[GetPCMInfo](PCMID) PCMINFO,
	CASE WHEN OLDBucht.[Status] = 13 THEN 'پی سی ام' ELSE '' END BuchtStatus
	

from TranslationPostInput tpi
LEFT JOIN TranslationPostInputConnections tpic on tpic.RequestID = tpi.RequestID
LEFT JOIN Request r on r.ID = tpi.RequestID
LEFT JOIN Cabinet OldCabinet on oldcabinet.id = tpi.FromCabinetID

LEFT JOIN CabinetInput OldCabinetInput on OldCabinetInput.cabinetID = OldCabinet.ID
LEFT JOIN PostContact OldPostContact on OldPostContact.ID = tpic.ConnectionID
LEFT JOIN Bucht OldBucht on OLDBucht.ConnectionID = OldPostContact.ID 

LEFT JOIN Cabinet NEWCabinet on NEWcabinet.id = tpi.ToCabinetID

LEFT JOIN CabinetInput NEWCabinetInput on NEWCabinetInput.ID = tpic.CabinetInputID 
LEFT JOIN Bucht NewBucht on NewBucht.CabinetInputID = tpic.CabinetInputID


LEFT JOIN PCMPORT on PCMPORT.ID = OLDBucht.PCMPORTID
LEFT JOIN Bucht OtherBucht on OtherBucht.ID = OLdBucht.BuchtIDConnectedOtherBucht
LEFT JOIN Telephone t on OldBucht.SwitchPortID = t.SwitchPortID
LEFT JOIN ADSLPapPort app on app.TelephoneNo = t.TelephoneNo 
WHERE 
	(@RequestIDs IS NULL OR LEN(@RequestIDs) = 0 OR tpi.RequestID in (Select * From dbo.ufnSplitList(@RequestIDs))) and OLDBucht.BuchtTypeID != 9
	AND (OldCabinetInput.ID = OLDBucht.CabinetInputID) 

END
GO


