-- Backup the entire database to disk with INIT to overwrite any existing backups
BACKUP DATABASE [YourDatabaseName] TO DISK = 'D:\Backups\YourDatabaseName.bak' WITH INIT;
GO
