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
    public class ShoppingCartController:Controller
    {
        private readonly IClothRepository _clothRepository;
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartController(IClothRepository clothRepository, ShoppingCart shoppingCart)
        {
            _clothRepository = clothRepository;
            _shoppingCart = shoppingCart;
        }

        public ViewResult Index()
        {
            ViewBag.session = HttpContext.Session.GetInt32("sex");
            var _shoppingcartItem = _shoppingCart.GetShoppingCartItems();

            ShoppingCartViewModel shoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            return View(shoppingCartViewModel);
            
        }

        public RedirectToActionResult AddtoShoppingCart(int id)
        {
            var cloth = _clothRepository.Clothes.FirstOrDefault(c => c.ClothId == id);

            if(cloth != null)
            {
                _shoppingCart.AddToCart(cloth, 1);
            }

            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoveFromShoppingCart(int clothId)
        {
            var selectedCloth = _clothRepository.Clothes.FirstOrDefault(p => p.ClothId == clothId);
            if (selectedCloth != null)
            {
                _shoppingCart.RemoveFromCart(selectedCloth);
            }
            return RedirectToAction("Index");
        }
    }
}
