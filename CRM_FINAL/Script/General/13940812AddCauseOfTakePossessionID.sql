Use CRM
alter table Telephone add CauseOfTakePossessionID int null
GO


ALTER TABLE [dbo].[Telephone]  WITH CHECK ADD  CONSTRAINT [FK_Telephone_CauseOfTakePossession] FOREIGN KEY([CauseOfTakePossessionID])
REFERENCES [dbo].[CauseOfTakePossession] ([ID])
GO

ALTER TABLE [dbo].[Telephone] CHECK CONSTRAINT [FK_Telephone_CauseOfTakePossession]
GO


update Telephone set Telephone.CauseOfTakePossessionID = TakePossession.CauseOfTakePossessionID  from TakePossession join Request on Request.ID = TakePossession.ID 
join Telephone on Telephone.TelephoneNo = Request.TelephoneNo
where Telephone.Status = 5
