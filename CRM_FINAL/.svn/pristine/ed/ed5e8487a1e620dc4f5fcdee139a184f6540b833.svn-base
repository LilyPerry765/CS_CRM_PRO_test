SELECT        CenterID, SwitchCode, COUNT(*) AS Expr1
FROM            Switch
GROUP BY CenterID, SwitchCode , SwitchTypeID
HAVING        (COUNT(*) > 1)