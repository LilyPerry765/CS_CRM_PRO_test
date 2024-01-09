USE [CRM]
GO
/****** Object:  StoredProcedure [dbo].[uspReportTranslationOpticalCabinetToNormalRequest]    Script Date: 7/6/2015 1:35:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[uspReportTranslationOpticalCabinetToNormalRequest]
(
	@CitiesID varchar(max) = null,
	@CentersId varchar(max) = null,
	@fromDate datetime = null,
	@toDate datetime = null,
	@RequestId bigint = null,
	@TelephoneNo bigint = null
)
AS
SET NOCOUNT ON
BEGIN
	BEGIN TRY
		--گزارش درخواست هاي برگردان کافو نوري به کافو معمولي
		
		SELECT 
			R.ID RequestId,
			ISNULL(CI.Name,'') CityName,
			C.CenterName CenterName,
			R.RequestDate,
			ISNULL(TC.FromTelephoneNo,'') OldTelephone,
			ISNULL(TC.ToTelephoneNo,'') NewTelephone,
			CU.FirstNameOrTitle,
			ISNULL(CU.LastName,'') LastName,
			ISNULL(CU.NationalCodeOrRecordNo,'') NationalCode,
			ISNULL(AA.AddressContent,'') InstallAddress,
			ISNULL(AA.PostalCode,'') InstallPostalCode,
			ISNULL(A.PostalCode,'') CorrespondencePostalCode,
			ISNULL(A.AddressContent,'') CorrespondenceAddress,
			OCAB.CabinetNumber OldCabinetNumber, --شماره کافو قديم
			OCI.InputNumber OldInputNumber, --ورودي يا مرکزي قديم
			OP.Number OldPostNumber,--شماره پست قديم
			OPC.ConnectionNo OldConnectionNo, --اتصالي پست قديم
			NCAB.CabinetNumber NewCabinetNumber, --شماره کافو جديد
			NCI.InputNumber NewInputNumber, --ورودي يا مرکزي جديد
			NP.Number NewPostNumber ,--شماره پست جديد
			NPC.ConnectionNo NewConnectionNo, --اتصالي پست جديد
			OMDF.Number OldMdfNumber, --ام دي اف قديم
			OVMC.VerticalCloumnNo OldVerticalColumnNo, --رديف قديم
			OVMR.VerticalRowNo OldVerticalRowNo, --طبقه قديم
			OB.BuchtNo OldBuchtNo, --اتصالي  بوخت قديم 
			NMDF.Number NewMdfNumber, --ام دي اف جديد
			NVMC.VerticalCloumnNo NewVerticalCloumnNo, --رديف جديد
			NVMR.VerticalRowNo NewVerticalRowNo, --طبقه جديد
			NB.BuchtNo NewBuchtNo --اتصالي بوخت جديد 
		FROM 
			Request R
		INNER JOIN 
			TranslationOpticalCabinetToNormal TN ON TN.ID = R.ID
		INNER JOIN 
			TranslationOpticalCabinetToNormalConncetions TC ON TC.RequestID = TN.ID
		INNER JOIN 
			Post OP ON OP.ID = TC.FromPostID --پست قديم
		INNER JOIN 
			Post NP ON NP.ID = TC.ToPostID --پست جديد
		INNER JOIN 
			Cabinet OCAB ON OCAB.ID = TN.OldCabinetID --کافو قديم
		INNER JOIN 
			Cabinet NCAB ON NCAB.ID = TN.NewCabinetID --کافو جديد
		INNER JOIN 
			CabinetInput NCI ON NCI.ID = TC.ToCabinetInputID --مرکزي جديد
		INNER JOIN
			CabinetInput OCI ON OCI.ID = TC.FromCabinetInputID --مرکزي قديم    ??????
		INNER JOIN 
			PostContact OPC ON OPC.ID = TC.FromPostContactID --اتصالي قديم
		INNER JOIN 
			PostContact NPC ON NPC.ID = TC.ToPostConntactID --اتصالي جديد
		INNER JOIN 
			Center C ON C.ID = R.CenterID
		INNER JOIN
			Region REG ON REG.ID = C.RegionID
		INNER JOIN 
			City CI ON CI.ID = REG.CityID
		LEFT JOIN 
			Customer CU ON CU.ID = TC.CustomerID --مشترک     ????????
		LEFT JOIN 
			[Address] A ON A.ID = TC.InstallAddressID    --?????????
		LEFT JOIN 
			[Address] AA ON AA.ID = TC.CorrespondenceAddressID  --???????
		LEFT JOIN 
			Bucht OB ON OB.ID = TC.FromBucht --بوخت قديم     ???????
		LEFT JOIN 
			Bucht NB ON NB.ID = TC.ToBucht --بوخت جديد       ???????
		INNER JOIN 
			VerticalMDFRow OVMR ON OB.MDFRowID = OVMR.ID --طبقه قديم
		INNER JOIN 
			VerticalMDFRow NVMR ON NB.MDFRowID = NVMR.ID --طبقه جديد
		INNER JOIN 
			VerticalMDFColumn OVMC ON OVMR.VerticalMDFColumnID = OVMC.ID  --رديف قديم
		INNER JOIN 
			VerticalMDFColumn NVMC ON NVMR.VerticalMDFColumnID = NVMC.ID --رديف جديد
		INNER JOIN
			MDFFrame OMF ON OVMC.MDFFrameID = OMF.ID --فريم قديم
		INNER JOIN 
			MDFFrame NMF ON NVMC.MDFFrameID = NMF.ID --فريم جديد
		INNER JOIN
			MDF OMDF ON OMF.MDFID = OMDF.ID --ام دي اف قديم
		INNER JOIN 
			MDF NMDF ON NMF.MDFID = NMDF.ID -- ام دي اف  جديد
		WHERE
			R.RequestTypeID = 99 --درخواست برگردان کافو نوري به کافو معمولي
			AND
			(@CitiesID IS NULL OR LEN(@CitiesID) = 0 OR CI.ID IN (SELECT * FROM DBO.ufnSplitList(@CitiesID)))
			AND
			(@CentersId IS NULL OR LEN(@CentersId) = 0 OR R.CenterID IN (SELECT * FROM DBO.ufnSplitList(@CentersId)))
			AND
			(@fromDate IS NULL OR @fromDate <= R.RequestDate)
			AND
			(@toDate IS NULL OR @toDate >= R.RequestDate)
			AND
			(@RequestId IS NULL OR R.ID = @RequestId) 
			AND 
			(@TelephoneNo IS NULL OR R.TelephoneNo = @TelephoneNo)
		ORDER BY 
			R.ID
		

	END TRY
	BEGIN CATCH
		EXEC  [dbo].[uspLogError] ;
		THROW
	END CATCH
END
