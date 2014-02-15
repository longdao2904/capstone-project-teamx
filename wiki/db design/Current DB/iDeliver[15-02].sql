USE [master]
GO
/****** Object:  Database [iDeliver]    Script Date: 02/15/2014 16:57:51 ******/
CREATE DATABASE [iDeliver] ON  PRIMARY 
( NAME = N'iDeliver', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\iDeliver.mdf' , SIZE = 2304KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'iDeliver_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\iDeliver_log.LDF' , SIZE = 576KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
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
ALTER DATABASE [iDeliver] SET RECOVERY FULL
GO
ALTER DATABASE [iDeliver] SET  MULTI_USER
GO
ALTER DATABASE [iDeliver] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [iDeliver] SET DB_CHAINING OFF
GO
EXEC sys.sp_db_vardecimal_storage_format N'iDeliver', N'ON'
GO
USE [iDeliver]
GO
/****** Object:  Table [dbo].[ManageFee]    Script Date: 02/15/2014 16:57:53 ******/
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
INSERT [dbo].[ManageFee] ([FeeID], [Fee], [MinWeight], [MaxWeight], [MinVolume], [MaxVolume]) VALUES (2, 100000, 0.1, 1, 0.1, 100)
INSERT [dbo].[ManageFee] ([FeeID], [Fee], [MinWeight], [MaxWeight], [MinVolume], [MaxVolume]) VALUES (4, 200000, 1.1, 5, 100.1, 500)
INSERT [dbo].[ManageFee] ([FeeID], [Fee], [MinWeight], [MaxWeight], [MinVolume], [MaxVolume]) VALUES (5, 350000, 5.1, 10, 500.1, 1000)
INSERT [dbo].[ManageFee] ([FeeID], [Fee], [MinWeight], [MaxWeight], [MinVolume], [MaxVolume]) VALUES (6, 500000, 10.1, 50, 1000.1, 1500)
SET IDENTITY_INSERT [dbo].[ManageFee] OFF
/****** Object:  Table [dbo].[DeliveryStatus]    Script Date: 02/15/2014 16:57:53 ******/
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
/****** Object:  Table [dbo].[Station]    Script Date: 02/15/2014 16:57:53 ******/
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
	[BreakTime] [float] NULL,
	[StationPhone] [varchar](15) NULL,
	[Description] [nvarchar](500) NULL,
 CONSTRAINT [PK_Station] PRIMARY KEY CLUSTERED 
(
	[StationID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Account]    Script Date: 02/15/2014 16:57:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Account](
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Role] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Phone] [varchar](15) NOT NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[Account] ([Username], [Password], [Role], [Email], [Phone]) VALUES (N'duc', N'abc', N'Staff', N'duc@yahoo.com', N'09125934873')
INSERT [dbo].[Account] ([Username], [Password], [Role], [Email], [Phone]) VALUES (N'long', N'aaaa', N'Admin', N'vuhoanglong1992.lv@gmail.com', N'0123456789')
INSERT [dbo].[Account] ([Username], [Password], [Role], [Email], [Phone]) VALUES (N'longbv', N'123', N'Customer', N'longbv@yahoo.com', N'09696969696')
INSERT [dbo].[Account] ([Username], [Password], [Role], [Email], [Phone]) VALUES (N'lulp', N'admin', N'Staff', N'lulp@yahoo.com', N'01636987483')
INSERT [dbo].[Account] ([Username], [Password], [Role], [Email], [Phone]) VALUES (N'tungnt', N'iridescent', N'Staff', N'tungnt@yahoo.com', N'01628468737')
INSERT [dbo].[Account] ([Username], [Password], [Role], [Email], [Phone]) VALUES (N'VuHoangLong', N'aaaaa', N'Customer', N'vuhoanglong1992.lv@gmail.com', N'0123456789')
/****** Object:  Table [dbo].[CoachType]    Script Date: 02/15/2014 16:57:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CoachType](
	[CoachTypeID] [int] IDENTITY(1,1) NOT NULL,
	[Seats] [int] NOT NULL,
 CONSTRAINT [PK_CoachType] PRIMARY KEY CLUSTERED 
(
	[CoachTypeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Coach]    Script Date: 02/15/2014 16:57:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Coach](
	[CoachID] [int] IDENTITY(1,1) NOT NULL,
	[NumberPlate] [nvarchar](50) NOT NULL,
	[RouteID] [int] NULL,
	[CoachTypeID] [int] NOT NULL,
 CONSTRAINT [PK_Coach] PRIMARY KEY CLUSTERED 
(
	[CoachID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Route]    Script Date: 02/15/2014 16:57:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Route](
	[RouteID] [nvarchar](50) NOT NULL,
	[RouteName] [nvarchar](50) NULL,
	[StartPoint] [int] NULL,
	[EndPoint] [int] NULL,
	[AvgRateLevel] [float] NULL,
	[CoachID] [int] NULL,
	[Duration] [float] NULL,
	[Distance] [float] NULL,
 CONSTRAINT [PK_Route] PRIMARY KEY CLUSTERED 
(
	[RouteID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Request]    Script Date: 02/15/2014 16:57:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Request](
	[RequestID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Status] [int] NULL,
	[FeeID] [int] NULL,
	[EstimateWeight] [float] NULL,
	[EstimateVolume] [float] NULL,
	[DateRequest] [date] NOT NULL,
	[Description] [nvarchar](500) NULL,
	[IsActive] [bit] NOT NULL,
	[FromLocation] [int] NOT NULL,
	[ToLocation] [int] NOT NULL,
	[SenderAddress] [nvarchar](500) NULL,
	[ReceiverAddress] [nvarchar](500) NULL,
 CONSTRAINT [PK_Request] PRIMARY KEY CLUSTERED 
(
	[RequestID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserInfo]    Script Date: 02/15/2014 16:57:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserInfo](
	[Username] [nvarchar](50) NOT NULL,
	[Firstname] [nvarchar](50) NULL,
	[Lastname] [nvarchar](50) NULL,
	[Address] [nvarchar](500) NULL,
 CONSTRAINT [PK_UserInfo_1] PRIMARY KEY CLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[UserInfo] ([Username], [Firstname], [Lastname], [Address]) VALUES (N'long', N'long', N'vu', N'phan đăng lưu')
INSERT [dbo].[UserInfo] ([Username], [Firstname], [Lastname], [Address]) VALUES (N'VuHoangLong', N'Long', N'Vũ', N'hehe')
/****** Object:  Table [dbo].[Volume]    Script Date: 02/15/2014 16:57:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Volume](
	[VolumeID] [int] IDENTITY(1,1) NOT NULL,
	[MaxVolume] [float] NULL,
	[MinVolume] [float] NULL,
	[RouteID] [nvarchar](50) NOT NULL,
	[CoachTypeID] [int] NOT NULL,
 CONSTRAINT [PK_Volume] PRIMARY KEY CLUSTERED 
(
	[VolumeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Trip]    Script Date: 02/15/2014 16:57:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Trip](
	[TripID] [int] IDENTITY(1,1) NOT NULL,
	[EstimateDepartureTime] [time](7) NULL,
	[EstimateArrivalTime] [time](7) NULL,
	[RealDepartureTime] [time](7) NULL,
	[RealArrivalTime] [time](7) NULL,
	[AvailableVolume] [float] NULL,
	[Date] [date] NOT NULL,
	[RouteID] [nvarchar](50) NOT NULL,
	[CoachID] [int] NOT NULL,
 CONSTRAINT [PK_Trip] PRIMARY KEY CLUSTERED 
(
	[TripID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comment]    Script Date: 02/15/2014 16:57:53 ******/
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
	[RouteID] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED 
