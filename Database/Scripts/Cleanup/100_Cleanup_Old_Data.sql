-- Delete records from a log table older than a year
DELETE FROM dbo.LogTable
WHERE LogDate < DATEADD(year, -1, GETDATE());
GO
