
declare @PreCodeID bigint=1848
declare @query varchar(max)
declare @TEL bigint


DECLARE contact_cursor CURSOR FOR

	select TelephoneNo from telephone where switchprecodeid=@PreCodeID


OPEN contact_cursor;
FETCH NEXT FROM contact_cursor
INTO  
@TEL
 

WHILE @@FETCH_STATUS = 0
BEGIN


			declare @table varchar(max)
			declare @column varchar(max)
			
			DECLARE contact1_cursor CURSOR FOR
			
						 
							SELECT t.name,
							c.name 
							FROM sys.tables t 
							INNER JOIN sys.columns c 
							ON c.object_id=t.object_id 
							WHERE c.name like '%TelephoneNo%'
							and t.name!='telephone'
							--and t.name!='aaaaa$'
							--and t.name!='Excel$'
							order by  t.name
						
			
			
			OPEN contact1_cursor;
			FETCH NEXT FROM contact1_cursor
			INTO  
			@table,
			@column;
			 
			
			WHILE @@FETCH_STATUS = 0
			BEGIN
			
			
				--set @query = 'select top 10 '+stuff (@tel,3,4,'3948')+',* From [CRM].[dbo].[' + @table + '] where ' + @column + '=' +cast ( @TEL as varchar (max)) + ''
				--exec (@query)


				set @query = 'update [CRM].[dbo].[' + @table + '] set ' + @column + '='+stuff (@tel,3,4,'3948')+'
				  where ' + @column + '=' +cast ( @TEL as varchar (max)) + ''
				exec (@query)
			
			
			FETCH NEXT FROM contact1_cursor
			INTO  
			@table,
			@column;
			END
			
			CLOSE contact1_cursor;
			DEALLOCATE contact1_cursor;





FETCH NEXT FROM contact_cursor
INTO  
@TEL
END

CLOSE contact_cursor;
DEALLOCATE contact_cursor;
GO
---------------------
declare @PreCodeID bigint=1848

		update ChangeLocation set OldTelephone= stuff(tel.TelephoneNo,3,4,'3948')
		from ChangeLocation as c
		join telephone as tel on tel.TelephoneNo=c.OldTelephone where tel.switchprecodeid=@PreCodeID

		update ChangeLocation set NewTelephone= stuff(tel.TelephoneNo,3,4,'3948')
		from ChangeLocation as c
		join telephone as tel on tel.TelephoneNo=c.NewTelephone where tel.switchprecodeid=@PreCodeID

		update TakePossession set OldTelephone= stuff(tel.TelephoneNo,3,4,'3948')
		from TakePossession as t
		join telephone as tel on tel.TelephoneNo=t.OldTelephone where tel.switchprecodeid=@PreCodeID

--------------------

		update SwitchPort set PortNo=stuff(tel.TelephoneNo,3,4,'3948'),MDFHorizentalID=stuff(tel.TelephoneNo,3,4,'3948')
		from Telephone  as tel
		join SwitchPort as s on tel.SwitchPortID=s.id
		where tel.SwitchPrecodeid=@PreCodeID 
		

		update Telephone set TelephoneNo=stuff(TelephoneNo,3,4,'3948'),TelephoneNoIndividual=stuff (TelephoneNoIndividual,1,4,'3948')
		where SwitchPrecodeid=@PreCodeID 
		

		update  SwitchPrecode  set FromNumber=stuff (FromNumber,1,4,'3948'),ToNumber=stuff (ToNumber,1,4,'3948'),SwitchPreNo=@PreCodeID
		where id=@PreCodeID 
