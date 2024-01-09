--13931027 - 10:20
--سرور کرمانشاه
--در جدول خرابی ها چون رکورد های اولبه از کانورت وارد شده بود بنابراین در این جدول رکورد متناظر هر مرکزی خراب وراد نشده بود
--INSERT INTO [dbo].[Malfuction]
--           ([MalfuctionType]
--           ,[CabinetInputID]
--           ,[PostContactID]
--           ,[PCMID]
--           ,[PCMPortID]
--           ,[MalfuctionOrhealthy]
--           ,[DateMalfunction]
--           ,[TimeMalfunction]
--           ,[TypeMalfunction]
--           ,[LicenseNumber]
--           ,[LicenseFile]
--           ,[DistanceFromMDF]
--           ,[DistanceFromCabinet]
--           ,[Description])
--select 2 , ID  ,null , null ,null ,0, '1900-01-01 00:00:00' , N'00:00' , 4 , null , null ,null ,null, N'استخراج از نرم افزار قدیم'  from CabinetInput where Status = 0
--GO


--update Malfuction
--set TypeMalfunction = 4
--where 
--	CabinetInputID is not null 
--	and 
--	MalfuctionOrhealthy = 0 
--	and 
--	TypeMalfunction = 0


