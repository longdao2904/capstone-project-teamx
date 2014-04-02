USE [master]
GO
/****** Object:  Database [iDeliver]    Script Date: 04/02/2014 12:50:12 ******/
CREATE DATABASE [iDeliver] ON  PRIMARY 
( NAME = N'iDeliver', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\iDeliver.mdf' , SIZE = 2048KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'iDeliver_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\iDeliver_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [iDeliver] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [iDeliver].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [iDeliver] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [iDeliver] SET ANSI_NULLS OFF
GO
ALTER DATABASE [iDeliver] SET ANSI_PADDING OFF
GO
ALTER DATABASE [iDeliver] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [iDeliver] SET ARITHABORT OFF
GO
ALTER DATABASE [iDeliver] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [iDeliver] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [iDeliver] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [iDeliver] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [iDeliver] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [iDeliver] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [iDeliver] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [iDeliver] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [iDeliver] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [iDeliver] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [iDeliver] SET  DISABLE_BROKER
GO
ALTER DATABASE [iDeliver] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [iDeliver] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [iDeliver] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [iDeliver] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [iDeliver] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [iDeliver] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [iDeliver] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [iDeliver] SET  READ_WRITE
GO
ALTER DATABASE [iDeliver] SET RECOVERY SIMPLE
GO
ALTER DATABASE [iDeliver] SET  MULTI_USER
GO
ALTER DATABASE [iDeliver] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [iDeliver] SET DB_CHAINING OFF
GO
USE [iDeliver]
GO
/****** Object:  Table [dbo].[Route]    Script Date: 04/02/2014 12:50:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Route](
	[RouteID] [int] IDENTITY(1,1) NOT NULL,
	[RouteName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Route] PRIMARY KEY CLUSTERED 
(
	[RouteID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Route] ON
INSERT [dbo].[Route] ([RouteID], [RouteName]) VALUES (1, N'Sài Gòn - Vũng Tàu')
INSERT [dbo].[Route] ([RouteID], [RouteName]) VALUES (2, N'Sài Gòn - Đà Lạt')
INSERT [dbo].[Route] ([RouteID], [RouteName]) VALUES (3, N'Sài Gòn - Cần Thơ')
INSERT [dbo].[Route] ([RouteID], [RouteName]) VALUES (4, N'Sài Gòn - Nha Trang')
INSERT [dbo].[Route] ([RouteID], [RouteName]) VALUES (5, N'Sài Gòn - Cà Mau')
INSERT [dbo].[Route] ([RouteID], [RouteName]) VALUES (6, N'Sài Gòn - Bình Định')
INSERT [dbo].[Route] ([RouteID], [RouteName]) VALUES (7, N'Sài Gòn - Đà Nẵng')
INSERT [dbo].[Route] ([RouteID], [RouteName]) VALUES (8, N'Vũng Tàu - Sài Gòn')
INSERT [dbo].[Route] ([RouteID], [RouteName]) VALUES (9, N'Nha Trang - Đà Nẵng')
INSERT [dbo].[Route] ([RouteID], [RouteName]) VALUES (10, N'Đà Nẵng - Nha Trang')
INSERT [dbo].[Route] ([RouteID], [RouteName]) VALUES (11, N'Vũng Tàu - Nha Trang')
INSERT [dbo].[Route] ([RouteID], [RouteName]) VALUES (12, N'Nha Trang - Vũng Tàu')
INSERT [dbo].[Route] ([RouteID], [RouteName]) VALUES (13, N'Nha Trang - Sài Gòn')
INSERT [dbo].[Route] ([RouteID], [RouteName]) VALUES (14, N'Cần Thơ - Sài Gòn')
INSERT [dbo].[Route] ([RouteID], [RouteName]) VALUES (15, N'Đà Lạt - Sài Gòn')
INSERT [dbo].[Route] ([RouteID], [RouteName]) VALUES (16, N'Cà Mau - Cần Thơ')
INSERT [dbo].[Route] ([RouteID], [RouteName]) VALUES (17, N'Đà Lạt - Cần Thơ')
INSERT [dbo].[Route] ([RouteID], [RouteName]) VALUES (18, N'Đà Lạt - Đà Nẵng')
INSERT [dbo].[Route] ([RouteID], [RouteName]) VALUES (19, N'Đà Lạt - Nha Trang')
INSERT [dbo].[Route] ([RouteID], [RouteName]) VALUES (20, N'Nha Trang - Đà Lạt')
INSERT [dbo].[Route] ([RouteID], [RouteName]) VALUES (22, N'Cần Thơ - Cà Mau')
INSERT [dbo].[Route] ([RouteID], [RouteName]) VALUES (23, N'Cần Thơ - Đà Lạt')
INSERT [dbo].[Route] ([RouteID], [RouteName]) VALUES (24, N'Cà Mau - Sài Gòn')
INSERT [dbo].[Route] ([RouteID], [RouteName]) VALUES (25, N'Cà Mau - Đà Lạt')
INSERT [dbo].[Route] ([RouteID], [RouteName]) VALUES (26, N'Đà Lạt - Quy Nhơn')
SET IDENTITY_INSERT [dbo].[Route] OFF
/****** Object:  Table [dbo].[CoachType]    Script Date: 04/02/2014 12:50:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CoachType](
	[CoachTypeID] [int] IDENTITY(1,1) NOT NULL,
	[Seats] [int] NOT NULL,
	[Capacity] [float] NOT NULL,
 CONSTRAINT [PK_CoachType] PRIMARY KEY CLUSTERED 
(
	[CoachTypeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[CoachType] ON
INSERT [dbo].[CoachType] ([CoachTypeID], [Seats], [Capacity]) VALUES (1, 16, 1000)
INSERT [dbo].[CoachType] ([CoachTypeID], [Seats], [Capacity]) VALUES (2, 29, 2500)
INSERT [dbo].[CoachType] ([CoachTypeID], [Seats], [Capacity]) VALUES (3, 35, 3000)
INSERT [dbo].[CoachType] ([CoachTypeID], [Seats], [Capacity]) VALUES (4, 40, 4500)
INSERT [dbo].[CoachType] ([CoachTypeID], [Seats], [Capacity]) VALUES (5, 45, 5800)
INSERT [dbo].[CoachType] ([CoachTypeID], [Seats], [Capacity]) VALUES (6, 52, 6000)
SET IDENTITY_INSERT [dbo].[CoachType] OFF
/****** Object:  Table [dbo].[ManageFee]    Script Date: 04/02/2014 12:50:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ManageFee](
	[FeeID] [int] IDENTITY(1,1) NOT NULL,
	[Fee] [float] NOT NULL,
	[MinWeight] [float] NOT NULL,
	[MaxWeight] [float] NOT NULL,
	[MinVolume] [float] NOT NULL,
	[MaxVolume] [float] NOT NULL,
 CONSTRAINT [PK_ManageFee] PRIMARY KEY CLUSTERED 
(
	[FeeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[ManageFee] ON
INSERT [dbo].[ManageFee] ([FeeID], [Fee], [MinWeight], [MaxWeight], [MinVolume], [MaxVolume]) VALUES (1, 20000, 0.1, 3.5, 0.1, 3.5)
INSERT [dbo].[ManageFee] ([FeeID], [Fee], [MinWeight], [MaxWeight], [MinVolume], [MaxVolume]) VALUES (2, 27000, 5, 8, 5, 8)
INSERT [dbo].[ManageFee] ([FeeID], [Fee], [MinWeight], [MaxWeight], [MinVolume], [MaxVolume]) VALUES (3, 35000, 10, 18, 10, 18)
INSERT [dbo].[ManageFee] ([FeeID], [Fee], [MinWeight], [MaxWeight], [MinVolume], [MaxVolume]) VALUES (4, 42000, 20, 27.5, 20, 28)
INSERT [dbo].[ManageFee] ([FeeID], [Fee], [MinWeight], [MaxWeight], [MinVolume], [MaxVolume]) VALUES (5, 50000, 30, 40, 30, 42)
INSERT [dbo].[ManageFee] ([FeeID], [Fee], [MinWeight], [MaxWeight], [MinVolume], [MaxVolume]) VALUES (6, 60000, 42, 52, 45, 55)
INSERT [dbo].[ManageFee] ([FeeID], [Fee], [MinWeight], [MaxWeight], [MinVolume], [MaxVolume]) VALUES (7, 72000, 55, 65, 57, 65)
INSERT [dbo].[ManageFee] ([FeeID], [Fee], [MinWeight], [MaxWeight], [MinVolume], [MaxVolume]) VALUES (8, 90000, 67, 78, 67, 75)
INSERT [dbo].[ManageFee] ([FeeID], [Fee], [MinWeight], [MaxWeight], [MinVolume], [MaxVolume]) VALUES (9, 100000, 80, 88, 77, 85)
INSERT [dbo].[ManageFee] ([FeeID], [Fee], [MinWeight], [MaxWeight], [MinVolume], [MaxVolume]) VALUES (10, 115000, 90, 98, 87, 100)
SET IDENTITY_INSERT [dbo].[ManageFee] OFF
/****** Object:  Table [dbo].[Province]    Script Date: 04/02/2014 12:50:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Province](
	[ProvinceID] [int] IDENTITY(1,1) NOT NULL,
	[ProvinceName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Province] PRIMARY KEY CLUSTERED 
(
	[ProvinceID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Province] ON
INSERT [dbo].[Province] ([ProvinceID], [ProvinceName]) VALUES (1, N'Hồ Chí Minh')
INSERT [dbo].[Province] ([ProvinceID], [ProvinceName]) VALUES (2, N'Vũng Tàu')
INSERT [dbo].[Province] ([ProvinceID], [ProvinceName]) VALUES (3, N'Đà Lạt')
INSERT [dbo].[Province] ([ProvinceID], [ProvinceName]) VALUES (4, N'Nha Trang')
INSERT [dbo].[Province] ([ProvinceID], [ProvinceName]) VALUES (5, N'Đà Nẵng ')
INSERT [dbo].[Province] ([ProvinceID], [ProvinceName]) VALUES (6, N'Bình Định')
INSERT [dbo].[Province] ([ProvinceID], [ProvinceName]) VALUES (7, N'Cần Thơ')
INSERT [dbo].[Province] ([ProvinceID], [ProvinceName]) VALUES (8, N'Cà Mau')
SET IDENTITY_INSERT [dbo].[Province] OFF
/****** Object:  Table [dbo].[DeliveryStatus]    Script Date: 04/02/2014 12:50:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeliveryStatus](
	[DeliveryStatusID] [int] IDENTITY(1,1) NOT NULL,
	[StatusName] [nvarchar](50) NOT NULL,
	[Note] [nvarchar](500) NULL,
 CONSTRAINT [PK_DeliveryStatus] PRIMARY KEY CLUSTERED 
(
	[DeliveryStatusID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[DeliveryStatus] ON
INSERT [dbo].[DeliveryStatus] ([DeliveryStatusID], [StatusName], [Note]) VALUES (1, N'Chờ xác nhận', N'Chờ xác nhận')
INSERT [dbo].[DeliveryStatus] ([DeliveryStatusID], [StatusName], [Note]) VALUES (2, N'Đã xác nhận', N'Đã xác nhận')
INSERT [dbo].[DeliveryStatus] ([DeliveryStatusID], [StatusName], [Note]) VALUES (3, N'Đã xếp lịch', N'Đã xếp lịch')
INSERT [dbo].[DeliveryStatus] ([DeliveryStatusID], [StatusName], [Note]) VALUES (4, N'Đã chuyển đi', N'Gói hàng đã rời trạm, đang trên đường vận chuyển')
INSERT [dbo].[DeliveryStatus] ([DeliveryStatusID], [StatusName], [Note]) VALUES (5, N'Đã đến đích', N'Gói hàng đã vận chuyển đến trạm đích')
INSERT [dbo].[DeliveryStatus] ([DeliveryStatusID], [StatusName], [Note]) VALUES (6, N'Đã giao hàng', N'Gói hàng đã được giao cho người nhận')
INSERT [dbo].[DeliveryStatus] ([DeliveryStatusID], [StatusName], [Note]) VALUES (7, N'Đã hủy', N'Yêu cầu đã bị hủy bởi người gửi/nhân viên')
INSERT [dbo].[DeliveryStatus] ([DeliveryStatusID], [StatusName], [Note]) VALUES (8, N'Đã hết hạn', N'Yêu cầu đã hết hạn chờ xử lý')
INSERT [dbo].[DeliveryStatus] ([DeliveryStatusID], [StatusName], [Note]) VALUES (9, N'Trễ hạn thanh toán', N'Hết thời gian thanh toán')
INSERT [dbo].[DeliveryStatus] ([DeliveryStatusID], [StatusName], [Note]) VALUES (10, N'Đang thanh toán', N'Khách hàng đang thanh toán')
SET IDENTITY_INSERT [dbo].[DeliveryStatus] OFF
/****** Object:  Table [dbo].[Coach]    Script Date: 04/02/2014 12:50:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Coach](
	[CoachID] [int] IDENTITY(1,1) NOT NULL,
	[NumberPlate] [nvarchar](50) NOT NULL,
	[CoachTypeID] [int] NOT NULL,
 CONSTRAINT [PK_Coach] PRIMARY KEY CLUSTERED 
(
	[CoachID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Coach] ON
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (1, N'51B-34572', 3)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (2, N'51B-34573', 3)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (3, N'51B-34574', 3)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (4, N'51B-34575', 3)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (5, N'51B-34576', 3)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (6, N'51B-34577', 3)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (7, N'51B-34578', 3)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (8, N'51B-34579', 3)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (9, N'51B-34580', 3)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (10, N'51B-34581', 3)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (11, N'51B-34582', 3)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (12, N'51B-34583', 3)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (13, N'51B-34584', 3)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (14, N'51B-34585', 3)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (15, N'51B-34586', 3)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (16, N'51B-34587', 3)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (17, N'51B-34588', 3)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (18, N'51B-34589', 3)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (19, N'51B-34590', 3)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (20, N'51B-34591', 3)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (21, N'51B-34592', 3)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (22, N'51B-34593', 3)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (23, N'51B-34594', 3)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (24, N'51B-34595', 3)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (25, N'51B-34596', 3)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (26, N'51B-34597', 3)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (27, N'51B-34598', 3)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (28, N'51C-45683', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (29, N'51C-45684', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (30, N'51C-45685', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (31, N'51C-45686', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (32, N'51C-45687', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (33, N'51C-45688', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (34, N'51C-45689', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (35, N'51C-45690', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (36, N'51C-45691', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (37, N'51C-45692', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (38, N'51C-45693', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (39, N'51C-45694', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (40, N'51C-45695', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (41, N'51C-45696', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (42, N'51C-45697', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (43, N'51C-45698', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (44, N'51C-45699', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (45, N'51C-45700', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (46, N'51C-45701', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (47, N'51C-45702', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (48, N'51C-45703', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (49, N'51C-45704', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (50, N'51C-45705', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (51, N'51C-45706', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (52, N'51C-45707', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (53, N'51C-45708', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (54, N'51C-45709', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (55, N'51C-45710', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (56, N'51C-45711', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (57, N'51C-45712', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (58, N'51C-45713', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (59, N'51C-45714', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (60, N'51C-45715', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (61, N'51C-45716', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (62, N'51C-45717', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (63, N'51C-45718', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (64, N'51C-45719', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (65, N'51C-45720', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (66, N'51C-45721', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (67, N'51C-45722', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (68, N'51C-45723', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (69, N'51C-45724', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (70, N'51C-45725', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (71, N'51C-45726', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (72, N'51C-45727', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (73, N'51C-45728', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (74, N'51C-45729', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (75, N'51C-45730', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (76, N'51C-45731', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (77, N'51C-45732', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (78, N'51C-45733', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (79, N'51C-45734', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (80, N'51C-45735', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (81, N'51C-45736', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (82, N'51C-45737', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (83, N'51C-45738', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (84, N'51C-45739', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (85, N'51C-45740', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (86, N'51C-45741', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (87, N'51C-45742', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (88, N'51C-45743', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (89, N'51C-45744', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (90, N'51D-56794', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (91, N'51D-56795', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (92, N'51D-56796', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (93, N'51D-56797', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (94, N'51D-56798', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (95, N'51D-56799', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (96, N'51D-56800', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (97, N'51D-56801', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (98, N'51D-56802', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (99, N'51D-56803', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (100, N'51D-56804', 5)
GO
print 'Processed 100 total records'
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (101, N'51D-56805', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (102, N'51D-56806', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (103, N'51D-56807', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (104, N'51D-56808', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (105, N'51D-56809', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (106, N'51D-56810', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (107, N'51D-56811', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (108, N'51D-56812', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (109, N'51D-56813', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (110, N'51D-56814', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (111, N'51D-56815', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (112, N'51D-56816', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (113, N'51D-56817', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (114, N'51D-56818', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (115, N'51D-56819', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (116, N'51D-56820', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (117, N'51D-56821', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (118, N'51D-56822', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (119, N'51D-56823', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (120, N'51D-56824', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (121, N'51D-56825', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (122, N'51D-56826', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (123, N'51D-56827', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (124, N'51D-56828', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (125, N'51D-56829', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (126, N'51D-56830', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (127, N'51D-56831', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (128, N'51D-56832', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (129, N'51D-56833', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (130, N'51D-56834', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (131, N'51D-56835', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (132, N'51D-56836', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (133, N'51D-56837', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (134, N'51D-56838', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (135, N'51D-56839', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (136, N'51D-56840', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (137, N'51D-56841', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (138, N'51D-56842', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (139, N'51D-56843', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (140, N'51D-56844', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (141, N'51D-56845', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (142, N'51D-56846', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (143, N'51D-56847', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (144, N'51D-56848', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (145, N'51D-56849', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (146, N'51D-56850', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (147, N'51D-56851', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (148, N'51D-56852', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (149, N'51D-56853', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (150, N'51D-56854', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (151, N'51D-56855', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (152, N'51D-56856', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (153, N'51D-56857', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (154, N'51D-56858', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (155, N'51D-56859', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (156, N'51D-56860', 5)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (157, N'51D-52211', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (158, N'51D-52212', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (159, N'51D-52213', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (160, N'51D-52214', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (161, N'51D-52215', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (162, N'51D-51901', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (163, N'51D-51902', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (164, N'51D-51903', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (165, N'51D-51904', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (166, N'51D-51905', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (167, N'51D-51201', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (168, N'51D-51202', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (169, N'51D-51203', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (170, N'51D-51204', 4)
INSERT [dbo].[Coach] ([CoachID], [NumberPlate], [CoachTypeID]) VALUES (171, N'51D-51205', 4)
SET IDENTITY_INSERT [dbo].[Coach] OFF
/****** Object:  Table [dbo].[Station]    Script Date: 04/02/2014 12:50:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Station](
	[StationID] [int] IDENTITY(1,1) NOT NULL,
	[StationName] [nvarchar](50) NOT NULL,
	[StationLocation] [nvarchar](500) NOT NULL,
	[StationPhone] [varchar](15) NULL,
	[Coordinate] [nvarchar](500) NOT NULL,
	[ProvinceID] [int] NOT NULL,
	[AvgRatingLevel] [float] NULL,
 CONSTRAINT [PK_Station] PRIMARY KEY CLUSTERED 
(
	[StationID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Station] ON
INSERT [dbo].[Station] ([StationID], [StationName], [StationLocation], [StationPhone], [Coordinate], [ProvinceID], [AvgRatingLevel]) VALUES (1, N'Lê Hồng Phong - Sài Gòn', N'233 Lê Hồng Phong, F4, Q.5, TP Hồ Chí Minh', N'(08) 38 309 309', N'10.7584848, 106.6777557', 1, NULL)
INSERT [dbo].[Station] ([StationID], [StationName], [StationLocation], [StationPhone], [Coordinate], [ProvinceID], [AvgRatingLevel]) VALUES (2, N'Tạ Uyên - Vũng Tàu', N'03 Tạ Uyên, Vũng Tàu', N'0643 52 53 54', N'10.3491804, 107.0855695', 2, NULL)
INSERT [dbo].[Station] ([StationID], [StationName], [StationLocation], [StationPhone], [Coordinate], [ProvinceID], [AvgRatingLevel]) VALUES (3, N'Bến xe liên tỉnh Đà Lạt', N'01 Tô Hiến Thành, F3, Đà Lạt', N'0633 58 58 58', N'11.9264949, 108.4460601', 3, NULL)
INSERT [dbo].[Station] ([StationID], [StationName], [StationLocation], [StationPhone], [Coordinate], [ProvinceID], [AvgRatingLevel]) VALUES (4, N'Bến xe Phía Nam Nha Trang', N'Số 58, đường 23/10, Xã Vĩnh Hiệp , Nha Trang, Tỉnh Khánh Hòa ', N'(058) 3562626', N'12.2527081, 109.1627343', 4, NULL)
INSERT [dbo].[Station] ([StationID], [StationName], [StationLocation], [StationPhone], [Coordinate], [ProvinceID], [AvgRatingLevel]) VALUES (5, N'Bến xe Miền Tây - Sài Gòn', N'395 Kinh Dương Vương, phường An Lạc, Quận Bình Tân, Hồ Chí Minh', N'(08) 38776594', N'10.740993, 106.617980', 1, NULL)
INSERT [dbo].[Station] ([StationID], [StationName], [StationLocation], [StationPhone], [Coordinate], [ProvinceID], [AvgRatingLevel]) VALUES (6, N'Bến xe Miền Đông - Sài Gòn', N'367 Đinh Bộ Lĩnh, 26, Bình Thạnh, Hồ Chí Minh', N'08.38 309 309 ', N'10.815213, 106.710674', 1, NULL)
INSERT [dbo].[Station] ([StationID], [StationName], [StationLocation], [StationPhone], [Coordinate], [ProvinceID], [AvgRatingLevel]) VALUES (7, N'Bến xe Hùng Vương - Cần Thơ', N'01 Hùng Vương, Thới Bình, Ninh Kiều, TP.Cần Thơ ', N'(0710)3769768', N'10.0449994, 105.7799216', 7, NULL)
INSERT [dbo].[Station] ([StationID], [StationName], [StationLocation], [StationPhone], [Coordinate], [ProvinceID], [AvgRatingLevel]) VALUES (8, N'Bến Xe Trung Tâm Đà Nẵng', N'201 Tôn Đức Thắng, Phường Hòa Minh, Liên Chiểu, TP.Đà Nẵng', N'(0511) 3786 786', N'16.051571, 108.214897', 5, NULL)
INSERT [dbo].[Station] ([StationID], [StationName], [StationLocation], [StationPhone], [Coordinate], [ProvinceID], [AvgRatingLevel]) VALUES (9, N'Bến xe Quy Nhơn', N'Bến xe Quy Nhơn, Quy Nhơn, Bình Định', N'0562242424', N'13.753419, 109.2095464', 6, NULL)
INSERT [dbo].[Station] ([StationID], [StationName], [StationLocation], [StationPhone], [Coordinate], [ProvinceID], [AvgRatingLevel]) VALUES (10, N'Bến xe Cà Mau', N'Bến xe Cà Mau, QL 1, Phường 6, Cà Mau, Cà Mau, Vietnam', N'07803651651', N'9.1760935, 105.1706709', 8, NULL)
SET IDENTITY_INSERT [dbo].[Station] OFF
/****** Object:  Table [dbo].[Stage]    Script Date: 04/02/2014 12:50:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stage](
	[StageID] [int] IDENTITY(1,1) NOT NULL,
	[StageName] [nvarchar](50) NULL,
	[StartPoint] [int] NULL,
	[EndPoint] [int] NULL,
	[BreakTime] [time](0) NULL,
	[Duration] [float] NULL,
	[Distance] [float] NULL,
	[Container] [float] NULL,
	[Price] [float] NULL,
 CONSTRAINT [PK_Stage] PRIMARY KEY CLUSTERED 
(
	[StageID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Stage] ON
INSERT [dbo].[Stage] ([StageID], [StageName], [StartPoint], [EndPoint], [BreakTime], [Duration], [Distance], [Container], [Price]) VALUES (1, N'Sài Gòn - Vũng Tàu', 1, 2, CAST(0x00100E0000000000 AS Time), 3, 125, 0.8, 200)
INSERT [dbo].[Stage] ([StageID], [StageName], [StartPoint], [EndPoint], [BreakTime], [Duration], [Distance], [Container], [Price]) VALUES (2, N'Sài Gòn - Đà Lạt', 1, 3, CAST(0x00100E0000000000 AS Time), 8.15, 308, 0.55, 300)
INSERT [dbo].[Stage] ([StageID], [StageName], [StartPoint], [EndPoint], [BreakTime], [Duration], [Distance], [Container], [Price]) VALUES (3, N'Sài Gòn - Cần Thơ', 5, 7, CAST(0x00100E0000000000 AS Time), 3, 168, 0.68, 520)
INSERT [dbo].[Stage] ([StageID], [StageName], [StartPoint], [EndPoint], [BreakTime], [Duration], [Distance], [Container], [Price]) VALUES (4, N'Sài Gòn - Nha Trang', 6, 4, CAST(0x00201C0000000000 AS Time), 8.5, 488, 0.35, 620)
INSERT [dbo].[Stage] ([StageID], [StageName], [StartPoint], [EndPoint], [BreakTime], [Duration], [Distance], [Container], [Price]) VALUES (5, N'Sài Gòn - Cà Mau', 5, 10, CAST(0x00100E0000000000 AS Time), 6.5, 350, 0.55, 800)
INSERT [dbo].[Stage] ([StageID], [StageName], [StartPoint], [EndPoint], [BreakTime], [Duration], [Distance], [Container], [Price]) VALUES (6, N'Sài Gòn - Bình Định', 6, 9, CAST(0x00100E0000000000 AS Time), 12, 686, 0.2, 900)
INSERT [dbo].[Stage] ([StageID], [StageName], [StartPoint], [EndPoint], [BreakTime], [Duration], [Distance], [Container], [Price]) VALUES (7, N'Sài Gòn - Đà Nẵng', 6, 8, CAST(0x00201C0000000000 AS Time), 20, 964, 0.2, 250)
INSERT [dbo].[Stage] ([StageID], [StageName], [StartPoint], [EndPoint], [BreakTime], [Duration], [Distance], [Container], [Price]) VALUES (8, N'Vũng Tàu - Sài Gòn', 2, 1, CAST(0x00100E0000000000 AS Time), 2, 125, 0.8, 200)
INSERT [dbo].[Stage] ([StageID], [StageName], [StartPoint], [EndPoint], [BreakTime], [Duration], [Distance], [Container], [Price]) VALUES (9, N'Nha Trang - Đà Nẵng', 4, 8, CAST(0x00201C0000000000 AS Time), 2, 521, 0.28, 550)
INSERT [dbo].[Stage] ([StageID], [StageName], [StartPoint], [EndPoint], [BreakTime], [Duration], [Distance], [Container], [Price]) VALUES (10, N'Đà Nẵng - Nha Trang', 8, 4, CAST(0x00201C0000000000 AS Time), 2, 521, 0.28, 720)
INSERT [dbo].[Stage] ([StageID], [StageName], [StartPoint], [EndPoint], [BreakTime], [Duration], [Distance], [Container], [Price]) VALUES (11, N'Vũng Tàu - Nha Trang', 2, 4, CAST(0x0018150000000000 AS Time), 2, 513, 0.28, 750)
INSERT [dbo].[Stage] ([StageID], [StageName], [StartPoint], [EndPoint], [BreakTime], [Duration], [Distance], [Container], [Price]) VALUES (12, N'Nha Trang - Vũng Tàu', 4, 2, CAST(0x00100E0000000000 AS Time), 2, 513, 0.28, 950)
INSERT [dbo].[Stage] ([StageID], [StageName], [StartPoint], [EndPoint], [BreakTime], [Duration], [Distance], [Container], [Price]) VALUES (13, N'Nha Trang - Sài Gòn', 4, 6, CAST(0x00201C0000000000 AS Time), 2, 441, 0.35, 800)
INSERT [dbo].[Stage] ([StageID], [StageName], [StartPoint], [EndPoint], [BreakTime], [Duration], [Distance], [Container], [Price]) VALUES (14, N'Cần Thơ - Sài Gòn', 7, 5, CAST(0x00100E0000000000 AS Time), 3, 167, 0.68, 900)
INSERT [dbo].[Stage] ([StageID], [StageName], [StartPoint], [EndPoint], [BreakTime], [Duration], [Distance], [Container], [Price]) VALUES (15, N'Đà Lạt - Sài Gòn', 3, 1, CAST(0x00100E0000000000 AS Time), 8.15, 293, 0.55, 250)
INSERT [dbo].[Stage] ([StageID], [StageName], [StartPoint], [EndPoint], [BreakTime], [Duration], [Distance], [Container], [Price]) VALUES (16, N'Cà Mau - Cần Thơ', 10, 7, CAST(0x0018150000000000 AS Time), 4.5, 180, 0.68, 520)
INSERT [dbo].[Stage] ([StageID], [StageName], [StartPoint], [EndPoint], [BreakTime], [Duration], [Distance], [Container], [Price]) VALUES (17, N'Đà Lạt - Cần Thơ', 3, 7, CAST(0x00100E0000000000 AS Time), 8.15, 293, 0.55, 250)
INSERT [dbo].[Stage] ([StageID], [StageName], [StartPoint], [EndPoint], [BreakTime], [Duration], [Distance], [Container], [Price]) VALUES (18, N'Đà Lạt - Đà Nẵng', 3, 8, CAST(0x00100E0000000000 AS Time), 8.15, 293, 0.55, 250)
INSERT [dbo].[Stage] ([StageID], [StageName], [StartPoint], [EndPoint], [BreakTime], [Duration], [Distance], [Container], [Price]) VALUES (19, N'Đà Lạt - Nha Trang', 3, 4, CAST(0x00100E0000000000 AS Time), 8.15, 293, 0.55, 250)
INSERT [dbo].[Stage] ([StageID], [StageName], [StartPoint], [EndPoint], [BreakTime], [Duration], [Distance], [Container], [Price]) VALUES (20, N'Nha Trang - Đà Lạt', 4, 3, CAST(0x00100E0000000000 AS Time), 8.15, 293, 0.55, 250)
INSERT [dbo].[Stage] ([StageID], [StageName], [StartPoint], [EndPoint], [BreakTime], [Duration], [Distance], [Container], [Price]) VALUES (22, N'Cần Thơ - Cà Mau', 7, 10, CAST(0x00100E0000000000 AS Time), 3, 180, 0.68, 900)
INSERT [dbo].[Stage] ([StageID], [StageName], [StartPoint], [EndPoint], [BreakTime], [Duration], [Distance], [Container], [Price]) VALUES (23, N'Cần Thơ - Đà Lạt', 7, 3, CAST(0x00100E0000000000 AS Time), 3, 180, 0.68, 900)
INSERT [dbo].[Stage] ([StageID], [StageName], [StartPoint], [EndPoint], [BreakTime], [Duration], [Distance], [Container], [Price]) VALUES (24, N'Sài Gòn - Đà Lạt', 5, 3, CAST(0x00100E0000000000 AS Time), 3, 150, 0.7, 900)
INSERT [dbo].[Stage] ([StageID], [StageName], [StartPoint], [EndPoint], [BreakTime], [Duration], [Distance], [Container], [Price]) VALUES (25, N'Đà Lạt - Quy Nhơn', 3, 9, CAST(0x00100E0000000000 AS Time), 3, 100, 0.7, 900)
INSERT [dbo].[Stage] ([StageID], [StageName], [StartPoint], [EndPoint], [BreakTime], [Duration], [Distance], [Container], [Price]) VALUES (26, N'Quy Nhơn - Đà Nẵng', 9, 8, CAST(0x00100E0000000000 AS Time), 3, 100, 0.7, 900)
SET IDENTITY_INSERT [dbo].[Stage] OFF
/****** Object:  Table [dbo].[CoachArrangement]    Script Date: 04/02/2014 12:50:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CoachArrangement](
	[ArrangeID] [int] IDENTITY(1,1) NOT NULL,
	[CoachID] [int] NULL,
	[RouteID] [int] NULL,
 CONSTRAINT [PK_CoachArrangement] PRIMARY KEY CLUSTERED 
(
	[ArrangeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[CoachArrangement] ON
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (1, 1, 1)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (3, 2, 1)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (4, 3, 1)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (5, 4, 1)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (6, 5, 1)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (7, 6, 1)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (8, 7, 1)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (9, 8, 1)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (10, 9, 1)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (11, 10, 1)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (12, 11, 1)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (13, 12, 1)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (14, 13, 1)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (15, 14, 1)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (16, 15, 16)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (17, 16, 16)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (18, 17, 16)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (19, 18, 16)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (20, 19, 16)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (21, 20, 16)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (22, 21, 16)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (23, 22, 16)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (24, 23, 16)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (25, 24, 16)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (26, 25, 16)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (27, 26, 16)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (28, 27, 16)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (29, 28, 2)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (30, 29, 2)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (31, 30, 2)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (32, 31, 2)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (33, 32, 2)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (34, 33, 2)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (35, 34, 2)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (36, 35, 2)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (37, 36, 2)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (38, 37, 2)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (39, 38, 2)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (40, 39, 2)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (41, 40, 2)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (42, 41, 2)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (43, 42, 2)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (44, 43, 2)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (45, 44, 2)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (46, 45, 7)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (47, 46, 7)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (48, 47, 7)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (49, 48, 7)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (50, 49, 7)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (51, 50, 4)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (52, 51, 4)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (53, 52, 4)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (54, 53, 4)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (55, 54, 4)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (56, 55, 4)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (57, 56, 4)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (58, 57, 4)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (59, 58, 2)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (60, 59, 2)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (61, 60, 2)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (62, 61, 17)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (63, 62, 17)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (64, 63, 18)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (65, 64, 18)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (66, 65, 18)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (67, 66, 18)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (68, 67, 23)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (69, 68, 23)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (70, 69, 13)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (71, 70, 13)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (72, 71, 13)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (73, 72, 13)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (74, 73, 13)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (75, 74, 13)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (76, 75, 13)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (77, 76, 5)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (78, 77, 5)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (79, 78, 5)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (80, 79, 5)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (81, 80, 5)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (82, 81, 5)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (83, 82, 6)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (84, 83, 6)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (85, 84, 6)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (86, 85, 6)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (87, 86, 5)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (88, 87, 5)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (89, 88, 5)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (90, 89, 5)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (91, 90, 3)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (92, 91, 3)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (93, 92, 3)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (94, 93, 3)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (95, 94, 3)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (96, 95, 3)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (97, 96, 3)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (98, 97, 3)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (99, 98, 3)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (100, 99, 3)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (101, 100, 3)
GO
print 'Processed 100 total records'
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (102, 101, 3)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (103, 102, 3)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (104, 103, 3)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (105, 104, 3)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (106, 105, 3)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (107, 106, 3)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (108, 107, 3)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (109, 108, 3)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (110, 109, 3)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (111, 110, 19)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (112, 111, 19)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (113, 112, 19)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (114, 113, 19)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (115, 114, 19)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (116, 115, 19)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (117, 116, 19)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (118, 117, 19)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (119, 118, 14)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (120, 119, 14)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (121, 120, 14)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (122, 121, 14)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (123, 122, 14)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (124, 123, 14)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (125, 124, 14)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (126, 125, 14)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (127, 126, 14)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (128, 127, 14)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (129, 128, 14)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (130, 129, 14)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (131, 130, 14)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (132, 131, 14)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (133, 132, 14)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (134, 133, 14)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (135, 134, 14)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (136, 135, 14)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (137, 136, 14)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (138, 137, 14)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (139, 138, 20)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (140, 139, 20)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (141, 140, 20)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (142, 141, 20)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (143, 142, 20)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (144, 143, 20)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (145, 144, 20)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (146, 145, 22)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (147, 146, 22)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (148, 147, 22)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (149, 148, 22)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (150, 149, 22)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (151, 150, 22)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (152, 151, 22)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (153, 152, 22)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (154, 153, 22)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (155, 154, 22)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (156, 155, 22)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (157, 156, 22)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (158, 157, 15)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (159, 158, 15)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (160, 159, 15)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (161, 160, 15)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (162, 161, 15)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (163, 162, 19)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (164, 163, 19)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (165, 164, 19)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (166, 165, 19)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (167, 166, 19)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (168, 167, 12)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (169, 168, 12)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (170, 169, 12)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (171, 170, 12)
INSERT [dbo].[CoachArrangement] ([ArrangeID], [CoachID], [RouteID]) VALUES (172, 171, 12)
SET IDENTITY_INSERT [dbo].[CoachArrangement] OFF
/****** Object:  Table [dbo].[Account]    Script Date: 04/02/2014 12:50:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[BackupPassword] [nvarchar](50) NULL,
	[Role] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](50) NOT NULL,
	[BannedStatus] [bit] NULL,
	[StationID] [int] NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Account] ([Username], [Password], [BackupPassword], [Role], [Email], [Phone], [BannedStatus], [StationID]) VALUES (N'billgates', N'billgates', N'billgates', N'Customer', N'billgates@microsoft.com', N'01234567890', 0, 1)
INSERT [dbo].[Account] ([Username], [Password], [BackupPassword], [Role], [Email], [Phone], [BannedStatus], [StationID]) VALUES (N'customer1', N'customer1', N'customer1', N'Customer', N'customer1@gmail.com', N'0906425539', 0, 1)
INSERT [dbo].[Account] ([Username], [Password], [BackupPassword], [Role], [Email], [Phone], [BannedStatus], [StationID]) VALUES (N'duc', N'abc', N'abc', N'Staff', N'duc@yahoo.com', N'09125934873', 0, 1)
INSERT [dbo].[Account] ([Username], [Password], [BackupPassword], [Role], [Email], [Phone], [BannedStatus], [StationID]) VALUES (N'long', N'aaaa', N'aaaa', N'Admin', N'vuhoanglong1992.lv@gmail.com', N'0123456789', 0, 1)
INSERT [dbo].[Account] ([Username], [Password], [BackupPassword], [Role], [Email], [Phone], [BannedStatus], [StationID]) VALUES (N'longbv', N'123', N'123', N'Customer', N'longbv@yahoo.com', N'09696969696', 1, 1)
INSERT [dbo].[Account] ([Username], [Password], [BackupPassword], [Role], [Email], [Phone], [BannedStatus], [StationID]) VALUES (N'longdb', N'longdb', N'longdb', N'Customer', N'winvn92@gmail.com', N'0906425539', 0, 1)
INSERT [dbo].[Account] ([Username], [Password], [BackupPassword], [Role], [Email], [Phone], [BannedStatus], [StationID]) VALUES (N'lulp', N'123456', N'123456', N'Staff', N'lulp@yahoo.com', N'01636987483', 0, 1)
INSERT [dbo].[Account] ([Username], [Password], [BackupPassword], [Role], [Email], [Phone], [BannedStatus], [StationID]) VALUES (N'newstaff', N'newstaff', N'newstaff', N'Customer', N'newstaff@staff.com', N'2673228852', 0, 1)
INSERT [dbo].[Account] ([Username], [Password], [BackupPassword], [Role], [Email], [Phone], [BannedStatus], [StationID]) VALUES (N'staff4', N'staff4', N'staff4', N'Staff', N'staff4@gmail.com', N'0123456789', 0, 4)
INSERT [dbo].[Account] ([Username], [Password], [BackupPassword], [Role], [Email], [Phone], [BannedStatus], [StationID]) VALUES (N'staff5', N'staff5', N'staff5', N'Staff', N'staff5@gmail.com', N'0123456789', 0, 5)
INSERT [dbo].[Account] ([Username], [Password], [BackupPassword], [Role], [Email], [Phone], [BannedStatus], [StationID]) VALUES (N'staff7', N'staff7', N'staff7', N'Staff', N'staff6@gmail.com', N'0123456789', 0, 1)
INSERT [dbo].[Account] ([Username], [Password], [BackupPassword], [Role], [Email], [Phone], [BannedStatus], [StationID]) VALUES (N'staff8', N'staff8', N'staff8', N'Staff', N'staff8@gmail.com', N'0123456789', 0, 8)
INSERT [dbo].[Account] ([Username], [Password], [BackupPassword], [Role], [Email], [Phone], [BannedStatus], [StationID]) VALUES (N'staff9', N'staff9', N'staff9', N'Staff', N'staff6@gmail.com', N'0123456789', 0, 1)
INSERT [dbo].[Account] ([Username], [Password], [BackupPassword], [Role], [Email], [Phone], [BannedStatus], [StationID]) VALUES (N'stevejobs', N'stevejobs', N'stevejobs', N'Customer', N'stevejobs@apple.com', N'0976777879', 0, 1)
INSERT [dbo].[Account] ([Username], [Password], [BackupPassword], [Role], [Email], [Phone], [BannedStatus], [StationID]) VALUES (N'tungnt', N'iridescent', N'iridescent', N'Staff', N'tungnt@yahoo.com', N'01628468737', 0, 1)
INSERT [dbo].[Account] ([Username], [Password], [BackupPassword], [Role], [Email], [Phone], [BannedStatus], [StationID]) VALUES (N'vanvd', N'vanvd', N'vanvd', N'Customer', N'vanvd@fpt.edu.vn', N'0977767774', 0, 1)
INSERT [dbo].[Account] ([Username], [Password], [BackupPassword], [Role], [Email], [Phone], [BannedStatus], [StationID]) VALUES (N'vinhnt', N'vinhnt', N'vinhnt', N'Customer', N'vinhnt@fpt.edu.vn', N'0937842371', 0, 1)
INSERT [dbo].[Account] ([Username], [Password], [BackupPassword], [Role], [Email], [Phone], [BannedStatus], [StationID]) VALUES (N'VuHoangLong', N'aaaaa', N'aaaaa', N'Customer', N'vuhoanglong1992.lv@gmail.com', N'0123456789', 0, 1)
/****** Object:  Table [dbo].[Comment]    Script Date: 04/02/2014 12:50:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comment](
	[CommentID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[CommentContent] [nvarchar](4000) NOT NULL,
	[DatePost] [date] NOT NULL,
	[Status] [bit] NOT NULL,
	[StationID] [int] NOT NULL,
 CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED 
(
	[CommentID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notification]    Script Date: 04/02/2014 12:50:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notification](
	[ID] [int] NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[StationID] [int] NOT NULL,
	[isView] [bit] NOT NULL,
 CONSTRAINT [PK_Notification] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Schedule]    Script Date: 04/02/2014 12:50:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Schedule](
	[ScheduleID] [int] IDENTITY(1,1) NOT NULL,
	[ArrangeID] [int] NOT NULL,
	[EstimateDepartureTime] [time](0) NOT NULL,
	[EstimateArrivalTime] [time](0) NOT NULL,
 CONSTRAINT [PK_Schedule] PRIMARY KEY CLUSTERED 
(
	[ScheduleID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Schedule] ON
INSERT [dbo].[Schedule] ([ScheduleID], [ArrangeID], [EstimateDepartureTime], [EstimateArrivalTime]) VALUES (1, 63, CAST(0x0070620000000000 AS Time), CAST(0x00300B0100000000 AS Time))
INSERT [dbo].[Schedule] ([ScheduleID], [ArrangeID], [EstimateDepartureTime], [EstimateArrivalTime]) VALUES (2, 62, CAST(0x00300B0100000000 AS Time), CAST(0x0070620000000000 AS Time))
INSERT [dbo].[Schedule] ([ScheduleID], [ArrangeID], [EstimateDepartureTime], [EstimateArrivalTime]) VALUES (3, 51, CAST(0x0048200100000000 AS Time), CAST(0x0050460000000000 AS Time))
INSERT [dbo].[Schedule] ([ScheduleID], [ArrangeID], [EstimateDepartureTime], [EstimateArrivalTime]) VALUES (4, 52, CAST(0x0050270100000000 AS Time), CAST(0x00584D0000000000 AS Time))
INSERT [dbo].[Schedule] ([ScheduleID], [ArrangeID], [EstimateDepartureTime], [EstimateArrivalTime]) VALUES (5, 53, CAST(0x00582E0100000000 AS Time), CAST(0x0060540000000000 AS Time))
INSERT [dbo].[Schedule] ([ScheduleID], [ArrangeID], [EstimateDepartureTime], [EstimateArrivalTime]) VALUES (6, 54, CAST(0x0060350100000000 AS Time), CAST(0x00685B0000000000 AS Time))
INSERT [dbo].[Schedule] ([ScheduleID], [ArrangeID], [EstimateDepartureTime], [EstimateArrivalTime]) VALUES (7, 55, CAST(0x00683C0100000000 AS Time), CAST(0x0070620000000000 AS Time))
INSERT [dbo].[Schedule] ([ScheduleID], [ArrangeID], [EstimateDepartureTime], [EstimateArrivalTime]) VALUES (8, 56, CAST(0x0078690000000000 AS Time), CAST(0x0000E10000000000 AS Time))
INSERT [dbo].[Schedule] ([ScheduleID], [ArrangeID], [EstimateDepartureTime], [EstimateArrivalTime]) VALUES (9, 57, CAST(0x0088770000000000 AS Time), CAST(0x0008E80000000000 AS Time))
INSERT [dbo].[Schedule] ([ScheduleID], [ArrangeID], [EstimateDepartureTime], [EstimateArrivalTime]) VALUES (10, 58, CAST(0x0098850000000000 AS Time), CAST(0x0010EF0000000000 AS Time))
INSERT [dbo].[Schedule] ([ScheduleID], [ArrangeID], [EstimateDepartureTime], [EstimateArrivalTime]) VALUES (11, 70, CAST(0x0090140100000000 AS Time), CAST(0x00C05D0000000000 AS Time))
INSERT [dbo].[Schedule] ([ScheduleID], [ArrangeID], [EstimateDepartureTime], [EstimateArrivalTime]) VALUES (12, 71, CAST(0x00F8240100000000 AS Time), CAST(0x00286E0000000000 AS Time))
INSERT [dbo].[Schedule] ([ScheduleID], [ArrangeID], [EstimateDepartureTime], [EstimateArrivalTime]) VALUES (13, 72, CAST(0x0008330100000000 AS Time), CAST(0x00387C0000000000 AS Time))
INSERT [dbo].[Schedule] ([ScheduleID], [ArrangeID], [EstimateDepartureTime], [EstimateArrivalTime]) VALUES (14, 73, CAST(0x00B8370100000000 AS Time), CAST(0x00E8800000000000 AS Time))
INSERT [dbo].[Schedule] ([ScheduleID], [ArrangeID], [EstimateDepartureTime], [EstimateArrivalTime]) VALUES (15, 74, CAST(0x00F4650000000000 AS Time), CAST(0x00A4000100000000 AS Time))
INSERT [dbo].[Schedule] ([ScheduleID], [ArrangeID], [EstimateDepartureTime], [EstimateArrivalTime]) VALUES (16, 75, CAST(0x0030750000000000 AS Time), CAST(0x00E00F0100000000 AS Time))
INSERT [dbo].[Schedule] ([ScheduleID], [ArrangeID], [EstimateDepartureTime], [EstimateArrivalTime]) VALUES (17, 76, CAST(0x00B09A0000000000 AS Time), CAST(0x0060350100000000 AS Time))
INSERT [dbo].[Schedule] ([ScheduleID], [ArrangeID], [EstimateDepartureTime], [EstimateArrivalTime]) VALUES (18, 91, CAST(0x00F0D20000000000 AS Time), CAST(0x0020FD0000000000 AS Time))
INSERT [dbo].[Schedule] ([ScheduleID], [ArrangeID], [EstimateDepartureTime], [EstimateArrivalTime]) VALUES (19, 92, CAST(0x0000E10000000000 AS Time), CAST(0x00300B0100000000 AS Time))
INSERT [dbo].[Schedule] ([ScheduleID], [ArrangeID], [EstimateDepartureTime], [EstimateArrivalTime]) VALUES (20, 93, CAST(0x0010EF0000000000 AS Time), CAST(0x0040190100000000 AS Time))
INSERT [dbo].[Schedule] ([ScheduleID], [ArrangeID], [EstimateDepartureTime], [EstimateArrivalTime]) VALUES (21, 94, CAST(0x0020FD0000000000 AS Time), CAST(0x0050270100000000 AS Time))
INSERT [dbo].[Schedule] ([ScheduleID], [ArrangeID], [EstimateDepartureTime], [EstimateArrivalTime]) VALUES (22, 95, CAST(0x00300B0100000000 AS Time), CAST(0x0060350100000000 AS Time))
INSERT [dbo].[Schedule] ([ScheduleID], [ArrangeID], [EstimateDepartureTime], [EstimateArrivalTime]) VALUES (23, 96, CAST(0x0040190100000000 AS Time), CAST(0x0070430100000000 AS Time))
INSERT [dbo].[Schedule] ([ScheduleID], [ArrangeID], [EstimateDepartureTime], [EstimateArrivalTime]) VALUES (24, 97, CAST(0x0000000000000000 AS Time), CAST(0x00302A0000000000 AS Time))
INSERT [dbo].[Schedule] ([ScheduleID], [ArrangeID], [EstimateDepartureTime], [EstimateArrivalTime]) VALUES (25, 98, CAST(0x0050460000000000 AS Time), CAST(0x0080700000000000 AS Time))
INSERT [dbo].[Schedule] ([ScheduleID], [ArrangeID], [EstimateDepartureTime], [EstimateArrivalTime]) VALUES (26, 99, CAST(0x0060540000000000 AS Time), CAST(0x00907E0000000000 AS Time))
INSERT [dbo].[Schedule] ([ScheduleID], [ArrangeID], [EstimateDepartureTime], [EstimateArrivalTime]) VALUES (27, 100, CAST(0x0070620000000000 AS Time), CAST(0x00A08C0000000000 AS Time))
INSERT [dbo].[Schedule] ([ScheduleID], [ArrangeID], [EstimateDepartureTime], [EstimateArrivalTime]) VALUES (28, 101, CAST(0x0080700000000000 AS Time), CAST(0x00B09A0000000000 AS Time))
INSERT [dbo].[Schedule] ([ScheduleID], [ArrangeID], [EstimateDepartureTime], [EstimateArrivalTime]) VALUES (29, 102, CAST(0x00907E0000000000 AS Time), CAST(0x00C0A80000000000 AS Time))
INSERT [dbo].[Schedule] ([ScheduleID], [ArrangeID], [EstimateDepartureTime], [EstimateArrivalTime]) VALUES (30, 103, CAST(0x00A08C0000000000 AS Time), CAST(0x00D0B60000000000 AS Time))
INSERT [dbo].[Schedule] ([ScheduleID], [ArrangeID], [EstimateDepartureTime], [EstimateArrivalTime]) VALUES (31, 104, CAST(0x00B09A0000000000 AS Time), CAST(0x00E0C40000000000 AS Time))
INSERT [dbo].[Schedule] ([ScheduleID], [ArrangeID], [EstimateDepartureTime], [EstimateArrivalTime]) VALUES (32, 105, CAST(0x00C0A80000000000 AS Time), CAST(0x00F0D20000000000 AS Time))
INSERT [dbo].[Schedule] ([ScheduleID], [ArrangeID], [EstimateDepartureTime], [EstimateArrivalTime]) VALUES (33, 106, CAST(0x00D0B60000000000 AS Time), CAST(0x0000E10000000000 AS Time))
INSERT [dbo].[Schedule] ([ScheduleID], [ArrangeID], [EstimateDepartureTime], [EstimateArrivalTime]) VALUES (34, 107, CAST(0x00E0C40000000000 AS Time), CAST(0x0010EF0000000000 AS Time))
INSERT [dbo].[Schedule] ([ScheduleID], [ArrangeID], [EstimateDepartureTime], [EstimateArrivalTime]) VALUES (35, 108, CAST(0x0050270100000000 AS Time), CAST(0x0000000000000000 AS Time))
INSERT [dbo].[Schedule] ([ScheduleID], [ArrangeID], [EstimateDepartureTime], [EstimateArrivalTime]) VALUES (36, 109, CAST(0x0060350100000000 AS Time), CAST(0x00100E0000000000 AS Time))
INSERT [dbo].[Schedule] ([ScheduleID], [ArrangeID], [EstimateDepartureTime], [EstimateArrivalTime]) VALUES (37, 110, CAST(0x0070430100000000 AS Time), CAST(0x00201C0000000000 AS Time))
INSERT [dbo].[Schedule] ([ScheduleID], [ArrangeID], [EstimateDepartureTime], [EstimateArrivalTime]) VALUES (38, 46, CAST(0x00A8930000000000 AS Time), CAST(0x00685B0000000000 AS Time))
INSERT [dbo].[Schedule] ([ScheduleID], [ArrangeID], [EstimateDepartureTime], [EstimateArrivalTime]) VALUES (39, 47, CAST(0x00C8AF0000000000 AS Time), CAST(0x0088770000000000 AS Time))
INSERT [dbo].[Schedule] ([ScheduleID], [ArrangeID], [EstimateDepartureTime], [EstimateArrivalTime]) VALUES (40, 48, CAST(0x00E8CB0000000000 AS Time), CAST(0x00A8930000000000 AS Time))
INSERT [dbo].[Schedule] ([ScheduleID], [ArrangeID], [EstimateDepartureTime], [EstimateArrivalTime]) VALUES (41, 49, CAST(0x0008E80000000000 AS Time), CAST(0x00C8AF0000000000 AS Time))
INSERT [dbo].[Schedule] ([ScheduleID], [ArrangeID], [EstimateDepartureTime], [EstimateArrivalTime]) VALUES (42, 50, CAST(0x0028040100000000 AS Time), CAST(0x00E8CB0000000000 AS Time))
INSERT [dbo].[Schedule] ([ScheduleID], [ArrangeID], [EstimateDepartureTime], [EstimateArrivalTime]) VALUES (43, 16, CAST(0x0080700000000000 AS Time), CAST(0x00C8AF0000000000 AS Time))
INSERT [dbo].[Schedule] ([ScheduleID], [ArrangeID], [EstimateDepartureTime], [EstimateArrivalTime]) VALUES (44, 17, CAST(0x00302A0000000000 AS Time), CAST(0x0078690000000000 AS Time))
INSERT [dbo].[Schedule] ([ScheduleID], [ArrangeID], [EstimateDepartureTime], [EstimateArrivalTime]) VALUES (45, 68, CAST(0x0048200100000000 AS Time), CAST(0x0088770000000000 AS Time))
INSERT [dbo].[Schedule] ([ScheduleID], [ArrangeID], [EstimateDepartureTime], [EstimateArrivalTime]) VALUES (46, 69, CAST(0x00E4570000000000 AS Time), CAST(0x00A4000100000000 AS Time))
INSERT [dbo].[Schedule] ([ScheduleID], [ArrangeID], [EstimateDepartureTime], [EstimateArrivalTime]) VALUES (47, 1, CAST(0x0080700000000000 AS Time), CAST(0x00B09A0000000000 AS Time))
INSERT [dbo].[Schedule] ([ScheduleID], [ArrangeID], [EstimateDepartureTime], [EstimateArrivalTime]) VALUES (48, 3, CAST(0x0010EF0000000000 AS Time), CAST(0x0040190100000000 AS Time))
INSERT [dbo].[Schedule] ([ScheduleID], [ArrangeID], [EstimateDepartureTime], [EstimateArrivalTime]) VALUES (49, 158, CAST(0x0088770000000000 AS Time), CAST(0x00C8AF0000000000 AS Time))
INSERT [dbo].[Schedule] ([ScheduleID], [ArrangeID], [EstimateDepartureTime], [EstimateArrivalTime]) VALUES (50, 159, CAST(0x00C0A80000000000 AS Time), CAST(0x0000E10000000000 AS Time))
INSERT [dbo].[Schedule] ([ScheduleID], [ArrangeID], [EstimateDepartureTime], [EstimateArrivalTime]) VALUES (51, 163, CAST(0x0070620000000000 AS Time), CAST(0x00B09A0000000000 AS Time))
INSERT [dbo].[Schedule] ([ScheduleID], [ArrangeID], [EstimateDepartureTime], [EstimateArrivalTime]) VALUES (52, 164, CAST(0x0080700000000000 AS Time), CAST(0x00C0A80000000000 AS Time))
INSERT [dbo].[Schedule] ([ScheduleID], [ArrangeID], [EstimateDepartureTime], [EstimateArrivalTime]) VALUES (53, 165, CAST(0x00907E0000000000 AS Time), CAST(0x00D0B60000000000 AS Time))
INSERT [dbo].[Schedule] ([ScheduleID], [ArrangeID], [EstimateDepartureTime], [EstimateArrivalTime]) VALUES (54, 166, CAST(0x00F0D20000000000 AS Time), CAST(0x00300B0100000000 AS Time))
INSERT [dbo].[Schedule] ([ScheduleID], [ArrangeID], [EstimateDepartureTime], [EstimateArrivalTime]) VALUES (55, 167, CAST(0x0000E10000000000 AS Time), CAST(0x0040190100000000 AS Time))
INSERT [dbo].[Schedule] ([ScheduleID], [ArrangeID], [EstimateDepartureTime], [EstimateArrivalTime]) VALUES (56, 168, CAST(0x0088770000000000 AS Time), CAST(0x00E0C40000000000 AS Time))
INSERT [dbo].[Schedule] ([ScheduleID], [ArrangeID], [EstimateDepartureTime], [EstimateArrivalTime]) VALUES (57, 169, CAST(0x00F0D20000000000 AS Time), CAST(0x0048200100000000 AS Time))
INSERT [dbo].[Schedule] ([ScheduleID], [ArrangeID], [EstimateDepartureTime], [EstimateArrivalTime]) VALUES (58, 170, CAST(0x00C0A80000000000 AS Time), CAST(0x0018F60000000000 AS Time))
SET IDENTITY_INSERT [dbo].[Schedule] OFF
/****** Object:  Table [dbo].[RouteStage]    Script Date: 04/02/2014 12:50:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RouteStage](
	[RouteID] [int] NOT NULL,
	[StageID] [int] NOT NULL,
	[StageIndex] [int] NULL,
 CONSTRAINT [PK_RouteStage] PRIMARY KEY CLUSTERED 
(
	[RouteID] ASC,
	[StageID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[RouteStage] ([RouteID], [StageID], [StageIndex]) VALUES (7, 24, 1)
INSERT [dbo].[RouteStage] ([RouteID], [StageID], [StageIndex]) VALUES (7, 25, 2)
INSERT [dbo].[RouteStage] ([RouteID], [StageID], [StageIndex]) VALUES (7, 26, 3)
INSERT [dbo].[RouteStage] ([RouteID], [StageID], [StageIndex]) VALUES (23, 14, 1)
INSERT [dbo].[RouteStage] ([RouteID], [StageID], [StageIndex]) VALUES (23, 24, 2)
INSERT [dbo].[RouteStage] ([RouteID], [StageID], [StageIndex]) VALUES (24, 14, 2)
INSERT [dbo].[RouteStage] ([RouteID], [StageID], [StageIndex]) VALUES (24, 16, 1)
INSERT [dbo].[RouteStage] ([RouteID], [StageID], [StageIndex]) VALUES (25, 14, 2)
INSERT [dbo].[RouteStage] ([RouteID], [StageID], [StageIndex]) VALUES (25, 16, 1)
INSERT [dbo].[RouteStage] ([RouteID], [StageID], [StageIndex]) VALUES (25, 24, 3)
INSERT [dbo].[RouteStage] ([RouteID], [StageID], [StageIndex]) VALUES (26, 25, 1)
/****** Object:  Table [dbo].[Request]    Script Date: 04/02/2014 12:50:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Request](
	[RequestID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[DeliveryStatusID] [int] NOT NULL,
	[FeeID] [int] NULL,
	[EstimateWeight] [nvarchar](50) NOT NULL,
	[EstimateVolume] [nvarchar](50) NOT NULL,
	[DateRequest] [date] NOT NULL,
	[Description] [nvarchar](500) NULL,
	[FromLocation] [int] NOT NULL,
	[ToLocation] [int] NOT NULL,
	[SenderAddress] [nvarchar](500) NULL,
	[ReceiverAddress] [nvarchar](500) NULL,
	[SenderPhone] [nvarchar](50) NULL,
	[ReceiverName] [nvarchar](100) NOT NULL,
	[ReceiverPhone] [nvarchar](50) NULL,
	[Type] [bit] NULL,
	[TypeOfPayment] [nvarchar](100) NOT NULL,
	[ArrivedDate] [date] NULL,
	[Payment] [bit] NULL,
	[RequestCode] [nvarchar](50) NULL,
 CONSTRAINT [PK_Request] PRIMARY KEY CLUSTERED 
(
	[RequestID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Request] ON
INSERT [dbo].[Request] ([RequestID], [Username], [DeliveryStatusID], [FeeID], [EstimateWeight], [EstimateVolume], [DateRequest], [Description], [FromLocation], [ToLocation], [SenderAddress], [ReceiverAddress], [SenderPhone], [ReceiverName], [ReceiverPhone], [Type], [TypeOfPayment], [ArrivedDate], [Payment], [RequestCode]) VALUES (1, N'billgates', 1, 2, N'5 - 8', N'5 - 8', CAST(0x5C380B00 AS Date), N'Hehe', 7, 5, N'Cần Thơ', N'Sài Gòn', N'0906425539', N'Lý Tiểu Long', N'01628468737', 1, N'Thanh toán online', NULL, 0, N'sad76&&&^')
INSERT [dbo].[Request] ([RequestID], [Username], [DeliveryStatusID], [FeeID], [EstimateWeight], [EstimateVolume], [DateRequest], [Description], [FromLocation], [ToLocation], [SenderAddress], [ReceiverAddress], [SenderPhone], [ReceiverName], [ReceiverPhone], [Type], [TypeOfPayment], [ArrivedDate], [Payment], [RequestCode]) VALUES (2, N'billgates', 1, 8, N'67 - 78 ', N'67 - 75', CAST(0x5C380B00 AS Date), N'Hehe', 10, 8, N'Cà Mau', N'Đà Nẵng', N'0906425539', N'Kiều Trọng Khánh', N'01628468737', 1, N'Thanh toán trực tiếp', NULL, 0, N'^%VS*^')
INSERT [dbo].[Request] ([RequestID], [Username], [DeliveryStatusID], [FeeID], [EstimateWeight], [EstimateVolume], [DateRequest], [Description], [FromLocation], [ToLocation], [SenderAddress], [ReceiverAddress], [SenderPhone], [ReceiverName], [ReceiverPhone], [Type], [TypeOfPayment], [ArrivedDate], [Payment], [RequestCode]) VALUES (3, N'customer1', 1, 4, N'20 - 27.5', N'20 - 28', CAST(0x5C380B00 AS Date), N'Hehe', 7, 9, N'Cần Thơ', N'Quy Nhơn', N'0906425539', N'Nguyễn Trọng Tài', N'01628468737', 1, N'Thanh toán online', NULL, 0, N'BEc@8#j')
INSERT [dbo].[Request] ([RequestID], [Username], [DeliveryStatusID], [FeeID], [EstimateWeight], [EstimateVolume], [DateRequest], [Description], [FromLocation], [ToLocation], [SenderAddress], [ReceiverAddress], [SenderPhone], [ReceiverName], [ReceiverPhone], [Type], [TypeOfPayment], [ArrivedDate], [Payment], [RequestCode]) VALUES (4, N'customer1', 1, 3, N'10 - 18', N'10 - 17', CAST(0x5C380B00 AS Date), N'Hehe', 10, 7, N'Cà Mau', N'Cần Thơ', N'0906425539', N'Phan Đình Tùng', N'01628468737', 1, N'Thanh toán trực tiếp', NULL, 0, N'fvs*^VD')
INSERT [dbo].[Request] ([RequestID], [Username], [DeliveryStatusID], [FeeID], [EstimateWeight], [EstimateVolume], [DateRequest], [Description], [FromLocation], [ToLocation], [SenderAddress], [ReceiverAddress], [SenderPhone], [ReceiverName], [ReceiverPhone], [Type], [TypeOfPayment], [ArrivedDate], [Payment], [RequestCode]) VALUES (5, N'vinhnt', 1, 7, N'55 - 65 ', N'57 - 65 ', CAST(0x5C380B00 AS Date), N'Hehe', 3, 8, N'Đà Lạt', N'Đà Nẵng', N'0906425539', N'Phan Đình Tùng', N'01628468737', 1, N'Thanh toán trực tiếp', NULL, 0, N'fvs*^VD')
INSERT [dbo].[Request] ([RequestID], [Username], [DeliveryStatusID], [FeeID], [EstimateWeight], [EstimateVolume], [DateRequest], [Description], [FromLocation], [ToLocation], [SenderAddress], [ReceiverAddress], [SenderPhone], [ReceiverName], [ReceiverPhone], [Type], [TypeOfPayment], [ArrivedDate], [Payment], [RequestCode]) VALUES (6, N'vanvd', 1, 10, N'90 - 98 ', N'87 - 100', CAST(0x5C380B00 AS Date), N'Hehe', 5, 9, N'Sài Gòn', N'Quy Nhơn', N'0906425539', N'Boss VT', N'01628468737', 1, N'Thanh toán online', NULL, 0, N'dl31vt')
INSERT [dbo].[Request] ([RequestID], [Username], [DeliveryStatusID], [FeeID], [EstimateWeight], [EstimateVolume], [DateRequest], [Description], [FromLocation], [ToLocation], [SenderAddress], [ReceiverAddress], [SenderPhone], [ReceiverName], [ReceiverPhone], [Type], [TypeOfPayment], [ArrivedDate], [Payment], [RequestCode]) VALUES (7, N'longdb', 1, 6, N'42 - 52', N'45 - 55', CAST(0x5C380B00 AS Date), N'Hehe', 7, 8, N'Cần Thơ', N'Đà Nẵng', N'0906425539', N'Dâu Đà Lạt', N'01628468737', 1, N'Thanh toán trực tiếp', NULL, 0, N'dl310cm')
INSERT [dbo].[Request] ([RequestID], [Username], [DeliveryStatusID], [FeeID], [EstimateWeight], [EstimateVolume], [DateRequest], [Description], [FromLocation], [ToLocation], [SenderAddress], [ReceiverAddress], [SenderPhone], [ReceiverName], [ReceiverPhone], [Type], [TypeOfPayment], [ArrivedDate], [Payment], [RequestCode]) VALUES (8, N'VuHoangLong', 1, 5, N'30 - 40 ', N'30 - 42', CAST(0x5C380B00 AS Date), N'Hehe', 5, 3, N'Sài Gòn ', N'Đà Lạt', N'0906425539', N'Boss VT', N'01628468737', 1, N'Thanh toán trực tiếp', NULL, 0, N'sg12vt')
SET IDENTITY_INSERT [dbo].[Request] OFF
/****** Object:  Table [dbo].[Rating]    Script Date: 04/02/2014 12:50:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rating](
	[RatingID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[RateLevel] [float] NOT NULL,
	[StationID] [int] NOT NULL,
 CONSTRAINT [PK_Rating] PRIMARY KEY CLUSTERED 
(
	[RatingID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserInfo]    Script Date: 04/02/2014 12:50:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserInfo](
	[Username] [nvarchar](50) NOT NULL,
	[Firstname] [nvarchar](50) NULL,
	[Lastname] [nvarchar](50) NULL,
	[Address] [nvarchar](500) NULL,
	[Phone number] [nchar](11) NULL,
 CONSTRAINT [PK_UserInfo_1] PRIMARY KEY CLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[UserInfo] ([Username], [Firstname], [Lastname], [Address], [Phone number]) VALUES (N'billgates', NULL, NULL, NULL, NULL)
INSERT [dbo].[UserInfo] ([Username], [Firstname], [Lastname], [Address], [Phone number]) VALUES (N'long', N'long', N'vu', N'phan đăng lưu', NULL)
INSERT [dbo].[UserInfo] ([Username], [Firstname], [Lastname], [Address], [Phone number]) VALUES (N'newstaff', NULL, NULL, NULL, NULL)
INSERT [dbo].[UserInfo] ([Username], [Firstname], [Lastname], [Address], [Phone number]) VALUES (N'vinhnt', NULL, NULL, NULL, NULL)
INSERT [dbo].[UserInfo] ([Username], [Firstname], [Lastname], [Address], [Phone number]) VALUES (N'VuHoangLong', N'Long', N'Vũ', N'hehe', NULL)
/****** Object:  Table [dbo].[Trip]    Script Date: 04/02/2014 12:50:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Trip](
	[TripID] [int] IDENTITY(1,1) NOT NULL,
	[EstimateDepartureTime] [time](0) NOT NULL,
	[EstimateArrivalTime] [time](0) NOT NULL,
	[RealDepartureTime] [time](0) NULL,
	[RealArrivalTime] [time](0) NULL,
	[EstimateVolume] [float] NOT NULL,
	[AvailableVolume] [float] NULL,
	[RealVolume] [float] NULL,
	[Date] [date] NOT NULL,
	[RouteID] [int] NOT NULL,
	[CoachID] [int] NOT NULL,
	[ScheduleID] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Trip] PRIMARY KEY CLUSTERED 
(
	[TripID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Trip] ON
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (62, CAST(0x00F0D20000000000 AS Time), CAST(0x0020FD0000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5A380B00 AS Date), 3, 90, 18, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (63, CAST(0x00F0D20000000000 AS Time), CAST(0x0020FD0000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5B380B00 AS Date), 3, 90, 18, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (64, CAST(0x00F0D20000000000 AS Time), CAST(0x0020FD0000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5C380B00 AS Date), 3, 90, 18, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (65, CAST(0x00F0D20000000000 AS Time), CAST(0x0020FD0000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5D380B00 AS Date), 3, 90, 18, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (66, CAST(0x00F0D20000000000 AS Time), CAST(0x0020FD0000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5E380B00 AS Date), 3, 90, 18, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (67, CAST(0x00F0D20000000000 AS Time), CAST(0x0020FD0000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5F380B00 AS Date), 3, 90, 18, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (68, CAST(0x00F0D20000000000 AS Time), CAST(0x0020FD0000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x60380B00 AS Date), 3, 90, 18, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (69, CAST(0x0000E10000000000 AS Time), CAST(0x00300B0100000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5A380B00 AS Date), 3, 91, 19, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (70, CAST(0x0000E10000000000 AS Time), CAST(0x00300B0100000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5B380B00 AS Date), 3, 91, 19, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (71, CAST(0x0000E10000000000 AS Time), CAST(0x00300B0100000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5C380B00 AS Date), 3, 91, 19, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (72, CAST(0x0000E10000000000 AS Time), CAST(0x00300B0100000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5D380B00 AS Date), 3, 91, 19, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (73, CAST(0x0000E10000000000 AS Time), CAST(0x00300B0100000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5E380B00 AS Date), 3, 91, 19, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (74, CAST(0x0000E10000000000 AS Time), CAST(0x00300B0100000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5F380B00 AS Date), 3, 91, 19, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (75, CAST(0x0000E10000000000 AS Time), CAST(0x00300B0100000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x60380B00 AS Date), 3, 91, 19, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (76, CAST(0x0010EF0000000000 AS Time), CAST(0x0040190100000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5A380B00 AS Date), 3, 92, 20, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (77, CAST(0x0010EF0000000000 AS Time), CAST(0x0040190100000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5B380B00 AS Date), 3, 92, 20, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (78, CAST(0x0010EF0000000000 AS Time), CAST(0x0040190100000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5C380B00 AS Date), 3, 92, 20, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (79, CAST(0x0010EF0000000000 AS Time), CAST(0x0040190100000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5D380B00 AS Date), 3, 92, 20, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (80, CAST(0x0010EF0000000000 AS Time), CAST(0x0040190100000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5E380B00 AS Date), 3, 92, 20, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (81, CAST(0x0010EF0000000000 AS Time), CAST(0x0040190100000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5F380B00 AS Date), 3, 92, 20, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (82, CAST(0x0010EF0000000000 AS Time), CAST(0x0040190100000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x60380B00 AS Date), 3, 92, 20, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (83, CAST(0x0020FD0000000000 AS Time), CAST(0x0050270100000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5A380B00 AS Date), 3, 93, 21, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (84, CAST(0x0020FD0000000000 AS Time), CAST(0x0050270100000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5B380B00 AS Date), 3, 93, 21, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (85, CAST(0x0020FD0000000000 AS Time), CAST(0x0050270100000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5C380B00 AS Date), 3, 93, 21, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (86, CAST(0x0020FD0000000000 AS Time), CAST(0x0050270100000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5D380B00 AS Date), 3, 93, 21, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (87, CAST(0x0020FD0000000000 AS Time), CAST(0x0050270100000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5E380B00 AS Date), 3, 93, 21, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (88, CAST(0x0020FD0000000000 AS Time), CAST(0x0050270100000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5F380B00 AS Date), 3, 93, 21, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (89, CAST(0x0020FD0000000000 AS Time), CAST(0x0050270100000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x60380B00 AS Date), 3, 93, 21, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (90, CAST(0x00300B0100000000 AS Time), CAST(0x0060350100000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5A380B00 AS Date), 3, 94, 22, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (91, CAST(0x00300B0100000000 AS Time), CAST(0x0060350100000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5B380B00 AS Date), 3, 94, 22, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (92, CAST(0x00300B0100000000 AS Time), CAST(0x0060350100000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5C380B00 AS Date), 3, 94, 22, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (93, CAST(0x00300B0100000000 AS Time), CAST(0x0060350100000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5D380B00 AS Date), 3, 94, 22, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (94, CAST(0x00300B0100000000 AS Time), CAST(0x0060350100000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5E380B00 AS Date), 3, 94, 22, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (95, CAST(0x00300B0100000000 AS Time), CAST(0x0060350100000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5F380B00 AS Date), 3, 94, 22, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (96, CAST(0x00300B0100000000 AS Time), CAST(0x0060350100000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x60380B00 AS Date), 3, 94, 22, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (97, CAST(0x0040190100000000 AS Time), CAST(0x0070430100000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5A380B00 AS Date), 3, 95, 23, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (98, CAST(0x0040190100000000 AS Time), CAST(0x0070430100000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5B380B00 AS Date), 3, 95, 23, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (99, CAST(0x0040190100000000 AS Time), CAST(0x0070430100000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5C380B00 AS Date), 3, 95, 23, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (100, CAST(0x0040190100000000 AS Time), CAST(0x0070430100000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5D380B00 AS Date), 3, 95, 23, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (101, CAST(0x0040190100000000 AS Time), CAST(0x0070430100000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5E380B00 AS Date), 3, 95, 23, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (102, CAST(0x0040190100000000 AS Time), CAST(0x0070430100000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5F380B00 AS Date), 3, 95, 23, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (103, CAST(0x0040190100000000 AS Time), CAST(0x0070430100000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x60380B00 AS Date), 3, 95, 23, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (104, CAST(0x0000000000000000 AS Time), CAST(0x00302A0000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5A380B00 AS Date), 3, 96, 24, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (105, CAST(0x0000000000000000 AS Time), CAST(0x00302A0000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5B380B00 AS Date), 3, 96, 24, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (106, CAST(0x0000000000000000 AS Time), CAST(0x00302A0000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5C380B00 AS Date), 3, 96, 24, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (107, CAST(0x0000000000000000 AS Time), CAST(0x00302A0000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5D380B00 AS Date), 3, 96, 24, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (108, CAST(0x0000000000000000 AS Time), CAST(0x00302A0000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5E380B00 AS Date), 3, 96, 24, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (109, CAST(0x0000000000000000 AS Time), CAST(0x00302A0000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5F380B00 AS Date), 3, 96, 24, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (110, CAST(0x0000000000000000 AS Time), CAST(0x00302A0000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x60380B00 AS Date), 3, 96, 24, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (111, CAST(0x0050460000000000 AS Time), CAST(0x0080700000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5A380B00 AS Date), 3, 97, 25, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (112, CAST(0x0050460000000000 AS Time), CAST(0x0080700000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5B380B00 AS Date), 3, 97, 25, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (113, CAST(0x0050460000000000 AS Time), CAST(0x0080700000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5C380B00 AS Date), 3, 97, 25, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (114, CAST(0x0050460000000000 AS Time), CAST(0x0080700000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5D380B00 AS Date), 3, 97, 25, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (115, CAST(0x0050460000000000 AS Time), CAST(0x0080700000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5E380B00 AS Date), 3, 97, 25, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (116, CAST(0x0050460000000000 AS Time), CAST(0x0080700000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5F380B00 AS Date), 3, 97, 25, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (117, CAST(0x0050460000000000 AS Time), CAST(0x0080700000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x60380B00 AS Date), 3, 97, 25, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (118, CAST(0x0060540000000000 AS Time), CAST(0x00907E0000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5A380B00 AS Date), 3, 98, 26, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (119, CAST(0x0060540000000000 AS Time), CAST(0x00907E0000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5B380B00 AS Date), 3, 98, 26, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (120, CAST(0x0060540000000000 AS Time), CAST(0x00907E0000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5C380B00 AS Date), 3, 98, 26, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (121, CAST(0x0060540000000000 AS Time), CAST(0x00907E0000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5D380B00 AS Date), 3, 98, 26, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (122, CAST(0x0060540000000000 AS Time), CAST(0x00907E0000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5E380B00 AS Date), 3, 98, 26, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (123, CAST(0x0060540000000000 AS Time), CAST(0x00907E0000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5F380B00 AS Date), 3, 98, 26, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (124, CAST(0x0060540000000000 AS Time), CAST(0x00907E0000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x60380B00 AS Date), 3, 98, 26, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (125, CAST(0x0070620000000000 AS Time), CAST(0x00A08C0000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5A380B00 AS Date), 3, 99, 27, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (126, CAST(0x0070620000000000 AS Time), CAST(0x00A08C0000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5B380B00 AS Date), 3, 99, 27, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (127, CAST(0x0070620000000000 AS Time), CAST(0x00A08C0000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5C380B00 AS Date), 3, 99, 27, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (128, CAST(0x0070620000000000 AS Time), CAST(0x00A08C0000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5D380B00 AS Date), 3, 99, 27, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (129, CAST(0x0070620000000000 AS Time), CAST(0x00A08C0000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5E380B00 AS Date), 3, 99, 27, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (130, CAST(0x0070620000000000 AS Time), CAST(0x00A08C0000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5F380B00 AS Date), 3, 99, 27, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (131, CAST(0x0070620000000000 AS Time), CAST(0x00A08C0000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x60380B00 AS Date), 3, 99, 27, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (132, CAST(0x0080700000000000 AS Time), CAST(0x00B09A0000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5A380B00 AS Date), 3, 100, 28, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (133, CAST(0x0080700000000000 AS Time), CAST(0x00B09A0000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5B380B00 AS Date), 3, 100, 28, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (134, CAST(0x0080700000000000 AS Time), CAST(0x00B09A0000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5C380B00 AS Date), 3, 100, 28, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (135, CAST(0x0080700000000000 AS Time), CAST(0x00B09A0000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5D380B00 AS Date), 3, 100, 28, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (136, CAST(0x0080700000000000 AS Time), CAST(0x00B09A0000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5E380B00 AS Date), 3, 100, 28, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (137, CAST(0x0080700000000000 AS Time), CAST(0x00B09A0000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5F380B00 AS Date), 3, 100, 28, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (138, CAST(0x0080700000000000 AS Time), CAST(0x00B09A0000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x60380B00 AS Date), 3, 100, 28, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (139, CAST(0x00907E0000000000 AS Time), CAST(0x00C0A80000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5A380B00 AS Date), 3, 101, 29, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (140, CAST(0x00907E0000000000 AS Time), CAST(0x00C0A80000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5B380B00 AS Date), 3, 101, 29, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (141, CAST(0x00907E0000000000 AS Time), CAST(0x00C0A80000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5C380B00 AS Date), 3, 101, 29, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (142, CAST(0x00907E0000000000 AS Time), CAST(0x00C0A80000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5D380B00 AS Date), 3, 101, 29, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (143, CAST(0x00907E0000000000 AS Time), CAST(0x00C0A80000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5E380B00 AS Date), 3, 101, 29, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (144, CAST(0x00907E0000000000 AS Time), CAST(0x00C0A80000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5F380B00 AS Date), 3, 101, 29, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (145, CAST(0x00907E0000000000 AS Time), CAST(0x00C0A80000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x60380B00 AS Date), 3, 101, 29, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (146, CAST(0x00A08C0000000000 AS Time), CAST(0x00D0B60000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5A380B00 AS Date), 3, 102, 30, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (147, CAST(0x00A08C0000000000 AS Time), CAST(0x00D0B60000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5B380B00 AS Date), 3, 102, 30, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (148, CAST(0x00A08C0000000000 AS Time), CAST(0x00D0B60000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5C380B00 AS Date), 3, 102, 30, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (149, CAST(0x00A08C0000000000 AS Time), CAST(0x00D0B60000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5D380B00 AS Date), 3, 102, 30, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (150, CAST(0x00A08C0000000000 AS Time), CAST(0x00D0B60000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5E380B00 AS Date), 3, 102, 30, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (151, CAST(0x00A08C0000000000 AS Time), CAST(0x00D0B60000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5F380B00 AS Date), 3, 102, 30, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (152, CAST(0x00A08C0000000000 AS Time), CAST(0x00D0B60000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x60380B00 AS Date), 3, 102, 30, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (153, CAST(0x00B09A0000000000 AS Time), CAST(0x00E0C40000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5A380B00 AS Date), 3, 103, 31, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (154, CAST(0x00B09A0000000000 AS Time), CAST(0x00E0C40000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5B380B00 AS Date), 3, 103, 31, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (155, CAST(0x00B09A0000000000 AS Time), CAST(0x00E0C40000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5C380B00 AS Date), 3, 103, 31, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (156, CAST(0x00B09A0000000000 AS Time), CAST(0x00E0C40000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5D380B00 AS Date), 3, 103, 31, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (157, CAST(0x00B09A0000000000 AS Time), CAST(0x00E0C40000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5E380B00 AS Date), 3, 103, 31, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (158, CAST(0x00B09A0000000000 AS Time), CAST(0x00E0C40000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5F380B00 AS Date), 3, 103, 31, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (159, CAST(0x00B09A0000000000 AS Time), CAST(0x00E0C40000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x60380B00 AS Date), 3, 103, 31, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (160, CAST(0x00C0A80000000000 AS Time), CAST(0x00F0D20000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5A380B00 AS Date), 3, 104, 32, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (161, CAST(0x00C0A80000000000 AS Time), CAST(0x00F0D20000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5B380B00 AS Date), 3, 104, 32, 1, N'Chưa chạy')
GO
print 'Processed 100 total records'
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (162, CAST(0x00C0A80000000000 AS Time), CAST(0x00F0D20000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5C380B00 AS Date), 3, 104, 32, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (163, CAST(0x00C0A80000000000 AS Time), CAST(0x00F0D20000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5D380B00 AS Date), 3, 104, 32, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (164, CAST(0x00C0A80000000000 AS Time), CAST(0x00F0D20000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5E380B00 AS Date), 3, 104, 32, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (165, CAST(0x00C0A80000000000 AS Time), CAST(0x00F0D20000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5F380B00 AS Date), 3, 104, 32, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (166, CAST(0x00C0A80000000000 AS Time), CAST(0x00F0D20000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x60380B00 AS Date), 3, 104, 32, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (167, CAST(0x00D0B60000000000 AS Time), CAST(0x0000E10000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5A380B00 AS Date), 3, 105, 33, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (168, CAST(0x00D0B60000000000 AS Time), CAST(0x0000E10000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5B380B00 AS Date), 3, 105, 33, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (169, CAST(0x00D0B60000000000 AS Time), CAST(0x0000E10000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5C380B00 AS Date), 3, 105, 33, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (170, CAST(0x00D0B60000000000 AS Time), CAST(0x0000E10000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5D380B00 AS Date), 3, 105, 33, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (171, CAST(0x00D0B60000000000 AS Time), CAST(0x0000E10000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5E380B00 AS Date), 3, 105, 33, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (172, CAST(0x00D0B60000000000 AS Time), CAST(0x0000E10000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5F380B00 AS Date), 3, 105, 33, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (173, CAST(0x00D0B60000000000 AS Time), CAST(0x0000E10000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x60380B00 AS Date), 3, 105, 33, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (174, CAST(0x00E0C40000000000 AS Time), CAST(0x0010EF0000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5A380B00 AS Date), 3, 106, 34, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (175, CAST(0x00E0C40000000000 AS Time), CAST(0x0010EF0000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5B380B00 AS Date), 3, 106, 34, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (176, CAST(0x00E0C40000000000 AS Time), CAST(0x0010EF0000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5C380B00 AS Date), 3, 106, 34, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (177, CAST(0x00E0C40000000000 AS Time), CAST(0x0010EF0000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5D380B00 AS Date), 3, 106, 34, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (178, CAST(0x00E0C40000000000 AS Time), CAST(0x0010EF0000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5E380B00 AS Date), 3, 106, 34, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (179, CAST(0x00E0C40000000000 AS Time), CAST(0x0010EF0000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5F380B00 AS Date), 3, 106, 34, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (180, CAST(0x00E0C40000000000 AS Time), CAST(0x0010EF0000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x60380B00 AS Date), 3, 106, 34, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (181, CAST(0x0050270100000000 AS Time), CAST(0x0000000000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5A380B00 AS Date), 3, 107, 35, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (182, CAST(0x0050270100000000 AS Time), CAST(0x0000000000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5B380B00 AS Date), 3, 107, 35, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (183, CAST(0x0050270100000000 AS Time), CAST(0x0000000000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5C380B00 AS Date), 3, 107, 35, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (184, CAST(0x0050270100000000 AS Time), CAST(0x0000000000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5D380B00 AS Date), 3, 107, 35, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (185, CAST(0x0050270100000000 AS Time), CAST(0x0000000000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5E380B00 AS Date), 3, 107, 35, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (186, CAST(0x0050270100000000 AS Time), CAST(0x0000000000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5F380B00 AS Date), 3, 107, 35, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (187, CAST(0x0050270100000000 AS Time), CAST(0x0000000000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x60380B00 AS Date), 3, 107, 35, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (188, CAST(0x0060350100000000 AS Time), CAST(0x00100E0000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5A380B00 AS Date), 3, 108, 36, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (189, CAST(0x0060350100000000 AS Time), CAST(0x00100E0000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5B380B00 AS Date), 3, 108, 36, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (190, CAST(0x0060350100000000 AS Time), CAST(0x00100E0000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5C380B00 AS Date), 3, 108, 36, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (191, CAST(0x0060350100000000 AS Time), CAST(0x00100E0000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5D380B00 AS Date), 3, 108, 36, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (192, CAST(0x0060350100000000 AS Time), CAST(0x00100E0000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5E380B00 AS Date), 3, 108, 36, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (193, CAST(0x0060350100000000 AS Time), CAST(0x00100E0000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5F380B00 AS Date), 3, 108, 36, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (194, CAST(0x0060350100000000 AS Time), CAST(0x00100E0000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x60380B00 AS Date), 3, 108, 36, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (195, CAST(0x0070430100000000 AS Time), CAST(0x00201C0000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5A380B00 AS Date), 3, 109, 37, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (196, CAST(0x0070430100000000 AS Time), CAST(0x00201C0000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5B380B00 AS Date), 3, 109, 37, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (197, CAST(0x0070430100000000 AS Time), CAST(0x00201C0000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5C380B00 AS Date), 3, 109, 37, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (198, CAST(0x0070430100000000 AS Time), CAST(0x00201C0000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5D380B00 AS Date), 3, 109, 37, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (199, CAST(0x0070430100000000 AS Time), CAST(0x00201C0000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5E380B00 AS Date), 3, 109, 37, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (200, CAST(0x0070430100000000 AS Time), CAST(0x00201C0000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x5F380B00 AS Date), 3, 109, 37, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (201, CAST(0x0070430100000000 AS Time), CAST(0x00201C0000000000 AS Time), NULL, NULL, 3944.0000000000009, 3944.0000000000009, NULL, CAST(0x60380B00 AS Date), 3, 109, 37, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (202, CAST(0x0090140100000000 AS Time), CAST(0x00C05D0000000000 AS Time), NULL, NULL, 1575, 1575, NULL, CAST(0x5A380B00 AS Date), 13, 69, 11, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (203, CAST(0x0090140100000000 AS Time), CAST(0x00C05D0000000000 AS Time), NULL, NULL, 1575, 1575, NULL, CAST(0x5B380B00 AS Date), 13, 69, 11, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (204, CAST(0x0090140100000000 AS Time), CAST(0x00C05D0000000000 AS Time), NULL, NULL, 1575, 1575, NULL, CAST(0x5C380B00 AS Date), 13, 69, 11, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (205, CAST(0x0090140100000000 AS Time), CAST(0x00C05D0000000000 AS Time), NULL, NULL, 1575, 1575, NULL, CAST(0x5D380B00 AS Date), 13, 69, 11, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (206, CAST(0x0090140100000000 AS Time), CAST(0x00C05D0000000000 AS Time), NULL, NULL, 1575, 1575, NULL, CAST(0x5E380B00 AS Date), 13, 69, 11, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (207, CAST(0x0090140100000000 AS Time), CAST(0x00C05D0000000000 AS Time), NULL, NULL, 1575, 1575, NULL, CAST(0x5F380B00 AS Date), 13, 69, 11, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (208, CAST(0x0090140100000000 AS Time), CAST(0x00C05D0000000000 AS Time), NULL, NULL, 1575, 1575, NULL, CAST(0x60380B00 AS Date), 13, 69, 11, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (209, CAST(0x00F8240100000000 AS Time), CAST(0x00286E0000000000 AS Time), NULL, NULL, 1575, 1575, NULL, CAST(0x5A380B00 AS Date), 13, 70, 12, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (210, CAST(0x00F8240100000000 AS Time), CAST(0x00286E0000000000 AS Time), NULL, NULL, 1575, 1575, NULL, CAST(0x5B380B00 AS Date), 13, 70, 12, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (211, CAST(0x00F8240100000000 AS Time), CAST(0x00286E0000000000 AS Time), NULL, NULL, 1575, 1575, NULL, CAST(0x5C380B00 AS Date), 13, 70, 12, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (212, CAST(0x00F8240100000000 AS Time), CAST(0x00286E0000000000 AS Time), NULL, NULL, 1575, 1575, NULL, CAST(0x5D380B00 AS Date), 13, 70, 12, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (213, CAST(0x00F8240100000000 AS Time), CAST(0x00286E0000000000 AS Time), NULL, NULL, 1575, 1575, NULL, CAST(0x5E380B00 AS Date), 13, 70, 12, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (214, CAST(0x00F8240100000000 AS Time), CAST(0x00286E0000000000 AS Time), NULL, NULL, 1575, 1575, NULL, CAST(0x5F380B00 AS Date), 13, 70, 12, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (215, CAST(0x00F8240100000000 AS Time), CAST(0x00286E0000000000 AS Time), NULL, NULL, 1575, 1575, NULL, CAST(0x60380B00 AS Date), 13, 70, 12, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (216, CAST(0x0008330100000000 AS Time), CAST(0x00387C0000000000 AS Time), NULL, NULL, 1575, 1575, NULL, CAST(0x5A380B00 AS Date), 13, 71, 13, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (217, CAST(0x0008330100000000 AS Time), CAST(0x00387C0000000000 AS Time), NULL, NULL, 1575, 1575, NULL, CAST(0x5B380B00 AS Date), 13, 71, 13, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (218, CAST(0x0008330100000000 AS Time), CAST(0x00387C0000000000 AS Time), NULL, NULL, 1575, 1575, NULL, CAST(0x5C380B00 AS Date), 13, 71, 13, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (219, CAST(0x0008330100000000 AS Time), CAST(0x00387C0000000000 AS Time), NULL, NULL, 1575, 1575, NULL, CAST(0x5D380B00 AS Date), 13, 71, 13, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (220, CAST(0x0008330100000000 AS Time), CAST(0x00387C0000000000 AS Time), NULL, NULL, 1575, 1575, NULL, CAST(0x5E380B00 AS Date), 13, 71, 13, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (221, CAST(0x0008330100000000 AS Time), CAST(0x00387C0000000000 AS Time), NULL, NULL, 1575, 1575, NULL, CAST(0x5F380B00 AS Date), 13, 71, 13, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (222, CAST(0x0008330100000000 AS Time), CAST(0x00387C0000000000 AS Time), NULL, NULL, 1575, 1575, NULL, CAST(0x60380B00 AS Date), 13, 71, 13, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (223, CAST(0x00B8370100000000 AS Time), CAST(0x00E8800000000000 AS Time), NULL, NULL, 1575, 1575, NULL, CAST(0x5A380B00 AS Date), 13, 72, 14, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (224, CAST(0x00B8370100000000 AS Time), CAST(0x00E8800000000000 AS Time), NULL, NULL, 1575, 1575, NULL, CAST(0x5B380B00 AS Date), 13, 72, 14, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (225, CAST(0x00B8370100000000 AS Time), CAST(0x00E8800000000000 AS Time), NULL, NULL, 1575, 1575, NULL, CAST(0x5C380B00 AS Date), 13, 72, 14, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (226, CAST(0x00B8370100000000 AS Time), CAST(0x00E8800000000000 AS Time), NULL, NULL, 1575, 1575, NULL, CAST(0x5D380B00 AS Date), 13, 72, 14, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (227, CAST(0x00B8370100000000 AS Time), CAST(0x00E8800000000000 AS Time), NULL, NULL, 1575, 1575, NULL, CAST(0x5E380B00 AS Date), 13, 72, 14, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (228, CAST(0x00B8370100000000 AS Time), CAST(0x00E8800000000000 AS Time), NULL, NULL, 1575, 1575, NULL, CAST(0x5F380B00 AS Date), 13, 72, 14, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (229, CAST(0x00B8370100000000 AS Time), CAST(0x00E8800000000000 AS Time), NULL, NULL, 1575, 1575, NULL, CAST(0x60380B00 AS Date), 13, 72, 14, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (230, CAST(0x00F4650000000000 AS Time), CAST(0x00A4000100000000 AS Time), NULL, NULL, 1575, 1575, NULL, CAST(0x5A380B00 AS Date), 13, 73, 15, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (231, CAST(0x00F4650000000000 AS Time), CAST(0x00A4000100000000 AS Time), NULL, NULL, 1575, 1575, NULL, CAST(0x5B380B00 AS Date), 13, 73, 15, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (232, CAST(0x00F4650000000000 AS Time), CAST(0x00A4000100000000 AS Time), NULL, NULL, 1575, 1575, NULL, CAST(0x5C380B00 AS Date), 13, 73, 15, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (233, CAST(0x00F4650000000000 AS Time), CAST(0x00A4000100000000 AS Time), NULL, NULL, 1575, 1575, NULL, CAST(0x5D380B00 AS Date), 13, 73, 15, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (234, CAST(0x00F4650000000000 AS Time), CAST(0x00A4000100000000 AS Time), NULL, NULL, 1575, 1575, NULL, CAST(0x5E380B00 AS Date), 13, 73, 15, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (235, CAST(0x00F4650000000000 AS Time), CAST(0x00A4000100000000 AS Time), NULL, NULL, 1575, 1575, NULL, CAST(0x5F380B00 AS Date), 13, 73, 15, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (236, CAST(0x00F4650000000000 AS Time), CAST(0x00A4000100000000 AS Time), NULL, NULL, 1575, 1575, NULL, CAST(0x60380B00 AS Date), 13, 73, 15, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (237, CAST(0x0030750000000000 AS Time), CAST(0x00E00F0100000000 AS Time), NULL, NULL, 1575, 1575, NULL, CAST(0x5A380B00 AS Date), 13, 74, 16, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (238, CAST(0x0030750000000000 AS Time), CAST(0x00E00F0100000000 AS Time), NULL, NULL, 1575, 1575, NULL, CAST(0x5B380B00 AS Date), 13, 74, 16, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (239, CAST(0x0030750000000000 AS Time), CAST(0x00E00F0100000000 AS Time), NULL, NULL, 1575, 1575, NULL, CAST(0x5C380B00 AS Date), 13, 74, 16, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (240, CAST(0x0030750000000000 AS Time), CAST(0x00E00F0100000000 AS Time), NULL, NULL, 1575, 1575, NULL, CAST(0x5D380B00 AS Date), 13, 74, 16, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (241, CAST(0x0030750000000000 AS Time), CAST(0x00E00F0100000000 AS Time), NULL, NULL, 1575, 1575, NULL, CAST(0x5E380B00 AS Date), 13, 74, 16, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (242, CAST(0x0030750000000000 AS Time), CAST(0x00E00F0100000000 AS Time), NULL, NULL, 1575, 1575, NULL, CAST(0x5F380B00 AS Date), 13, 74, 16, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (243, CAST(0x0030750000000000 AS Time), CAST(0x00E00F0100000000 AS Time), NULL, NULL, 1575, 1575, NULL, CAST(0x60380B00 AS Date), 13, 74, 16, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (244, CAST(0x00B09A0000000000 AS Time), CAST(0x0060350100000000 AS Time), NULL, NULL, 1575, 1575, NULL, CAST(0x5A380B00 AS Date), 13, 75, 17, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (245, CAST(0x00B09A0000000000 AS Time), CAST(0x0060350100000000 AS Time), NULL, NULL, 1575, 1575, NULL, CAST(0x5B380B00 AS Date), 13, 75, 17, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (246, CAST(0x00B09A0000000000 AS Time), CAST(0x0060350100000000 AS Time), NULL, NULL, 1575, 1575, NULL, CAST(0x5C380B00 AS Date), 13, 75, 17, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (247, CAST(0x00B09A0000000000 AS Time), CAST(0x0060350100000000 AS Time), NULL, NULL, 1575, 1575, NULL, CAST(0x5D380B00 AS Date), 13, 75, 17, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (248, CAST(0x00B09A0000000000 AS Time), CAST(0x0060350100000000 AS Time), NULL, NULL, 1575, 1575, NULL, CAST(0x5E380B00 AS Date), 13, 75, 17, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (249, CAST(0x00B09A0000000000 AS Time), CAST(0x0060350100000000 AS Time), NULL, NULL, 1575, 1575, NULL, CAST(0x5F380B00 AS Date), 13, 75, 17, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (250, CAST(0x00B09A0000000000 AS Time), CAST(0x0060350100000000 AS Time), NULL, NULL, 1575, 1575, NULL, CAST(0x60380B00 AS Date), 13, 75, 17, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (251, CAST(0x00A8930000000000 AS Time), CAST(0x00685B0000000000 AS Time), NULL, NULL, 900, 900, NULL, CAST(0x5A380B00 AS Date), 7, 45, 38, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (252, CAST(0x00A8930000000000 AS Time), CAST(0x00685B0000000000 AS Time), NULL, NULL, 900, 900, NULL, CAST(0x5B380B00 AS Date), 7, 45, 38, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (253, CAST(0x00A8930000000000 AS Time), CAST(0x00685B0000000000 AS Time), NULL, NULL, 900, 900, NULL, CAST(0x5C380B00 AS Date), 7, 45, 38, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (254, CAST(0x00A8930000000000 AS Time), CAST(0x00685B0000000000 AS Time), NULL, NULL, 900, 900, NULL, CAST(0x5D380B00 AS Date), 7, 45, 38, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (255, CAST(0x00A8930000000000 AS Time), CAST(0x00685B0000000000 AS Time), NULL, NULL, 900, 900, NULL, CAST(0x5E380B00 AS Date), 7, 45, 38, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (256, CAST(0x00A8930000000000 AS Time), CAST(0x00685B0000000000 AS Time), NULL, NULL, 900, 900, NULL, CAST(0x5F380B00 AS Date), 7, 45, 38, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (257, CAST(0x00A8930000000000 AS Time), CAST(0x00685B0000000000 AS Time), NULL, NULL, 900, 900, NULL, CAST(0x60380B00 AS Date), 7, 45, 38, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (258, CAST(0x00C8AF0000000000 AS Time), CAST(0x0088770000000000 AS Time), NULL, NULL, 900, 900, NULL, CAST(0x5A380B00 AS Date), 7, 46, 39, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (259, CAST(0x00C8AF0000000000 AS Time), CAST(0x0088770000000000 AS Time), NULL, NULL, 900, 900, NULL, CAST(0x5B380B00 AS Date), 7, 46, 39, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (260, CAST(0x00C8AF0000000000 AS Time), CAST(0x0088770000000000 AS Time), NULL, NULL, 900, 900, NULL, CAST(0x5C380B00 AS Date), 7, 46, 39, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (261, CAST(0x00C8AF0000000000 AS Time), CAST(0x0088770000000000 AS Time), NULL, NULL, 900, 900, NULL, CAST(0x5D380B00 AS Date), 7, 46, 39, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (262, CAST(0x00C8AF0000000000 AS Time), CAST(0x0088770000000000 AS Time), NULL, NULL, 900, 900, NULL, CAST(0x5E380B00 AS Date), 7, 46, 39, 1, N'Chưa chạy')
GO
print 'Processed 200 total records'
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (263, CAST(0x00C8AF0000000000 AS Time), CAST(0x0088770000000000 AS Time), NULL, NULL, 900, 900, NULL, CAST(0x5F380B00 AS Date), 7, 46, 39, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (264, CAST(0x00C8AF0000000000 AS Time), CAST(0x0088770000000000 AS Time), NULL, NULL, 900, 900, NULL, CAST(0x60380B00 AS Date), 7, 46, 39, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (265, CAST(0x00E8CB0000000000 AS Time), CAST(0x00A8930000000000 AS Time), NULL, NULL, 900, 900, NULL, CAST(0x5A380B00 AS Date), 7, 47, 40, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (266, CAST(0x00E8CB0000000000 AS Time), CAST(0x00A8930000000000 AS Time), NULL, NULL, 900, 900, NULL, CAST(0x5B380B00 AS Date), 7, 47, 40, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (267, CAST(0x00E8CB0000000000 AS Time), CAST(0x00A8930000000000 AS Time), NULL, NULL, 900, 900, NULL, CAST(0x5C380B00 AS Date), 7, 47, 40, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (268, CAST(0x00E8CB0000000000 AS Time), CAST(0x00A8930000000000 AS Time), NULL, NULL, 900, 900, NULL, CAST(0x5D380B00 AS Date), 7, 47, 40, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (269, CAST(0x00E8CB0000000000 AS Time), CAST(0x00A8930000000000 AS Time), NULL, NULL, 900, 900, NULL, CAST(0x5E380B00 AS Date), 7, 47, 40, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (270, CAST(0x00E8CB0000000000 AS Time), CAST(0x00A8930000000000 AS Time), NULL, NULL, 900, 900, NULL, CAST(0x5F380B00 AS Date), 7, 47, 40, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (271, CAST(0x00E8CB0000000000 AS Time), CAST(0x00A8930000000000 AS Time), NULL, NULL, 900, 900, NULL, CAST(0x60380B00 AS Date), 7, 47, 40, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (272, CAST(0x0008E80000000000 AS Time), CAST(0x00C8AF0000000000 AS Time), NULL, NULL, 900, 900, NULL, CAST(0x5A380B00 AS Date), 7, 48, 41, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (273, CAST(0x0008E80000000000 AS Time), CAST(0x00C8AF0000000000 AS Time), NULL, NULL, 900, 900, NULL, CAST(0x5B380B00 AS Date), 7, 48, 41, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (274, CAST(0x0008E80000000000 AS Time), CAST(0x00C8AF0000000000 AS Time), NULL, NULL, 900, 900, NULL, CAST(0x5C380B00 AS Date), 7, 48, 41, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (275, CAST(0x0008E80000000000 AS Time), CAST(0x00C8AF0000000000 AS Time), NULL, NULL, 900, 900, NULL, CAST(0x5D380B00 AS Date), 7, 48, 41, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (276, CAST(0x0008E80000000000 AS Time), CAST(0x00C8AF0000000000 AS Time), NULL, NULL, 900, 900, NULL, CAST(0x5E380B00 AS Date), 7, 48, 41, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (277, CAST(0x0008E80000000000 AS Time), CAST(0x00C8AF0000000000 AS Time), NULL, NULL, 900, 900, NULL, CAST(0x5F380B00 AS Date), 7, 48, 41, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (278, CAST(0x0008E80000000000 AS Time), CAST(0x00C8AF0000000000 AS Time), NULL, NULL, 900, 900, NULL, CAST(0x60380B00 AS Date), 7, 48, 41, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (279, CAST(0x0028040100000000 AS Time), CAST(0x00E8CB0000000000 AS Time), NULL, NULL, 900, 900, NULL, CAST(0x5A380B00 AS Date), 7, 49, 42, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (280, CAST(0x0028040100000000 AS Time), CAST(0x00E8CB0000000000 AS Time), NULL, NULL, 900, 900, NULL, CAST(0x5B380B00 AS Date), 7, 49, 42, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (281, CAST(0x0028040100000000 AS Time), CAST(0x00E8CB0000000000 AS Time), NULL, NULL, 900, 900, NULL, CAST(0x5C380B00 AS Date), 7, 49, 42, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (282, CAST(0x0028040100000000 AS Time), CAST(0x00E8CB0000000000 AS Time), NULL, NULL, 900, 900, NULL, CAST(0x5D380B00 AS Date), 7, 49, 42, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (283, CAST(0x0028040100000000 AS Time), CAST(0x00E8CB0000000000 AS Time), NULL, NULL, 900, 900, NULL, CAST(0x5E380B00 AS Date), 7, 49, 42, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (284, CAST(0x0028040100000000 AS Time), CAST(0x00E8CB0000000000 AS Time), NULL, NULL, 900, 900, NULL, CAST(0x5F380B00 AS Date), 7, 49, 42, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (285, CAST(0x0028040100000000 AS Time), CAST(0x00E8CB0000000000 AS Time), NULL, NULL, 900, 900, NULL, CAST(0x60380B00 AS Date), 7, 49, 42, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (286, CAST(0x0080700000000000 AS Time), CAST(0x00C8AF0000000000 AS Time), NULL, NULL, 2040, 2040, NULL, CAST(0x5A380B00 AS Date), 16, 15, 43, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (287, CAST(0x0080700000000000 AS Time), CAST(0x00C8AF0000000000 AS Time), NULL, NULL, 2040, 2040, NULL, CAST(0x5B380B00 AS Date), 16, 15, 43, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (288, CAST(0x0080700000000000 AS Time), CAST(0x00C8AF0000000000 AS Time), NULL, NULL, 2040, 2040, NULL, CAST(0x5C380B00 AS Date), 16, 15, 43, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (289, CAST(0x0080700000000000 AS Time), CAST(0x00C8AF0000000000 AS Time), NULL, NULL, 2040, 2040, NULL, CAST(0x5D380B00 AS Date), 16, 15, 43, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (290, CAST(0x0080700000000000 AS Time), CAST(0x00C8AF0000000000 AS Time), NULL, NULL, 2040, 2040, NULL, CAST(0x5E380B00 AS Date), 16, 15, 43, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (291, CAST(0x0080700000000000 AS Time), CAST(0x00C8AF0000000000 AS Time), NULL, NULL, 2040, 2040, NULL, CAST(0x5F380B00 AS Date), 16, 15, 43, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (292, CAST(0x0080700000000000 AS Time), CAST(0x00C8AF0000000000 AS Time), NULL, NULL, 2040, 2040, NULL, CAST(0x60380B00 AS Date), 16, 15, 43, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (293, CAST(0x00302A0000000000 AS Time), CAST(0x0078690000000000 AS Time), NULL, NULL, 2040, 2040, NULL, CAST(0x5A380B00 AS Date), 16, 16, 44, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (294, CAST(0x00302A0000000000 AS Time), CAST(0x0078690000000000 AS Time), NULL, NULL, 2040, 2040, NULL, CAST(0x5B380B00 AS Date), 16, 16, 44, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (295, CAST(0x00302A0000000000 AS Time), CAST(0x0078690000000000 AS Time), NULL, NULL, 2040, 2040, NULL, CAST(0x5C380B00 AS Date), 16, 16, 44, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (296, CAST(0x00302A0000000000 AS Time), CAST(0x0078690000000000 AS Time), NULL, NULL, 2040, 2040, NULL, CAST(0x5D380B00 AS Date), 16, 16, 44, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (297, CAST(0x00302A0000000000 AS Time), CAST(0x0078690000000000 AS Time), NULL, NULL, 2040, 2040, NULL, CAST(0x5E380B00 AS Date), 16, 16, 44, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (298, CAST(0x00302A0000000000 AS Time), CAST(0x0078690000000000 AS Time), NULL, NULL, 2040, 2040, NULL, CAST(0x5F380B00 AS Date), 16, 16, 44, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (299, CAST(0x00302A0000000000 AS Time), CAST(0x0078690000000000 AS Time), NULL, NULL, 2040, 2040, NULL, CAST(0x60380B00 AS Date), 16, 16, 44, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (300, CAST(0x0048200100000000 AS Time), CAST(0x0088770000000000 AS Time), NULL, NULL, 3060, 3060, NULL, CAST(0x5A380B00 AS Date), 23, 67, 45, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (301, CAST(0x0048200100000000 AS Time), CAST(0x0088770000000000 AS Time), NULL, NULL, 3060, 3060, NULL, CAST(0x5B380B00 AS Date), 23, 67, 45, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (302, CAST(0x0048200100000000 AS Time), CAST(0x0088770000000000 AS Time), NULL, NULL, 3060, 3060, NULL, CAST(0x5C380B00 AS Date), 23, 67, 45, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (303, CAST(0x0048200100000000 AS Time), CAST(0x0088770000000000 AS Time), NULL, NULL, 3060, 3060, NULL, CAST(0x5D380B00 AS Date), 23, 67, 45, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (304, CAST(0x0048200100000000 AS Time), CAST(0x0088770000000000 AS Time), NULL, NULL, 3060, 3060, NULL, CAST(0x5E380B00 AS Date), 23, 67, 45, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (305, CAST(0x0048200100000000 AS Time), CAST(0x0088770000000000 AS Time), NULL, NULL, 3060, 3060, NULL, CAST(0x5F380B00 AS Date), 23, 67, 45, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (306, CAST(0x0048200100000000 AS Time), CAST(0x0088770000000000 AS Time), NULL, NULL, 3060, 3060, NULL, CAST(0x60380B00 AS Date), 23, 67, 45, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (307, CAST(0x00E4570000000000 AS Time), CAST(0x00A4000100000000 AS Time), NULL, NULL, 3060, 3060, NULL, CAST(0x5A380B00 AS Date), 23, 68, 46, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (308, CAST(0x00E4570000000000 AS Time), CAST(0x00A4000100000000 AS Time), NULL, NULL, 3060, 3060, NULL, CAST(0x5B380B00 AS Date), 23, 68, 46, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (309, CAST(0x00E4570000000000 AS Time), CAST(0x00A4000100000000 AS Time), NULL, NULL, 3060, 3060, NULL, CAST(0x5C380B00 AS Date), 23, 68, 46, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (310, CAST(0x00E4570000000000 AS Time), CAST(0x00A4000100000000 AS Time), NULL, NULL, 3060, 3060, NULL, CAST(0x5D380B00 AS Date), 23, 68, 46, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (311, CAST(0x00E4570000000000 AS Time), CAST(0x00A4000100000000 AS Time), NULL, NULL, 3060, 3060, NULL, CAST(0x5E380B00 AS Date), 23, 68, 46, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (312, CAST(0x00E4570000000000 AS Time), CAST(0x00A4000100000000 AS Time), NULL, NULL, 3060, 3060, NULL, CAST(0x5F380B00 AS Date), 23, 68, 46, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (313, CAST(0x00E4570000000000 AS Time), CAST(0x00A4000100000000 AS Time), NULL, NULL, 3060, 3060, NULL, CAST(0x60380B00 AS Date), 23, 68, 46, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (314, CAST(0x0080700000000000 AS Time), CAST(0x00B09A0000000000 AS Time), NULL, NULL, 2400, 2400, NULL, CAST(0x5A380B00 AS Date), 1, 1, 47, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (315, CAST(0x0080700000000000 AS Time), CAST(0x00B09A0000000000 AS Time), NULL, NULL, 2400, 2400, NULL, CAST(0x5B380B00 AS Date), 1, 1, 47, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (316, CAST(0x0080700000000000 AS Time), CAST(0x00B09A0000000000 AS Time), NULL, NULL, 2400, 2400, NULL, CAST(0x5C380B00 AS Date), 1, 1, 47, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (317, CAST(0x0080700000000000 AS Time), CAST(0x00B09A0000000000 AS Time), NULL, NULL, 2400, 2400, NULL, CAST(0x5D380B00 AS Date), 1, 1, 47, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (318, CAST(0x0080700000000000 AS Time), CAST(0x00B09A0000000000 AS Time), NULL, NULL, 2400, 2400, NULL, CAST(0x5E380B00 AS Date), 1, 1, 47, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (319, CAST(0x0080700000000000 AS Time), CAST(0x00B09A0000000000 AS Time), NULL, NULL, 2400, 2400, NULL, CAST(0x5F380B00 AS Date), 1, 1, 47, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (320, CAST(0x0080700000000000 AS Time), CAST(0x00B09A0000000000 AS Time), NULL, NULL, 2400, 2400, NULL, CAST(0x60380B00 AS Date), 1, 1, 47, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (321, CAST(0x0010EF0000000000 AS Time), CAST(0x0040190100000000 AS Time), NULL, NULL, 2400, 2400, NULL, CAST(0x5A380B00 AS Date), 1, 2, 48, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (322, CAST(0x0010EF0000000000 AS Time), CAST(0x0040190100000000 AS Time), NULL, NULL, 2400, 2400, NULL, CAST(0x5B380B00 AS Date), 1, 2, 48, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (323, CAST(0x0010EF0000000000 AS Time), CAST(0x0040190100000000 AS Time), NULL, NULL, 2400, 2400, NULL, CAST(0x5C380B00 AS Date), 1, 2, 48, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (324, CAST(0x0010EF0000000000 AS Time), CAST(0x0040190100000000 AS Time), NULL, NULL, 2400, 2400, NULL, CAST(0x5D380B00 AS Date), 1, 2, 48, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (325, CAST(0x0010EF0000000000 AS Time), CAST(0x0040190100000000 AS Time), NULL, NULL, 2400, 2400, NULL, CAST(0x5E380B00 AS Date), 1, 2, 48, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (326, CAST(0x0010EF0000000000 AS Time), CAST(0x0040190100000000 AS Time), NULL, NULL, 2400, 2400, NULL, CAST(0x5F380B00 AS Date), 1, 2, 48, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (327, CAST(0x0010EF0000000000 AS Time), CAST(0x0040190100000000 AS Time), NULL, NULL, 2400, 2400, NULL, CAST(0x60380B00 AS Date), 1, 2, 48, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (328, CAST(0x0088770000000000 AS Time), CAST(0x00C8AF0000000000 AS Time), NULL, NULL, 2475, 2475, NULL, CAST(0x5A380B00 AS Date), 15, 157, 49, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (329, CAST(0x0088770000000000 AS Time), CAST(0x00C8AF0000000000 AS Time), NULL, NULL, 2475, 2475, NULL, CAST(0x5B380B00 AS Date), 15, 157, 49, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (330, CAST(0x0088770000000000 AS Time), CAST(0x00C8AF0000000000 AS Time), NULL, NULL, 2475, 2475, NULL, CAST(0x5C380B00 AS Date), 15, 157, 49, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (331, CAST(0x0088770000000000 AS Time), CAST(0x00C8AF0000000000 AS Time), NULL, NULL, 2475, 2475, NULL, CAST(0x5D380B00 AS Date), 15, 157, 49, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (332, CAST(0x0088770000000000 AS Time), CAST(0x00C8AF0000000000 AS Time), NULL, NULL, 2475, 2475, NULL, CAST(0x5E380B00 AS Date), 15, 157, 49, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (333, CAST(0x0088770000000000 AS Time), CAST(0x00C8AF0000000000 AS Time), NULL, NULL, 2475, 2475, NULL, CAST(0x5F380B00 AS Date), 15, 157, 49, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (334, CAST(0x0088770000000000 AS Time), CAST(0x00C8AF0000000000 AS Time), NULL, NULL, 2475, 2475, NULL, CAST(0x60380B00 AS Date), 15, 157, 49, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (335, CAST(0x00C0A80000000000 AS Time), CAST(0x0000E10000000000 AS Time), NULL, NULL, 2475, 2475, NULL, CAST(0x5A380B00 AS Date), 15, 158, 50, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (336, CAST(0x00C0A80000000000 AS Time), CAST(0x0000E10000000000 AS Time), NULL, NULL, 2475, 2475, NULL, CAST(0x5B380B00 AS Date), 15, 158, 50, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (337, CAST(0x00C0A80000000000 AS Time), CAST(0x0000E10000000000 AS Time), NULL, NULL, 2475, 2475, NULL, CAST(0x5C380B00 AS Date), 15, 158, 50, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (338, CAST(0x00C0A80000000000 AS Time), CAST(0x0000E10000000000 AS Time), NULL, NULL, 2475, 2475, NULL, CAST(0x5D380B00 AS Date), 15, 158, 50, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (339, CAST(0x00C0A80000000000 AS Time), CAST(0x0000E10000000000 AS Time), NULL, NULL, 2475, 2475, NULL, CAST(0x5E380B00 AS Date), 15, 158, 50, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (340, CAST(0x00C0A80000000000 AS Time), CAST(0x0000E10000000000 AS Time), NULL, NULL, 2475, 2475, NULL, CAST(0x5F380B00 AS Date), 15, 158, 50, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (341, CAST(0x00C0A80000000000 AS Time), CAST(0x0000E10000000000 AS Time), NULL, NULL, 2475, 2475, NULL, CAST(0x60380B00 AS Date), 15, 158, 50, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (342, CAST(0x0088770000000000 AS Time), CAST(0x00E0C40000000000 AS Time), NULL, NULL, 1260.0000000000002, 1260.0000000000002, NULL, CAST(0x5A380B00 AS Date), 12, 167, 56, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (343, CAST(0x0088770000000000 AS Time), CAST(0x00E0C40000000000 AS Time), NULL, NULL, 1260.0000000000002, 1260.0000000000002, NULL, CAST(0x5B380B00 AS Date), 12, 167, 56, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (344, CAST(0x0088770000000000 AS Time), CAST(0x00E0C40000000000 AS Time), NULL, NULL, 1260.0000000000002, 1260.0000000000002, NULL, CAST(0x5C380B00 AS Date), 12, 167, 56, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (345, CAST(0x0088770000000000 AS Time), CAST(0x00E0C40000000000 AS Time), NULL, NULL, 1260.0000000000002, 1260.0000000000002, NULL, CAST(0x5D380B00 AS Date), 12, 167, 56, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (346, CAST(0x0088770000000000 AS Time), CAST(0x00E0C40000000000 AS Time), NULL, NULL, 1260.0000000000002, 1260.0000000000002, NULL, CAST(0x5E380B00 AS Date), 12, 167, 56, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (347, CAST(0x0088770000000000 AS Time), CAST(0x00E0C40000000000 AS Time), NULL, NULL, 1260.0000000000002, 1260.0000000000002, NULL, CAST(0x5F380B00 AS Date), 12, 167, 56, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (348, CAST(0x0088770000000000 AS Time), CAST(0x00E0C40000000000 AS Time), NULL, NULL, 1260.0000000000002, 1260.0000000000002, NULL, CAST(0x60380B00 AS Date), 12, 167, 56, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (349, CAST(0x00F0D20000000000 AS Time), CAST(0x0048200100000000 AS Time), NULL, NULL, 1260.0000000000002, 1260.0000000000002, NULL, CAST(0x5A380B00 AS Date), 12, 168, 57, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (350, CAST(0x00F0D20000000000 AS Time), CAST(0x0048200100000000 AS Time), NULL, NULL, 1260.0000000000002, 1260.0000000000002, NULL, CAST(0x5B380B00 AS Date), 12, 168, 57, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (351, CAST(0x00F0D20000000000 AS Time), CAST(0x0048200100000000 AS Time), NULL, NULL, 1260.0000000000002, 1260.0000000000002, NULL, CAST(0x5C380B00 AS Date), 12, 168, 57, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (352, CAST(0x00F0D20000000000 AS Time), CAST(0x0048200100000000 AS Time), NULL, NULL, 1260.0000000000002, 1260.0000000000002, NULL, CAST(0x5D380B00 AS Date), 12, 168, 57, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (353, CAST(0x00F0D20000000000 AS Time), CAST(0x0048200100000000 AS Time), NULL, NULL, 1260.0000000000002, 1260.0000000000002, NULL, CAST(0x5E380B00 AS Date), 12, 168, 57, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (354, CAST(0x00F0D20000000000 AS Time), CAST(0x0048200100000000 AS Time), NULL, NULL, 1260.0000000000002, 1260.0000000000002, NULL, CAST(0x5F380B00 AS Date), 12, 168, 57, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (355, CAST(0x00F0D20000000000 AS Time), CAST(0x0048200100000000 AS Time), NULL, NULL, 1260.0000000000002, 1260.0000000000002, NULL, CAST(0x60380B00 AS Date), 12, 168, 57, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (356, CAST(0x00C0A80000000000 AS Time), CAST(0x0018F60000000000 AS Time), NULL, NULL, 1260.0000000000002, 1260.0000000000002, NULL, CAST(0x5A380B00 AS Date), 12, 169, 58, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (357, CAST(0x00C0A80000000000 AS Time), CAST(0x0018F60000000000 AS Time), NULL, NULL, 1260.0000000000002, 1260.0000000000002, NULL, CAST(0x5B380B00 AS Date), 12, 169, 58, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (358, CAST(0x00C0A80000000000 AS Time), CAST(0x0018F60000000000 AS Time), NULL, NULL, 1260.0000000000002, 1260.0000000000002, NULL, CAST(0x5C380B00 AS Date), 12, 169, 58, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (359, CAST(0x00C0A80000000000 AS Time), CAST(0x0018F60000000000 AS Time), NULL, NULL, 1260.0000000000002, 1260.0000000000002, NULL, CAST(0x5D380B00 AS Date), 12, 169, 58, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (360, CAST(0x00C0A80000000000 AS Time), CAST(0x0018F60000000000 AS Time), NULL, NULL, 1260.0000000000002, 1260.0000000000002, NULL, CAST(0x5E380B00 AS Date), 12, 169, 58, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (361, CAST(0x00C0A80000000000 AS Time), CAST(0x0018F60000000000 AS Time), NULL, NULL, 1260.0000000000002, 1260.0000000000002, NULL, CAST(0x5F380B00 AS Date), 12, 169, 58, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (362, CAST(0x00C0A80000000000 AS Time), CAST(0x0018F60000000000 AS Time), NULL, NULL, 1260.0000000000002, 1260.0000000000002, NULL, CAST(0x60380B00 AS Date), 12, 169, 58, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (363, CAST(0x0070620000000000 AS Time), CAST(0x00B09A0000000000 AS Time), NULL, NULL, 2475, 2475, NULL, CAST(0x5A380B00 AS Date), 19, 162, 51, 1, N'Chưa chạy')
GO
print 'Processed 300 total records'
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (364, CAST(0x0070620000000000 AS Time), CAST(0x00B09A0000000000 AS Time), NULL, NULL, 2475, 2475, NULL, CAST(0x5B380B00 AS Date), 19, 162, 51, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (365, CAST(0x0070620000000000 AS Time), CAST(0x00B09A0000000000 AS Time), NULL, NULL, 2475, 2475, NULL, CAST(0x5C380B00 AS Date), 19, 162, 51, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (366, CAST(0x0070620000000000 AS Time), CAST(0x00B09A0000000000 AS Time), NULL, NULL, 2475, 2475, NULL, CAST(0x5D380B00 AS Date), 19, 162, 51, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (367, CAST(0x0070620000000000 AS Time), CAST(0x00B09A0000000000 AS Time), NULL, NULL, 2475, 2475, NULL, CAST(0x5E380B00 AS Date), 19, 162, 51, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (368, CAST(0x0070620000000000 AS Time), CAST(0x00B09A0000000000 AS Time), NULL, NULL, 2475, 2475, NULL, CAST(0x5F380B00 AS Date), 19, 162, 51, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (369, CAST(0x0070620000000000 AS Time), CAST(0x00B09A0000000000 AS Time), NULL, NULL, 2475, 2475, NULL, CAST(0x60380B00 AS Date), 19, 162, 51, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (370, CAST(0x0080700000000000 AS Time), CAST(0x00C0A80000000000 AS Time), NULL, NULL, 2475, 2475, NULL, CAST(0x5A380B00 AS Date), 19, 163, 52, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (371, CAST(0x0080700000000000 AS Time), CAST(0x00C0A80000000000 AS Time), NULL, NULL, 2475, 2475, NULL, CAST(0x5B380B00 AS Date), 19, 163, 52, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (372, CAST(0x0080700000000000 AS Time), CAST(0x00C0A80000000000 AS Time), NULL, NULL, 2475, 2475, NULL, CAST(0x5C380B00 AS Date), 19, 163, 52, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (373, CAST(0x0080700000000000 AS Time), CAST(0x00C0A80000000000 AS Time), NULL, NULL, 2475, 2475, NULL, CAST(0x5D380B00 AS Date), 19, 163, 52, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (374, CAST(0x0080700000000000 AS Time), CAST(0x00C0A80000000000 AS Time), NULL, NULL, 2475, 2475, NULL, CAST(0x5E380B00 AS Date), 19, 163, 52, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (375, CAST(0x0080700000000000 AS Time), CAST(0x00C0A80000000000 AS Time), NULL, NULL, 2475, 2475, NULL, CAST(0x5F380B00 AS Date), 19, 163, 52, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (376, CAST(0x0080700000000000 AS Time), CAST(0x00C0A80000000000 AS Time), NULL, NULL, 2475, 2475, NULL, CAST(0x60380B00 AS Date), 19, 163, 52, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (377, CAST(0x00907E0000000000 AS Time), CAST(0x00D0B60000000000 AS Time), NULL, NULL, 2475, 2475, NULL, CAST(0x5A380B00 AS Date), 19, 164, 53, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (378, CAST(0x00907E0000000000 AS Time), CAST(0x00D0B60000000000 AS Time), NULL, NULL, 2475, 2475, NULL, CAST(0x5B380B00 AS Date), 19, 164, 53, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (379, CAST(0x00907E0000000000 AS Time), CAST(0x00D0B60000000000 AS Time), NULL, NULL, 2475, 2475, NULL, CAST(0x5C380B00 AS Date), 19, 164, 53, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (380, CAST(0x00907E0000000000 AS Time), CAST(0x00D0B60000000000 AS Time), NULL, NULL, 2475, 2475, NULL, CAST(0x5D380B00 AS Date), 19, 164, 53, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (381, CAST(0x00907E0000000000 AS Time), CAST(0x00D0B60000000000 AS Time), NULL, NULL, 2475, 2475, NULL, CAST(0x5E380B00 AS Date), 19, 164, 53, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (382, CAST(0x00907E0000000000 AS Time), CAST(0x00D0B60000000000 AS Time), NULL, NULL, 2475, 2475, NULL, CAST(0x5F380B00 AS Date), 19, 164, 53, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (383, CAST(0x00907E0000000000 AS Time), CAST(0x00D0B60000000000 AS Time), NULL, NULL, 2475, 2475, NULL, CAST(0x60380B00 AS Date), 19, 164, 53, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (384, CAST(0x00F0D20000000000 AS Time), CAST(0x00300B0100000000 AS Time), NULL, NULL, 2475, 2475, NULL, CAST(0x5A380B00 AS Date), 19, 165, 54, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (385, CAST(0x00F0D20000000000 AS Time), CAST(0x00300B0100000000 AS Time), NULL, NULL, 2475, 2475, NULL, CAST(0x5B380B00 AS Date), 19, 165, 54, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (386, CAST(0x00F0D20000000000 AS Time), CAST(0x00300B0100000000 AS Time), NULL, NULL, 2475, 2475, NULL, CAST(0x5C380B00 AS Date), 19, 165, 54, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (387, CAST(0x00F0D20000000000 AS Time), CAST(0x00300B0100000000 AS Time), NULL, NULL, 2475, 2475, NULL, CAST(0x5D380B00 AS Date), 19, 165, 54, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (388, CAST(0x00F0D20000000000 AS Time), CAST(0x00300B0100000000 AS Time), NULL, NULL, 2475, 2475, NULL, CAST(0x5E380B00 AS Date), 19, 165, 54, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (389, CAST(0x00F0D20000000000 AS Time), CAST(0x00300B0100000000 AS Time), NULL, NULL, 2475, 2475, NULL, CAST(0x5F380B00 AS Date), 19, 165, 54, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (390, CAST(0x00F0D20000000000 AS Time), CAST(0x00300B0100000000 AS Time), NULL, NULL, 2475, 2475, NULL, CAST(0x60380B00 AS Date), 19, 165, 54, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (391, CAST(0x0000E10000000000 AS Time), CAST(0x0040190100000000 AS Time), NULL, NULL, 2475, 2475, NULL, CAST(0x5A380B00 AS Date), 19, 166, 55, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (392, CAST(0x0000E10000000000 AS Time), CAST(0x0040190100000000 AS Time), NULL, NULL, 2475, 2475, NULL, CAST(0x5B380B00 AS Date), 19, 166, 55, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (393, CAST(0x0000E10000000000 AS Time), CAST(0x0040190100000000 AS Time), NULL, NULL, 2475, 2475, NULL, CAST(0x5C380B00 AS Date), 19, 166, 55, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (394, CAST(0x0000E10000000000 AS Time), CAST(0x0040190100000000 AS Time), NULL, NULL, 2475, 2475, NULL, CAST(0x5D380B00 AS Date), 19, 166, 55, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (395, CAST(0x0000E10000000000 AS Time), CAST(0x0040190100000000 AS Time), NULL, NULL, 2475, 2475, NULL, CAST(0x5E380B00 AS Date), 19, 166, 55, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (396, CAST(0x0000E10000000000 AS Time), CAST(0x0040190100000000 AS Time), NULL, NULL, 2475, 2475, NULL, CAST(0x5F380B00 AS Date), 19, 166, 55, 1, N'Chưa chạy')
INSERT [dbo].[Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID], [ScheduleID], [IsActive], [Status]) VALUES (397, CAST(0x0000E10000000000 AS Time), CAST(0x0040190100000000 AS Time), NULL, NULL, 2475, 2475, NULL, CAST(0x60380B00 AS Date), 19, 166, 55, 1, N'Chưa chạy')
SET IDENTITY_INSERT [dbo].[Trip] OFF
/****** Object:  Table [dbo].[Invoice]    Script Date: 04/02/2014 12:50:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoice](
	[InvoiceID] [int] IDENTITY(1,1) NOT NULL,
	[Weight] [float] NULL,
	[Volume] [float] NULL,
	[Price] [float] NOT NULL,
	[RequestID] [int] NOT NULL,
 CONSTRAINT [PK_Invoice] PRIMARY KEY CLUSTERED 
(
	[InvoiceID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Assigning]    Script Date: 04/02/2014 12:50:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Assigning](
	[TripID] [int] NOT NULL,
	[RequestID] [int] NOT NULL,
	[IndicateOrder] [int] NULL,
	[AssignedDate] [date] NULL,
	[StopStation] [int] NULL,
 CONSTRAINT [PK_Assigning] PRIMARY KEY CLUSTERED 
(
	[TripID] ASC,
	[RequestID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_Coach_CoachType]    Script Date: 04/02/2014 12:50:14 ******/
ALTER TABLE [dbo].[Coach]  WITH CHECK ADD  CONSTRAINT [FK_Coach_CoachType] FOREIGN KEY([CoachTypeID])
REFERENCES [dbo].[CoachType] ([CoachTypeID])
GO
ALTER TABLE [dbo].[Coach] CHECK CONSTRAINT [FK_Coach_CoachType]
GO
/****** Object:  ForeignKey [FK_StationInProvince]    Script Date: 04/02/2014 12:50:14 ******/
ALTER TABLE [dbo].[Station]  WITH CHECK ADD  CONSTRAINT [FK_StationInProvince] FOREIGN KEY([ProvinceID])
REFERENCES [dbo].[Province] ([ProvinceID])
GO
ALTER TABLE [dbo].[Station] CHECK CONSTRAINT [FK_StationInProvince]
GO
/****** Object:  ForeignKey [FK_EndStationOfStage]    Script Date: 04/02/2014 12:50:14 ******/
ALTER TABLE [dbo].[Stage]  WITH CHECK ADD  CONSTRAINT [FK_EndStationOfStage] FOREIGN KEY([EndPoint])
REFERENCES [dbo].[Station] ([StationID])
GO
ALTER TABLE [dbo].[Stage] CHECK CONSTRAINT [FK_EndStationOfStage]
GO
/****** Object:  ForeignKey [FK_StartStationOfStage]    Script Date: 04/02/2014 12:50:14 ******/
ALTER TABLE [dbo].[Stage]  WITH CHECK ADD  CONSTRAINT [FK_StartStationOfStage] FOREIGN KEY([StartPoint])
REFERENCES [dbo].[Station] ([StationID])
GO
ALTER TABLE [dbo].[Stage] CHECK CONSTRAINT [FK_StartStationOfStage]
GO
/****** Object:  ForeignKey [FK_CoachArrangement_Coach]    Script Date: 04/02/2014 12:50:14 ******/
ALTER TABLE [dbo].[CoachArrangement]  WITH CHECK ADD  CONSTRAINT [FK_CoachArrangement_Coach] FOREIGN KEY([CoachID])
REFERENCES [dbo].[Coach] ([CoachID])
GO
ALTER TABLE [dbo].[CoachArrangement] CHECK CONSTRAINT [FK_CoachArrangement_Coach]
GO
/****** Object:  ForeignKey [FK_CoachArrangement_Route]    Script Date: 04/02/2014 12:50:14 ******/
ALTER TABLE [dbo].[CoachArrangement]  WITH NOCHECK ADD  CONSTRAINT [FK_CoachArrangement_Route] FOREIGN KEY([RouteID])
REFERENCES [dbo].[Route] ([RouteID])
NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[CoachArrangement] CHECK CONSTRAINT [FK_CoachArrangement_Route]
GO
/****** Object:  ForeignKey [FK_StaffOfStation]    Script Date: 04/02/2014 12:50:14 ******/
ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [FK_StaffOfStation] FOREIGN KEY([StationID])
REFERENCES [dbo].[Station] ([StationID])
GO
ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [FK_StaffOfStation]
GO
/****** Object:  ForeignKey [FK_Comment_Account]    Script Date: 04/02/2014 12:50:14 ******/
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_Account] FOREIGN KEY([Username])
REFERENCES [dbo].[Account] ([Username])
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_Account]
GO
/****** Object:  ForeignKey [FK_CommentForStation]    Script Date: 04/02/2014 12:50:14 ******/
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_CommentForStation] FOREIGN KEY([StationID])
REFERENCES [dbo].[Station] ([StationID])
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_CommentForStation]
GO
/****** Object:  ForeignKey [FK_Notification_Account]    Script Date: 04/02/2014 12:50:14 ******/
ALTER TABLE [dbo].[Notification]  WITH CHECK ADD  CONSTRAINT [FK_Notification_Account] FOREIGN KEY([Username])
REFERENCES [dbo].[Account] ([Username])
GO
ALTER TABLE [dbo].[Notification] CHECK CONSTRAINT [FK_Notification_Account]
GO
/****** Object:  ForeignKey [FK_NotificationForStation]    Script Date: 04/02/2014 12:50:14 ******/
ALTER TABLE [dbo].[Notification]  WITH CHECK ADD  CONSTRAINT [FK_NotificationForStation] FOREIGN KEY([StationID])
REFERENCES [dbo].[Station] ([StationID])
GO
ALTER TABLE [dbo].[Notification] CHECK CONSTRAINT [FK_NotificationForStation]
GO
/****** Object:  ForeignKey [FK_Schedule_CoachArrangement]    Script Date: 04/02/2014 12:50:14 ******/
ALTER TABLE [dbo].[Schedule]  WITH CHECK ADD  CONSTRAINT [FK_Schedule_CoachArrangement] FOREIGN KEY([ArrangeID])
REFERENCES [dbo].[CoachArrangement] ([ArrangeID])
GO
ALTER TABLE [dbo].[Schedule] CHECK CONSTRAINT [FK_Schedule_CoachArrangement]
GO
/****** Object:  ForeignKey [FK_RouteStage_Route]    Script Date: 04/02/2014 12:50:14 ******/
ALTER TABLE [dbo].[RouteStage]  WITH CHECK ADD  CONSTRAINT [FK_RouteStage_Route] FOREIGN KEY([RouteID])
REFERENCES [dbo].[Route] ([RouteID])
GO
ALTER TABLE [dbo].[RouteStage] CHECK CONSTRAINT [FK_RouteStage_Route]
GO
/****** Object:  ForeignKey [FK_RouteStage_Stage]    Script Date: 04/02/2014 12:50:14 ******/
ALTER TABLE [dbo].[RouteStage]  WITH CHECK ADD  CONSTRAINT [FK_RouteStage_Stage] FOREIGN KEY([StageID])
REFERENCES [dbo].[Stage] ([StageID])
GO
ALTER TABLE [dbo].[RouteStage] CHECK CONSTRAINT [FK_RouteStage_Stage]
GO
/****** Object:  ForeignKey [FK_Request_DeliveryStatus]    Script Date: 04/02/2014 12:50:14 ******/
ALTER TABLE [dbo].[Request]  WITH CHECK ADD  CONSTRAINT [FK_Request_DeliveryStatus] FOREIGN KEY([DeliveryStatusID])
REFERENCES [dbo].[DeliveryStatus] ([DeliveryStatusID])
GO
ALTER TABLE [dbo].[Request] CHECK CONSTRAINT [FK_Request_DeliveryStatus]
GO
/****** Object:  ForeignKey [FK_Request_FromStation]    Script Date: 04/02/2014 12:50:14 ******/
ALTER TABLE [dbo].[Request]  WITH CHECK ADD  CONSTRAINT [FK_Request_FromStation] FOREIGN KEY([FromLocation])
REFERENCES [dbo].[Station] ([StationID])
GO
ALTER TABLE [dbo].[Request] CHECK CONSTRAINT [FK_Request_FromStation]
GO
/****** Object:  ForeignKey [FK_Request_ManageFee]    Script Date: 04/02/2014 12:50:14 ******/
ALTER TABLE [dbo].[Request]  WITH CHECK ADD  CONSTRAINT [FK_Request_ManageFee] FOREIGN KEY([FeeID])
REFERENCES [dbo].[ManageFee] ([FeeID])
GO
ALTER TABLE [dbo].[Request] CHECK CONSTRAINT [FK_Request_ManageFee]
GO
/****** Object:  ForeignKey [FK_Request_ToStation]    Script Date: 04/02/2014 12:50:14 ******/
ALTER TABLE [dbo].[Request]  WITH CHECK ADD  CONSTRAINT [FK_Request_ToStation] FOREIGN KEY([ToLocation])
REFERENCES [dbo].[Station] ([StationID])
GO
ALTER TABLE [dbo].[Request] CHECK CONSTRAINT [FK_Request_ToStation]
GO
/****** Object:  ForeignKey [FK_RequestOfAccount]    Script Date: 04/02/2014 12:50:14 ******/
ALTER TABLE [dbo].[Request]  WITH CHECK ADD  CONSTRAINT [FK_RequestOfAccount] FOREIGN KEY([Username])
REFERENCES [dbo].[Account] ([Username])
GO
ALTER TABLE [dbo].[Request] CHECK CONSTRAINT [FK_RequestOfAccount]
GO
/****** Object:  ForeignKey [FK_Rating_Account]    Script Date: 04/02/2014 12:50:14 ******/
ALTER TABLE [dbo].[Rating]  WITH CHECK ADD  CONSTRAINT [FK_Rating_Account] FOREIGN KEY([Username])
REFERENCES [dbo].[Account] ([Username])
GO
ALTER TABLE [dbo].[Rating] CHECK CONSTRAINT [FK_Rating_Account]
GO
/****** Object:  ForeignKey [FK_RatingForStation]    Script Date: 04/02/2014 12:50:14 ******/
ALTER TABLE [dbo].[Rating]  WITH CHECK ADD  CONSTRAINT [FK_RatingForStation] FOREIGN KEY([StationID])
REFERENCES [dbo].[Station] ([StationID])
GO
ALTER TABLE [dbo].[Rating] CHECK CONSTRAINT [FK_RatingForStation]
GO
/****** Object:  ForeignKey [FK_UserInfo_Account]    Script Date: 04/02/2014 12:50:14 ******/
ALTER TABLE [dbo].[UserInfo]  WITH CHECK ADD  CONSTRAINT [FK_UserInfo_Account] FOREIGN KEY([Username])
REFERENCES [dbo].[Account] ([Username])
GO
ALTER TABLE [dbo].[UserInfo] CHECK CONSTRAINT [FK_UserInfo_Account]
GO
/****** Object:  ForeignKey [FK_Trip_Schedule]    Script Date: 04/02/2014 12:50:14 ******/
ALTER TABLE [dbo].[Trip]  WITH CHECK ADD  CONSTRAINT [FK_Trip_Schedule] FOREIGN KEY([ScheduleID])
REFERENCES [dbo].[Schedule] ([ScheduleID])
GO
ALTER TABLE [dbo].[Trip] CHECK CONSTRAINT [FK_Trip_Schedule]
GO
/****** Object:  ForeignKey [FK_InvoiceOfRequest]    Script Date: 04/02/2014 12:50:14 ******/
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceOfRequest] FOREIGN KEY([RequestID])
REFERENCES [dbo].[Request] ([RequestID])
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_InvoiceOfRequest]
GO
/****** Object:  ForeignKey [FK_Assigning_Trip]    Script Date: 04/02/2014 12:50:14 ******/
ALTER TABLE [dbo].[Assigning]  WITH CHECK ADD  CONSTRAINT [FK_Assigning_Trip] FOREIGN KEY([TripID])
REFERENCES [dbo].[Trip] ([TripID])
GO
ALTER TABLE [dbo].[Assigning] CHECK CONSTRAINT [FK_Assigning_Trip]
GO
/****** Object:  ForeignKey [FK_AssigningForRequest]    Script Date: 04/02/2014 12:50:14 ******/
ALTER TABLE [dbo].[Assigning]  WITH CHECK ADD  CONSTRAINT [FK_AssigningForRequest] FOREIGN KEY([RequestID])
REFERENCES [dbo].[Request] ([RequestID])
GO
ALTER TABLE [dbo].[Assigning] CHECK CONSTRAINT [FK_AssigningForRequest]
GO
