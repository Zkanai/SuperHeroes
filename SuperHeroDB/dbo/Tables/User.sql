CREATE TABLE [dbo].[User] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (50)  NOT NULL,
    [UserName] NVARCHAR (30)  NOT NULL,
    [Email]    NVARCHAR (50)  NOT NULL,
    [PassWord] NVARCHAR (100) NOT NULL,
    [Salt]     NVARCHAR (50)  NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC)
);



