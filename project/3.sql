USE [EmployeesProject]
GO
SET IDENTITY_INSERT [dbo].[Companys] ON 
GO
INSERT [dbo].[Companys] ([Id], [Name], [OrganizationForm]) VALUES (1, N'Гум', N'ОДО')
GO
INSERT [dbo].[Companys] ([Id], [Name], [OrganizationForm]) VALUES (2, N'дом2 быта', N'ООО')
GO
INSERT [dbo].[Companys] ([Id], [Name], [OrganizationForm]) VALUES (4, N'Новая сущность', N'ОДО')
GO
INSERT [dbo].[Companys] ([Id], [Name], [OrganizationForm]) VALUES (5, N'Еще одна для теста', N'ооо')
GO
SET IDENTITY_INSERT [dbo].[Companys] OFF
GO
SET IDENTITY_INSERT [dbo].[Employees] ON 
GO
INSERT [dbo].[Employees] ([Id], [FirstName], [SecondName], [MidleName], [Date], [Position], [CompanyId]) VALUES (2, N'Евген', N'Фамилия ', N'Отчество', CAST(N'2016-01-01' AS Date), 3, 1)
GO
INSERT [dbo].[Employees] ([Id], [FirstName], [SecondName], [MidleName], [Date], [Position], [CompanyId]) VALUES (4, N'Александр', N'Лукашенко', N'Григорьевич', CAST(N'2017-10-19' AS Date), 3, 1)
GO
INSERT [dbo].[Employees] ([Id], [FirstName], [SecondName], [MidleName], [Date], [Position], [CompanyId]) VALUES (5, N'Вася', N'NoName', N'Батькович', CAST(N'2017-10-20' AS Date), 2, 5)
GO
INSERT [dbo].[Employees] ([Id], [FirstName], [SecondName], [MidleName], [Date], [Position], [CompanyId]) VALUES (6, N'Женя', N'Соколюк', N'Александрович', CAST(N'2017-10-20' AS Date), 1, 4)
GO
INSERT [dbo].[Employees] ([Id], [FirstName], [SecondName], [MidleName], [Date], [Position], [CompanyId]) VALUES (7, N'Дом', N'Гора', N'Мальвина', CAST(N'2017-10-20' AS Date), 2, 2)
GO
INSERT [dbo].[Employees] ([Id], [FirstName], [SecondName], [MidleName], [Date], [Position], [CompanyId]) VALUES (8, N'Тот', N'Самый', N'Чувак', CAST(N'2017-10-20' AS Date), 0, 4)
GO
INSERT [dbo].[Employees] ([Id], [FirstName], [SecondName], [MidleName], [Date], [Position], [CompanyId]) VALUES (9, N'Бил', N'Гейтс', N'Ибрагимович', CAST(N'2017-10-20' AS Date), 1, 4)
GO
SET IDENTITY_INSERT [dbo].[Employees] OFF
GO
