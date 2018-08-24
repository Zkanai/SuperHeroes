using SuperHero;
using SuperHero.Models;
using SuperHero.Models.ApiModels;
using SuperHeroBLL.Mapping;
using SuperHeroDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHeroBLL
{
    /// <summary>
    /// manage the requests from DetailedHeroViewController
    /// </summary>
    public class DetailedHeroBLL
    {
        private AspNetUsersDb aspNetUsersDb;
        private FavouriteSuperHeroDb favouriteSuperHeroDb;

        public DetailedHeroBLL()
        {
            aspNetUsersDb = new AspNetUsersDb();
            favouriteSuperHeroDb = new FavouriteSuperHeroDb();          
        }

        /// <summary>
        /// save hero to a given user's favourite hero list 
        /// </summary>
        /// <param name="apiId"></param>
        /// <param name="user"></param>
        public void SaveHeroToUserFavHeroList(int apiId, string userId)
        {
            aspNetUsersDb.SaveHeroToUserFavHeroList(apiId, userId);
        }

        /// <summary>
        /// save a new favourite hero to db
        /// (means add to the given user favourite hero list, 
        /// and also add to the favouritehero table)
        /// </summary>
        /// <param name="newFavouriteHero"></param>
        /// <param name="user"></param>
        public void SaveHeroToDb(FavouriteSuperHero newFavouriteHero, string userId)
        {
            favouriteSuperHeroDb.SaveHeroToDb(newFavouriteHero, userId);
        }

        /// <summary>
        /// Remove the given hero from the 
        /// given user's favourite superhero list
        /// </summary>
        /// <param name="user"></param>
        /// <param name="userFavouriteHero"></param>
        public void RemoveHeroFromUserFavouriteList(string userId, int heroId)
        {
            aspNetUsersDb.RemoveHeroFromUserFavouriteList(userId, heroId);
        }

        /// <summary>
        /// mappging the data from our api model
        /// </summary>
        /// <param name="model"></param>
        /// <param name="hero"></param>
        /// <returns></returns>
        public DetailedHeroViewModel MappingFromApi(DetailedHeroViewModel model, SuperHeroById.HeroById hero)
        {
            return DetailedHeroMapping.MappingFromApi(model, hero);
        }

        /// <summary>
        /// mapping the data from our db
        /// or if the hero not in our db then mapping
        /// a not available hero
        /// </summary>
        /// <param name="model"></param>
        /// <param name="hero"></param>
        /// <returns></returns>
        public DetailedHeroViewModel MappingWhenApiNA(DetailedHeroViewModel model, FavouriteSuperHero hero)
        {
            return DetailedHeroMapping.MappingWhenApiNA(model, hero);
        }

        /// <summary>
        /// mapping a hero that we get from api,
        /// to a new favourite superhero, because the hero 
        /// insn't in our db
        /// </summary>
        /// <param name="hero"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public FavouriteSuperHero MappingNewFavouriteHero(SuperHeroById.HeroById hero)
        {
            return DetailedHeroMapping.MappingNewFavouriteHero(hero);
        }
    }
}
