using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuperHero;
using SuperHero.Controllers;
using SuperHero.Models;

namespace SuperHero.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod(), Description("When first load the website")]
        public void IndexFirst()
        {
            // Arrange
            HomeController controller = new HomeController();
            var exceptedHeroNumber = 0;

            // Act
            ViewResult result = controller.Index() as ViewResult;
            var viewModel = result.Model as List<FavouriteSuperHeroViewModel>;
            var resultHeroNumber = viewModel.Count;
            // Assert
            Assert.AreEqual(exceptedHeroNumber, resultHeroNumber);
            Assert.IsNotNull(result);
        }

        [TestMethod(), Description("When a user log in")]
        public void Index_WhenLoggedIn()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act //how to simulate session?
            var viewResult = controller.Index() as ViewResult;

            //// Assert
            //Assert.IsNotNull(result);
        }
    }
}
