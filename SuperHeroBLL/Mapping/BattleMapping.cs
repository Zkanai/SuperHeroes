using SuperHero;
using SuperHero.ManageApi;
using SuperHero.Models.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHeroBLL.Mapping
{
    internal abstract class BattleMapping
    {
        /// <summary>
        /// help modelling the data we get from the api
        /// </summary>
        /// <param name="randomOpponentHero"></param>
        /// <returns></returns>
        internal static FavouriteSuperHero Mapping(SuperHeroById.HeroById randomOpponentHero)
        {

            var opponentHero = new FavouriteSuperHero();

            try
            {
                opponentHero.ApiId = Convert.ToInt32(randomOpponentHero.ApiId);
                opponentHero.Name = randomOpponentHero.Name;
                opponentHero.RealName = randomOpponentHero.Biography.Full_Name;
                opponentHero.ImgUrl = randomOpponentHero.Image.Url;
                opponentHero.Intelligence = MappingExtensions.StatStringToInt(randomOpponentHero.Powerstats.Intelligence);
                opponentHero.Strength = MappingExtensions.StatStringToInt(randomOpponentHero.Powerstats.Strength);
                opponentHero.Speed = MappingExtensions.StatStringToInt(randomOpponentHero.Powerstats.Speed);
                opponentHero.Durability = MappingExtensions.StatStringToInt(randomOpponentHero.Powerstats.Durability);
                opponentHero.Power = MappingExtensions.StatStringToInt(randomOpponentHero.Powerstats.Power);
                opponentHero.Combat = MappingExtensions.StatStringToInt(randomOpponentHero.Powerstats.Combat);

                return opponentHero;
            }
            catch (Exception)
            {

                throw;
            }


        }

        /// <summary>
        /// modelling a random enemy hero when api
        /// not working
        /// </summary>
        /// <param name="randomOpponentHero"></param>
        /// <returns></returns>
        internal static FavouriteSuperHero MappingWhenApiNotWorking(SuperHeroById.HeroById randomOpponentHero)
        {

            var opponentHero = new FavouriteSuperHero();

            try
            {
                opponentHero.ApiId = 0;
                opponentHero.Name = "Temporarily Unavailable!";
                opponentHero.RealName = "Temporarily Unavailable!";
                opponentHero.ImgUrl = "../../../img/pictureNA.jpg";
                opponentHero.Intelligence = 0;
                opponentHero.Strength = 0;
                opponentHero.Speed = 0;
                opponentHero.Durability = 0;
                opponentHero.Power = 0;
                opponentHero.Combat = 0;

                return opponentHero;
            }
            catch (Exception)
            {

                throw;
            }


        }
    }
}
