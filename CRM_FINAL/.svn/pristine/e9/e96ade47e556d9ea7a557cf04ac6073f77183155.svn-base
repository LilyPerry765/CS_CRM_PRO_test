

/****** Object:  UserDefinedFunction [dbo].[GetPCMInfo]    Script Date: 1/28/2015 3:22:32 PM ******/
DROP FUNCTION [dbo].[GetPCMInfo]
GO

/****** Object:  UserDefinedFunction [dbo].[GetPCMInfo]    Script Date: 1/28/2015 3:22:32 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[GetPCMInfo]
    (
	@PCMPortID		BIGINT
    )
RETURNS nvarchar(max)
AS 
    BEGIN
	Declare @PCMInfo Nvarchar(max)
	,@IsExistPcm bit = CASE WHEN (Select ID from PCMPort where PCMPort.ID = @PCMPortID) IS NOT NULL THEN 1 ELSE 0 END
select @PCMInfo = (CASE WHEN @IsExistPcm = 1 THEN
concat(N'رک:',PCMRock.Number,N'  شلف : ', PCMShelf.Number,N'   کارت : ' ,PCM.Card ,N'   پورت : ' ,PCMPort.PortNumber)
ELSE ''
END )
        	
From  PCMPort 
Left JOIN PCM on PCM.ID = PCMPort.PCMID 
Left JOIN PCMShelf on PCM.ShelfID = PCMShelf.ID
Left JOIN pcmrock on PCMRock.ID = PCMShelf.PCMRockID

where PCMPort.ID = @PCMPortID
Return @PCMInfo            
 END


GO


