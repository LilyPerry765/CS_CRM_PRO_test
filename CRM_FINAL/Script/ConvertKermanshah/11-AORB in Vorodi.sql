

update  [salas].[dbo].[Vorodi] set AORB = 2 where POST like  '%A%'
update  [salas].[dbo].[Vorodi] set POST = REPLACE(POST,'A','')  where POST like  '%A%'

update [salas].[dbo].[Vorodi] set AORB = 3 where POST like  '%B%'
update [salas].[dbo].[Vorodi] set POST = REPLACE(POST,'B','')  where POST like  '%B%'

update [salas].[dbo].[Vorodi] set AORB = 4 where POST like  '%C%'
update [salas].[dbo].[Vorodi] set POST = REPLACE(POST,'C','')  where POST like  '%C%'


update  [salas].[dbo].[Vorodi] set AORB = 2 where POST like  '%a%'
update  [salas].[dbo].[Vorodi] set POST = REPLACE(POST,'a','')  where POST like  '%a%'

update [salas].[dbo].[Vorodi] set AORB = 3 where POST like  '%b%'
update [salas].[dbo].[Vorodi] set POST = REPLACE(POST,'b','')  where POST like  '%b%'

update [salas].[dbo].[Vorodi] set AORB = 4 where POST like  '%c%'
update [salas].[dbo].[Vorodi] set POST = REPLACE(POST,'c','')  where POST like  '%c%'


update [salas].[dbo].[Vorodi] set AORB = 1 where AORB is null


