--update RR
--SET Template = RL.Template,
--	[TimeStamp] = RL.[TimeStamp]
--from 
--	[78.39.252.109].[CRM].[dbo].ReportTemplate RR
--inner join	
--	ReportTemplate rl ON RL.ID = RR.ID
--where 
--	rl.ID = 138 AND RR.ID = 138


select * from [78.39.252.109].[CRM].[dbo].ReportTemplate where id = 138