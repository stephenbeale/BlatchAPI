USE BlatchBlob
GO

-- Drop existing tables if they exist (in correct order due to foreign keys)
IF OBJECT_ID('dbo.Colleagues', 'U') IS NOT NULL DROP TABLE dbo.Colleagues
IF OBJECT_ID('dbo.UserSkills', 'U') IS NOT NULL DROP TABLE dbo.UserSkills
IF OBJECT_ID('dbo.Skills', 'U') IS NOT NULL DROP TABLE dbo.Skills
IF OBJECT_ID('dbo.Users', 'U') IS NOT NULL DROP TABLE dbo.Users
IF OBJECT_ID('dbo.Addresses', 'U') IS NOT NULL DROP TABLE dbo.Addresses
GO

-- Create Address table first (referenced by Users)
CREATE TABLE Addresses (
    [ID] [uniqueidentifier] PRIMARY KEY DEFAULT NEWID(),
    [StreetNumber] [nvarchar](50) NULL,
    [StreetName] [nvarchar](200) NULL,
    [City] [nvarchar](100) NULL,
    [StateOrCounty] [nvarchar](100) NULL,
    [PostalCode] [nvarchar](20) NULL,
    [Country] [nvarchar](100) NULL
)
GO

-- Create Users table
CREATE TABLE Users (
    [ID] [uniqueidentifier] PRIMARY KEY,
    [FirstName] [nvarchar](100) NULL,
    [LastName] [nvarchar](100) NULL,
    [Email] [nvarchar](200) NULL,
    [Phone] [nvarchar](50) NULL,
    [AddressID] [uniqueidentifier] NULL,
    [Age] [int] NULL,
    [Gender] [nvarchar](20) NULL,
    [Company] [nvarchar](200) NULL,
    [Department] [nvarchar](100) NULL,
    [HeadshotImage] [nvarchar](max) NULL,
    [Longitude] [float] NULL,
    [Latitude] [float] NULL,
    [EmploymentStart] [datetimeoffset] NULL,
    [EmploymentEnd] [datetimeoffset] NULL,
    [FullName] [nvarchar](200) NULL,
    CONSTRAINT FK_Users_Address FOREIGN KEY ([AddressID]) 
        REFERENCES Addresses([ID])
)
GO

-- Create Skills table
CREATE TABLE Skills (
    [ID] [uniqueidentifier] PRIMARY KEY DEFAULT NEWID(),
    [Name] [nvarchar](100) NOT NULL UNIQUE
)
GO

-- Create junction table for User-Skills many-to-many relationship
CREATE TABLE UserSkills (
    [UserID] [uniqueidentifier] NOT NULL,
    [SkillID] [uniqueidentifier] NOT NULL,
    PRIMARY KEY ([UserID], [SkillID]),
    CONSTRAINT FK_UserSkills_Users FOREIGN KEY ([UserID]) 
        REFERENCES Users([ID]) ON DELETE CASCADE,
    CONSTRAINT FK_UserSkills_Skills FOREIGN KEY ([SkillID]) 
        REFERENCES Skills([ID]) ON DELETE CASCADE
)
GO

-- Create Colleagues table (self-referencing many-to-many)
CREATE TABLE Colleagues (
    [UserID] [uniqueidentifier] NOT NULL,
    [ColleagueID] [uniqueidentifier] NOT NULL,
    PRIMARY KEY ([UserID], [ColleagueID]),
    CONSTRAINT FK_Colleagues_User FOREIGN KEY ([UserID]) 
        REFERENCES Users([ID]),
    CONSTRAINT FK_Colleagues_Colleague FOREIGN KEY ([ColleagueID]) 
        REFERENCES Users([ID]),
    CONSTRAINT CHK_Colleagues_Different CHECK ([UserID] != [ColleagueID])
)
GO
