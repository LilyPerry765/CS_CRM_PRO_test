--13951113 - 1409
use CRM
alter table Customer
add AgentID int null
go
exec sp_addextendedproperty N'MS_Description',N'شناسه نماینده','schema',dbo,'table',Customer,'column',AgentID

go

alter table Customer
add constraint FK_Customer_Agent_AgentID foreign key (AgentID) references Agent(ID)

go