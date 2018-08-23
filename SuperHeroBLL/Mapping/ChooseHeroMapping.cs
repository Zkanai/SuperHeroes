using SuperHero;
using SuperHero.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHeroBLL.Mapping
{
    internal static class ChooseHeroMapping
    {
        
        /// <summary>
        /// mapping back the view model,
        /// when something goes wrong
        /// </summary>
        /// <returns></returns>
        internal static ChooseHeroesViewModel Mapping(List<FavouriteSuperHero> favSuperHeroList)
        {
            //mapping back
            var model = new ChooseHeroesViewModel();
                               
            model.UserHeroList = favSuperHeroList;
            model.OpponentHeroList = favSuperHeroList;

            return model;
        }
    }
}
