USE [master]
GO
/****** Object:  Database [MDH2]    Script Date: 09/23/2020 13:12:29 ******/
CREATE DATABASE [MDH2] ON  PRIMARY 
( NAME = N'MDH2', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\MDH2.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'MDH2_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\MDH2_1.ldf' , SIZE = 1280KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [MDH2] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MDH2].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MDH2] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [MDH2] SET ANSI_NULLS OFF
GO
ALTER DATABASE [MDH2] SET ANSI_PADDING OFF
GO
ALTER DATABASE [MDH2] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [MDH2] SET ARITHABORT OFF
GO
ALTER DATABASE [MDH2] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [MDH2] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [MDH2] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [MDH2] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [MDH2] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [MDH2] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [MDH2] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [MDH2] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [MDH2] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [MDH2] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [MDH2] SET  DISABLE_BROKER
GO
ALTER DATABASE [MDH2] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [MDH2] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [MDH2] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [MDH2] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [MDH2] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [MDH2] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [MDH2] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [MDH2] SET  READ_WRITE
GO
ALTER DATABASE [MDH2] SET RECOVERY FULL
GO
ALTER DATABASE [MDH2] SET  MULTI_USER
GO
ALTER DATABASE [MDH2] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [MDH2] SET DB_CHAINING OFF
GO
USE [MDH2]
GO
/****** Object:  User [chunbo1]    Script Date: 09/23/2020 13:12:29 ******/
CREATE USER [chunbo1] FOR LOGIN [chunbo1] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [chunbo]    Script Date: 09/23/2020 13:12:29 ******/
CREATE USER [chunbo] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[Teachers]    Script Date: 09/23/2020 13:12:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Teachers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[Address] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[HomePhone] [varchar](50) NULL,
	[CellPhone] [varchar](50) NULL,
	[UpdateDateTime] [datetime] NOT NULL,
	[Enabled] [bit] NULL,
	[Salary] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Students]    Script Date: 09/23/2020 13:12:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Students](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[Gender] [varchar](2) NULL,
	[BirthDate] [varchar](50) NULL,
	[ContactName] [varchar](50) NULL,
	[Address] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[CellPhone] [varchar](50) NULL,
	[HomePhone] [varchar](50) NULL,
	[StudentPhone] [varchar](50) NULL,
	[Comment] [varchar](250) NULL,
	[UpdateDateTime] [datetime] NULL,
	[Enabled] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Membership]    Script Date: 09/23/2020 13:12:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Membership](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MemberName] [varchar](50) NULL,
	[TotalFee] [float] NULL,
	[PaidFee] [float] NULL,
	[StartDate] [date] NULL,
	[EndDate] [date] NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[Comment] [varchar](100) NULL,
	[StudentId] [int] NULL,
	[UpdateDateTime] [datetime] NULL,
	[Enabled] [bit] NULL,
 CONSTRAINT [PK_Membership] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ClassStudents]    Script Date: 09/23/2020 13:12:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ClassStudents](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [int] NULL,
	[ClassId] [int] NULL,
	[TuitionPaid] [int] NULL,
	[InvoiceNumber] [varchar](50) NULL,
	[CasherName] [varchar](50) NULL,
	[CashReceived] [float] NULL,
	[CheckReceived] [float] NULL,
	[CheckNumber] [varchar](50) NULL,
	[CreditCardReceived] [float] NULL,
	[OtherReceived] [float] NULL,
	[OtherSource] [varchar](50) NULL,
	[RegistrationDate] [datetime] NULL,
	[Discount] [float] NULL,
	[Comment] [varchar](250) NULL,
	[UpdateDateTime] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE NONCLUSTERED INDEX [ind_StudentClass] ON [dbo].[ClassStudents] 
(
	[StudentId] ASC,
	[ClassId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Classes]    Script Date: 09/23/2020 13:12:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Classes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Division] [varchar](50) NOT NULL,
	[ClassName] [varchar](50) NOT NULL,
	[Location] [varchar](50) NULL,
	[Semester] [varchar](50) NULL,
	[Dayofweek] [varchar](50) NULL,
	[Timeofweek] [varchar](50) NULL,
	[Tuition] [float] NULL,
	[TeacherId] [int] NULL,
	[Enabled] [bit] NULL,
	[TeacherCost] [float] NULL,
	[OfficeRentalCost] [float] NULL,
	[MiscCost] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SchedulesHeader]    Script Date: 09/23/2020 13:12:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SchedulesHeader](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ClassId] [int] NULL,
	[TeacherId] [int] NULL,
	[ClassDate] [date] NULL,
	[StartTime] [varchar](10) NULL,
	[EndTime] [varchar](10) NULL,
	[Status] [varchar](50) NULL,
	[Comment] [varchar](250) NULL,
	[UpdateDateTime] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE NONCLUSTERED INDEX [ind_scheduleHerder] ON [dbo].[SchedulesHeader] 
