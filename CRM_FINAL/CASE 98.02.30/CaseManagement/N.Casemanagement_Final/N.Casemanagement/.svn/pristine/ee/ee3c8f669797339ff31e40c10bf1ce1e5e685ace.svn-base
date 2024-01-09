use CaseManagement
update ML
set ML.IsActivityRequestUpdated = 1
From MessageLog ML
LEFT JOIN ActivityRequest AR ON AR.ID = ML.ActivityRequestID
where (ML.IsActivityRequestUpdated is NULL) and (ML.InsertDate < AR.ModifiedDate) 
