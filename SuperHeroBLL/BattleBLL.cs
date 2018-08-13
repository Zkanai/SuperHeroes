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
    /// this class manage the requests from
    /// battle controller
    /// </summary>
    public class BattleBLL
    {

        private BattleDb objDb;

        public BattleBLL()
        {
            objDb = new BattleDb();
        }

        ///// <summary>
        ///// gets the user from the db
        ///// based on her id
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public AspNetUsers GetUserById(string id)
        //{
        //    return objDb.GetUserById(id);
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
        /// get's back the given user's hero based
        /// on its id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userHeroApiId"></param>
        /// <returns></returns>
        public FavouriteSuperHero GetUserHero(string userId, int heroApiId)
        {
            return objDb.GetUserHero(userId, heroApiId);
        }

    }
}
