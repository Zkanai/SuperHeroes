using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SuperHero.Models
{
    public class ChooseHeroesViewModel
    {

        [Display(Name ="YOUR HERO")]
        public List<FavouriteSuperHero> UserHeroList { get; set; }
        [Display(Name = "OPPONENT HERO")]
        public List<FavouriteSuperHero> OpponentHeroList { get; set; }
        [Display(Name = "YOUR HERO")]
        public List<int> UserHeroApiIdList { get; set; }
        [Display(Name = "OPPONENT HEROES")]
        public List<int> OpponentHeroApiIdList { get; set; }
    }
}