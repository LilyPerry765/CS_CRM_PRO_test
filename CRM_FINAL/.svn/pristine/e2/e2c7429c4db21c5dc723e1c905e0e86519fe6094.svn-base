--لیست پرداختی های(فقط ودیعه) تلفن در روال دایری
;with cte as
(
	select  
		row_number() over(order by t.telephoneno) [Index],
		t.TelephoneNo,
		b.Cost,
		rp.AmountSum,
		case when t.Status = 0 then N'آزاد'
			 when t.status = 1 then N'رزرو'
			 when t.status = 2 then N'دایری'
			 when t.status = 3 then N'قطع'
			 when t.status = 4 then N'در حال تغییر مکان'
			 when t.status = 5 then N'تخلیه'
			 when t.status = 6 then N'خراب'
			 when t.status = 7 then N'جمع آوری منصوبات'
		end CurrentTelephoneStatus
	from 
		request r
	join
		InstallRequest ir on r.id=ir.RequestID
	join
		RequestPayment rp on r.id=rp.RequestID
	join
		BaseCost b on b.id=rp.BaseCostID
	join 
		Telephone t on t.TelephoneNo=r.TelephoneNo
	where 
		b.IsDeposit=1 --ودیعه
		and
		rp.IsPaid=1 --پرداخت شده
		and
		rp.IsKickedBack  = 0 --بار پرداخت نشده باشد
	
)
select * 
into test..TelephonesStatistics
from 
	cte

