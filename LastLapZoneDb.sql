USE LapZoneDb;


-- Create UserRole table
CREATE TABLE UserRole (
    RoleID INT IDENTITY(1,1) PRIMARY KEY,
    RoleName VARCHAR(50) NOT NULL
);

-- Insert default user roles (e.g., Admin, Normal User)
INSERT INTO UserRole (RoleName) VALUES
('Admin'),
('NormalUser');

-- Create _User table
CREATE TABLE _User (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    FullName VARCHAR(100) NOT NULL,
    Email VARCHAR(100) UNIQUE NOT NULL,
    PasswordHash VARCHAR(255) NOT NULL,  -- Store hashed password
    PhoneNumber VARCHAR(20),
    RoleID INT NOT NULL,
    CONSTRAINT FK_Role__User FOREIGN KEY (RoleID) REFERENCES UserRole(RoleID)
);

-- Create _Address table
CREATE TABLE _Address (
    AddressID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT NOT NULL,
    Country VARCHAR(50),
    Governorate VARCHAR(50),
    City VARCHAR(50),
    AddressLine VARCHAR(255) NOT NULL,
    CONSTRAINT FK_User_Address FOREIGN KEY (UserID) REFERENCES _User(UserID),
    CONSTRAINT UQ_User_Address UNIQUE (UserID, Country, Governorate, City, AddressLine)
);

-- Create Category table
CREATE TABLE Category (
    CategoryID INT IDENTITY(1,1) PRIMARY KEY,
    CategoryName VARCHAR(50) NOT NULL,
    _Description TEXT
);

-- Create Product table
CREATE TABLE Product (
    ProductID INT IDENTITY(1,1) PRIMARY KEY,
    ProductName VARCHAR(100) NOT NULL,
    _Description TEXT,
    Price DECIMAL(10, 2) NOT NULL CHECK (Price >= 0),
    CategoryID INT NOT NULL,
    StockQuantity INT NOT NULL CHECK (StockQuantity >= 0),
    CONSTRAINT FK_Category_Product FOREIGN KEY (CategoryID) REFERENCES Category(CategoryID)
);

-- Create Cart table
CREATE TABLE Cart (
    CartID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT NOT NULL,
    CONSTRAINT FK__User_Cart FOREIGN KEY (UserID) REFERENCES _User(UserID)
);

-- Create CartItem table
CREATE TABLE CartItem (
    CartItemID INT IDENTITY(1,1) PRIMARY KEY,
    CartID INT NOT NULL,
    ProductID INT NOT NULL,
    Quantity INT NOT NULL CHECK (Quantity > 0),
    CONSTRAINT FK_Cart_CartItem FOREIGN KEY (CartID) REFERENCES Cart(CartID),
    CONSTRAINT FK_Product_CartItem FOREIGN KEY (ProductID) REFERENCES Product(ProductID),
    CONSTRAINT UQ_CartItem UNIQUE (CartID, ProductID)
);

-- Create Wishlist table
CREATE TABLE Wishlist (
    WishlistID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT NOT NULL,
    ProductID INT NOT NULL,
    CONSTRAINT FK__User_Wishlist FOREIGN KEY (UserID) REFERENCES _User(UserID),
    CONSTRAINT FK_Product_Wishlist FOREIGN KEY (ProductID) REFERENCES Product(ProductID),
    CONSTRAINT UQ_Wishlist UNIQUE (UserID, ProductID)
);

-- Create _Order table
CREATE TABLE _Order (
    OrderID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT NOT NULL,
    OrderDate DATETIME default getdate() NOT NULL,
    TotalAmount DECIMAL(12, 2) NOT NULL CHECK (TotalAmount >= 0),
    CONSTRAINT FK__User_Order FOREIGN KEY (UserID) REFERENCES _User(UserID)
);

-- Create OrderItem table
CREATE TABLE OrderItem (
    OrderItemID INT IDENTITY(1,1) PRIMARY KEY,
    OrderID INT NOT NULL,
    ProductID INT NOT NULL,
    Quantity INT NOT NULL CHECK (Quantity > 0),
    Price DECIMAL(10, 2) NOT NULL CHECK (Price >= 0),
    CONSTRAINT FK_Order_OrderItem FOREIGN KEY (OrderID) REFERENCES _Order(OrderID),
    CONSTRAINT FK_Product_OrderItem FOREIGN KEY (ProductID) REFERENCES Product(ProductID),
    CONSTRAINT UQ_OrderItem UNIQUE (OrderID, ProductID)
);

-- Create Review table
CREATE TABLE Review (
    ReviewID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT NOT NULL,
    ProductID INT NOT NULL,
    Rating INT NOT NULL CHECK (Rating BETWEEN 1 AND 5),
    Comment TEXT,
    ReviewDate DATETIME default getdate() NOT NULL,
    CONSTRAINT FK__User_Review FOREIGN KEY (UserID) REFERENCES _User(UserID),
    CONSTRAINT FK_Product_Review FOREIGN KEY (ProductID) REFERENCES Product(ProductID)
);

-- Create LaptopDetail table
CREATE TABLE LaptopDetail (
    LaptopID INT IDENTITY(1,1) PRIMARY KEY,
    ProductID INT UNIQUE NOT NULL,
    Brand VARCHAR(50),
    CPU VARCHAR(100),
    Storage VARCHAR(50),
    RAM VARCHAR(50),
    GPU VARCHAR(50),
    ScreenSize DECIMAL(4, 2),
    _Weight DECIMAL(5, 2),
    HasWebcam BIT,
    CONSTRAINT FK_Product_LaptopDetail FOREIGN KEY (ProductID) REFERENCES Product(ProductID)
);
