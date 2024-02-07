BEGIN TRANSACTION;

BEGIN TRY
    -- Add a column with a default value
    ALTER TABLE dbo.[User] ADD NewColumn NVARCHAR(255) NULL;

    -- Update the column based on some condition or calculation
    -- Replace 'SomeCondition = True' with your actual condition
    UPDATE dbo.[User] SET NewColumn = 'DefaultValue' WHERE SomeCondition = True;

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
    BEGIN
        ROLLBACK TRANSACTION;
    END

    DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
    RAISERROR (@ErrorMessage, 16, 1);
END CATCH

GO
