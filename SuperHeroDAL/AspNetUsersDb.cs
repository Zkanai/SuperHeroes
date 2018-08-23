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
    public class AspNetUsersDb:SharedDb
    {
      
        /// <summary>
        /// get's back the actually logged user
        /// based on id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public new AspNetUsers GetUserById(string id)
        {
            var user = db.AspNetUsers.Include(u => u.FavouriteSuperHero).Where(u => u.Id == id).FirstOrDefault();
            return user;
        }

        public void SaveHeroToUserFavHeroList(int apiId, string userId)
        {
            
                var user = GetUserById(userId);
                var heroToSave = GetFavouriteHeroById(apiId);
                
                using (DbContextTransaction tran = db.Database.BeginTransaction())
                {
                    user.FavouriteSuperHero.Add(heroToSave);
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    tran.Commit();
                }
            
        }

        public void RemoveHeroFromUserFavouriteList(string userId, int heroId)
        {            
                var user = GetUserById(userId);
                var userFavouriteHero = GetFavouriteHeroById(heroId);

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
