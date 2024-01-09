USE [CRM]
GO

UPDATE  C
        SET [FromInputNo] = (select min(cast( minki.InputNumber as int)) from  CabinetInput as minki where minki.CabinetID = C.ID)
           ,[ToInputNo]   = (select max(cast( maxki.InputNumber as int)) from  CabinetInput as maxki where maxki.CabinetID = C.ID)
		   from [dbo].[Cabinet] as C
GO

