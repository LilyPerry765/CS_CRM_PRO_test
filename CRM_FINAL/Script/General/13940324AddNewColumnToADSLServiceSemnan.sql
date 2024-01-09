--13940324
ALTER TABLE ADSLService
ADD ServiceCode int
GO
exec sp_addextendedproperty N'MS_Description',N'کد سرویس','Schema',dbo,'table',ADSLService,'column',ServiceCode
GO