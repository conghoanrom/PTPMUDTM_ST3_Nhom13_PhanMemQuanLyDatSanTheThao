USE [DBSPORTSFIELDBOOKING]
GO
/****** Object:  Table [dbo].[CasualBookings]    Script Date: 11/13/2024 1:04:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CasualBookings](
	[BookingId] [varchar](11) NOT NULL,
	[BookingDate] [date] NULL,
	[StartDate] [date] NULL,
	[StartTime] [time](7) NULL,
	[Minutes] [int] NULL,
	[FieldId] [int] NULL,
	[CustomerName] [nvarchar](50) NULL,
	[Phone] [varchar](11) NULL,
	[EmployeeId] [varchar](5) NULL,
	[ServiceId] [int] NULL,
	[TotalPrice] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[BookingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DiscountServices]    Script Date: 11/13/2024 1:04:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DiscountServices](
	[ServiceId] [int] IDENTITY(1,1) NOT NULL,
	[ServiceName] [nvarchar](100) NULL,
	[StartDate] [date] NULL,
	[Days] [int] NULL,
	[Discount] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ServiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 11/13/2024 1:04:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[EmployeeId] [varchar](5) NOT NULL,
	[FullName] [nvarchar](50) NULL,
	[Gender] [nvarchar](7) NULL,
	[Birth] [date] NULL,
	[Phone] [varchar](11) NULL,
	[Username] [varchar](100) NULL,
	[Password] [varchar](100) NULL,
	[RoleId] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Fields]    Script Date: 11/13/2024 1:04:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Fields](
	[FieldId] [int] IDENTITY(1,1) NOT NULL,
	[FieldName] [nvarchar](50) NULL,
	[Location] [int] NULL,
	[TypeId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[FieldId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FieldTypes]    Script Date: 11/13/2024 1:04:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FieldTypes](
	[TypeId] [int] IDENTITY(1,1) NOT NULL,
	[TypeName] [nvarchar](50) NULL,
	[CasualRentalPrice] [int] NULL,
	[FixedRentalPrice] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[TypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 11/13/2024 1:04:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleId] [varchar](20) NOT NULL,
	[RoleName] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SalaryPayments]    Script Date: 11/13/2024 1:04:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalaryPayments](
	[EmployeeId] [varchar](5) NOT NULL,
	[MonthWorking] [int] NULL,
	[Status] [nvarchar](30) NULL,
	[SALARY] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TimeKeepings]    Script Date: 11/13/2024 1:04:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TimeKeepings](
	[EmployeeId] [varchar](5) NOT NULL,
	[DayWorking] [date] NULL,
	[HOURS] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[CasualBookings] ([BookingId], [BookingDate], [StartDate], [StartTime], [Minutes], [FieldId], [CustomerName], [Phone], [EmployeeId], [ServiceId], [TotalPrice]) VALUES (N'CSBK1AQAPE', CAST(N'2024-11-13' AS Date), CAST(N'2024-12-05' AS Date), CAST(N'15:00:00' AS Time), 60, 22, N'Lê Hoan', N'0369656502', N'NV001', 3, 42500)
INSERT [dbo].[CasualBookings] ([BookingId], [BookingDate], [StartDate], [StartTime], [Minutes], [FieldId], [CustomerName], [Phone], [EmployeeId], [ServiceId], [TotalPrice]) VALUES (N'CSBK1BXUN9', CAST(N'2024-11-13' AS Date), CAST(N'2024-12-09' AS Date), CAST(N'15:00:00' AS Time), 60, 22, N'Hữu Toàn', N'0369656502', N'NV001', 3, 42500)
INSERT [dbo].[CasualBookings] ([BookingId], [BookingDate], [StartDate], [StartTime], [Minutes], [FieldId], [CustomerName], [Phone], [EmployeeId], [ServiceId], [TotalPrice]) VALUES (N'CSBK1YAOD0', CAST(N'2024-11-13' AS Date), CAST(N'2024-11-15' AS Date), CAST(N'15:00:00' AS Time), 120, 1, N'Lê Hoan', N'0369656502', N'NV001', 3, 170000)
INSERT [dbo].[CasualBookings] ([BookingId], [BookingDate], [StartDate], [StartTime], [Minutes], [FieldId], [CustomerName], [Phone], [EmployeeId], [ServiceId], [TotalPrice]) VALUES (N'CSBK66E86K', CAST(N'2024-11-13' AS Date), CAST(N'2024-11-14' AS Date), CAST(N'15:00:00' AS Time), 60, 22, N'Lê Hoan', N'0369656502', N'NV001', 3, 42500)
INSERT [dbo].[CasualBookings] ([BookingId], [BookingDate], [StartDate], [StartTime], [Minutes], [FieldId], [CustomerName], [Phone], [EmployeeId], [ServiceId], [TotalPrice]) VALUES (N'CSBK8BDLN5', CAST(N'2024-11-13' AS Date), CAST(N'2024-11-18' AS Date), CAST(N'15:00:00' AS Time), 60, 22, N'Hữu Toàn', N'0369656502', N'NV001', 3, 42500)
INSERT [dbo].[CasualBookings] ([BookingId], [BookingDate], [StartDate], [StartTime], [Minutes], [FieldId], [CustomerName], [Phone], [EmployeeId], [ServiceId], [TotalPrice]) VALUES (N'CSBKBEP8D2', CAST(N'2024-11-13' AS Date), CAST(N'2024-11-28' AS Date), CAST(N'15:00:00' AS Time), 60, 22, N'Lê Hoan', N'0369656502', N'NV001', 3, 42500)
INSERT [dbo].[CasualBookings] ([BookingId], [BookingDate], [StartDate], [StartTime], [Minutes], [FieldId], [CustomerName], [Phone], [EmployeeId], [ServiceId], [TotalPrice]) VALUES (N'CSBKBUHLU6', CAST(N'2024-11-12' AS Date), CAST(N'2024-11-14' AS Date), NULL, 60, 6, N'Lê Nguyễn Công Hoan', N'0369656502', N'NV001', 4, 60000)
INSERT [dbo].[CasualBookings] ([BookingId], [BookingDate], [StartDate], [StartTime], [Minutes], [FieldId], [CustomerName], [Phone], [EmployeeId], [ServiceId], [TotalPrice]) VALUES (N'CSBKDX60YG', CAST(N'2024-11-12' AS Date), CAST(N'2024-11-12' AS Date), CAST(N'23:50:43.3504389' AS Time), 60, 6, N'Lê Nguyễn Công Hoan', N'0369656502', N'NV001', 4, 60000)
INSERT [dbo].[CasualBookings] ([BookingId], [BookingDate], [StartDate], [StartTime], [Minutes], [FieldId], [CustomerName], [Phone], [EmployeeId], [ServiceId], [TotalPrice]) VALUES (N'CSBKGHP0E6', CAST(N'2024-11-12' AS Date), CAST(N'2024-11-13' AS Date), CAST(N'22:50:00' AS Time), 60, 6, N'Công Hoan', N'0369656502', N'NV001', 4, 60000)
INSERT [dbo].[CasualBookings] ([BookingId], [BookingDate], [StartDate], [StartTime], [Minutes], [FieldId], [CustomerName], [Phone], [EmployeeId], [ServiceId], [TotalPrice]) VALUES (N'CSBKJTK878', CAST(N'2024-11-13' AS Date), CAST(N'2024-11-13' AS Date), CAST(N'10:50:00' AS Time), 60, 6, N'Lê Hoan', N'0366656502', N'NV001', 4, 60000)
INSERT [dbo].[CasualBookings] ([BookingId], [BookingDate], [StartDate], [StartTime], [Minutes], [FieldId], [CustomerName], [Phone], [EmployeeId], [ServiceId], [TotalPrice]) VALUES (N'CSBKK7ID7W', CAST(N'2024-11-13' AS Date), CAST(N'2024-11-25' AS Date), CAST(N'15:00:00' AS Time), 60, 22, N'Hữu Toàn', N'0369656502', N'NV001', 3, 42500)
INSERT [dbo].[CasualBookings] ([BookingId], [BookingDate], [StartDate], [StartTime], [Minutes], [FieldId], [CustomerName], [Phone], [EmployeeId], [ServiceId], [TotalPrice]) VALUES (N'CSBKLIQWT8', CAST(N'2024-11-13' AS Date), CAST(N'2024-12-12' AS Date), CAST(N'15:00:00' AS Time), 60, 22, N'Lê Hoan', N'0369656502', N'NV001', 3, 42500)
INSERT [dbo].[CasualBookings] ([BookingId], [BookingDate], [StartDate], [StartTime], [Minutes], [FieldId], [CustomerName], [Phone], [EmployeeId], [ServiceId], [TotalPrice]) VALUES (N'CSBKNEXYBQ', CAST(N'2024-11-13' AS Date), CAST(N'2024-12-02' AS Date), CAST(N'15:00:00' AS Time), 60, 22, N'Hữu Toàn', N'0369656502', N'NV001', 3, 42500)
INSERT [dbo].[CasualBookings] ([BookingId], [BookingDate], [StartDate], [StartTime], [Minutes], [FieldId], [CustomerName], [Phone], [EmployeeId], [ServiceId], [TotalPrice]) VALUES (N'CSBKNUNCJD', CAST(N'2024-11-13' AS Date), CAST(N'2024-11-13' AS Date), CAST(N'15:00:00' AS Time), 120, 6, N'Văn Chiến', N'0369656502', N'NV001', 3, 102000)
INSERT [dbo].[CasualBookings] ([BookingId], [BookingDate], [StartDate], [StartTime], [Minutes], [FieldId], [CustomerName], [Phone], [EmployeeId], [ServiceId], [TotalPrice]) VALUES (N'CSBKRCYJ75', CAST(N'2024-11-13' AS Date), CAST(N'2024-11-13' AS Date), CAST(N'21:40:00' AS Time), 60, 6, N'Lê Hoan', N'0366656502', N'NV001', 4, 60000)
INSERT [dbo].[CasualBookings] ([BookingId], [BookingDate], [StartDate], [StartTime], [Minutes], [FieldId], [CustomerName], [Phone], [EmployeeId], [ServiceId], [TotalPrice]) VALUES (N'CSBKUDVNLG', CAST(N'2024-11-13' AS Date), CAST(N'2024-11-21' AS Date), CAST(N'15:00:00' AS Time), 60, 22, N'Lê Hoan', N'0369656502', N'NV001', 3, 42500)
GO
SET IDENTITY_INSERT [dbo].[DiscountServices] ON 

INSERT [dbo].[DiscountServices] ([ServiceId], [ServiceName], [StartDate], [Days], [Discount]) VALUES (1, N'Giảm giá dịp lễ quốc khánh 2/9', CAST(N'2024-08-31' AS Date), 7, 50)
INSERT [dbo].[DiscountServices] ([ServiceId], [ServiceName], [StartDate], [Days], [Discount]) VALUES (2, N'Giảm giá dịp tết', CAST(N'2025-01-29' AS Date), 9, 20)
INSERT [dbo].[DiscountServices] ([ServiceId], [ServiceName], [StartDate], [Days], [Discount]) VALUES (3, N'Giảm giá sinh viên', CAST(N'2024-11-01' AS Date), 9999, 15)
INSERT [dbo].[DiscountServices] ([ServiceId], [ServiceName], [StartDate], [Days], [Discount]) VALUES (4, N'Không áp dụng dịch vụ', CAST(N'2024-11-01' AS Date), 9999, 0)
SET IDENTITY_INSERT [dbo].[DiscountServices] OFF
GO
INSERT [dbo].[Employees] ([EmployeeId], [FullName], [Gender], [Birth], [Phone], [Username], [Password], [RoleId]) VALUES (N'NV001', N'Lê Nguyễn Công Hoan', N'Nam', CAST(N'2003-08-21' AS Date), N'0369656502', N'admin', N'IY9WkvLT3LvvxvpgoJblIw==', N'MANAGER')
INSERT [dbo].[Employees] ([EmployeeId], [FullName], [Gender], [Birth], [Phone], [Username], [Password], [RoleId]) VALUES (N'NV002', N'Hoàng Văn Chiến', N'Nam', CAST(N'2003-11-15' AS Date), N'0123456789', N'vanchien123', N'IY9WkvLT3LvvxvpgoJblIw==', N'EMPLOYEE')
GO
SET IDENTITY_INSERT [dbo].[Fields] ON 

INSERT [dbo].[Fields] ([FieldId], [FieldName], [Location], [TypeId]) VALUES (1, N'Sân 5 người 1', 1, 1)
INSERT [dbo].[Fields] ([FieldId], [FieldName], [Location], [TypeId]) VALUES (2, N'Sân 5 người 2', 2, 1)
INSERT [dbo].[Fields] ([FieldId], [FieldName], [Location], [TypeId]) VALUES (3, N'Sân 5 người 3', 3, 1)
INSERT [dbo].[Fields] ([FieldId], [FieldName], [Location], [TypeId]) VALUES (4, N'Sân 5 người 4', 4, 1)
INSERT [dbo].[Fields] ([FieldId], [FieldName], [Location], [TypeId]) VALUES (5, N'Sân 7 người', 1, 2)
INSERT [dbo].[Fields] ([FieldId], [FieldName], [Location], [TypeId]) VALUES (6, N'Sân cầu lông 1', 1, 3)
INSERT [dbo].[Fields] ([FieldId], [FieldName], [Location], [TypeId]) VALUES (7, N'Sân cầu lông 2', 2, 3)
INSERT [dbo].[Fields] ([FieldId], [FieldName], [Location], [TypeId]) VALUES (8, N'Sân cầu lông 3', 3, 3)
INSERT [dbo].[Fields] ([FieldId], [FieldName], [Location], [TypeId]) VALUES (9, N'Sân cầu lông 4', 4, 3)
INSERT [dbo].[Fields] ([FieldId], [FieldName], [Location], [TypeId]) VALUES (10, N'Bàn bóng bàn 1', 1, 5)
INSERT [dbo].[Fields] ([FieldId], [FieldName], [Location], [TypeId]) VALUES (11, N'Bàn bóng bàn 2', 2, 5)
INSERT [dbo].[Fields] ([FieldId], [FieldName], [Location], [TypeId]) VALUES (12, N'Bàn bóng bàn 3', 3, 5)
INSERT [dbo].[Fields] ([FieldId], [FieldName], [Location], [TypeId]) VALUES (13, N'Bàn bóng bàn 4', 4, 5)
INSERT [dbo].[Fields] ([FieldId], [FieldName], [Location], [TypeId]) VALUES (14, N'Sân tennis 1', 1, 4)
INSERT [dbo].[Fields] ([FieldId], [FieldName], [Location], [TypeId]) VALUES (15, N'Sân tennis 2', 2, 4)
INSERT [dbo].[Fields] ([FieldId], [FieldName], [Location], [TypeId]) VALUES (16, N'Sân tennis 3', 3, 4)
INSERT [dbo].[Fields] ([FieldId], [FieldName], [Location], [TypeId]) VALUES (17, N'Sân tennis 4', 4, 4)
INSERT [dbo].[Fields] ([FieldId], [FieldName], [Location], [TypeId]) VALUES (21, N'Sân bóng đá không cố định', 0, 1)
INSERT [dbo].[Fields] ([FieldId], [FieldName], [Location], [TypeId]) VALUES (22, N'Sân cầu lông không  cố định', 0, 3)
INSERT [dbo].[Fields] ([FieldId], [FieldName], [Location], [TypeId]) VALUES (23, N'Sân tennis không cố định', 0, 4)
INSERT [dbo].[Fields] ([FieldId], [FieldName], [Location], [TypeId]) VALUES (24, N'Bàn bóng bàn không cố định', 0, 5)
SET IDENTITY_INSERT [dbo].[Fields] OFF
GO
SET IDENTITY_INSERT [dbo].[FieldTypes] ON 

INSERT [dbo].[FieldTypes] ([TypeId], [TypeName], [CasualRentalPrice], [FixedRentalPrice]) VALUES (1, N'Sân bóng 5 người', 100000, 80000)
INSERT [dbo].[FieldTypes] ([TypeId], [TypeName], [CasualRentalPrice], [FixedRentalPrice]) VALUES (2, N'Sân bóng 7 người', 300000, 240000)
INSERT [dbo].[FieldTypes] ([TypeId], [TypeName], [CasualRentalPrice], [FixedRentalPrice]) VALUES (3, N'Sân cầu lông', 60000, 50000)
INSERT [dbo].[FieldTypes] ([TypeId], [TypeName], [CasualRentalPrice], [FixedRentalPrice]) VALUES (4, N'Sân tennis', 100000, 80000)
INSERT [dbo].[FieldTypes] ([TypeId], [TypeName], [CasualRentalPrice], [FixedRentalPrice]) VALUES (5, N'Bàn bóng bàn', 40000, 30000)
SET IDENTITY_INSERT [dbo].[FieldTypes] OFF
GO
INSERT [dbo].[Roles] ([RoleId], [RoleName]) VALUES (N'EMPLOYEE', N'Nhân viên thời vụ')
INSERT [dbo].[Roles] ([RoleId], [RoleName]) VALUES (N'MANAGER', N'Quản lý')
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Discount__A42B5F99748D14CD]    Script Date: 11/13/2024 1:04:28 PM ******/
ALTER TABLE [dbo].[DiscountServices] ADD UNIQUE NONCLUSTERED 
(
	[ServiceName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Fields__A88707A62E9E9FAB]    Script Date: 11/13/2024 1:04:28 PM ******/
ALTER TABLE [dbo].[Fields] ADD UNIQUE NONCLUSTERED 
(
	[FieldName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__FieldTyp__D4E7DFA8E6551834]    Script Date: 11/13/2024 1:04:28 PM ******/
ALTER TABLE [dbo].[FieldTypes] ADD UNIQUE NONCLUSTERED 
(
	[TypeName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CasualBookings] ADD  DEFAULT (getdate()) FOR [BookingDate]
GO
ALTER TABLE [dbo].[CasualBookings]  WITH CHECK ADD FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([EmployeeId])
GO
ALTER TABLE [dbo].[CasualBookings]  WITH CHECK ADD FOREIGN KEY([FieldId])
REFERENCES [dbo].[Fields] ([FieldId])
GO
ALTER TABLE [dbo].[CasualBookings]  WITH CHECK ADD FOREIGN KEY([ServiceId])
REFERENCES [dbo].[DiscountServices] ([ServiceId])
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Roles]
GO
ALTER TABLE [dbo].[Fields]  WITH CHECK ADD FOREIGN KEY([TypeId])
REFERENCES [dbo].[FieldTypes] ([TypeId])
GO
ALTER TABLE [dbo].[SalaryPayments]  WITH CHECK ADD FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([EmployeeId])
GO
ALTER TABLE [dbo].[TimeKeepings]  WITH CHECK ADD FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([EmployeeId])
GO
ALTER TABLE [dbo].[CasualBookings]  WITH CHECK ADD CHECK  (([Minutes]>(0)))
GO
ALTER TABLE [dbo].[DiscountServices]  WITH CHECK ADD CHECK  (([Discount]>=(0) AND [Discount]<=(100)))
GO
ALTER TABLE [dbo].[DiscountServices]  WITH CHECK ADD CHECK  (([Days]>(0)))
GO
ALTER TABLE [dbo].[Fields]  WITH CHECK ADD  CONSTRAINT [CK__Fields__Location__3D5E1FD2] CHECK  (([Location]>(-1)))
GO
ALTER TABLE [dbo].[Fields] CHECK CONSTRAINT [CK__Fields__Location__3D5E1FD2]
GO
ALTER TABLE [dbo].[FieldTypes]  WITH CHECK ADD CHECK  (([CasualRentalPrice]>(0)))
GO
ALTER TABLE [dbo].[FieldTypes]  WITH CHECK ADD CHECK  (([FixedRentalPrice]>(0)))
GO
ALTER TABLE [dbo].[SalaryPayments]  WITH CHECK ADD CHECK  (([MonthWorking]>=(1) AND [MonthWorking]<=(12)))
GO
ALTER TABLE [dbo].[SalaryPayments]  WITH CHECK ADD CHECK  (([Status]=N'Ðã thanh toán' OR [Status]=N'Chua thanh toán'))
GO
