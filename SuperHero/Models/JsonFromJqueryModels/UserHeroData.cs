using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperHero.Models.JsonFromJqueryModels
{
    /// <summary>
    /// for manage the jason object that get from jquery
    /// </summary>
    public class UserHeroData
    {
        public int? UserHeroId { get; set; }
        public string UserHeroName { get; set; }
        public string UserHeroRealName { get; set; }
        public int UserHeroIntelligence { get; set; }
        public int UserHeroStrength { get; set; }
        public int UserHeroSpeed { get; set; }
        public int UserHeroDurability { get; set; }
        public int UserHeroPower { get; set; }
        public int UserHeroCombat { get; set; }
        public string UserHeroImgUrl { get; set; }

    }
}