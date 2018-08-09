using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace SuperHero.Models.ApiModels
{
    /// <summary>
    /// helps to model the object that we get back from
    /// api in json when searching by id
    /// </summary>
    public class SuperHeroById
    {
            
        public class HeroById
        {
            public string Response { get; set; }
            [JsonProperty(PropertyName = "id")]
            public string ApiId { get; set; }
            public string Name { get; set; }
            public Powerstats Powerstats { get; set; }
            public Biography Biography { get; set; }
            public Appearance Appearance { get; set; }
            public Work Work { get; set; }
            public Connections Connections { get; set; }
            public Image Image { get; set; }
        }

        public class Image
        {
            public string Url { get; set; }
        }

        public class Connections
        {
            [JsonProperty(PropertyName = "group-affiliation")]
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
            [JsonProperty(PropertyName = "eye-color")]
            public string Eye_Color { get; set; }
            [JsonProperty(PropertyName = "hair-color")]
            public string Hair_Color { get; set; }
        }

        public class Biography
        {
            [JsonProperty(PropertyName = "full-name")]
            public string Full_Name { get; set; }
            [JsonProperty(PropertyName = "alter-egos")]
            public string Alter_Egos { get; set; }
            public List<string> Aliases { get; set; }
            [JsonProperty(PropertyName = "place-of-birth")]
            public string Place_Of_Birth { get; set; }
            [JsonProperty(PropertyName = "first-appearance")]
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