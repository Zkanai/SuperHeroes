using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperHero.Models.JsonFromJqueryModels
{
    /// <summary>
    /// this class helps to manage the datas from AJAX 
    /// </summary>
    public class Combat
    {
        public int LeftHeroId { get; set; }
        public int RightHeroId { get; set; }
        public string UserHeroName { get; set; }
        public string OpponentHeroName { get; set; }
        public int UserHeroSkill { get; set; }
        public int UserHeroStat { get; set; }
        public int UserHeroHp { get; set; }
        public int UserHeroDmg { get; set; }
        public int OpponentHeroSkill { get; set; }
        public int OpponentHeroStat { get; set; }
        public int OpponentHeroHp { get; set; }
        public int OpponentHeroDmg { get; set; }

    }
}