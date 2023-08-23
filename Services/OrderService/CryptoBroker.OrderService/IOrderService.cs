using CryptoBroker.Application;
using CryptoBroker.Models.Queries;
using CryptoBroker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBroker.OrderService;

public interface IOrderService : IServiceBase
{
    Task<OrderModel> GetActiveOrder(string userId);
    Task<OrderModel> GetOrder(string userId, int orderId);
    Task<List<OrderModel>> GetOrders(GetOrdersQueryModel getOrdersQueryModel);
}
