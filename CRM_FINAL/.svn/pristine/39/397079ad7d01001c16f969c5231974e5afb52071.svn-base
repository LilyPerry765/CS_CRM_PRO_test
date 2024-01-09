USE [CRM]
GO

--SELECT * 
--INTO ExchangetT
--From (SELECT     TranslationOpticalCabinetToNormalConncetions.FromTelephoneNo , oldPost.Number as OldPost , oldPostContact.ConnectionNo as oldConnection , TranslationOpticalCabinetToNormalConncetions.ToTelephoneNo , NewPost.Number as NewPost , NewPostContact.ConnectionNo as NewConnection
--from   TranslationOpticalCabinetToNormalConncetions
--join Post as OldPost on oldPost.ID = TranslationOpticalCabinetToNormalConncetions.FromPostID
--join PostContact as OldPostContact on OldPostContact.ID = TranslationOpticalCabinetToNormalConncetions.FromPostContactID
--join Post as NewPost on NewPost.ID = TranslationOpticalCabinetToNormalConncetions.ToPostID
--join PostContact as NewPostContact on NewPostContact.ID = TranslationOpticalCabinetToNormalConncetions.ToPostConntactID
--WHERE (RequestID = 9406230229) ) AS T

--select telephone.TelephoneNo
--FROM            ExchangetT AS ET INNER JOIN
--                         TranslationOpticalCabinetToNormalConncetions AS EC ON ET.ToTelephoneNo = EC.ToTelephoneNo INNER JOIN
--                         PostContact ON ET.NewConnection = PostContact.ConnectionNo INNER JOIN
--                         Post ON PostContact.PostID = Post.ID INNER JOIN
--                         Cabinet ON Post.CabinetID = Cabinet.ID INNER JOIN
--                         Center ON Cabinet.CenterID = Center.ID INNER JOIN
--						 bucht on bucht.ConnectionID = PostContact.ID INNER JOIN
--						 telephone on telephone.SwitchPortID = bucht.SwitchPortID
--						 where Center.ID = 5 and Cabinet.CabinetNumber = 102 and Post.number = ET.NewPost and PostContact.ConnectionNo = ET.NewConnection and telephone.TelephoneNo not in
--						 (select ExchangetT.totelephoneNo from ExchangetT )

--Update EC set EC.ToPostID = Post.ID , EC.ToPostConntactID= PostContact.ID
--FROM            ExchangetT AS ET INNER JOIN
--                         TranslationOpticalCabinetToNormalConncetions AS EC ON ET.ToTelephoneNo = EC.ToTelephoneNo INNER JOIN
--                         PostContact ON ET.NewConnection = PostContact.ConnectionNo INNER JOIN
--                         Post ON PostContact.PostID = Post.ID INNER JOIN
--                         Cabinet ON Post.CabinetID = Cabinet.ID INNER JOIN
--                         Center ON Cabinet.CenterID = Center.ID
--						 where Center.ID = 5 and Cabinet.CabinetNumber = 102 and Post.number = ET.NewPost and PostContact.ConnectionNo = ET.NewConnection

--update  PostContact set PostContact.Status = 5
--FROM            ExchangetT AS ET INNER JOIN
--                         Telephone AS Tel ON Tel.TelephoneNo = ET.ToTelephoneNo INNER JOIN
--						 Bucht as B on Tel.SwitchportID = B.switchportid INNER JOIN
--                         PostContact ON ET.NewConnection = PostContact.ConnectionNo INNER JOIN
--                         Post ON PostContact.PostID = Post.ID INNER JOIN
--                         Cabinet ON Post.CabinetID = Cabinet.ID INNER JOIN
--                         Center ON Cabinet.CenterID = Center.ID
--						 where Center.ID = 5 and Cabinet.CabinetNumber = 102 and Post.number = ET.NewPost and PostContact.ConnectionNo = ET.NewConnection

--update  Bucht set Bucht.ConnectionID = PostContact.ID
--FROM            ExchangetT AS ET INNER JOIN
--                         Telephone AS Tel ON Tel.TelephoneNo = ET.ToTelephoneNo INNER JOIN
--						 Bucht as B on Tel.SwitchportID = B.switchportid INNER JOIN
--                         PostContact ON ET.NewConnection = PostContact.ConnectionNo INNER JOIN
--                         Post ON PostContact.PostID = Post.ID INNER JOIN
--                         Cabinet ON Post.CabinetID = Cabinet.ID INNER JOIN
--                         Center ON Cabinet.CenterID = Center.ID
--						 where Center.ID = 5 and Cabinet.CabinetNumber = 102 and Post.number = ET.NewPost and PostContact.ConnectionNo = ET.NewConnection


--update  PostContact set PostContact.Status = 1
--FROM            ExchangetT AS ET INNER JOIN
--                         Telephone AS Tel ON Tel.TelephoneNo = ET.ToTelephoneNo INNER JOIN
--						 Bucht as B on Tel.SwitchportID = B.switchportid INNER JOIN
--                         PostContact ON ET.NewConnection = PostContact.ConnectionNo INNER JOIN
--                         Post ON PostContact.PostID = Post.ID INNER JOIN
--                         Cabinet ON Post.CabinetID = Cabinet.ID INNER JOIN
--                         Center ON Cabinet.CenterID = Center.ID
--						 where Center.ID = 5 and Cabinet.CabinetNumber = 102 and Post.number = ET.NewPost and PostContact.ConnectionNo = ET.NewConnection


DECLARE @q AS TABLE (rowNumber int , xid INT NOT NULL, xdoc XML NOT NULL, modified TINYINT NOT NULL DEFAULT 0 , post int , connection int)
INSERT
INTO    @q (rowNumber , xid, xdoc , post , connection)
SELECT ROW_NUMBER() OVER(ORDER BY RequestLog.ID DESC) AS rowNumber , RequestLog.ID, RequestLog.Description , ET.NewPost as post, ET.NewConnection as connection
FROM ExchangetT AS ET INNER JOIN RequestLog on ET.ToTelephoneNo = RequestLog.ToTelephoneNo


declare @i int = 1;
declare @count int = (select Count(*) from @q)

while(@i <= @count)
begin

declare @post int; select @post = post from @q where rowNumber = @i
declare @connection int; select @connection = connection from @q where rowNumber = @i


UPDATE  @q SET xdoc.modify('replace value of (/TranslationOpticalToNormal/NewPostContact/text())[1] with sql:variable("@connection")') where rowNumber = @i
UPDATE  @q SET xdoc.modify('replace value of (/TranslationOpticalToNormal/NewPost/text())[1] with sql:variable("@post")') where rowNumber = @i

set @i = @i + 1 ;
end

UPDATE  RequestLog SET   Description = xdoc FROM    @q q WHERE   id = q.xid


--select * FROM ExchangetT AS ET INNER JOIN RequestLog on ET.ToTelephoneNo = RequestLog.ToTelephoneNo where 8338431301 = RequestLog.ToTelephoneNo


GO
	

