using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SuperHero.Models
{
    /// <summary>
    /// this helps to display the user favourite superheroes
    /// what saved by her/him
    /// after he/she logged in
    /// </summary>
    public class FavouriteSuperHeroViewModel
    {        
        public int Id { get; set; }
        public string ApiId { get; set; }
        public string Name { get; set; }

        [Display(Name = "Real-name")]
        public string RealName { get; set; }      
        public string ImgUrl { get; set; }
        public int Intelligence { get; set; }
        public int Strength { get; set; }
        public int Speed { get; set; }
        public int Durability { get; set; }
        public int Power { get; set; }
        public int Combat { get; set; }
        public int Win { get; set; }
        public int Loose { get; set; }
        public int Draw { get; set; }
    }
}