using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WearMe.Data.Interfaces;
using WearMe.ViewModels;

namespace WearMe.Components
{
    public class SexMenu: ViewComponent
    {
        private readonly ISexRepository _sexRepository;

        public SexMenu(ISexRepository sexRepository)
        {
            _sexRepository = sexRepository;
        }

        public IViewComponentResult Invoke()
        {
            //if (TempData["currentCategory"] == null) { TempData["currentCategory"] = "0"; }
            //if (TempData["currentSex"] == null) { TempData["currentSex"] = "0"; }
            int _currentSex = (HttpContext.Session.GetInt32("sex").HasValue) 
                ?  (HttpContext.Session.GetInt32("sex").Value) : 0;
            int _currentCat = (HttpContext.Session.GetInt32("category").HasValue)
                ? (HttpContext.Session.GetInt32("category").Value) : 0;


            SexMenuViewModel sexMenuView = new SexMenuViewModel
            {
                Sexs = _sexRepository.SexGroups,
                CurrentCategory = _currentCat,
                CurrentSex = _currentSex
            };
            
            return View(sexMenuView);
        }
    }
}
