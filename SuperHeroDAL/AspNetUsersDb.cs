using SuperHero;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace SuperHeroDAL
{
    public class AspNetUsersDb
    {
        SuperHeroDBEntities db = new SuperHeroDBEntities();

        public AspNetUsers GetUserById(string id)
        {
            var user = db.AspNetUsers.Include(u => u.FavouriteSuperHero).Where(u => u.Id == id).FirstOrDefault();
            return user;
        }

        public List<int> GetHeroIdList(AspNetUsers user)
        {
            var userFavSuperHeroesIdList = user.FavouriteSuperHero.Select(h => h.ApiId).ToList();
            return userFavSuperHeroesIdList;
        }

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

        public FavouriteSuperHero GetUserFavouriteHeroById(int apiId, AspNetUsers user)
        {
            var userFavouriteHero = user.FavouriteSuperHero.Where(h => h.ApiId == apiId).FirstOrDefault();
            return userFavouriteHero;
        }

        public void SaveHeroToUserFavHeroList(FavouriteSuperHero heroToSave, AspNetUsers user)
        {
            
            using (DbContextTransaction tran = db.Database.BeginTransaction())
            {
                user.FavouriteSuperHero.Add(heroToSave);
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                tran.Commit();
            }
        }


        public void SaveHeroToDb(FavouriteSuperHero newFavouriteHero, AspNetUsers user)
        {
            using (DbContextTransaction tran = db.Database.BeginTransaction())
            {
                newFavouriteHero.AspNetUsers.Add(user);
                db.FavouriteSuperHero.Add(newFavouriteHero);
                db.SaveChanges();
                tran.Commit();
            }
        }

        public void RemoveHeroFromUserFavouriteList(AspNetUsers user, FavouriteSuperHero userFavouriteHero)
        {
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
