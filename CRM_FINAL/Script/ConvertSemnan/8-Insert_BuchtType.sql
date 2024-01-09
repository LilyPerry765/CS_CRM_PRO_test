
DELETE Bucht
DBCC CHECKIDENT ('Bucht',RESEED , 0)

DELETE BuchtType
DBCC CHECKIDENT ('BuchtType',RESEED , 0)



insert into BuchtType
       (
	    BuchtTypeName
		,ParentID
		,IsReadOnly
	    ,ElkaID
	   )
  SELECT 
         --  (select id  from Center where CenterCode = bb.[CEN_CODE])as CenterID 
		 --,
		 bt.[BOOKHT_TYPE_NAME]
		 ,null
		 ,0
		 ,bt.[BOOKHT_TYPE_ID]
  FROM [ORACLECRM]..[TT].[BOOKHT_TYPE] as bt


UPDATE [dbo].[BuchtType]
   SET [IsReadOnly] = 1
WHERE  (BuchtTypeName NOT LIKE N'%بوخت نوری%')

UPDATE [dbo].[BuchtType]
   SET ParentID = (SELECT ID FROM BuchtType WHERE  (BuchtTypeName LIKE N' بوخت نوری'))
WHERE  (BuchtTypeName LIKE N'بوخت نوری%')
GO


---- برای پی سی ام های یک نوع در نظر گرفته شد حالت ورودی و خروجی با فیلد دیگر مشخص میشود
--DELETE BuchtType
--WHERE BuchtTypeName = 'بوخت PCMO'

--UPDATE BuchtType SET BuchtTypeName = 'بوخت PCM' WHERE BuchtTypeName = 'بوخت PCMI'




