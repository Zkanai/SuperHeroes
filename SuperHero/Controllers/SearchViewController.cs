using System.Threading.Tasks;
using System.Web.Mvc;
using SuperHero.ManageApi;
using SuperHero.Models;
using SuperHero.Models.ApiModels;
using System;

namespace SuperHero.Controllers
{
    public class SearchViewController : Controller
    {
        SuperHeroDBEntities db = new SuperHeroDBEntities();

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
    }
}