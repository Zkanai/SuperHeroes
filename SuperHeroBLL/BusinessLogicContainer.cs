using SuperHero;
using SuperHeroDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHeroBLL
{
    /// <summary>
    /// contains as properties all of
    /// the business logic layer classes
    /// </summary>
    public class BusinessLogicContainer
    {

        public DetailedHeroBLL detailedHeroBLL;
        public BattleBLL battleBLL;
        public ChooseHeroBLL chooseHeroBLL;
            
        public BusinessLogicContainer()
        {
            detailedHeroBLL = new DetailedHeroBLL();
            battleBLL = new BattleBLL();
            chooseHeroBLL = new ChooseHeroBLL();
            
        }
      
    }
}
