using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WearMe.Data.Models
{
    public class ShoppingCart
    {
        private readonly AppDbContext _appDbContext;
        private ShoppingCart(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;

            var context = services.GetService<AppDbContext>();
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        public void AddToCart(Cloth cloth, int amount)
        {
            var _shoppingcartitem = _appDbContext.ShoppingCartItems.SingleOrDefault
                (s => s.Cloth.ClothId == cloth.ClothId && s.ShoppingCartId == ShoppingCartId);

            if(_shoppingcartitem == null)
            {
                _shoppingcartitem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Cloth = cloth,
                    Amount = amount
                };
                _appDbContext.ShoppingCartItems.Add(_shoppingcartitem);
            }
            else
            {
                _shoppingcartitem.Amount = _shoppingcartitem.Amount + amount;
            }
            _appDbContext.SaveChanges();
            
        }
         public List<ShoppingCartItem> GetShoppingCartItems()
        {
            //var _shoppingCartItems = _appDbContext.ShoppingCartItems.Where
            //    (s => s.ShoppingCartId == ShoppingCartId).Include(s => s.Cloth).ToList();

            return ShoppingCartItems ?? (ShoppingCartItems = _appDbContext.ShoppingCartItems.Where
                (s => s.ShoppingCartId == ShoppingCartId).Include(s => s.Cloth).ToList());
        }

        public decimal GetShoppingCartTotal()
        {
            var total= _appDbContext.ShoppingCartItems.Where(s => s.ShoppingCartId == ShoppingCartId)
                .Select(s => s.Cloth.Price * s.Amount).Sum();

            return total;
        }

        public int RemoveFromCart(Cloth cloth)
        {
            var shoppingCartItem =
                    _appDbContext.ShoppingCartItems.SingleOrDefault(
                        s => s.Cloth.ClothId == cloth.ClothId && s.ShoppingCartId == ShoppingCartId);

            var localAmount = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    _appDbContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            _appDbContext.SaveChanges();

            return localAmount;
        }

        public void ClearCart()
        {
            var cartItems = _appDbContext
                .ShoppingCartItems
                .Where(cart => cart.ShoppingCartId == ShoppingCartId);

            _appDbContext.ShoppingCartItems.RemoveRange(cartItems);

            _appDbContext.SaveChanges();
        }
    }
}
