USE [CRM]
GO
/****** Object:  StoredProcedure [dbo].[uspLogError]    Script Date: 12/8/2014 11:16:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--برای ثبت خطاهای ایجاد شده در بدنه رویه ها مورد استفاده قرار می گیرد
ALTER proc [dbo].[uspLogError]
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
