USE [master]
GO
/****** Object:  Database [ArenaGestorDB]    Script Date: 14/06/2022 20:04:51 ******/
CREATE DATABASE [ArenaGestorDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ArenaGestorDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\ArenaGestorDB.mdf' , SIZE = 4160KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ArenaGestorDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\ArenaGestorDB_log.ldf' , SIZE = 1040KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [ArenaGestorDB] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ArenaGestorDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ArenaGestorDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ArenaGestorDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ArenaGestorDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ArenaGestorDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ArenaGestorDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [ArenaGestorDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ArenaGestorDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ArenaGestorDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ArenaGestorDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ArenaGestorDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ArenaGestorDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ArenaGestorDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ArenaGestorDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ArenaGestorDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ArenaGestorDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [ArenaGestorDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ArenaGestorDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ArenaGestorDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ArenaGestorDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ArenaGestorDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ArenaGestorDB] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [ArenaGestorDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ArenaGestorDB] SET RECOVERY FULL 
GO
ALTER DATABASE [ArenaGestorDB] SET  MULTI_USER 
GO
ALTER DATABASE [ArenaGestorDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ArenaGestorDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ArenaGestorDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ArenaGestorDB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'ArenaGestorDB', N'ON'
GO
USE [ArenaGestorDB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 14/06/2022 20:04:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Artist]    Script Date: 14/06/2022 20:04:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Artist](
	[ArtistId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_Artist] PRIMARY KEY CLUSTERED 
(
	[ArtistId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ArtistBand]    Script Date: 14/06/2022 20:04:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArtistBand](
	[ArtistId] [int] NOT NULL,
	[MusicalProtagonistId] [int] NOT NULL,
	[RoleArtistId] [int] NOT NULL,
 CONSTRAINT [PK_ArtistBand] PRIMARY KEY CLUSTERED 
(
	[ArtistId] ASC,
	[MusicalProtagonistId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Band]    Script Date: 14/06/2022 20:04:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Band](
	[MusicalProtagonistId] [int] NOT NULL,
 CONSTRAINT [PK_Band] PRIMARY KEY CLUSTERED 
(
	[MusicalProtagonistId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Concert]    Script Date: 14/06/2022 20:04:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Concert](
	[ConcertId] [int] IDENTITY(1,1) NOT NULL,
	[TourName] [nvarchar](50) NOT NULL,
	[Date] [datetime2](7) NOT NULL,
	[TicketCount] [int] NOT NULL,
	[Price] [float] NOT NULL,
	[LocationId] [int] NOT NULL,
 CONSTRAINT [PK_Concert] PRIMARY KEY CLUSTERED 
(
	[ConcertId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ConcertProtagonist]    Script Date: 14/06/2022 20:04:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConcertProtagonist](
	[ConcertId] [int] NOT NULL,
	[MusicalProtagonistId] [int] NOT NULL,
 CONSTRAINT [PK_ConcertProtagonist] PRIMARY KEY CLUSTERED 
(
	[ConcertId] ASC,
	[MusicalProtagonistId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Countrys]    Script Date: 14/06/2022 20:04:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Countrys](
	[CountryId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Countrys] PRIMARY KEY CLUSTERED 
(
	[CountryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Gender]    Script Date: 14/06/2022 20:04:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gender](
	[GenderId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Gender] PRIMARY KEY CLUSTERED 
(
	[GenderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Location]    Script Date: 14/06/2022 20:04:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Location](
	[LocationId] [int] IDENTITY(1,1) NOT NULL,
	[Place] [nvarchar](50) NOT NULL,
	[CountryId] [int] NOT NULL,
	[Street] [nvarchar](500) NOT NULL,
	[Number] [int] NOT NULL,
 CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED 
(
	[LocationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MusicalProtagonist]    Script Date: 14/06/2022 20:04:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MusicalProtagonist](
	[MusicalProtagonistId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[StartDate] [datetime2](7) NOT NULL,
	[GenderId] [int] NOT NULL,
 CONSTRAINT [PK_MusicalProtagonist] PRIMARY KEY CLUSTERED 
(
	[MusicalProtagonistId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 14/06/2022 20:04:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[RoleId] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleArtist]    Script Date: 14/06/2022 20:04:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleArtist](
	[RoleArtistId] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_RoleArtist] PRIMARY KEY CLUSTERED 
(
	[RoleArtistId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleUser]    Script Date: 14/06/2022 20:04:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleUser](
	[RoleId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_RoleUser] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Session]    Script Date: 14/06/2022 20:04:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Session](
	[SessionId] [int] IDENTITY(1,1) NOT NULL,
	[Token] [nvarchar](max) NULL,
	[Created] [datetime2](7) NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_Session] PRIMARY KEY CLUSTERED 
(
	[SessionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Soloist]    Script Date: 14/06/2022 20:04:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Soloist](
	[MusicalProtagonistId] [int] NOT NULL,
	[ArtistId] [int] NOT NULL,
	[RoleArtistId] [int] NOT NULL,
 CONSTRAINT [PK_Soloist] PRIMARY KEY CLUSTERED 
(
	[MusicalProtagonistId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ticket]    Script Date: 14/06/2022 20:04:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ticket](
	[TicketId] [uniqueidentifier] NOT NULL,
	[TicketStatusId] [int] NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[ConcertId] [int] NOT NULL,
	[Amount] [int] NOT NULL,
 CONSTRAINT [PK_Ticket] PRIMARY KEY CLUSTERED 
(
	[TicketId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TicketStatus]    Script Date: 14/06/2022 20:04:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TicketStatus](
	[TicketStatusId] [int] NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_TicketStatus] PRIMARY KEY CLUSTERED 
(
	[TicketStatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 14/06/2022 20:04:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Surname] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Artist_Name]    Script Date: 14/06/2022 20:04:51 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Artist_Name] ON [dbo].[Artist]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Artist_UserId]    Script Date: 14/06/2022 20:04:51 ******/
CREATE NONCLUSTERED INDEX [IX_Artist_UserId] ON [dbo].[Artist]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ArtistBand_MusicalProtagonistId]    Script Date: 14/06/2022 20:04:51 ******/
CREATE NONCLUSTERED INDEX [IX_ArtistBand_MusicalProtagonistId] ON [dbo].[ArtistBand]
(
	[MusicalProtagonistId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ArtistBand_RoleArtistId]    Script Date: 14/06/2022 20:04:51 ******/
CREATE NONCLUSTERED INDEX [IX_ArtistBand_RoleArtistId] ON [dbo].[ArtistBand]
(
	[RoleArtistId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Concert_LocationId]    Script Date: 14/06/2022 20:04:51 ******/
CREATE NONCLUSTERED INDEX [IX_Concert_LocationId] ON [dbo].[Concert]
(
	[LocationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ConcertProtagonist_MusicalProtagonistId]    Script Date: 14/06/2022 20:04:51 ******/
CREATE NONCLUSTERED INDEX [IX_ConcertProtagonist_MusicalProtagonistId] ON [dbo].[ConcertProtagonist]
(
	[MusicalProtagonistId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Gender_Name]    Script Date: 14/06/2022 20:04:51 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Gender_Name] ON [dbo].[Gender]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Location_CountryId]    Script Date: 14/06/2022 20:04:51 ******/
CREATE NONCLUSTERED INDEX [IX_Location_CountryId] ON [dbo].[Location]
(
	[CountryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_MusicalProtagonist_GenderId]    Script Date: 14/06/2022 20:04:51 ******/
CREATE NONCLUSTERED INDEX [IX_MusicalProtagonist_GenderId] ON [dbo].[MusicalProtagonist]
(
	[GenderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_MusicalProtagonist_Name]    Script Date: 14/06/2022 20:04:51 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_MusicalProtagonist_Name] ON [dbo].[MusicalProtagonist]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Role_Name]    Script Date: 14/06/2022 20:04:51 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Role_Name] ON [dbo].[Role]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_RoleUser_UserId]    Script Date: 14/06/2022 20:04:51 ******/
CREATE NONCLUSTERED INDEX [IX_RoleUser_UserId] ON [dbo].[RoleUser]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Session_UserId]    Script Date: 14/06/2022 20:04:51 ******/
CREATE NONCLUSTERED INDEX [IX_Session_UserId] ON [dbo].[Session]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Soloist_ArtistId]    Script Date: 14/06/2022 20:04:51 ******/
CREATE NONCLUSTERED INDEX [IX_Soloist_ArtistId] ON [dbo].[Soloist]
(
	[ArtistId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Soloist_RoleArtistId]    Script Date: 14/06/2022 20:04:51 ******/
CREATE NONCLUSTERED INDEX [IX_Soloist_RoleArtistId] ON [dbo].[Soloist]
(
	[RoleArtistId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Ticket_ConcertId]    Script Date: 14/06/2022 20:04:51 ******/
CREATE NONCLUSTERED INDEX [IX_Ticket_ConcertId] ON [dbo].[Ticket]
(
	[ConcertId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Ticket_TicketStatusId]    Script Date: 14/06/2022 20:04:51 ******/
CREATE NONCLUSTERED INDEX [IX_Ticket_TicketStatusId] ON [dbo].[Ticket]
(
	[TicketStatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_TicketStatus_Status]    Script Date: 14/06/2022 20:04:51 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_TicketStatus_Status] ON [dbo].[TicketStatus]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_User_Email]    Script Date: 14/06/2022 20:04:51 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_User_Email] ON [dbo].[User]
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Artist] ADD  DEFAULT ((0)) FOR [UserId]
GO
ALTER TABLE [dbo].[ArtistBand] ADD  DEFAULT ((0)) FOR [RoleArtistId]
GO
ALTER TABLE [dbo].[Concert] ADD  DEFAULT ((0)) FOR [LocationId]
GO
ALTER TABLE [dbo].[Location] ADD  DEFAULT ((0)) FOR [CountryId]
GO
ALTER TABLE [dbo].[Soloist] ADD  DEFAULT ((0)) FOR [RoleArtistId]
GO
ALTER TABLE [dbo].[Artist]  WITH CHECK ADD  CONSTRAINT [FK_Artist_User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Artist] CHECK CONSTRAINT [FK_Artist_User_UserId]
GO
ALTER TABLE [dbo].[ArtistBand]  WITH CHECK ADD  CONSTRAINT [FK_ArtistBand_Artist_ArtistId] FOREIGN KEY([ArtistId])
REFERENCES [dbo].[Artist] ([ArtistId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ArtistBand] CHECK CONSTRAINT [FK_ArtistBand_Artist_ArtistId]
GO
ALTER TABLE [dbo].[ArtistBand]  WITH CHECK ADD  CONSTRAINT [FK_ArtistBand_Band_MusicalProtagonistId] FOREIGN KEY([MusicalProtagonistId])
REFERENCES [dbo].[Band] ([MusicalProtagonistId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ArtistBand] CHECK CONSTRAINT [FK_ArtistBand_Band_MusicalProtagonistId]
GO
ALTER TABLE [dbo].[ArtistBand]  WITH CHECK ADD  CONSTRAINT [FK_ArtistBand_RoleArtist_RoleArtistId] FOREIGN KEY([RoleArtistId])
REFERENCES [dbo].[RoleArtist] ([RoleArtistId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ArtistBand] CHECK CONSTRAINT [FK_ArtistBand_RoleArtist_RoleArtistId]
GO
ALTER TABLE [dbo].[Band]  WITH CHECK ADD  CONSTRAINT [FK_Band_MusicalProtagonist_MusicalProtagonistId] FOREIGN KEY([MusicalProtagonistId])
REFERENCES [dbo].[MusicalProtagonist] ([MusicalProtagonistId])
GO
ALTER TABLE [dbo].[Band] CHECK CONSTRAINT [FK_Band_MusicalProtagonist_MusicalProtagonistId]
GO
ALTER TABLE [dbo].[Concert]  WITH CHECK ADD  CONSTRAINT [FK_Concert_Location_LocationId] FOREIGN KEY([LocationId])
REFERENCES [dbo].[Location] ([LocationId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Concert] CHECK CONSTRAINT [FK_Concert_Location_LocationId]
GO
ALTER TABLE [dbo].[ConcertProtagonist]  WITH CHECK ADD  CONSTRAINT [FK_ConcertProtagonist_Concert_ConcertId] FOREIGN KEY([ConcertId])
REFERENCES [dbo].[Concert] ([ConcertId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ConcertProtagonist] CHECK CONSTRAINT [FK_ConcertProtagonist_Concert_ConcertId]
GO
ALTER TABLE [dbo].[ConcertProtagonist]  WITH CHECK ADD  CONSTRAINT [FK_ConcertProtagonist_MusicalProtagonist_MusicalProtagonistId] FOREIGN KEY([MusicalProtagonistId])
REFERENCES [dbo].[MusicalProtagonist] ([MusicalProtagonistId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ConcertProtagonist] CHECK CONSTRAINT [FK_ConcertProtagonist_MusicalProtagonist_MusicalProtagonistId]
GO
ALTER TABLE [dbo].[Location]  WITH CHECK ADD  CONSTRAINT [FK_Location_Countrys_CountryId] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Countrys] ([CountryId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Location] CHECK CONSTRAINT [FK_Location_Countrys_CountryId]
GO
ALTER TABLE [dbo].[MusicalProtagonist]  WITH CHECK ADD  CONSTRAINT [FK_MusicalProtagonist_Gender_GenderId] FOREIGN KEY([GenderId])
REFERENCES [dbo].[Gender] ([GenderId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MusicalProtagonist] CHECK CONSTRAINT [FK_MusicalProtagonist_Gender_GenderId]
GO
ALTER TABLE [dbo].[RoleUser]  WITH CHECK ADD  CONSTRAINT [FK_RoleUser_Role_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([RoleId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RoleUser] CHECK CONSTRAINT [FK_RoleUser_Role_RoleId]
GO
ALTER TABLE [dbo].[RoleUser]  WITH CHECK ADD  CONSTRAINT [FK_RoleUser_User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RoleUser] CHECK CONSTRAINT [FK_RoleUser_User_UserId]
GO
ALTER TABLE [dbo].[Session]  WITH CHECK ADD  CONSTRAINT [FK_Session_User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Session] CHECK CONSTRAINT [FK_Session_User_UserId]
GO
ALTER TABLE [dbo].[Soloist]  WITH CHECK ADD  CONSTRAINT [FK_Soloist_Artist_ArtistId] FOREIGN KEY([ArtistId])
REFERENCES [dbo].[Artist] ([ArtistId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Soloist] CHECK CONSTRAINT [FK_Soloist_Artist_ArtistId]
GO
ALTER TABLE [dbo].[Soloist]  WITH CHECK ADD  CONSTRAINT [FK_Soloist_MusicalProtagonist_MusicalProtagonistId] FOREIGN KEY([MusicalProtagonistId])
REFERENCES [dbo].[MusicalProtagonist] ([MusicalProtagonistId])
GO
ALTER TABLE [dbo].[Soloist] CHECK CONSTRAINT [FK_Soloist_MusicalProtagonist_MusicalProtagonistId]
GO
ALTER TABLE [dbo].[Soloist]  WITH CHECK ADD  CONSTRAINT [FK_Soloist_RoleArtist_RoleArtistId] FOREIGN KEY([RoleArtistId])
REFERENCES [dbo].[RoleArtist] ([RoleArtistId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Soloist] CHECK CONSTRAINT [FK_Soloist_RoleArtist_RoleArtistId]
GO
ALTER TABLE [dbo].[Ticket]  WITH CHECK ADD  CONSTRAINT [FK_Ticket_Concert_ConcertId] FOREIGN KEY([ConcertId])
REFERENCES [dbo].[Concert] ([ConcertId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Ticket] CHECK CONSTRAINT [FK_Ticket_Concert_ConcertId]
GO
ALTER TABLE [dbo].[Ticket]  WITH CHECK ADD  CONSTRAINT [FK_Ticket_TicketStatus_TicketStatusId] FOREIGN KEY([TicketStatusId])
REFERENCES [dbo].[TicketStatus] ([TicketStatusId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Ticket] CHECK CONSTRAINT [FK_Ticket_TicketStatus_TicketStatusId]
GO
USE [master]
GO
ALTER DATABASE [ArenaGestorDB] SET  READ_WRITE 
GO
