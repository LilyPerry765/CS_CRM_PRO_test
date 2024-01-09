select  R.ID , R.TelephoneNo,C.CenterName , CT.Title as CustomerType , CG.Title as CustomerGroup ,
iif(R.TelephoneNo is not null, CASE  T.Status
    WHEN 0 THEN N'آزاد'
    WHEN 1 THEN N'رزرو'
    WHEN 2 THEN N'دایری'
	WHEN 3 THEN N'قطع'
	WHEN 4 THEN N'در حال تغییر مکان'
	WHEN 5 THEN N'تخلیه'
	WHEN 6 THEN N'خراب'
	else N'نامشخص' END , '' )
from Request as R 
join  InstallRequest as IR on R.ID = IR.RequestID
join  CustomerType as CT on  IR.TelephoneType = CT.ID
left join CustomerGroup as CG on CG.ID = IR.TelephoneTypeGroup
join Center as C on C.ID = R.CenterID
left join Telephone as T on R.TelephoneNo = T.TelephoneNo
where   R.EndDate is null and CG.ID is null
order by C.CenterName , R.TelephoneNo , R.ID