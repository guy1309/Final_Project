create DATABASE Emedicine;
use Emedicine

Create Table Users(ID INT IDENTITY (1,1) PRIMARY KEY,
FirstName VARCHAR(100),
LastName VARCHAR (100),
Password VARCHAR (100),
Email VARCHAR (100),
Fund DECIMAL (18,2),
Type VARCHAR(100),
Status INT,CreatedOn Datetime);


Create Table Medicines (ID INT IDENTITY (1,1) PRIMARY KEY,
Name VARCHAR (100),
Manufacturer VARCHAR (100),
UnitPrice DECIMAL (18,2),
Discount DECIMAL (18,2),
Quantity  INT,
ExpDate Datetime,
ImageUrl VARCHAR(100),
Status INT )


Create Table Cart (ID INT IDENTITY(1,1) PRIMARY KEY,
UserId INT,
MedicineId INT,
UnitPrice DECIMAL(18,2),
Discount DECIMAL (18,2),
Quantity INT,
TotalPrice DECIMAL (18,2))


create Table  Orders (ID INT IDENTITY (1,1) PRIMARY KEY,
UserId INT,
OrderNo VARCHAR(100),
OrderTotal DECIMAL (18,2),
OrderStatus VARCHAR(100))

Create Table OrderItems (ID INT IDENTITY (1,1) PRIMARY KEY,
OrderId INT,
MedicineId INT,
UnitPice DECIMAL(18,2),
Discount DECIMAL (18,2),
Quantity INT,
TotalPrice DECIMAL (18,2))


SELECT * FROM Users;
SELECT * FROM Medicines;
SELECT * FROM Cart;
SELECT * FROM Orders;
SELECT * FROM OrderItems;


Insert into Users 