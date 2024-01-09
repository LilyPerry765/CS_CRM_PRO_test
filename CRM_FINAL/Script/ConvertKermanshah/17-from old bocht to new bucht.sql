
update B set B.SwitchPortID = T3.SwitchPortID , b.Status = T3.Status  from CRM.dbo.Bucht as B 
join (
select T.ID , t.SwitchPortID , t.ConnectionID , t.Status  from 
(
SELECT     Bucht.*
FROM         [befor fix pcm].dbo.MDF INNER JOIN
             [befor fix pcm].dbo.MDFFrame ON MDF.ID = MDFFrame.MDFID INNER JOIN
             [befor fix pcm].dbo.VerticalMDFColumn ON MDFFrame.ID = VerticalMDFColumn.MDFFrameID INNER JOIN
             [befor fix pcm].dbo.VerticalMDFRow ON VerticalMDFColumn.ID = VerticalMDFRow.VerticalMDFColumnID INNER JOIN
             [befor fix pcm].dbo.Bucht ON VerticalMDFRow.ID = Bucht.MDFRowID
where MDF.CenterID = 4 and BuchtTypeID = 8  and SwitchPortID is not null
) as T join (
SELECT     Bucht.*
FROM         CRM.dbo.MDF INNER JOIN
             CRM.dbo.MDFFrame ON MDF.ID = MDFFrame.MDFID INNER JOIN
             CRM.dbo.VerticalMDFColumn ON MDFFrame.ID = VerticalMDFColumn.MDFFrameID INNER JOIN
             CRM.dbo.VerticalMDFRow ON VerticalMDFColumn.ID = VerticalMDFRow.VerticalMDFColumnID INNER JOIN
             CRM.dbo.Bucht ON VerticalMDFRow.ID = Bucht.MDFRowID
where MDF.CenterID = 4 and BuchtTypeID = 8 ) as T2 on T.ID = T2.ID
) as T3 on B.ID = T3.ID

