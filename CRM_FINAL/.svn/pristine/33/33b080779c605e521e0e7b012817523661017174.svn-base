use crm
go

update telephone set status=2 where telephoneno in
  (
  select telephoneno from telephone  
  where telephoneno in
		(SELECT newtelephone
		FROM Salas.[dbo].[SUBSCRIB]
		where STOP_DATE=99999999 )
  and status=0
  )

  update telephone set status=5 where telephoneno in
  (
  select telephoneno from telephone  
  where telephoneno in
		(SELECT newtelephone
		FROM Salas.[dbo].[SUBSCRIB]
		where STOP_DATE!=99999999 )
  and status=0
  )

