using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using WearMe.Data.Interfaces;
using WearMe.Data.Models;
using WearMe.ViewModels;

namespace WearMe.Data.Repositories
{
    public class OrderRepository: IOrderRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly ShoppingCart _shoppingCart;
        
        public OrderRepository(AppDbContext appDbContext, ShoppingCart shoppingcart)
        {
            _appDbContext = appDbContext;
            _shoppingCart = shoppingcart;
        }

        public int CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;

            _appDbContext.Orders.Add(order);

            var shoppingcartItems = _shoppingCart.ShoppingCartItems;

            foreach(var shoppingcartItem in shoppingcartItems)
            {
                var orderdetails = new OrderDetail
                {
                    ClothId = shoppingcartItem.Cloth.ClothId,
                    OrderId = order.OrderId,
                    Amount = shoppingcartItem.Amount,
                    Price = shoppingcartItem.Cloth.Price
                };
                _appDbContext.OrderDetails.Add(orderdetails);
            }
            _appDbContext.SaveChanges();
            return _appDbContext.Orders.Select(o => o.OrderId).Last();
        }

        public Order EditOrder(EditOrderViewModel order)
        {
            var orderDb = _appDbContext.Orders.Find(order.OrderId);
            orderDb.LastName = order.LastName;
            orderDb.FirstName = order.FirstName;
            orderDb.AddressLine1 = order.AddressLine1;
            orderDb.AddressLine2 = order.AddressLine2;
            orderDb.ZipCode = order.ZipCode;
            orderDb.City = order.City;
            orderDb.State = order.State;
            orderDb.Country = order.Country;
            orderDb.Email = order.Email;
            orderDb.PhoneNumber = order.PhoneNumber;

            _appDbContext.SaveChanges();
            return orderDb;
        }
        public List<OrderDetail> GetOrderDetails()
        {
            var order = _appDbContext.Orders.Last();
            var orderDetails = _appDbContext.OrderDetails.Where(or=>or.OrderId == order.OrderId).Include(o=>o.Cloth).ToList();
                                         
       
            return orderDetails;
        }

        public Order GetOrder(int? id=null)
        {
            var order = new Order();
            if (id != null)
            {
                 order = _appDbContext.Orders.Include(o => o.OrderLines).FirstOrDefault(o => o.OrderId == id);

            }
            else
            {
                order = _appDbContext.Orders.Include(o => o.OrderLines).Last();

            }

            return order;
        }

        public void DeleteOrder(int OrderId)
        {
            var orderItems = _appDbContext.OrderDetails
                .Where(o => o.OrderId == OrderId);
            var order = _appDbContext.Orders.Where(or => or.OrderId == OrderId);
            _appDbContext.OrderDetails.RemoveRange(orderItems);
            _appDbContext.Orders.RemoveRange(order);
            _appDbContext.SaveChanges();
        }

    }
}
