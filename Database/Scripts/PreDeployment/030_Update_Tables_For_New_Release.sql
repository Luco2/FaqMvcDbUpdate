-- Script to add new columns to the 'User' table for a new release

ALTER TABLE [dbo].[User]
ADD 
    ProfilePictureUrl NVARCHAR(MAX) NULL, -- Add a new column for profile picture URLs
    IsActive BIT NOT NULL DEFAULT 1 -- Add a new column to indicate if the user is active
GO
