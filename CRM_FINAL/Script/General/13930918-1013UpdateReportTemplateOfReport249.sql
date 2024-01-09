update RR
SET Template = RL.Template
from 
	ReportTemplate RL
INNER JOIN 
	[78.39.252.109].[CRM].[dbo].[ReportTemplate] RR ON RL.ID = RR.ID
where 
	RL.ID = 249 AND RR.ID = 249

