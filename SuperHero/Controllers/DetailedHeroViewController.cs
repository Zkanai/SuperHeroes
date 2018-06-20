using SuperHero.ManageApi;
using SuperHero.Models;
using SuperHero.Models.ApiModels;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;



namespace SuperHero.Controllers
{
    public class DetailedHeroViewController : Controller
    {

        SuperHeroDBEntities db = new SuperHeroDBEntities();

        /// <summary>
        /// mappging the data from our api model
        /// </summary>
        /// <param name="model"></param>
        /// <param name="hero"></param>
        /// <returns></returns>
        public DetailedHeroViewModel Mapping(DetailedHeroViewModel model, SuperHeroById.HeroById hero)
        {
            try
            {
                model.ApiId = hero.ApiId;
                model.Name = hero.Name;
                model.ImageUrl = hero.Image.Url;
                model.BiographyData.Full_Name = hero.Biography.Full_Name;
                model.BiographyData.Alignment = hero.Biography.Alignment;
                model.BiographyData.Place_Of_Birth = hero.Biography.Place_Of_Birth;
                model.BiographyData.Publisher = hero.Biography.Publisher;
                model.AppearanceValues.Gender = hero.Appearance.Gender;
                model.AppearanceValues.Race = hero.Appearance.Race;
                model.Powerstat.Intelligence = hero.Powerstats.Intelligence;
                model.Powerstat.Strength = hero.Powerstats.Strength;
                model.Powerstat.Speed = hero.Powerstats.Speed;
                model.Powerstat.Durability = hero.Powerstats.Durability;
                model.Powerstat.Power = hero.Powerstats.Power;
                model.Powerstat.Combat = hero.Powerstats.Combat;

                return model;
            }
            catch (Exception)
            {

                throw;
            }


        }

        /// <summary>
        /// mapping the data from our db
        /// or if the hero not in our db then mapping
        /// a not available hero
        /// </summary>
        /// <param name="model"></param>
        /// <param name="hero"></param>
        /// <returns></returns>
        public DetailedHeroViewModel Mapping(DetailedHeroViewModel model, FavouriteSuperHero hero)
        {
            var infoWhenApiNA = "Temporarily Unavailable!";

            try
            {
                //if the hero doesn't exist in our db yet
                if (hero == null)
                {
                    model.Name = infoWhenApiNA;
                    model.ImageUrl = "../../../img/pictureNA.jpg";
                    model.BiographyData.Full_Name = infoWhenApiNA;
                    model.BiographyData.Alignment = infoWhenApiNA;
                    model.BiographyData.Place_Of_Birth = infoWhenApiNA;
                    model.BiographyData.Publisher = infoWhenApiNA;
                    model.AppearanceValues.Gender = infoWhenApiNA;
                    model.AppearanceValues.Race = infoWhenApiNA;
                    model.Powerstat.Intelligence = infoWhenApiNA;
                    model.Powerstat.Strength = infoWhenApiNA;
                    model.Powerstat.Speed = infoWhenApiNA;
                    model.Powerstat.Durability = infoWhenApiNA;
                    model.Powerstat.Power = infoWhenApiNA;
                    model.Powerstat.Combat = infoWhenApiNA;

                    return model;
                }

                //if the hero is in our db already
                model.ApiId = hero.ApiId.ToString();
                model.Name = hero.Name;
                model.ImageUrl = hero.ImgUrl;
                model.BiographyData.Full_Name = hero.RealName;
                model.BiographyData.Alignment = infoWhenApiNA;
                model.BiographyData.Place_Of_Birth = infoWhenApiNA;
                model.BiographyData.Publisher = infoWhenApiNA;
                model.AppearanceValues.Gender = infoWhenApiNA;
                model.AppearanceValues.Race = infoWhenApiNA;
                model.Powerstat.Intelligence = hero.Intelligence.ToString();
                model.Powerstat.Strength = hero.Strength.ToString();
                model.Powerstat.Speed = hero.Speed.ToString();
                model.Powerstat.Durability = hero.Durability.ToString();
                model.Powerstat.Power = hero.Power.ToString();
                model.Powerstat.Combat = hero.Combat.ToString();

                return model;
            }
            catch (Exception)
            {

                throw;
            }


        }

