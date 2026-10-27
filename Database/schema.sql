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

-- FileShares: who a file is shared with (revoking just switches IsActive to 0)
CREATE TABLE FileShares (
    ShareId          INT IDENTITY(1,1) PRIMARY KEY,
    FileId           INT      NOT NULL,
    SharedWithUserId INT      NOT NULL,
    SharedAt         DATETIME NOT NULL DEFAULT GETDATE(),
    IsActive         BIT      NOT NULL DEFAULT 1,
    CONSTRAINT FK_FileShares_Files FOREIGN KEY (FileId) REFERENCES Files(FileId),
    CONSTRAINT FK_FileShares_Users FOREIGN KEY (SharedWithUserId) REFERENCES Users(UserId)
);
GO

-- ActivityLogs: record of every file view and download
CREATE TABLE ActivityLogs (
    LogId     INT IDENTITY(1,1) PRIMARY KEY,
    UserId    INT          NOT NULL,
    FileId    INT          NOT NULL,
    Action    NVARCHAR(20) NOT NULL CHECK (Action IN ('View', 'Download')),
    LoggedAt  DATETIME     NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_ActivityLogs_Users FOREIGN KEY (UserId) REFERENCES Users(UserId),
    CONSTRAINT FK_ActivityLogs_Files FOREIGN KEY (FileId) REFERENCES Files(FileId)
);
GO
