using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WearMe.Data.Interfaces;
using WearMe.Data.Models;
using WearMe.ViewModels;

namespace WearMe.Controllers
{
    public class HomeController : Controller
    {
        private readonly IClothRepository _clothRepository;

        public HomeController(IClothRepository clothRepository)
        {
            _clothRepository = clothRepository;
        }
        public IActionResult Index()
        {
            HttpContext.Session.Clear();
            //TempData["currentSex"] = "0";
            //TempData["currentCategory"] = "0";
            HomeViewModel homeViewModel = new HomeViewModel
            {
                Trending = _clothRepository.Trending
            };
            return View(homeViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Index1()
        {
            return View();
        }

    }
}
