

/****** Object:  UserDefinedFunction [dbo].[GetBuchtInfo]    Script Date: 1/28/2015 3:23:06 PM ******/
DROP FUNCTION [dbo].[GetBuchtInfo]
GO

/****** Object:  UserDefinedFunction [dbo].[GetBuchtInfo]    Script Date: 1/28/2015 3:23:06 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[GetBuchtInfo]
    (
	@BuchtID		BIGINT
    )
RETURNS nvarchar(max)
AS 
    BEGIN
	Declare @BuchtInfo Nvarchar(max)
select @BuchtInfo = Concat( N'ام دی اف:' , MDF.Number, N'ردیف:', VerticalMDFColumn.VerticalCloumnNo , N'طبقه:' , VerticalMDFRow.VerticalRowNo ,N'اتصالی:', Bucht.BuchtNo)
	
        	
From BUCHT
	
JOIN VerticalMDFRow on Bucht.MDFRowID = VerticalMDFRow.ID 
JOIN VerticalMDFColumn on VerticalMDFRow.VerticalMDFColumnID = VerticalMDFColumn .ID
JOIN MDFFrame on MDFFrame.ID = VerticalMDFColumn.MDFFrameID
JOIN MDF on MDF.ID = MDFFrame.MDFID
where Bucht.ID = @BuchtID
			
RETURN @BuchtInfo
            
    END


GO


