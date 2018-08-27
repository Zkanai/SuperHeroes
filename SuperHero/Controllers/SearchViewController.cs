using System.Threading.Tasks;
using System.Web.Mvc;
using SuperHero.ManageApi;
using SuperHero.Models;
using System;
using SuperHero.Models.JsonFromJqueryModels;

namespace SuperHero.Controllers
{
    public class SearchViewController : Controller
    {
       
        // GET: SearchView
        [HttpGet]
        public ActionResult Index()
        {
            var model = new SearchViewModel();
            return View(model);
        }

        //POST: Search
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(SearchViewModel model)
        {
            try
            {
                var searchString = model.Filter.Name;
                var heroesList = await ApiCall.GetHeroesByName(searchString);

                //if there's no hero with that name
                if (heroesList?.response == "error")
                {
                    ModelState.AddModelError("Filter.Name", "There is no superhero with that name!");
                }

                if (!ModelState.IsValid)
                {
                    model = new SearchViewModel();
                    return View(model);
                }

                model.SearchResult = heroesList.results;

                return View(model);

            }
            catch (ApiNotFoundException)
            {
                ModelState.AddModelError("Filter.Name", "The service is temporarily unavailable!");

                model = new SearchViewModel();
                return View(model);

            }
            catch (Exception)
            {

                throw;
            }

        }

        //POST: FROM JQUERY AJAX
        //when user want to check hero details
        //but not logged in yet
        //so store the hero in session, to show after login
        [HttpPost] 
        public ActionResult HeroToLogin(DetailHeroToLogin data)
        {

            Session["heroToShowId"] = data.HeroId;

            return Json(data);
        }


    }
}