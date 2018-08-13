using SuperHeroBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SuperHero.Controllers
{
    public class BaseController : Controller
    {

        protected BusinessLogicContainer objBs;

        public BaseController()
        {
            objBs = new BusinessLogicContainer();
        }

        

    }
}