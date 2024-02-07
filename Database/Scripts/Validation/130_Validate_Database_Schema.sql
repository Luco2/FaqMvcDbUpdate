-- Script to validate the database schema

-- Check if User table exists
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'User')
RAISERROR ('The User table does not exist.', 16, 1);

-- Check if User table has expected columns
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'User' AND COLUMN_NAME = 'Email')
RAISERROR ('The Email column is missing from the User table.', 16, 1);

-- Add more checks as needed for other tables, columns, constraints, etc.

GO
