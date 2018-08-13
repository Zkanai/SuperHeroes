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
    /// this class manage the requests from detailedHeroBLL
    /// </summary>
    public class DetailedHeroDb:BaseDb
    {

        //public override AspNetUsers GetUserById(string id)
        //{
        //    var user = db.AspNetUsers.Include(u => u.FavouriteSuperHero).Where(u => u.Id == id).FirstOrDefault();
        //    return user;
        //}

        //public List<int> GetUserFavouriteHeroIdList(string userId)
        //{
        //    var user = GetUserById(userId);
        //    var userFavSuperHeroesIdList = user.FavouriteSuperHero.Select(h => h.ApiId).ToList();
        //    return userFavSuperHeroesIdList;
        //}

        public List<int> GetFavouriteHeroIdList()
        {
            var favHeroIdList = db.FavouriteSuperHero.Select(h => h.ApiId).ToList();
            return favHeroIdList;
        }

        public FavouriteSuperHero GetFavouriteHeroById(int heroId)
        {
            var heroFromDb = db.FavouriteSuperHero.Where(h => h.ApiId == heroId).FirstOrDefault();
            return heroFromDb;
        }

        public FavouriteSuperHero GetUserFavouriteHeroById(int apiId, string userId)
        {
            var user = GetUserById(userId);
            var userFavouriteHero = user.FavouriteSuperHero.Where(h => h.ApiId == apiId).FirstOrDefault();
            return userFavouriteHero;
        }

        public void SaveHeroToUserFavHeroList(FavouriteSuperHero heroToSave, string userId)
        {
            var user = GetUserById(userId);

            using (DbContextTransaction tran = db.Database.BeginTransaction())
            {
                user.FavouriteSuperHero.Add(heroToSave);
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                tran.Commit();
            }
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

        public void RemoveHeroFromUserFavouriteList(string userId, FavouriteSuperHero userFavouriteHero)
        {
            var user = GetUserById(userId);
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
