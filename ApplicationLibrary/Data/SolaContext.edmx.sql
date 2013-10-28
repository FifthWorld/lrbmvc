
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 09/02/2013 14:31:13
-- Generated from EDMX file: C:\Users\tomiwa\Documents\Visual Studio 2012\Projects\OndoLRBWeb\ApplicationLibrary\Data\SolaContext.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [SolaApplicationForms];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_CitizenAddress]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Addresses] DROP CONSTRAINT [FK_CitizenAddress];
GO
IF OBJECT_ID(N'[dbo].[FK_PropertyAddress]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Addresses] DROP CONSTRAINT [FK_PropertyAddress];
GO
IF OBJECT_ID(N'[dbo].[FK_PartyAddress]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Addresses] DROP CONSTRAINT [FK_PartyAddress];
GO
IF OBJECT_ID(N'[dbo].[FK_ApplicationProperty]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Properties] DROP CONSTRAINT [FK_ApplicationProperty];
GO
IF OBJECT_ID(N'[dbo].[FK_ApplicationParty]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Parties] DROP CONSTRAINT [FK_ApplicationParty];
GO
IF OBJECT_ID(N'[dbo].[FK_ApplicationDocument]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Documents] DROP CONSTRAINT [FK_ApplicationDocument];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Parties]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Parties];
GO
IF OBJECT_ID(N'[dbo].[Documents]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Documents];
GO
IF OBJECT_ID(N'[dbo].[Properties]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Properties];
GO
IF OBJECT_ID(N'[dbo].[Applications]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Applications];
GO
IF OBJECT_ID(N'[dbo].[Citizens]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Citizens];
GO
IF OBJECT_ID(N'[dbo].[Addresses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Addresses];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Parties'
CREATE TABLE [dbo].[Parties] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Surname] nvarchar(max)  NULL,
    [Firstname] nvarchar(max)  NULL,
    [Middlename] nvarchar(max)  NULL,
    [Gender] nvarchar(max)  NOT NULL,
    [DOB] datetime  NULL,
    [StateofOrigin] nvarchar(max)  NULL,
    [HomeTown] nvarchar(max)  NULL,
    [LGA] nvarchar(max)  NULL,
    [OfficeNo] nvarchar(max)  NULL,
    [MobileNo] nvarchar(max)  NULL,
    [HomeNo] nvarchar(max)  NULL,
    [Fax] nvarchar(max)  NULL,
    [Email] nvarchar(max)  NULL,
    [PartyType] nvarchar(max)  NULL,
    [OrganizationName] nvarchar(max)  NULL,
    [Occupation] nvarchar(max)  NULL,
    [EmployerName] nvarchar(max)  NULL,
    [EmployerAddress] nvarchar(max)  NULL,
    [Application_Id] int  NOT NULL
);
GO

-- Creating table 'Documents'
CREATE TABLE [dbo].[Documents] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FileName] nvarchar(max)  NOT NULL,
    [DocumentType] nvarchar(max)  NOT NULL,
    [Extension] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Content] varbinary(max)  NOT NULL,
    [Application_Id] int  NOT NULL
);
GO

-- Creating table 'Properties'
CREATE TABLE [dbo].[Properties] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Developed] bit  NULL,
    [CapacityofOwnership] nvarchar(max)  NULL,
    [LandUse] nvarchar(max)  NULL,
    [PeriodofPossession] nvarchar(max)  NULL,
    [ApproximateArea] decimal(18,0)  NULL,
    [Application_Id] int  NOT NULL
);
GO

-- Creating table 'Applications'
CREATE TABLE [dbo].[Applications] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(max)  NOT NULL,
    [SubmittedbyApplicant] bit  NULL,
    [OtherRelevantInfo] nvarchar(max)  NULL,
    [SubmissionDate] datetime  NULL,
    [StartDate] datetime  NOT NULL,
    [Status] nvarchar(max)  NOT NULL,
    [SolaId] nvarchar(max)  NULL
);
GO

-- Creating table 'Citizens'
CREATE TABLE [dbo].[Citizens] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [RelationshiptoApplicant] nvarchar(max)  NULL,
    [Firstname] nvarchar(max)  NULL,
    [Lastname] nvarchar(max)  NULL,
    [Middlename] nvarchar(max)  NULL,
    [PostHeld] nvarchar(max)  NULL
);
GO

-- Creating table 'Addresses'
CREATE TABLE [dbo].[Addresses] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Street] nvarchar(max)  NOT NULL,
    [City] nvarchar(max)  NOT NULL,
    [State] nvarchar(max)  NOT NULL,
    [PMBNo] nvarchar(max)  NOT NULL,
    [Citizen_Id] int  NULL,
    [Property_Id] int  NULL,
    [Party_Id] int  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Parties'
