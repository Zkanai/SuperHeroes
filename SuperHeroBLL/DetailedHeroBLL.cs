﻿using SuperHero;
using SuperHero.Models;
using SuperHeroDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHeroBLL
{
    public class DetailedHeroBLL
    {

        protected AspNetUsersObj objUser;

        public DetailedHeroBLL()
        {
            objUser = new AspNetUsersObj();
        }

        /// <summary>
        /// create a new model, and
        /// set isfavourite property false for help
        /// check if this hero is already favourite for the given user
        /// </summary>
        /// <returns></returns>
        public DetailedHeroViewModel CreateNewDetailedHeroViewModel()
        {
            var model = new DetailedHeroViewModel();
            model.IsFavourite = false;

            return model;
        }

        public AspNetUsers GetUserById(string id)
        {
            var user = objUser.GetUserById(id);
            return user;
        }

    }
}
