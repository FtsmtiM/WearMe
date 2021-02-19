using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WearMe.Data.Models;
using WearMe.ViewModels;

namespace WearMe.Data.Interfaces
{
    public interface IOrderRepository
    {
        int CreateOrder(Order order);
        List<OrderDetail> GetOrderDetails();

        Order GetOrder(int? id = null);

        Order EditOrder(EditOrderViewModel order);
        void DeleteOrder(int OrderId);
    }
}
