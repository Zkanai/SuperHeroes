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
    public static class BattleMapping
    {
        /// <summary>
        /// help modelling the data we get from the api
        /// </summary>
        /// <param name="randomOpponentHero"></param>
        /// <returns></returns>
        public static FavouriteSuperHero Mapping(SuperHeroById.HeroById randomOpponentHero)
        {

            var opponentHero = new FavouriteSuperHero();

            try
            {
                opponentHero.ApiId = Convert.ToInt32(randomOpponentHero.ApiId);
                opponentHero.Name = randomOpponentHero.Name;
                opponentHero.RealName = randomOpponentHero.Biography.Full_Name;
                opponentHero.ImgUrl = randomOpponentHero.Image.Url;
                opponentHero.Intelligence = ApiCall.StatStringToInt(randomOpponentHero.Powerstats.Intelligence);
                opponentHero.Strength = ApiCall.StatStringToInt(randomOpponentHero.Powerstats.Strength);
                opponentHero.Speed = ApiCall.StatStringToInt(randomOpponentHero.Powerstats.Speed);
                opponentHero.Durability = ApiCall.StatStringToInt(randomOpponentHero.Powerstats.Durability);
                opponentHero.Power = ApiCall.StatStringToInt(randomOpponentHero.Powerstats.Power);
                opponentHero.Combat = ApiCall.StatStringToInt(randomOpponentHero.Powerstats.Combat);

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
        public static FavouriteSuperHero MappingWhenApiNotWorking(SuperHeroById.HeroById randomOpponentHero)
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
