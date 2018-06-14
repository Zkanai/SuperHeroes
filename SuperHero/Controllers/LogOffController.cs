
using System.Web.Mvc;

namespace SuperHero.Controllers
{
    public class LogOffController : Controller
    {
        // GET: LogOff
        public ActionResult LogOff()
        {
            Session["role"] = "";
            Session["userName"] = "";
            Session["userId"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}