ALTER TABLE [dbo].[Parties]
ADD CONSTRAINT [PK_Parties]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Documents'
ALTER TABLE [dbo].[Documents]
ADD CONSTRAINT [PK_Documents]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Properties'
ALTER TABLE [dbo].[Properties]
ADD CONSTRAINT [PK_Properties]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Applications'
ALTER TABLE [dbo].[Applications]
ADD CONSTRAINT [PK_Applications]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Citizens'
ALTER TABLE [dbo].[Citizens]
ADD CONSTRAINT [PK_Citizens]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Addresses'
ALTER TABLE [dbo].[Addresses]
ADD CONSTRAINT [PK_Addresses]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Citizen_Id] in table 'Addresses'
ALTER TABLE [dbo].[Addresses]
ADD CONSTRAINT [FK_CitizenAddress]
    FOREIGN KEY ([Citizen_Id])
    REFERENCES [dbo].[Citizens]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CitizenAddress'
CREATE INDEX [IX_FK_CitizenAddress]
ON [dbo].[Addresses]
    ([Citizen_Id]);
GO

-- Creating foreign key on [Property_Id] in table 'Addresses'
ALTER TABLE [dbo].[Addresses]
ADD CONSTRAINT [FK_PropertyAddress]
    FOREIGN KEY ([Property_Id])
    REFERENCES [dbo].[Properties]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PropertyAddress'
CREATE INDEX [IX_FK_PropertyAddress]
ON [dbo].[Addresses]
    ([Property_Id]);
GO

-- Creating foreign key on [Party_Id] in table 'Addresses'
ALTER TABLE [dbo].[Addresses]
ADD CONSTRAINT [FK_PartyAddress]
    FOREIGN KEY ([Party_Id])
    REFERENCES [dbo].[Parties]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PartyAddress'
CREATE INDEX [IX_FK_PartyAddress]
ON [dbo].[Addresses]
    ([Party_Id]);
GO

-- Creating foreign key on [Application_Id] in table 'Properties'
ALTER TABLE [dbo].[Properties]
ADD CONSTRAINT [FK_ApplicationProperty]
    FOREIGN KEY ([Application_Id])
    REFERENCES [dbo].[Applications]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ApplicationProperty'
CREATE INDEX [IX_FK_ApplicationProperty]
ON [dbo].[Properties]
    ([Application_Id]);
GO

-- Creating foreign key on [Application_Id] in table 'Parties'
ALTER TABLE [dbo].[Parties]
ADD CONSTRAINT [FK_ApplicationParty]
    FOREIGN KEY ([Application_Id])
    REFERENCES [dbo].[Applications]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ApplicationParty'
CREATE INDEX [IX_FK_ApplicationParty]
ON [dbo].[Parties]
    ([Application_Id]);
GO

-- Creating foreign key on [Application_Id] in table 'Documents'
ALTER TABLE [dbo].[Documents]
ADD CONSTRAINT [FK_ApplicationDocument]
    FOREIGN KEY ([Application_Id])
    REFERENCES [dbo].[Applications]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ApplicationDocument'
CREATE INDEX [IX_FK_ApplicationDocument]
ON [dbo].[Documents]
    ([Application_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------