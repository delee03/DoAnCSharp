create database QuanLyGarage
go

USE QuanLyGarage
GO

-- Car
-- OrderForm
-- OrderDetails

-- Suplier
-- Customer
-- Staff
-- Account
-- Inventory
-- Bill
-- BillInfo (id car, id category ...)
-- ReportExcel

CREATE TABLE Car (
	idCar NVARCHAR(10) PRIMARY KEY,
	nameCar NVARCHAR(100) NOT NULL,
	imageCar IMAGE,
	idSup int Default null,
	ngayNhap datetime,
	price FLOAT NOT NULL DEFAULT 0,
	
)
GO
alter table Car drop idDatHang;



CREATE TABLE Suplier(
	idSup INT PRIMARY KEY,
	nameSup NVARCHAR(100) NOT NULL DEFAULT N'CHƯA CÓ TÊN',
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
	Email NVARCHAR(100) NULL,
)
GO

CREATE TABLE Inventory (
	id INT IDENTITY PRIMARY KEY NOT NULL,
)
GO
Create table HoaDon(
	idHoaDon nvarchar(10) primary key,
	tenKH nvarchar(100) not null,
	sdt char(11) not null,
	tenNV nvarchar(50) not null,
	idCar nvarchar(10) not null,
	imageCar image default null,
	ngayLap datetime,
	FOREIGN KEY (idCar) REFERENCES Car(idCar)
)
go



CREATE TABLE Bill(
	id INT IDENTITY PRIMARY KEY,
	DataCheckIn DATE NOT NULL DEFAULT GETDATE(),
	idCar nvarchar(10) NOT NULL,
	status INT NOT NULL DEFAULT 0, -- 1: ĐÃ THANH TOÁN && 0: CHƯA THANH TOÁN
	-- TOTAL PRICE 

	FOREIGN KEY (idCar) REFERENCES Car(idCar)
)
GO
ALTER TABLE dbo.BillInfo drop CONSTRAINT FK_idBill;


CREATE TABLE BillInfo (
	id INT IDENTITY PRIMARY KEY,
	idBill INT NOT NULL,
	idSup INT NOT NULL,
	count INT NOT NULL DEFAULT 0,

	FOREIGN KEY (idBill) REFERENCES Bill(id),
	FOREIGN KEY (idSup) REFERENCES Suplier(idSup)
)
GO

CREATE TABLE Staff (
	id Nvarchar(10) PRIMARY KEY,
   Avatar_image image null,
	name NVARCHAR(100) NOT NULL,
	phone NvarCHAR(11) NOT NULL, 
	address nvarchar(100) not null
)
GO
drop table Staff
ALTER TABLE dbo.Car drop CONSTRAINT FK_idDatHang;
ALTER TABLE dbo.Car ADD CONSTRAINT FK_idSup FOREIGN KEY(idSup) REFERENCES dbo.Suplier(idSup)

insert into dbo.Suplier(idSup, nameSup)
values(1,'Mercedes'),(2, 'Porsche'),(3,'Toyota'),(4, 'Honda'),(5, 'Mazda');


Insert into dbo.Car (idCar, nameCar,imageCar, idSup, ngayNhap, price, idDatHang)
values('car01', 'C200', null,1, '2023-03-03', 1000000, 1),
		('car02', '911 Turbo',null, 2,'2023-04-03', 8888888, 0),
		('car03', 'CamryHyrid',null, 3, '2023-05-03', 2000000, 0),
		('car04', 'Civic', null,4, '2023-06-03', 800000, 0),
		('car05', 'CX5', null, 5, '2023-07-03', 750000, 2);
Insert into dbo.HoaDon(idHoaDon, tenKH,sdt , tenNV, idCar, imageCar, ngayLap)
values('hd1', 'NGuyen Van A', '0987654321', N'Viet', 'car01', NULL , 10/20/2023)


INSERT INTO Account(
	UserName,
	DisplayName,
	PassWord,
	Email,
)
VALUES (
	N'hoangviet' , -- UserName - nvarchar(100)
	N'Hoàng Việt', -- DisplayName - nvarchar(100)
	N'123', -- PassWord - nvarchar(1000)
	1 -- Type - int
),
 (
	N'thuanphat' , -- UserName - nvarchar(100)
	N'Thuận Phát', -- DisplayName - nvarchar(100)
	N'123', -- PassWord - nvarchar(1000)
	1 -- Type - int
),
(
	N'tiendat' , -- UserName - nvarchar(100)
	N'Tiến Đạt', -- DisplayName - nvarchar(100)
	N'123', -- PassWord - nvarchar(1000)
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
select * from Account

EXEC USP_Login @username = N'viet', @passWord = N'1'

select * from Account where Email = 'viet@gmail.com'
