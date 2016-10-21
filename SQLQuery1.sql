
drop table WK_UserLog
go
create table WK_UserLog
(
	Z_Id int primary key identity(1,1),
	Z_WeiKeId int foreign key references WeiKe(Id),
	Z_UserId int foreign key references Users(Id),
	Z_PlayCount int default 0,
	Z_SurfaceCount int default 1,
	Z_Progress float default 0.0,
)
go
create table WK_WeiKeLog
(  
	Z_Id int primary key identity(1,1),
	Z_WeiKeId int foreign key references WeiKe(Id),
	Z_UserId int foreign key references Users(Id),
)
go
select * from WK_UserLog
declare @weikeid int, @userid int, @pro float;go

set @weikeid = 882;
set @userid = 7;
set @playcount = 0;
set @surfacecount = 0;
set @pro = 2.123;
go
CREATE procedure P_I_WK_UserLog_AddSurfaceCount
	@weikeid int, @userid int,@pro float
as
	declare @is int
	select @is=count(1) from WK_UserLog where Z_UserId = @userid and Z_WeiKeId = @weikeid
	if @is > 0 
		update WK_UserLog set Z_SurfaceCount += 1 where Z_UserId = @userid and Z_WeiKeId = @weikeid
	else
		insert into WK_UserLog(Z_UserId,Z_WeiKeId, Z_Progress) values (@userid,@weikeid, @pro)
go 
delete WK_UserLog


create table WK_WeiKeView
(
	Z_Id int primary key identity(1,1),
	Z_UserId int foreign key references Users(ID),
	Z_WeikeId int foreign key references Weike(ID),
	Z_CollectSum int default 0,
	Z_PlaySum int default 0,
	Z_SupportSum int default 0
)
go
alter table weike
add Rating int default 0