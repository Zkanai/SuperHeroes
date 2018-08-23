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
    public class FavouriteSuperHeroDb:SharedDb
    {

        public FavouriteSuperHero GetUserFavouriteHeroById(int apiId, string userId)
        {
            var user = GetUserById(userId);        
            return user.FavouriteSuperHero.Where(h => h.ApiId == apiId).FirstOrDefault();
        }

        public List<int> GetUserFavouriteHeroIdList(string userId)
        {
            var user = GetUserById(userId);            
            return user.FavouriteSuperHero.Select(h => h.ApiId).ToList();
        }

        public List<int> GetFavouriteHeroIdList()
        {            
            return db.FavouriteSuperHero.Select(h => h.ApiId).ToList();
        }

        public new FavouriteSuperHero GetFavouriteHeroById(int heroId)
        {           
            return db.FavouriteSuperHero.Where(h => h.ApiId == heroId).FirstOrDefault(); ;
        }

        public void SaveHeroToDb(FavouriteSuperHero newFavouriteHero, string userId)
        {          
                var user = GetUserById(userId);

                using (DbContextTransaction tran = db.Database.BeginTransaction())
                {
                    newFavouriteHero.AspNetUsers.Add(user);
                    db.FavouriteSuperHero.Add(newFavouriteHero);
                    db.SaveChanges();
                    tran.Commit();
                }           
        }

        public List<FavouriteSuperHero> GetUserFavouriteHeroList(string userId)
        {
            var user = GetUserById(userId);
            return user.FavouriteSuperHero.ToList();            
        }
    }
}
