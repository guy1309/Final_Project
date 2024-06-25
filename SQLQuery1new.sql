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


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_AddToCart](@ID INT, @Email VARCHAR(100) = null, @UnitPrice DECIMAL(18,2) = NULL, @Discount DECIMAL(18,2) = NULL  
,@Quantity INT  = NULL,@TotalPrice DECIMAL(18,2)  = NULL)  
AS  
BEGIN  
        DECLARE @UserId INT;  
  DECLARE @UnitPrice_ DECIMAL(18,2);  
  DECLARE @Discount_ DECIMAL(18,2);  
  DECLARE @TotalPrice_ DECIMAL(18,2);  
  SET @UserId = (SELECT TOp 1 ID FROM Users WHERE Email = @Email);  
  SET @UnitPrice_ = (SELECT  TOp 1 UnitPrice FROM Medicines WHERE ID = @ID);  
  SET @Discount_ = (SELECT (UnitPrice * @Quantity * Discount)/ 100 FROM Medicines WHERE ID = @ID);  
  SET @TotalPrice_ = (SELECT (UnitPrice * @Quantity) - @Discount_ FROM Medicines WHERE ID = @ID);  
    
  IF NOT EXISTS(SELECT 1 FROM Cart WHERE UserId = @UserId AND MedicineID = @ID)  
  BEGIN  
   INSERT INTO Cart(UserId,MedicineID,UnitPrice,Discount,Quantity,TotalPrice)  
   VALUES(@UserId,@ID,@UnitPrice_,@Discount_,@Quantity,@TotalPrice_);   
  END  
  ELSE  
  BEGIN  
   UPDATE Cart SET Quantity = (Quantity + @Quantity) WHERE UserId = @UserId AND MedicineID = @ID;  
  END  
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_AddUpdateMedicine](@ID INT = null, @Name VARCHAR(100) = null, @Manufacturer VARCHAR(100) = null,@UnitPrice DECIMAL(18,2) = null
,@Discount DECIMAL(18,2)  = null,@Quantity INT  = null,@ExpDate DATETIME  = null,@ImageUrl VARCHAR(100) = null,@Status INT = null
, @Type VARCHAR(100) = null)
AS
BEGIN
	IF @Type = 'Add'
	BEGIN
		INSERT INTO Medicines(Name,Manufacturer,UnitPrice,Discount,Quantity,ExpDate,ImageUrl,Status)
		VALUES(@Name,@Manufacturer,@UnitPrice,@Discount,@Quantity,@ExpDate,@ImageUrl,@Status)
	END
	IF @Type = 'Update'
	BEGIN
		UPDATE Medicines SET Name=@Name,Manufacturer=@Manufacturer,UnitPrice=@UnitPrice,Discount=@Discount,Quantity=@Quantity		
		WHERE ID = @ID;
	END
	IF @Type = 'Delete'
	BEGIN
		UPDATE Medicines SET Status = 0 WHERE ID = @ID;
	END
	IF @Type = 'Get'
	BEGIN
		SELECT * FROM Medicines WHERE Status = 1;
	END
	IF @Type = 'GetByID'
	BEGIN
		SELECT * FROM Medicines WHERE ID = @ID;
	END
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_CartList](@Email VARCHAR(100))
AS
BEGIN
    IF @Email != 'Admin'
	BEGIN
		SELECT C.ID, M.Name, M.Manufacturer, M.UnitPrice, M.Discount, C.Quantity, C.TotalPrice, M.ImageUrl FROM Cart C 
		INNER JOIN Medicines M ON M.ID = C.MedicineID
		INNER JOIN Users U ON U.ID = C.UserId
		WHERE U.Email =  @Email;
	END
	ELSE
	BEGIN
		SELECT M.ID, M.Name, M.Manufacturer, M.UnitPrice, M.Discount, M.Quantity, M.ImageUrl , 0 AS TotalPrice FROM Medicines M;
	END
END;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_login](@Email VARCHAR(100), @Password VARCHAR(100))
AS
BEGIN
	SELECT * FROM Users WHERE Email = @Email AND Password = @Password AND Status = 1;
