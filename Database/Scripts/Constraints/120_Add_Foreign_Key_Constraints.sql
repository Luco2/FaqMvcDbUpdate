-- Add foreign key constraint to UserPrompts table referencing the User table
ALTER TABLE dbo.UserPrompts
ADD CONSTRAINT FK_UserPrompts_User
FOREIGN KEY (UserId) REFERENCES dbo.[User] (UserId);
GO
