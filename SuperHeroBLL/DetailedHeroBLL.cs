using SuperHero;
using SuperHero.Models;
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
    public class DetailedHeroBLL:BaseBLL
    {
      
        private FavouriteSuperHeroDb favHeroObjDb;
        private AspNetUsersDb userObjDb;

        public DetailedHeroBLL()
        {
            favHeroObjDb = new FavouriteSuperHeroDb();
            userObjDb = new AspNetUsersDb();
        }

        /// <summary>
        /// save hero to a given user's favourite hero list 
        /// </summary>
        /// <param name="apiId"></param>
        /// <param name="user"></param>
        public void SaveHeroToUserFavHeroList(int apiId, string userId)
        {
            userObjDb.SaveHeroToUserFavHeroList(apiId, userId);
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
            favHeroObjDb.SaveHeroToDb(newFavouriteHero, userId);
        }

        /// <summary>
        /// Remove the given hero from the 
        /// given user's favourite superhero list
        /// </summary>
        /// <param name="user"></param>
        /// <param name="userFavouriteHero"></param>
        public void RemoveHeroFromUserFavouriteList(string userId, int heroId)
        {
            userObjDb.RemoveHeroFromUserFavouriteList(userId, heroId);
        }
    }
}