        // GET: DetailedHeroView
        [HttpGet]
        public async Task<ActionResult> DetailedHero(string id)
        {
            var heroId = 0;

            try
            {
                #region Validation
                if (!int.TryParse(id, out heroId))
                {
                    return RedirectToAction("Index", "SearchView");
                }

                if (Session["userId"] == null)
                {
                    return RedirectToAction("Index", "Home");
                }

                if (heroId <= 0)
                {
                    return RedirectToAction("Index", "SearchView");
                }
                #endregion

                var hero = await ApiCall.GetHeroById(heroId);

                var model = new DetailedHeroViewModel();
                model.IsFavourite = false;

                var userId = (int)Session["userId"];
                var user = db.User.Include(u => u.FavouriteSuperHero).Where(u => u.Id == userId).FirstOrDefault();
                var userFavSuperHeroesIdList = user.FavouriteSuperHero.Select(h => h.ApiId).ToList();

                //to help check if the hero is already a favourite one
                //that the user actually viewing                          
                if (userFavSuperHeroesIdList.Contains(heroId))
                    model.IsFavourite = true;

                //mapping if we get back the hero from api
                model = Mapping(model, hero);

                return View(model);
            }
            catch (ApiNotFoundException)
            {
                //if api not working

                var model = new DetailedHeroViewModel();
                model.IsFavourite = false;

                var userId = (int)Session["userId"];      
                var user = db.User.Include(u => u.FavouriteSuperHero).Where(u => u.Id == userId).FirstOrDefault();
                var userFavSuperHeroesIdList = user.FavouriteSuperHero.Select(h => h.ApiId).ToList();

                //to help check if the hero is already a favourite one
                //that the user actually viewing                          
                if (userFavSuperHeroesIdList.Contains(heroId))
                    model.IsFavourite = true;

                var heroFromDb = db.FavouriteSuperHero.Where(h => h.ApiId == heroId).FirstOrDefault();
                model = Mapping(model, heroFromDb);
                return View(model);
            }
            catch (Exception)
            {

                throw;
            }


        }

