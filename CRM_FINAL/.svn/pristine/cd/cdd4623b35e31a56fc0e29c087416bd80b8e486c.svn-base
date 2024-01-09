USE [CRM]
GO

UPDATE [dbo].[Bucht]
   SET [BuchtIDConnectedOtherBucht] =(SELECT ID FROM Bucht WHERE ElkaID = kpb.[BOOKHT_ID])
  FROM [ORACLECRM]..[TT].[PCM] AS p 
   JOIN [ORACLECRM]..[TT].[PCM_CON] AS pc ON p.[PCM_ID] = pc.[PCM_ID]  
   JOIN [ORACLECRM]..[TT].[BASE_BOOKHT] AS b ON pc.[BOOKHT_ID] = b.[BOOKHT_ID]
   JOIN [ORACLECRM]..[TT].[BOOKHT_TYPE] AS bt ON bt.[BOOKHT_TYPE_ID] = b.[BOOKHT_TYPE]
   JOIN [ORACLECRM]..[TT].[KAFU_PCM] AS kp ON kp.[PORT_ID] =  p.[PCM_ID]
   JOIN [ORACLECRM]..[TT].[BASE_BOOKHT] AS kpb ON kpb.[BOOKHT_ID] = kp.[BOOKHT_ID]
WHERE ElkaID = b.[BOOKHT_ID]


UPDATE [dbo].[Bucht]
   SET [BuchtIDConnectedOtherBucht] =(SELECT ID FROM Bucht WHERE ElkaID = b.[BOOKHT_ID]) 
      -- ,CabinetInputID = (select ID from CabinetInput as CI where  CI.ElkaID =  ElkaID)
      ,[Status] = 13
  FROM [ORACLECRM]..[TT].[PCM] AS p 
   JOIN [ORACLECRM]..[TT].[PCM_CON] AS pc ON p.[PCM_ID] = pc.[PCM_ID]  
   JOIN [ORACLECRM]..[TT].[BASE_BOOKHT] AS b ON pc.[BOOKHT_ID] = b.[BOOKHT_ID]
   JOIN [ORACLECRM]..[TT].[BOOKHT_TYPE] AS bt ON bt.[BOOKHT_TYPE_ID] = b.[BOOKHT_TYPE]
   JOIN [ORACLECRM]..[TT].[KAFU_PCM] AS kp ON kp.[PORT_ID] =  p.[PCM_ID]
    JOIN [ORACLECRM]..[TT].[BASE_BOOKHT] AS kpb ON kpb.[BOOKHT_ID] = kp.[BOOKHT_ID]
WHERE ElkaID =kpb.[BOOKHT_ID]

UPDATE Bucht SET CabinetInputID =C.CabinetInput
FROM Bucht as BaseBucht INNER Join 
(select ID , T.CabinetInput from(
(
SELECT dbo.PCM.ID AS PCMID, dbo.Bucht.*
FROM            dbo.PCMPort INNER JOIN
                         dbo.PCM ON dbo.PCMPort.PCMID = dbo.PCM.ID INNER JOIN
                         dbo.Bucht ON dbo.PCMPort.ID = dbo.Bucht.PCMPortID INNER JOIN
                         dbo.PCMRock INNER JOIN
                         dbo.PCMShelf ON dbo.PCMRock.ID = dbo.PCMShelf.PCMRockID ON dbo.PCM.ShelfID = dbo.PCMShelf.ID 
--WHERE        (dbo.PCMRock.CenterID = 1) AND (dbo.PCMRock.Number = 1) AND (dbo.PCMShelf.Number = 3) AND (dbo.PCM.Card = 9)
) B
JOIN
(
SELECT dbo.PCMPort.PCMID AS PCMID, Bucht_1.CabinetInputID AS  CabinetInput
FROM            dbo.PCMPort INNER JOIN
                         dbo.PCM ON dbo.PCMPort.PCMID = dbo.PCM.ID INNER JOIN
                         dbo.Bucht ON dbo.PCMPort.ID = dbo.Bucht.PCMPortID INNER JOIN
                         dbo.PCMRock INNER JOIN
                         dbo.PCMShelf ON dbo.PCMRock.ID = dbo.PCMShelf.PCMRockID ON dbo.PCM.ShelfID = dbo.PCMShelf.ID  JOIN
                         dbo.Bucht AS Bucht_1 ON dbo.Bucht.BuchtIDConnectedOtherBucht = Bucht_1.ID
--WHERE        (dbo.PCMRock.CenterID = 1) AND (dbo.PCMRock.Number = 1) AND (dbo.PCMShelf.Number = 3) AND (dbo.PCM.Card = 9)
) T ON B.PCMID = T.PCMID)) C ON BaseBucht.ID = C.ID



