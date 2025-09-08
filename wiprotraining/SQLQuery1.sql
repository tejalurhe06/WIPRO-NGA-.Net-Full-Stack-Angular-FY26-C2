use Person;

1 . Primary key

create table Person
(
id int primary key,
lastname varchar(50) not null,
firstname varchar(50) not null,
age int 
)

insert into Person(id,lastname,firstname,age) values(100,'Urhe','Tejal',24);

2 . Check and not null constraint

create table Person1
(
id int ,
lastname varchar(50) not null,
firstname varchar(50) not null,
age int check (age >=18)
)

insert into Person1(id,lastname,firstname,age) values(200,'Urhe','Tulsi',12);

3 . default constraint

create table Person2
(
id int ,
lastname varchar(50) not null,
firstname varchar(50) not null,
city varchar(50) default 'Banglore'
)

insert into Person2(id,lastname,firstname) values(300,'Deshmukh','Prachi');

4 . unique constraint

create table Person3
(
id int not null unique,
lastname varchar(50) not null,
firstname varchar(50) not null,
city varchar(50) 
)

insert into Person3(id,lastname,firstname,city) values(300,'Deshmukh','Prachi','latur');

5 . Foreign key

create table Person4
(
id int primary key,
lastname varchar(50) not null,
firstname varchar(50) not null,
city varchar(50) 
)

create table Orders
(
orderid int,
person_id int,

primary key(orderid),
foreign key(person_id) references Person4(id)
)

