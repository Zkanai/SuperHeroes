using Microsoft.AspNet.Identity;
using SuperHero.ManageApi;
using SuperHero.Models;
using SuperHeroBLL;
using SuperHeroBLL.Mapping;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;



namespace SuperHero.Controllers
{
    [Authorize]
    public class DetailedHeroViewController : Controller
    {

        SuperHeroDBEntities db = new SuperHeroDBEntities(); //INNEN KELL MAJD FOLYTATNI
        protected DetailedHeroBLL objBs;

        public DetailedHeroViewController()
        {
            objBs = new DetailedHeroBLL();
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
                    return RedirectToAction("Index", "SearchView");
                
                if (heroId <= 0)             
                    return RedirectToAction("Index", "SearchView");              
                #endregion

                var hero = await ApiCall.GetHeroById(heroId);

                var model = objBs.CreateNewDetailedHeroViewModel();

                var userId = User.Identity.GetUserId();
                var user = objBs.GetUserById(userId);
                var userFavSuperHeroesIdList = user.FavouriteSuperHero.Select(h => h.ApiId).ToList();

                //to help check if the hero is already a favourite one
                //that the user actually viewing                          
                if (userFavSuperHeroesIdList.Contains(heroId))
                    model.IsFavourite = true;

                //mapping if we get back the hero from api
                model = DetailedHeroMapping.Mapping(model, hero);

                return View(model);
            }
            catch (ApiNotFoundException)
            {
                //if api not working

                var model = new DetailedHeroViewModel();
                model.IsFavourite = false;

                var userId = User.Identity.GetUserId();
                var user = db.AspNetUsers.Include(u => u.FavouriteSuperHero).Where(u => u.Id == userId).FirstOrDefault();
                var userFavSuperHeroesIdList = user.FavouriteSuperHero.Select(h => h.ApiId).ToList();

                //to help check if the hero is already a favourite one
                //that the user actually viewing                          
                if (userFavSuperHeroesIdList.Contains(heroId))
                    model.IsFavourite = true;

                var heroFromDb = db.FavouriteSuperHero.Where(h => h.ApiId == heroId).FirstOrDefault();
                model = DetailedHeroMapping.Mapping(model, heroFromDb);
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
                if (id == null)                    
                    return RedirectToAction("Index", "Home");


                if (!int.TryParse(id, out apiId))
                    return RedirectToAction("Index", "Home");


                var userId = User.Identity.GetUserId();
                var user = db.AspNetUsers.Include(u => u.FavouriteSuperHero).Where(u => u.Id == userId).FirstOrDefault();
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
                    model = DetailedHeroMapping.Mapping(model, hero);
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
                newFavouriteHero.AspNetUsers.Add(user);

                //insert record to db
                using (DbContextTransaction tran = db.Database.BeginTransaction())
                {
                    db.FavouriteSuperHero.Add(newFavouriteHero);
                    db.SaveChanges();
                    tran.Commit();
                }

                //mapping back the model

                model = DetailedHeroMapping.Mapping(model, hero);
                model.IsFavourite = true;

                return View("DetailedHero", model);
            }
            catch (ApiNotFoundException)
            {
                //if the hero is already in database
                //the program just add the hero to the user fav list
                //but won't save in the db
                var userId = User.Identity.GetUserId();
                var favHeroIdList = db.FavouriteSuperHero.Select(h => h.ApiId);
                if (favHeroIdList.Contains(apiId))
                {

                    var user = db.AspNetUsers.Where(u => u.Id == userId).FirstOrDefault();
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
                    model = DetailedHeroMapping.Mapping(model, heroToSave);
                    model.IsFavourite = true;

                    return View("DetailedHero", model);

                }

                throw new Exception("The api not available, and the hero is not in our db!");
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<ActionResult> RemoveFromFavourite(string id)
        {
            var apiId = 0;

            try
            {

                #region Validation
                if (id == null)                    
                    return RedirectToAction("Index", "Home");

                if (!int.TryParse(id, out apiId))               
                    return RedirectToAction("Index", "Home");
                
                #endregion

                var userId = User.Identity.GetUserId();
                var user = db.AspNetUsers.Include(u => u.FavouriteSuperHero).Where(u => u.Id == userId).FirstOrDefault();
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
                model = DetailedHeroMapping.Mapping(model, hero);
                model.IsFavourite = false;

                return View("DetailedHero", model);
            }
            catch (ApiNotFoundException)
            {
                var model = new DetailedHeroViewModel();
                var heroFromDb = db.FavouriteSuperHero.Where(h => h.ApiId == apiId).FirstOrDefault();
                model = DetailedHeroMapping.Mapping(model, heroFromDb);
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