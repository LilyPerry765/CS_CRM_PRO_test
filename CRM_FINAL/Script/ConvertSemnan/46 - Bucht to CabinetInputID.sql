use crm
go


update CB set CabinetInputID = CI.ID , CablePairID = CP.ID
from [ORACLECRM]..[TT].[BASE_BOOKHT] as BB join [ORACLECRM]..[TT].[KAFU_INPUT] as KI on BB.[BOOKHT_ID] = KI.[INPUT_ID]
join CRM.dbo.CabinetInput as CI on CI.ElkaID = KI.[INPUT_ID]
join CRM.dbo.CablePair as CP on CP.CabinetInputID = CI.ID
join CRM.dbo.Bucht as CB on CB.ElkaID = BB.[BOOKHT_ID]

