using SuperHero;
using SuperHero.Models;
using SuperHeroBLL.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHeroBLL
{
    public class HomeBLL
    {
        private FavouriteSuperHeroMapping objMap;

        public HomeBLL()
        {
            objMap = new FavouriteSuperHeroMapping();
        }

        public List<FavouriteSuperHeroViewModel> MappingFavouriteSuperHeroes(string userId,List<FavouriteSuperHero> userFavSuperHeroList)
        {
            return objMap.MappingFavouriteSuperHeroes(userId, userFavSuperHeroList);
        }
    }
}
