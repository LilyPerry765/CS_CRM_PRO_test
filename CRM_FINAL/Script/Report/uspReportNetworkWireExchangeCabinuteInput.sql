
/****** Object:  StoredProcedure [dbo].[uspReportNetworkWireExchangeCabinuteInput]    Script Date: 2/18/2015 10:00:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--exec [dbo].[uspReportNetworkWireExchangeCabinuteInput] '9311190002'
ALTER  Procedure [dbo].[uspReportNetworkWireExchangeCabinuteInput]
(
	  @RequestIDs varchar(max) = null
)
AS
BEGIN


;with cte1 AS 
(

select *,((rowno - 1) * PostContactCount ) + 1 FromPostContactNew , (Rowno * PostContactCount)  ToPostContactNew
From 
(
SELECT     
 PostContact.connectionNo,Post.Number PostNumber,( Post.ToPostContact - Post.FromPostContact + 1) PostContactCount,
 ROW_NUMBER() over (partition by c.cabinetnumber  order by Post.Number) rowno,
 CASE WHEN Bucht.[Status] = 13 Then [dbo].[GetPCMPhoneNo](Bucht.CabinetInputID) Else t.TelephoneNo END TelephoneNo
 FROM            
ExchangeCabinetInput eciI
Left join CabinetInput ci on ci.InputNumber between  (select InputNumber from CabinetInput where ID = eciI.FromOLDCabinetInputID)  and (select InputNumber from CabinetInput where ID = eciI.ToOLDCabinetInputID) and ci.CabinetID  = eciI.OldCabinetID
LEFT JOIN Cabinet c on C.ID = ci.cabinetId																 				  
LEFT JOIN Bucht ON ci.ID = Bucht.CabinetInputID 
LEFT JOIN PostContact ON Bucht.ConnectionID = PostContact.ID 
LEFT JOIN Post ON PostContact.PostID = Post.ID
LEFT JOIN Telephone t on Bucht.SwitchPortID = t.SwitchPortID

where 
	eciI.ID =  @RequestIDs 
	AND (case when eciI.IsChangePost = 1 then  Postcontact.[status] ELSE 1 END ) = 1
	AND BUCHT.[status] != 13
	
)T
)


, OldInfo as 
(
select *
From 
(
SELECT     
 PostContact.connectionNo,Post.Number PostNumber,( Post.ToPostContact - Post.FromPostContact + 1) PostContactCount,  ci.InputNumber, c.CabinetNumber, 
 ROW_NUMBER() over (partition by c.cabinetNumber  order by ci.InputNumber) rowno,
 --t.TelephoneNo
 CASE WHEN Bucht.[Status] = 13 Then [dbo].[GetPCMPhoneNo](CabinetInputID) Else t.TelephoneNo END TelephoneNo
 FROM            
ExchangeCabinetInput eciI
Left join CabinetInput ci on ci.InputNumber between  (select InputNumber from CabinetInput where ID = eciI.FromOLDCabinetInputID)  and (select InputNumber from CabinetInput where ID = eciI.ToOLDCabinetInputID) and ci.CabinetID  = eciI.OldCabinetID
LEFT JOIN Cabinet c on C.ID = ci.cabinetId																 				  
LEFT JOIN Bucht ON ci.ID = Bucht.CabinetInputID 
LEFT JOIN PostContact ON Bucht.ConnectionID = PostContact.ID 
LEFT JOIN Post ON PostContact.PostID = Post.ID


LEFT JOIN Telephone t on Bucht.SwitchPortID = t.SwitchPortID
Left join [Address] adrs on adrs.id = t.InstallAddressID
Left join Customer on t.CustomerID = Customer.ID
where 
	eciI.ID =  @RequestIDs 
	AND BUCHT.BuchtTypeID NOT IN (8,9)
) T

)
, NewInfo as 
(
SELECT     
PostContact.connectionNo NewconnectionNo,Post.Number NewPostNumber,ci.InputNumber NewInputNumber, c.CabinetNumber NewCabinetNumber, ROW_NUMBER() over (partition by Ci.CabinetID order by ci.InputNumber) NewPostrowno
,
 NEWBuchtInfo = Concat( N'ام دی اف:' , MDF.Number, N'ردیف:', VerticalMDFColumn.VerticalCloumnNo , N'طبقه:' , VerticalMDFRow.VerticalRowNo ,N'اتصالی:', Bucht.BuchtNo)

FROM            
ExchangeCabinetInput eciI
Left join CabinetInput ci on ci.InputNumber between  (select InputNumber from CabinetInput where ID = eciI.FromNewCabinetInputID)  and (select InputNumber from CabinetInput where ID = eciI.ToNewCabinetInputID) and ci.CabinetID  = eciI.NewCabinetID
LEFT JOIN Cabinet c on C.ID = ci.cabinetId																	 				  
LEFT JOIN Bucht ON ci.ID = Bucht.CabinetInputID 
LEFT JOIN PostContact ON Bucht.ConnectionID = PostContact.ID 
LEFT JOIN Post ON PostContact.PostID = Post.ID


----NEWBuchtInfo
JOIN VerticalMDFRow on Bucht.MDFRowID = VerticalMDFRow.ID 
JOIN VerticalMDFColumn on VerticalMDFRow.VerticalMDFColumnID = VerticalMDFColumn .ID
JOIN MDFFrame on MDFFrame.ID = VerticalMDFColumn.MDFFrameID
JOIN MDF on MDF.ID = MDFFrame.MDFID
-----
where eciI.ID = @RequestIDs
)
,cte3 as
(

	select  
	eci.IsChangePost, 
	Newpost.Number MiddlePostNumber, 
	Pc.ConnectionNo MiddleConnectionNo,
	( NewPost.ToPostContact - NewPost.FromPostContact + 1) PostContactMiddleCount, 
	Row_Number() over (partition by c.ID order by NewPost.Number) NewMiddleRowno
	
			from ExchangeCabinetInput eci 
		    left JOIN Post Newpost ON Newpost.CabinetID = eci.NewCabinetID and  Newpost.Number between (select Number from Post where ID = eci.FromNewPostID) and (select Number from Post where ID = eci.FromNewPostID) - 1 + 
			                                                                 (SELECT     
																				Count(Distinct(Post.ID))
																			 FROM            
																				ExchangeCabinetInput eciI
																				Left join CabinetInput ci on ci.InputNumber between  (select InputNumber from CabinetInput where ID = eciI.FromOLDCabinetInputID)  and (select InputNumber from CabinetInput where ID = eciI.ToOLDCabinetInputID) and ci.CabinetID  = eciI.OldCabinetID 
																				LEFT JOIN Bucht ON ci.ID = Bucht.CabinetInputID 
																				LEFT JOIN PostContact ON Bucht.ConnectionID = PostContact.ID 
																				LEFT JOIN Post ON PostContact.PostID = Post.ID
																			 where eciI.ID = eci.ID
																			) 
			left join PostContact as Pc on Pc.PostID = Newpost.ID
			left join Cabinet c on NewCabinetID = c.ID
	where eci.ID = @RequestIDs
)
, NewAbone AS
(
	select 

		(
		   CASE WHEN IsChangePost = 1 THEN	concat(N'کافو:' , NEWInfo.NewCabinetNumber, N'مرکزی:', NEWInfo.NewInputNumber, N'پست:' , cte3.MiddlePostNumber , N'اتصالی', cte3.MiddleConnectionNo)  
		   ELSE 
			concat(N'کافو:' , NEWInfo.NewCabinetNumber, N'مرکزی:', NEWInfo.NewInputNumber, N'پست:' , OLDInfo.PostNumber , N'اتصالی', OLDInfo.ConnectionNo)
		   END
		)
		NEWAboneInfo
		,cte1.TelephoneNo
		,NEWBuchtInfo
	from  cte1 

	LEFT JOIN OLDInfo on OLDINFO.TELEPHONENO = cte1.TELEPHONENO
	Left join NEWInfo on  OldInfo.rowno = NEWInfo.Newpostrowno
	Left join cte3 on   NewMiddleRowno = (FromPostContactNew + OLDInfo.ConnectionNo - 1)
	
 )

 select 
 
	R.TelephoneNo,
	concat(Customer.FirstNameOrTitle , ' ' , Customer.LastName) CustomerFullName,
	adrs.AddressContent,
	na.NewAboneInfo,
	NEWBuchtInfo,
	OLDAboneInfo = concat(N'کافو:' , CabinetNumber, N'مرکزی:', InputNumber, N'پست:' , OldPostNumber , N'اتصالی', ConnectionNo) ,
	OLDBuchtInfo = Concat( N'ام دی اف:' , MDFNumber, N'ردیف:', VerticalCloumnNo , N'طبقه:' , VerticalRowNo ,N'اتصالی:', BuchtNo)

	
 from (
select 
CASE WHEN OldBucht.[status] = 13 Then [dbo].[GetPCMPhoneNo](OldCabinetInput.ID) Else t.TelephoneNo END TelephoneNo,
	OldCabinetInput.ID OldCabinetInputID,
	OldBucht.[status],
	OldBucht.SwitchPortID,
	OLDCabinet.CabinetNumber,
	OLDCabinetInput.InputNumber,
	OldPost.Number OldPostNumber, 
	OldPostContact.ConnectionNo,
	MDF.Number MDFNumber,
	VerticalMDFColumn.VerticalCloumnNo , 
	VerticalMDFRow.VerticalRowNo ,
	OldBucht.BuchtNo,

	oldbucht.BuchtTypeID
	

from ExchangeCabinetInput eci 
 JOIN Request r on r.ID = eci.ID
 JOIN Cabinet OldCabinet on oldcabinet.id = OldCabinetID
 JOIN CabinetInput OldCabinetInput on OldCabinetInput.cabinetID = OldCabinet.ID and (OldCabinetInput.ID between eci.FromOldCabinetInputID and eci.ToOldCabinetInputID)
 JOIN Bucht OldBucht on OldCabinetInput.id = OldBucht.CabinetInputID
 JOIN PostContact OldPostContact on OldPostContact.ID = OldBucht.ConnectionID 
 JOIN Post OldPost on OldPost.ID = OldPostContact.PostID
 LEFT JOIN Telephone t on OldBucht.SwitchPortID = t.SwitchPortID
----OLDBuchtInfo
JOIN VerticalMDFRow on OldBucht.MDFRowID = VerticalMDFRow.ID 
JOIN VerticalMDFColumn on VerticalMDFRow.VerticalMDFColumnID = VerticalMDFColumn .ID
JOIN MDFFrame on MDFFrame.ID = VerticalMDFColumn.MDFFrameID
JOIN MDF on MDF.ID = MDFFrame.MDFID
-----


WHERE         
	
	  (@RequestIDs IS NULL OR LEN(@RequestIDs) = 0 OR eci.ID in (Select * From dbo.ufnSplitList(@RequestIDs)))
	  	and (OldPostcontact.status = 1 or OldPostcontact.status = 0)
		AND OldBUCHT.BuchtTypeID NOT IN (8,9)
)r
LEFT JOIN Telephone t on r.SwitchPortID = t.SwitchPortID


Left join NewAbone na on  na.TelephoneNo = (case when t.switchPortid is null THEN [dbo].[GetPCMPhoneNo](OldCabinetInputID)  else t.telephoneno END)
LEFT JOIN Telephone nat on nat.TELEPHONENO = R.TELEPHONENO
LEFT join Customer on nat.CustomerID = Customer.ID
LEFT join [Address] adrs on adrs.id = nat.InstallAddressID
order by MDFNumber,VerticalCloumnNo,VerticalRowNo,BuchtNo


END
