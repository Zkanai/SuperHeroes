using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SuperHero.Models
{
    public class SearchViewModel
    {
        public List<ApiModels.SuperHeroByName.Result> SearchResult { get; set; }
        public HeroSearchFilter Filter { get; set; }
        public DefaultRow DefaultRow { get; set; }

        public SearchViewModel()
        {
            SearchResult = new List<ApiModels.SuperHeroByName.Result>();
            Filter = new HeroSearchFilter();
        }
        
    }

    public class HeroSearchFilter
    {
        [MinLength(2,ErrorMessage ="You have to give two characters min!")]
        public string Name { get; set; }
    }

    
    public class DefaultRow
    {
        public int ApiId { get; set; }
        public string Name { get; set; }
        [Display(Name = "Real-name")]
        public string RealName { get; set; }
        public string Universe { get; set; }
    }
}