alter table InstallRequest
add MethodOfPaymentForTelephoneConnection tinyint null
go
exec sp_addextendedproperty N'MS_Description',N'روش پرداخت هزینه اتصال تلفن','schema',dbo,'table',InstallRequest,'column',MethodOfPaymentForTelephoneConnection

