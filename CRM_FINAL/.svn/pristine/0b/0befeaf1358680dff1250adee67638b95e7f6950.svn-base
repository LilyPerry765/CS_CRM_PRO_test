--1393/09/13
--توضیحات : برای جدا سازی لیستی از شناسه ها

--نحوه استفاده
--DECLARE @posts varchar(5) = '25,4'
--SELECT * FROM ufnSplitList(@posts)

IF OBJECT_ID('ufnSplitList') IS NOT NULL 
	DROP FUNCTION ufnSplitList
GO

CREATE FUNCTION ufnSplitList
(
	@List varchar(max)
)
RETURNS @Result table(ID bigint)
AS
BEGIN
	DECLARE @ID varchar(12),
			@Position int

	SET @List = CONCAT(LTRIM(RTRIM(@List)),',')
	SET @Position = CHARINDEX(',',@List,1)

	IF REPLACE(@List,',','') <> ''
		BEGIN
			WHILE @Position > 0
				BEGIN 
					SET @ID = LTRIM(RTRIM(LEFT(@List,@Position - 1)))
					IF @ID <> ''
						BEGIN

							INSERT INTO @Result (ID) VALUES (CONVERT(BIGINT,@ID));

						END
					SET @List = RIGHT(@List,LEN(@List) - @Position)

					SET @Position = CHARINDEX(',',@List,1)
				END
		END

	RETURN
END

GO