use crm
go

insert into [CRM].[dbo].[VisitAddress]
  SELECT
	  [address].id,--[AddressID]
      r.id,--[RequestID]
      null,--[RelatedVisitID]
      r.EndDate,--[VisitDate]
      '00:00',--[VisitHour]
      null,--[AirCableMeter]
      1,--[IsOutBound]
      addr.METR,--[OutBoundMeter]
      null,--[OutBoundEstablishDate]
      null,--[CommentStatus]
      null,--[InsertDate]
      null,--[SixMeterMasts]
      null,--[EightMeterMasts]
      null,--[ThroughWidth]
      null,--[CrossPostID]
      0,--[Status]
      1,--[ConfirmInvestigatePossibility]
      null--[CableMeter]

  FROM [Salas].[dbo].[ADDRESS] as addr

   join [Salas].[dbo].[SUBSCRIB] as s 
   on s.ID_FINANCE=addr.ID_FINANCE
   
   join [Address]
   on [ADDRESS].ElkaID=addr.ID_FINANCE
   
   join Telephone as tel 
   on tel.TelephoneNo=s.newtelephone
  
   join Request as r 
   on r.TelephoneNo=tel.TelephoneNo
  
    group by   
      [address].id,
      r.id,
      r.EndDate,
      addr.METR,
	  tel.InstallAddressID,
	  RequestTypeID

  having addr.metr>0 
  and tel.InstallAddressID=ADDRESS.id
  and r.RequestTypeID=1




  

