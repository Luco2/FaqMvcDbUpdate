-- Script to create indexes for performance optimization

CREATE NONCLUSTERED INDEX [IX_User_Email]
ON [dbo].[AspNetUsers] ([Email])
INCLUDE ([Username], [LastLoginDate])
WHERE IsActive = 1 -- Filtered index for active users
GO

CREATE NONCLUSTERED INDEX [IX_User_LastLoginDate]
ON [dbo].[AspNetUsers] ([LastLoginDate])
GO
