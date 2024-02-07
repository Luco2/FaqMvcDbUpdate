-- Create an audit log table
CREATE TABLE dbo.AuditLog
(
    AuditLogID INT IDENTITY(1,1) PRIMARY KEY,
    TableName NVARCHAR(128),
    OperationType NVARCHAR(10),
    OldValue NVARCHAR(MAX),
    NewValue NVARCHAR(MAX),
    UpdatedBy NVARCHAR(256),
    UpdateDate DATETIME
);
GO

-- Example trigger to log UPDATE operations
CREATE TRIGGER trgAfterUpdate ON dbo.[User]
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.AuditLog (TableName, OperationType, OldValue, NewValue, UpdatedBy, UpdateDate)
    SELECT 
        'User', 
        'UPDATE', 
        CONVERT(NVARCHAR(MAX), DELETED.YourColumn), 
        CONVERT(NVARCHAR(MAX), INSERTED.YourColumn), 
        SYSTEM_USER, 
        GETDATE()
    FROM INSERTED
    INNER JOIN DELETED ON INSERTED.UserId = DELETED.UserId;
END;
GO
