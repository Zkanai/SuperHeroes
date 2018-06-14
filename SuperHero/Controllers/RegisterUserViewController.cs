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
                    ModelState.AddModelError(nameof(model.ConfirmPassWord), "A két jelszónak meg kell egyeznie!");

                if (userNameList.Contains(model.UserName))
                    ModelState.AddModelError(nameof(model.UserName), "Már létezik ilyen felhasználó név!");

                if (emailList.Contains(model.Email))
                    ModelState.AddModelError(nameof(model.Email), "Már regisztráltak ilyen email-címmel!");

                if (!ModelState.IsValid)
                {
                    //mapping                   
                    return View(model);
                }

                var salt = Crypto.GenerateSalt();
                var hashedPw = Crypto.SHA256(model.PassWord+salt);
                //insert record to the publication table
                User user = new User();
                user.UserName = model.UserName;
                user.Email = model.Email;
                user.PassWord = hashedPw;
                user.Name = model.Name;             
                user.Salt = salt;

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

                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}