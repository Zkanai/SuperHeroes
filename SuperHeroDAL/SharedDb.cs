using SuperHero;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SuperHeroDAL
{

    public class SharedDb
    {
        protected SuperHeroDBEntities db;
        private AspNetUsersDb userObjDb;
        private FavouriteSuperHeroDb heroObjDb;

        public SharedDb()
        {
            db = new SuperHeroDBEntities();
            userObjDb = new AspNetUsersDb();
            heroObjDb = new FavouriteSuperHeroDb();
        }

        protected AspNetUsers GetUserById(string id)
        {
            return userObjDb.GetUserById(id);
        }

        protected FavouriteSuperHero GetFavouriteHeroById(int heroId)
        {
            return heroObjDb.GetFavouriteHeroById(heroId);
        }

    }
}
