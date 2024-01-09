ALTER TABLE TranslationOpticalCabinetToNormal
add OldCabinetUsageTypeID int,
	NewCabinetUsageTypeID int

GO
exec sp_addextendedproperty N'MS_Description',N'نوع کافو استفاده شده - قدیم','schema',dbo,'table',TranslationOpticalCabinetToNormal,'column',OldCabinetUsageTypeID
exec sp_addextendedproperty N'MS_Description',N'نوع کافو استفاده شده - جدید','schema',dbo,'table',TranslationOpticalCabinetToNormal,'column',NewCabinetUsageTypeID

GO
SELECT 
	TOCN.ID,
	TOCN.NewCabinetID,
	NCB.CabinetUsageType NewUsage,
	NCB.CabinetNumber NewNumber,
	TOCN.NewCabinetUsageTypeID,
	TOCN.OldCabinetID,
	OCB.CabinetUsageType OldUsage,
	OCB.CabinetNumber OldNumber,
	TOCN.OldCabinetUsageTypeID
FROM 
	TranslationOpticalCabinetToNormal TOCN
INNER JOIN 
	Cabinet OCB ON OCB.ID = TOCN.OldCabinetID
INNER JOIN 
	Cabinet NCB ON NCB.ID = TOCN.NewCabinetID

GO
ALTER TABLE TranslationOpticalCabinetToNormal
add constraint FK_TranslationOpticalCabinetToNormal_CabinetUsageType_OldCabinetUsageTypeID foreign key (OldCabinetUsageTypeID) references CabinetUsageType (ID)
GO
ALTER TABLE TranslationOpticalCabinetToNormal
add constraint FK_TranslationOpticalCabinetToNormal_CabinetUsageType_NewCabinetUsageTypeID foreign key (NewCabinetUsageTypeID) references CabinetUsageType (ID)
GO
UPDATE TOCN
SET NewCabinetUsageTypeID = NCB.CabinetUsageType,
	OldCabinetUsageTypeID = OCB.CabinetUsageType
FROM 
	TranslationOpticalCabinetToNormal TOCN
INNER JOIN 
	Cabinet OCB ON OCB.ID = TOCN.OldCabinetID
INNER JOIN 
	Cabinet NCB ON NCB.ID = TOCN.NewCabinetID

GO

ALTER TABLE TranslationOpticalCabinetToNormal
ALTER COLUMN OldCabinetUsageTypeID INT NOT NULL
GO
ALTER TABLE TranslationOpticalCabinetToNormal
ALTER COLUMN NewCabinetUsageTypeID INT NOT NULL
GO