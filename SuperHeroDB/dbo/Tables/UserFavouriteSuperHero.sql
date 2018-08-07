CREATE TABLE [dbo].[UserFavouriteSuperHero] (
    [UserId]               NVARCHAR (128) NOT NULL,
    [FavouriteSuperHeroId] INT            NOT NULL,
    CONSTRAINT [PK_UserFavouriteSuperHero_1] PRIMARY KEY CLUSTERED ([UserId] ASC, [FavouriteSuperHeroId] ASC),
    CONSTRAINT [FK_UserFavouriteSuperHero_AspNetUsers] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_UserFavouriteSuperHero_FavouriteSuperHero] FOREIGN KEY ([FavouriteSuperHeroId]) REFERENCES [dbo].[FavouriteSuperHero] ([Id])
);



