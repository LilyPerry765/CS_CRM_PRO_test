USE [CRM]
GO

UPDATE PCM 
   SET   [Card] = CONVERT (varchar ,(SELECT REPLACE ((SELECT REPLACE ( (SELECT REPLACE(UPPER([Card])  , 'a' ,'')) ,'b','')),'0َ','')))
  WHERE ISNUMERIC([Card]) <> 1
GO


