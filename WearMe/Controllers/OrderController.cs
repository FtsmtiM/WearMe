using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WearMe.Data.Interfaces;
using WearMe.Data.Models;
using WearMe.ViewModels;

namespace WearMe.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ShoppingCart _shoppingcart;


        public OrderController(IOrderRepository orderRepository, ShoppingCart shoppingCart)
        {
            _orderRepository = orderRepository;
            _shoppingcart = shoppingCart;
        }

        public IActionResult CheckOut()
        {
            return View();
        }

        [HttpPost]
        public  IActionResult CheckOut(Order order)
        {
            var shoppingitems = _shoppingcart.GetShoppingCartItems();

          
            if (shoppingitems.Count() == 0)
            {
                ModelState.AddModelError("", "Order can not be processed, your cart is empty. ");

            }
            if (ModelState.IsValid)
            {
                int OrderId = _orderRepository.CreateOrder(order);
                _shoppingcart.ClearCart();
                //return RedirectToAction("OrderCompleted", new RouteValueDictionary(newOrder));
                return RedirectToAction("OrderCompleted", new { OrderId });

            }

            return View(order);
        }
        [HttpGet]
        public IActionResult CheckOutEdit(int id)
        {
            var order = _orderRepository.GetOrder(id);
            var orderDetails = _orderRepository.GetOrderDetails();
            EditOrderViewModel orderView = new EditOrderViewModel()
            {
                OrderId = id,
                FirstName = order.FirstName,
                LastName = order.LastName,
                AddressLine1 = order.AddressLine1,
                AddressLine2=order.AddressLine2,
                ZipCode = order.ZipCode,
                City = order.City,
                State=order.State,
                Country= order.Country,
                PhoneNumber = order.PhoneNumber,
                Email = order.Email,
                //OrderLines = orderDetails


            };
            return View(orderView);
        }

        [HttpPost]
        public IActionResult CheckOutEdit(EditOrderViewModel order)
        {

            if (ModelState.IsValid)
            {
                Order newOrder = _orderRepository.EditOrder(order);
                //return RedirectToAction("OrderCompleted", new RouteValueDictionary(newOrder));
                return RedirectToAction("OrderCompleted", new { newOrder.OrderId });

            }

            return View(order);
        }
        [HttpGet]
        public IActionResult OrderCompleted(int OrderId)
        {

            ViewBag.ThankyouMessage = "Thankyou, hope you had a nice time on our page. We look forward to see you soon.";
            var newOrder = _orderRepository.GetOrder(OrderId);
            var orderDetails = _orderRepository.GetOrderDetails();
            Order orderView = new Order()
            {
                OrderId = OrderId,
                FirstName = newOrder.FirstName,
                LastName = newOrder.LastName,
                AddressLine1= newOrder.AddressLine1,
                ZipCode= newOrder.ZipCode,
                City= newOrder.City,
                Email= newOrder.Email,
                OrderLines = orderDetails
                
                
            };
            return View(orderView);           

        }
        
        public IActionResult OrderProcessed()
        {
            ViewBag.ThankyouMessage = "Thankyou, hope you had a nice time on our page. We look forward to see you soon.";
            return View();
        }

        public IActionResult RemoveOrder(int id)
        {
            _orderRepository.DeleteOrder(id);
            return RedirectToAction("Index","Home");
        }
    }
}
