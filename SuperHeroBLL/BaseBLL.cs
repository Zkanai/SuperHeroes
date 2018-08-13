using SuperHero;
using SuperHeroDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHeroBLL
{
    public class BaseBLL
    {

        public DetailedHeroBLL detailedHeroBLL;

        /// <summary>
        /// gives back a user from db based on
        /// her id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BaseBLL()
        {
            detailedHeroBLL = new DetailedHeroBLL();         
        }
    
    }
}
