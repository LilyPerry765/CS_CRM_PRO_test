GO	
ALTER TABLE ADSL	
add CustomerGroupID int
GO
exec sp_addextendedproperty 'MS_Description',N'گروه مشتری','schema',dbo,'table',ADSL,'column',CustomerGroupID
go
ALTER TABLE ADSL
add constraint FK_ADSL_ADSLCustomerGroup_CustomerGroupID foreign key (CustomerGroupID) references ADSLCustomerGroup (ID) 
GO
--پر کردن مقدار گروه مشتری برای ای دی اس ال های قبلی با توجه به درخواست های ای دی اس ال
;WITH Duplicated AS
(
	SELECT  ADL.TelephoneNo
	FROM 
		ADSL ADL
	INNER JOIN 
		Request RE ON ADL.TelephoneNo = RE.TelephoneNo
	INNER JOIN 
		ADSLRequest ADR ON ADR.ID = RE.ID
	WHERE 
		RE.RequestTypeID = 35 AND RE.IsCancelation = 0 AND RE.EndDate IS NOT NULL
	GROUP BY
		ADL.TelephoneNo
	HAVING
		COUNT(ADL.TelephoneNo) > 1
)
UPDATE ADL
SET CustomerGroupID = ADR.CustomerGroupID
FROM 
	ADSL ADL
INNER JOIN 
	Request RE ON ADL.TelephoneNo = RE.TelephoneNo
INNER JOIN 
	ADSLRequest ADR ON ADR.ID = RE.ID
WHERE 
	RE.RequestTypeID = 35 
	AND 
	RE.IsCancelation = 0 
	AND 
	RE.EndDate IS NOT NULL
	AND
	RE.TelephoneNo NOT IN (SELECT * FROM Duplicated)
 