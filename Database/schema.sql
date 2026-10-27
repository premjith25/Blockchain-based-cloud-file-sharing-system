-- Database schema for Blockchain-based Cloud File Sharing System
-- Owner: Person 1

CREATE DATABASE BlockchainFileShare;
GO

USE BlockchainFileShare;
GO

-- Users: registered users and admins
CREATE TABLE Users (
    UserId       INT IDENTITY(1,1) PRIMARY KEY,
    FullName     NVARCHAR(100) NOT NULL,
    Email        NVARCHAR(150) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL,
    Role         NVARCHAR(10)  NOT NULL DEFAULT 'User' CHECK (Role IN ('User', 'Admin')),
    CreatedAt    DATETIME      NOT NULL DEFAULT GETDATE()
);
GO

-- Files: details of every uploaded file (the file itself is stored encrypted on disk)
CREATE TABLE Files (
    FileId      INT IDENTITY(1,1) PRIMARY KEY,
    OwnerId     INT           NOT NULL,
    FileName    NVARCHAR(255) NOT NULL,
    StoragePath NVARCHAR(500) NOT NULL,
    FileSize    BIGINT        NULL,
    UploadedAt  DATETIME      NOT NULL DEFAULT GETDATE(),
    IsTampered  BIT           NOT NULL DEFAULT 0,
    CONSTRAINT FK_Files_Users FOREIGN KEY (OwnerId) REFERENCES Users(UserId)
);
GO
