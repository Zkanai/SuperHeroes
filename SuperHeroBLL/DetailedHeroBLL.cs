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
    public class DetailedHeroBLL
    {

        private AspNetUsersDb objDb;

        public DetailedHeroBLL()
        {
            objDb = new AspNetUsersDb();
        }

        public AspNetUsers GetUserById(string id)
        {
            var user = objDb.GetUserById(id);
            return user;
        }

        /// <summary>
        /// get'S back a given user favourite superheroes
        /// id list
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public List<int> GetHeroIdList(AspNetUsers user)
        {
            return objDb.GetHeroIdList(user);
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
        public FavouriteSuperHero GetUserFavouriteHeroById(int apiId, AspNetUsers user)
        {
            return objDb.GetUserFavouriteHeroById(apiId, user);
        }

        /// <summary>
        /// save hero to a given user's favourite hero list 
        /// </summary>
        /// <param name="apiId"></param>
        /// <param name="user"></param>
        public void SaveHeroToUserFavHeroList(FavouriteSuperHero heroToSave, AspNetUsers user)
        {
            objDb.SaveHeroToUserFavHeroList(heroToSave, user);
        }

        /// <summary>
        /// save a new favourite hero to db
        /// (means add to the given user favourite hero list, 
        /// and also add to the favouritehero table)
        /// </summary>
        /// <param name="newFavouriteHero"></param>
        /// <param name="user"></param>
        public void SaveHeroToDb(FavouriteSuperHero newFavouriteHero, AspNetUsers user)
        {
            objDb.SaveHeroToDb(newFavouriteHero, user);
        }

        /// <summary>
        /// Remove the given hero from the 
        /// given user's favourite superhero list
        /// </summary>
        /// <param name="user"></param>
        /// <param name="userFavouriteHero"></param>
        public void RemoveHeroFromUserFavouriteList(AspNetUsers user, FavouriteSuperHero userFavouriteHero)
        {
            objDb.RemoveHeroFromUserFavouriteList(user, userFavouriteHero);
        }
    }
}
