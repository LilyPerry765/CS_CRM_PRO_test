USE [CRM]
GO
/****** Object:  StoredProcedure [dbo].[uspReportDayeriWiringNetwork]    Script Date: 3/7/2015 4:45:35 PM ******/
--از این اس پی برای ایچاد گزارش در مرحله سیم بندی شبکه هوایی درخواست دایری استفاده میشود
--RequestTypeID = 1
--RequestStepID = 20
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[uspReportDayeriWiringNetwork]
(
	@RequestIds varchar(max) = null
)
AS
SET NOCOUNT ON
BEGIN
	BEGIN TRY
		
		SELECT 
			CU.CustomerID FieldID,
			CONCAT(CU.FirstNameOrTitle,' ',CU.LastName) CustomerName,
			CU.UrgentTelNo,
			CU.MobileNo,
			CAST(IR.NearestTelephon AS VARCHAR(15)) NearestTelephon,
			CAST(CI.InputNumber AS VARCHAR(15)) CabinetinputNo,
			CAST(CB.CabinetNumber AS VARCHAR(15)) CabinetNo,
			CAST(P.[Card] AS VARCHAR(15)) [Card],
			CAST(PMS.Number AS VARCHAR(15)) Shelf,
			CAST(PMR.Number AS VARCHAR(15)) Rock,
			CAST(PP.PortNumber AS VARCHAR(15)) Port,
			CAST(VMC.VerticalCloumnNo AS VARCHAR(15)) Radif,
			CAST(VMR.VerticalRowNo AS VARCHAR(15)) Tabagheh,
			CAST(BU.BuchtNo AS VARCHAR(15)) Etesali,
			CAST(CU.PersonType AS VARCHAR(15)) PersonType,
			IA.AddressContent InstallAddress,
			IA.PostalCode,
			CAST(T.TelephoneNo AS VARCHAR(15)) TelephoneNo,
			CUt.Title TelephoneType,
			cast(PC.ConnectionNo as varchar(15)) PostEtesaliNo,
			CAST(Po.Number as varchar(15)) PostNo,
			CE.CenterName,
			C.Name RegionName ,
			cast(BU.PCMPortID AS VARCHAR(15)) PCM,
			SW.FeatureONU UNO ,
			ca.AddressContent CorrespondenceAddress ,
			CAST(IR.PassTelephone AS VARCHAR(15)) OldTelephoneNo
		FROM 
			Telephone T 
		INNER JOIN 
			Request R ON T.TelephoneNo = R.TelephoneNo
		INNER JOIN 
			InstallRequest IR ON R.ID = IR.RequestID
		INNER JOIN
			Customer CU ON CU.ID = R.CustomerID
		INNER JOIN	
			CustomerType CUT ON CUT.ID = IR.TelephoneType
		INNER JOIN 
			[Address] IA ON IA.ID = T.InstallAddressID
		INNER JOIN 
			[Address] CA ON CA.ID = T.CorrespondenceAddressID
		INNER JOIN 
			SwitchPort SP ON T.SwitchPortID = SP.ID 
		INNER JOIN 
			Switch SW ON SW.ID = SP.SwitchID
		INNER JOIN 
			Bucht BU ON BU.SwitchPortID = SP.ID
		INNER JOIN 
			PostContact PC ON PC.ID = BU.ConnectionID
		INNER JOIN 
			Post PO ON PO.ID = PC.PostID
		INNER JOIN 
			CabinetInput CI ON CI.ID = BU.CabinetInputID
		INNER JOIN 
			Cabinet CB ON CB.ID = CI.CabinetID
		LEFT JOIN 
			PCMPort PP ON BU.PCMPortID = PP.ID
		LEFT JOIN 
			PCM P ON PP.PCMID = P.ID
		LEFT JOIN 
			PCMShelf PMS ON PMS.ID = P.ShelfID
		LEFT JOIN 
			PCMRock PMR ON PMR.ID = PMS.PCMRockID
		INNER JOIN 
			VerticalMDFRow VMR ON BU.MDFRowID = VMR.ID
		INNER JOIN 
			VerticalMDFColumn VMC ON VMR.VerticalMDFColumnID = VMC.ID 
		INNER JOIN 
			Center CE ON CE.ID = R.CenterID
		INNER JOIN 
			Region RE ON RE.ID = CE.RegionID
		INNER JOIN 
			City C ON C.ID = RE.CityID
		WHERE 
			(@RequestIds IS NULL OR LEN(@RequestIds) = 0 OR R.ID IN (SELECT * FROM DBO.ufnSplitList(@RequestIds)))
	END TRY
	BEGIN CATCH

		EXEC uspLogError;
		THROW;

	END CATCH
END