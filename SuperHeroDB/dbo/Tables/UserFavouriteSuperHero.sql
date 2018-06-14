CREATE TABLE [dbo].[UserFavouriteSuperHero] (
    [UserId]               INT NOT NULL,
    [FavouriteSuperHeroId] INT NOT NULL,
    CONSTRAINT [PK_UserFavouriteSuperHero] PRIMARY KEY CLUSTERED ([UserId] ASC, [FavouriteSuperHeroId] ASC),
    CONSTRAINT [FK_UserFavouriteSuperHero_FavouriteSuperHero] FOREIGN KEY ([FavouriteSuperHeroId]) REFERENCES [dbo].[FavouriteSuperHero] ([Id]),
    CONSTRAINT [FK_UserFavouriteSuperHero_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id])
);

