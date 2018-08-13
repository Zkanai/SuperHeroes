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
    /// this class manage the requests from DetailedHeroViewController
    /// </summary>
    public class DetailedHeroBLL
    {

        private DetailedHeroDb objDb;

        public DetailedHeroBLL()
        {
            objDb = new DetailedHeroDb();
        }

        ///// <summary>
        ///// gets the user from the db
        ///// based on her id
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public AspNetUsers GetUserById(string id)
        //{
        //    var user = objDb.GetUserById(id);
        //    return user;
        //}

        /// <summary>
        /// get'S back a given user favourite superheroes
        /// id list
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public List<int> GetUserFavouriteHeroIdList(string userId)
        {
            return objDb.GetUserFavouriteHeroIdList(userId);
        }

        /// <summary>
        /// returns all favourite hero id from the
        /// db in a int list
        /// </summary>
        /// <returns></returns>
        public List<int> GetFavouriteHeroIdList()
        {            
            return objDb.GetFavouriteHeroIdList();
        }

        /// <summary>
        /// get's back a favourite hero
        /// based on the hero id
        /// </summary>
        /// <param name="heroId"></param>
        /// <returns></returns>
        public FavouriteSuperHero GetFavouriteHeroById(int heroId)
        {           
            return objDb.GetFavouriteHeroById(heroId);
        }

        /// <summary>
        /// get's back the given user's favourite superhero,
        /// based on the hero id
        /// </summary>
        /// <param name="apiId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public FavouriteSuperHero GetUserFavouriteHeroById(int apiId, string userId)
        {
            return objDb.GetUserFavouriteHeroById(apiId, userId);
        }

        /// <summary>
        /// save hero to a given user's favourite hero list 
        /// </summary>
        /// <param name="apiId"></param>
        /// <param name="user"></param>
        public void SaveHeroToUserFavHeroList(FavouriteSuperHero heroToSave, string userId)
        {
            objDb.SaveHeroToUserFavHeroList(heroToSave, userId);
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
            objDb.SaveHeroToDb(newFavouriteHero, userId);
        }

        /// <summary>
        /// Remove the given hero from the 
        /// given user's favourite superhero list
        /// </summary>
        /// <param name="user"></param>
        /// <param name="userFavouriteHero"></param>
        public void RemoveHeroFromUserFavouriteList(string userId, FavouriteSuperHero userFavouriteHero)
        {
            objDb.RemoveHeroFromUserFavouriteList(userId, userFavouriteHero);
        }
    }
}
