

/****** Object:  StoredProcedure [dbo].[uspReportNetworkWireExchangeCentralPost]    Script Date: 1/28/2015 3:07:25 PM ******/
DROP PROCEDURE [dbo].[uspReportNetworkWireExchangeCentralPost]
GO

/****** Object:  StoredProcedure [dbo].[uspReportNetworkWireExchangeCentralPost]    Script Date: 1/28/2015 3:07:25 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--exec [dbo].[uspReportNetworkWireExchangeCentralPost] '9311110001'
CREATE  Procedure [dbo].[uspReportNetworkWireExchangeCentralPost]
(
	  @RequestIDs varchar(max) = null
)
AS
BEGIN


;with ReplacePostInfo AS
(
select 
tpi.RequestID,
pc.ConnectionNo,
Post.ID PostID,
	concat(N'کافو:' , NewCabinet.CabinetNumber, N'مرکزی:', NewCabinetInput.InputNumber, N'پست:' , Post.Number , N'اتصالی', pc.ConnectionNo)hh
	
from TranslationPostInput tpi
LEFT JOIN TranslationPostInputConnections tpic on tpic.RequestID = tpi.RequestID
LEFT JOIN Post ON tpi.FromPostID = Post.ID
LEFT JOIN PostContact pc on pc.PostID = Post.ID
LEFT JOIN Bucht NewBucht on newBucht.ConnectionID = pc.ID
LEFT JOIN CabinetInput NewCabinetInput on NewCabinetInput.ID = NEWBucht.CabinetInputID
LEFT JOIN Cabinet NewCabinet on Newcabinet.id = NEWCabinetInput.CabinetID
WHERE         
 (@RequestIDs IS NULL OR LEN(@RequestIDs) = 0 OR tpi.RequestID in (Select * From dbo.ufnSplitList(@RequestIDs)))
)


select 
	dbo.[GetBuchtInfo](OldBucht.ID) OldBuchtInfo, 
	dbo.[GetBuchtInfo](NewBucht.ID) NEWBuchtInfo, 
	CASE WHEN OldBucht.[Status] = 13 Then [dbo].[GetPCMPhoneNo](OldBucht.CabinetInputID) Else t.TelephoneNo END TelephoneNo,
	
	concat(Customer.FirstNameOrTitle , ' ' , Customer.LastName) CustomerFullName,
	Customer.UrgentTelNo ,
	
	(
	   CASE WHEN PC.ConnectionNo IS NOT NULL  THEN	concat(N'کافو:' , NEWCabinet.CabinetNumber, N'مرکزی:', NEWCabinetInput.InputNumber, N'پست:' , Post.Number , N'اتصالی', pc.ConnectionNo) 
	   ELSE 
	  ''-- concat(N'کافو:' , NEWCabinet.CabinetNumber, N'مرکزی:', NEWCabinetInput.InputNumber, N'پست:' , OldPost.Number , N'اتصالی', OldPostContact.ConnectionNo)
	   END
	)
	NEWAboneInfo,
	OLDAboneInfo = concat(N'کافو:' , OLDCabinet.CabinetNumber, N'مرکزی:', OLDCabinetInput.InputNumber, N'پست:' , OldPost.Number , N'اتصالی', OldPostContact.ConnectionNo) ,
	adrs.AddressContent

from TranslationPostInput tpi
LEFT JOIN TranslationPostInputConnections tpic on tpic.RequestID = tpi.RequestID
LEFT JOIN Request r on r.ID = tpi.RequestID
LEFT JOIN Cabinet OldCabinet on oldcabinet.id = tpi.FromCabinetID
LEFT JOIN CabinetInput OldCabinetInput on OldCabinetInput.CabinetID = OldCabinet.ID 
LEFT JOIN PostContact OldPostContact on OldPostContact.ID = tpic.ConnectionID
LEFT JOIN Bucht OldBucht on OLDBucht.ConnectionID = OldPostContact.ID
LEFT JOIN Post OldPost on OldPost.ID = OldPostContact.PostID

LEFT JOIN PostContact pc on pc.ID = tpic.NewConnectionID
LEFT JOIN Post ON Post.ID = pc.PostID

LEFT JOIN CabinetInput NewCabinetInput on NewCabinetInput.ID = tpic.CabinetInputID

LEFT JOIN Cabinet NewCabinet on Newcabinet.id =  NewCabinetInput.CabinetID
LEFT JOIN Bucht NewBucht on newBucht.cabinetInputID = tpic.CabinetInputID

LEFT JOIN Telephone t on OldBucht.SwitchPortID = t.SwitchPortID
Left join [Address] adrs on adrs.id = t.InstallAddressID
Left join Customer on t.CustomerID = Customer.ID
--Left join ReplacePostInfo rpi on rpi.ConnectionNo = tpic.ConnectionID --and rpi.PostID = Post.ID

WHERE         

 
 (@RequestIDs IS NULL OR LEN(@RequestIDs) = 0 OR tpi.RequestID in (Select * From dbo.ufnSplitList(@RequestIDs))) and OLDBucht.BuchtTypeID != 9
 AND OldCabinetInput.ID = OLDBucht.CabinetInputID
	
Order by OLDCabinet.CabinetNumber,OLDCabinetInput.InputNumber, OldPost.Number ,OldPostContact.ConnectionNo

END
GO


