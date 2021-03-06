USE [EmployeesProject]
GO
/****** Object:  Table [dbo].[Companys]    Script Date: 21.10.2017 0:54:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Companys](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NULL,
	[OrganizationForm] [varchar](50) NULL,
 CONSTRAINT [PK_Companys_ID] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 21.10.2017 0:54:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](255) NULL,
	[SecondName] [varchar](255) NULL,
	[MidleName] [varchar](255) NULL,
	[Date] [date] NULL,
	[Position] [tinyint] NULL,
	[CompanyId] [int] NULL,
 CONSTRAINT [PK_Employees_ID] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_CompanysId_Employees] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Companys] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_CompanysId_Employees]
GO
/****** Object:  StoredProcedure [dbo].[DeleteCompany]    Script Date: 21.10.2017 0:54:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteCompany]
(
	@Id int
)
AS
BEGIN
	DELETE FROM [Companys] WHERE [Id] = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteEmployee]    Script Date: 21.10.2017 0:54:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteEmployee]
(
	@Id int
)
AS
BEGIN
	DELETE FROM [Employees] WHERE [Id] = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[LoadCompany]    Script Date: 21.10.2017 0:54:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[LoadCompany] 
	@Id int
AS
BEGIN
	SELECT * FROM [Companys] WHERE Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[LoadCompanys]    Script Date: 21.10.2017 0:54:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[LoadCompanys]

AS
BEGIN
	SELECT 
	[Id]
	,[Name]
	,(SELECT COUNT([Employees].[Id]) FROM [Employees] where [Employees].[CompanyId] = [Companys].[Id]) as SizeCompany
	,[OrganizationForm]
	FROM [Companys]
END
	
GO
/****** Object:  StoredProcedure [dbo].[LoadEmployee]    Script Date: 21.10.2017 0:54:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[LoadEmployee]
(
	@Id int
)
AS
BEGIN
	SELECT * FROM [Employees] WHERE [Id] = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[LoadEmployees]    Script Date: 21.10.2017 0:54:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[LoadEmployees]
(
	@IdCompany int = null
)
AS
BEGIN

	SELECT * 
	FROM [Employees] 
	where 
		@IdCompany is null or [CompanyId] = @IdCompany
END
GO
/****** Object:  StoredProcedure [dbo].[SaveCompany]    Script Date: 21.10.2017 0:54:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SaveCompany] 
	@Id int = null OUTPUT,
	@Name varchar(255) = null,
	@OrganizationForm varchar(50) = null
AS
BEGIN
	if(@Id is null)
		Begin
			Insert into [Companys]
			(
			[Name],
			[OrganizationForm]
			)
			values
			(
			@Name,
			@OrganizationForm
			)
			set @Id =  SCOPE_IDENTITY() 
		End
	else
		begin
			Update [Companys]
			SET
			[Name]=@Name,
			OrganizationForm = @OrganizationForm
			where id = @Id
		end
END
GO
/****** Object:  StoredProcedure [dbo].[SaveEmployee]    Script Date: 21.10.2017 0:54:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SaveEmployee]
	(
	@Id int = null output,
	@FirstName varchar(255) = null,
	@SecondName varchar(255) = null,
	@MidleName varchar(255) = null,
	@Date Date = null,
	@Position tinyint = null,
	@CompanyId int = null
	)
AS
BEGIN
	if(@Id is null)
		Begin
			Insert into [Employees]
			(
			[FirstName],
			[SecondName],
			[MidleName],
			[Date],
			[Position],
			[CompanyId]
			)
			values
			(
			@FirstName,
			@SecondName,
			@MidleName,
			GETDATE(),
			@Position,
			@CompanyId
			)
			set @Id =  SCOPE_IDENTITY() 
		End
	else
		begin
			Update [Employees]
			SET
			[FirstName] = @FirstName,
			[SecondName] = @SecondName,
			[MidleName] = @MidleName,
			[Position] = @Position,
			[CompanyId] = @CompanyId
			where id = @Id
		end
END
GO
