using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SuperHero.Models;


namespace SuperHero.Controllers
{
    public class HomeController : Controller
    {
        SuperHeroDBEntities db = new SuperHeroDBEntities();

        //Get
        [HttpGet]
        public ActionResult Index()
        {
            var model = new List<FavouriteSuperHeroViewModel>();

            try
            {
                if (Session?["userId"] != null)
                {
                    var userId = (int)Session["userId"];
                    var user = db.User.Where(u => u.Id == userId).FirstOrDefault();

                    if (user==null)
                    {
                        return View(model);
                    }

                    var favSuperHero = user.FavouriteSuperHero;
                    
                    //mapping
                    model = favSuperHero.Select(hero => new FavouriteSuperHeroViewModel()
                    {
                        Id = hero.Id,
                        ApiId=hero.ApiId.ToString(),
                        Name = hero.Name,
                        RealName = hero.RealName,
                        ImgUrl = hero.ImgUrl,
                        Intelligence = hero.Intelligence,
                        Strength = hero.Strength,
                        Speed = hero.Speed,
                        Durability = hero.Durability,
                        Power = hero.Power,
                        Combat = hero.Combat,
                        Win = db.BattleLog.Where(log => log.UserHeroId == hero.ApiId && log.WinnerHeroId == hero.ApiId && log.UserId == userId).ToList().Count,
                        Loose = db.BattleLog.Where(log => log.UserHeroId == hero.ApiId && log.WinnerHeroId != hero.ApiId && log.WinnerHeroId != null && log.UserId == userId).ToList().Count,
                        Draw = db.BattleLog.Where(log => log.UserHeroId == hero.ApiId && log.WinnerHeroId == null && log.UserId == userId).ToList().Count

                    }).ToList();
                }

                return View(model);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}