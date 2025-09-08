-- 1. Create database
CREATE DATABASE BookStoreDb;
GO


USE BookStoreDb;
GO

-- 2. Books table
IF OBJECT_ID('dbo.Books', 'U') IS NOT NULL DROP TABLE dbo.Books;
GO
CREATE TABLE dbo.Books (
Id INT IDENTITY(1,1) PRIMARY KEY,
Title NVARCHAR(200) NOT NULL,
Author NVARCHAR(150) NOT NULL,
Genre NVARCHAR(100) NULL,
ISBN NVARCHAR(20) NULL UNIQUE,
Price DECIMAL(10,2) NOT NULL CHECK (Price >= 0),
Stock INT NOT NULL DEFAULT 0 CHECK (Stock >= 0),
PublishedDate DATE NULL,
CreatedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
UpdatedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
);
GO

-- 3. Helper: auto-update UpdatedAt
CREATE OR ALTER TRIGGER trg_Books_SetUpdatedAt
ON dbo.Books
AFTER UPDATE
AS
BEGIN
SET NOCOUNT ON;
UPDATE b
SET UpdatedAt = SYSUTCDATETIME()
FROM dbo.Books b
JOIN inserted i ON b.Id = i.Id;
END
GO

-- 4. Stored Procedures


-- Add
CREATE OR ALTER PROCEDURE dbo.usp_AddBook
@Title NVARCHAR(200),
@Author NVARCHAR(150),
@Genre NVARCHAR(100) = NULL,
@ISBN NVARCHAR(20) = NULL,
@Price DECIMAL(10,2),
@Stock INT = 0,
@PublishedDate DATE = NULL
AS
BEGIN
SET NOCOUNT ON;
INSERT INTO dbo.Books (Title, Author, Genre, ISBN, Price, Stock, PublishedDate)
VALUES (@Title, @Author, @Genre, @ISBN, @Price, @Stock, @PublishedDate);


SELECT SCOPE_IDENTITY() AS NewId;
END
GO

-- Update
CREATE OR ALTER PROCEDURE dbo.usp_UpdateBook
@Id INT,
@Title NVARCHAR(200),
@Author NVARCHAR(150),
@Genre NVARCHAR(100) = NULL,
@ISBN NVARCHAR(20) = NULL,
@Price DECIMAL(10,2),
@Stock INT,
@PublishedDate DATE = NULL
AS
BEGIN
SET NOCOUNT ON;
UPDATE dbo.Books
SET Title=@Title, Author=@Author, Genre=@Genre, ISBN=@ISBN,
Price=@Price, Stock=@Stock, PublishedDate=@PublishedDate
WHERE Id=@Id;
END
GO

-- Delete
CREATE OR ALTER PROCEDURE dbo.usp_DeleteBook
@Id INT
AS
BEGIN
SET NOCOUNT ON;
DELETE FROM dbo.Books WHERE Id=@Id;
END
GO

-- Get by Id
CREATE OR ALTER PROCEDURE dbo.usp_GetBookById
@Id INT
AS
BEGIN
SET NOCOUNT ON;
SELECT * FROM dbo.Books WHERE Id=@Id;
END
GO

-- Get all
CREATE OR ALTER PROCEDURE dbo.usp_GetAllBooks
AS
BEGIN
SET NOCOUNT ON;
SELECT * FROM dbo.Books ORDER BY CreatedAt DESC;
END
GO

-- Seed a few rows (optional)
INSERT INTO dbo.Books (Title, Author, Genre, ISBN, Price, Stock, PublishedDate)
VALUES
(N'The Pragmatic Programmer', N'Andrew Hunt', N'Tech', N'978-0201616224', 42.50, 10, '1999-10-20'),
(N'Clean Code', N'Robert C. Martin', N'Tech', N'978-0132350884', 39.99, 7, '2008-08-01');
GO