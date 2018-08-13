using SuperHero;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace SuperHeroDAL
{
    public class BattleDb:BaseDb
    {


        //public List<int> GetUserFavouriteHeroIdList(string userId)
        //{
        //    var user = GetUserById(userId);
        //    var userFavSuperHeroesIdList = user.FavouriteSuperHero.Select(h => h.ApiId).ToList();
        //    return userFavSuperHeroesIdList;
        //}

        public FavouriteSuperHero GetUserHero(string userId, int heroId)
        {
            var user = GetUserById(userId);
            var userHero = user.FavouriteSuperHero.Where(hero => hero.ApiId == heroId).FirstOrDefault();
            return userHero;
        }
    }
}
