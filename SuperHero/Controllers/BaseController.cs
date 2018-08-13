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

        protected BaseBLL objBs;

        public BaseController()
        {
            objBs = new BaseBLL();
        }

        

    }
}