USE [CRM]
GO

UPDATE [dbo].[Post]
   SET [Number] = LEFT(Number , LEN(Number)-1)

WHERE   (ISNUMERIC(Number) <> 1)
GO



