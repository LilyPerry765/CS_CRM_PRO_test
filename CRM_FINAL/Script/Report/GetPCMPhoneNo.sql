
/****** Object:  UserDefinedFunction [dbo].[GetPCMPhoneNo]    Script Date: 2/18/2015 11:43:51 AM ******/
DROP FUNCTION [dbo].[GetPCMPhoneNo]
GO

/****** Object:  UserDefinedFunction [dbo].[GetPCMPhoneNo]    Script Date: 2/18/2015 11:43:51 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[GetPCMPhoneNo]
    (
		@CabinetInputID		BIGINT
    )
RETURNS nvarchar(max)
AS 
    BEGIN
	Declare @PhoneNo Nvarchar(max)
;WITH HEADBUCHT AS 
(
	
	SELECT SwitchPortID, ROW_NUMBER() OVER (PARTITION BY CABINETINPUTID ORDER BY BUCHTNO) RN
	FROM BUCHT 
	WHERE CABINETINPUTID = @CabinetInputID
	AND [STATUS] != 13
	AND SWITCHPORTID IS NOT NULL
)
SELECT @PhoneNo = TELEPHONE.TELEPHONENO
FROM HEADBUCHT
LEFT JOIN SWITCHPORT SP ON SP.ID = HEADBUCHT.SwitchPortID
LEFT JOIN TELEPHONE ON TELEPHONE.SwitchPortID = SP.ID
WHERE HEADBUCHT.RN = 1
			
RETURN @PhoneNo
            
    END


GO


