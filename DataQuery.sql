CREATE DATABASE QuanLyGarage
GO

USE QuanLyGarage
GO

-- Car
-- CarCategory
-- Suplier
-- Customer
-- Staff
-- Account
-- Inventory
-- Bill
-- BillInfo (id car, id category ...)
-- ReportExcel

CREATE TABLE Car (
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL DEFAULT N'CHƯA CÓ TÊN',
	idCategory INT NOT NULL,
	price FLOAT NOT NULL DEFAULT 0,
	status NVARCHAR(100) NOT NULL DEFAULT N'CHƯA BÁN',
	FOREIGN KEY (idCategory) REFERENCES CarCategory(id)
)
GO

CREATE TABLE CarCategory(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL DEFAULT N'CHƯA CÓ TÊN'
)
GO

CREATE TABLE Suplier(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL DEFAULT N'CHƯA CÓ TÊN',
)
GO

CREATE TABLE Customer (
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL DEFAULT N'CHƯA CÓ TÊN',
	sex BIT NOT NULL DEFAULT 0, -- 0: MALE, 1: FEMALE 
	DateOfBirth DATETIME NOT NULL,
	Address NVARCHAR(100) NOT NULL,
	Phone NVARCHAR(11) NOT NULL -- RÀNG BUỘC,
)
GO

CREATE TABLE Account (
	UserName NVARCHAR(100) NOT NULL PRIMARY KEY,
	DisplayName NVARCHAR(100) NOT NULL DEFAULT N'VIỆT',
	PassWord NVARCHAR(1000) NOT NULL DEFAULT 0, -- PASSWORD && MÃ HÓA PASS
	Type INT NOT NULL DEFAULT 1 -- DEFAULT ADMIN
)
GO

CREATE TABLE Inventory (
	id INT IDENTITY PRIMARY KEY NOT NULL,

)
GO

CREATE TABLE Bill(
	id INT IDENTITY PRIMARY KEY,
	DataCheckIn DATE NOT NULL DEFAULT GETDATE(),
	idCar INT NOT NULL,
	status INT NOT NULL DEFAULT 0, -- 1: ĐÃ THANH TOÁN && 0: CHƯA THANH TOÁN
	-- TOTAL PRICE 

	FOREIGN KEY (idCar) REFERENCES Car(id)
)
GO

CREATE TABLE BillInfo (
	id INT IDENTITY PRIMARY KEY,
	idBill INT NOT NULL,
	idCarCategory INT NOT NULL,
	count INT NOT NULL DEFAULT 0,

	FOREIGN KEY (idBill) REFERENCES Bill(id),
	FOREIGN KEY (idCarCategory) REFERENCES CarCategory(id)
)
GO

CREATE TABLE Staff (
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL,
	age INT NOT NULL,
	sex BIT NOT NULL DEFAULT 0, -- 0: MALE, -- 1: FEMALE
	phone CHAR(11) NOT NULL, -- RÀNG BUỘC
	position NVARCHAR(100) NOT NULL DEFAULT N'NHÂN VIÊN',
)
GO

INSERT INTO Account(
	UserName,
	DisplayName,
	PassWord,
	Type
)

VALUES (
	N'viet' , -- UserName - nvarchar(100)
	N'Việt', -- DisplayName - nvarchar(100)
	N'1', -- PassWord - nvarchar(1000)
	1 -- Type - int
)

CREATE PROC USP_GetAccountByUserName
@userName nvarchar(100)
AS 
BEGIN
	SELECT * FROM Account WHERE UserName = @userName
END
GO

select * from Account where UserName = N'viet' and PassWord = N'1'

EXEC USP_GetAccountByUserName @userName = N'viet'
GO

CREATE PROC USP_Login
@userName nvarchar(100) , @passWord nvarchar(100)
AS
BEGIN
	SELECT * FROM Account WHERE UserName = @userName AND PassWord = @passWord
END
GO

