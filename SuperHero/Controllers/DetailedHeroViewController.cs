﻿using Microsoft.AspNet.Identity;
using SuperHero.ManageApi;
using SuperHero.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;



namespace SuperHero.Controllers
{
    [Authorize]
    public class DetailedHeroViewController : BaseController
    {
      
        // GET: DetailedHeroView
        [HttpGet]
        public async Task<ActionResult> DetailedHero(string id)
        {
            var heroId = 0;
            var userId = User.Identity.GetUserId();
            
            try
            {
                #region Validation
                if (!int.TryParse(id, out heroId))
                    return RedirectToAction("Index", "SearchView");

                if (heroId <= 0)
                    return RedirectToAction("Index", "SearchView");
                #endregion

                var hero = await ApiCall.GetHeroById(heroId);
                var model = new DetailedHeroViewModel();
                model.IsFavourite = false;
                var userFavSuperHeroesIdList = objBs.FavouriteSuperHeroBLL.GetUserFavouriteHeroIdList(userId);
                

                //to help check if the hero is already a favourite one
                //that the user actually viewing                          
                if (userFavSuperHeroesIdList.Contains(heroId))
                    model.IsFavourite = true;

                //mapping if we get back the hero from api
                model = objBs.detailedHeroBLL.MappingFromApi(model, hero);

                return View(model);



            }
            catch (ApiNotFoundException)
            {
                //if api not working

                var model = new DetailedHeroViewModel();
                model.IsFavourite = false;
                var userFavSuperHeroesIdList = objBs.FavouriteSuperHeroBLL.GetUserFavouriteHeroIdList(userId);

                //to help check if the hero is already a favourite one
                //that the user actually viewing                          
                if (userFavSuperHeroesIdList.Contains(heroId))
                    model.IsFavourite = true;

                var heroFromDb = objBs.FavouriteSuperHeroBLL
                    .GetFavouriteHeroById(heroId);
                model = objBs.detailedHeroBLL.MappingWhenApiNA(model, heroFromDb);

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
            var userId = User.Identity.GetUserId();
            
            try
            {

                #region Validtion
                if (id == null)
                    return RedirectToAction("Index", "Home");


                if (!int.TryParse(id, out apiId))
                    return RedirectToAction("Index", "Home");
              
                var userFavSuperHeroesIdList = objBs.FavouriteSuperHeroBLL.GetUserFavouriteHeroIdList(userId);

                //if this hero already in the user favourites
                if (userFavSuperHeroesIdList.Contains(apiId))
                    return RedirectToAction("Index", "Home");

                #endregion

                var hero = await ApiCall.GetHeroById(apiId);
                var model = new DetailedHeroViewModel();

                //if the hero is already in database
                //the program just add the hero to the user fav list
                //but won't save in the favouritehero table
                var favHeroIdList = objBs.FavouriteSuperHeroBLL.GetFavouriteHeroIdList();
                if (favHeroIdList.Contains(apiId))
                {
                   
                    objBs.detailedHeroBLL.SaveHeroToUserFavHeroList(apiId, userId);

                    //mapping back the model from api
                    model = new DetailedHeroViewModel();
                    model = objBs.detailedHeroBLL.MappingFromApi(model, hero);
                    model.IsFavourite = true;

                    return View("DetailedHero", model);
                }

                //if the hero doesn't exist in the db yet
                //then we create a new one, add to the user
                //and save to the db
                var newFavouriteHero = objBs.detailedHeroBLL.MappingNewFavouriteHero(hero);
                objBs.detailedHeroBLL.SaveHeroToDb(newFavouriteHero, userId);

                //mapping back the model
                model = objBs.detailedHeroBLL.MappingFromApi(model, hero);
                model.IsFavourite = true;

                return View("DetailedHero", model);
            }
            catch (ApiNotFoundException)
            {
                //if the hero is already in database
                //the program just add the hero to the user fav list
                //but won't save in the favouritehero table            
                var favHeroIdList = objBs.FavouriteSuperHeroBLL.GetFavouriteHeroIdList();
                if (favHeroIdList.Contains(apiId))
                {

                    var heroToSave = objBs.FavouriteSuperHeroBLL.GetFavouriteHeroById(apiId);
                    objBs.detailedHeroBLL.SaveHeroToUserFavHeroList(apiId, userId);

                    //mapping back the model if we can't reach the api                       
                    var model = new DetailedHeroViewModel();
                    model = objBs.detailedHeroBLL.MappingWhenApiNA(model, heroToSave);
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
            var userId = User.Identity.GetUserId();
           
            try
            {

                #region Validation
                if (id == null)
                    return RedirectToAction("Index", "Home");

                if (!int.TryParse(id, out apiId))
                    return RedirectToAction("Index", "Home");

                #endregion
              
                //delete record from db
                objBs.detailedHeroBLL.RemoveHeroFromUserFavouriteList(userId, apiId); //KI KELL E TÖRÖLNI A BATTLELOG TÁBLÁBÓL IS???

                var hero = await ApiCall.GetHeroById(apiId);
                var model = new DetailedHeroViewModel();

                //mapping back the model from api               
                model = objBs.detailedHeroBLL.MappingFromApi(model, hero);
                model.IsFavourite = false;

                return View("DetailedHero", model);
            }
            catch (ApiNotFoundException)
            {
                var model = new DetailedHeroViewModel();
                var heroFromDb = objBs.FavouriteSuperHeroBLL.GetFavouriteHeroById(apiId);
                model = objBs.detailedHeroBLL.MappingWhenApiNA(model, heroFromDb);
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