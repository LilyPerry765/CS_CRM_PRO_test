SELECT        CenterID, CabinetNumber, CabinetCode, COUNT(*) AS Expr1
FROM            Cabinet
GROUP BY CenterID, CabinetNumber, CabinetCode
HAVING        (COUNT(*) > 1)