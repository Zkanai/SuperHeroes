using System;
using System.Linq;
using System.Web.Mvc;
using SuperHero.Models;
using SuperHero.Models.JsonFromJqueryModels;

namespace SuperHero.Controllers
{
    public class ChooseHeroViewController : Controller
    {
        SuperHeroDBEntities db = new SuperHeroDBEntities();

        /// <summary>
        /// mapping back the view model,
        /// when something goes wrong
        /// </summary>
        /// <returns></returns>
        public ChooseHeroesViewModel Mapping()
        {
            //mapping back
            var model = new ChooseHeroesViewModel();
            var userId = (int)Session["userId"];
            var favSuperHeroList = db.User.Where(u => u.Id == userId).FirstOrDefault().FavouriteSuperHero.ToList();

            ViewBag.HeroList = favSuperHeroList;
            model.UserHeroList = favSuperHeroList;
            model.OpponentHeroList = favSuperHeroList;

            return model;
        }

        // GET: ChooseHeroView
        public ActionResult ChooseHero()
        {
            var model = new ChooseHeroesViewModel();

            if (Session["userId"] != null)
            {
                try
                {
                    var userId = (int)Session["userId"];
                    var favSuperHeroList = db.User.Where(u => u.Id == userId).FirstOrDefault().FavouriteSuperHero.ToList();

                    ViewBag.HeroList = favSuperHeroList;
                    model.UserHeroList = favSuperHeroList;
                    model.OpponentHeroList = favSuperHeroList;

                    return View();
                }
                catch (Exception)
                {

                    throw;
                }

            }

            return RedirectToAction("Index", "Home");

        }

        //POST FROM AJAX, when user select a hero
        [HttpPost]
        public ActionResult ChosenUserHero(UserHeroData data)
        {
            var chosenUserHero = new UserHeroData();

            try
            {
                #region Validation
                if (Session["userId"]==null)
                {
                    return RedirectToAction("Index", "Home");
                }

                //got wrong invalid userhero
                //relaod the page
                if (data.UserHeroId == null || data.UserHeroId <= 0)
                {
                    //mapping the model back and return the view
                    return View(nameof(ChooseHero), Mapping());
                }
                #endregion

                var userHero = db.FavouriteSuperHero.Where(hero => hero.ApiId == data.UserHeroId).FirstOrDefault();

                //mapping
                if (userHero != null)
                {
                    chosenUserHero.UserHeroId = data.UserHeroId;
                    chosenUserHero.UserHeroName = userHero.Name;
                    chosenUserHero.UserHeroRealName = userHero.RealName;
                    chosenUserHero.UserHeroIntelligence = userHero.Intelligence;
                    chosenUserHero.UserHeroStrength = userHero.Strength;
                    chosenUserHero.UserHeroSpeed = userHero.Speed;
                    chosenUserHero.UserHeroDurability = userHero.Durability;
                    chosenUserHero.UserHeroPower = userHero.Power;
                    chosenUserHero.UserHeroCombat = userHero.Combat;
                    chosenUserHero.UserHeroImgUrl = userHero.ImgUrl;
                }

                return Json(chosenUserHero, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }

        //POST FROM AJAX, when user selet a enemy hero
        [HttpPost]
        public ActionResult ChosenOpponentHero(OpponentHeroData data)
        {
            var chosenOpponentHero = new OpponentHeroData();

            try
            {
                #region Validation
                if (Session["userId"] == null)
                {
                    return RedirectToAction("Index", "Home");
                }

                if (data.OpponentHeroId == null || data.OpponentHeroId <= 0)
                {
                    //mapping the model back and return the view                  
                    return View(nameof(ChooseHero), Mapping());
                }
                #endregion

                var opponentHero = db.FavouriteSuperHero.Where(hero => hero.ApiId == data.OpponentHeroId).FirstOrDefault();

                //mapping
                if (opponentHero != null)
                {
                    chosenOpponentHero.OpponentHeroId = data.OpponentHeroId;
                    chosenOpponentHero.OpponentHeroName = opponentHero.Name;
                    chosenOpponentHero.OpponentHeroRealName = opponentHero.RealName;
                    chosenOpponentHero.OpponentHeroImgUrl = opponentHero.ImgUrl;
                }

                return Json(chosenOpponentHero, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }       
        }

        //POST: ChooseHeroView to BattleView
        [HttpPost]
        public ActionResult ChooseHero(ChooseHeroesViewModel model)
        {
            try
            {
                var rnd = new Random();
                var heroId = model.UserHeroApiIdList.FirstOrDefault();
                var oppId = model.OpponentHeroApiIdList.FirstOrDefault();

                //the user choosed a random enemy hero
                if (oppId == 0)
                {
                    oppId = rnd.Next(1, 733);
                    return RedirectToAction("BattleRandom", "Battle", new { leftHeroApiId = heroId, rightHeroApiId = oppId });
                }

                return RedirectToAction("Battle", "Battle", new { leftHeroApiId = heroId, rightHeroApiId = oppId });
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}