using SuperHero;
using SuperHero.Models.JsonFromJqueryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHeroBLL.Mapping
{
    internal abstract class UserHeroMapping
    {
        internal static UserHeroData MapUserHero(int heroId, FavouriteSuperHero userHero)
        {
            var chosenUserHero = new UserHeroData();

            chosenUserHero.UserHeroId = heroId;
            chosenUserHero.UserHeroName = userHero.Name;
            chosenUserHero.UserHeroRealName = userHero.RealName;
            chosenUserHero.UserHeroIntelligence = userHero.Intelligence;
            chosenUserHero.UserHeroStrength = userHero.Strength;
            chosenUserHero.UserHeroSpeed = userHero.Speed;
            chosenUserHero.UserHeroDurability = userHero.Durability;
            chosenUserHero.UserHeroPower = userHero.Power;
            chosenUserHero.UserHeroCombat = userHero.Combat;
            chosenUserHero.UserHeroImgUrl = userHero.ImgUrl;

            return chosenUserHero;
        }
    }
}
