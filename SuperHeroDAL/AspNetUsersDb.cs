using SuperHero;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHeroDAL
{
    /// <summary>
    /// manage CRUD for BattleLog table from db
    /// </summary>
    public class AspNetUsersDb
    {

        /// <summary>
        /// get's back the actually logged user
        /// based on id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static AspNetUsers GetUserById(string id, SuperHeroDBEntities db)
        {
            var user = db.AspNetUsers.Include(u => u.FavouriteSuperHero).Where(u => u.Id == id).FirstOrDefault();
            return user;
        }

        public void SaveHeroToUserFavHeroList(int apiId, string userId)
        {
            using (SuperHeroDBEntities db = new SuperHeroDBEntities())
            {
                var user = GetUserById(userId, db);
                var heroToSave = db.FavouriteSuperHero.Where(h => h.ApiId == apiId).FirstOrDefault();

                using (DbContextTransaction tran = db.Database.BeginTransaction())
                {
                    user.FavouriteSuperHero.Add(heroToSave);
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    tran.Commit();
                }
            }
        }

        public void RemoveHeroFromUserFavouriteList(string userId, int heroId)
        {
            using (SuperHeroDBEntities db = new SuperHeroDBEntities())
            {
                var user = GetUserById(userId, db);
                var userFavouriteHero = FavouriteSuperHeroDb.GetFavouriteHeroById(heroId, db);

                using (DbContextTransaction tran = db.Database.BeginTransaction())
                {
                    user.FavouriteSuperHero.Remove(userFavouriteHero);
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    tran.Commit();
                }
            }
        }
    }
}
