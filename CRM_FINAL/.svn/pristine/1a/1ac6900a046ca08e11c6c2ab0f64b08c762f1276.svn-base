use crm
go

--update  [Salas].[dbo].[SUBSCRIB] set newtelephone=[TEL_PISH]+TEL
update IR set IR.ChargingType = case ty.ID when 1 then 0
		                      when 2 then 1
		                      when 3 then 3
							  when 4 then 4
							  when 11 then 0
							  end
   FROM Salas.[dbo].[ADDRESS] as a join  [Salas].[dbo].[TYADR] as ty on a.ID_TYADR = ty.ID
  join Salas.[dbo].[Subscrib] as S on  A.ID_FINANCE = S.ID_FINANCE 
  join [CRM].[dbo].Request as R on R.TelephoneNo = s.NewTelephone
  join [CRM].[dbo].InstallRequest as IR on IR.RequestID = R.ID
  where R.CreatorUserID is null 


 ---- update IR set IR.ChargingType = case ty.ID when 1 then 0
	--	                      when 2 then 1
	--	                      when 3 then 3
	--						  when 4 then 4
	--						  end

	--select count(*) 
 --  FROM [OldCustomerDate].[dbo].[ADDRESS] as a join  [OldCustomerDate].[dbo].[TYADR] as ty on a.ID_TYADR = ty.ID
 -- join [OldCustomerDate].[dbo].[Subscrib] as S on  A.ID_FINANCE = S.ID_FINANCE 
 -- join [CRM].[dbo].Telephone as tel on tel.TelephoneNo = s.NewTelephone
 -- where status = 2 and tel.ChargingType is null