END;
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_OrderList](@Type VARCHAR(100), @Email VARCHAR(100) = null, @ID INT)  
AS  
BEGIN  
 IF @Type = 'Admin'  
 BEGIN  
  SELECT O.ID,OrderNo,OrderTotal,OrderStatus,CONVERT(NVARCHAR,O.CreatedOn,107) AS CreatedOn  
  ,CONCAT(U.FirstName,' ',U.LastName ) AS CustomerName  
  FROM Orders O INNER JOIN Users U  
  ON U.ID = O.UserID;  
 END   
 IF @Type = 'User'  
 BEGIN  
  SELECT O.ID,OrderNo,OrderTotal,OrderStatus,CONVERT(NVARCHAR,O.CreatedOn,107) AS CreatedOn  
  ,CONCAT(U.FirstName,' ',U.LastName ) AS CustomerName  
  FROM Orders O INNER JOIN Users U  
  ON U.ID = O.UserID  
  WHERE U.Email = @Email;  
 END   
 IF @Type = 'UserItems'  
 BEGIN  
  SELECT   
  O.ID, O.OrderNo,O.OrderTotal,O.OrderStatus, M.Name AS MedicineName, M.Manufacturer,M.UnitPrice,OI.Quantity,OI.TotalPrice   
  ,CONVERT(NVARCHAR,O.CreatedOn,107) AS CreatedOn ,CONCAT(U.FirstName,' ',U.LastName ) AS CustomerName  
  , M.ImageUrl  
  FROM Orders O   
  INNER JOIN Users U ON U.ID = O.UserID  
  INNER JOIN OrderItems OI ON OI.OrderID = O.ID  
  INNER JOIN Medicines M ON M.ID = OI.MedicineID  
  WHERE O.ID = @ID;  
 END   
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_PlaceOrder](@Email VARCHAR(100))
AS
BEGIN
	DECLARE @OrderNO VARCHAR(100);
	DECLARE @OrderID INT;
	DECLARE @OrderTotal DECIMAL(18,2);
	DECLARE @UserID INT;
	SET @OrderNO =	(SELECT NEWID());
	SET @UserID = (SELECT ID FROM Users WHERE Email = @Email);

	IF OBJECT_ID('tempdb..#TempOrder') IS NOT NULL DROP TABLE #TempOrder; 
	
	SELECT * INTO #TempOrder 
	FROM Cart WHERE UserId = @UserID;

	SET @OrderTotal = (SELECT SUM(TotalPrice) from #TempOrder);

	INSERT INTO Orders(UserID,OrderNo,OrderTotal,OrderStatus,CreatedOn)
	VALUES(@UserID,@OrderNO,@OrderTotal,'Pending',GETDATE());

	SET @OrderID = (SELECT ID FROM Orders WHERE OrderNo = @OrderNO);

	INSERT INTO OrderItems(OrderID,MedicineID,UnitPrice,Discount,Quantity,TotalPrice)
	SELECT @OrderID, MedicineID,UnitPrice,Discount,Quantity,TotalPrice FROM #TempOrder;

	DELETE FROM Cart WHERE UserId = @UserID;
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_register](@ID INT = NULL, @FirstName VARCHAR(100) = NULL, @LastName VARCHAR(100) = NULL, @Password VARCHAR(100) = NULL,
@Email VARCHAR(100) = NULL, @Fund DECIMAL(18,2) = NULL, @Type VARCHAR(100) = NULL, @Status INT = NULL, @ActionType VARCHAR(100) = NULL)
AS
BEGIN
	IF @ActionType = 'Add'
	BEGIN
		INSERT INTO Users(FirstName,LastName,Password,Email,Fund,Type,Status,CreatedOn)
		VALUES(@FirstName,@LastName,@Password,@Email,@Fund,@Type,@Status,GETDATE())
	END
	IF @ActionType = 'Update'
	BEGIN
		UPDATE Users SET FirstName = @FirstName,LastName = @LastName,Password = @Password
		WHERE Email = @Email;
	END
	IF @ActionType = 'AddFund'
	BEGIN
		UPDATE Users SET Fund = @Fund WHERE Email = @Email;
	END
END;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_RemoveToCart](@ID INT, @Email VARCHAR(100) = null)    
AS    
BEGIN    
  DECLARE @UserId INT;    
  SET @UserId = (SELECT TOp 1 ID FROM Users WHERE Email = @Email);    
     
  DELETE FROM Cart WHERE UserId = @UserId AND ID = @ID;    
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_updateOrderStatus](@OrderNo VARCHAR(100) = NULL, @OrderStatus VARCHAR(100) = NULL)
AS
BEGIN
	UPDATE Orders SET OrderStatus = @OrderStatus WHERE OrderNo = @OrderNo;
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_UserList]
AS
BEGIN
	SELECT ID, FirstName, LastName, Email, CASE WHEN Fund IS NULL THEN 0.00 ELSE FUND END AS FUND
	, CONVERT(NVARCHAR,CreatedON,107) AS OrderDate, Status, Password  FROM Users WHERE Status = 1 AND Type != 'Admin';
