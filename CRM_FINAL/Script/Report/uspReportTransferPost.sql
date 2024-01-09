USE [CRM]
GO

/****** Object:  StoredProcedure [dbo].[uspReportTransferPost]    Script Date: 2/16/2015 3:09:58 PM ******/
DROP PROCEDURE [dbo].[uspReportTransferPost]
GO

/****** Object:  StoredProcedure [dbo].[uspReportTransferPost]    Script Date: 2/16/2015 3:09:58 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--exec [dbo].[Report.ChangeLocationNetworkSpecialWire] '9309170365'
CREATE  Procedure [dbo].[uspReportTransferPost]
(
	  @RequestIDs varchar(max) = null
)
AS
BEGIN

;with PostTranslation  AS
(
	SELECT 
	RequestID,	OldCabinetID,	OldPostID,	NewPostID,	OldPostContactID,	NewPostContactID,	OverallTransfer,R.TelephoneNo

	FROM TranslationPost
	LEFT JOIN REQUEST R on R.ID = RequestID
	WHERE
	 R.ID = @RequestIDs
		 
)
, OLDPOST AS
(
	SELECT pt.*, PC.ConnectionNo , PC.ConnectionType  , cabinet.CabinetNumber , p.Number POSTNUMBER
	FROM PostTranslation pt
	LEFT JOIN PostContact PC on pt.OLDPOSTID = PC.PostID
	LEFT JOIN CABINET on OLDCABINETID = Cabinet.ID
	LEFT JOIN POST p on P.ID = PostID
	where  ConnectionType != 5 
)
, NEWPOST AS
(
	SELECT pt.*, PC.ConnectionNo , PC.ConnectionType , p.Number POSTNUMBER
	FROM PostTranslation pt
	LEFT JOIN PostContact PC on pt.NEWPOSTID = PC.PostID
	LEFT JOIN POST p on P.ID = PostID
	where  ConnectionType != 5 
)
, OverallTransferAll AS 
(
	SELECT 
	op.RequestID, 
	op.CabinetNumber,	
	op.POSTNUMBER OLDPOSTNUMBER,	
	np.POSTNUMBER NEWPOSTNUMBER,	
	op.OldPostContactID,	
	op.NewPostContactID,	
	op.OverallTransfer,
	op.TelephoneNo , 
	op.ConnectionNo opConnectionNo, 
	op.ConnectionType opConnectionType,
	np.ConnectionNo npConnectionNo, 
	np.ConnectionType npConnectionType

	FROM OLDPOST op
	LEFT JOIN NEWPOST np on np.ConnectionNo = op.ConnectionNo
)
--select * from OverallTransfer
, TransferPostAll AS
(
 SELECT OT.* , OLDPC.ConnectionNo OLDPCConnectionNo,  NEWPC.ConnectionNo NEWPCConnectionNo
 FROM OverallTransferAll OT
 LEFT JOIN POSTCONTACT OLDPC ON OLDPC.ID = OldPostContactID
 LEFT JOIN POSTCONTACT NEWPC ON NEWPC.ID = NEWPostContactID
)

SELECT 
 CabinetNumber,
 OLDPOSTNUMBER,
 NEWPOSTNUMBER,
 CASE WHEN OverallTransfer = 1 THEN opConnectionNo ELSE OLDPCConnectionNo END OLDPCConnectionNo,
 CASE WHEN OverallTransfer = 1 THEN npConnectionNo ELSE NEWPCConnectionNo END NEWPCConnectionNo,
 CASE WHEN opConnectionType = 4 THEN N'دارد' ELSE N'ندارد' END PCMSTATE
FROM TransferPostAll
WHERE (Case When OLDPCConnectionNo IS NULL THEN opConnectionNo ELSE OLDPCConnectionNo END)  = opConnectionNo
END
GO


