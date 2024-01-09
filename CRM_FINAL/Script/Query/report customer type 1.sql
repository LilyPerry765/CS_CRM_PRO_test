select  T.TelephoneNo,C.CenterName , CT.Title as CustomerType , CG.Title as CustomerGroup ,
 CASE  T.Status
    WHEN 0 THEN N'آزاد'
    WHEN 1 THEN N'رزرو'
    WHEN 2 THEN N'دایری'
	WHEN 3 THEN N'قطع'
	WHEN 4 THEN N'در حال تغییر مکان'
	WHEN 5 THEN N'تخلیه'
	WHEN 6 THEN N'خراب'
	else N'نامشخص' END  as Status
from Telephone as T
join  CustomerType as CT on  T.CustomerTypeID = CT.ID
left join CustomerGroup as CG on CG.ID = T.CustomerGroupID
join Center as C on C.ID = T.CenterID
where CG.ID is null
order by C.CenterName , T.TelephoneNo