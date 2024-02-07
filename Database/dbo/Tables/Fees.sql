CREATE TABLE [dbo].[Fees] (
    [FeeInfoId]         INT             IDENTITY (1, 1) NOT NULL,
    [ServiceName]       NVARCHAR (MAX)  NOT NULL,
    [ProvisionInAct]    NVARCHAR (MAX)  NULL,
    [Fee]               DECIMAL (18, 2) NOT NULL,
    [LatePenalty]       DECIMAL (18, 2) NULL,
    [AdditionalDetails] NVARCHAR (MAX)  NOT NULL,
    CONSTRAINT [PK_Fees] PRIMARY KEY CLUSTERED ([FeeInfoId] ASC)
);

