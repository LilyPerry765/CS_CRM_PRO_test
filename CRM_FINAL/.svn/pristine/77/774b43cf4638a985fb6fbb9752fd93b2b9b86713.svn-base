SELECT pn.PhoneNo ,  s.ID
            FROM Switch s  
			JOIN Center c ON c.ID = s.CenterID
			JOIN City ci ON c.CityID = ci.ID
			RIGHT JOIN PhoneNumber pn ON  pn.PhoneNo BETWEEN CAST(ci.Code AS varchar(3)) + s.Prefix + s.FromRange AND CAST(ci.Code AS varchar(3)) + s.Prefix + s.ToRange AND s.CenterID = pn.CenterID
			WHERE pn.CenterID = 26
			and s.ID is null



--SELECT pn.PhoneNoIndividual ,  s.ID
--            FROM Switch s  
--			JOIN Center c ON c.ID = s.CenterID
--			JOIN City ci ON c.CityID = ci.ID
--			RIGHT JOIN PhoneNumber pn ON  pn.PhoneNoIndividual BETWEEN s.Prefix + s.FromRange AND s.Prefix + s.ToRange AND s.CenterID = pn.CenterID
--			WHERE pn.CenterID = 26
--			and s.ID is null

----2314428897
--SELECT * FROM switch s
--JOIN Center c ON c.ID = s.CenterID
--			JOIN City ci ON c.CityID = ci.ID
-- WHERE s.Prefix = '442'


-- 231 442 8897