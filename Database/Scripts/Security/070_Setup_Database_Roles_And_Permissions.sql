-- Create a new database role
CREATE ROLE db_application_role;
GO

-- Grant SELECT permissions on all tables to the role
GRANT SELECT ON SCHEMA::dbo TO db_application_role;
GO

-- Create a user and add to the role
CREATE USER app_user FOR LOGIN app_login;
ALTER ROLE db_application_role ADD MEMBER app_user;
GO
