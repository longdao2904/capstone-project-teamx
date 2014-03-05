USE [iDeliver]
GO
INSERT [Account] ([Username], [Password], [BackupPassword], [Role], [Email], [Phone], [BannedStatus]) VALUES (N'customer1', N'customer1', N'customer1', N'Customer', N'customer1@gmail.com', N'0906425539', 0)
INSERT [Account] ([Username], [Password], [BackupPassword], [Role], [Email], [Phone], [BannedStatus]) VALUES (N'duc', N'abc', N'abc', N'Staff', N'duc@yahoo.com', N'09125934873', 0)
INSERT [Account] ([Username], [Password], [BackupPassword], [Role], [Email], [Phone], [BannedStatus]) VALUES (N'long', N'aaaa', N'aaaa', N'Admin', N'vuhoanglong1992.lv@gmail.com', N'0123456789', 0)
INSERT [Account] ([Username], [Password], [BackupPassword], [Role], [Email], [Phone], [BannedStatus]) VALUES (N'longbv', N'123', N'123', N'Customer', N'longbv@yahoo.com', N'09696969696', 1)
INSERT [Account] ([Username], [Password], [BackupPassword], [Role], [Email], [Phone], [BannedStatus]) VALUES (N'longdb', N'longdb', N'longdb', N'Customer', N'winvn92@gmail.com', N'0906425539', 0)
INSERT [Account] ([Username], [Password], [BackupPassword], [Role], [Email], [Phone], [BannedStatus]) VALUES (N'lulp', N'123456', N'123456', N'Staff', N'lulp@yahoo.com', N'01636987483', 0)
INSERT [Account] ([Username], [Password], [BackupPassword], [Role], [Email], [Phone], [BannedStatus]) VALUES (N'tungnt', N'iridescent', N'iridescent', N'Staff', N'tungnt@yahoo.com', N'01628468737', 0)
INSERT [Account] ([Username], [Password], [BackupPassword], [Role], [Email], [Phone], [BannedStatus]) VALUES (N'VuHoangLong', N'aaaaa', N'aaaaa', N'Customer', N'vuhoanglong1992.lv@gmail.com', N'0123456789', 0)
SET IDENTITY_INSERT [Province] ON
INSERT [Province] ([ProvinceID], [ProvinceName]) VALUES (1, N'Hồ Chí Minh')
INSERT [Province] ([ProvinceID], [ProvinceName]) VALUES (3, N'Nha Trang')
INSERT [Province] ([ProvinceID], [ProvinceName]) VALUES (4, N'Đà Lạt')
INSERT [Province] ([ProvinceID], [ProvinceName]) VALUES (5, N'Vũng Tàu')
INSERT [Province] ([ProvinceID], [ProvinceName]) VALUES (6, N'Đồng Tháp')
INSERT [Province] ([ProvinceID], [ProvinceName]) VALUES (7, N'Đồng Nai')
INSERT [Province] ([ProvinceID], [ProvinceName]) VALUES (8, N'An Giang')
INSERT [Province] ([ProvinceID], [ProvinceName]) VALUES (9, N'Tiền Giang')
INSERT [Province] ([ProvinceID], [ProvinceName]) VALUES (10, N'Cà Mau')
INSERT [Province] ([ProvinceID], [ProvinceName]) VALUES (11, N'Cần Thơ')
INSERT [Province] ([ProvinceID], [ProvinceName]) VALUES (12, N'Đà Nẵng')
INSERT [Province] ([ProvinceID], [ProvinceName]) VALUES (13, N'Kiên Giang')
INSERT [Province] ([ProvinceID], [ProvinceName]) VALUES (14, N'Hậu Giang')
SET IDENTITY_INSERT [Province] ON
SET IDENTITY_INSERT [DeliveryStatus] ON
INSERT [DeliveryStatus] ([DeliveryStatusID], [StatusName], [Note]) VALUES (1, N'Chờ xác nhận ', NULL)
INSERT [DeliveryStatus] ([DeliveryStatusID], [StatusName], [Note]) VALUES (2, N'Đã xếp lịch', N'Yêu cầu đã được sắp xếp để chuyển đi')
INSERT [DeliveryStatus] ([DeliveryStatusID], [StatusName], [Note]) VALUES (3, N'Đã chuyển đi', N'Gói hàng đã rời trạm, đang trên đường vận chuyển')
INSERT [DeliveryStatus] ([DeliveryStatusID], [StatusName], [Note]) VALUES (4, N'Đã đến đích', N'Gói hàng đã vận chuyển đến trạm đích')
INSERT [DeliveryStatus] ([DeliveryStatusID], [StatusName], [Note]) VALUES (5, N'Đã giao hàng', N'Gói hàng đã được giao cho người nhận')
INSERT [DeliveryStatus] ([DeliveryStatusID], [StatusName], [Note]) VALUES (6, N'Đã hủy', N'Yêu cầu đã bị hủy bởi người gửi/nhân viên')
INSERT [DeliveryStatus] ([DeliveryStatusID], [StatusName], [Note]) VALUES (7, N'Đã hết hạn', N'Yêu cầu đã hết hạn chờ xử lý')
SET IDENTITY_INSERT [DeliveryStatus] ON
SET IDENTITY_INSERT [CoachType] ON
INSERT [CoachType] ([CoachTypeID], [Seats], [Capacity]) VALUES (1, 16, NULL)
INSERT [CoachType] ([CoachTypeID], [Seats], [Capacity]) VALUES (2, 29, NULL)
INSERT [CoachType] ([CoachTypeID], [Seats], [Capacity]) VALUES (4, 45, 6)
INSERT [CoachType] ([CoachTypeID], [Seats], [Capacity]) VALUES (5, 52, NULL)
SET IDENTITY_INSERT [CoachType] ON
SET IDENTITY_INSERT [Coach] ON
INSERT [Coach] ([CoachID], [NumberPlate], [RouteID], [CoachTypeID]) VALUES (2, N'53N-9795', NULL, 4)
INSERT [Coach] ([CoachID], [NumberPlate], [RouteID], [CoachTypeID]) VALUES (3, N'53S-2951', 1, 4)
INSERT [Coach] ([CoachID], [NumberPlate], [RouteID], [CoachTypeID]) VALUES (4, N'51B-04134', 1, 4)
INSERT [Coach] ([CoachID], [NumberPlate], [RouteID], [CoachTypeID]) VALUES (5, N'51B-10558', 1, 4)
SET IDENTITY_INSERT [Coach] ON
SET IDENTITY_INSERT [Station] ON
INSERT [Station] ([StationID], [StationName], [StationLocation], [BreakTime], [StationPhone], [Description], [Coordinate], [ProvinceID]) VALUES (1, N'SG.Lê Hồng Phong', N'233 Lê Hồng Phong, F4, Q.5, TP Hồ Chí Minh', 2, N'(08) 38 309 309', N'Phòng vé Lê Hồng Phong', N'10.7584848, 106.6777557', 1)
INSERT [Station] ([StationID], [StationName], [StationLocation], [BreakTime], [StationPhone], [Description], [Coordinate], [ProvinceID]) VALUES (2, N'Tạ Uyên - VT', N'03 Tạ Uyên, Vũng Tàu', 1.5, N'0643 52 53 54', N'Phòng vé VT1', N'10.3491804, 107.0855695', 5)
INSERT [Station] ([StationID], [StationName], [StationLocation], [BreakTime], [StationPhone], [Description], [Coordinate], [ProvinceID]) VALUES (3, N'Bến xe liên tỉnh Đà Lạt', N'01 Tô Hiến Thành, F3, Đà Lạt', 2.5, N'0633 58 58 58', N'Phòng vé LĐ1', N'11.9264949, 108.4460601', 4)
INSERT [Station] ([StationID], [StationName], [StationLocation], [BreakTime], [StationPhone], [Description], [Coordinate], [ProvinceID]) VALUES (4, N'SG2.Xa Lộ Hà Nội', N'798A Xa Lộ Hà Nội, Q9, Hồ Chí Minh', 1.5, N'(08) 3897 3894', N'Phòng vé: Xa Lộ Hà Nội', N'10.8512267, 106.7767284', 1)
INSERT [Station] ([StationID], [StationName], [StationLocation], [BreakTime], [StationPhone], [Description], [Coordinate], [ProvinceID]) VALUES (6, N'Bến xe Phía Nam Nha Trang', N'Số 58, đường 23/10, Xã Vĩnh Hiệp , Nha Trang, Tỉnh Khánh Hòa ', 1.5, N'(058) 3562626', N'VP Bến xe Phía Nam Nha Trang', N'12.2527081, 109.1627343', 3)
INSERT [Station] ([StationID], [StationName], [StationLocation], [BreakTime], [StationPhone], [Description], [Coordinate], [ProvinceID]) VALUES (7, N'Bến xe Cao Lãnh', N'71 Nguyễn Văn Trỗi, Tp Cao Lãnh, Đồng Tháp', 2, N'(067)3876850', N'VP Bến xe Cao Lãnh', N'10.4560583, 105.6381782', 6)
INSERT [Station] ([StationID], [StationName], [StationLocation], [BreakTime], [StationPhone], [Description], [Coordinate], [ProvinceID]) VALUES (8, N'Suối Linh - Biên Hòa', N'D9 - Tổ 2 - KP.4 - P. Long Bình - Đồng Nai', 3, N'(0618)890639', N'Phòng vé Suối Linh - Đồng Nai', N'10.9413981, 106.9004472', 7)
INSERT [Station] ([StationID], [StationName], [StationLocation], [BreakTime], [StationPhone], [Description], [Coordinate], [ProvinceID]) VALUES (9, N'SG.Bến xe Miền Tây', N'395 Kinh Dương Vương, phường An Lạc, Quận Bình Tân, Hồ Chí Minh', 3, N'(08) 38776594', N'VP Bến xe Miền Tây', N'10.740993, 106.617980', 1)
INSERT [Station] ([StationID], [StationName], [StationLocation], [BreakTime], [StationPhone], [Description], [Coordinate], [ProvinceID]) VALUES (10, N'SG.Bến xe Miền Đông', N'367 Đinh Bộ Lĩnh, 26, Bình Thạnh, Hồ Chí Minh', 6, N'08.38 309 309 ', N'VP Bến xe Miền Đông', N'10.815213, 106.710674', 1)
INSERT [Station] ([StationID], [StationName], [StationLocation], [BreakTime], [StationPhone], [Description], [Coordinate], [ProvinceID]) VALUES (11, N'Bến xe Châu Đốc', N'89 Phan Văn Vàng, Phường Châu Phú A, Châu Đốc, Tỉnh An Giang', 6, N'( 076) 3565888 ', N'VP Bến xe Châu Đốc', N'10.7097216, 105.1184159', 8)
INSERT [Station] ([StationID], [StationName], [StationLocation], [BreakTime], [StationPhone], [Description], [Coordinate], [ProvinceID]) VALUES (12, N'Bến xe Gò Công', N'Đồng Khởi - Phường 4 - Gò Công - Tiền Giang', 8.5, N'(073)3841278', N'VP Bến xe Gò Công', N'10.360669, 106.6628406', 9)
INSERT [Station] ([StationID], [StationName], [StationLocation], [BreakTime], [StationPhone], [Description], [Coordinate], [ProvinceID]) VALUES (13, N'Rạch Giá', N'260A Nguyễn Bỉnh Khiêm, Phường Vĩnh Quang, Rạch Giá, Tỉnh Kiên Giang ', 1, N'(077) 3691691', N'VP Rạch Giá', N'10.021507, 105.0910974', 13)
INSERT [Station] ([StationID], [StationName], [StationLocation], [BreakTime], [StationPhone], [Description], [Coordinate], [ProvinceID]) VALUES (14, N'Bến Xe Vị Thanh', N'Trần Hưng Đạo, Phường 5, Vị Thanh, Tỉnh Hậu Giang', 2, N'(0711) 3583583', N'VP Bến Xe Vị Thanh', N'9.786458300000001, 105.4703164', 14)
INSERT [Station] ([StationID], [StationName], [StationLocation], [BreakTime], [StationPhone], [Description], [Coordinate], [ProvinceID]) VALUES (15, N'Bến xe Hùng Vương - Cần Thơ', N'01 Hùng Vương, Thới Bình, Ninh Kiều, TP.Cần Thơ ', 1, N'(0710)3769768', N'VP Bến xe Hùng Vương', N'10.0449994, 105.7799216', 11)
INSERT [Station] ([StationID], [StationName], [StationLocation], [BreakTime], [StationPhone], [Description], [Coordinate], [ProvinceID]) VALUES (16, N'Bến Xe Trung Tâm Đà Nẵng', N'201 Tôn Đức Thắng, Phường Hòa Minh, Liên Chiểu, TP.Đà Nẵng', NULL, N'(0511) 3786 786', N'VP Bến Xe Trung Tâm Đà Nẵng', N'16.051571, 108.214897', 12)
SET IDENTITY_INSERT [Station] ON
SET IDENTITY_INSERT [Route] ON
INSERT [Route] ([RouteID], [RouteName], [StartPoint], [EndPoint], [AvgRateLevel], [Duration], [Distance], [Container]) VALUES (1, N'Sài Gòn - Vũng Tàu', 1, 2, NULL, 1.3, 130, NULL)
INSERT [Route] ([RouteID], [RouteName], [StartPoint], [EndPoint], [AvgRateLevel], [Duration], [Distance], [Container]) VALUES (2, N'Sài Gòn - Đà Lạt', 1, 3, NULL, 8.1, 300, NULL)
INSERT [Route] ([RouteID], [RouteName], [StartPoint], [EndPoint], [AvgRateLevel], [Duration], [Distance], [Container]) VALUES (3, N'Sài Gòn - Cao Lãnh', 9, 7, NULL, 3.5, 163, NULL)
INSERT [Route] ([RouteID], [RouteName], [StartPoint], [EndPoint], [AvgRateLevel], [Duration], [Distance], [Container]) VALUES (4, N'Sài Gòn - Sa Đéc', 9, 12, NULL, 1.5, 71, NULL)
INSERT [Route] ([RouteID], [RouteName], [StartPoint], [EndPoint], [AvgRateLevel], [Duration], [Distance], [Container]) VALUES (5, N'Sài Gòn - Rạch Giá', 9, 13, NULL, 5.5, 248, NULL)
INSERT [Route] ([RouteID], [RouteName], [StartPoint], [EndPoint], [AvgRateLevel], [Duration], [Distance], [Container]) VALUES (6, N'Sài Gòn - Vị Thanh', 9, 14, NULL, 5, 240, NULL)
INSERT [Route] ([RouteID], [RouteName], [StartPoint], [EndPoint], [AvgRateLevel], [Duration], [Distance], [Container]) VALUES (7, N'Nha Trang - Sài Gòn', 6, 10, NULL, 11, 439, NULL)
INSERT [Route] ([RouteID], [RouteName], [StartPoint], [EndPoint], [AvgRateLevel], [Duration], [Distance], [Container]) VALUES (8, N'Cần Thơ - Sài Gòn', 15, 9, NULL, 3, 167, NULL)
INSERT [Route] ([RouteID], [RouteName], [StartPoint], [EndPoint], [AvgRateLevel], [Duration], [Distance], [Container]) VALUES (9, N'Đà Nẵng - Sài Gòn', 16, 10, NULL, 23, 960, NULL)
SET IDENTITY_INSERT [Route] ON
SET IDENTITY_INSERT [Trip] ON
INSERT [Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID]) VALUES (1, CAST(0x070040230E430000 AS Time), CAST(0x0700DCC9A04F0000 AS Time), NULL, NULL, NULL, NULL, NULL, CAST(0x01380B00 AS Date), 1, 2)
INSERT [Trip] ([TripID], [EstimateDepartureTime], [EstimateArrivalTime], [RealDepartureTime], [RealArrivalTime], [EstimateVolume], [AvailableVolume], [RealVolume], [Date], [RouteID], [CoachID]) VALUES (2, CAST(0x070074053F470000 AS Time), CAST(0x07003CE781870000 AS Time), NULL, NULL, NULL, NULL, NULL, CAST(0x5E380B00 AS Date), 2, 4)
SET IDENTITY_INSERT [Trip] ON
