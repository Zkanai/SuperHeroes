using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SuperHero.Models
{
    public class DetailedHeroViewModel
    {

        public string ApiId { get; set; }
        public string Name { get; set; }
        [Display(Name ="PowerStats")]
        public Powerstats Powerstat { get; set; }
        [Display(Name = "Biography")]
        public Biography BiographyData { get; set; }
        public Appearance AppearanceValues { get; set; }
        public Work WorkData { get; set; }
        public Connections Connection { get; set; }
        public string ImageUrl { get; set; }
        public bool IsFavourite { get; set; }

        public DetailedHeroViewModel()
        {
            Powerstat = new Powerstats();
            BiographyData = new Biography();
            AppearanceValues = new Appearance();
            WorkData = new Work();
            Connection = new Connections();
        }


        public class Connections
        {
            public string Group_Affiliation { get; set; }
            public string Relatives { get; set; }
        }

        public class Work
        {
            public string Occupation { get; set; }
            public string @base { get; set; }
        }

        public class Appearance
        {
            public string Gender { get; set; }
            public string Race { get; set; }
            public List<string> Height { get; set; }
            public List<string> Weight { get; set; }
            public string Eye_Color { get; set; }
            public string Hair_Color { get; set; }
        }

        public class Biography
        {
            [Display(Name = "Real-name")]
            public string Full_Name { get; set; }
            public string Alter_Egos { get; set; }
            public List<string> Aliases { get; set; }
            [Display(Name = "Birthplace")]
            public string Place_Of_Birth { get; set; }
            public string First_Appearance { get; set; }
            public string Publisher { get; set; }
            public string Alignment { get; set; }
        }

        public class Powerstats
        {
            public string Intelligence { get; set; }
            public string Strength { get; set; }
            public string Speed { get; set; }
            public string Durability { get; set; }
            public string Power { get; set; }
            public string Combat { get; set; }
        }
    }
}