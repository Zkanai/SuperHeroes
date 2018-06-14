CREATE TABLE [dbo].[FavouriteSuperHero] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [ApiId]        INT            NOT NULL,
    [Name]         NVARCHAR (50)  NOT NULL,
    [RealName]     NVARCHAR (50)  NOT NULL,
    [Intelligence] INT            NOT NULL,
    [Strength]     INT            NOT NULL,
    [Speed]        INT            NOT NULL,
    [Durability]   INT            NOT NULL,
    [Power]        INT            NOT NULL,
    [Combat]       INT            NOT NULL,
    [ImgUrl]       NVARCHAR (200) NULL,
    CONSTRAINT [PK_FavouriteSuperHero_1] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_FavouriteSuperHero]
    ON [dbo].[FavouriteSuperHero]([ApiId] ASC);

