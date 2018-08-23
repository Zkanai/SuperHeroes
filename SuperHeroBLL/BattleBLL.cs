using SuperHero.Models.JsonFromJqueryModels;
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
    public class BattleBLL:SharedBLL
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
                   objBs.SaveDuelBattelog(data.LeftHeroId, data.RightHeroId, data.LeftHeroId, userId);
                    return $"The winner is {data.UserHeroName}!!!";
                }
                else if (data.UserHeroStat < data.OpponentHeroStat)
                {
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

    }
}
