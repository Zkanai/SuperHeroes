using Newtonsoft.Json;
using SuperHero.Models.ApiModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace SuperHero.ManageApi
{

    /// <summary>
    /// this class helps to manage to get datas from the api //just a test
    /// </summary>
    public static class ApiCall
    {
        
        /// <summary>
        /// it returns a hero from another db through api 
        /// searching by id
        /// </summary>
        /// <returns></returns>
        public static async Task<SuperHeroById.HeroById> GetHeroById(int id)
        {
            try
            {
                var baseAddress = new Uri(ConfigurationManager.AppSettings["apiUrl"].ToString());
                using (var httpClient = new HttpClient { BaseAddress = baseAddress })
                {

                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("accept", "application/json");
                    using (var response = await httpClient.GetAsync($"{id}"))
                    {

                        var hero = new SuperHeroById.HeroById();
                        string jsonFile = await response.Content.ReadAsStringAsync();

                        hero = JsonConvert.DeserializeObject<SuperHeroById.HeroById>(jsonFile);

                        return hero;

                    }

                }
            }
            catch (Exception)
            {

                throw new ApiNotFoundException("Api temporarily unavailable!");
            }
               
            
          
            
        }
    
        /// <summary>
        /// it's get's the heroes similar or
        /// same name that the user search for
        /// </summary>
        /// <returns></returns>
        public static async Task<SuperHeroByName.HeroByName> GetHeroesByName(string name)
        {
            try
            {
                var baseAddress = new Uri(ConfigurationManager.AppSettings["apiUrl"].ToString());
                using (var httpClient = new HttpClient { BaseAddress = baseAddress })
                {

                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("accept", "application/json");
                    using (var response = await httpClient.GetAsync($"search/{name}"))
                    {

                        var heroList = new SuperHeroByName.HeroByName();
                        string jsonFile = await response.Content.ReadAsStringAsync();

                        heroList = JsonConvert.DeserializeObject<SuperHeroByName.HeroByName>(jsonFile);

                        return heroList;

                    }
                }
            }
            catch (Exception)
            {
                throw new ApiNotFoundException("The api temporarily unavaiable!");
            }
                                
        }

        /// <summary>
        /// helps to convert the heroes stats, cause we get them in string from the api
        /// </summary>
        /// <param name="herostat"></param>
        /// <returns></returns>
        public static int StatStringToInt(string herostat)
        {
            int stat = 0;

            int.TryParse(herostat, out stat);

            return stat;
        }

        
    }
}