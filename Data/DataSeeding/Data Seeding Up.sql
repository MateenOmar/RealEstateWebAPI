select 'Bangalore',@UserID,getdate(),'India'

--------------------------
--Seed Properties
--------------------------
--Seed property for sell
IF not exists (select top 1 name from Properties where Name='White House Demo')
insert into Properties(SellRent,Name,PropertyTypeId,BHK,FurnishingTypeId,Price,BuiltArea,CarpetArea,Address,
Address2,CityId,FloorNo,TotalFloors,ReadyToMove,MainEntrance,Security,Gated,Maintenance,EstPossessionOn,Age,Description,PostedOn,PostedBy,LastUpdatedOn,LastUpdatedBy)
select 
1, --Sell Rent
'White House Demo', --Name
(select Id from PropertyTypes where Name='Apartment'), --Property Type ID
2, --BHK
(select Id from FurnishingTypes where Name='Fully'), --Furnishing Type ID
1800, --Price
1400, --Built Area
900, --Carpet Area
'6 Street', --Address
'Golf Course Road', -- Address2
(select top 1 Id from Cities), -- City ID
3, -- Floor No
3, --Total Floors
1, --Ready to Move
'East', --Main Entrance
0, --Security
1, --Gated
300, -- Maintenance
'2019-01-01', -- Establishment or Posession on
0, --Age
'Well Maintained builder floor available for rent at prime location. # property features- - 5 mins away from metro station - Gated community - 24*7 security. # property includes- - Big rooms (Cross ventilation & proper sunlight) - 
Drawing and dining area - Washrooms - Balcony - Modular kitchen - Near gym, market, temple and park - Easy commuting to major destination. Feel free to call With Query.', --Description
GETDATE(), --Posted on
@UserID, --Posted by
GETDATE(), --Last Updated on
@UserID --Last Updated by

---------------------------
--Seed property for rent
---------------------------
IF not exists (select top 1 name from Properties where Name='Birla House Demo')
insert into Properties(SellRent,Name,PropertyTypeID,BHK,FurnishingTypeID,Price,BuiltArea,CarpetArea,Address,
Address2,CityID,FloorNo,TotalFloors,ReadyToMove,MainEntrance,Security,Gated,Maintenance,EstPossessionOn,Age,Description,PostedOn,PostedBy,LastUpdatedOn,LastUpdatedBy)
select 
2, --Sell Rent
'Birla House Demo', --Name
(select ID from PropertyTypes where Name='Apartment'), --Property Type ID
2, --BHK
(select ID from FurnishingTypes where Name='Fully'), --Furnishing Type ID
1800, --Price
1400, --Built Area
900, --Carpet Area
'6 Street', --Address
'Golf Course Road', -- Address2
(select top 1 ID from Cities), -- City ID
3, -- Floor No
3, --Total Floors
1, --Ready to Move
'East', --Main Entrance
0, --Security
1, --Gated
300, -- Maintenance
'2019-01-01', -- Establishment or Posession on
0, --Age
'Well Maintained builder floor available for rent at prime location. # property features- - 5 mins away from metro station - Gated community - 24*7 security. # property includes- - Big rooms (Cross ventilation & proper sunlight) - 
Drawing and dining area - Washrooms - Balcony - Modular kitchen - Near gym, market, temple and park - Easy commuting to major destination. Feel free to call With Query.', --Description
GETDATE(), --Posted on
@UserID, --Posted by
GETDATE(), --Last Updated on
@UserID --Last Updated by

