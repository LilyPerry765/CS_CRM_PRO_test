USE [CRM]
GO

/****** Object:  UserDefinedFunction [dbo].[serverdate]    Script Date: 5/27/2017 1:34:33 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

alter function [dbo].ufnGetServerCompleteDateTime()
returns datetime
as
begin
declare @ServerDate datetime;
select  @ServerDate  =sysdatetime() ;
RETURN @ServerDate  

end
GO


