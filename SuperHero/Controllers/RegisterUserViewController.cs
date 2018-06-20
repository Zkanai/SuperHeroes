using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Helpers;
using SuperHero.Models;
using System.Data.Entity;

namespace SuperHero.Controllers
{
    public class RegisterUserViewController : Controller
    {
        SuperHeroDBEntities db = new SuperHeroDBEntities();
       
        // Get Register
        [HttpGet]
        public ActionResult Register()
        {          
            return View();
        }

        //POST Create
        [HttpPost]
        public ActionResult Register(RegisterUserViewModel model)
        {
            try
            {
                //unique validations
                var userNameList = db.User.Select(u => u.UserName).ToList();
                var emailList = db.User.Select(u => u.Email).ToList();

                if (model.PassWord != model.ConfirmPassWord)
                    ModelState.AddModelError(nameof(model.ConfirmPassWord), "The two passwords has to be same!");

                if (userNameList.Contains(model.UserName))
                    ModelState.AddModelError(nameof(model.UserName), "Already exists!");

                if (emailList.Contains(model.Email))
                    ModelState.AddModelError(nameof(model.Email), "Already exists!");

                if (!ModelState.IsValid)
                {
                    //mapping                   
                    return View(model);
                }

                var salt = Crypto.GenerateSalt();
                var hashedPw = Crypto.SHA256(model.PassWord+salt);

                //mapping
                User user = new User();
                user.UserName = model.UserName;
                user.Email = model.Email;
                user.PassWord = hashedPw;
                user.Name = model.Name;             
                user.Salt = salt;

                //insert record to the db
                using (DbContextTransaction tran = db.Database.BeginTransaction())
                {
                    db.User.Add(user);
                    db.SaveChanges();
                    tran.Commit();
                }

                var userIdList = db.User.Select(u => u.Id).ToList();
                Session["role"] = "member";
                Session["userId"] = userIdList.LastOrDefault();
                Session["userName"] = user.Name;

                //when user come here from searching first,
                //redirect to the hero details view by the hero id
                var heroToShowId = Session["heroToShowId"]?.ToString();

                if (heroToShowId != null)
                    return RedirectToAction("DetailedHero", "DetailedHeroView", new { id = heroToShowId });


                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}