USE [QuanLyGarage]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 10/26/2023 2:39:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[UserName] [nvarchar](100) NOT NULL,
	[DisplayName] [nvarchar](100) NOT NULL DEFAULT (N'VIỆT'),
	[PassWord] [nvarchar](1000) NOT NULL DEFAULT ((0)),
	[Email] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Bill]    Script Date: 10/26/2023 2:39:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bill](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[DataCheckIn] [date] NOT NULL,
	[idCar] [nvarchar](10) NOT NULL,
	[status] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BillInfo]    Script Date: 10/26/2023 2:39:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BillInfo](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idBill] [int] NOT NULL,
	[idCarSuplier] [int] NOT NULL,
	[count] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Car]    Script Date: 10/26/2023 2:39:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Car](
	[idCar] [nvarchar](10) NOT NULL,
	[nameCar] [nvarchar](100) NOT NULL,
	[imageCar] [image] NULL,
	[idSup] [int] NULL DEFAULT (NULL),
	[ngayNhap] [datetime] NULL,
	[price] [float] NOT NULL DEFAULT ((0)),
PRIMARY KEY CLUSTERED 
(
	[idCar] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Customer]    Script Date: 10/26/2023 2:39:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NOT NULL,
	[sex] [bit] NOT NULL,
	[DateOfBirth] [datetime] NOT NULL,
	[Address] [nvarchar](100) NOT NULL,
	[Phone] [nvarchar](11) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[HoaDon]    Script Date: 10/26/2023 2:39:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[HoaDon](
	[idHoaDon] [nvarchar](10) NOT NULL,
	[tenKH] [nvarchar](100) NOT NULL,
	[sdt] [char](11) NOT NULL,
	[tenNV] [nvarchar](50) NOT NULL,
	[idCar] [nvarchar](10) NOT NULL,
	[imageCar] [image] NULL DEFAULT (NULL),
	[ngayLap] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[idHoaDon] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Inventory]    Script Date: 10/26/2023 2:39:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Inventory](
	[id] [int] IDENTITY(1,1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Staff]    Script Date: 10/26/2023 2:39:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Staff](
	[id] [nvarchar](10) NOT NULL,
	[Avartar_image] [image] NULL,
	[name] [nvarchar](100) NOT NULL,
	[phone] [nvarchar](11) NOT NULL,
	[address] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Suplier]    Script Date: 10/26/2023 2:39:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Suplier](
	[idSup] [int] NOT NULL,
	[nameSup] [nvarchar](100) NOT NULL DEFAULT (N'CHƯA CÓ TÊN'),
PRIMARY KEY CLUSTERED 
(
	[idSup] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Bill] ADD  DEFAULT (getdate()) FOR [DataCheckIn]
GO
ALTER TABLE [dbo].[Bill] ADD  DEFAULT ((0)) FOR [status]
GO
ALTER TABLE [dbo].[BillInfo] ADD  DEFAULT ((0)) FOR [count]
GO
ALTER TABLE [dbo].[Customer] ADD  DEFAULT (N'CHƯA CÓ TÊN') FOR [name]
GO
ALTER TABLE [dbo].[Customer] ADD  DEFAULT ((0)) FOR [sex]
GO
ALTER TABLE [dbo].[Bill]  WITH CHECK ADD FOREIGN KEY([idCar])
REFERENCES [dbo].[Car] ([idCar])
GO
ALTER TABLE [dbo].[BillInfo]  WITH CHECK ADD FOREIGN KEY([idBill])
REFERENCES [dbo].[Bill] ([id])
GO
ALTER TABLE [dbo].[BillInfo]  WITH CHECK ADD FOREIGN KEY([idCarSuplier])
REFERENCES [dbo].[Suplier] ([idSup])
GO
ALTER TABLE [dbo].[Car]  WITH CHECK ADD  CONSTRAINT [FK_idSup] FOREIGN KEY([idSup])
REFERENCES [dbo].[Suplier] ([idSup])
GO
ALTER TABLE [dbo].[Car] CHECK CONSTRAINT [FK_idSup]
GO
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD FOREIGN KEY([idCar])
REFERENCES [dbo].[Car] ([idCar])
GO
/****** Object:  StoredProcedure [dbo].[USP_GetAccountByUserName]    Script Date: 10/26/2023 2:39:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_GetAccountByUserName]
@userName nvarchar(100)
AS 
BEGIN
	SELECT * FROM Account WHERE UserName = @userName
END

GO
/****** Object:  StoredProcedure [dbo].[USP_Login]    Script Date: 10/26/2023 2:39:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_Login]
@userName nvarchar(100) , @passWord nvarchar(100)
AS
BEGIN
	SELECT * FROM Account WHERE UserName = @userName AND PassWord = @passWord
END

GO
