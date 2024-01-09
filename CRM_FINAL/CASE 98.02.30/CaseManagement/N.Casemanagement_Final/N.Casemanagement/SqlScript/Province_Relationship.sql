use CaseManagement
ALTER TABLE MessageLog     
ADD CONSTRAINT FK_MessageLog_ReceiverProvinceId FOREIGN KEY (ReceiverProvinceId)     
    REFERENCES Province(ID)     
    ON DELETE CASCADE    
    ON UPDATE CASCADE    
;    
GO  
-