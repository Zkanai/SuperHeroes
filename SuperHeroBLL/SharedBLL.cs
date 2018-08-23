using SuperHero;
using SuperHeroDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHeroBLL
{
    /// <summary>
    /// contains the shared business logic methods
    /// </summary>
    public abstract class SharedBLL
    {
        private AspNetUsersDb aspNetUsersDb;
        private FavouriteSuperHeroDb favouriteSuperHeroDb;

        public SharedBLL()
        {
            aspNetUsersDb = new AspNetUsersDb();
            favouriteSuperHeroDb = new FavouriteSuperHeroDb();
        }

        /// <summary>
        /// get back a given user's
        /// favourite superheroes
        /// id list
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public List<int> GetUserFavouriteHeroIdList(string userId)
        {
            return favouriteSuperHeroDb.GetUserFavouriteHeroIdList(userId);
        }

        /// <summary>
        /// returns all favourite hero id from the
        /// db in a int list
        /// </summary>
        /// <returns></returns>
        public List<int> GetFavouriteHeroIdList()
        {
            return favouriteSuperHeroDb.GetFavouriteHeroIdList();
        }

        /// <summary>
        /// get's back a favourite hero
        /// based on the hero id
        /// </summary>
        /// <param name="heroId"></param>
        /// <returns></returns>
        public FavouriteSuperHero GetFavouriteHeroById(int heroId)
        {
            return favouriteSuperHeroDb.GetFavouriteHeroById(heroId);
        }

        /// <summary>
        /// get's back the given user's favourite superhero based
        /// on its id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userHeroApiId"></param>
        /// <returns></returns>
        public FavouriteSuperHero GetUserFavouriteHeroById(string userId, int heroApiId)
        {
            return favouriteSuperHeroDb.GetUserFavouriteHeroById(heroApiId, userId);
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
            return favouriteSuperHeroDb.GetUserFavouriteHeroById(apiId, userId);
        }
    }
}
