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
    }
}
