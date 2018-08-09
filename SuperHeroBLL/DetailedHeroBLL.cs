using SuperHero.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHeroBLL
{
    public class DetailedHeroBLL
    {
        /// <summary>
        /// create a new model, and
        /// set isfavourite property false for help
        /// check if this hero is already favourite for the given user
        /// </summary>
        /// <returns></returns>
        public DetailedHeroViewModel CreateNewDetailedHeroViewModel()
        {
            var model = new DetailedHeroViewModel();
            model.IsFavourite = false;

            return model;
        }

    }
}
