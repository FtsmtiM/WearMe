using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WearMe.Data.Models;
using WearMe.ViewModels;

namespace WearMe.Components
{
    public class ShoppingCartMenu:ViewComponent
    {
        private readonly ShoppingCart _shoppingCart;
        public ShoppingCartMenu(ShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        public IViewComponentResult Invoke(string viewname = null)
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };

            if (!string.IsNullOrEmpty(viewname))
            {
                return View(viewname, shoppingCartViewModel);
            }
            return View(shoppingCartViewModel);
        }
    }
}
