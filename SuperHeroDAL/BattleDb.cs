using SuperHero;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace SuperHeroDAL
{
    public class BattleDb:BaseDb
    {

        /// <summary>
        /// save the battle result to the db
        /// </summary>
        /// <param name="userHeroId"></param>
        /// <param name="opponentHeroId"></param>
        /// <param name="winnerId"></param>
        public void SaveDuelBattelog(int userHeroId, int opponentHeroId, int? winnerId, string userId)
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
