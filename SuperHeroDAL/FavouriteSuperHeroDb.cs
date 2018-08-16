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
    public class FavouriteSuperHeroDb:BaseDb
    {
        

       

        

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
    }
}
