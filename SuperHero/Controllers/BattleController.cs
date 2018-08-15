using System;
using System.Linq;
using System.Web.Mvc;
using SuperHero.Models;
using System.Threading.Tasks;
using SuperHero.Models.JsonFromJqueryModels;
using SuperHero.Models.ApiModels;
using SuperHero.ManageApi;
using Microsoft.AspNet.Identity;
using SuperHeroBLL.Mapping;

namespace SuperHero.Controllers
{

    [Authorize]
    public class BattleController : BaseController  
    {
              
        //GET: BATTLE WITH FAVOURITE
        [HttpGet]
        public ActionResult Battle(int? leftHeroApiId, int? rightHeroApiId)
        {
            try
            {
                #region Validation
                //if not logged in or get nulls for heroes
                if (rightHeroApiId == null || leftHeroApiId == null)
                    return RedirectToAction("Index", "Home");

                var userId = User.Identity.GetUserId();
                var userHeroApiId = (int)leftHeroApiId;
                var opponentHeroApiId = (int)rightHeroApiId;
                var model = new BattleViewModel();

                //if something went wrong
                var userFavHeroApiIdList = objBs.battleBLL.GetUserFavouriteHeroIdList(userId);               
                if (!userFavHeroApiIdList.Contains(userHeroApiId) || !userFavHeroApiIdList.Contains(opponentHeroApiId))
                {
                    return RedirectToAction("ChooseHero", "ChooseHeroView");
                }
                #endregion

                model.UserHero = objBs.battleBLL.GetUserFavouriteHeroById(userId, userHeroApiId);
                model.OpponentHero = objBs.battleBLL.GetUserFavouriteHeroById(userId, opponentHeroApiId);

                return View(model);
            }
            catch (Exception)
            {

                throw;
            }
           
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

                //if something went wrong
                var userHeroApiIdList = objBs.battleBLL.GetUserFavouriteHeroIdList(userId);
                if (!userHeroApiIdList.Contains(userHeroApiId))
                {
                    return RedirectToAction("ChooseHero", "ChooseHeroView");
                }
                #endregion

                var apiIdList = objBs.battleBLL.GetFavouriteHeroIdList();
                var userHero = objBs.battleBLL.GetUserFavouriteHeroById(userId, userHeroApiId);
                model.UserHero = userHero;

                //the opponent hero is in our db, so don't need to call the API
                //just get the hero from the db
                if (apiIdList.Contains(opponentHeroApiId))
                {
                    var opponentHero = objBs.battleBLL.GetFavouriteHeroById(opponentHeroApiId);
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
                //mapping user's hero
                var userId = User.Identity.GetUserId();
                var userHeroApiId = (int)leftHeroApiId;
                var model = new BattleViewModel();
                var userHero = objBs.battleBLL.GetUserFavouriteHeroById(userId, userHeroApiId);
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
            try
            {
                var userId = User.Identity.GetUserId();

                return Json(objBs.battleBLL.Duel(data, userId));
            }
            catch (Exception)
            {

                throw;
            }
           
        }


    }
}