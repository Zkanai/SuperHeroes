using System.Linq;
using System.Web.Mvc;
using SuperHero.Models;
using System.Web.Helpers;


namespace SuperHero.Controllers
{

    public class LoginViewController : Controller
    {

        SuperHeroDBEntities db = new SuperHeroDBEntities();

        // GET: Login
        [HttpGet]
        public ActionResult Login()
        {

            return View();
            
        }

        // POST: Login
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {

            var userNameList = db.User.Select(u => u.UserName).ToList();
            var emailList = db.User.Select(u => u.Email).ToList();

            //username or email not exist
            if (!(userNameList.Contains(model.UserName) || !emailList.Contains(model.UserName)))
            {
                ModelState.AddModelError(nameof(model.PassWord), "Wrong password/username or email!");
                return View(model);
            }

            var hashedPw = db.User.Where(u => (u.UserName == model.UserName) || (u.Email == model.UserName))
                                  .Select(u => u.PassWord).FirstOrDefault();
            var salt = db.User.Where(u => (u.UserName == model.UserName) || (u.Email == model.UserName))
                                  .Select(u => u.Salt).FirstOrDefault();

            //verify hashed pw          
            if (model.PassWord == null || Crypto.SHA256(model.PassWord + salt) != hashedPw)
            {
                ModelState.AddModelError(nameof(model.PassWord), "Wrong password/username or email!");
                return View(model);
            }

            Session["role"] = "member";
            Session["userName"] = db.User.Where(u => (u.UserName == model.UserName) || (u.Email == model.UserName))
                                         .Select(u => u.UserName).FirstOrDefault();
            var userId = db.User.Where(u => (u.UserName == model.UserName) || (u.Email == model.UserName))
                                .Select(u => u.Id).FirstOrDefault();
            Session["userId"] = userId;

            //when user come here from searching first,
            //redirect to the hero details view by the hero id
            var heroToShowId = Session["heroToShowId"]?.ToString();

            if (heroToShowId != null)
            {
                return RedirectToAction("DetailedHero", "DetailedHeroView", new { id = heroToShowId });
            }

            return RedirectToAction("Index", "Home");
        }
    }
}