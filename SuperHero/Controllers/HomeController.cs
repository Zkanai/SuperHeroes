using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SuperHero.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

namespace SuperHero.Controllers
{
    public class HomeController : BaseController
    {
        //SuperHeroDBEntities db = new SuperHeroDBEntities();

        //Get
        [HttpGet]
        public ActionResult Index()
        {
            var model = new List<FavouriteSuperHeroViewModel>();

            try
            {

                var userId = User.Identity.GetUserId();
                var user = objBs.AspNetUserBLL.GetUserIncludeFavouriteHeroesById(userId);

                if (user == null)
                {
                    return View(model);
                }

                var userFavSuperHeroList = user.FavouriteSuperHero.ToList();
                            
                return View(objBs.HomeBLL.MappingFavouriteSuperHeroes(userId, userFavSuperHeroList));
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}