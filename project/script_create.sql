
GO
/****** Object:  Database [EmployeesProject]    Script Date: 07.12.2017 17:36:48 ******/

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [EmployeesProject].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [EmployeesProject] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [EmployeesProject] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [EmployeesProject] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [EmployeesProject] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [EmployeesProject] SET ARITHABORT OFF 
GO
ALTER DATABASE [EmployeesProject] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [EmployeesProject] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [EmployeesProject] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [EmployeesProject] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [EmployeesProject] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [EmployeesProject] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [EmployeesProject] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [EmployeesProject] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [EmployeesProject] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [EmployeesProject] SET  DISABLE_BROKER 
GO
ALTER DATABASE [EmployeesProject] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [EmployeesProject] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [EmployeesProject] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [EmployeesProject] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [EmployeesProject] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [EmployeesProject] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [EmployeesProject] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [EmployeesProject] SET RECOVERY FULL 
GO
ALTER DATABASE [EmployeesProject] SET  MULTI_USER 
GO
ALTER DATABASE [EmployeesProject] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [EmployeesProject] SET DB_CHAINING OFF 
GO
EXEC sys.sp_db_vardecimal_storage_format N'EmployeesProject', N'ON'
GO
USE [EmployeesProject]
GO
/****** Object:  Table [dbo].[Companys]    Script Date: 07.12.2017 17:36:49 ******/
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
/****** Object:  Table [dbo].[Employees]    Script Date: 07.12.2017 17:36:49 ******/
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
/****** Object:  Table [dbo].[Users]    Script Date: 07.12.2017 17:36:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Login] [varchar](50) NOT NULL,
	[Password] [varchar](100) NOT NULL,
	[Name] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_CompanysId_Employees] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Companys] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_CompanysId_Employees]
GO
/****** Object:  StoredProcedure [dbo].[DeleteCompany]    Script Date: 07.12.2017 17:36:49 ******/
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
/****** Object:  StoredProcedure [dbo].[DeleteEmployee]    Script Date: 07.12.2017 17:36:49 ******/
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
/****** Object:  StoredProcedure [dbo].[LoadCompany]    Script Date: 07.12.2017 17:36:49 ******/
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
/****** Object:  StoredProcedure [dbo].[LoadCompanys]    Script Date: 07.12.2017 17:36:49 ******/
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
/****** Object:  StoredProcedure [dbo].[LoadEmployee]    Script Date: 07.12.2017 17:36:49 ******/
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
/****** Object:  StoredProcedure [dbo].[LoadEmployees]    Script Date: 07.12.2017 17:36:49 ******/
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
/****** Object:  StoredProcedure [dbo].[SaveCompany]    Script Date: 07.12.2017 17:36:49 ******/
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
/****** Object:  StoredProcedure [dbo].[SaveEmployee]    Script Date: 07.12.2017 17:36:49 ******/
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
/****** Object:  StoredProcedure [dbo].[SaveUser]    Script Date: 07.12.2017 17:36:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[SaveUser]
(
	@Id int = null output,
	@Name varchar(max),
	@Password varchar(100),
	@Login varchar(50)
)
as
begin
if(@Id is null)
	begin
		insert into [Users]
		(
		[Name],
		[Password],
		[Login]
		)
		values
		(
		@Name,
		@Password,
		@Login
		)
	end
else
	begin
		update [Users]
		set
		[Name] = @Name,
		[Password] = @Password,
		[Login] = @Login
		where 
		[Id] = @Id
	end


end

GO
/****** Object:  StoredProcedure [dbo].[ValidateUser]    Script Date: 07.12.2017 17:36:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ValidateUser] 
	@Login varchar(50),
	@Password varchar(100)
AS
BEGIN
	SELECT * FROM [Users] WHERE upper(@Login) = upper([Login])
END
GO
USE [master]
GO
ALTER DATABASE [EmployeesProject] SET  READ_WRITE 
GO
