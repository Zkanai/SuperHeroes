using SuperHero;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace SuperHeroDAL
{
    public abstract class BaseDb
    {
        protected SuperHeroDBEntities db;

        public BaseDb()
        {
            db = new SuperHeroDBEntities();
        }

        /// <summary>
        /// get's back the actually logged user
        /// based on id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AspNetUsers GetUserById(string id)
        {
            var user = db.AspNetUsers.Include(u => u.FavouriteSuperHero).Where(u => u.Id == id).FirstOrDefault();
            return user;
        }

        public List<int> GetUserFavouriteHeroIdList(string userId)
        {
            var user = GetUserById(userId);
            var userFavSuperHeroesIdList = user.FavouriteSuperHero.Select(h => h.ApiId).ToList();
            return userFavSuperHeroesIdList;
        }
    }
}