(
	[ClassId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SchedulesDetail]    Script Date: 09/23/2020 13:12:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SchedulesDetail](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[HeaderId] [int] NULL,
	[StudentId] [int] NULL,
	[Status] [bit] NULL,
	[Comment] [varchar](250) NULL,
	[UpdateDateTime] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE NONCLUSTERED INDEX [ind_scheduleDetail] ON [dbo].[SchedulesDetail] 
(
	[HeaderId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[AddSchedulesHeader]    Script Date: 09/23/2020 13:12:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddSchedulesHeader]

(

  @ClassId int,

  @TeacherId int,

  @ClassDate Date,

  @StartTime varchar(10),

  @EndTime varchar(10),

  @Status varchar(50),

  @Comment   varchar(250),

  @Id int OUTPUT

)

AS

BEGIN TRY

        BEGIN TRANSACTION

    INSERT dbo.SchedulesHeader(ClassId, TeacherId, ClassDate, StartTime, EndTime, Status, Comment, UpdateDateTime)

    VALUES (@ClassId, @TeacherId, @ClassDate, @StartTime, @EndTime, @Status, @Comment, getdate());

    COMMIT TRANSACTION

    SET @Id = SCOPE_IDENTITY();

END TRY

BEGIN CATCH

    IF @@TRANCOUNT > 0

    ROLLBACK

    -- Raise an error with the details of the exception

        DECLARE @ErrMsg nvarchar(4000), @ErrSeverity int

        SELECT @ErrMsg = ERROR_MESSAGE(),

           @ErrSeverity = ERROR_SEVERITY()

        RAISERROR(@ErrMsg, @ErrSeverity, 1)

END CATCH
GO
/****** Object:  StoredProcedure [dbo].[AddCS]    Script Date: 09/23/2020 13:12:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddCS]

(

  @StudentId int,

  @ClassId   int,

  @TuitionPaid int,

  @Comment   varchar(50),

@InvoiceNumber varchar(50),

@CasherName varchar(50),

@CashReceived float,

@CheckReceived float,

@CheckNumber varchar(50),

@CreditCardReceived float,

@OtherReceived float,

@OtherSource varchar(50),

@RegistrationDate DATETIME,

  @Id int OUTPUT

)

AS

BEGIN TRY

        BEGIN TRANSACTION

    INSERT dbo.ClassStudents (StudentId, ClassId, TuitionPaid, Comment, InvoiceNumber, CasherName, CashReceived, CheckReceived, CheckNumber, CreditCardReceived, OtherReceived ,OtherSource,RegistrationDate, UpdateDateTime)

              VALUES (@StudentId, @ClassId, @TuitionPaid, @Comment, @InvoiceNumber, @CasherName, @CashReceived, @CheckReceived, @CheckNumber, @CreditCardReceived, @OtherReceived, @OtherSource, @RegistrationDate, getdate());

    COMMIT TRANSACTION

    SET @Id = SCOPE_IDENTITY();

END TRY

BEGIN CATCH

    IF @@TRANCOUNT > 0

    ROLLBACK

    -- Raise an error with the details of the exception

        DECLARE @ErrMsg nvarchar(4000), @ErrSeverity int

        SELECT @ErrMsg = ERROR_MESSAGE(),

           @ErrSeverity = ERROR_SEVERITY()

        RAISERROR(@ErrMsg, @ErrSeverity, 1)

END CATCH
GO
/****** Object:  StoredProcedure [dbo].[DeleteCS]    Script Date: 09/23/2020 13:12:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteCS]

(

  @Id int

)

AS

BEGIN TRY

    BEGIN TRANSACTION

    BEGIN

        DELETE FROM dbo.ClassStudents WHERE ID = @Id;

    END

        COMMIT     

END TRY

BEGIN CATCH

    IF @@TRANCOUNT > 0

    ROLLBACK

        DECLARE @ErrMsg nvarchar(4000), @ErrSeverity int

        SELECT @ErrMsg = ERROR_MESSAGE(),

           @ErrSeverity = ERROR_SEVERITY()

        RAISERROR(@ErrMsg, @ErrSeverity, 1)

END CATCH
GO
/****** Object:  View [dbo].[vwStudentByClass]    Script Date: 09/23/2020 13:12:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[vwStudentByClass] AS

select S.ID, S.FirstName , S.LastName , S.Gender, S.BirthDate, ClassId

from ClassStudents C

join Students S ON C.StudentId = S.ID

--where ClassId = 4
GO
/****** Object:  View [dbo].[vwSemester]    Script Date: 09/23/2020 13:12:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vwSemester] AS

select distinct top 20 Semester from Classes

order by Semester desc
GO
/****** Object:  View [dbo].[vwCST]    Script Date: 09/23/2020 13:12:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[vwCST] AS

SELECT top 500 C.Division, C.ClassName, C.Location, C.Semester, C.Dayofweek, C.Timeofweek, C.Tuition,

T.Firstname + ' ' + T.Lastname as Teacher,

S.ID StudentId, S.FirstName + ' ' + S.Lastname as Student

from ClassStudents CS

LEFT JOIN Classes C ON CS.ClassId = C.ID

LEFT JOIN Students S  ON CS.StudentId = S.ID

LEFT JOIN Teachers T  ON C.TeacherId = T.ID

order by Semester desc
GO
/****** Object:  StoredProcedure [dbo].[UpdateCS]    Script Date: 09/23/2020 13:12:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateCS]

(

  @Id int,

  @StudentId int,

  @ClassId   int,

  @TuitionPaid int,

  @Comment   varchar(50),

@InvoiceNumber varchar(50),

@CasherName varchar(50),

@CashReceived float,

@CheckReceived float,

@CheckNumber varchar(50),

@CreditCardReceived float,

@OtherReceived float,

@OtherSource varchar(50),

@RegistrationDate DATETIME

)

AS

BEGIN TRY

        BEGIN TRANSACTION

    UPDATE dbo.ClassStudents

    SET StudentId=@StudentId,

                ClassId=@ClassId,

                TuitionPaid=@TuitionPaid,

                Comment=@Comment,

        InvoiceNumber = @InvoiceNumber,

        CasherName = @CasherName,

        CashReceived = @CashReceived,

        CheckReceived = @CheckReceived,

        CheckNumber = @CheckNumber,

        CreditCardReceived = @CreditCardReceived,

        OtherReceived = @OtherReceived,

        OtherSource = @OtherSource,

        RegistrationDate = @RegistrationDate,

                UpdateDateTime=getDate()

        WHERE ID=@Id

        COMMIT TRANSACTION    

END TRY

BEGIN CATCH

    IF @@TRANCOUNT > 0

                ROLLBACK

END CATCH
GO
/****** Object:  StoredProcedure [dbo].[spGetStudentDetail]    Script Date: 09/23/2020 13:12:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[spGetStudentDetail]

@Id int

AS

BEGIN

SET NOCOUNT ON

SELECT Distinct *

FROM Students

WHERE ID = @Id

END
GO
/****** Object:  StoredProcedure [dbo].[spGetClassTuition]    Script Date: 09/23/2020 13:12:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[spGetClassTuition]

@ClassId int

AS

BEGIN

SET NOCOUNT ON

SELECT Tuition

FROM Classes

WHERE ID = @ClassId

END
GO
/****** Object:  StoredProcedure [dbo].[spGetClassAttendanceDetail]    Script Date: 09/23/2020 13:12:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[spGetClassAttendanceDetail]

@HeaderId int

AS

BEGIN

SET NOCOUNT ON

SELECT S.ID, S.FirstName+ ' ' + S.LastName Student, D.Status, D.Comment

FROM SchedulesDetail  D

JOIN SchedulesHeader H ON H.ID = D.HeaderId

JOIN Students S ON S.ID = D.StudentId

JOIN Classes C ON C.ID = H.ClassId

WHERE D.HeaderId= @HeaderId

END
GO
/****** Object:  StoredProcedure [dbo].[spDeleteSchedulesHeader]    Script Date: 09/23/2020 13:12:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spDeleteSchedulesHeader]

@Id int

AS

BEGIN TRY

        BEGIN TRANSACTION    

    DELETE FROM SchedulesDetail WHERE HeaderId = @Id

    DELETE FROM SchedulesHeader WHERE ID = @Id

    COMMIT TRANSACTION

END TRY

BEGIN CATCH

    IF @@TRANCOUNT > 0

    ROLLBACK

    -- Raise an error with the details of the exception

        DECLARE @ErrMsg nvarchar(4000), @ErrSeverity int

        SELECT @ErrMsg = ERROR_MESSAGE(),

           @ErrSeverity = ERROR_SEVERITY()

        RAISERROR(@ErrMsg, @ErrSeverity, 1)

END CATCH
GO
/****** Object:  StoredProcedure [dbo].[spClassAttendanceHeader]    Script Date: 09/23/2020 13:12:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[spClassAttendanceHeader]

@ClassName varchar(50)

AS

BEGIN

SET NOCOUNT ON

SELECT Distinct top 500 H.ID AttendanceHeader, H.ClassDate, H.Comment, H.UpdateDateTime, C.ClassName, C.Semester, H.StartTime, H.EndTime

FROM SchedulesHeader H

JOIN SchedulesDetail D ON H.ID = D.HeaderId

JOIN Students S ON S.ID = D.StudentId

JOIN Classes C ON C.ID = H.ClassId

WHERE rtrim(C.ClassName)+ ' ' + rtrim(C.Semester) + ' ' + rtrim(C.Dayofweek) + ' ' + rtrim(C.Timeofweek) = @ClassName

ORDER BY H.ID DESC

END
GO
/****** Object:  StoredProcedure [dbo].[AddSchedulesDetail]    Script Date: 09/23/2020 13:12:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddSchedulesDetail]

(

  @HeaderId int,

  @StudentId int,

  @Status bit,

  @Comment   varchar(250),

  @Id int OUTPUT

)

AS

BEGIN TRY

        BEGIN TRANSACTION

    INSERT dbo.SchedulesDetail(HeaderId, StudentId, Status, Comment, UpdateDateTime)

    VALUES (@HeaderId, @StudentId, @Status, @Comment, getdate());

    COMMIT TRANSACTION

    SET @Id = SCOPE_IDENTITY();

END TRY

BEGIN CATCH

    IF @@TRANCOUNT > 0

    ROLLBACK

    -- Raise an error with the details of the exception

        DECLARE @ErrMsg nvarchar(4000), @ErrSeverity int

        SELECT @ErrMsg = ERROR_MESSAGE(),

           @ErrSeverity = ERROR_SEVERITY()

        RAISERROR(@ErrMsg, @ErrSeverity, 1)

END CATCH
GO
/****** Object:  Default [DF__Teachers__Enable__014935CB]    Script Date: 09/23/2020 13:12:31 ******/
ALTER TABLE [dbo].[Teachers] ADD  DEFAULT ((1)) FOR [Enabled]
GO
/****** Object:  Default [DF__Students__Enable__2A4B4B5E]    Script Date: 09/23/2020 13:12:31 ******/
ALTER TABLE [dbo].[Students] ADD  DEFAULT ((1)) FOR [Enabled]
GO
/****** Object:  Default [DF__Classes__Enabled__09DE7BCC]    Script Date: 09/23/2020 13:12:31 ******/
ALTER TABLE [dbo].[Classes] ADD  DEFAULT ((1)) FOR [Enabled]
GO
/****** Object:  ForeignKey [FK__Schedules__Heade__22AA2996]    Script Date: 09/23/2020 13:12:31 ******/
ALTER TABLE [dbo].[SchedulesDetail]  WITH CHECK ADD FOREIGN KEY([HeaderId])
REFERENCES [dbo].[SchedulesHeader] ([ID])
GO
