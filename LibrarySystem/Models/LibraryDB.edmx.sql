
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/10/2020 21:40:59
-- Generated from EDMX file: E:\webApplication-Library\LibrarySystem\LibrarySystem\Models\LibraryDB.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [aspnet-LibrarySystem-20200904024101];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Books]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Books];
GO
IF OBJECT_ID(N'[dbo].[Weights]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Weights];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Books'
CREATE TABLE [dbo].[Books] (
    [CallNum] nvarchar(50)  NOT NULL,
    [Author] nvarchar(max)  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [ISBN] nvarchar(max)  NOT NULL,
    [Duplicates] int  NOT NULL,
    [CirculationFrequency] float  NOT NULL,
    [PublisherName] nvarchar(max)  NOT NULL,
    [PublisherBaiJia] bit  NOT NULL,
    [CirculationCount] int  NOT NULL,
    [PublishYear] int  NOT NULL,
    [YearCount] int  NOT NULL,
    [OffTime] int  NOT NULL,
    [OffTimeCount] int  NOT NULL,
    [BWI] float  NOT NULL,
    [Id] bigint  NOT NULL
);
GO

-- Creating table 'Weights'
CREATE TABLE [dbo].[Weights] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Wyear] float  NOT NULL,
    [WBaijia] float  NOT NULL,
    [Wduplication] float  NOT NULL,
    [WOffTime] nvarchar(max)  NOT NULL,
    [WcirculationFequency] float  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Books'
ALTER TABLE [dbo].[Books]
ADD CONSTRAINT [PK_Books]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Weights'
ALTER TABLE [dbo].[Weights]
ADD CONSTRAINT [PK_Weights]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------