-- Insert test data into the User table
INSERT INTO dbo.[User] (Username, Email, PasswordHash, CreateDate)
VALUES 
('TestUser1', 'testuser1@example.com', 'testhash1', GETDATE()),
('TestUser2', 'testuser2@example.com', 'testhash2', GETDATE());
GO