(
	[CommentID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invoice]    Script Date: 02/15/2014 16:57:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoice](
	[InvoiceID] [int] IDENTITY(1,1) NOT NULL,
	[Weight] [float] NULL,
	[Volume] [float] NULL,
	[Price] [float] NOT NULL,
	[FeeID] [int] NULL,
	[RequestID] [int] NOT NULL,
 CONSTRAINT [PK_Invoice] PRIMARY KEY CLUSTERED 
(
	[InvoiceID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rating]    Script Date: 02/15/2014 16:57:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rating](
	[RatingID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[RateLevel] [int] NOT NULL,
	[RouteID] [int] NOT NULL,
 CONSTRAINT [PK_Rating] PRIMARY KEY CLUSTERED 
(
	[RatingID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Assigning]    Script Date: 02/15/2014 16:57:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Assigning](
	[TripID] [int] NOT NULL,
	[RequestID] [int] NOT NULL,
	[IndicateOrder] [int] NULL,
 CONSTRAINT [PK_Assigning] PRIMARY KEY CLUSTERED 
(
	[TripID] ASC,
	[RequestID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_Coach_CoachType]    Script Date: 02/15/2014 16:57:53 ******/
ALTER TABLE [dbo].[Coach]  WITH CHECK ADD  CONSTRAINT [FK_Coach_CoachType] FOREIGN KEY([CoachTypeID])
REFERENCES [dbo].[CoachType] ([CoachTypeID])
GO
ALTER TABLE [dbo].[Coach] CHECK CONSTRAINT [FK_Coach_CoachType]
GO
/****** Object:  ForeignKey [FK_Route_Station]    Script Date: 02/15/2014 16:57:53 ******/
ALTER TABLE [dbo].[Route]  WITH CHECK ADD  CONSTRAINT [FK_Route_Station] FOREIGN KEY([StartPoint])
REFERENCES [dbo].[Station] ([StationID])
GO
ALTER TABLE [dbo].[Route] CHECK CONSTRAINT [FK_Route_Station]
GO
/****** Object:  ForeignKey [FK_Route_Station1]    Script Date: 02/15/2014 16:57:53 ******/
ALTER TABLE [dbo].[Route]  WITH CHECK ADD  CONSTRAINT [FK_Route_Station1] FOREIGN KEY([EndPoint])
REFERENCES [dbo].[Station] ([StationID])
GO
ALTER TABLE [dbo].[Route] CHECK CONSTRAINT [FK_Route_Station1]
GO
/****** Object:  ForeignKey [FK_Request_Account]    Script Date: 02/15/2014 16:57:53 ******/
ALTER TABLE [dbo].[Request]  WITH CHECK ADD  CONSTRAINT [FK_Request_Account] FOREIGN KEY([Username])
REFERENCES [dbo].[Account] ([Username])
GO
ALTER TABLE [dbo].[Request] CHECK CONSTRAINT [FK_Request_Account]
GO
/****** Object:  ForeignKey [FK_Request_DeliveryStatus]    Script Date: 02/15/2014 16:57:53 ******/
ALTER TABLE [dbo].[Request]  WITH CHECK ADD  CONSTRAINT [FK_Request_DeliveryStatus] FOREIGN KEY([Status])
REFERENCES [dbo].[DeliveryStatus] ([DeliveryStatusID])
GO
ALTER TABLE [dbo].[Request] CHECK CONSTRAINT [FK_Request_DeliveryStatus]
GO
/****** Object:  ForeignKey [FK_Request_ManageFee]    Script Date: 02/15/2014 16:57:53 ******/
ALTER TABLE [dbo].[Request]  WITH CHECK ADD  CONSTRAINT [FK_Request_ManageFee] FOREIGN KEY([FeeID])
REFERENCES [dbo].[ManageFee] ([FeeID])
GO
ALTER TABLE [dbo].[Request] CHECK CONSTRAINT [FK_Request_ManageFee]
GO
/****** Object:  ForeignKey [FK_Request_Station]    Script Date: 02/15/2014 16:57:53 ******/
ALTER TABLE [dbo].[Request]  WITH CHECK ADD  CONSTRAINT [FK_Request_Station] FOREIGN KEY([FromLocation])
REFERENCES [dbo].[Station] ([StationID])
GO
ALTER TABLE [dbo].[Request] CHECK CONSTRAINT [FK_Request_Station]
GO
/****** Object:  ForeignKey [FK_Request_Station1]    Script Date: 02/15/2014 16:57:53 ******/
ALTER TABLE [dbo].[Request]  WITH CHECK ADD  CONSTRAINT [FK_Request_Station1] FOREIGN KEY([ToLocation])
REFERENCES [dbo].[Station] ([StationID])
GO
ALTER TABLE [dbo].[Request] CHECK CONSTRAINT [FK_Request_Station1]
GO
/****** Object:  ForeignKey [FK_UserInfo_Account]    Script Date: 02/15/2014 16:57:53 ******/
ALTER TABLE [dbo].[UserInfo]  WITH CHECK ADD  CONSTRAINT [FK_UserInfo_Account] FOREIGN KEY([Username])
REFERENCES [dbo].[Account] ([Username])
GO
ALTER TABLE [dbo].[UserInfo] CHECK CONSTRAINT [FK_UserInfo_Account]
GO
/****** Object:  ForeignKey [FK_Volume_CoachType]    Script Date: 02/15/2014 16:57:53 ******/
ALTER TABLE [dbo].[Volume]  WITH CHECK ADD  CONSTRAINT [FK_Volume_CoachType] FOREIGN KEY([CoachTypeID])
REFERENCES [dbo].[CoachType] ([CoachTypeID])
GO
ALTER TABLE [dbo].[Volume] CHECK CONSTRAINT [FK_Volume_CoachType]
GO
/****** Object:  ForeignKey [FK_Volume_Route1]    Script Date: 02/15/2014 16:57:53 ******/
ALTER TABLE [dbo].[Volume]  WITH CHECK ADD  CONSTRAINT [FK_Volume_Route1] FOREIGN KEY([RouteID])
REFERENCES [dbo].[Route] ([RouteID])
GO
ALTER TABLE [dbo].[Volume] CHECK CONSTRAINT [FK_Volume_Route1]
GO
/****** Object:  ForeignKey [FK_Trip_Coach]    Script Date: 02/15/2014 16:57:53 ******/
ALTER TABLE [dbo].[Trip]  WITH CHECK ADD  CONSTRAINT [FK_Trip_Coach] FOREIGN KEY([CoachID])
REFERENCES [dbo].[Coach] ([CoachID])
GO
ALTER TABLE [dbo].[Trip] CHECK CONSTRAINT [FK_Trip_Coach]
GO
/****** Object:  ForeignKey [FK_Trip_Route]    Script Date: 02/15/2014 16:57:53 ******/
ALTER TABLE [dbo].[Trip]  WITH CHECK ADD  CONSTRAINT [FK_Trip_Route] FOREIGN KEY([RouteID])
REFERENCES [dbo].[Route] ([RouteID])
GO
ALTER TABLE [dbo].[Trip] CHECK CONSTRAINT [FK_Trip_Route]
GO
/****** Object:  ForeignKey [FK_Comment_Account]    Script Date: 02/15/2014 16:57:53 ******/
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_Account] FOREIGN KEY([Username])
REFERENCES [dbo].[Account] ([Username])
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_Account]
GO
/****** Object:  ForeignKey [FK_Comment_Route]    Script Date: 02/15/2014 16:57:53 ******/
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_Route] FOREIGN KEY([RouteID])
REFERENCES [dbo].[Route] ([RouteID])
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_Route]
GO
/****** Object:  ForeignKey [FK_Invoice_ManageFee]    Script Date: 02/15/2014 16:57:53 ******/
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_ManageFee] FOREIGN KEY([FeeID])
REFERENCES [dbo].[ManageFee] ([FeeID])
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_ManageFee]
GO
/****** Object:  ForeignKey [FK_Invoice_Request]    Script Date: 02/15/2014 16:57:53 ******/
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_Request] FOREIGN KEY([RequestID])
REFERENCES [dbo].[Request] ([RequestID])
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_Request]
GO
/****** Object:  ForeignKey [FK_Rating_Account]    Script Date: 02/15/2014 16:57:53 ******/
ALTER TABLE [dbo].[Rating]  WITH CHECK ADD  CONSTRAINT [FK_Rating_Account] FOREIGN KEY([Username])
REFERENCES [dbo].[Account] ([Username])
GO
ALTER TABLE [dbo].[Rating] CHECK CONSTRAINT [FK_Rating_Account]
GO
/****** Object:  ForeignKey [FK_Rating_Route]    Script Date: 02/15/2014 16:57:53 ******/
ALTER TABLE [dbo].[Rating]  WITH CHECK ADD  CONSTRAINT [FK_Rating_Route] FOREIGN KEY([Username])
REFERENCES [dbo].[Route] ([RouteID])
GO
ALTER TABLE [dbo].[Rating] CHECK CONSTRAINT [FK_Rating_Route]
GO
/****** Object:  ForeignKey [FK_Assigning_Request]    Script Date: 02/15/2014 16:57:53 ******/
ALTER TABLE [dbo].[Assigning]  WITH CHECK ADD  CONSTRAINT [FK_Assigning_Request] FOREIGN KEY([RequestID])
REFERENCES [dbo].[Request] ([RequestID])
GO
ALTER TABLE [dbo].[Assigning] CHECK CONSTRAINT [FK_Assigning_Request]
GO
/****** Object:  ForeignKey [FK_Assigning_Trip]    Script Date: 02/15/2014 16:57:53 ******/
ALTER TABLE [dbo].[Assigning]  WITH CHECK ADD  CONSTRAINT [FK_Assigning_Trip] FOREIGN KEY([TripID])
REFERENCES [dbo].[Trip] ([TripID])
GO
ALTER TABLE [dbo].[Assigning] CHECK CONSTRAINT [FK_Assigning_Trip]
GO
