CREATE TABLE [dbo].[Faqs] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [Question]  NVARCHAR (MAX) NOT NULL,
    [Answer]    NVARCHAR (MAX) NOT NULL,
    [DateAsked] DATETIME2 (7)  NOT NULL,
    CONSTRAINT [PK_Faqs] PRIMARY KEY CLUSTERED ([Id] ASC)
);

