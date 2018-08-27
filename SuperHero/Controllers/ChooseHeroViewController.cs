using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SuperHero.Models;
using SuperHero.Models.JsonFromJqueryModels;

namespace SuperHero.Controllers
{
    [Authorize]
    public class ChooseHeroViewController : BaseController
    {
         
        // GET: ChooseHeroView
        public ActionResult ChooseHero()
        {
            
            var model = new ChooseHeroesViewModel();
            var userId = User.Identity.GetUserId();

            try
            {
                var userFavSuperHeroesList= objBs.FavouriteSuperHeroBLL.GetUserFavouriteSuperHeroList(userId);
                model = objBs.chooseHeroBLL.Mapping(userFavSuperHeroesList);
                ViewBag.HeroList = userFavSuperHeroesList;
              
                return View();
            }
            catch (Exception)
            {

                throw;
            }

        }

        //POST FROM AJAX, when user select a hero
        [HttpPost]
        public ActionResult ChosenUserHero(UserHeroData data)
        {
            
            var userId = User.Identity.GetUserId();

            try
            {
                #region Validation              
                //got wrong invalid userhero
                //relaod the page
                if (data.UserHeroId == null || data.UserHeroId <= 0)
                {
                    //mapping the model back and return the view
                    var userFavSuperHeroesList = objBs.FavouriteSuperHeroBLL.GetUserFavouriteSuperHeroList(userId);
                    ViewBag.HeroList = userFavSuperHeroesList;
                    return View(nameof(ChooseHero), objBs.chooseHeroBLL.Mapping(userFavSuperHeroesList));
                }
                #endregion

                var heroId = (int)data.UserHeroId; 
                
                return Json(objBs.chooseHeroBLL.MapUserHero(heroId), JsonRequestBehavior.AllowGet);
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
            
            var userId = User.Identity.GetUserId();

            try
            {
                #region Validation             
                if (data.OpponentHeroId == null || data.OpponentHeroId <= 0)
                {
                    //mapping the model back and return the view  
                    var userFavSuperHeroesList = objBs.FavouriteSuperHeroBLL.GetUserFavouriteSuperHeroList(userId);
                    ViewBag.HeroList = userFavSuperHeroesList;
                    return View(nameof(ChooseHero), objBs.chooseHeroBLL.Mapping(userFavSuperHeroesList));
                }
                #endregion

                var heroId = (int)data.OpponentHeroId;
               
                return Json(objBs.chooseHeroBLL.MapOpponentHero(heroId), JsonRequestBehavior.AllowGet);
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