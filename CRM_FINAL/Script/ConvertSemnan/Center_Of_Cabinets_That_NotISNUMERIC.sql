USE [CRM]
GO

SELECT [ID]
      ,[RegionID]
      ,[CenterCode]
      ,[CenterName]
      ,[Telephone]
      ,[Address]
      ,[IsActive]
      ,[IDInFolder]
      ,[InsertDate]
      ,[ModifyDate]
  FROM [dbo].[Center]

  WHERE ID IN (

SELECT T.CenterID
FROM
(
SELECT         ID, CenterID, CabinetNumber, ABType, CabinetCode, CabinetTypeID, CabinetUsageType, FromInputNo, ToInputNo, DistanceFromCenter, IsOutBound, 
                         OutBoundMeter, Address, PostCode, FromPostalCode, ToPostalCode, Status, Capacity
FROM            Cabinet
--WHERE        (ISNUMERIC(CabinetNumber) <> 1)
WHERE ID IN (100281,105642,105861)
) T
)