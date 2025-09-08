-- 1. Users Table
CREATE TABLE Users (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    Email NVARCHAR(255) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL,
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    UserType INT NOT NULL DEFAULT 1, -- 1 = Customer, 2 = Admin
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    IsActive BIT NOT NULL DEFAULT 1
);

-- 2. Categories Table
CREATE TABLE Categories (
    CategoryId INT IDENTITY(1,1) PRIMARY KEY,
    CategoryName NVARCHAR(100) NOT NULL UNIQUE,
    Description NVARCHAR(500) NULL
);

-- 3. Products Table
CREATE TABLE Products (
    ProductId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    Price DECIMAL(10, 2) NOT NULL CHECK (Price >= 0),
    StockQuantity INT NOT NULL DEFAULT 0 CHECK (StockQuantity >= 0),
    ImageUrl NVARCHAR(500) NULL,
    CategoryId INT NOT NULL FOREIGN KEY REFERENCES Categories(CategoryId) ON DELETE CASCADE,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    IsActive BIT NOT NULL DEFAULT 1,
    AverageRating DECIMAL(3, 2) NULL DEFAULT 0.00 CHECK (AverageRating >= 0 AND AverageRating <= 5)
);

-- 4. ProductReviews Table (NEW - Highly Recommended)
CREATE TABLE ProductReviews (
    ReviewId INT IDENTITY(1,1) PRIMARY KEY,
    ProductId INT NOT NULL FOREIGN KEY REFERENCES Products(ProductId) ON DELETE CASCADE,
    UserId INT NOT NULL FOREIGN KEY REFERENCES Users(UserId) ON DELETE CASCADE,
    Rating INT NOT NULL CHECK (Rating >= 1 AND Rating <= 5),
    Comment NVARCHAR(MAX) NULL,
    ReviewedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    CONSTRAINT UK_ProductReviews_UserProduct UNIQUE (UserId, ProductId)
);

-- 5. Carts Table
CREATE TABLE Carts (
    CartId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserId INT NOT NULL UNIQUE FOREIGN KEY REFERENCES Users(UserId) ON DELETE CASCADE,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE()
);

-- 6. CartItems Table
CREATE TABLE CartItems (
    CartItemId INT IDENTITY(1,1) PRIMARY KEY,
    CartId UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES Carts(CartId) ON DELETE CASCADE,
    ProductId INT NOT NULL FOREIGN KEY REFERENCES Products(ProductId) ON DELETE CASCADE,
    Quantity INT NOT NULL CHECK (Quantity > 0),
    AddedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    CONSTRAINT UK_CartItems_CartProduct UNIQUE (CartId, ProductId)
);

-- 7. Wishlists Table
CREATE TABLE Wishlists (
    WishlistId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL FOREIGN KEY REFERENCES Users(UserId) ON DELETE CASCADE,
    ProductId INT NOT NULL FOREIGN KEY REFERENCES Products(ProductId) ON DELETE CASCADE,
    AddedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    CONSTRAINT UK_Wishlists_UserProduct UNIQUE (UserId, ProductId)
);

-- 8. Addresses Table (NEW - Optional but Recommended)
CREATE TABLE Addresses (
    AddressId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL FOREIGN KEY REFERENCES Users(UserId) ON DELETE CASCADE,
    FullName NVARCHAR(255) NOT NULL,
    PhoneNumber NVARCHAR(20) NULL,
    AddressLine1 NVARCHAR(255) NOT NULL,
    AddressLine2 NVARCHAR(255) NULL,
    City NVARCHAR(100) NOT NULL,
    State NVARCHAR(100) NOT NULL,
    PostalCode NVARCHAR(20) NOT NULL,
    Country NVARCHAR(100) NOT NULL DEFAULT 'India',
    IsDefault BIT NOT NULL DEFAULT 0
);

-- 9. Orders Table
CREATE TABLE Orders (
    OrderId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL FOREIGN KEY REFERENCES Users(UserId),
    OrderTotal DECIMAL(10, 2) NOT NULL,
    OrderStatus NVARCHAR(50) NOT NULL DEFAULT 'Pending',
    OrderDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    ShippingAddressId INT NOT NULL FOREIGN KEY REFERENCES Addresses(AddressId) -- Changed from ShippingAddress NVARCHAR(MAX)
);

-- 10. OrderItems Table
CREATE TABLE OrderItems (
    OrderItemId INT IDENTITY(1,1) PRIMARY KEY,
    OrderId INT NOT NULL FOREIGN KEY REFERENCES Orders(OrderId) ON DELETE CASCADE,
    ProductId INT NOT NULL FOREIGN KEY REFERENCES Products(ProductId),
    Quantity INT NOT NULL CHECK (Quantity > 0),
    UnitPrice DECIMAL(10, 2) NOT NULL CHECK (UnitPrice >= 0)
);

-- 11. Payments Table (NEW - Highly Recommended)
CREATE TABLE Payments (
    PaymentId INT IDENTITY(1,1) PRIMARY KEY,
    OrderId INT NOT NULL FOREIGN KEY REFERENCES Orders(OrderId),
    PaymentDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    PaymentMethod NVARCHAR(50) NOT NULL,
    Amount DECIMAL(10, 2) NOT NULL,
    Status NVARCHAR(50) NOT NULL DEFAULT 'Pending'
);

-- 12. Coupons Table
CREATE TABLE Coupons (
    CouponId INT IDENTITY(1,1) PRIMARY KEY,
    Code NVARCHAR(50) NOT NULL UNIQUE,
    Description NVARCHAR(255) NULL,
    DiscountType INT NOT NULL,
    DiscountValue DECIMAL(10, 2) NOT NULL CHECK (DiscountValue > 0),
    MinimumAmount DECIMAL(10, 2) NULL CHECK (MinimumAmount >= 0),
    CreatedById INT NOT NULL FOREIGN KEY REFERENCES Users(UserId),
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    ExpiresAt DATETIME2 NULL,
    IsActive BIT NOT NULL DEFAULT 1
);

-- 13. UserCoupons Table
CREATE TABLE UserCoupons (
    UserCouponId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL FOREIGN KEY REFERENCES Users(UserId) ON DELETE CASCADE,
    CouponId INT NOT NULL FOREIGN KEY REFERENCES Coupons(CouponId) ON DELETE CASCADE,
    IsUsed BIT NOT NULL DEFAULT 0,
    CONSTRAINT UK_UserCoupons_UserCoupon UNIQUE (UserId, CouponId)
);