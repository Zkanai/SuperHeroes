using SuperHero;
using SuperHero.Models.ApiModels;
using SuperHero.Models.JsonFromJqueryModels;
using SuperHeroBLL.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHeroBLL
{
    /// <summary>
    /// manage the requests from
    /// battle controller, manage the battles
    /// </summary>
    public class BattleBLL
    {

        private BattleLogBLL objBs;

        public BattleBLL()
        {
            objBs = new BattleLogBLL();
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

            try
            {

                Initializer(data, rnd);
                Round(data, rnd);


                //itt kell eldönteni levonni a megfelelő sebzést, és eldönteni, hogy vége van e a csatának
                var dmgUHero = 0;
                var dmgOHero = 0;
                //manage the result, write out to the user, and save the battle into db
                if (data.UserHeroStat >= data.OpponentHeroStat)
                {

                     dmgUHero = (data.UserHeroStat - data.OpponentHeroStat) / 10 + data.UserHeroDmg;
                     dmgOHero = data.OpponentHeroDmg / 2;
                    objBs.SaveDuelBattelog(data.LeftHeroId, data.RightHeroId, data.LeftHeroId, userId);
                    return $"The winner is {data.UserHeroName}!!!";
                }
                else if (data.UserHeroStat < data.OpponentHeroStat)
                {
                    dmgUHero = data.UserHeroDmg / 2;
                    dmgOHero= (data.OpponentHeroStat - data.UserHeroStat) / 10 + data.OpponentHeroDmg;
                    objBs.SaveDuelBattelog(data.LeftHeroId, data.RightHeroId, data.RightHeroId, userId);
                    return $"The winner is {data.OpponentHeroName}!!!";
                }
                else
                {
                    objBs.SaveDuelBattelog(data.LeftHeroId, data.RightHeroId, null, userId);
                    return "The fight ends with a draw!!!";
                }
            }
            catch (Exception)
            {

                throw;
            }


        }

        /// <summary>
        /// setting up the fight
        /// </summary>
        /// <param name="data"></param>
        private void Initializer(Combat data, Random rnd)
        {
            Balancer(data, rnd);
            SetHp(data);
        }

        /// <summary>
        /// make the stats closer or
        /// when we get heroes those not exactly modelled
        /// then we increase their  attributes
        /// to make more exciting the fight
        /// </summary>
        /// <param name="data"></param>
        private void Balancer(Combat data, Random rnd)
        {

            if (data.UserHeroStat < 100)
                data.UserHeroStat += rnd.Next(50, 151);

            if (data.OpponentHeroStat < 100)
                data.OpponentHeroStat += rnd.Next(50, 151);

            data.UserHeroStat /= 5;
            data.OpponentHeroStat /= 5;

            if (data.UserHeroHp <= 0)
                data.UserHeroHp = 50;

            if (data.OpponentHeroHp <= 0)
                data.OpponentHeroHp = 50;

            if (data.UserHeroDmg <= 20)
                data.UserHeroDmg = 20;

            if (data.OpponentHeroDmg <= 20)
                data.OpponentHeroDmg = 20;

        }

        private void SetHp(Combat data)
        {
            data.UserHeroHp *= 10;
            data.OpponentHeroHp *= 10;
        }

        /// <summary>
        /// this manage a round of the fight,
        /// actually the logic behind who will win
        /// </summary>
        /// <param name="data"></param>
        /// <param name="rnd"></param>
        private void Round(Combat data, Random rnd) //EZT MÉG KI KELL AGYALNI
        {
            if (data.UserHeroSkill >= data.OpponentHeroSkill)
            {
                data.UserHeroStat += (data.UserHeroSkill - data.OpponentHeroSkill) * 2 + rnd.Next(110, 181);
            }
            else
            {
                data.OpponentHeroStat += data.OpponentHeroSkill - data.UserHeroSkill + rnd.Next(0, 101);
            }
        }

        public FavouriteSuperHero Mapping(SuperHeroById.HeroById randomOpponentHero)
        {
            return BattleMapping.Mapping(randomOpponentHero);
        }

        public FavouriteSuperHero MappingWhenApiNotWorking(SuperHeroById.HeroById randomOpponentHero)
        {
            return BattleMapping.MappingWhenApiNotWorking(randomOpponentHero);
        }

    }
}
