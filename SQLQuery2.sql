create table [dbo].[MessagesType]
(
	Id int primary key Identity(1,1),
	Name nvarchar(20) not null
)
go
CREATE TABLE [dbo].[Messages]
(
	[Id] INT NOT NULL PRIMARY KEY identity(1,1),
	[FromUserId] int not null foreign key references Users(Id),
	[ToUserID] int not null	foreign key references Users(Id),
	[Subject] int not null,
	[MSGTypeId] int foreign key references MessagesType(Id),
	[MSG] nvarchar(max) not null,
	[state] bit default(0),
	[Time] datetime default(getdate())
)
go
drop table Messages
go
insert into MessagesType(Name) values('用户消息')
select * from MessagesType