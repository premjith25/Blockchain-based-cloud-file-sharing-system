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
