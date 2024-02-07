-- Script to add a stored procedure for creating new users

CREATE PROCEDURE [dbo].[spAddUsers]
    @Username NVARCHAR(256),
    @Email NVARCHAR(256),
    @PasswordHash NVARCHAR(MAX),
    @CreateDate DATETIME2
AS
BEGIN
    -- Assuming unique Email is required, checking if it already exists
    IF EXISTS (SELECT 1 FROM [dbo].[AspNetUsers] WHERE [Email] = @Email)
    BEGIN
        RAISERROR ('A user with the provided email already exists.', 16, 1);
        RETURN;
    END

    INSERT INTO [dbo].[User] ([Username], [Email], [PasswordHash], [CreateDate])
    VALUES (@Username, @Email, @PasswordHash, @CreateDate)
END
GO