        public async Task<ActionResult> AddToFavourite(string id)
        {
            var apiId = 0;

            try
            {

                #region Validtion
                if (id == null || Session["userId"] == null)
                    return RedirectToAction("Index", "Home");


                if (!int.TryParse(id, out apiId))
                    return RedirectToAction("Index", "Home");


                var userId = (int)Session["userId"];
                var user = db.User.Include(u => u.FavouriteSuperHero).Where(u => u.Id == userId).FirstOrDefault();
                var userFavSuperHeroesIdList = user.FavouriteSuperHero.Select(h => h.ApiId).ToList();

                //if this hero already in the user favourites
                if (userFavSuperHeroesIdList.Contains(apiId))
                    return RedirectToAction("Index", "Home");

                #endregion

                var hero = await ApiCall.GetHeroById(apiId);
                var model = new DetailedHeroViewModel();

                //if the hero is already in database
                //the program just add the hero to the user fav list
                //but won't save in the db
                var favHeroIdList = db.FavouriteSuperHero.Select(h => h.ApiId);
                if (favHeroIdList.Contains(apiId))
                {

                    var heroToSave = db.FavouriteSuperHero.Where(h => h.ApiId == apiId).FirstOrDefault();
                    using (DbContextTransaction tran = db.Database.BeginTransaction())
                    {
                        user.FavouriteSuperHero.Add(heroToSave);
                        db.Entry(user).State = EntityState.Modified;
                        db.SaveChanges();
                        tran.Commit();
                    }

                    //mapping back the model from api
                    model = new DetailedHeroViewModel();
                    model = Mapping(model, hero);
                    model.IsFavourite = true;

                    return View("DetailedHero", model);
                }

                //if the hero doesn't exist in the db yet
                //then we create a new one, add to the user
                //and save to the db
                var newFavouriteHero = new FavouriteSuperHero();

                //mapping for our db from api
                newFavouriteHero.ApiId = Convert.ToInt32(hero.ApiId);
                newFavouriteHero.Name = hero.Name;
                newFavouriteHero.RealName = hero.Biography.Full_Name;
                newFavouriteHero.ImgUrl = hero.Image.Url;
                newFavouriteHero.Intelligence = ApiCall.StatStringToInt(hero.Powerstats.Intelligence);
                newFavouriteHero.Strength = ApiCall.StatStringToInt(hero.Powerstats.Strength);
                newFavouriteHero.Speed = ApiCall.StatStringToInt(hero.Powerstats.Speed);
                newFavouriteHero.Durability = ApiCall.StatStringToInt(hero.Powerstats.Durability);
                newFavouriteHero.Power = ApiCall.StatStringToInt(hero.Powerstats.Power);
                newFavouriteHero.Combat = ApiCall.StatStringToInt(hero.Powerstats.Combat);
                newFavouriteHero.User.Add(user);

                //insert record to db
                using (DbContextTransaction tran = db.Database.BeginTransaction())
                {
                    db.FavouriteSuperHero.Add(newFavouriteHero);
                    db.SaveChanges();
                    tran.Commit();
                }

                //mapping back the model

                model = Mapping(model, hero);
                model.IsFavourite = true;

                return View("DetailedHero", model);
            }
            catch (ApiNotFoundException)
            {
                //if the hero is already in database
                //the program just add the hero to the user fav list
                //but won't save in the db
                var userId = (int)Session["userId"];
                var favHeroIdList = db.FavouriteSuperHero.Select(h => h.ApiId);
                if (favHeroIdList.Contains(apiId))
                {

                    var user = db.User.Where(u => u.Id == userId).FirstOrDefault();
                    var heroToSave = db.FavouriteSuperHero.Where(h => h.ApiId == apiId).FirstOrDefault();
                    using (DbContextTransaction tran = db.Database.BeginTransaction())
                    {
                        user.FavouriteSuperHero.Add(heroToSave);
                        db.Entry(user).State = EntityState.Modified;
                        db.SaveChanges();
                        tran.Commit();
                    }

                    //mapping back the model if we can't reach the api                       
                    var model = new DetailedHeroViewModel();
                    model = Mapping(model, heroToSave);
                    model.IsFavourite = true;

                    return View("DetailedHero", model);

                }

                throw new Exception("The api not available, and the hero is not in our db!");
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<ActionResult> RemoveFromFavourite(string id)
        {
            var apiId = 0;

            try
            {

                #region Validation
                if (id == null || Session["userId"] == null)
                {
                    return RedirectToAction("Index", "Home");
                }

                if (!int.TryParse(id, out apiId))
                {
                    return RedirectToAction("Index", "Home");
                }
                #endregion

                var userId = (int)Session["userId"];
                var user = db.User.Include(u => u.FavouriteSuperHero).Where(u => u.Id == userId).FirstOrDefault();
                var userFavouriteHero = user.FavouriteSuperHero.Where(h => h.ApiId == apiId).FirstOrDefault();

                //delete record from db
                using (DbContextTransaction tran = db.Database.BeginTransaction())
                {
                    user.FavouriteSuperHero.Remove(userFavouriteHero);
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    tran.Commit();
                }

                var hero = await ApiCall.GetHeroById(apiId);
                var model = new DetailedHeroViewModel();

                //mapping back the model from api               
                model = Mapping(model, hero);
                model.IsFavourite = false;

                return View("DetailedHero", model);
            }
            catch (ApiNotFoundException)
            {
                var model = new DetailedHeroViewModel();
                var heroFromDb = db.FavouriteSuperHero.Where(h => h.ApiId == apiId).FirstOrDefault();
                model = Mapping(model, heroFromDb);
                model.IsFavourite = false;
                return View("DetailedHero", model);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}