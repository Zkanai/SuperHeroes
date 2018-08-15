using SuperHero;
using SuperHero.Models;
using SuperHero.Models.JsonFromJqueryModels;
using SuperHeroDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHeroBLL
{
    /// <summary>
    /// this class manage the requests from
    /// battle controller
    /// </summary>
    public class BattleBLL
    {

        private BattleDb objDb;

        public BattleBLL()
        {
            objDb = new BattleDb();
        }

        /// <summary>
        /// returns all favourite hero id from the
        /// db in a int list
        /// </summary>
        /// <returns></returns>
        public List<int> GetFavouriteHeroIdList()
        {
            return objDb.GetFavouriteHeroIdList();
        }

        /// <summary>
        /// get back an id list of a given
        /// user's favourite superheroes
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public List<int> GetUserFavouriteHeroIdList(string userId)
        {
            return objDb.GetUserFavouriteHeroIdList(userId);
        }

        /// <summary>
        /// get's back the given user's favourite superhero based
        /// on its id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userHeroApiId"></param>
        /// <returns></returns>
        public FavouriteSuperHero GetUserFavouriteHeroById(string userId, int heroApiId)
        {
            return objDb.GetUserFavouriteHeroById(heroApiId, userId);
        }

        /// <summary>
        /// get's back the given user's favourite superhero,
        /// based on the hero id
        /// </summary>
        /// <param name="apiId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public FavouriteSuperHero GetUserFavouriteHeroById(int apiId, string userId)
        {
            return objDb.GetUserFavouriteHeroById(apiId, userId);
        }

        /// <summary>
        /// get's back a favourite hero
        /// based on the hero id
        /// </summary>
        /// <param name="heroId"></param>
        /// <returns></returns>
        public FavouriteSuperHero GetFavouriteHeroById(int heroId)
        {
            return objDb.GetFavouriteHeroById(heroId);
        }

        /// <summary>
        /// manage the battle,
        /// save the result of the battle to db,
        /// pass back the result to jquery
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string Duel(Combat data, string userId)
        {
           
            Random rnd = new Random();

            //need for balancing fights
            data.UserHeroStat /= 2;
            data.OpponentHeroStat /= 2;

            try
            {

                //when we get heroes those not exactly modelled
                //then we increase their  attributes
                //to make more exciting the fight
                if (data.UserHeroStat > 100)
                    data.UserHeroStat += rnd.Next(50, 151);

                if (data.OpponentHeroStat > 100)
                    data.OpponentHeroStat += rnd.Next(50, 151);

                //here is the combat logic
                if (data.UserHeroSkill >= data.OpponentHeroSkill)
                {
                    data.UserHeroStat += (data.UserHeroSkill - data.OpponentHeroSkill) * 2 + rnd.Next(1, 101);
                    data.OpponentHeroStat += rnd.Next(1, 51);
                }
                else
                {
                    data.UserHeroStat += (data.UserHeroSkill * 2) - data.OpponentHeroSkill + rnd.Next(1, 101);
                    data.OpponentHeroStat += rnd.Next(1, 51);
                }

                //manage the result, write out to the user, and save the battle into db
                if (data.UserHeroStat > data.OpponentHeroStat)
                {
                   objDb.SaveDuelBattelog(data.LeftHeroId, data.RightHeroId, data.LeftHeroId, userId);
                    return $"The winner is {data.UserHeroName}!!!";
                }
                else if (data.UserHeroStat < data.OpponentHeroStat)
                {
                    objDb.SaveDuelBattelog(data.LeftHeroId, data.RightHeroId, data.RightHeroId, userId);
                    return $"The winner is {data.OpponentHeroName}!!!";
                }
                else
                {
                    objDb.SaveDuelBattelog(data.LeftHeroId, data.RightHeroId, null, userId);
                    return "The fight ends with a draw!!!";
                }
            }
            catch (Exception)
            {

                throw;
            }


        }
    }
}
