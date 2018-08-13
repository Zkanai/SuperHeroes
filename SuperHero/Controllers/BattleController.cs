using System;
using System.Linq;
using System.Web.Mvc;
using SuperHero.Models;
using System.Threading.Tasks;
using SuperHero.Models.JsonFromJqueryModels;
using SuperHero.Models.ApiModels;
using SuperHero.ManageApi;
using SuperHero.ManageBattle;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using SuperHeroBLL.Mapping;

namespace SuperHero.Controllers
{

    [Authorize]
    public class BattleController : BaseController  
    {

        SuperHeroDBEntities db = new SuperHeroDBEntities(); //ITT TARTUNK!!!!
        
        //GET: BATTLE WITH FAVOURITE
        [HttpGet]
        public ActionResult Battle(int? leftHeroApiId, int? rightHeroApiId)
        {

            #region Validation
            //if not logged in or get nulls for heroes
            if (rightHeroApiId == null || leftHeroApiId == null)           
                return RedirectToAction("Index", "Home");
           

            var userId = User.Identity.GetUserId();           
            var userHeroApiId = (int)leftHeroApiId;
            var opponentHeroApiId = (int)rightHeroApiId;
            var model = new BattleViewModel();

            var userFavHeroApiIdList = objBs.battleBLL.GetUserFavouriteHeroIdList(userId);

            //if something went wrong
            if (!userFavHeroApiIdList.Contains(userHeroApiId) || !userFavHeroApiIdList.Contains(opponentHeroApiId))
            {
                return RedirectToAction("ChooseHero", "ChooseHeroView");
            }
            #endregion
       
            model.UserHero = objBs.battleBLL.GetUserHero(userId, userHeroApiId);            
            model.OpponentHero = objBs.battleBLL.GetUserHero(userId, userHeroApiId);

            return View(model);
        }

        //GET: BATTLE WITH RANDOM OPPONENT
        [HttpGet]
        public async Task<ActionResult> BattleRandom(int? leftHeroApiId, int? rightHeroApiId)
        {
            try
            {
                #region Validation
                //if get nulls for heroes
                if (rightHeroApiId == null || leftHeroApiId == null)               
                    return RedirectToAction("Index", "Home");
                

                var userId = User.Identity.GetUserId();
                var userHeroApiId = (int)leftHeroApiId;
                var opponentHeroApiId = (int)rightHeroApiId;
                var model = new BattleViewModel();
                var user = db.AspNetUsers.Include(u=>u.FavouriteSuperHero).Where(u => u.Id == userId).FirstOrDefault();

                var userHeroApiIdList = user.FavouriteSuperHero.Select(hero => hero.ApiId).ToList();
                var apiIdList = db.FavouriteSuperHero.Select(hero => hero.ApiId).ToList();

                //if something went wrong
                if (!userHeroApiIdList.Contains(userHeroApiId))
                {
                    return RedirectToAction("ChooseHero", "ChooseHeroView");
                }
                #endregion

                var userHero = user.FavouriteSuperHero.Where(hero => hero.ApiId == userHeroApiId).FirstOrDefault();
                model.UserHero = userHero;

                //the opponent hero is in our db, so don't need to call the API
                //just get the hero from the db
                if (apiIdList.Contains(opponentHeroApiId))
                {
                    var opponentHero = db.FavouriteSuperHero.Where(hero => hero.ApiId == opponentHeroApiId).FirstOrDefault();
                    model.OpponentHero = opponentHero;

                    return View("Battle", model);
                }

                //if hero don't exist in our db yet
                //the we call the API, and get the required data
                var randomOpponentHero = await ApiCall.GetHeroById(opponentHeroApiId);
                model.OpponentHero = BattleMapping.Mapping(randomOpponentHero);

                return View("Battle", model);
            }
            catch (ApiNotFoundException)
            {
                //mapping userHero
                var userId = User.Identity.GetUserId();
                var userHeroApiId = (int)leftHeroApiId;
                var user = db.AspNetUsers.Include(u=>u.FavouriteSuperHero).Where(u => u.Id == userId).FirstOrDefault();
                var model = new BattleViewModel();
                var userHero = user.FavouriteSuperHero.Where(hero => hero.ApiId == userHeroApiId).FirstOrDefault();
                model.UserHero = userHero;
               
                //mapping enemy hero
                var randomOpponentHero = new SuperHeroById.HeroById();
                model.OpponentHero = BattleMapping.MappingWhenApiNotWorking(randomOpponentHero);

                return View("Battle", model);
            }
            catch (Exception)
            {

                throw;
            }

        }

        //POST: FROM JQUERY AJAX 
        [HttpPost]
        public JsonResult Battle(Combat data)
        {

            var userId = User.Identity.GetUserId();

            return Json(Duel.Combat(data, User.Identity.GetUserId()));
        }


    }
}