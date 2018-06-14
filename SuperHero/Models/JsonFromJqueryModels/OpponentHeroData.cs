using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperHero.Models.JsonFromJqueryModels
{
    /// <summary>
    /// for manage the jason object that get from jquery
    /// </summary>
    public class OpponentHeroData
    {
        public int? OpponentHeroId { get; set; }
        public string OpponentHeroName { get; set; }
        public string OpponentHeroRealName { get; set; }
        public string OpponentHeroImgUrl { get; set; }
    }
}