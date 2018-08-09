using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperHero.Models
{
    public class BattleViewModel
    {
        public BattleViewModel()
        {
            UserHero = new FavouriteSuperHero();
            OpponentHero = new FavouriteSuperHero();
        }
       
        public FavouriteSuperHero UserHero { get; set; }
        public FavouriteSuperHero OpponentHero { get; set; }
       
       
    }
}