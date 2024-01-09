ALTER TABLE CauseOfChangeNo
add IsReadOnly bit not null default (0) 
go
UPDATE CauseOfChangeNo
SET IsReadOnly = 1
WHERE ID = 6
go