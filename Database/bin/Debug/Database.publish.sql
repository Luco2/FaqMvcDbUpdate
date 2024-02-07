﻿/*
Deployment script for FaqSqlDb

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "FaqSqlDb"
:setvar DefaultFilePrefix "FaqSqlDb"
:setvar DefaultDataPath "C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\"
:setvar DefaultLogPath "C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
PRINT N'The following operation was generated from a refactoring log file 8dd14e0d-e822-445f-9991-8ba1821859e0, b646978a-8b31-41e3-92a3-9409135049b0';

PRINT N'Rename [dbo].[AspNetRoles] to Role';


GO
EXECUTE sp_rename @objname = N'[dbo].[AspNetRoles]', @newname = N'Role', @objtype = N'OBJECT';


GO
PRINT N'The following operation was generated from a refactoring log file 79c1d9d8-c753-4aaf-ab2b-ca4ef5177d1c';

PRINT N'Rename [dbo].[AspNetUsers] to Users';


GO
EXECUTE sp_rename @objname = N'[dbo].[AspNetUsers]', @newname = N'Users', @objtype = N'OBJECT';


GO
-- Refactoring step to update target server with deployed transaction logs

IF OBJECT_ID(N'dbo.__RefactorLog') IS NULL
BEGIN
    CREATE TABLE [dbo].[__RefactorLog] (OperationKey UNIQUEIDENTIFIER NOT NULL PRIMARY KEY)
    EXEC sp_addextendedproperty N'microsoft_database_tools_support', N'refactoring log', N'schema', N'dbo', N'table', N'__RefactorLog'
END
GO
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '8dd14e0d-e822-445f-9991-8ba1821859e0')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('8dd14e0d-e822-445f-9991-8ba1821859e0')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'b646978a-8b31-41e3-92a3-9409135049b0')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('b646978a-8b31-41e3-92a3-9409135049b0')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '79c1d9d8-c753-4aaf-ab2b-ca4ef5177d1c')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('79c1d9d8-c753-4aaf-ab2b-ca4ef5177d1c')

GO

GO
PRINT N'Update complete.';


GO
