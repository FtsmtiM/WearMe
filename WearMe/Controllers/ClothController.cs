using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WearMe.Data.Interfaces;
using WearMe.Data.Models;
using WearMe.ViewModels;

namespace WearMe.Controllers
{
    public class ClothController: Controller
    {
        private readonly IClothRepository _clothRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISexRepository _sexRepository;
        private int _currentSex { get; set; }
        public void SetcurrentSex(int Id)
        {
            this._currentSex = Id;
        }
        public int getcurrentSex()
        {
            return _currentSex;
        }

        public ClothController(IClothRepository clothRepository, ICategoryRepository categoryRepository, ISexRepository sexRepository)
        {
            _clothRepository = clothRepository;
            _categoryRepository = categoryRepository;
            _sexRepository = sexRepository;
        }

        public ViewResult List(int sexid, int categoryid)
        {
            //if(sexid == 0){
            //    _currentSex = 1;
            //     }
            HttpContext.Session.SetInt32("sex", sexid);
            HttpContext.Session.SetInt32("category", categoryid);
            int _sexId = sexid;
            int _catagoryId = categoryid;
            int currentSexId = 0;
            int currentCatagoryId = 0;
            IEnumerable<Cloth> clothsList;
            if(_sexId == 0 && _catagoryId == 0)
            {
                clothsList = _clothRepository.Clothes;
                currentSexId = 0;
                currentCatagoryId = 0;
            }
            else if(_sexId == 0)
            {
                clothsList = _clothRepository.Clothes.Where(c => c.SexCategory.CategoryId.Equals(_catagoryId));
                currentCatagoryId = _catagoryId;
                currentSexId = 0;
            }
            else if (_catagoryId == 0)
            {
                clothsList = _clothRepository.Clothes.Where(c => c.SexCategory.SexId.Equals(_sexId));
                currentSexId = _sexId;
                currentCatagoryId = 0;
            }
            else
            {
                clothsList = _clothRepository.Clothes.Where(c=>c.SexCategory.SexId.Equals(_sexId) && c.SexCategory.CategoryId.Equals(_catagoryId));
                currentSexId = _sexId;
                currentCatagoryId = _catagoryId;
            }
            TempData["currentSex"] = currentSexId;
            TempData["currentCategory"] = currentCatagoryId;
            return View (new ClothsListViewModel
            {Cloths= clothsList,
            CurrentSex=currentSexId,
            CurrentCatagory = currentCatagoryId
            });
        }
        [Authorize]
        public ViewResult Details(int id)
        {
            ////TempData["currentSex"] = "0";
            ////TempData["currentCategory"] = "0";
            ViewBag.session = HttpContext.Session.GetInt32("sex");
            ClothDetailsViewModel clothDetailsViewModel = new ClothDetailsViewModel()
            {
                Cloth = _clothRepository.GetClothbyId(id),
                PageTitle = "Cloth Detailes"

            };
            //Employee model = _employeeRepositorty.GetEmployee(1);
            return View(clothDetailsViewModel);
        }

    }
}
