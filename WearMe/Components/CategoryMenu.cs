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
    public class CategoryMenu: ViewComponent
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryMenu(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IViewComponentResult Invoke()
        {
            //if (TempData["currentSex"] == null) { TempData["currentSex"] = "0"; }
            //var sexId = int.Parse(TempData["currentSex"].ToString());
            int _currentSex = (HttpContext.Session.GetInt32("sex").HasValue)
                 ? (HttpContext.Session.GetInt32("sex").Value) : 0;
            int _currentCat = (HttpContext.Session.GetInt32("category").HasValue)
                ? (HttpContext.Session.GetInt32("category").Value) : 0;

            CategoryMenuViewModel categoryMenuView = new CategoryMenuViewModel
            {
                 CurrentCategory = _currentCat,
                CurrentSex = _currentSex,
                CategoriesSex = _categoryRepository.CategoriesbySex(_currentSex)
            };
            return View(categoryMenuView);
        }
    }
}
