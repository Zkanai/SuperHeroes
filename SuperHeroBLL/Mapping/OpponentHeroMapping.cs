using SuperHero;
using SuperHero.Models.JsonFromJqueryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHeroBLL.Mapping
{
    internal abstract class OpponentHeroMapping
    {
        internal static OpponentHeroData MapOpponentHero(int heroId, FavouriteSuperHero opponentHero)
        {
            var chosenOpponentHero = new OpponentHeroData();

            chosenOpponentHero.OpponentHeroId = heroId;
            chosenOpponentHero.OpponentHeroName = opponentHero.Name;
            chosenOpponentHero.OpponentHeroRealName = opponentHero.RealName;
            chosenOpponentHero.OpponentHeroImgUrl = opponentHero.ImgUrl;

            return chosenOpponentHero;
        }
    }
}
