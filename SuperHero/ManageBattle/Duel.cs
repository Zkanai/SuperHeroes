using SuperHero.Models;
using SuperHero.Models.JsonFromJqueryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperHero.ManageBattle
{
    public class Duel
    {
        /// <summary>
        /// manage the battle,
        /// save the result of the battle to db,
        /// pass back the result to jquery
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Combat(Combat data, int? sessionUserId, SuperHeroDBEntities db)
        {
            var model = new BattleViewModel();
            Random rnd = new Random();
            var userId = (int)sessionUserId;

            //need for fair fights
            data.UserHeroStat /= 2;
            data.OpponentHeroStat /= 2;

            try
            {
                var user = db.User.Where(u => u.Id == userId).FirstOrDefault();

                //when we get heroes those not exactly modelled
                //then we increase their  attributes
                //to make more exciting the fight
                if (data.UserHeroStat>100)
                {
                    data.UserHeroStat += rnd.Next(50,151);
                }

                if (data.OpponentHeroStat > 100)
                {
                    data.OpponentHeroStat += rnd.Next(50, 151);
                }

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
                    SaveBattelog(data.LeftHeroId, data.RightHeroId, data.LeftHeroId, userId, db);
                    return $"The winner is {data.UserHeroName}!!!";
                }
                else if (data.UserHeroStat < data.OpponentHeroStat)
                {
                    SaveBattelog(data.LeftHeroId, data.RightHeroId, data.RightHeroId, userId, db);
                    return $"The winner is {data.OpponentHeroName}!!!";
                }
                else
                {
                    SaveBattelog(data.LeftHeroId, data.RightHeroId, null, userId, db);
                    return "The fight ends with a draw!!!";
                }
            }
            catch (Exception)
            {

                throw;
            }
           

        }

        /// <summary>
        /// save the battle result to the db
        /// </summary>
        /// <param name="userHeroId"></param>
        /// <param name="opponentHeroId"></param>
        /// <param name="winnerId"></param>
        public static void SaveBattelog(int userHeroId, int opponentHeroId, int? winnerId, int userId, SuperHeroDBEntities db)
        {
            var newLog = new BattleLog();

            try
            {            
                //mapping
                newLog.UserHeroId = userHeroId;
                newLog.OpponentHeroId = opponentHeroId;
                newLog.WinnerHeroId = winnerId;
                newLog.UserId = userId;

                //insert record to the db
                using (var transaction = db.Database.BeginTransaction())
                {
                    db.BattleLog.Add(newLog);
                    db.SaveChanges();
                    transaction.Commit();
                }
            }
            catch (Exception)
            {

                throw;
            }

            
        }
    }
}