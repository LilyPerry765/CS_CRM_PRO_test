

/****** Object:  StoredProcedure [dbo].[uspReportMDFExchangeCentralPost]    Script Date: 1/28/2015 3:08:24 PM ******/
DROP PROCEDURE [dbo].[uspReportMDFExchangeCentralPost]
GO

/****** Object:  StoredProcedure [dbo].[uspReportMDFExchangeCentralPost]    Script Date: 1/28/2015 3:08:24 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE  Procedure [dbo].[uspReportMDFExchangeCentralPost]
(
	  @RequestIDs varchar(max) = null
)
AS
BEGIN

;with OldBuchtInfo AS
(
select 
	dbo.[GetBuchtInfo](OldBucht.ID) OldBuchtInfo, 
	CASE WHEN OldBucht.[Status] = 13 Then [dbo].[GetPCMPhoneNo](OldBucht.CabinetInputID) Else t.TelephoneNo END TelephoneNo, 
	CASE WHEN (app.RowNo IS NULL AND app.ColumnNo IS NULL AND app.BuchtNo IS NULL ) THEN ''
		 ELSE concat(N'ردیف:' , app.RowNo, N'طبقه:', app.ColumnNo , N'اتصالی:' , app.BuchtNo) 
		 END ADSLPapInfo
	, ROW_NUMBER() OVER (partition By OldCabinet.ID order by OldCabinet.ID,OldCabinetInput.ID) RowNo,
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

LEFT JOIN PCMPORT on PCMPORT.ID = OLDBucht.PCMPORTID
LEFT JOIN Bucht OtherBucht on OtherBucht.ID = OLdBucht.BuchtIDConnectedOtherBucht
LEFT JOIN Telephone t on OldBucht.SwitchPortID = t.SwitchPortID
LEFT JOIN ADSLPapPort app on app.TelephoneNo = t.TelephoneNo 
WHERE 
	(@RequestIDs IS NULL OR LEN(@RequestIDs) = 0 OR tpi.RequestID in (Select * From dbo.ufnSplitList(@RequestIDs)))
	AND (OldCabinetInput.ID = OLDBucht.CabinetInputID) 
)
,NewBuchtInfo AS
(
select 
	dbo.[GetBuchtInfo](NEWBucht.ID) NEWBuchtInfo, 
	ROW_NUMBER() OVER (partition By NewCabinet.ID order by NewCabinet.ID,NewCabinetInput.ID) RowNo


from TranslationPostInput tpi
LEFT JOIN TranslationPostInputConnections tpic on tpic.RequestID = tpi.RequestID
LEFT JOIN Cabinet NEWCabinet on NEWcabinet.id = tpi.ToCabinetID

LEFT JOIN CabinetInput NEWCabinetInput on NEWCabinetInput.cabinetID = NEWCabinet.ID  
LEFT JOIN Bucht NewBucht on NewBucht.CabinetInputID = tpic.CabinetInputID

WHERE 
		(@RequestIDs IS NULL OR LEN(@RequestIDs) = 0 OR tpi.RequestID in (Select * From dbo.ufnSplitList(@RequestIDs)))
	
)

Select 
		OldBuchtInfo.ADSLPapInfo  ,
		OldBuchtInfo.OldBuchtInfo ,
		OldBuchtInfo.OtherBuchtInfo ,
		OldBuchtInfo.PCMINFO ,
		OldBuchtInfo.TelephoneNo,
		OldBuchtInfo.BuchtStatus,
		NewBuchtInfo.NEWBuchtInfo
from 
		OLDBuchtInfo
		JOIN NewBuchtInfo on OldBuchtInfo.RowNo = NewBuchtInfo.RowNo
		END
GO


