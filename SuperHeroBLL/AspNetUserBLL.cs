using SuperHero;
using SuperHeroDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHeroBLL
{
    public class AspNetUserBLL
    {
        private AspNetUsersDb objDb;

        public AspNetUserBLL()
        {
            objDb = new AspNetUsersDb();
        }

        /// <summary>
        /// get's back the actually logged user
        /// based on id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AspNetUsers GetUserIncludeFavouriteHeroesById(string userId)
        {
            return objDb.GetUserIncludeFavouriteHeroesById(userId);
        }
    }
}
