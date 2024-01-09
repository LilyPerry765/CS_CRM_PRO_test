
--برای خالی کردن باید به طور موقت رابطه از کار بندازیم
--ابتدا باید نام Constraint  را بیابیم

--SELECT 
--	f.*
--FROM 
--	sys.foreign_keys f
--INNER JOIN 
--	sys.foreign_key_columns fc on f.object_id = fc.constraint_object_id
--INNER JOIN 
--	sys.tables T ON T.object_id = FC.referenced_object_id
--WHERE 
--	OBJECT_NAME(f.referenced_object_id) = 'Resource' 

--ALTER TABLE RoleResource NOCHECK CONSTRAINT FK_RoleResource_Resource 
--ALTER TABLE [Resource] NOCHECK CONSTRAINT FK_Resource_Resource

--خالی کردن جدول منابع
--delete from  [Resource]

--ALTER TABLE RoleResource CHECK CONSTRAINT FK_RoleResource_Resource 

--تشخیص این که آیا جدول منابع داری ستون شمارش اتوماتیک هست یا خیر
--روش اول
--SELECT 
--	c.name
--FROM 
--	SYS.schemas S 
--INNER JOIN 
--	SYS.tables T ON S.schema_id = T.schema_id
--INNER JOIN 
--	SYS.columns C ON T.object_id  = C.object_id
--INNER JOIN 
--	SYS.identity_columns IC ON IC.object_id = t.object_id and ic.column_id = c.column_id
--WHERE 
--	T.name = 'Resource'

----روش دوم
--SELECT 
--	c.*
--FROM
--	INFORMATION_SCHEMA.COLUMNS C
--WHERE 
--	C.TABLE_SCHEMA = 'dbo'
--	AND
--	C.TABLE_NAME  = 'Resource'
--	AND
--	COLUMNPROPERTY(OBJECT_ID(C.TABLE_NAME),C.COLUMN_NAME,'IsIdentity') = 1


--13931010 - 13:27
--منابع از سرور بر روی لوکال وارد شد
--SET IDENTITY_INSERT [Resource] ON

--INSERT INTO [Resource]
--(ID,ParentID,Name,[Description],[Type])
--SELECT 
--	RR.ID,rr.ParentID,rr.Name,rr.[Description],rr.[Type]
--FROM 
--	[78.39.252.109].CRM.dbo.[Resource] RR

--SET IDENTITY_INSERT [Resource] OFF

--13931010 - 13:41
--منابع-نقش از روی سرور برای کاربر سیستمی وارد شد
--RoleResource

--SET IDENTITY_INSERT RoleResource ON

--INSERT INTO RoleResource
--(ID,ResourceID,RoleID,IsEditable)
--SELECT 
--	RR.ID,
--	RR.ResourceID,
--	37, --شناسه نقش سیستم بر روی لوکال
--	RR.IsEditable
--FROM	
--	[78.39.252.109].CRM.dbo.RoleResource RR
--WHERE 
--	RR.RoleID = 1 --شناسه نقش سیستم بر روی سرور کرمانشاه

--SET IDENTITY_INSERT RoleResource OFF
