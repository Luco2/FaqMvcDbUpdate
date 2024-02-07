CREATE TABLE [dbo].[UserPrompts] (
    [UserPromptId] BIGINT         IDENTITY (1, 1) NOT NULL,
    [Question]     NVARCHAR (MAX) NOT NULL,
    [Answer]       NVARCHAR (MAX) NOT NULL,
    [UserId]       NVARCHAR (450) NOT NULL,
    [DateAsked]    DATETIME2 (7)  NOT NULL,
    CONSTRAINT [PK_UserPrompts] PRIMARY KEY CLUSTERED ([UserPromptId] ASC),
    CONSTRAINT [FK_UserPrompts_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_UserPrompts_UserId]
    ON [dbo].[UserPrompts]([UserId] ASC);

