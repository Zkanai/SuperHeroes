using SuperHero;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace SuperHeroDAL
{
    /// <summary>   
    /// manage CRUD for BattleLog table from db
    /// </summary>
    /// </summary>
    public class FavouriteSuperHeroDb
    {

        public FavouriteSuperHero GetUserFavouriteHeroById(int apiId, string userId)
        {
            using (SuperHeroDBEntities db = new SuperHeroDBEntities())
            {
                var user = AspNetUsersDb.GetUserById(userId, db);
                return user.FavouriteSuperHero.Where(h => h.ApiId == apiId).FirstOrDefault();
            }
        }

        public List<int> GetUserFavouriteHeroIdList(string userId)
        {
            using (SuperHeroDBEntities db = new SuperHeroDBEntities())
            {
                var user = AspNetUsersDb.GetUserById(userId, db);
                return user.FavouriteSuperHero.Select(h => h.ApiId).ToList();
            }
        }

        public List<int> GetFavouriteHeroIdList()
        {
            using (SuperHeroDBEntities db = new SuperHeroDBEntities())
            {
                return db.FavouriteSuperHero.Select(h => h.ApiId).ToList();
            }
        }


        public FavouriteSuperHero GetFavouriteHeroById(int heroId)
        {
            using (SuperHeroDBEntities db = new SuperHeroDBEntities())
            {
                return db.FavouriteSuperHero.Where(h => h.ApiId == heroId).FirstOrDefault();
            }
        }

        public static FavouriteSuperHero GetFavouriteHeroById(int heroId, SuperHeroDBEntities db)
        {           
            return db.FavouriteSuperHero.Where(h => h.ApiId == heroId).FirstOrDefault();
        }

        public void SaveHeroToDb(FavouriteSuperHero newFavouriteHero, string userId)
        {
            using (SuperHeroDBEntities db = new SuperHeroDBEntities())
            {
                var user = AspNetUsersDb.GetUserById(userId, db);

                using (DbContextTransaction tran = db.Database.BeginTransaction())
                {
                    newFavouriteHero.AspNetUsers.Add(user);
                    db.FavouriteSuperHero.Add(newFavouriteHero);
                    db.SaveChanges();
                    tran.Commit();
                }
            }
        }

        public List<FavouriteSuperHero> GetUserFavouriteHeroList(string userId)
        {
            using (SuperHeroDBEntities db = new SuperHeroDBEntities())
            {
                var user = AspNetUsersDb.GetUserById(userId, db);
                return user.FavouriteSuperHero.ToList();
            }
        }
    }
}
