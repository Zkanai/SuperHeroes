using SuperHeroDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHeroBLL
{
    /// <summary>
    /// manage the requests that use the BattLog table from the db
    /// </summary>
    public class BattleLogBLL
    {
        private BattleLogDb objDb;

        public BattleLogBLL()
        {
            objDb = new BattleLogDb();
        }

        /// <summary>
        /// save the battle result to the db
        /// </summary>
        /// <param name="userHeroId"></param>
        /// <param name="opponentHeroId"></param>
        /// <param name="winnerId"></param>
        public void SaveDuelBattelog(int userHeroId, int opponentHeroId, int? winnerId, string userId)
        {

            objDb.SaveDuelBattelog(userHeroId, opponentHeroId, winnerId, userId);

        }
    }
}
