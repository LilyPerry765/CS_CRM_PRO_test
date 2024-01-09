
-- excute line by line


insert into [dbo].[SwitchOffice] select RequestID , SwitchFileID , '7/14/2015' from e1 where SwitchFileID != null

ALTER TABLE dbo.E1 DROP CONSTRAINT FK_E1_DocumentsFile5; 
alter table dbo.E1 drop column [SwitchFileID]

--------------------------------------------------------------

insert into [dbo].TransferDepartmentOffice select RequestID , TransferDepartmentFileID , '7/14/2015' from e1 where TransferDepartmentFileID != null
--ALTER TABLE dbo.E1 DROP CONSTRAINT FK_E1_DocumentsFile2; 
alter table dbo.E1 drop column TransferDepartmentFileID

-----------------------------------------------------------------

insert into [dbo].CableDesignOffice select RequestID , CommandCircuitFileID , '7/14/2015' from e1 where CommandCircuitFileID != null
insert into [dbo].CableDesignOffice select RequestID , CableDesignFileID , '7/14/2015' from e1 where CableDesignFileID != null
--ALTER TABLE dbo.E1 DROP CONSTRAINT FK_E1_DocumentsFile2; 
alter table dbo.E1 drop column CommandCircuitFileID

ALTER TABLE dbo.E1 DROP CONSTRAINT FK_E1_DocumentsFile4; 
alter table dbo.E1 drop column CableDesignFileID

------------------------------------------------------------


insert into [dbo].PowerOffice select RequestID , PowerFileID , '7/14/2015' from e1 where PowerFileID != null
ALTER TABLE dbo.E1 DROP CONSTRAINT FK_E1_DocumentsFile3; 
alter table dbo.E1 drop column PowerFileID

ALTER TABLE dbo.E1 DROP CONSTRAINT FK_E1_DocumentsFile1; 
ALTER TABLE dbo.E1 DROP CONSTRAINT FK_E1_DocumentsFile; 
alter table dbo.E1 drop column DesignDirectorFileID


