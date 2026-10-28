# Blockchain-based Cloud File Sharing System

A web application where users upload, share and revoke access to files in the cloud. Files are encrypted before storage, and a blockchain of SHA-256 hashes detects any tampering.

## Tech Stack
- Front end: HTML, CSS, JavaScript
- Back end: ASP.NET (C#)
- Database: MSSQL
- Tools: Visual Studio, SQL Server Management Studio

## Modules
- User: register, login, upload, share, revoke, activity logs, feedback
- Admin: view users, tampered files, logs, feedback
- Blockchain and Encryption: SHA-256 hashing, block chaining, AES encryption

## Team
- Person 1: Database and Admin - (madan)
- Person 2: Blockchain and Encryption - (premjith/rohan/diwkar)
- Person 3: Accounts, Logs and Feedback - (premjith/rohan/diwkar)
- Person 4: File Management and Sharing - (premjith/rohan/diwkar)
- Person 5: UI, Testing and Documentation - (gaurav)

## Database Setup

Requires SQL Server. Windows: SQL Server Express or Developer edition. Mac: run SQL Server in Docker.

1. Mac only: start SQL Server in Docker. Choose your own password (8+ characters with upper case, lower case, a number and a symbol).

        docker run --platform linux/amd64 -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=YOUR_PASSWORD" -p 1433:1433 --name sqlserver -d mcr.microsoft.com/mssql/server:2022-latest

2. Create the database and tables by running `Database/schema.sql`.
   - Windows: open the file in SQL Server Management Studio and click Execute.
   - Mac (Docker):

        docker cp Database/schema.sql sqlserver:/tmp/schema.sql
        docker exec sqlserver /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "YOUR_PASSWORD" -C -i /tmp/schema.sql

3. The script creates a default admin account: `admin@fileshare.com` / `Admin@123`.

## Running the Admin Portal

Requires the .NET 10 SDK.

1. Create the file `AdminPortal/appsettings.Development.json` with your own connection string. This file is git-ignored, so your password never goes to GitHub.

        {
          "ConnectionStrings": {
            "DefaultConnection": "Server=localhost,1433;Database=BlockchainFileShare;User Id=sa;Password=YOUR_PASSWORD;TrustServerCertificate=True;"
          }
        }

2. Start the app:

        cd AdminPortal
        dotnet run --launch-profile http

3. Open the address shown in the terminal, then go to `/AdminLogin`.

## Rules for Committing
- Run `git pull` before you start work and before every `git push`.
- Commit small pieces of work with clear messages.
- Never commit passwords or connection strings.
