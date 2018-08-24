using SuperHero;
using SuperHero.Models;
using SuperHeroDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHeroBLL.Mapping
{
    internal class FavouriteSuperHeroMapping
    {
        private BattleLogDb objDb;

        public FavouriteSuperHeroMapping()
        {
            objDb = new BattleLogDb();
        }


        internal  List<FavouriteSuperHeroViewModel> MappingFavouriteSuperHeroes(string userId, List<FavouriteSuperHero> userFavSuperHeroList)
        {

            var model = new List<FavouriteSuperHeroViewModel>();

            model = userFavSuperHeroList.Select(hero => new FavouriteSuperHeroViewModel()
            {
                Id = hero.Id,
                ApiId = hero.ApiId.ToString(),
                Name = hero.Name,
                RealName = hero.RealName,
                ImgUrl = hero.ImgUrl,
                Intelligence = hero.Intelligence,
                Strength = hero.Strength,
                Speed = hero.Speed,
                Durability = hero.Durability,
                Power = hero.Power,
                Combat = hero.Combat,
                Win = objDb.GetWinCount(hero, userId),
                Loose =  objDb.GetLooseCount(hero, userId),
                Draw = objDb.GetDrawCount(hero, userId)
            }).ToList();

            return model;
        }
    }
}
