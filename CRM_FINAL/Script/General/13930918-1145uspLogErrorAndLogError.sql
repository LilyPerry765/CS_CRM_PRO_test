--if object_id('ErrorLog') is not null
--	drop table ErrorLog
--GO
--create table ErrorLog
--(
--	ErrorLogID int not null primary key identity(1,1),
--	ErrorTime datetime  default(getdate()) not null,
--	UserName sysname,
--	ErrorNumber int not null,
--	ErrorSeverity int,
--	ErrorState int,
--	ErrorProcedure nvarchar(250),
--	ErrorLine int,
--	ErrorMessage nvarchar(4000) not null
--)
--exec sp_addextendedproperty 'MS_Description',N'جدول خطا های رخ داده در بدنه رویه ها','schema',dbo,'table',ErrorLog,'column',ErrorLogID
--exec sp_addextendedproperty 'MS_Description',N'زمان خطا','schema',dbo,'table',ErrorLog,'column',ErrorTime
--exec sp_addextendedproperty 'MS_Description',N'نام کاربر','schema',dbo,'table',ErrorLog,'column',UserName
--exec sp_addextendedproperty 'MS_Description',N'شماره خطا','schema',dbo,'table',ErrorLog,'column',ErrorNumber
--exec sp_addextendedproperty 'MS_Description',N'میزان اهمیت','schema',dbo,'table',ErrorLog,'column',ErrorSeverity
--exec sp_addextendedproperty 'MS_Description',N'نام رویه محل خطا','schema',dbo,'table',ErrorLog,'column',ErrorProcedure
--exec sp_addextendedproperty 'MS_Description',N'شماره خط خطا','schema',dbo,'table',ErrorLog,'column',ErrorLine
--exec sp_addextendedproperty 'MS_Description',N'پیغام خطا','schema',dbo,'table',ErrorLog,'column',ErrorMessage

USE [CRM]
GO
/****** Object:  StoredProcedure [dbo].[uspLogError]    Script Date: 12/8/2014 11:16:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--برای ثبت خطاهای ایجاد شده در بدنه رویه ها مورد استفاده قرار می گیرد
CREATE proc [dbo].[uspLogError]
(
	@ErrorIsLogged bit =1 output --مقدار این متغیر نشان میدهد که خطا ثبت شده است یاخیر
)
AS
BEGIN
	BEGIN TRY
		
		SELECT  @ErrorIsLogged = 0;

		--اگر هیچ خطایی وجود نداشت نیازی به ثبت نیست
		if ERROR_NUMBER() is null
			return;

		--return if inside an uncommitable transaction .
		if XACT_STATE() = -1
			begin
				ROLLBACK TRANSACTION;
				return;
			end

		INSERT INTO ErrorLog
		(
			UserName,
			ErrorNumber,
			ErrorSeverity,
			ErrorState,
			ErrorProcedure,
			ErrorLine,
			ErrorMessage
		)
		values
		(
			CONVERT(sysname,CURRENT_USER),
			ERROR_NUMBER(),
			ERROR_SEVERITY(),
			ERROR_STATE(),
			ERROR_PROCEDURE(),
			ERROR_LINE(),
			ERROR_MESSAGE()
		);

		SELECT @ErrorIsLogged = 1 ;
	END TRY
	BEGIN CATCH
		SELECT @ErrorIsLogged = 0
	END CATCH
END
