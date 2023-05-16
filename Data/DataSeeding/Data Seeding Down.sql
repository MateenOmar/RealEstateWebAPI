--------------------
--Seeding Down
--------------------
DECLARE @UserID as int
SET @UserID = (select ID from Users where Username='Demo')
delete from Users where Username='Demo'
delete from PropertyTypes where LastUpdatedBy=@UserID
delete from FurnishingTypes where LastUpdatedBy=@UserID
delete from Cities where LastUpdatedBy=@UserID
delete from Properties where PostedBy=@UserID