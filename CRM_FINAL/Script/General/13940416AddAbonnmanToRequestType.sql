
alter table RequestType add Abonman bit null
update RequestType set Abonman = 0
alter table RequestType alter column Abonman bit not null
