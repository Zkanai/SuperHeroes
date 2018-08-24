using SuperHero;
using SuperHero.ManageApi;
using SuperHero.Models;
using SuperHero.Models.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHeroBLL.Mapping
{
    /// <summary>
    /// manage mapping for DetailedHeroViewModel
    /// </summary>
   internal static class DetailedHeroMapping
    {

        /// <summary>
        /// mappging the data from our api model
        /// </summary>
        /// <param name="model"></param>
        /// <param name="hero"></param>
        /// <returns></returns>
        internal static DetailedHeroViewModel MappingFromApi(DetailedHeroViewModel model, SuperHeroById.HeroById hero)
        {
            try
            {
                model.ApiId = hero.ApiId;
                model.Name = hero.Name;
                model.ImageUrl = hero.Image.Url;
                model.BiographyData.Full_Name = hero.Biography.Full_Name;
                model.BiographyData.Alignment = hero.Biography.Alignment;
                model.BiographyData.Place_Of_Birth = hero.Biography.Place_Of_Birth;
                model.BiographyData.Publisher = hero.Biography.Publisher;
                model.AppearanceValues.Gender = hero.Appearance.Gender;
                model.AppearanceValues.Race = hero.Appearance.Race;
                model.Powerstat.Intelligence = hero.Powerstats.Intelligence;
                model.Powerstat.Strength = hero.Powerstats.Strength;
                model.Powerstat.Speed = hero.Powerstats.Speed;
                model.Powerstat.Durability = hero.Powerstats.Durability;
                model.Powerstat.Power = hero.Powerstats.Power;
                model.Powerstat.Combat = hero.Powerstats.Combat;

                return model;
            }
            catch (Exception)
            {

                throw;
            }


        }

        /// <summary>
        /// mapping the data from our db
        /// or if the hero not in our db then mapping
        /// a not available hero
        /// </summary>
        /// <param name="model"></param>
        /// <param name="hero"></param>
        /// <returns></returns>
        internal static DetailedHeroViewModel MappingWhenApiNA(DetailedHeroViewModel model, FavouriteSuperHero hero)
        {
            var infoWhenApiNA = "Temporarily Unavailable!";

            try
            {
                //if the hero doesn't exist in our db yet
                if (hero == null)
                {
                    model.Name = infoWhenApiNA;
                    model.ImageUrl = "../../../img/pictureNA.jpg";
                    model.BiographyData.Full_Name = infoWhenApiNA;
                    model.BiographyData.Alignment = infoWhenApiNA;
                    model.BiographyData.Place_Of_Birth = infoWhenApiNA;
                    model.BiographyData.Publisher = infoWhenApiNA;
                    model.AppearanceValues.Gender = infoWhenApiNA;
                    model.AppearanceValues.Race = infoWhenApiNA;
                    model.Powerstat.Intelligence = infoWhenApiNA;
                    model.Powerstat.Strength = infoWhenApiNA;
                    model.Powerstat.Speed = infoWhenApiNA;
                    model.Powerstat.Durability = infoWhenApiNA;
                    model.Powerstat.Power = infoWhenApiNA;
                    model.Powerstat.Combat = infoWhenApiNA;

                    return model;
                }

                //if the hero is in our db already
                model.ApiId = hero.ApiId.ToString();
                model.Name = hero.Name;
                model.ImageUrl = hero.ImgUrl;
                model.BiographyData.Full_Name = hero.RealName;
                model.BiographyData.Alignment = infoWhenApiNA;
                model.BiographyData.Place_Of_Birth = infoWhenApiNA;
                model.BiographyData.Publisher = infoWhenApiNA;
                model.AppearanceValues.Gender = infoWhenApiNA;
                model.AppearanceValues.Race = infoWhenApiNA;
                model.Powerstat.Intelligence = hero.Intelligence.ToString();
                model.Powerstat.Strength = hero.Strength.ToString();
                model.Powerstat.Speed = hero.Speed.ToString();
                model.Powerstat.Durability = hero.Durability.ToString();
                model.Powerstat.Power = hero.Power.ToString();
                model.Powerstat.Combat = hero.Combat.ToString();

                return model;
            }
            catch (Exception)
            {

                throw;
            }


        }

        /// <summary>
        /// mapping a hero that we get from api,
        /// to a new favourite superhero, because the hero 
        /// insn't in our db
        /// </summary>
        /// <param name="hero"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        internal static FavouriteSuperHero MappingNewFavouriteHero(SuperHeroById.HeroById hero)
        {
            var newFavouriteHero = new FavouriteSuperHero();

            //mapping for our db from api
            newFavouriteHero.ApiId = Convert.ToInt32(hero.ApiId);
            newFavouriteHero.Name = hero.Name;
            newFavouriteHero.RealName = hero.Biography.Full_Name;
            newFavouriteHero.ImgUrl = hero.Image.Url;
            newFavouriteHero.Intelligence = MappingExtensions.StatStringToInt(hero.Powerstats.Intelligence);
            newFavouriteHero.Strength = MappingExtensions.StatStringToInt(hero.Powerstats.Strength);
            newFavouriteHero.Speed = MappingExtensions.StatStringToInt(hero.Powerstats.Speed);
            newFavouriteHero.Durability = MappingExtensions.StatStringToInt(hero.Powerstats.Durability);
            newFavouriteHero.Power = MappingExtensions.StatStringToInt(hero.Powerstats.Power);
            newFavouriteHero.Combat = MappingExtensions.StatStringToInt(hero.Powerstats.Combat);

            return newFavouriteHero;
        }
    }
}
