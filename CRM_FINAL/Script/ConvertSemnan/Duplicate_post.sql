SELECT        CabinetID, Number, COUNT(*) AS Expr1
FROM            Post
GROUP BY CabinetID, Number , AorBType
HAVING        (COUNT(*) > 1)