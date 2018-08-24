using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHeroBLL.Mapping
{
    internal static class MappingExtensions
    {
        /// <summary>
        /// helps to convert the heroes stats, cause we get them in string from the api
        /// </summary>
        /// <param name="herostat"></param>
        /// <returns></returns>
        internal static int StatStringToInt(string herostat)
        {
            int stat = 0;
            int.TryParse(herostat, out stat);
            return stat;
        }
    }
}
