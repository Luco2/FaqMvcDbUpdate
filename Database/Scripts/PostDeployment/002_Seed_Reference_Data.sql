-- Script to seed reference data into the 'Role' lookup table

INSERT INTO [dbo].[AspNetRoles] ([Name], [Description])
VALUES 
('Admin', 'Administrator role with full permissions'),
('User', 'Standard user role with limited permissions'),
('Guest', 'Guest user role with minimal permissions')
GO
