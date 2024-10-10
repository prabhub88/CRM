--CREATE DATABASE CRM
--GO

USE CRM
GO


CREATE TABLE UserTypes
(
Id bigint identity NOT NULL primary key,
UserType VARCHAR(250) NOT NULL,
Descriptions VARCHAR(500) NOT NULL,
IsActive BIT
)
GO

INSERT INTO UserTypes Values('RelationshipManager','This is relationship manager role',1)
INSERT INTO UserTypes Values('Manager','This is manager role',1)
INSERT INTO UserTypes Values('Auditor','This is auditor role',1)
INSERT INTO UserTypes Values('HR','This is HR role',1)
select * from usertypes

CREATE TABLE dbo.Users
(
Id bigint identity NOT NULL,
UserName Nvarchar(100) Not NULL,
FirstName Varchar(100) NOT NULL,
SecondName VARCHAR(100) NULL,
UserType bigint NOT NULL,
Password NVARCHAR(100) NOT NULL,
CreatedBy VARCHAR(100) NOT NULL,
Created DATETIME NOT NULL,
ModifiedBy VARCHAR(100) NOT NULL,
Modified DATETIME NOT NULL
)
GO
ALTER TABLE Users
ADD FOREIGN KEY (UserType) REFERENCES UserTypes (Id)  

INSERT INTO Users Values('admin','Manger','Relationship',1,'Test#123','admin',getdate(),'admin',getdate())
select * from users
CREATE TABLE Genders
(
Id bigint identity primary key,
Descriptions Varchar(100)
)

insert into Genders values ('Male')
insert into Genders values ('Female')
insert into Genders values ('Not interested to Reveal')

CREATE TABLE Customers
(
Id bigint identity,
CustomerNumber bigint primary key,
CustomerName Varchar(255),
DOB Date,
Gender bigint
)
ALTER TABLE Customers
ADD FOREIGN KEY (Gender) REFERENCES Genders (Id) 

INSERT INTO Customers VALUES(111,'pRABHU','1988-07-15',1)
INSERT INTO Customers VALUES(112,'raja','1988-07-15',1)
INSERT INTO Customers VALUES(113,'banu','1988-07-15',2)
INSERT INTO Customers VALUES(114,'abdul','1988-07-15',1)
INSERT INTO Customers VALUES(115,'arya','1988-07-15',2)
INSERT INTO Customers VALUES(116,'rahuman','1988-07-15',1)
INSERT INTO Customers VALUES(117,'deva','1988-07-15',1)


select * from customers