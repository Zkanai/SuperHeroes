using SuperHero;
using SuperHero.Models;
using SuperHero.Models.JsonFromJqueryModels;
using SuperHeroBLL.Mapping;
using SuperHeroDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHeroBLL
{
    public class ChooseHeroBLL
    {
        private FavouriteSuperHeroDb favHeroObjDb;
        
        public ChooseHeroBLL()
        {
            favHeroObjDb = new FavouriteSuperHeroDb();           
        }
         
        public ChooseHeroesViewModel Mapping(List<FavouriteSuperHero> favSuperHeroList)
        {
            return ChooseHeroMapping.Mapping(favSuperHeroList);
        }

        public UserHeroData MapUserHero(int heroId)
        {         
            return UserHeroMapping.MapUserHero(heroId, favHeroObjDb.GetFavouriteHeroById(heroId));
        }

        public OpponentHeroData MapOpponentHero(int heroId)
        {
            return OpponentHeroMapping.MapOpponentHero(heroId, favHeroObjDb.GetFavouriteHeroById(heroId));
        }

    }
}