END;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_viewUser](@ID INT = null, @Email VARCHAR(100) = null)
AS
BEGIN
	IF @ID IS NOT null AND @ID != 0
	BEGIN
		SELECT * FROM Users WHERE ID = @ID AND Status = 1;
	END
	IF @Email IS NOT null AND @Email != ''
	BEGIN
		SELECT * FROM Users WHERE Email = @Email AND Status = 1;
	END
END;
GO

USE [master]
GO
ALTER DATABASE [EMedicine] SET  READ_WRITE 
GO

INSERT INTO Users (FirstName, Lastname, Password, Email, Fund, Type, Status)
VALUES ('Guy','Mizrahi' ,'1234', 'Guy132000@gmail.com',0.00,'Users',1 )

INSERT INTO Users (FirstName, Lastname, Password, Email, Fund, Type, Status)
VALUES ('Adi','Offek' ,'Adi0402', 'adioffek@gmail.com',0.00,'Users',1 )

INSERT INTO Users (FirstName, Lastname, Password, Email, Fund, Type, Status)
VALUES ('admin','admin' ,'admin', 'admin',NULL,'admin',1 )

INSERT INTO Medicines(Name, Manufacturer, UnitPrice, Discount, Quantity, ExpDate, ImageUrl, Status)
VALUES ('Bepanthen','Bayer' , 27.0, 9.0 ,1,13-09-2027,'bepanthen.jpg',1 )
INSERT INTO Medicines(Name, Manufacturer, UnitPrice, Discount, Quantity, ExpDate, ImageUrl, Status)
VALUES ('Paracetamol','Teva' , 35.0, 9.0 ,1,13-09-2027,'paracetamol.jpg',1 )
INSERT INTO Medicines(Name, Manufacturer, UnitPrice, Discount, Quantity, ExpDate, ImageUrl, Status)
VALUES ('Protein bar','All-in' , 17.0, 7.0 ,1,13-09-2027,'allin.jpg',1 )
INSERT INTO Medicines(Name, Manufacturer, UnitPrice, Discount, Quantity, ExpDate, ImageUrl, Status)
VALUES ('Head& Shoulders','Procter & Gamble' , 20.0, 5.0 ,1,13-09-2027,'head&shoulders.jpg',1 )
INSERT INTO Medicines(Name, Manufacturer, UnitPrice, Discount, Quantity, ExpDate, ImageUrl, Status)
VALUES ('Ciprlex','Novartis Pharmaceuticals Corporation' , 30.0, 6.0 ,1,13-09-2027,'ciprlex.jpg',1 )
INSERT INTO Medicines(Name, Manufacturer, UnitPrice, Discount, Quantity, ExpDate, ImageUrl, Status)
VALUES ('Otrivin','Teva' , 22.0, 7.0 ,1,13-09-2027,'otrivin.jpg',1 )
INSERT INTO Medicines(Name, Manufacturer, UnitPrice, Discount, Quantity, ExpDate, ImageUrl, Status)
VALUES ('Rennie','Teva' , 37.0, 10.0 ,1,13-09-2027,'rennie.jpg',1 )
INSERT INTO Medicines(Name, Manufacturer, UnitPrice, Discount, Quantity, ExpDate, ImageUrl, Status)
VALUES ('Strepsils','Teva' , 23.0, 4.0 ,1,13-09-2027,'strepsils.jpg',1 )
INSERT INTO Medicines(Name, Manufacturer, UnitPrice, Discount, Quantity, ExpDate, ImageUrl, Status)
VALUES ('Solaray','Ogden' , 29.0, 0.0 ,1,13-09-2027,'solaray.jpg',1 )
GO


INSERT INTO Orders (UserID, OrderNo, OrderTotal, OrderStatus,CreatedOn)
VALUES(13,'57A29D33-8D55-4B30-AEED-0D9C16C6140A',188.00,'Pending',GETDATE())
GO

INSERT INTO OrderItems (OrderID, MedicineId, UnitPrice, Discount,Quantity, TotalPrice)
VALUES(1,'7','27.00','2.43','1','24.57')


INSERT INTO OrderItems (OrderID, MedicineId, UnitPrice, Discount,Quantity, TotalPrice)
VALUES(1,'9','17.00','3.57','3','47.43')

INSERT INTO OrderItems (OrderID, MedicineId, UnitPrice, Discount,Quantity, TotalPrice)
VALUES(1,'15','29.00','0.00','4','116.00')