CREATE PROCEDURE uspReportDischargeWiringNetwork
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
			CAST(CI.InputNumber AS VARCHAR(MAX)) CabinetinputNo,
			CAST(CB.CabinetNumber AS VARCHAR(MAX)) CabinetNo,
			CAST(PM.[Card] AS VARCHAR(MAX)) [Card],
			CAST(PMS.Number AS VARCHAR(MAX)) Shelf,
			CAST(PMR.Number AS VARCHAR(MAX)) Rock,
			CAST(PP.PortNumber AS VARCHAR(MAX)) Port,
			CAST(VMC.VerticalCloumnNo AS VARCHAR(MAX)) Radif,
			CAST(VMR.VerticalRowNo AS VARCHAR(MAX)) Tabagheh,
			CAST(BU.BuchtNo AS VARCHAR(MAX)) Etesali,
			CAST(CU.PersonType AS VARCHAR(MAX)) PersonType,
			IA.AddressContent InstallAddress,
			IA.PostalCode,
			CAST(T.TelephoneNo AS VARCHAR(MAX)) TelephoneNo,
			CAST(PC.ConnectionNo AS VARCHAR(MAX)) PostEtesaliNo,
			CAST(PO.Number AS VARCHAR(MAX)) PostNo,
			CE.CenterName,
			CIT.NAME RegionName,
			CAST(BU.PCMPortID AS VARCHAR(MAX)) PCM,
			SW.FeatureONU UNO,
			CA.AddressContent CorrespondenceAddress 
		FROM 
			Telephone T 
		INNER JOIN 
			Request R ON T.TelephoneNo = R.TelephoneNo
		INNER JOIN 
			TakePossession TP ON TP.ID = R.ID
		INNER JOIN 
			Customer CU ON CU.ID = R.CustomerID
		INNER JOIN 
			[Address] IA ON IA.ID = T.InstallAddressID
		INNER JOIN 
			[Address] CA ON CA.ID = T.CorrespondenceAddressID
		INNER JOIN 
			SwitchPort SP ON SP.ID = TP.SwitchPortID
		INNER JOIN 
			Switch SW ON SW.ID = SP.SwitchID
		INNER JOIN 
			Bucht BU ON BU.ID = TP.BuchtID
		INNER JOIN 
			PostContact PC ON PC.ID = BU.ConnectionID
		INNER JOIN 
			Post PO ON PO.ID = PC.PostID
		INNER JOIN 
			CabinetInput CI ON CI.ID = TP.CabinetInputID
		INNER JOIN 
			Cabinet CB ON CB.ID = CI.CabinetID
		LEFT JOIN 
			PCMPort PP ON PP.ID = BU.PCMPortID
		LEFT JOIN 
			PCM PM ON PM.ID = PP.PCMID
		LEFT JOIN 
			PCMShelf PMS ON PMS.ID = PM.ShelfID
		LEFT JOIN 
			PCMRock PMR ON PMR.ID = PMS.PCMRockID
		INNER JOIN 
			VerticalMDFRow VMR ON VMR.ID = BU.MDFRowID
		INNER JOIN 
			VerticalMDFColumn VMC ON VMC.ID = VMR.VerticalMDFColumnID
		INNER JOIN 
			Center CE ON CE.ID = R.CenterID
		INNER JOIN 
			Region RE ON RE.ID = CE.RegionID
		INNER JOIN 
			City CIT ON CIT.ID = RE.CityID
		WHERE 
			(@RequestIds IS NULL OR LEN(@RequestIds) = 0 OR R.ID IN (SELECT * FROM DBO.ufnSplitList(@RequestIds)))

	END TRY
	BEGIN CATCH

		EXEC uspLogError;
		THROW;

	END CATCH
END