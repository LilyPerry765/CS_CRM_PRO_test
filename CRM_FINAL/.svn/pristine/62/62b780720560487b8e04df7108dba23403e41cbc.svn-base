
update   Telephone set RoundType = null , IsRound = 0
FROM            Switch INNER JOIN
                         SwitchPrecode ON Switch.ID = SwitchPrecode.SwitchID INNER JOIN
                         Telephone ON SwitchPrecode.ID = Telephone.SwitchPrecodeID INNER JOIN
                         Center ON Switch.CenterID = Center.ID INNER JOIN
                         Region ON Center.RegionID = Region.ID INNER JOIN
                         City ON Region.CityID = City.ID
WHERE        (City.ID = 7)



Update T
  set RoundType = case p.ID_Gold when 1 then 0 when 2 then 1 when 3 then 2 when 4 then null else null end
  from [CRM].dbo.Telephone as T join [Salas].[dbo].[PHON] as p on T.TelephoneNo = p.TEL_PISH + p.TEL


  Update T
  set RoundType = case p.ID_ROND when 1 then null else 5 end
  from [CRM].dbo.Telephone as T join [Salas].[dbo].[PHON] as p on T.TelephoneNo = p.TEL_PISH + p.TEL



  update [CRM].dbo.Telephone
    set IsRound = 1
	where RoundType is not null
    


