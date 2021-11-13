create table users(
Id int not null primary key IDENTITY(1,1),
username nvarchar(50),
password nvarchar(50)
)

insert into users values ('admin', 'admin123')

select * from users


