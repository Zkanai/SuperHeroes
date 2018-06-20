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

namespace SuperHero.Controllers
{

    public class BattleController : Controller
    {

        SuperHeroDBEntities db = new SuperHeroDBEntities();

        /// <summary>
        /// help modelling the data we get from the api
        /// </summary>
        /// <param name="randomOpponentHero"></param>
        /// <returns></returns>
        public FavouriteSuperHero Mapping(SuperHeroById.HeroById randomOpponentHero)
        {

            var opponentHero = new FavouriteSuperHero();

            try
            {
                opponentHero.ApiId = Convert.ToInt32(randomOpponentHero.ApiId);
                opponentHero.Name = randomOpponentHero.Name;
                opponentHero.RealName = randomOpponentHero.Biography.Full_Name;
                opponentHero.ImgUrl = randomOpponentHero.Image.Url;
                opponentHero.Intelligence = ApiCall.StatStringToInt(randomOpponentHero.Powerstats.Intelligence);
                opponentHero.Strength = ApiCall.StatStringToInt(randomOpponentHero.Powerstats.Strength);
                opponentHero.Speed = ApiCall.StatStringToInt(randomOpponentHero.Powerstats.Speed);
                opponentHero.Durability = ApiCall.StatStringToInt(randomOpponentHero.Powerstats.Durability);
                opponentHero.Power = ApiCall.StatStringToInt(randomOpponentHero.Powerstats.Power);
                opponentHero.Combat = ApiCall.StatStringToInt(randomOpponentHero.Powerstats.Combat);

                return opponentHero;
            }
            catch (Exception)
            {

                throw;
            }


        }

        /// <summary>
        /// modelling a random enemy hero when api
        /// not working
        /// </summary>
        /// <param name="randomOpponentHero"></param>
        /// <returns></returns>
        public FavouriteSuperHero MappingWhenApiNotWorking(SuperHeroById.HeroById randomOpponentHero)
        {

            var opponentHero = new FavouriteSuperHero();

            try
            {
                opponentHero.ApiId = 0;
                opponentHero.Name = "Temporarily Unavailable!";
                opponentHero.RealName = "Temporarily Unavailable!";
                opponentHero.ImgUrl = "../../../img/pictureNA.jpg";
                opponentHero.Intelligence = 0;
                opponentHero.Strength = 0;
                opponentHero.Speed = 0;
                opponentHero.Durability = 0;
                opponentHero.Power = 0;
                opponentHero.Combat = 0;    

                return opponentHero;
            }
            catch (Exception)
            {

                throw;
            }


        }

        //GET: BATTLE WITH FAVOURITE
        [HttpGet]
        public ActionResult Battle(int? leftHeroApiId, int? rightHeroApiId)
        {

            #region Validation
            //if not logged in or get nulls for heroes
            if ((Session["userId"] == null) || rightHeroApiId == null || leftHeroApiId == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var userId = (int)Session["userId"];
            var userHeroApiId = (int)leftHeroApiId;
            var opponentHeroApiId = (int)rightHeroApiId;
            var model = new BattleViewModel();
            var user = db.User.Include(u=>u.FavouriteSuperHero).Where(u => u.Id == userId).FirstOrDefault();

            var userFavHeroApiIdList = user.FavouriteSuperHero.Select(hero => hero.ApiId).ToList();

            //if something went wrong
            if (!userFavHeroApiIdList.Contains(userHeroApiId) || !userFavHeroApiIdList.Contains(opponentHeroApiId))
            {
                return RedirectToAction("ChooseHero", "ChooseHeroView");
            }
            #endregion

            var userHero = user.FavouriteSuperHero.Where(hero => hero.ApiId == userHeroApiId).FirstOrDefault();
            model.UserHero = userHero;

            var opponentHero = user.FavouriteSuperHero.Where(hero => hero.ApiId == opponentHeroApiId).FirstOrDefault();
            model.OpponentHero = opponentHero;

            return View(model);
        }

        //GET: BATTLE WITH RANDOM OPPONENT
        [HttpGet]
        public async Task<ActionResult> BattleRandom(int? leftHeroApiId, int? rightHeroApiId)
        {
            try
            {
                #region Validation
                //if not logged in or get nulls for heroes
                if ((Session["userId"] == null) || rightHeroApiId == null || leftHeroApiId == null)
                {
                    return RedirectToAction("Index", "Home");
                }

                var userId = (int)Session["userId"];
                var userHeroApiId = (int)leftHeroApiId;
                var opponentHeroApiId = (int)rightHeroApiId;
                var model = new BattleViewModel();
                var user = db.User.Include(u=>u.FavouriteSuperHero).Where(u => u.Id == userId).FirstOrDefault();

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
                model.OpponentHero = Mapping(randomOpponentHero);

                return View("Battle", model);
            }
            catch (ApiNotFoundException)
            {
                //mapping userHero
                var userId = (int)Session["userId"];
                var userHeroApiId = (int)leftHeroApiId;
                var user = db.User.Include(u=>u.FavouriteSuperHero).Where(u => u.Id == userId).FirstOrDefault();
                var model = new BattleViewModel();
                var userHero = user.FavouriteSuperHero.Where(hero => hero.ApiId == userHeroApiId).FirstOrDefault();
                model.UserHero = userHero;
               
                //mapping enemy hero
                var randomOpponentHero = new SuperHeroById.HeroById();
                model.OpponentHero = MappingWhenApiNotWorking(randomOpponentHero);

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
            if (Session["userId"] == null)
            {
                return Json("Something went wrong!");
            }

            var userId = (int)Session["userId"];

            return Json(Duel.Combat(data, userId));
        }


    }
}