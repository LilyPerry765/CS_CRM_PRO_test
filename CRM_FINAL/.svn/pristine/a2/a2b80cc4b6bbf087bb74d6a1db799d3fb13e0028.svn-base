USE [CRM]
GO
/****** Object:  StoredProcedure [dbo].[uspReportTrasnlationOpticalCabinetToNormal]    Script Date: 12/7/2014 12:08:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[uspReportTrasnlationOpticalCabinetToNormal]
(
	@requestsId varchar(max) = null
)
AS
BEGIN
		BEGIN TRY

			SELECT 
				R.ID RequestID,
				TC.ToTelephoneNo,
				TC.FromTelephoneNo,
				ISNULL(C.FirstNameOrTitle,'') FirstNameOrTitle,
				ISNULL(C.LastName,'') LastName,
				ISNULL(A.AddressContent,'') InstallAddress,
				ISNULL(A.PostalCode,'') InstallPostalCode,
				ISNULL(AA.AddressContent,'') CorrespondenceAddress,
				ISNULL(AA.PostalCode,'') CorrespondencePostalCode
			FROM 
				Request R
			INNER JOIN 
				TranslationOpticalCabinetToNormal TN ON TN.ID = R.ID
			INNER JOIN 
				TranslationOpticalCabinetToNormalConncetions TC ON TN.ID = TC.RequestID
			LEFT JOIN
				[Address] A ON A.ID = TC.InstallAddressID
			LEFT JOIN
				[Address] AA ON AA.ID = TC.CorrespondenceAddressID
			LEFT JOIN 
				Customer C ON C.ID = TC.CustomerID
			WHERE 
				TN.[Type] = 2 --برگردان جزئی
				AND
				(@requestsId IS NULL OR LEN(@requestsId) = 0 OR R.ID IN (SELECT * FROM DBO.ufnSplitList(@requestsId)))

		END TRY
		BEGIN CATCH
			THROW;
		END CATCH
END