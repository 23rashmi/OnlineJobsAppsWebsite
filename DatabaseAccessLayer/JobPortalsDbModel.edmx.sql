
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/04/2024 10:07:02
-- Generated from EDMX file: C:\Users\shrey\Downloads\JobsPortal-first-master\JobsPortal-first-master\DatabaseAccessLayer\JobPortalsDbModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [JobsPortalDb];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_PostJobTable_CompanyTable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PostJobTables] DROP CONSTRAINT [FK_PostJobTable_CompanyTable];
GO
IF OBJECT_ID(N'[dbo].[FK_PostJobTable_JobCategoryTable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PostJobTables] DROP CONSTRAINT [FK_PostJobTable_JobCategoryTable];
GO
IF OBJECT_ID(N'[dbo].[FK_PostJobTable_JobNatureTable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PostJobTables] DROP CONSTRAINT [FK_PostJobTable_JobNatureTable];
GO
IF OBJECT_ID(N'[dbo].[FK_JobRequirementDetailsTable_JobRequirementsTable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[JobRequirementDetailsTables] DROP CONSTRAINT [FK_JobRequirementDetailsTable_JobRequirementsTable];
GO
IF OBJECT_ID(N'[dbo].[FK_JobRequirementDetailsTable_PostJobTable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[JobRequirementDetailsTables] DROP CONSTRAINT [FK_JobRequirementDetailsTable_PostJobTable];
GO
IF OBJECT_ID(N'[dbo].[FK_PostJobTable_JobStatusTable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PostJobTables] DROP CONSTRAINT [FK_PostJobTable_JobStatusTable];
GO
IF OBJECT_ID(N'[dbo].[FK_CompanyTable_UserTable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CompanyTables] DROP CONSTRAINT [FK_CompanyTable_UserTable];
GO
IF OBJECT_ID(N'[dbo].[FK_PostJobTable_UserTable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PostJobTables] DROP CONSTRAINT [FK_PostJobTable_UserTable];
GO
IF OBJECT_ID(N'[dbo].[FK_UserTypeTable_UserTable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserTables] DROP CONSTRAINT [FK_UserTypeTable_UserTable];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeesTables_PostJobTables]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeesTables] DROP CONSTRAINT [FK_EmployeesTables_PostJobTables];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeesTables_UserTables]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeesTables] DROP CONSTRAINT [FK_EmployeesTables_UserTables];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[CompanyTables]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CompanyTables];
GO
IF OBJECT_ID(N'[dbo].[JobCategoryTables]', 'U') IS NOT NULL
    DROP TABLE [dbo].[JobCategoryTables];
GO
IF OBJECT_ID(N'[dbo].[JobNatureTables]', 'U') IS NOT NULL
    DROP TABLE [dbo].[JobNatureTables];
GO
IF OBJECT_ID(N'[dbo].[JobRequirementDetailsTables]', 'U') IS NOT NULL
    DROP TABLE [dbo].[JobRequirementDetailsTables];
GO
IF OBJECT_ID(N'[dbo].[JobRequirementsTables]', 'U') IS NOT NULL
    DROP TABLE [dbo].[JobRequirementsTables];
GO
IF OBJECT_ID(N'[dbo].[JobStatusTables]', 'U') IS NOT NULL
    DROP TABLE [dbo].[JobStatusTables];
GO
IF OBJECT_ID(N'[dbo].[PostJobTables]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PostJobTables];
GO
IF OBJECT_ID(N'[dbo].[UserTypeTables]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserTypeTables];
GO
IF OBJECT_ID(N'[dbo].[UserTables]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserTables];
GO
IF OBJECT_ID(N'[dbo].[EmployeesTables]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmployeesTables];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'CompanyTables'
CREATE TABLE [dbo].[CompanyTables] (
    [CompanyID] int IDENTITY(1,1) NOT NULL,
    [UserID] int  NOT NULL,
    [CompanyName] nvarchar(500)  NOT NULL,
    [ContactNo] nvarchar(20)  NOT NULL,
    [PhoneNo] nvarchar(150)  NOT NULL,
    [EmailAddress] nvarchar(500)  NOT NULL,
    [Logo] nvarchar(max)  NOT NULL,
    [Description] nvarchar(1000)  NULL
);
GO

-- Creating table 'JobCategoryTables'
CREATE TABLE [dbo].[JobCategoryTables] (
    [JobCategoryID] int IDENTITY(1,1) NOT NULL,
    [JobCategory] nvarchar(max)  NOT NULL,
    [Description] nvarchar(500)  NULL
);
GO

-- Creating table 'JobNatureTables'
CREATE TABLE [dbo].[JobNatureTables] (
    [JobNatureID] int IDENTITY(1,1) NOT NULL,
    [JobNature] nvarchar(100)  NOT NULL
);
GO

-- Creating table 'JobRequirementDetailsTables'
CREATE TABLE [dbo].[JobRequirementDetailsTables] (
    [JobRequirementDetailsID] int IDENTITY(1,1) NOT NULL,
    [JobRequirementID] int  NOT NULL,
    [JobRequirementDetails] nvarchar(2000)  NOT NULL,
    [PostJobID] int  NULL
);
GO

-- Creating table 'JobRequirementsTables'
CREATE TABLE [dbo].[JobRequirementsTables] (
    [JobRequirementID] int IDENTITY(1,1) NOT NULL,
    [JobRequirement] nvarchar(1000)  NOT NULL
);
GO

-- Creating table 'JobStatusTables'
CREATE TABLE [dbo].[JobStatusTables] (
    [JobStatusID] int IDENTITY(1,1) NOT NULL,
    [JobStatus] nvarchar(150)  NOT NULL,
    [StatusMessage] nvarchar(2000)  NULL
);
GO

-- Creating table 'PostJobTables'
CREATE TABLE [dbo].[PostJobTables] (
    [PostJobID] int IDENTITY(1,1) NOT NULL,
    [UserID] int  NOT NULL,
    [CompanyID] int  NOT NULL,
    [JobCategoryID] int  NOT NULL,
    [JobTitle] nvarchar(500)  NOT NULL,
    [JobDescription] nvarchar(2000)  NOT NULL,
    [MinSalary] int  NOT NULL,
    [MaxSalary] int  NOT NULL,
    [Location] nvarchar(500)  NOT NULL,
    [Vacancy] int  NOT NULL,
    [JobNatureID] int  NOT NULL,
    [PostDate] datetime  NOT NULL,
    [ApplicationDeadline] datetime  NOT NULL,
    [LastDate] datetime  NOT NULL,
    [JobStatusID] int  NOT NULL
);
GO

-- Creating table 'UserTypeTables'
CREATE TABLE [dbo].[UserTypeTables] (
    [UserTypeID] int IDENTITY(1,1) NOT NULL,
    [UserType] nvarchar(150)  NULL
);
GO

-- Creating table 'UserTables'
CREATE TABLE [dbo].[UserTables] (
    [UserID] int IDENTITY(1,1) NOT NULL,
    [UserTypeID] int  NOT NULL,
    [UserName] nvarchar(150)  NOT NULL,
    [Password] nvarchar(20)  NOT NULL,
    [EmailAddress] nvarchar(150)  NOT NULL,
    [ContactNo] nvarchar(20)  NOT NULL,
    [Preferred_location] nvarchar(50)  NULL,
    [Skills] nvarchar(50)  NULL,
    [Resume] nvarchar(max)  NULL
);
GO

-- Creating table 'EmployeesTables'
CREATE TABLE [dbo].[EmployeesTables] (
    [EmployeeID] int IDENTITY(1,1) NOT NULL,
    [UserId] int  NOT NULL,
    [EmployeeName] nvarchar(150)  NOT NULL,
    [DOB] datetime  NULL,
    [Education] varchar(200)  NULL,
    [WorkExperience] varchar(500)  NULL,
    [Skills] nvarchar(500)  NULL,
    [EmailAddress] nvarchar(150)  NOT NULL,
    [Gender] nvarchar(20)  NOT NULL,
    [Qualification] nvarchar(150)  NULL,
    [PermanentAddress] nvarchar(500)  NULL,
    [JobReference] nvarchar(250)  NULL,
    [Description] nvarchar(4000)  NULL,
    [Photo] nvarchar(max)  NULL,
    [Resume] nvarchar(max)  NULL,
    [PostJobID] int  NULL,
    [JobTitle] nvarchar(500)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [CompanyID] in table 'CompanyTables'
ALTER TABLE [dbo].[CompanyTables]
ADD CONSTRAINT [PK_CompanyTables]
    PRIMARY KEY CLUSTERED ([CompanyID] ASC);
GO

-- Creating primary key on [JobCategoryID] in table 'JobCategoryTables'
ALTER TABLE [dbo].[JobCategoryTables]
ADD CONSTRAINT [PK_JobCategoryTables]
    PRIMARY KEY CLUSTERED ([JobCategoryID] ASC);
GO

-- Creating primary key on [JobNatureID] in table 'JobNatureTables'
ALTER TABLE [dbo].[JobNatureTables]
ADD CONSTRAINT [PK_JobNatureTables]
    PRIMARY KEY CLUSTERED ([JobNatureID] ASC);
GO

-- Creating primary key on [JobRequirementDetailsID] in table 'JobRequirementDetailsTables'
ALTER TABLE [dbo].[JobRequirementDetailsTables]
ADD CONSTRAINT [PK_JobRequirementDetailsTables]
    PRIMARY KEY CLUSTERED ([JobRequirementDetailsID] ASC);
GO

-- Creating primary key on [JobRequirementID] in table 'JobRequirementsTables'
ALTER TABLE [dbo].[JobRequirementsTables]
ADD CONSTRAINT [PK_JobRequirementsTables]
    PRIMARY KEY CLUSTERED ([JobRequirementID] ASC);
GO

-- Creating primary key on [JobStatusID] in table 'JobStatusTables'
ALTER TABLE [dbo].[JobStatusTables]
ADD CONSTRAINT [PK_JobStatusTables]
    PRIMARY KEY CLUSTERED ([JobStatusID] ASC);
GO

-- Creating primary key on [PostJobID] in table 'PostJobTables'
ALTER TABLE [dbo].[PostJobTables]
ADD CONSTRAINT [PK_PostJobTables]
    PRIMARY KEY CLUSTERED ([PostJobID] ASC);
GO

-- Creating primary key on [UserTypeID] in table 'UserTypeTables'
ALTER TABLE [dbo].[UserTypeTables]
ADD CONSTRAINT [PK_UserTypeTables]
    PRIMARY KEY CLUSTERED ([UserTypeID] ASC);
GO

-- Creating primary key on [UserID] in table 'UserTables'
ALTER TABLE [dbo].[UserTables]
ADD CONSTRAINT [PK_UserTables]
    PRIMARY KEY CLUSTERED ([UserID] ASC);
GO

-- Creating primary key on [EmployeeID] in table 'EmployeesTables'
ALTER TABLE [dbo].[EmployeesTables]
ADD CONSTRAINT [PK_EmployeesTables]
    PRIMARY KEY CLUSTERED ([EmployeeID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CompanyID] in table 'PostJobTables'
ALTER TABLE [dbo].[PostJobTables]
ADD CONSTRAINT [FK_PostJobTable_CompanyTable]
    FOREIGN KEY ([CompanyID])
    REFERENCES [dbo].[CompanyTables]
        ([CompanyID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PostJobTable_CompanyTable'
CREATE INDEX [IX_FK_PostJobTable_CompanyTable]
ON [dbo].[PostJobTables]
    ([CompanyID]);
GO

-- Creating foreign key on [JobCategoryID] in table 'PostJobTables'
ALTER TABLE [dbo].[PostJobTables]
ADD CONSTRAINT [FK_PostJobTable_JobCategoryTable]
    FOREIGN KEY ([JobCategoryID])
    REFERENCES [dbo].[JobCategoryTables]
        ([JobCategoryID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PostJobTable_JobCategoryTable'
CREATE INDEX [IX_FK_PostJobTable_JobCategoryTable]
ON [dbo].[PostJobTables]
    ([JobCategoryID]);
GO

-- Creating foreign key on [JobNatureID] in table 'PostJobTables'
ALTER TABLE [dbo].[PostJobTables]
ADD CONSTRAINT [FK_PostJobTable_JobNatureTable]
    FOREIGN KEY ([JobNatureID])
    REFERENCES [dbo].[JobNatureTables]
        ([JobNatureID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PostJobTable_JobNatureTable'
CREATE INDEX [IX_FK_PostJobTable_JobNatureTable]
ON [dbo].[PostJobTables]
    ([JobNatureID]);
GO

-- Creating foreign key on [JobRequirementID] in table 'JobRequirementDetailsTables'
ALTER TABLE [dbo].[JobRequirementDetailsTables]
ADD CONSTRAINT [FK_JobRequirementDetailsTable_JobRequirementsTable]
    FOREIGN KEY ([JobRequirementID])
    REFERENCES [dbo].[JobRequirementsTables]
        ([JobRequirementID])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_JobRequirementDetailsTable_JobRequirementsTable'
CREATE INDEX [IX_FK_JobRequirementDetailsTable_JobRequirementsTable]
ON [dbo].[JobRequirementDetailsTables]
    ([JobRequirementID]);
GO

-- Creating foreign key on [PostJobID] in table 'JobRequirementDetailsTables'
ALTER TABLE [dbo].[JobRequirementDetailsTables]
ADD CONSTRAINT [FK_JobRequirementDetailsTable_PostJobTable]
    FOREIGN KEY ([PostJobID])
    REFERENCES [dbo].[PostJobTables]
        ([PostJobID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_JobRequirementDetailsTable_PostJobTable'
CREATE INDEX [IX_FK_JobRequirementDetailsTable_PostJobTable]
ON [dbo].[JobRequirementDetailsTables]
    ([PostJobID]);
GO

-- Creating foreign key on [JobStatusID] in table 'PostJobTables'
ALTER TABLE [dbo].[PostJobTables]
ADD CONSTRAINT [FK_PostJobTable_JobStatusTable]
    FOREIGN KEY ([JobStatusID])
    REFERENCES [dbo].[JobStatusTables]
        ([JobStatusID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PostJobTable_JobStatusTable'
CREATE INDEX [IX_FK_PostJobTable_JobStatusTable]
ON [dbo].[PostJobTables]
    ([JobStatusID]);
GO

-- Creating foreign key on [UserID] in table 'CompanyTables'
ALTER TABLE [dbo].[CompanyTables]
ADD CONSTRAINT [FK_CompanyTable_UserTable]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[UserTables]
        ([UserID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CompanyTable_UserTable'
CREATE INDEX [IX_FK_CompanyTable_UserTable]
ON [dbo].[CompanyTables]
    ([UserID]);
GO

-- Creating foreign key on [UserID] in table 'PostJobTables'
ALTER TABLE [dbo].[PostJobTables]
ADD CONSTRAINT [FK_PostJobTable_UserTable]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[UserTables]
        ([UserID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PostJobTable_UserTable'
CREATE INDEX [IX_FK_PostJobTable_UserTable]
ON [dbo].[PostJobTables]
    ([UserID]);
GO

-- Creating foreign key on [UserTypeID] in table 'UserTables'
ALTER TABLE [dbo].[UserTables]
ADD CONSTRAINT [FK_UserTypeTable_UserTable]
    FOREIGN KEY ([UserTypeID])
    REFERENCES [dbo].[UserTypeTables]
        ([UserTypeID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserTypeTable_UserTable'
CREATE INDEX [IX_FK_UserTypeTable_UserTable]
ON [dbo].[UserTables]
    ([UserTypeID]);
GO

-- Creating foreign key on [PostJobID] in table 'EmployeesTables'
ALTER TABLE [dbo].[EmployeesTables]
ADD CONSTRAINT [FK_EmployeesTables_PostJobTables]
    FOREIGN KEY ([PostJobID])
    REFERENCES [dbo].[PostJobTables]
        ([PostJobID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeesTables_PostJobTables'
CREATE INDEX [IX_FK_EmployeesTables_PostJobTables]
ON [dbo].[EmployeesTables]
    ([PostJobID]);
GO

-- Creating foreign key on [UserId] in table 'EmployeesTables'
ALTER TABLE [dbo].[EmployeesTables]
ADD CONSTRAINT [FK_EmployeesTables_UserTables]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[UserTables]
        ([UserID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmployeesTables_UserTables'
CREATE INDEX [IX_FK_EmployeesTables_UserTables]
ON [dbo].[EmployeesTables]
    ([UserId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------