CREATE TABLE [dbo].[BattleLog] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [UserHeroId]     INT            NOT NULL,
    [OpponentHeroId] INT            NOT NULL,
    [WinnerHeroId]   INT            NULL,
    [UserId]         NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_BattleLog] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BattleLog_AspNetUsers] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);







