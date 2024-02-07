-- Script for routine index maintenance

EXEC sp_MSforeachtable 'SET QUOTED_IDENTIFIER ON; ALTER INDEX ALL ON ? REORGANIZE;'
GO

-- Update statistics on all tables
EXEC sp_MSforeachtable 'SET QUOTED_IDENTIFIER ON; UPDATE STATISTICS ? WITH FULLSCAN;'
GO
