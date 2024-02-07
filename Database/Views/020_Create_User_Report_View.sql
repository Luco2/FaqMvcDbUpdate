-- Script to create a view for user reporting

CREATE VIEW [dbo].[View_UserReport]
AS
SELECT 
    u.UserId,
    u.Username,
    u.Email,
    u.CreateDate,
    u.LastLoginDate
FROM 
    [dbo].[User] u
WHERE 
    u.IsActive = 1 -- Assuming there is an 'IsActive' column to filter active users
GO
