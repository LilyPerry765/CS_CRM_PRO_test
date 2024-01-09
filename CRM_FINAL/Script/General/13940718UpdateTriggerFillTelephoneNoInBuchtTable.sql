USE [CRM]
GO
/****** Object:  Trigger [dbo].[TriggerFillTelephoneNo]    Script Date: 10/10/2015 11:38:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER TRIGGER [dbo].[TriggerFillTelephoneNo] ON  [dbo].[Bucht]  AFTER  UPDATE
AS 
BEGIN
	SET NOCOUNT ON;

if exists(SELECT * FROM inserted where inserted.SwitchPortID is not null)
     begin
     update Bucht set TelephoneNo = Telephone.TelephoneNo
     FROM  Bucht join  inserted on Bucht.ID = inserted.ID JOIN Telephone ON Bucht.SwitchPortID = Telephone.SwitchPortID 
     end
else
begin
     update Bucht set TelephoneNo = null
     FROM  Bucht join  inserted on Bucht.ID = inserted.ID JOIN Telephone ON Bucht.SwitchPortID = Telephone.SwitchPortID 
end


if exists(SELECT * FROM inserted where inserted.BuchtTypeID in (8,9))
     begin
     update B set  B.PCMMainBuchtID = (select top 1 ID from Bucht where CabinetInputID = isd.CabinetInputID and Bucht.BuchtTypeID = 13 )
     FROM  Bucht as B join inserted as isd on isd.ID = b.ID
     end
	 else
	 begin
	 update B set  B.PCMMainBuchtID = null
     FROM  Bucht as B join inserted as isd on isd.ID = b.ID
	 end
END
