create database store_system

use store_system

--create tables and PK
CREATE TABLE Customers (
    CustomerID INT not null,
    FullName NVARCHAR(100),
    Address NVARCHAR(200),
    Phone NVARCHAR(20),
    Email NVARCHAR(100)
);

ALTER TABLE Customers
ADD CONSTRAINT PK_Customers PRIMARY KEY (CustomerID);

CREATE TABLE Products (
    ProductID INT not null,
    ProductName NVARCHAR(100),
    Price DECIMAL(10,2),
    StockQuantity INT,
    Category NVARCHAR(50)
);

ALTER TABLE Products
ADD CONSTRAINT PK_Products PRIMARY KEY (ProductID);

CREATE TABLE Orders (
    OrderID INT not null ,
    OrderDate DATETIME,
    CustomerID INT,
    TotalAmount DECIMAL(12,2)
);

ALTER TABLE Orders
ADD CONSTRAINT PK_Orders PRIMARY KEY (OrderID);

CREATE TABLE OrderDetails (
    OrderID INT not null,
    ProductID INT not null,
    Quantity INT,
    UnitPrice DECIMAL(10,2)
);

ALTER TABLE OrderDetails
ADD CONSTRAINT PK_OrderDetails PRIMARY KEY (OrderID, ProductID);


-- Foreign Keys
ALTER TABLE Orders
ADD CONSTRAINT FK_Orders_Customers
FOREIGN KEY (CustomerID)
REFERENCES Customers(CustomerID);

ALTER TABLE OrderDetails
ADD CONSTRAINT FK_OrderDetails_Orders
FOREIGN KEY (OrderID)
REFERENCES Orders(OrderID);

ALTER TABLE OrderDetails
ADD CONSTRAINT FK_OrderDetails_Products
FOREIGN KEY (ProductID)
REFERENCES Products(ProductID);


-- Check Constraint
ALTER TABLE OrderDetails
ADD CONSTRAINT CHK_Quantity CHECK (Quantity > 0);

--total func
CREATE FUNCTION GetOrderTotal
(
    @OrderID INT
)
RETURNS DECIMAL(12,2)
AS
BEGIN
    DECLARE @Total DECIMAL(12,2);

    SELECT @Total = SUM(Quantity * UnitPrice)
    FROM OrderDetails
    WHERE OrderID = @OrderID;

    IF @Total IS NULL
        SET @Total = 0;

    RETURN @Total;
END;


--stored procdure
CREATE PROCEDURE UpdateOrderTotal
    @OrderID INT
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM Orders WHERE OrderID = @OrderID)
    BEGIN
        PRINT 'Order does not exist';
        RETURN;
    END

    DECLARE @Total DECIMAL(12,2);

    SET @Total = dbo.GetOrderTotal(@OrderID);

    IF @Total > 0
    BEGIN
        UPDATE Orders
        SET TotalAmount = @Total
        WHERE OrderID = @OrderID;

        PRINT 'Total written successfully';
    END
    ELSE
    BEGIN
        PRINT 'Total is zero or negative. Not written.';
    END
END;

--triggers 

CREATE TABLE OrderDetails_Audit (
    AuditID INT IDENTITY(1,1) PRIMARY KEY,
    OrderID INT,
    ProductID INT,
    OldQuantity INT NULL,
    NewQuantity INT NULL,
    OldUnitPrice DECIMAL(10,2) NULL,
    NewUnitPrice DECIMAL(10,2) NULL,
    ActionType NVARCHAR(20),
    ActionDate DATETIME DEFAULT GETDATE()
);

CREATE TRIGGER TR_AfterUpdate_OrderDetails
ON OrderDetails
AFTER UPDATE
AS
BEGIN
    INSERT INTO OrderDetails_Audit
    (
        OrderID,
        ProductID,
        OldQuantity,
        NewQuantity,
        OldUnitPrice,
        NewUnitPrice,
        ActionType
    )
    SELECT 
        d.OrderID,
        d.ProductID,
        d.Quantity AS OldQuantity,
        i.Quantity AS NewQuantity,
        d.UnitPrice AS OldUnitPrice,
        i.UnitPrice AS NewUnitPrice,
        'UPDATE'
    FROM deleted d
    INNER JOIN inserted i
        ON d.OrderID = i.OrderID
        AND d.ProductID = i.ProductID;
END;

CREATE TRIGGER TR_AfterDelete_OrderDetails
ON OrderDetails
AFTER DELETE
AS
BEGIN
    INSERT INTO OrderDetails_Audit
    (
        OrderID,
        ProductID,
        OldQuantity,
        OldUnitPrice,
        ActionType
    )
    SELECT 
        d.OrderID,
        d.ProductID,
        d.Quantity,
        d.UnitPrice,
        'DELETE'
    FROM deleted d;
END;