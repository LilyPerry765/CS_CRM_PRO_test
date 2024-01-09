
		SELECT 
			R.ID RequestId,
			ISNULL(CI.Name,'') CityName,
			C.CenterName CenterName,
			R.RequestDate,
			ISNULL(TC.FromTelephoneNo,'') OldTelephone,
			ISNULL(TC.ToTelephoneNo,'') NewTelephone,
			CU.FirstNameOrTitle,
			ISNULL(CU.LastName,'') LastName,
			ISNULL(AA.AddressContent,'') InstallAddress,
			ISNULL(A.AddressContent,'') CorrespondenceAddress,
			OCAB.CabinetNumber OldCabinetNumber, --شماره کافو قدیم
			OCI.InputNumber OldInputNumber, --ورودی یا مرکزی قدیم
			OP.Number OldPostNumber,--شماره پست قدیم
			OPC.ConnectionNo OldConnectionNo, --اتصالی پست قدیم
			NCAB.CabinetNumber NewCabinetNumber, --شماره کافو جدید
			NCI.InputNumber NewInputNumber, --ورودی یا مرکزی جدید
			NP.Number ,--شماره پست جدید
			NPC.ConnectionNo NewConnectionNo, --اتصالی پست جدید
			OMDF.Number OldMdfNumber, --ام دی اف قدیم
			OVMC.VerticalCloumnNo OldVerticalColumnNo, --ردیف قدیم
			OVMR.VerticalRowNo OldVertcalRowNo, --طبقه قدیم
			OB.BuchtNo OldBuchtNo, --اتصالی  بوخت قدیم 
			NMDF.Number NewMdfNumber, --ام دی اف جدید
			NVMC.VerticalCloumnNo NewVerticalCloumnNo, --ردیف جدید
			NVMR.VerticalRowNo NewVerticalRowNo, --طبقه جدید
			NB.BuchtNo NewBuchtNo --اتصالی بوخت جدید 
		FROM 
			Request R
		INNER JOIN 
			TranslationOpticalCabinetToNormal TN ON TN.ID = R.ID
		INNER JOIN 
			TranslationOpticalCabinetToNormalConncetions TC ON TC.RequestID = TN.ID
		INNER JOIN 
			Post OP ON OP.ID = TC.FromPostID --پست قدیم
		INNER JOIN 
			Post NP ON NP.ID = TC.ToPostID --پست جدید
		INNER JOIN 
			Cabinet OCAB ON OCAB.ID = TN.OldCabinetID --کافو قدیم
		INNER JOIN 
			Cabinet NCAB ON NCAB.ID = TN.NewCabinetID --کافو جدید
		INNER JOIN 
			CabinetInput NCI ON NCI.ID = TC.ToCabinetInputID --مرکزی جدید
		INNER JOIN
			CabinetInput OCI ON OCI.ID = TC.FromCabinetInputID --مرکزی قدیم    ??????
		INNER JOIN 
			PostContact OPC ON OPC.ID = TC.FromPostContactID --اتصالی قدیم
		INNER JOIN 
			PostContact NPC ON NPC.ID = TC.ToPostConntactID --اتصالی جدید
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
			Bucht OB ON OB.ID = TC.FromBucht --بوخت قدیم     ???????
		LEFT JOIN 
			Bucht NB ON NB.ID = TC.ToBucht --بوخت جدید       ???????
		INNER JOIN 
			VerticalMDFRow OVMR ON OB.MDFRowID = OVMR.ID --طبقه قدیم
		INNER JOIN 
			VerticalMDFRow NVMR ON NB.MDFRowID = NVMR.ID --طبقه جدید
		INNER JOIN 
			VerticalMDFColumn OVMC ON OVMR.VerticalMDFColumnID = OVMC.ID  --ردیف قدیم
		INNER JOIN 
			VerticalMDFColumn NVMC ON NVMR.VerticalMDFColumnID = NVMC.ID --ردیف جدید
		INNER JOIN
			MDFFrame OMF ON OVMC.MDFFrameID = OMF.ID --فریم قدیم
		INNER JOIN 
			MDFFrame NMF ON NVMC.MDFFrameID = NMF.ID --فریم جدید
		INNER JOIN
			MDF OMDF ON OMF.MDFID = OMDF.ID --ام دی اف قدیم
		INNER JOIN 
			MDF NMDF ON NMF.MDFID = NMDF.ID -- ام دی اف  جدید
		WHERE
			R.RequestTypeID = 99 --درخواست برگردان کافو نوری به کافو معمولی
		